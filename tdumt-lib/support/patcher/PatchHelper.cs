using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Common.Types.Collections;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.specific;
using TDUModdingLibrary.support.patcher.instructions;

namespace TDUModdingLibrary.support.patcher
{
    /// <summary>
    /// Class to manage and use patch files
    /// </summary>
    public static class PatchHelper
    {
        #region Enumerations and constants
        /// <summary>
        /// List of all run results
        /// </summary>
        public enum RunResult
        {
            OK,
            NotRun,
            RunWithWarnings,
            RunWithErrors
        }

        /// <summary>
        /// Message when beginning instruction
        /// </summary>
        private const string _MSG_RUNNING_INSTRUCTION = "Running instruction #{0} from group '{1}': {2}...";

        /// <summary>
        /// Message when instruction ended with error
        /// </summary>
        private const string _MSG_INSTRUCTION_END_ERR = "Instruction #{0} ended with error.";

        /// <summary>
        /// Message when instruction ended with warning
        /// </summary>
        private const string _MSG_INSTRUCTION_END_WARN = "Instruction #{0} ended with {1} warning(s).";

        /// <summary>
        /// Message when current instruction is disabled
        /// </summary>
        private const string _MSG_INSTRUCTION_DISABLED = "Instruction #{0} is disabled.";

        /// <summary>
        /// Message when current instruction won't be installed (group restriction)
        /// </summary>
        private const string _MSG_INSTRUCTION_REFUSED = "Instruction #{0} from group '{1}' is not wanted.";

        /// <summary>
        /// Message when finishing patch execution without errors
        /// </summary>
        private const string _MSG_FINISHING_OK = "Patch run succesfully.";

        /// <summary>
        /// Message when finishing patch execution with error
        /// </summary>
        private const string _MSG_FINISHING_ERR = "Patch halted because of an error.";

        /// <summary>
        /// Message when finishing patch execution with warning(s)
        /// </summary>
        private const string _MSG_FINISHING_WAR = "Patch run with {0} warning(s).";

        /// <summary>
        /// Message when beginning to run the patch
        /// </summary>
        private const string _MSG_RUNNING_PATCH = "Running patch {0} - {1} instruction(s).";

        /// <summary>
        /// Message displaying chosen groups
        /// </summary>
        private const string _MSG_CHOSEN_GROUPS = "Chosen install groups: {0}.";

        /// <summary>
        /// Message when instruction count = 0
        /// </summary>
        private const string _MSG_NO_INSTRUCTIONS = "There are no instructions to run.";

        /// <summary>
        /// Message when patch was not run
        /// </summary>
        private const string _MSG_NOT_RUN = "Patch was not run.";

        /// <summary>
        /// Message to introduce collected messages during execution
        /// </summary>
        private const string _MSG_COLLECTED_MESSAGES = "Collected messages ({0}):";

        /// <summary>
        /// Message when no message was collected
        /// </summary>
        private const string _MSG_NO_COLLECTED_MESSAGES = "No collected messages.";
        #endregion

        #region Properties
        /// <summary>
        /// Path from the patch is started
        /// </summary>
        public static string CurrentPath
        {
            get { return _CurrentPath; }
        }
        private static string _CurrentPath;

        /// <summary>
        /// Culture used for accessing DB files
        /// </summary>
        public static DB.Culture CurrentCulture
        {
            get { return _CurrentCulture; }
        }
        private static DB.Culture _CurrentCulture = DB.Culture.Global;

        /// <summary>
        /// Optional progress bar to use
        /// </summary>
        public static ProgressBar ProgressBar
        {
            // EVO_100
            set { _ProgressBar = value; }
        }
        private static ProgressBar _ProgressBar;

        /// <summary>
        /// Optional tool strip progress bar to use
        /// </summary>
        public static ToolStripProgressBar TSProgressBar
        {
            // EVO_100
            set { _TSProgressBar = value; }
        }
        private static ToolStripProgressBar _TSProgressBar;

        /// <summary>
        /// Current patch
        /// </summary>
        public static PCH CurrentPatch
        {
            get { return _CurrentPatch; }
        }
        private static PCH _CurrentPatch;

        /// <summary>
        /// List of all messages to report to GUI
        /// </summary>
        public static Collection<string> Messages
        {
            get { return _Messages; }
        }
        private static readonly Collection<string> _Messages = new Collection<string>();
        #endregion

        #region Members
        /// <summary>
        /// Logger for patch execution
        /// </summary>
        private static Log _PatchLog;
        #endregion

        #region Public methods
        /// <summary>
        /// Handles patch execution
        /// </summary>
        /// <param name="patchToRun">Patch instance to execute</param>
        /// <param name="log">Log to store events - can be null</param>
        /// <param name="filePath">Path for patch files</param>
        /// <param name="currentCulture">Culture used in DB files</param>
        /// <param name="installGroups">List of instruction groups to install</param>
        /// <returns></returns>
        public static RunResult RunPatch(PCH patchToRun, Log log, string filePath, DB.Culture currentCulture, List<string> installGroups)
        {
            RunResult result = RunResult.NotRun;
            int warningCount = 0;
            int instructionCount = 1;

            if (patchToRun == null || string.IsNullOrEmpty(filePath))
                return result;

            // Group handling
            if (installGroups == null)
                installGroups = new List<string> {PCH.REQUIRED_GROUP_NAME};

            _InitializeComponents(patchToRun, filePath, currentCulture, log);

            _PatchLog.WriteEvent(string.Format(_MSG_RUNNING_PATCH, patchToRun.FileName, patchToRun.PatchInstructions.Count));
            _PatchLog.WriteEvent(string.Format(_MSG_CHOSEN_GROUPS, List2.ToString(installGroups)));

            // Instruction browsing
            bool isCritical = false;

            if (patchToRun.PatchInstructions.Count == 0)
                _PatchLog.WriteEvent(_MSG_NO_INSTRUCTIONS);
            else
            {
                foreach (PatchInstruction instr in patchToRun.PatchInstructions)
                {
                    if (instr.Enabled)
                    {
                        if (installGroups.Contains(instr.Group.name))
                        {
                            result = _ProcessInstruction(instr, instructionCount);

                            // Result analysis
                            switch (result)
                            {
                                case RunResult.RunWithErrors:
                                    // Critical failure > patch stopped
                                    _PatchLog.WriteEvent(string.Format(_MSG_INSTRUCTION_END_ERR, instr.Order));
                                    isCritical = true;
                                    break;
                                case RunResult.RunWithWarnings:
                                    // Non-critical failure > next instruction
                                    _PatchLog.WriteEvent(string.Format(_MSG_INSTRUCTION_END_WARN, instr.Order, 1));
                                    warningCount++;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                            // Refused instruction
                            _PatchLog.WriteEvent(string.Format(_MSG_INSTRUCTION_REFUSED, instr.Order, instr.Group.name));

                        if (isCritical)
                            break;
                    }
                    else
                        // Disabled instruction
                        _PatchLog.WriteEvent(string.Format(_MSG_INSTRUCTION_DISABLED, instr.Order));

                    // EVO_100: Progress update
                    if (_ProgressBar != null)
                        _ProgressBar.PerformStep();

                    if (_TSProgressBar != null)
                        _TSProgressBar.PerformStep();

                    Application.DoEvents();

                    instructionCount++;
                }
            }

            switch (result)
            {
                case RunResult.NotRun:
                    _PatchLog.WriteEvent(_MSG_NOT_RUN);
                    break;
                case RunResult.RunWithErrors:
                    _PatchLog.WriteEvent(_MSG_FINISHING_ERR);
                    break;
                case RunResult.OK:
                    if (warningCount > 0)
                    {
                        _PatchLog.WriteEvent(string.Format(_MSG_FINISHING_WAR, warningCount));
                        result = RunResult.RunWithWarnings;
                    }
                    else
                        _PatchLog.WriteEvent(_MSG_FINISHING_OK);
                    break;
                default:
                    break;
            }

            // Displaying collected messages
            _DisplayCollectedMessages();

            // Saving Logger to file
            _PatchLog.SaveToFile(_PatchLog.Name);

            return result;
        }

        /// <summary>
        /// Handles single execution
        /// </summary>
        /// <param name="patchToRun">Patch instance to execute</param>
        /// <param name="instructionNumber">Instruction number to execute</param>
        /// <param name="log">Log for message storing</param>
        /// <param name="tduPath">Path for tdu files; can be null = default</param>
        /// <param name="filePath">Path for patch files</param>
        /// <param name="currentCulture"></param>
        /// <returns></returns>
        public static RunResult RunSingleInstruction(PCH patchToRun, int instructionNumber, Log log, string tduPath, string filePath, DB.Culture currentCulture)
        {
            RunResult result = RunResult.NotRun;

            if (patchToRun == null || string.IsNullOrEmpty(filePath))
                return result;

            _InitializeComponents(patchToRun, filePath, currentCulture, log);

            // Instruction finding
            PatchInstruction instr = patchToRun.GetInstruction(instructionNumber);

            if (instr != null)
            {
                result = _ProcessInstruction(instr, instructionNumber);

                switch (result)
                {
                    case RunResult.RunWithWarnings:
                        // Non-critical failure > next instruction
                        _PatchLog.WriteEvent(string.Format(_MSG_INSTRUCTION_END_WARN, instr.Order, 1));
                        break;
                    case RunResult.RunWithErrors:
                        _PatchLog.WriteEvent(_MSG_FINISHING_ERR);
                        break;
                    default:
                        _PatchLog.WriteEvent(_MSG_FINISHING_OK);
                        break;
                }
            }

            // Displaying collected messages
            _DisplayCollectedMessages();

            // Saving Logger to file
            _PatchLog.SaveToFile(_PatchLog.Name);

            return result;
        }

        /// <summary>
        /// Utility method converting specified installer patch to uninstaller one
        /// </summary>
        /// <param name="installerPatch"></param>
        /// <param name="uninstallerPatchFileName"></param>
        public static void ConvertInstallerToUninstaller(PCH installerPatch, string uninstallerPatchFileName)
        {
            if (installerPatch == null)
                throw new Exception("Invalid installer patch specified.");

            PCH uninstallerPatch = TduFile.GetFile(uninstallerPatchFileName) as PCH;

            if (uninstallerPatch == null)
                throw new Exception("Unable to create specified uninstaller patch.");

            // Cloning properties
            installerPatch.DuplicateProperties(uninstallerPatch);

            // Browsing instructions
            int ignoredCount = 0;

            foreach (PatchInstruction patchInstruction in installerPatch.PatchInstructions)
            {
                PatchInstruction correspondingInstruction = patchInstruction.Clone() as PatchInstruction;
                
                // checkPatch instruction is not used in uninstaller
                if ( correspondingInstruction == null
                    || PatchInstruction.InstructionName.checkPatch.ToString().Equals(correspondingInstruction.Name)
                    || PatchInstruction.InstructionName.nothing.ToString().Equals(correspondingInstruction.Name))
                {
                    ignoredCount++;
                    continue;
                }

                // installFile instruction must be converted into uninstallFile, keeping parameter values
                if ( PatchInstruction.InstructionName.installFile.ToString().Equals(correspondingInstruction.Name))
                    correspondingInstruction =
                        PatchInstruction.ChangeInstruction(correspondingInstruction,
                                                           PatchInstruction.InstructionName.uninstallFile);

                // bnkRemap instruction must be converted into bnkUnmap, keeping parameter values
                if (PatchInstruction.InstructionName.bnkRemap.ToString().Equals(correspondingInstruction.Name))
                    correspondingInstruction =
                        PatchInstruction.ChangeInstruction(correspondingInstruction,
                                                           PatchInstruction.InstructionName.bnkUnmap);

                // installPackedFile instruction must be converted into uninstallPackedFile, keeping parameter values
                if (PatchInstruction.InstructionName.installPackedFile.ToString().Equals(correspondingInstruction.Name))
                    correspondingInstruction =
                        PatchInstruction.ChangeInstruction(correspondingInstruction,
                                                           PatchInstruction.InstructionName.uninstallPackedFile);

                // Updates instruction index
                patchInstruction.Order -= ignoredCount;

                // Adding instruction to uninstaller
                uninstallerPatch.SetInstruction(correspondingInstruction);
            }

            // Saving
            uninstallerPatch.Save();
        }
        
        /// <summary>
        /// Adds a message to list
        /// </summary>
        /// <param name="message"></param>
        public static void AddMessage(string message)
        {
            if (message != null && !Messages.Contains(message))
                Messages.Add(message);         
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Initializes standard components for this helper
        /// </summary>
        /// <param name="patchToRun"></param>
        /// <param name="filePath"></param>
        /// <param name="currentCulture"></param>
        /// <param name="log">Log to write events to (optional)</param>
        private static void _InitializeComponents(PCH patchToRun, string filePath, DB.Culture currentCulture, Log log)
        {
            // Testing TDU main folder
            _TestTduInstallPath();

            // Messages
            _Messages.Clear();

            // Current patch
            _CurrentPatch = patchToRun;

            // Current path
            // BUG_64: using specified folder as startup directory
            _CurrentPath = filePath;

            // Culture
            _CurrentCulture = currentCulture;

            // Logger
            if (log == null)
                _PatchLog = new Log(null);
            else
                _PatchLog = log;
        }

        /// <summary>
        /// Processes a single instruction
        /// </summary>
        /// <param name="instr">Instruction to execute</param>
        /// <param name="instructionNumber"></param>
        /// <returns></returns>
        private static RunResult _ProcessInstruction(PatchInstruction instr, int instructionNumber)
        {
            // Logger
            if (_PatchLog != null)
                _PatchLog.WriteEvent(string.Format(_MSG_RUNNING_INSTRUCTION, instructionNumber, instr.Group.name, instr.Comment));

            return instr.Run(_PatchLog);
        }

        /// <summary>
        /// Utility to get TDU install path from parameter or operating system
        /// </summary>
        private static void _TestTduInstallPath()
        {
            string path = Tools.TduPath;

            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                throw new Exception("TDU does not appear to be installed on '" + path + "'.");
        }

        /// <summary>
        /// Writes all collected messages into patch log
        /// </summary>
        private static void _DisplayCollectedMessages()
        {
            if (_PatchLog != null)
            {
                if (Messages.Count == 0)
                    _PatchLog.WriteEvent(_MSG_NO_COLLECTED_MESSAGES);
                else
                {
                    _PatchLog.WriteEvent(string.Format(_MSG_COLLECTED_MESSAGES, Messages.Count));

                    foreach (string message in Messages)
                        _PatchLog.WriteEvent(message);
                }
            }
        }
        #endregion
    }
}
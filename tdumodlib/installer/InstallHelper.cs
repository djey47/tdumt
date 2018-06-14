using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using DjeFramework1.Common.Support.Traces;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.specific;
using TDUModdingLibrary.installer.steps;
using TDUModdingLibrary.support;
using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.installer
{
    /// <summary>
    /// Utility class helping with installing TDU components
    /// </summary>
    public class InstallHelper
    {
        #region Constants
        /// <summary>
        /// Name for default mod
        /// </summary>
        internal const string DEFAULT_MOD_NAME = "NONAME";

        /// <summary>
        /// Folder name for hi_gauge part
        /// </summary>
        internal const string FOLDER_HI_GAUGE_PART = @"\gauges-hi\";

        /// <summary>
        /// Folder name for model part
        /// </summary>
        internal const string FOLDER_LO_GAUGE_PART = @"\gauges-lo\";

        /// <summary>
        /// Name for install flag file
        /// </summary>
        internal const string FILE_INSTALL_FLAG = "installed";

        /// <summary>
        /// String format name for remap flag file
        /// </summary>
        internal const string FORMAT_FILE_REMAP_FLAG = "remap-{0}";
        #endregion

        #region Properties
        /// <summary>
        /// Returns the patch which is currently applied
        /// </summary>
        internal static PCH CurrentPatch
        {
            get { return _CurrentPatch; }
        }
        private static PCH _CurrentPatch;

        /// <summary>
        /// Log which is used to write events
        /// </summary>
        internal static Log Log
        {
            get { return _Log; }
        }
        private static Log _Log;

        /// <summary>
        /// Specified culture for language-wise installation
        /// </summary>
        internal static DB.Culture Culture
        {
            get { return _Culture; }
        }
        private static DB.Culture _Culture;

        /// <summary>
        /// Slot folder
        /// </summary>
        internal static string SlotPath
        {
            get { return _SlotPath; }
            set { _SlotPath = value; }
        }
        private static string _SlotPath;

        /// <summary>
        /// Full path to mod base folder (...\Test Drive Unlimited\Mods\)
        /// </summary>
        internal static string BasePath
        {
            get { return string.Concat(Tools.TduPath, LibraryConstants.FOLDER_MODS); }
        }

        /// <summary>
        /// Current installed mod folder
        /// </summary>
        internal static string ModPath
        {
            get { return _ModPath; }
            set { _ModPath = value; }
        }
        private static string _ModPath;

        /// <summary>
        /// Remapping folder
        /// </summary>
        internal static string RemapPath
        {
            get { return string.Concat(BasePath, @"\", Tools.KEY_REMAPPING_SLOT, @"\"); }
        }

        /// <summary>
        /// List of files to remap/unmap (name, lock code)
        /// </summary>
        internal static Dictionary<string, string> RemappedFiles
        {
            get { return _RemappedFiles; }
        }
        private static readonly Dictionary<string, string> _RemappedFiles = new Dictionary<string, string>();

        /// <summary>
        /// List of chosen install groups
        /// </summary>
        internal static List<string> InstallGroups
        {
            get { return _InstallGroups; }
        }
        private static List<string> _InstallGroups;
        #endregion

        #region Public methods
        /// <summary>
        /// Runs all install tasks
        /// </summary>
        /// <param name="patch"></param>
        /// <param name="patchLog"></param>
        /// <param name="culture"></param>
        /// <param name="isInstall"></param>
        public static void RunAll(PCH patch, Log patchLog, DB.Culture culture, bool isInstall)
        {
            RunAll(patch, patchLog, culture, isInstall, null);
        }

        /// <summary>
        /// Runs all install tasks. Takes chosen install groups into account
        /// </summary>
        /// <param name="patch"></param>
        /// <param name="log"></param>
        /// <param name="culture"></param>
        /// <param name="isInstall"></param>
        /// <param name="chosenInstallGroups"></param>
        public static void RunAll(PCH patch, Log log, DB.Culture culture, bool isInstall, List<string> chosenInstallGroups)
        {
            try
            {
                // 1. Preparatory : folders
                try
                {
                    // Setting transversal values
                    if (patch == null)
                        throw new Exception("No patch specified.");

                    _CurrentPatch = patch;

                    if (log != null)
                        _Log = log;

                    if (string.IsNullOrEmpty(Tools.TduPath))
                        throw new Exception("Invalid TDU install path specified.");

                    _Culture = culture;
                    _InstallGroups = chosenInstallGroups;

                    // Go!
                    PreparatoryTask.Run();
                }
                catch (Exception ex)
                {
                    Log.Error("Install error when executing preparatory task.", ex);
                    throw;
                }

                // 2. Running patch
                try
                {
                    PatchTask.Run();
                }
                catch (Exception ex)
                {
                    Log.Error("Error when running core patch instructions.", ex);
                    throw;
                }

                // 3. Finalization
                try
                {
                    FinalizationTask.Run(isInstall);
                }
                catch (Exception ex)
                {
                    Log.Error("Error when executing finalization task.", ex);
                    throw;
                }
            }
            catch (Exception ex)
            {
                if (isInstall)
                    throw new Exception("Current patch can't be installed:\r\n" + ex.Message, ex);

                throw new Exception("Current patch can't be uninstalled:\r\n" + ex.Message, ex);
            }
        }

        
        /// <summary>
        /// Indicates if community patch is installed
        /// </summary>
        /// <returns></returns>
        public static bool IsCommunityPatchInstalled()
        {
            bool isInstalled = false;

            // Browses all installed mods
            DirectoryInfo di = new DirectoryInfo(BasePath);

            if (di.Exists)
            {
                foreach (DirectoryInfo directoryInfo in di.GetDirectories())
                {
                    if (directoryInfo.Name.Length == 8 && directoryInfo.Name.StartsWith("00000"))
                    {
                        // Directory found. Is flag present ?
                        FileInfo[] flagFi = directoryInfo.GetFiles(FILE_INSTALL_FLAG);

                        if (flagFi.Length == 1)
                        {
                            isInstalled = true;
                            break;
                        }
                    }
                }
            }

            return isInstalled;
        }

        /// <summary>
        /// Indicates if specified mod has already been installed
        /// </summary>
        /// <param name="slotRef"></param>
        /// <param name="modName"></param>
        /// <returns></returns>
        public static bool IsModInstalled(string slotRef, string modName)
        {
            bool isInstalled = false;

            if (!string.IsNullOrEmpty(slotRef))
            {
                string readModName = GetInstalledModName(slotRef);

                if (readModName != null && modName.Equals(readModName))
                    isInstalled = true;
            }

            return isInstalled;
        }

        /// <summary>
        /// Indicates if another mod has already been installed
        /// </summary>
        /// <param name="slotRef"></param>
        /// <param name="modName"></param>
        /// <returns></returns>
        public static bool IsAnotherModInstalled(string slotRef, string modName)
        {
            bool isInstalled = false;

            if (!string.IsNullOrEmpty(slotRef))
            {
                string readModName = GetInstalledModName(slotRef);

                if (readModName != null && !modName.Equals(readModName))
                    isInstalled = true;
            }

            return isInstalled;
        }

        /// <summary>
        /// Indicates if remapped file 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="unlockCode"></param>
        /// <returns></returns>
        public static bool IsSameLockCode(string fileName, string unlockCode)
        {
            bool returnedResult = false;

            if (!string.IsNullOrEmpty(fileName))
            {
                string flagFileName = string.Format(FORMAT_FILE_REMAP_FLAG, fileName);
                string[] flagFileContents = _GetFlagFileContents(Tools.KEY_REMAPPING_SLOT, flagFileName);

                // First item is as following: <unlock code>|<mod name> or <mod name>
                if (flagFileContents.Length > 0)
                {
                    string[] firstItem = flagFileContents[0].Split(Tools.SYMBOL_VALUE_SEPARATOR);

                    if (firstItem.Length == 2)
                    {
                        if (firstItem[0] != null && firstItem[0].Equals(unlockCode))
                            returnedResult = true;
                    }
                    else if (firstItem.Length == 1)
                    {
                        // Old file (no lock code)
                        returnedResult = true;
                    }
                }
            }

            return returnedResult;
        }

        /// <summary>
        /// Returns mods names which have remapped specified file name
        /// </summary>
        /// <param name="fileName">Without extension (assumed=BNK)</param>
        /// <returns></returns>
        public static string GetAlreadyRemappedModNames(string fileName)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(fileName))
            {
                string flagFileName = string.Format(FORMAT_FILE_REMAP_FLAG, fileName);
                string[] flagFileContents = _GetFlagFileContents(Tools.KEY_REMAPPING_SLOT, flagFileName);
                int itemIndex = 0;

                foreach(string anotherName in flagFileContents)
                {
                    // First item must be cut
                    if (itemIndex == 0)
                    {
                        string[] firstItem = anotherName.Split(Tools.SYMBOL_VALUE_SEPARATOR);

                        if (firstItem.Length == 1)
                            sb.Append(firstItem[0]);
                        else
                            sb.Append(firstItem[1]);
                    }
                    else
                        sb.Append(anotherName);

                    itemIndex++;

                    if (itemIndex < flagFileContents.Length)
                        sb.Append("-");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns name of mod which is installed on specified slot
        /// </summary>
        /// <param name="slotRef"></param>
        /// <returns>mod name, or null if no mod is installed</returns>
        public static string GetInstalledModName(string slotRef)
        {
            string returnedName = null;
            string[] flagFileContents = _GetFlagFileContents(slotRef, FILE_INSTALL_FLAG);

            if (flagFileContents != null && flagFileContents.Length == 1)
                returnedName = flagFileContents[0];

            return returnedName;
        }
        #endregion

        #region Internal methods
        /// <summary>
        /// Updates specified flag file with current installed/uninstalled mod
        /// </summary>
        /// <param name="flagFileName"></param>
        /// <param name="isInstall"></param>
        /// <param name="lockCode">(can be null)</param>
        internal static void _UpdateFlagFile(string flagFileName, bool isInstall, string lockCode)
        {
            if (CurrentPatch != null)
            {
                string[] currentContents = _GetFlagFileContents(null, flagFileName);

                if (File.Exists(flagFileName))
                    File.Delete(flagFileName);

                if (isInstall)
                {
                    // Getting current information...
                    Collection<string> modNames = new Collection<string>();
                    StringBuilder sb = new StringBuilder();
                    int itemIndex = 0;

                    using (StreamWriter writer = File.CreateText(flagFileName))
                    {
                        foreach (string anotherLine in currentContents)
                        {
                            if (itemIndex == 0)
                            {
                                if (lockCode != null)
                                {
                                    sb.Append(lockCode);
                                    sb.Append(Tools.SYMBOL_VALUE_SEPARATOR);
                                }

                                string[] firstItem = anotherLine.Split(Tools.SYMBOL_VALUE_SEPARATOR);

                                if (firstItem.Length == 2)
                                {
                                    modNames.Add(firstItem[1]);
                                    sb.Append(firstItem[1]);
                                }
                                else
                                {
                                    modNames.Add(firstItem[0]);
                                    sb.Append(firstItem[0]);
                                }
                            }
                            else
                                sb.Append(anotherLine);

                            sb.Append(Environment.NewLine);
                            itemIndex++;
                        }

                        // New mod name
                        if (!modNames.Contains(CurrentPatch.Name))
                            sb.Append(CurrentPatch.Name);

                        writer.Write(sb);
                    }
                }
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Utility method returning textual contents of specified flag file in specified slot reference. One line by item.
        /// </summary>
        /// <param name="slotRef"></param>
        /// <param name="flagFileName"></param>
        /// <returns></returns>
        private static string[] _GetFlagFileContents(string slotRef, string flagFileName)
        {
            string[] returnedLines = new string[0];

            if (!string.IsNullOrEmpty(flagFileName))
            {
                // Searches for flag file
                string fullPath;
                
                if (slotRef == null)
                    fullPath = flagFileName;
                else
                    fullPath = string.Concat(BasePath, slotRef, @"\", flagFileName);

                if (File.Exists(fullPath))
                {
                    // Opening flag file...
                    try
                    {
                        string contents = File.ReadAllText(fullPath);

                        returnedLines =
                            contents.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
                    }
                    catch (Exception)
                    {
                        Log.Error("Warning ! '" + flagFileName + "' flag file exists but can't be read: " + fullPath, null);
                    }
                }
            }

            return returnedLines;
        }
        #endregion
    }
}
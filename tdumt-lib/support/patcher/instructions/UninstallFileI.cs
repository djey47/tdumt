using System.Collections.Generic;
using System.IO;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.installer;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.providers;
using TDUModdingLibrary.support.patcher.parameters.util;

namespace TDUModdingLibrary.support.patcher.instructions
{
    /// <summary>
    /// Instruction to properly uninstall a file with new modding guidelines
    /// </summary>
    internal class UninstallFileI : PatchInstruction
    {
        /// <summary>
        /// Instruction name
        /// </summary>
        public override string Name
        {
            get { return InstructionName.uninstallFile.ToString(); }
        }

        /// <summary>
        /// Instruction description
        /// </summary>
        public override string Description
        {
            get { return "Provides all required information to uninstall a file."; }
        }

        /// <summary>
        /// List of supported parameters
        /// </summary>
        internal override Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo>
            SupportedParameterInformation
        {
            get
            {
                ParameterInfo fileNameParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.fileName, true);
                ParameterInfo fileTypeParameter = 
                    new ParameterInfo(PatchInstructionParameter.ParameterName.fileType, false);
                ParameterInfo destinationParameter = 
                    new ParameterInfo(PatchInstructionParameter.ParameterName.destination, true);

                return _DefineParameters(fileNameParameter, fileTypeParameter, destinationParameter);
            }
        }

        /// <summary>
        /// What the instruction should do
        /// </summary>
        protected override void _Process()
        {
            // Parameters
            string fileName = _GetParameter(PatchInstructionParameter.ParameterName.fileName);
            string fileType = _GetParameter(PatchInstructionParameter.ParameterName.fileType);
            string destinationDirectory = _GetParameter(PatchInstructionParameter.ParameterName.destination);

            // Copies backup file into TDU contents
            string backupFolder = InstallHelper.SlotPath;

            if (backupFolder == null)
            {
                string message = "Sorry, uninstallFile instruction can only be run within TDU ModAndPlay.";

                if (!PatchHelper.Messages.Contains(message))
                    PatchHelper.Messages.Add(message);
            }
            else
                _UninstallFile(fileName, fileType, backupFolder, destinationDirectory);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="backupDirectory"></param>
        /// <param name="tduFolder"></param>
        private static void _UninstallFile(string name, string type, string backupDirectory, string tduFolder)
        {
            string sourceSubFolder = "";
            
            if (FileTypeVP.TYPE_GAUGES_HIGH.Equals(type))
                sourceSubFolder = InstallHelper.FOLDER_HI_GAUGE_PART;
            else if (FileTypeVP.TYPE_GAUGES_LOW.Equals(type))
                sourceSubFolder = InstallHelper.FOLDER_LO_GAUGE_PART;

            string sourceFileName = string.Concat(backupDirectory, sourceSubFolder, name);
            string destinationFileName = string.Concat(tduFolder, name);

            // Removing read-only flag on destination file
            if (File.Exists(destinationFileName))
                File2.RemoveAttribute(destinationFileName, FileAttributes.ReadOnly);

            // Backup file is kept
            File.Copy(sourceFileName, destinationFileName, true);
        }
    }
}
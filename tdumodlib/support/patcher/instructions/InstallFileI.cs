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
    /// Instruction to properly install a file with new modding guidelines
    /// </summary>
    internal class InstallFileI : PatchInstruction
    {
        /// <summary>
        /// Instruction name
        /// </summary>
        public override string Name
        {
            get { return InstructionName.installFile.ToString(); }
        }

        /// <summary>
        /// Instruction description
        /// </summary>
        public override string Description
        {
            get { return "Provides all required information to install a file."; }
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
                ParameterInfo sourceDirectoryParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.patchDirectory, false);
                ParameterInfo destinationParameter = 
                    new ParameterInfo(PatchInstructionParameter.ParameterName.destination, true);

                return _DefineParameters(fileNameParameter, fileTypeParameter, sourceDirectoryParameter, destinationParameter);
            }
        }

        /// <summary>
        /// What the instruction should do
        /// </summary>
        protected override void _Process()
        {
            // 0. Parameters
            string fileName = _GetParameter(PatchInstructionParameter.ParameterName.fileName);
            string fileType = _GetParameter(PatchInstructionParameter.ParameterName.fileType);
            string sourceDirectory = _GetParameter(PatchInstructionParameter.ParameterName.patchDirectory);
            string destinationDirectory = _GetParameter(PatchInstructionParameter.ParameterName.destination);

            string backupFolder = InstallHelper.SlotPath;
            string modFolder = InstallHelper.ModPath;

            if (backupFolder == null || modFolder == null)
            {
                string message = "Sorry, installFile instruction can only be run within TDU ModAndPlay.";

                PatchHelper.AddMessage(message);
            }
            else
            {
                // 1. Backup
                _BackupFile(fileName, fileType, destinationDirectory, backupFolder);

                // 2. Put patch contents into mod folder
                _GetContents(fileName, fileType, sourceDirectory, modFolder);

                // 3. Install to TDU
                _InstallFile(fileName, fileType, destinationDirectory, modFolder);
            }
        }

        /// <summary>
        /// Get file from patch contents and put it into mod repository
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileType"></param>
        /// <param name="sourceDirectory"></param>
        /// <param name="modFolder"></param>
        private static void _GetContents(string fileName, string fileType, string sourceDirectory, string modFolder)
        {
            string subFolder = "";

            if (FileTypeVP.TYPE_GAUGES_HIGH.Equals(fileType))
                subFolder = InstallHelper.FOLDER_HI_GAUGE_PART;
            else if (FileTypeVP.TYPE_GAUGES_LOW.Equals(fileType))
                subFolder = InstallHelper.FOLDER_LO_GAUGE_PART;

            string sourceFileName = PatchHelper.CurrentPath + @"\" + sourceDirectory + @"\" + fileName;
            string destinationFileName = modFolder + subFolder + fileName;
            
            // Removing read-only flag on destination file
            if (File.Exists(destinationFileName))
                File2.RemoveAttribute(destinationFileName, FileAttributes.ReadOnly);

            // If file with same name already exists in repo, it is overwritten
            File.Copy(sourceFileName, destinationFileName, true);
        }

        /// <summary>
        /// Copy specified patch from mod folder to game files
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="destinationDirectory"></param>
        /// <param name="modFolder"></param>
        private static void _InstallFile(string name, string type, string destinationDirectory, string modFolder)
        {
            string sourceSubFolder = "";
            
            if (FileTypeVP.TYPE_GAUGES_HIGH.Equals(type))
                sourceSubFolder = InstallHelper.FOLDER_HI_GAUGE_PART;
            else if (FileTypeVP.TYPE_GAUGES_LOW.Equals(type))
                sourceSubFolder = InstallHelper.FOLDER_LO_GAUGE_PART;

            string sourceFileName = string.Concat(modFolder, sourceSubFolder, name);
            string destinationFileName = string.Concat(destinationDirectory, name);

            // Removing read-only flag on destination file
            if (File.Exists(destinationFileName))
                File2.RemoveAttribute(destinationFileName, FileAttributes.ReadOnly);

            File.Copy(sourceFileName, destinationFileName, true);
        }

        /// <summary>
        /// Creates backup copy of replaced file
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="sourceDirectory"></param>
        /// <param name="targetFolder"></param>
        private static void _BackupFile(string name, string type, string sourceDirectory, string targetFolder)
        {
            string targetSubFolder = "";

            if (FileTypeVP.TYPE_GAUGES_HIGH.Equals(type))
                targetSubFolder = InstallHelper.FOLDER_HI_GAUGE_PART;
            else if (FileTypeVP.TYPE_GAUGES_LOW.Equals(type))
                targetSubFolder = InstallHelper.FOLDER_LO_GAUGE_PART;

            string destinationFileName = string.Concat(targetFolder,targetSubFolder, name);

            // If backup file already exists, it is kept
            if (!File.Exists(destinationFileName))
            {
                string sourceFileName = string.Concat(sourceDirectory, name);
 
                File.Copy(sourceFileName, destinationFileName); 
            }
        }
    }
}
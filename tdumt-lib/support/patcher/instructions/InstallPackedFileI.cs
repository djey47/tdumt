using System;
using System.Collections.Generic;
using System.IO;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.installer;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.util;

namespace TDUModdingLibrary.support.patcher.instructions
{
    /// <summary>
    /// Instruction to directly replace packed files in BNK
    /// </summary>
    class InstallPackedFileI : PatchInstruction
    {
        #region Constants
        /// <summary>
        /// Suffix to add to packed files folder
        /// </summary>
        internal const string _SUFFIX_PACKED = "_packed";
        #endregion

        #region PatchInstruction overrides
        public override string Name
        {
            get { return InstructionName.installPackedFile.ToString(); }
        }

        public override string Description
        {
            get { return "Provides all required information to install a file into a BNK."; }
        }

        internal override Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo> SupportedParameterInformation
        {
            get
            {
                ParameterInfo bnkFileParameter = new ParameterInfo(PatchInstructionParameter.ParameterName.bnkFile, true);
                ParameterInfo sourceDirectoryParameter = new ParameterInfo(PatchInstructionParameter.ParameterName.patchDirectory, false);
                ParameterInfo newFileParameter = new ParameterInfo(PatchInstructionParameter.ParameterName.newFile, false);

                return _DefineParameters(bnkFileParameter, sourceDirectoryParameter, newFileParameter);
            }
        }

        protected override void _Process()
        {
            // 0. Parameters
            string fileName = _GetParameter(PatchInstructionParameter.ParameterName.newFile);
            string sourceDirectory = _GetParameter(PatchInstructionParameter.ParameterName.patchDirectory);
            string destinationBnkFilePath = _GetParameter(PatchInstructionParameter.ParameterName.bnkFile);

            string backupFolder = InstallHelper.SlotPath;
            string modFolder = InstallHelper.ModPath;

            if (backupFolder == null || modFolder == null)
            {
                const string message = "Sorry, installPackedFile instruction can only be run within TDU ModAndPlay.";

                PatchHelper.AddMessage(message);
            }
            else
            {
                // 1. Backup
                _BackupFile(destinationBnkFilePath, fileName, backupFolder);

                // 2. Put patch contents into mod folder
                _GetContents(destinationBnkFilePath, sourceDirectory, fileName, modFolder);

                // 3. Install to TDU
                _InstallFile(destinationBnkFilePath, fileName, modFolder);
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Creates backup copy of replaced packed file
        /// </summary>
        /// <param name="bnkFilePath"></param>
        /// <param name="packedFileName"></param>
        /// <param name="backupFolder"></param>
        private static void _BackupFile(string bnkFilePath, string packedFileName, string backupFolder)
        {
            FileInfo fi = new FileInfo(bnkFilePath);
            // BUG_84: suffix added to folder
            string backupSubFolder = string.Concat(fi.Name, _SUFFIX_PACKED);
            string destinationFileName = string.Concat(backupFolder, @"\", backupSubFolder, @"\", packedFileName);

            // If backup file already exists, it is kept
            if (!File.Exists(destinationFileName))
            {
                // Loading Bnk to extract file
                BNK bnk = TduFile.GetFile(bnkFilePath) as BNK;

                if (bnk == null || !bnk.Exists)
                    throw new Exception("Invalid BNK file: " + bnkFilePath);
                
                fi = new FileInfo(destinationFileName);

                if (fi.Directory == null)
                    throw new Exception("Invalid file name: " + destinationFileName);
                
                if (!Directory.Exists(fi.Directory.FullName))
                    Directory.CreateDirectory(fi.Directory.FullName);

                string packedFilePath = bnk.GetPackedFilesPaths(packedFileName)[0];

                bnk.ExtractPackedFile(packedFilePath, destinationFileName, false);               
            }
        }

        /// <summary>
        /// Get file from patch contents and put it into mod folder
        /// </summary>
        /// <param name="bnkFilePath"></param>
        /// <param name="sourceDirectory"></param>
        /// <param name="packedFileName"></param>
        /// <param name="modFolder"></param>
        private static void _GetContents(string bnkFilePath, string sourceDirectory, string packedFileName, string modFolder)
        {
            if (sourceDirectory == null)
                sourceDirectory = "";

            FileInfo fi = new FileInfo(bnkFilePath);
            string sourceFileName = string.Concat(PatchHelper.CurrentPath, @"\", sourceDirectory, @"\", packedFileName);
            // BUG_84: suffix added to folder
            string modSubFolder = string.Concat(fi.Name, _SUFFIX_PACKED);
            string destinationFileName = string.Concat(modFolder, @"\", modSubFolder, @"\", packedFileName);

            // If file with same name already exists in repo, it is overwritten
            fi = new FileInfo(destinationFileName);

            if (fi.Directory == null)
                throw new Exception("Invalid file name: " + destinationFileName);

            if (!Directory.Exists(fi.Directory.FullName))
                Directory.CreateDirectory(fi.Directory.FullName);

            // Removing read-only flag on destination file
            if (fi.Exists)
                File2.RemoveAttribute(destinationFileName, FileAttributes.ReadOnly);

            File.Copy(sourceFileName, destinationFileName, true);
        }

        /// <summary>
        /// Integrates file from mod repository to TDU BNK file 
        /// </summary>
        /// <param name="bnkFilePath"></param>
        /// <param name="packedFileName"></param>
        /// <param name="modFolder"></param>
        private static void _InstallFile(string bnkFilePath, string packedFileName, string modFolder)
        {
            FileInfo fi = new FileInfo(bnkFilePath);
            // BUG_84: suffix added to folder
            string modSubFolder = string.Concat(fi.Name, _SUFFIX_PACKED);
            string sourceFileName = string.Concat(modFolder, @"\", modSubFolder, @"\", packedFileName);

            // Replaces file in BNK
            BNK bnk = TduFile.GetFile(bnkFilePath) as BNK;

            if (bnk == null || !bnk.Exists)
                throw new Exception("Invalid BNK file: " + bnkFilePath);
            
            string packedFilePath = bnk.GetPackedFilesPaths(packedFileName)[0];

            bnk.ReplacePackedFile(packedFilePath, sourceFileName);
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.IO;
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
    class UninstallPackedFileI : PatchInstruction
    {
        #region PatchInstruction overrides
        public override string Name
        {
            get
            {
                return InstructionName.uninstallPackedFile.ToString();
            }
        }

        public override string Description
        {
            get { return "Provides all required information to uninstall a file from a BNK."; }
        }

        internal override Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo> SupportedParameterInformation
        {
            get
            {
                ParameterInfo bnkFileParameter = new ParameterInfo(PatchInstructionParameter.ParameterName.bnkFile, true);
                ParameterInfo packedFileNameParameter = new ParameterInfo(PatchInstructionParameter.ParameterName.newFile, true);

                return _DefineParameters(bnkFileParameter, packedFileNameParameter);
            }
        }

        protected override void _Process()
        {
            // Parameters
            string bnkFilePath = _GetParameter(PatchInstructionParameter.ParameterName.bnkFile);
            string packedFileName = _GetParameter(PatchInstructionParameter.ParameterName.newFile);

            string backupFolder = InstallHelper.SlotPath;

            if (backupFolder == null)
            {
                const string message = "Sorry, uninstallPackedFile instruction can only be run within TDU ModAndPlay.";

                PatchHelper.AddMessage(message);
            }
            else
                _UninstallPackedFile(bnkFilePath, packedFileName, backupFolder);
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Replaces packed file in TDU installed files by previous backup
        /// </summary>
        /// <param name="bnkFilePath"></param>
        /// <param name="packedFileName"></param>
        /// <param name="backupFolder"></param>
        private static void _UninstallPackedFile(string bnkFilePath, string packedFileName, string backupFolder)
        {
            FileInfo fi = new FileInfo(bnkFilePath);
            // BUG_84: suffix added to folder
            string backupSubFolder = string.Concat(fi.Name, InstallPackedFileI._SUFFIX_PACKED);
            string backupFileName = string.Concat(backupFolder, @"\", backupSubFolder, @"\", packedFileName);

            // Replaces file in BNK
            BNK bnk = TduFile.GetFile(bnkFilePath) as BNK;

            if (bnk == null || !bnk.Exists)
                throw new Exception("Invalid BNK file: " + bnkFilePath);
            
            string packedFilePath = bnk.GetPackedFilesPaths(packedFileName)[0];

            bnk.ReplacePackedFile(packedFilePath, backupFileName);
        }
        #endregion
    }
}
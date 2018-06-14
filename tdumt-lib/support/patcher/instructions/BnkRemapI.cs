using System;
using System.Collections.Generic;
using TDUModdingLibrary.installer;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.util;

namespace TDUModdingLibrary.support.patcher.instructions
{
    /// <summary>
    /// Instruction to lock shared files
    /// </summary>
    class BnkRemapI : PatchInstruction
    {
        /// <summary>
        /// Instruction name
        /// </summary>
        public override string Name
        {
            get { return InstructionName.bnkRemap.ToString(); }
        }

        /// <summary>
        /// Instruction description
        /// </summary>
        public override string Description
        {
            get { return "Locks specified shared BNK files; generates an error if already locked."; }
        }

        /// <summary>
        /// List of supported parameters
        /// </summary>
        // EVO_81: property added
        internal override Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo>
            SupportedParameterInformation
        {
            get
            {
                ParameterInfo fileNameParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.mappedFileName, true);
                ParameterInfo lockCodeParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.lockCode, false);

                return _DefineParameters(fileNameParameter, lockCodeParameter);
            }
        }

        /// <summary>
        /// What the instruction should do
        /// </summary>
        protected override void _Process()
        {
            // Checking parameters
            string fileName = _GetParameter(PatchInstructionParameter.ParameterName.mappedFileName, false);
            string cleanFileName = fileName.Replace(@"\", "_");
            string lockCode = _GetParameter(PatchInstructionParameter.ParameterName.lockCode, false);

            // Checks if specified BNK file has already been remapped
            string modName = InstallHelper.GetAlreadyRemappedModNames(cleanFileName);

            if (string.Empty.Equals(modName) || modName.Contains(PatchHelper.CurrentPatch.Name) || InstallHelper.IsSameLockCode(cleanFileName, lockCode))
                // Not remapped yet or overwrite allowed, telling installer to create file during finalization
                InstallHelper.RemappedFiles.Add(cleanFileName, lockCode);
            else
            {
                // Already remapped
                string realFileName = _GetParameter(PatchInstructionParameter.ParameterName.mappedFileName, true);
                string message = string.Format("'{0}' mod(s) already replacing following file:\r\n{1}", modName, realFileName);

                PatchHelper.AddMessage(string.Concat(message, Environment.NewLine, "Please uninstall this mod first."));
                throw new Exception(message);
            }
        }
    }
}
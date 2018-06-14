using System.Collections.Generic;
using TDUModdingLibrary.installer;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.util;

namespace TDUModdingLibrary.support.patcher.instructions
{
    /// <summary>
    /// Instruction to remove lock on specified file
    /// </summary>
    class BnkUnmapI : PatchInstruction
    {
        /// <summary>
        /// Instruction name
        /// </summary>
        public override string Name
        {
            get { return InstructionName.bnkUnmap.ToString(); }
        }

        /// <summary>
        /// Instruction description
        /// </summary>
        public override string Description
        {
            get { return "Unlocks specified shared BNK file. To be used in uninstall patch only!"; }
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

                return _DefineParameters(fileNameParameter);
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

            // Checks if specified BNK file has already been remapped
            string modNames = InstallHelper.GetAlreadyRemappedModNames(cleanFileName);

            if (modNames != null && modNames.Contains(PatchHelper.CurrentPatch.Name))
                // Telling installer to remove file lock
                InstallHelper.RemappedFiles.Add(cleanFileName, null);
        }
    }
}
using System;
using System.Collections.Generic;
using TDUModdingLibrary.installer;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.util;

namespace TDUModdingLibrary.support.patcher.instructions
{
    /// <summary>
    /// Instruction checking if latest patch is installed
    /// </summary>
    class CheckPatchI:PatchInstruction
    {
        /// <summary>
        /// Instruction name
        /// </summary>
        public override string Name
        {
            get { return InstructionName.checkPatch.ToString(); }
        }

        /// <summary>
        /// Instruction description
        /// </summary>
        public override string Description
        {
            get { return "Checks if latest patch is installed; generates an error if not."; }
        }

        /// <summary>
        /// List of supported parameters
        /// </summary>
        internal override Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo>
            SupportedParameterInformation
        {
            get { return _DefineParameters(); }
        }

        /// <summary>
        /// What the instruction should do
        /// </summary>
        protected override void _Process()
        {
            // No parameters
            if (!InstallHelper.IsCommunityPatchInstalled())
            {
                PatchHelper.AddMessage("Unable to install current mod as latest Community Patch is required.\r\nGet it at http://www.oahucars-unlimited.com");
                throw new Exception("Community Patch is not installed on this system.");
            }
        }
    }
}
using System.Collections.Generic;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.util;

namespace TDUModdingLibrary.support.patcher.instructions
{
    /// <summary>
    /// Instruction to delete radial.cdb file
    /// </summary>
    class DeleteRadialI:PatchInstruction
    {
        public override string Name
        {
            get {
                return "";/* InstructionName.deleteRadial.ToString();*/ }
        }

        public override string Description
        {
            get { return "Deletes this radial.cdb file.\nNo parameters."; }
        }

        internal override Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo> SupportedParameterInformation
        {
            // No parameter needed
            get { return new Dictionary<PatchInstructionParameter.ParameterName,ParameterInfo>(); }
        }

        protected override void _Process()
        {
            Tools.DeleteRadial();
        }
    }
}
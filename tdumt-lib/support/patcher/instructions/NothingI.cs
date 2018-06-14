using System.Collections.Generic;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.util;

namespace TDUModdingLibrary.support.patcher.instructions
{
    /// <summary>
    /// Default instruction which does nothing !
    /// </summary>
    public class NothingI:PatchInstruction
    {
        public override string Name
        {
            get {
                return InstructionName.nothing.ToString(); }
        }

        public override string Description
        {
            get { return "Does nothing."; }
        }

        internal override Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo> SupportedParameterInformation
        {
            // No parameter needed
            get { return new Dictionary<PatchInstructionParameter.ParameterName,ParameterInfo>(); }
        }

        protected override void _Process()
        {
            // Does nothing
        }
    }
}
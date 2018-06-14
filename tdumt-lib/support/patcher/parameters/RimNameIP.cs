using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    /// <summary>
    /// Parameter related to a new name for rim
    /// </summary>
    class RimNameIP:PatchInstructionParameter
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public override string Name
        {
            get { return ParameterName.rimName.ToString(); }
        }

        /// <summary>
        /// Parameter description
        /// </summary>
        public override string Description
        {
            get { return "New name for rim."; }
        }

        public override IValuesProvider DefaultValuesProvider
        {
            get { return null; }
        }
    }
}

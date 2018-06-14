using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    class ResourceValuesIP:PatchInstructionParameter
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public override string Name
        {
            get { return ParameterName.resourceValues.ToString(); }
        }

        /// <summary>
        /// Parameter description
        /// </summary>
        public override string Description
        {
            get { return "Specifies list of couples (id, value) to set into resource file. Following: id1|value1||id2|value2 etc."; }
        }

        /// <summary>
        /// Provider for common parameter values
        /// </summary>
        public override IValuesProvider DefaultValuesProvider
        {
            get { return null; }
        }
    }
}

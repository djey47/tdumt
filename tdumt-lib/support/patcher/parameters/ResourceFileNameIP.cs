using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    class ResourceFileNameIP:PatchInstructionParameter
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public override string Name
        {
            get { return ParameterName.resourceFileName.ToString(); }
        }

        /// <summary>
        /// Parameter description
        /// </summary>
        public override string Description
        {
            get {
                return "Specifies which resource file to be updated."; }
        }

        /// <summary>
        /// Provider for common parameter values
        /// </summary>
        public override IValuesProvider DefaultValuesProvider
        {
            get { return new ResourceFileNameVP(); }
        }
    }
}
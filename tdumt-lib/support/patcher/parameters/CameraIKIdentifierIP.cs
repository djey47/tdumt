using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    class CameraIKIdentifierIP : PatchInstructionParameter
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public override string Name
        {
            get { return ParameterName.cameraIKIdentifier.ToString(); }
        }

        /// <summary>
        /// Parameter description
        /// </summary>
        public override string Description
        {
            get { return "Identifier for a camera or IK set."; }
        }

        /// <summary>
        /// Provider for common parameter values
        /// </summary>
        public override IValuesProvider DefaultValuesProvider
        {
            get { return new CameraIKVP(); }
        }
    }
}
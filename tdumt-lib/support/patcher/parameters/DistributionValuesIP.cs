using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    /// <summary>
    /// Parameter to specify distribution values
    /// </summary>
    class DistributionValuesIP:PatchInstructionParameter
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public override string Name
        {
            get { return ParameterName.distributionValues.ToString(); }
        }

        /// <summary>
        /// Parameter description
        /// </summary>
        public override string Description
        {
            get { return "Distribution data: zone|value||... couples. zone is a number from 1 to 6, value is a float."; }
        }

        /// <summary>
        /// Default values provider
        /// </summary>
        public override IValuesProvider DefaultValuesProvider
        {
            get { return new MapZonesVP(); }
        }
    }
}
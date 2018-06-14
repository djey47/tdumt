using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    /// <summary>
    /// Parameter to specify id of traffic vehicle
    /// </summary>
    class TrafficVehicleIdIP:PatchInstructionParameter
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public override string Name
        {
            get { return ParameterName.trafficVehicleId.ToString(); }
        }

        /// <summary>
        /// Parameter description
        /// </summary>
        public override string Description
        {
            get { return "Identifier of traffic vehicle to modify (ID_Car information in TDU database)"; }
        }

        /// <summary>
        /// Default values provider
        /// </summary>
        public override IValuesProvider DefaultValuesProvider
        {
            get { return null; }
        }
    }
}
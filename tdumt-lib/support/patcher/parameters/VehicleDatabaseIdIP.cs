using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    class VehicleDatabaseIdIP:DatabaseIdIP
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public override string Name
        {
            get { return ParameterName.vehicleDatabaseId.ToString(); }
        }

        /// <summary>
        /// Parameter description
        /// </summary>
        public override string Description
        {
            get { return "Identifier of vehicle to modify."; }
        }

        public override IValuesProvider DefaultValuesProvider
        {
            get { return new VehiclesVP() ; }
        }
    }
}
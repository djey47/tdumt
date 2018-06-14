using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    abstract class AbstractCockpitPositionIP:PatchInstructionParameter
    {
        #region Overrides of PatchInstructionParameter
        /// <summary>
        /// Default values provider
        /// </summary>
        public override IValuesProvider DefaultValuesProvider
        {
            get { return new CockpitPositionVP(); }
        }

        /// <summary>
        /// Parameter description
        /// </summary>
        public override string Description
        {
            get {
                return "Position for source/target of cockpit cam."; }
        }
        #endregion
    }
}

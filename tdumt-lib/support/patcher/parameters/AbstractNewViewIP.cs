using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    abstract class AbstractNewViewIP:PatchInstructionParameter
    {
        #region Overrides of PatchInstructionParameter

        /// <summary>
        /// Parameter description
        /// </summary>
        public override string Description
        {
            get { return "Camera and view type to use for this view, following: <camId>|<viewType>"; }
        }

        /// <summary>
        /// Default values provider
        /// </summary>
        public override IValuesProvider DefaultValuesProvider
        {
            get { return new CameraViewsIKVP(); }
        }

        #endregion
    }
}

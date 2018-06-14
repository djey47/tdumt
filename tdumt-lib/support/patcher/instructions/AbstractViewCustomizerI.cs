using System;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.binaries;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.support.constants;
using TDUModdingLibrary.support.patcher.parameters;

namespace TDUModdingLibrary.support.patcher.instructions
{
    abstract class AbstractViewCustomizerI:PatchInstruction
    {
        #region Members
        /// <summary>
        /// File name for camera reference
        /// </summary>
        protected string camReferenceFilename;
        #endregion

        #region Protected methods
        /// <summary>
        /// What the instruction should do
        /// </summary>
        protected void _CommonProcess()
        {
            // Common parameters
            string camId = _GetParameter(PatchInstructionParameter.ParameterName.cameraIKIdentifier);
            string hoodView = _GetParameter(PatchInstructionParameter.ParameterName.newHoodView);
            string hoodBackView = _GetParameter(PatchInstructionParameter.ParameterName.newHoodBackView);
            string cockpitView = _GetParameter(PatchInstructionParameter.ParameterName.newCockpitView);
            string cockpitBackView = _GetParameter(PatchInstructionParameter.ParameterName.newCockpitBackView);

            // Parameter validation
            string hoodCamId = hoodView.Split(Tools.SYMBOL_VALUE_SEPARATOR)[0];
            Cameras.ViewType hoodViewType = _ValidateViewType(hoodView.Split(Tools.SYMBOL_VALUE_SEPARATOR)[1]);
            string hoodBackCamId = hoodBackView.Split(Tools.SYMBOL_VALUE_SEPARATOR)[0];
            Cameras.ViewType hoodBackViewType = _ValidateViewType(hoodBackView.Split(Tools.SYMBOL_VALUE_SEPARATOR)[1]);
            string cockpitCamId = cockpitView.Split(Tools.SYMBOL_VALUE_SEPARATOR)[0];
            Cameras.ViewType cockpitViewType = _ValidateViewType(cockpitView.Split(Tools.SYMBOL_VALUE_SEPARATOR)[1]);
            string cockpitBackCamId = cockpitBackView.Split(Tools.SYMBOL_VALUE_SEPARATOR)[0];
            Cameras.ViewType cockpitBackViewType = _ValidateViewType(cockpitBackView.Split(Tools.SYMBOL_VALUE_SEPARATOR)[1]);

            // Loading current camera
            string currentCamFile = LibraryConstants.GetSpecialFile(LibraryConstants.TduSpecialFile.CamerasBin);
            Cameras currentCameras = TduFile.GetFile(currentCamFile) as Cameras;

            if (currentCameras == null || !currentCameras.Exists)
                throw new Exception("Unable to load current camera data: " + currentCamFile);

            // Loading default camera
            Cameras defaultCameras = TduFile.GetFile(camReferenceFilename) as Cameras;

            if (defaultCameras == null || !defaultCameras.Exists)
                throw new Exception("Unable to load new camera data: " + camReferenceFilename);

            // Views
            VehicleSlotsHelper.InitReference(Tools.WorkingPath + LibraryConstants.FOLDER_XML);

            _Customize(currentCameras, defaultCameras, camId, Cameras.ViewType.Hood, hoodCamId, hoodViewType);
            _Customize(currentCameras, defaultCameras, camId, Cameras.ViewType.Hood_Back, hoodBackCamId, hoodBackViewType);
            _Customize(currentCameras, defaultCameras, camId, Cameras.ViewType.Cockpit, cockpitCamId, cockpitViewType);
            _Customize(currentCameras, defaultCameras, camId, Cameras.ViewType.Cockpit_Back, cockpitBackCamId, cockpitBackViewType);

            // Saving
            currentCameras.Save();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Validates then return specified view type code
        /// </summary>
        /// <param name="viewTypeCode"></param>
        /// <returns></returns>
        private static Cameras.ViewType _ValidateViewType(string viewTypeCode)
        {
            Cameras.ViewType returnedViewType;

            try
            {
                int viewType = int.Parse(viewTypeCode);

                returnedViewType = (Cameras.ViewType)viewType;
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid view type specified: " + viewTypeCode, ex);
            }

            return returnedViewType;
        }

        /// <summary>
        /// Customizes specified camera view
        /// </summary>
        /// <param name="currentData"></param>
        /// <param name="defaultData"></param>
        /// <param name="camToChange"></param>
        /// <param name="viewToChange"></param>
        /// <param name="camIdToTake"></param>
        /// <param name="viewToTake"></param>
        private static void _Customize(Cameras currentData, Cameras defaultData, string camToChange, Cameras.ViewType viewToChange, string camIdToTake, Cameras.ViewType viewToTake)
        {
            Cameras.CamEntry entryToChange = currentData.GetEntryByCameraId(camToChange);
            Cameras.CamEntry entryToTake = defaultData.GetEntryByCameraId(camIdToTake);

            VehicleSlotsHelper.CustomizeCameraView(currentData, entryToChange, viewToChange, entryToTake, viewToTake);
        }
        #endregion
    }
}

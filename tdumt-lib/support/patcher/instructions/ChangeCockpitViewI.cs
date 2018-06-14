using System;
using System.Collections.Generic;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.binaries;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.support.constants;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.util;

namespace TDUModdingLibrary.support.patcher.instructions
{
    class ChangeCockpitViewI:PatchInstruction
    {
        #region Overrides of PatchInstruction

        /// <summary>
        /// Instruction name
        /// </summary>
        public override string Name
        {
            get { return InstructionName.changeCockpitView.ToString(); }
        }

        /// <summary>
        /// Instruction description
        /// </summary>
        public override string Description
        {
            get {
                return "Allows to switch cockpit camera source and target over 3 presets. To be used AFTER customizeViews instruction, if any."; }
        }

        /// <summary>
        /// List of supported parameters
        /// </summary>
        internal override Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo> SupportedParameterInformation
        {
            get
            {
                ParameterInfo cameraIdParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.cameraIKIdentifier, true);
                ParameterInfo viewSourceParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.viewSource, true);
                ParameterInfo viewTargetParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.viewTarget, true);

                return _DefineParameters(cameraIdParameter, viewSourceParameter, viewTargetParameter);
            }
        }

        /// <summary>
        /// What the instruction should do
        /// </summary>
        protected override void _Process()
        {
            // Parameters
            string camId = _GetParameter(PatchInstructionParameter.ParameterName.cameraIKIdentifier);
            string viewSource = _GetParameter(PatchInstructionParameter.ParameterName.viewSource);
            Cameras.Position sourcePosition = _ValidatePosition(viewSource);
            string viewTarget = _GetParameter(PatchInstructionParameter.ParameterName.viewTarget);
            Cameras.Position targetPosition = _ValidatePosition(viewTarget);

            // Loading current camera
            string currentCamFile = LibraryConstants.GetSpecialFile(LibraryConstants.TduSpecialFile.CamerasBin);
            Cameras currentCameras = TduFile.GetFile(currentCamFile) as Cameras;

            if (currentCameras == null || !currentCameras.Exists)
                throw new Exception("Unable to load current camera data: " + currentCamFile);

            // Retrieving entry
            Cameras.CamEntry entryToEdit = currentCameras.GetEntryByCameraId(camId);

            VehicleSlotsHelper.CustomizeCameraPosition(currentCameras, entryToEdit, Cameras.ViewType.Cockpit, sourcePosition, targetPosition);

            // Saving
            currentCameras.Save();
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Validates then return specified position code
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private static Cameras.Position _ValidatePosition(string pos)
        {
            Cameras.Position returnedPosition;

            try
            {
                int viewPosition = int.Parse(pos);

                returnedPosition = (Cameras.Position) viewPosition;
            }
            catch(Exception ex)
            {
                throw new Exception("Invalid view position specified: " + pos, ex);
            }

            return returnedPosition;
        }
        #endregion
    }
}
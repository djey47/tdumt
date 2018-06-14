using System.Collections.Generic;
using TDUModdingLibrary.fileformats.binaries;
using TDUModdingLibrary.support.constants;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.util;

namespace TDUModdingLibrary.support.patcher.instructions
{
    class CustomizeViewsI:AbstractViewCustomizerI
    {
        #region Overrides of PatchInstruction
        /// <summary>
        /// Instruction name
        /// </summary>
        public override string Name
        {
            get { return InstructionName.customizeViews.ToString(); }
        }

        /// <summary>
        /// Instruction description
        /// </summary>
        public override string Description
        {
            get { return "Allows to customize views of specified camera with those of other cameras."; }
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
                ParameterInfo newHoodViewParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.newHoodView, true);
                ParameterInfo newHoodBackViewParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.newHoodBackView, true);
                ParameterInfo newCockpitViewParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.newCockpitView, true);
                ParameterInfo newCockpitBackViewParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.newCockpitBackView, true);

                return _DefineParameters(cameraIdParameter, newHoodViewParameter, newHoodBackViewParameter, newCockpitViewParameter, newCockpitBackViewParameter);
            }
        }

        /// <summary>
        /// What the instruction should do
        /// </summary>
        protected override void _Process()
        {
            // Reference is standard bin backup
            camReferenceFilename = string.Concat(PatchHelper.CurrentPath, LibraryConstants.FOLDER_XML, LibraryConstants.FOLDER_DEFAULT, Cameras.FILE_CAMERAS_BIN);

            // Calling parent class
            _CommonProcess();
        }
        #endregion
    }
}
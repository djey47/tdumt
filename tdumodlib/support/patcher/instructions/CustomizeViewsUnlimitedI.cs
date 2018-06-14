using System.Collections.Generic;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.util;

namespace TDUModdingLibrary.support.patcher.instructions
{
    class CustomizeViewsUnlimitedI:AbstractViewCustomizerI
    {
        #region Overrides of PatchInstruction
        /// <summary>
        /// Instruction name
        /// </summary>
        public override string Name
        {
            get { return InstructionName.customizeViewsUnlimited.ToString(); }
        }

        /// <summary>
        /// Instruction description
        /// </summary>
        public override string Description
        {
            get { return "Allows to customize views of specified camera with those of other cameras. A custom cameras file must be specified."; }
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
                ParameterInfo camerasFile =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.fileName, true);

                return _DefineParameters(cameraIdParameter, newHoodViewParameter, newHoodBackViewParameter, newCockpitViewParameter, newCockpitBackViewParameter, camerasFile);
            }
        }

        /// <summary>
        /// What the instruction should do
        /// </summary>
        protected override void _Process()
        {
            // Reference is provided with parameters
            camReferenceFilename = _GetParameter(PatchInstructionParameter.ParameterName.fileName);

            // Calling parent class
            _CommonProcess();
        }
        #endregion
    }
}
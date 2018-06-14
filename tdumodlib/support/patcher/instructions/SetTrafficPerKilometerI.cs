using System;
using System.Collections.Generic;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.xml;
using TDUModdingLibrary.support.constants;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.util;

namespace TDUModdingLibrary.support.patcher.instructions
{
    class SetTrafficPerKilometerI:PatchInstruction
    {
        #region Overrides of PatchInstruction

        /// <summary>
        /// Instruction name
        /// </summary>
        public override string Name
        {
            get { return InstructionName.setTrafficPerKilometer.ToString(); }
        }

        /// <summary>
        /// Instruction description
        /// </summary>
        public override string Description
        {
            get {   
                return "Changes traffic count per kilometer for various zones.\r\n- distributionValues: <zone number1>|<traffic count1>||<zone number2>|<traffic count2>..."; }
        }

        /// <summary>
        /// List of supported parameters
        /// </summary>
        // EVO_81: property added
        internal override Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo> SupportedParameterInformation
        {
            get
            {
                ParameterInfo countValuesParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.distributionValues, true);

                return _DefineParameters(countValuesParameter);
            }
        }

        /// <summary>
        /// What the instruction should do
        /// </summary>
        protected override void _Process()
        {
            // Parameters
            string countValues = _GetParameter(PatchInstructionParameter.ParameterName.distributionValues);

            // Using TduFile impl
            string xmlFullPath = LibraryConstants.GetSpecialFile(LibraryConstants.TduSpecialFile.AIConfig);
            AIConfig aiConfigFile = TduFile.GetFile(xmlFullPath) as AIConfig;

            if (aiConfigFile == null || !aiConfigFile.Exists)
                throw new Exception("Invalid AI configuration file: " + xmlFullPath);
            // Parsing distribution values
            string[] countCouples = countValues.Split(new[] { Tools.SYMBOL_VALUE_SEPARATOR2 }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string anotherCouple in countCouples)
            {
                string[] zoneValue = anotherCouple.Split(Tools.SYMBOL_VALUE_SEPARATOR);

                if (zoneValue.Length == 2)
                {
                    int zoneId = int.Parse(zoneValue[0]);

                    aiConfigFile.SetTrafficVehicleCountPerKilometer(zoneId, zoneValue[1]);
                }
                else
                    throw new Exception("Invalid count parameter specified: " + anotherCouple);
            }

            // Saving
            aiConfigFile.Save();
        }
        #endregion
    }
}
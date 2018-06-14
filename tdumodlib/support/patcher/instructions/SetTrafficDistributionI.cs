using System;
using System.Collections.Generic;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.xml;
using TDUModdingLibrary.support.constants;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.util;

namespace TDUModdingLibrary.support.patcher.instructions
{
    /// <summary>
    /// Instruction to define traffic distribution over the 6 TDU zones
    /// </summary>
    class SetTrafficDistributionI:PatchInstruction
    {
        /// <summary>
        /// Instruction name
        /// </summary>
        public override string Name
        {
            get {
                return InstructionName.setTrafficDistribution.ToString(); }
        }

        /// <summary>
        /// Instruction description
        /// </summary>
        public override string Description
        {
            get {
                return
                    "Changes distribution set of specified traffic vehicle.\r\n- trafficVehicleId: id of traffic vehicle to be modified.\r\n- distributionValues: <zone number1>|<distribution index1>||<zone number2>|<index2>..."; }
        }

        /// <summary>
        /// List of supported parameters
        /// </summary>
        // EVO_81: property added
        internal override Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo>
            SupportedParameterInformation
        {
            get
            {
                ParameterInfo trafficVehicleIdParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.trafficVehicleId, true);
                ParameterInfo distributionValuesParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.distributionValues, true);

                return _DefineParameters(trafficVehicleIdParameter, distributionValuesParameter);
            }
        }

        /// <summary>
        /// What the instruction should do
        /// </summary>
        protected override void _Process()
        {
            // Parameters
            string trafficVehicleId = _GetParameter(PatchInstructionParameter.ParameterName.trafficVehicleId);
            string distributionValues = _GetParameter(PatchInstructionParameter.ParameterName.distributionValues);

            // Using TduFile impl
            string xmlFullPath = LibraryConstants.GetSpecialFile(LibraryConstants.TduSpecialFile.AIConfig);
            AIConfig aiConfigFile = TduFile.GetFile(xmlFullPath) as AIConfig;

            if (aiConfigFile == null || !aiConfigFile.Exists)
                throw new Exception("Invalid AI configuration file: " + xmlFullPath);
            // Parsing distribution values
            string[] distriCouples = distributionValues.Split(new[] { Tools.SYMBOL_VALUE_SEPARATOR2 }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string anotherCouple in distriCouples)
            {
                string[] zoneValue = anotherCouple.Split(Tools.SYMBOL_VALUE_SEPARATOR);

                if (zoneValue.Length == 2)
                {
                    int zoneId = int.Parse(zoneValue[0]);

                    aiConfigFile.SetTrafficVehicleDistribution(trafficVehicleId, zoneId, zoneValue[1]);
                }
                else
                    throw new Exception("Invalid distribution parameter specified: " + anotherCouple);
            }

            // Saving
            aiConfigFile.Save();
        }
    }
}
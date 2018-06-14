using System.Collections.ObjectModel;
using TDUModdingLibrary.support.patcher.vars;

namespace TDUModdingLibrary.support.patcher.parameters.providers
{
    class MappableFilesVP:IValuesProvider
    {
        #region IValuesProvider Members
        /// <summary>
        /// Provides common values for this type of instruction
        /// </summary>
        public Collection<string> Values
        {
            get
            {
                Collection<string> valuesList = new Collection<string>();

                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "AC_427"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "CLK_TC"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "Cobalt"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "Corv_63"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "Diablo_GT"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "G35_Cpe"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "hud01"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "Hud02"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "hud06"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "hud13"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "hud14"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "hud19"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "Judge"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "Lotec"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "MX5_Rst"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "Quattroporte"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "RX8"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "Sixteen"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "Solstice"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "XKR_Conv"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesHighPath, "Yes_RST"));

                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "AC_427"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "CLK_TC"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "Cobalt"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "Corv_63"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "Diablo_GT"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "G35_Cpe"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "hud01"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "Hud02"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "hud06"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "hud13"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "hud14"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "hud19"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "Judge"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "Lotec"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "MX5_Rst"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "Quattroporte"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "RX8"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "Sixteen"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "Solstice"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "XKR_Conv"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkGaugesLowPath, "Yes_RST"));

                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkSoundVehiclePath, "Default_audio"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkSoundVehiclePath, "Default_Bike_audio"));

                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkVehiclePath, "Default"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkVehiclePath, "Default_Bike"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkVehiclePath, "Default_I"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkVehiclePath, "Default_Bike_I"));

                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkVehicleRimPath, @"AC\AC_427_F_01"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkVehicleRimPath, @"Brabus\Brabus_F_01"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkVehicleRimPath, @"Brabus\Brabus_F_02"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkVehicleRimPath, @"Citroen\2CV_F_01"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkVehicleRimPath, @"Default\Default_F_01"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkVehicleRimPath, @"Gramlights\57S_pro_Fin_01"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkVehicleRimPath, @"Gramlights\57S_pro_Fin_02"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkVehicleRimPath, @"Kawasaki\Z10R_R_F_01"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkVehicleRimPath, @"Kawazaki\ZX10R_F_01"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkVehicleRimPath, @"Kawazaki\ZX10R_R_01"));
                valuesList.Add(_BuildFileFullName(PatchVariable.VariableName.bnkVehicleRimPath, @"Kawazki\Zx10R_F_01"));

                // TODO: to complete

                return valuesList;
            }
        }

        /// <summary>
        /// Returns real value from chosen label (to handle id-label associations)
        /// </summary>
        /// <param name="chosenLabel"></param>
        /// <returns></returns>
        public string GetValueFromLabel(string chosenLabel)
        {
            return chosenLabel;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Returns full name for specified file and folder variable names
        /// </summary>
        /// <param name="folderVarName"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string _BuildFileFullName(PatchVariable.VariableName folderVarName, string fileName)
        {
            string returnedName = null;

            if (!string.IsNullOrEmpty(fileName))
            {
                string fullVarName = PatchVariable.GetFullName(folderVarName);

                returnedName = string.Concat(fullVarName, @"\", fileName);
            }

            return returnedName;
        }
        #endregion
    }
}
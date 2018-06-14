using System.Collections.ObjectModel;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.support.patcher.parameters.providers
{
    class SlotNamesVP : IValuesProvider
    {
        #region IValuesProvider Members
        /// <summary>
        /// Provides common values for this type of instruction
        /// </summary>
        public Collection<string> Values
        {
            get
            {
                Collection<string> returnValues = new Collection<string>();

                // Gets slot list from reference
                VehicleSlotsHelper.InitReference(Tools.WorkingPath + LibraryConstants.FOLDER_XML);

                foreach (string vehicleName in VehicleSlotsHelper.SlotReference.Keys)
                    returnValues.Add(vehicleName);

                return returnValues;
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
    }
}
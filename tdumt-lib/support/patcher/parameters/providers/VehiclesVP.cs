using System.Collections.Generic;
using System.Collections.ObjectModel;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.support.patcher.parameters.providers
{
    /// <summary>
    /// Provider for vehicle ref from understable name
    /// </summary>
    class VehiclesVP:IValuesProvider
    {
        #region Constants
        /// <summary>
        /// Format for slot name
        /// </summary>
        private static readonly string _FORMAT_VEHICLE_SLOT = "{0}" + Tools.SYMBOL_VALUE_SEPARATOR + "{1}";
        #endregion

        #region Members
        /// <summary>
        /// Index of returned ids by displayed values
        /// </summary>
        private readonly Dictionary<string, string> idsByValues = new Dictionary<string, string>();
        #endregion

        #region IValuesProvider Members
        /// <summary>
        /// Provides common values for this type of instruction
        /// </summary>
        public Collection<string> Values
        {
            get
            {
                Collection<string> returnedList = new Collection<string>();

                // Parsing reference
                VehicleSlotsHelper.InitReference(Tools.WorkingPath + LibraryConstants.FOLDER_XML);

                foreach (KeyValuePair<string, string> keyValuePair in VehicleSlotsHelper.SlotReference)
                {
                    string displayedValue = string.Format(_FORMAT_VEHICLE_SLOT, keyValuePair.Key, keyValuePair.Value);

                    returnedList.Add(displayedValue);
                    idsByValues.Add(displayedValue, keyValuePair.Value);
                }

                return returnedList;
            }
        }

        /// <summary>
        /// Returns real value from chosen label (to handle id-label associations)
        /// </summary>
        /// <param name="chosenLabel"></param>
        /// <returns></returns>
        public string GetValueFromLabel(string chosenLabel)
        {
            return idsByValues[chosenLabel];
        }

        #endregion
    }
}
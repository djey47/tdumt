using System.Collections.Generic;
using System.Collections.ObjectModel;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.support.patcher.parameters.providers
{
    class CameraIKVP : IValuesProvider
    {
        #region Constants
        /// <summary>
        /// Format for slot name
        /// </summary>
        private static readonly string _FORMAT_CAMERA_SLOT = "{0}" + Tools.SYMBOL_VALUE_SEPARATOR + "{1}";
        #endregion

        #region Members
        /// <summary>
        /// Index of all ids by values
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
                Collection<string> returnList = new Collection<string>();

                // Using reference...
                VehicleSlotsHelper.InitReference(Tools.WorkingPath + LibraryConstants.FOLDER_XML);

                foreach (KeyValuePair<string, string> camEntry in VehicleSlotsHelper.CamReference)
                {
                    string displayedValue = string.Format(_FORMAT_CAMERA_SLOT, camEntry.Key, camEntry.Value);

                    returnList.Add(displayedValue);
                    idsByValues.Add(displayedValue, camEntry.Key);
                }

                return returnList;
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
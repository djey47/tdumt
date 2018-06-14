using System.Collections.Generic;
using System.Collections.ObjectModel;
using TDUModdingLibrary.fileformats.binaries;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.support.patcher.parameters.providers
{
    class CameraViewsIKVP:IValuesProvider
    {
        #region Constants
        /// <summary>
        /// Format for camera slot name
        /// </summary>
        private static readonly string _FORMAT_CAMERA_SLOT = "[CAM] {0}" + Tools.SYMBOL_VALUE_SEPARATOR + "{1}";

        /// <summary>
        /// Format for view type
        /// </summary>
        private static readonly string _FORMAT_VIEW_TYPE = "[VIEW] {0}" + Tools.SYMBOL_VALUE_SEPARATOR + "{1}";
        #endregion

        #region Members
        /// <summary>
        /// Index of all ids by values
        /// </summary>
        private readonly Dictionary<string, string> idsByValues = new Dictionary<string, string>();
        #endregion

        #region Implementation of IValuesProvider
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

                // All cameras
                foreach (KeyValuePair<string, string> camEntry in VehicleSlotsHelper.CamReference)
                {
                    string displayedValue = string.Format(_FORMAT_CAMERA_SLOT, camEntry.Key, camEntry.Value);

                    returnList.Add(displayedValue);
                    idsByValues.Add(displayedValue, camEntry.Key);
                }

                // Handled view types
                string viewDisplayedValue = string.Format(_FORMAT_VIEW_TYPE, (int) Cameras.ViewType.Cockpit, "Cockpit");

                returnList.Add(viewDisplayedValue);
                idsByValues.Add(viewDisplayedValue, ((int)Cameras.ViewType.Cockpit).ToString());

                viewDisplayedValue = string.Format(_FORMAT_VIEW_TYPE, (int)Cameras.ViewType.Cockpit_Back, "Cockpit (back)");
                returnList.Add(viewDisplayedValue);
                idsByValues.Add(viewDisplayedValue, ((int)Cameras.ViewType.Cockpit_Back).ToString());
                
                viewDisplayedValue = string.Format(_FORMAT_VIEW_TYPE, (int)Cameras.ViewType.Hood, "Hood");
                returnList.Add(viewDisplayedValue);
                idsByValues.Add(viewDisplayedValue, ((int)Cameras.ViewType.Hood).ToString());

                viewDisplayedValue = string.Format(_FORMAT_VIEW_TYPE, (int)Cameras.ViewType.Hood_Back, "Hood (back)");
                returnList.Add(viewDisplayedValue);
                idsByValues.Add(viewDisplayedValue, ((int)Cameras.ViewType.Hood_Back).ToString());

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
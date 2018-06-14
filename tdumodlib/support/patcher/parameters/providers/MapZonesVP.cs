using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TDUModdingLibrary.support.patcher.parameters.providers
{
    /// <summary>
    /// Provides values for TDU's map zones
    /// </summary>
    class MapZonesVP:IValuesProvider
    {
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

                string displayedValue = "[ZONE 1] Dead zone";

                returnList.Add(displayedValue);
                idsByValues.Add(displayedValue,"1");

                displayedValue = "[ZONE 2] Industrial areas, highways and towns";
                returnList.Add(displayedValue);
                idsByValues.Add(displayedValue,"2");

                displayedValue = "[ZONE 3] Honolulu's downtown";
                returnList.Add(displayedValue);
                idsByValues.Add(displayedValue,"3");

                displayedValue = "[ZONE 4] Plain roads";
                returnList.Add(displayedValue);
                idsByValues.Add(displayedValue,"4");

                displayedValue = "[ZONE 5] Winding roads";
                returnList.Add(displayedValue);
                idsByValues.Add(displayedValue,"5");

                displayedValue = "[ZONE 6] Coastal ways";
                returnList.Add(displayedValue);
                idsByValues.Add(displayedValue,"6");

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
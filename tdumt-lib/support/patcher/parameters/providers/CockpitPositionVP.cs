using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TDUModdingLibrary.fileformats.binaries;

namespace TDUModdingLibrary.support.patcher.parameters.providers
{
    class CockpitPositionVP:IValuesProvider
    {
        #region Members
        /// <summary>
        /// Index of all ids by values
        /// </summary>
        private readonly Dictionary<string, int> idsByValues = new Dictionary<string, int>();
        #endregion

        #region Implementation of IValuesProvider

        /// <summary>
        /// Provides common values for this kind of instruction
        /// </summary>
        public Collection<string> Values
        {
            get
            {
                Collection<string> returnList = new Collection<string>();
                Array positions = Enum.GetValues(typeof (Cameras.Position));

                foreach (Cameras.Position position in positions)
                {
                    if (position != Cameras.Position.Unknown)
                    {
                        idsByValues.Add(position.ToString(), (int) position);
                        returnList.Add(position.ToString());
                    }
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
            return idsByValues[chosenLabel].ToString();
        }

        #endregion
    }
}

using System.Collections.ObjectModel;

namespace TDUModdingLibrary.support.patcher.parameters.providers
{
    /// <summary>
    /// Provider for lock codes
    /// </summary>
    class LockCodeVP:IValuesProvider
    {
        #region IValuesProvider Members
        /// <summary>
        /// Provides common values for this type of instruction
        /// </summary>
        public Collection<string> Values
        {
            get
            {
                Collection<string> values = new Collection<string>();

                values.Add("0000000");

                return values;
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
using System.Collections.ObjectModel;

namespace TDUModdingLibrary.support.patcher.parameters.providers
{
    /// <summary>
    /// Provider for Boolean values
    /// </summary>
    public class BooleanVP : IValuesProvider
    {
        #region IValuesProvider Members
        public Collection<string> Values
        {
            get
            {
                Collection<string> values = new Collection<string>();

                values.Add(bool.TrueString);
                values.Add(bool.FalseString);

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
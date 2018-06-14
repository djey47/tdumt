using System.Collections.ObjectModel;

namespace TDUModdingLibrary.support.patcher.parameters.providers
{
    /// <summary>
    /// Provides values for data length to be written
    /// </summary>
    public class LengthVP : IValuesProvider
    {        
        #region IValuesProvider Members
        public Collection<string> Values
        {
            get
            {
                Collection <string> values = new Collection<string>();

                values.Add("1");
                values.Add("2");
                values.Add("4");
                values.Add("8");
                
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
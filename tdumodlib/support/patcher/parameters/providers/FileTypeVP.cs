using System.Collections.ObjectModel;

namespace TDUModdingLibrary.support.patcher.parameters.providers
{
    /// <summary>
    /// Provider for file types
    /// </summary>
    internal class FileTypeVP:IValuesProvider
    {
        #region Constants
        /// <summary>
        /// 
        /// </summary>
        internal const string TYPE_OTHER = "Other";
        /// <summary>
        /// 
        /// </summary>
        internal const string TYPE_GAUGES_LOW = "Gauges-Low";
        /// <summary>
        /// 
        /// </summary>
        internal const string TYPE_GAUGES_HIGH = "Gauges-High";
        #endregion

        #region IValuesProvider Members
        /// <summary>
        /// Provides common values for file type
        /// </summary>
        public Collection<string> Values
        {
            get
            {
                Collection<string> values = new Collection<string>();

                values.Add(TYPE_OTHER);
                values.Add(TYPE_GAUGES_LOW);
                values.Add(TYPE_GAUGES_HIGH);

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
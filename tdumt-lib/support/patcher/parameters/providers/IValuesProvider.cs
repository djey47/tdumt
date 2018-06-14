using System.Collections.ObjectModel;

namespace TDUModdingLibrary.support.patcher.parameters.providers
{
    /// <summary>
    /// Interface to implement to give common values for an instruction parameter
    /// </summary>
    public interface IValuesProvider
    {
        /// <summary>
        /// Provides common values for this type of instruction
        /// </summary>
        Collection<string> Values { get;}

        /// <summary>
        /// Returns real value from chosen label (to handle id-label associations)
        /// </summary>
        /// <param name="chosenLabel"></param>
        /// <returns></returns>
        string GetValueFromLabel(string chosenLabel);
    }
}

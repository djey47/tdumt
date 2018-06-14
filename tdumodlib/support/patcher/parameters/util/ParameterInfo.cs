namespace TDUModdingLibrary.support.patcher.parameters.util
{
    /// <summary>
    /// Provides information for a particular instruction parameter
    /// </summary>
    internal class ParameterInfo
    {
        #region Properties
        /// <summary>
        /// Name of this parameter
        /// </summary>
        internal PatchInstructionParameter.ParameterName Name
        {
            get { return _Name; }
        }
        private readonly PatchInstructionParameter.ParameterName _Name;

        /// <summary>
        /// Indicates if this parameter is mandatory (true) or not (false)
        /// </summary>
        internal bool Required
        {
            get { return _Required; }
        }
        private readonly bool _Required;
        #endregion

        /// <summary>
        /// Default constructor - forbidden
        /// </summary>
        internal ParameterInfo()
        {}

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="name">Name of the parameter</param>
        /// <param name="isRequired">true to indicate this parameter as mandatory, false else</param>
        internal ParameterInfo(PatchInstructionParameter.ParameterName name, bool isRequired)
        {
            _Name = name;
            _Required = isRequired;
        }
    }
}
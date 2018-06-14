using System;
using TDUModdingLibrary.support.patcher.parameters;

namespace TDUModdingLibrary.support.patcher.exceptions
{
    /// <summary>
    /// Represents an exception when a parameter is missing
    /// </summary>
    class MissingParameterException : Exception
    {
        #region Constants
        /// <summary>
        /// Formatted message for exception
        /// </summary>
        private const string _EXCEPTION_MESSAGE = "Missing parameter: {0}.";
        #endregion

        #region Properties
        /// <summary>
        /// Exception message
        /// </summary>
        public override string Message
        {
            get { return string.Format(_EXCEPTION_MESSAGE, _ParameterName); }
        }
        private readonly string _ParameterName;
        #endregion

        /// <summary>
        /// Standard constructor
        /// </summary>
        /// <param name="parameter">Missing parameter</param>
        public MissingParameterException(PatchInstructionParameter.ParameterName parameter)
        {
            _ParameterName = parameter.ToString();
        }

        /// <summary>
        /// Embedding constructor
        /// </summary>
        /// <param name="parameter">Missing parameter</param>
        /// <param name="innerException">Previous exception to embed into this container</param>
        public MissingParameterException(PatchInstructionParameter.ParameterName parameter, Exception innerException) : base(null, innerException)
        {
            _ParameterName = parameter.ToString();
        }
    }
}
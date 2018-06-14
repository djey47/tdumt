using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    /// <summary>
    /// Represents a file type (other - gauges high - gauges low) to prevent file name conflicts
    /// </summary>
    internal class FileTypeIP : PatchInstructionParameter
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public override string Name
        {
            get { return ParameterName.fileType.ToString(); }
        }

        /// <summary>
        /// Parameter description
        /// </summary>
        public override string Description
        {
            get { return "Indicates type of current file."; }
        }

        /// <summary>
        /// Provider for common parameter values
        /// </summary>
        public override IValuesProvider DefaultValuesProvider
        {
            get { return new FileTypeVP(); }
        }
    }
}
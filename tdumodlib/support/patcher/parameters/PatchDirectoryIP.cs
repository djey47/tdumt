using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    /// <summary>
    /// Represents a directory in current patch
    /// </summary>
    internal class PatchDirectoryIP : PatchInstructionParameter
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public override string Name
        {
            get {
                return ParameterName.patchDirectory.ToString(); }
        }

        /// <summary>
        /// Parameter description
        /// </summary>
        public override string Description
        {
            get { return "Directory in current patch where to find file to install."; }
        }

        /// <summary>
        /// Provider for common parameter values
        /// </summary>
        public override IValuesProvider DefaultValuesProvider
        {
            get {return null; }
        }
    }
}
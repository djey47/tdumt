using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    /// <summary>
    /// Represents a file name parameter
    /// </summary>
    class FileNameIP:PatchInstructionParameter
    {
        public override string Name
        {
            get { return ParameterName.fileName.ToString(); }
        }

        public override string Description
        {
            get { return "File name to process. Can be a single filename when processing a packed file."; }
        }

        public override IValuesProvider DefaultValuesProvider
        {
            get { return null; }
        }
    }
}
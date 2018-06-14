using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    /// <summary>
    /// Parameter specifying a target path
    /// </summary>
    class DestinationIP:PatchInstructionParameter
    {
        public override string Name
        {
            get { return ParameterName.destination.ToString(); }
        }

        public override string Description
        {
            get { return "Target directory and/or filename."; }
        }

        public override IValuesProvider DefaultValuesProvider
        {
            get { return null; }
        }
    }
}

using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    /// <summary>
    /// Parameter used to specify data to be written.
    /// </summary>
    class ValueIP:PatchInstructionParameter
    {
        public override string Name
        {
            get { return ParameterName.value.ToString(); }
        }

        public override string Description
        {
            get { return "Value to be written (in decimal). If not specified, assumed value is 0."; }
        }

        public override IValuesProvider DefaultValuesProvider
        {
            get { return null; }
        }
    }
}

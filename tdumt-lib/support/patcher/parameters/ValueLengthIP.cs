using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    /// <summary>
    /// Parameter referring to length of data to be written, in bytes
    /// </summary>
    class ValueLengthIP:PatchInstructionParameter
    {
        public override string Name
        {
            get { return ParameterName.valueLength.ToString(); }
        }

        public override string Description
        {
            get { return "Number of bytes to be written (in decimal). If not specified, assumed value is 1. Possible values: 1 / 2 / 4 / 8."; }
        }

        public override IValuesProvider DefaultValuesProvider
        {
            get { return new LengthVP(); }
        }
    }
}
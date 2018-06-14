using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    /// <summary>
    /// Instruction parameter to specify address where data will be written.
    /// </summary>
    class ValueAddressIP:PatchInstructionParameter
    {
        public override string Name
        {
            get { return ParameterName.valueAddress.ToString(); }
        }

        public override string Description
        {
            get { return "Start address of data to be written (in bytes - decimal). This value is relative to file origin."; }
        }

        public override IValuesProvider DefaultValuesProvider
        {
            get { return null; }
        }
    }
}
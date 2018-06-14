using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    /// <summary>
    /// Instruction parameter referring to a new file name
    /// </summary>
    class NewFileIP:PatchInstructionParameter
    {
        public override string Name
        {
            get { return ParameterName.newFile.ToString(); }
        }

        public override string Description
        {
            get { return "Name of new file to pack into a BNK."; }
        }

        public override IValuesProvider DefaultValuesProvider
        {
            get { return null; }
        }
    }
}

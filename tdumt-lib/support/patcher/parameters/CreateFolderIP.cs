using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    /// <summary>
    /// Parameter allowing folder creation when it does not exist
    /// </summary>
    class CreateFolderIP:PatchInstructionParameter
    {
        public override string Name
        {
            get { return ParameterName.createFolder.ToString(); }
        }

        public override string Description
        {
            get { return "Enforces folder creation when it does not exist. Possible values: true / false."; }
        }

        public override IValuesProvider DefaultValuesProvider
        {
            get { return new BooleanVP(); }
        }
    }
}

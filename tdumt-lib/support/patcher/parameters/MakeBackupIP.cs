using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    /// <summary>
    /// Represents the makeBackup boolean parameter
    /// </summary>
    class MakeBackupIP:PatchInstructionParameter
    {
        public override string Name
        {
            get { return ParameterName.makeBackup.ToString(); }
        }

        public override string Description
        {
            get { return "Specifies if backup of modified files must be made. Possible values: true / false."; }
        }

        public override IValuesProvider DefaultValuesProvider
        {
            get { return new BooleanVP(); }
        }
    }
}
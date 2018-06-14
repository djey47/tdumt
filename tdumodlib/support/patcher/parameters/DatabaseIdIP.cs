using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    /// <summary>
    /// Parameter used to indicate a generic database identifier
    /// </summary>
    class DatabaseIdIP : PatchInstructionParameter
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public override string Name
        {
            get { return ParameterName.databaseId.ToString(); }
        }

        /// <summary>
        /// Parameter description
        /// </summary>
        public override string Description
        {
            get { return "Identifier of database value to modify."; }
        }

        public override IValuesProvider DefaultValuesProvider
        {
            get { return null; }
        }
    }
}
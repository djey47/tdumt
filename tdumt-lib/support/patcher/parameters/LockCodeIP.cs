using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingLibrary.support.patcher.parameters
{
    /// <summary>
    /// Parameter: code for current lock
    /// </summary>
    class LockCodeIP:PatchInstructionParameter
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public override string Name
        {
            get { return ParameterName.lockCode.ToString(); }
        }

        /// <summary>
        /// Parameter description
        /// </summary>
        public override string Description
        {
            get { return "Code for current lock."; }
        }

        /// <summary>
        /// Default values provider
        /// </summary>
        public override IValuesProvider DefaultValuesProvider
        {
            get { return new LockCodeVP(); }
        }
    }
}
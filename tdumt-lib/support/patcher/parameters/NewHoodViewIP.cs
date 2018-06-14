namespace TDUModdingLibrary.support.patcher.parameters
{
    class NewHoodViewIP:AbstractNewViewIP
    {
        #region Overrides of PatchInstructionParameter

        /// <summary>
        /// Parameter name
        /// </summary>
        public override string Name
        {
            get { return ParameterName.newHoodView.ToString(); }
        }
        #endregion
    }
}
namespace TDUModdingLibrary.support.patcher.parameters
{
    class NewCockpitViewIP : AbstractNewViewIP
    {
        #region Overrides of PatchInstructionParameter

        /// <summary>
        /// Parameter name
        /// </summary>
        public override string Name
        {
            get { return ParameterName.newCockpitView.ToString(); }
        }
        #endregion
    }
}
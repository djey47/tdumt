namespace TDUModdingLibrary.support.patcher.parameters
{
    class NewCockpitBackViewIP : AbstractNewViewIP
    {
        #region Overrides of PatchInstructionParameter

        /// <summary>
        /// Parameter name
        /// </summary>
        public override string Name
        {
            get { return ParameterName.newCockpitBackView.ToString(); }
        }
        #endregion
    }
}
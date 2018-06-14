namespace TDUModdingLibrary.support.patcher.parameters
{
    class NewHoodBackViewIP : AbstractNewViewIP
    {
        #region Overrides of PatchInstructionParameter

        /// <summary>
        /// Parameter name
        /// </summary>
        public override string Name
        {
            get { return ParameterName.newHoodBackView.ToString(); }
        }
        #endregion
    }
}
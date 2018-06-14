namespace TDUModdingLibrary.support.patcher.parameters
{
    class ViewSourceIP:AbstractCockpitPositionIP
    {
        #region Overrides of PatchInstructionParameter

        /// <summary>
        /// Parameter name
        /// </summary>
        public override string Name
        {
            get { return ParameterName.viewSource.ToString(); }
        }

        #endregion
    }
}

namespace TDUModdingLibrary.support.patcher.parameters
{
    class ViewTargetIP:AbstractCockpitPositionIP
    {
        #region Overrides of PatchInstructionParameter

        /// <summary>
        /// Parameter name
        /// </summary>
        public override string Name
        {
            get { return ParameterName.viewTarget.ToString(); }
        }

        #endregion
    }
}

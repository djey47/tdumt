namespace TDUModdingLibrary.support.patcher.vars
{
    /// <summary>
    /// Variable referring to TDU's install path
    /// </summary>
    class TduPathV:PatchVariable
    {
        public override string Name
        {
            get { return VariableName.tduPath.ToString(); }
        }

        public override string Description
        {
            get { return @"TDU's install path (...\Test Drive Unlimited\)"; }
        }

        internal override string GetValue()
        {
            return Tools.TduPath;
        }
    }
}

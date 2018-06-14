namespace TDUModdingLibrary.support.patcher.vars
{
    /// <summary>
    /// Variable referring to patch starting path
    /// </summary>
    class PatchPathV:PatchVariable
    {
        public override string Name
        {
            get { return VariableName.patchPath.ToString(); }
        }

        public override string Description
        {
            get { return @"Patch starting path (<current directory>)"; }
        }

        internal override string GetValue()
        {
            return PatchHelper.CurrentPath;
        }
    }
}
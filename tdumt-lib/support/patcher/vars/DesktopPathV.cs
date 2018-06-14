using System;

namespace TDUModdingLibrary.support.patcher.vars
{
    /// <summary>
    /// Variable referring to Desktop folder
    /// </summary>
    class DesktopPathV:PatchVariable
    {
        public override string Name
        {
            get { return VariableName.desktopPath.ToString(); }
        }

        public override string Description
        {
            get { return "User's desktop folder"; }
        }

        internal override string GetValue()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }
    }
}

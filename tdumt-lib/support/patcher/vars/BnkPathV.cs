using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.support.patcher.vars
{
    /// <summary>
    /// Variable referring to TDU's BNK folder (.../euro/bnk)
    /// </summary>
    class BnkPathV:PatchVariable
    {
        public override string Name
        {
            get { return VariableName.bnkPath.ToString(); }
        }

        public override string Description
        {
            get { return @"Folder where BNK files are stored (...\Test Drive Unlimited\Euro\Bnk\)"; }
        }

        internal override string GetValue()
        {
            return LibraryConstants.GetSpecialFolder(LibraryConstants.TduSpecialFolder.Bnk);
        }
    }
}
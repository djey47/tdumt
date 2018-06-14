using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.support.patcher.vars
{
    /// <summary>
    /// Variable referring to TDU's database folder (.../euro/bnk/database)
    /// </summary>
    class DbPathV:PatchVariable
    {
        public override string Name
        {
            get { return VariableName.dbPath.ToString(); }
        }

        public override string Description
        {
            get { return @"Folder where database files are stored (...\Test Drive Unlimited\Euro\Bnk\Database\)"; }
        }

        internal override string GetValue()
        {
            return LibraryConstants.GetSpecialFolder(LibraryConstants.TduSpecialFolder.Database);
        }
    }
}

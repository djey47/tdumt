using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.support.patcher.vars
{
    /// <summary>
    /// Variable referring to TDU's BNK Hawai folder (.../euro/bnk/level/hawai)
    /// </summary>
    class BnkLevelHawaiPathV:PatchVariable
    {
        public override string Name
        {
            get { return VariableName.bnkLevelHawaiPath.ToString(); }
        }

        public override string Description
        {
            get { return @"Folder where Hawai files are stored (...\Test Drive Unlimited\Euro\Bnk\Level\Hawai)"; }
        }

        internal override string GetValue()
        {
            return string.Concat(Tools.TduPath, LibraryConstants.FOLDER_LEVEL_HAWAI);
        }
    }
}
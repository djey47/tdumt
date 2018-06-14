using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.support.patcher.vars
{
    /// <summary>
    /// Variable referring to TDU's high-res gauges BNK folder
    /// </summary>
    class BnkGaugesHighPathV:PatchVariable
    {
        public override string Name
        {
            get { return VariableName.bnkGaugesHighPath.ToString(); }
        }

        public override string Description
        {
            get { return @"Folder where high-res gauges BNK files are stored (...\Test Drive Unlimited\Euro\Bnk\FrontEnd\HiRes\Gauges)"; }
        }

        internal override string GetValue()
        {
            return string.Concat(Tools.TduPath, LibraryConstants.FOLDER_VEHICLES_GAUGES_HIGH);
        }
    }
}
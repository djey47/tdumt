using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.support.patcher.vars
{
    /// <summary>
    /// Variable referring to TDU's low-res gauges BNK folder
    /// </summary>
    class BnkGaugesLowPathV : PatchVariable
    {
        public override string Name
        {
            get { return VariableName.bnkGaugesLowPath.ToString(); }
        }

        public override string Description
        {
            get { return @"Folder where low-res gauges BNK files are stored (...\Test Drive Unlimited\Euro\Bnk\FrontEnd\LowRes\Gauges)"; }
        }

        internal override string GetValue()
        {
            return string.Concat(Tools.TduPath, LibraryConstants.FOLDER_VEHICLES_GAUGES_LOW);
        }
    }
}
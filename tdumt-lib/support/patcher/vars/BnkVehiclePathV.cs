using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.support.patcher.vars
{
    /// <summary>
    /// Variable referring to TDU's vehicle models BNK folder
    /// </summary>
    class BnkVehiclePathV : PatchVariable
    {
        public override string Name
        {
            get { return VariableName.bnkVehiclePath.ToString(); }
        }

        public override string Description
        {
            get { return @"Folder where vehicle models BNK files are stored (...\Test Drive Unlimited\Euro\Bnk\Vehicules)"; }
        }

        internal override string GetValue()
        {
            return string.Concat(Tools.TduPath, LibraryConstants.FOLDER_VEHICLES_MODELS);
        }
    }
}
using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.support.patcher.vars
{
    /// <summary>
    /// Variable referring to TDU's rim models BNK parent folder
    /// </summary>
    class BnkVehicleRimPathV : PatchVariable
    {
        public override string Name
        {
            get { return VariableName.bnkVehicleRimPath.ToString(); }
        }

        public override string Description
        {
            get { return @"Parent folder where rim vehicle models BNK files are stored (...\Test Drive Unlimited\Euro\Bnk\Vehicules\Rim)"; }
        }

        internal override string GetValue()
        {
            return string.Concat(Tools.TduPath, LibraryConstants.FOLDER_PARENT_VEHICLES_RIMS);
        }
    }
}
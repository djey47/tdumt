using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.support.patcher.vars
{
    /// <summary>
    /// Variable referring to TDU's vehicle sounds BNK folder
    /// </summary>
    class BnkSoundVehiclePathV:PatchVariable
    {
        public override string Name
        {
            get { return VariableName.bnkSoundVehiclePath.ToString(); }
        }

        public override string Description
        {
            get { return @"Folder where vehicle sounds BNK files are stored (...\Test Drive Unlimited\Euro\Bnk\Sound\Vehicules)"; }
        }

        internal override string GetValue()
        {
            return string.Concat(Tools.TduPath, LibraryConstants.FOLDER_VEHICLES_SOUNDS);
        }
    }
}
using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.support.patcher.vars
{
    class BnkVehicleTrafficPathV:PatchVariable
    {
        #region Overrides of PatchVariable
        /// <summary>
        /// Variable name
        /// </summary>
        public override string Name
        {
            get { return VariableName.bnkVehicleTrafficPath.ToString(); }
        }

        /// <summary>
        /// Variable description
        /// </summary>
        public override string Description
        {
            get { return @"Parent folder where traffic vehicle models BNK files are stored (...\Test Drive Unlimited\Euro\Bnk\Vehicules\Traffic)"; }
        }

        /// <summary>
        /// Which value the variable should refer to
        /// </summary>
        internal override string GetValue()
        {
            return string.Concat(Tools.TduPath, LibraryConstants.FOLDER_PARENT_VEHICLES_TRAFFIC);
        }
        #endregion
    }
}
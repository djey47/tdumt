using TDUModdingTools.gui;

namespace TDUModdingTools.common.handlers
{
    /// <summary>
    /// Handler class for manipulating MAP files from TDU Modding Tools
    /// </summary>
    class MAPHandler : FileHandler
    {
        #region Properties
        /// <summary>
        /// Editeur par défaut
        /// </summary>
        public static new string DefaultEditor
        {
            get { return "MAP Tool"; }
        }
        #endregion

        #region Implémentation de FileHandler
        /// <summary>
        /// Implémentation
        /// </summary>
        public override void Edit()
        {
            // Not handled
        }

        /// <summary>
        /// Application des modifs
        /// </summary>
        public override void Apply(params object[] paramList)
        {
            // Non nécessaire
        }
        #endregion
    }
}
using TDUModdingTools.common.handlers;
using TDUModdingTools.gui;

namespace TDUModdingTools.common.handlers.database
{
    /// <summary>
    /// Handles processing of database resource files in TDUMT
    /// </summary>
    class DBResourceHandler:FileHandler
    {
        #region Properties
        /// <summary>
        /// Editeur par défaut
        /// </summary>
        public static new string DefaultEditor
        {
            get { return "DB Resources Editor"; }
        }
        #endregion

        #region FileHandler implementation
        /// <summary>
        /// Implémentation
        /// </summary>
        public override void Edit()
        {
            // Edition avec Database Editor
            MainForm.Instance.LoadTool(MainForm.ModuleType.DBResourcesEditor, FileName);
        }

        /// <summary>
        /// Application des modifs
        /// </summary>
        public override void Apply(params object[] paramList)
        {
            // Rien à appliquer dans ce cas; le fichier est déjà à jour.
        }
        #endregion
    }
}
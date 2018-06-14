using System;
using TDUModdingTools.common.handlers;
using TDUModdingTools.gui;

namespace TDUModdingTools.common.handlers.specific
{
    /// <summary>
    /// Handles processing of .PCH patch files in TDUMT
    /// </summary>
    class PCHHandler:FileHandler
    {
        #region Properties
        /// <summary>
        /// Default Editor
        /// </summary>
        public static new String DefaultEditor
        {
            get { return "Patch Editor"; }
        }
        #endregion

        #region FileHandler implementation
        public override void Edit()
        {
            // Uses Patch Editor Wizard
            MainForm.Instance.LoadTool(MainForm.ModuleType.PatchEditor, FileName);
        }

        public override void Apply(params object[] paramList)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
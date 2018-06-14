using System;

namespace TDUModdingTools.common.handlers
{
    /// <summary>
    /// Handles processing of XMB config files in TDUMT
    /// </summary>
    class XMBHandler:FileHandler
    {
        #region Properties
        /// <summary>
        /// Editeur par défaut
        /// </summary>
        public static new string DefaultEditor
        {
            get { return "default editor"; }
        }
        #endregion

        #region FileHandler implementation
        public override void Edit()
        {
            throw new NotImplementedException();
        }

        public override void Apply(params object[] paramList)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
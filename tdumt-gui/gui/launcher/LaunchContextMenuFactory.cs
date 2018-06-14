using System.Collections.ObjectModel;
using System.Windows.Forms;
using TDUModdingTools.common.settings;
using DjeFramework1.Common.GUI.Tools;

namespace TDUModdingTools.gui.launcher
{
    /// <summary>
    /// Classe permettant de générer des menus contextuels à la volée
    /// </summary>
    class LaunchContextMenuFactory : IContextMenuFactory
    {
        #region Constantes
        // MenuItems
        private const string _MENU_ITEM_SETTINGS = "Settings...";
        #endregion

        #region IContexteMenuFactoryImplementation
        /// <summary>
        /// Construit le menu contextuel pour configs de lancement
        /// </summary>
        /// <param name="baseForm"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ContextMenuStrip CreateContextMenu(Form baseForm, params object[] args)
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            Collection<LaunchConfiguration> configList = Program.ApplicationSettings.GetLaunchConfigList();
            MainForm mainForm = baseForm as MainForm;

            if (mainForm == null)
                return contextMenu;

            // Parcours de la liste de configurations
            if (configList != null)
            {
                // All configurations are added
                foreach (LaunchConfiguration anotherConfig in configList)
                    contextMenu.Items.Add(anotherConfig.Name, null, mainForm.TduLaunchTarget);
            }

            // Item de paramétrage
            if (contextMenu.Items.Count > 0)
                contextMenu.Items.Add(new ToolStripSeparator());

            contextMenu.Items.Add(_MENU_ITEM_SETTINGS, null, mainForm.LaunchSettingsTarget);

            return contextMenu;
        }
        #endregion
    }
}
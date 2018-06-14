using System;
using System.Reflection;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Tools;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.support.constants;
using TDUModdingTools.common.handlers;

namespace TDUModdingTools.gui.filebrowser.common
{
    /// <summary>
    /// Classe permettant de générer des menus contextuels à la volée
    /// </summary>
    class FileBrowserContextMenuFactory : IContextMenuFactory
    {
        #region Types personnalisés
        /// <summary>
        /// Types de vues  pour menus contextuels
        /// </summary>
        public enum ViewType
        {
            FolderTree, FileList, ContentListFlat, ContentListHierarchical
        }
        #endregion

        #region Constants
        // MenuItems
        private const string _MENU_ITEM_OPEN = "Open with {0}";
        private const string _MENU_ITEM_DELETE = "Delete";
        private const string _MENU_ITEM_EXTRACT = "Extract To...";
        private const string _MENU_ITEM_EXTRACT_ALL = "Extract All...";
        private const string _MENU_ITEM_RESTORE = "Restore";
        private const string _MENU_ITEM_BACKUP = "Backup";
        //private const string _MENU_ITEM_OPEN_PACKED = "Open with {0} (read-only)";
        private const string _MENU_ITEM_EDIT_PACKED = "Edit with {0}";
        private const string _MENU_ITEM_REPLACE_KEEP = "Replace, keep name...";
        private const string _MENU_ITEM_REPLACE_RENAME = "Replace, change name...";

        private const string _MENU_ITEM_PROPERTIES = "Properties...";
        private const string _MENU_ITEM_RENAME = "Rename";
        // Properties
        private const string _PROPERTY_NAME_DEFAULT_EDITOR = "DefaultEditor";
        // Messages
        private const string _ERROR_DEFAULT_EDITOR = "Error when trying to get default editor for extension {0}.";
        #endregion

        #region IContextMenuFactoryImplementation
        /// <summary>
        /// Génére le menu contextuel relatif à la vue concernée
        /// </summary>
        /// <param name="baseForm">Form de base</param>
        /// <param name="args">Parameters : expected [0] viewType, [1] fileName</param>
        /// <returns></returns>
        public ContextMenuStrip CreateContextMenu(Form baseForm, params object[] args)
        {
            // Parameters
            ViewType viewType = (ViewType) args[0];
            string fileName = args[1] as string;

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            string fileExtension = File2.GetExtensionFromFilename(fileName);
            FileBrowserForm browserForm = baseForm as FileBrowserForm;

            if (browserForm == null)
                return contextMenu;

            // Récupère l'éditeur par défaut pour le fichier
            string defaultEditor = FileHandler.DefaultEditor;

            if (!string.IsNullOrEmpty(fileExtension))
            {
                try
                {
                    FileHandler f = FileHandler.GetHandler(fileName);

                    // On récupère l'éditeur par réflexion
                    if (f != null)
                    {
                        PropertyInfo pi = f.GetType().GetProperty(_PROPERTY_NAME_DEFAULT_EDITOR);

                        defaultEditor = (string)pi.GetValue(f.GetType(), null);
                    }
                }
                catch (Exception ex)
                {
                    // Erreur silencieuse
                    string message = string.Format(_ERROR_DEFAULT_EDITOR, fileExtension);

                    Exception2.PrintStackTrace(new Exception(message, ex));
                }
            }

            if (viewType == ViewType.FileList)
            {
                // Liste de fichiers

                // Selon l'extension
                if (fileExtension != null)
                {
                    switch (fileExtension.ToUpper())
                    {
                        case "":
                            // Commandes communes
                            // EVO_149: Backup
                            contextMenu.Items.Add(_MENU_ITEM_BACKUP, null, browserForm.BackupTarget);
                            contextMenu.Items.Add(new ToolStripSeparator());
                            break;
                        case LibraryConstants.EXTENSION_BNK_FILE:
                            // Extract
                            contextMenu.Items.Add(_MENU_ITEM_EXTRACT_ALL, null, browserForm.ExtractAllBnkTarget);
                            contextMenu.Items.Add(new ToolStripSeparator());
                            // Backup
                            contextMenu.Items.Add(_MENU_ITEM_BACKUP, null, browserForm.BackupTarget);
                            contextMenu.Items.Add(new ToolStripSeparator());
                            break;
                        case LibraryConstants.EXTENSION_BACKUP:
                            // Restore
                            contextMenu.Items.Add(_MENU_ITEM_RESTORE, null, browserForm.RestoreTarget);
                            contextMenu.Items.Add(new ToolStripSeparator());
                            break;
                        default:
                            // Open
                            contextMenu.Items.Add(string.Format(_MENU_ITEM_OPEN, defaultEditor), null,
                                                  browserForm.OpenFileTarget);
                            contextMenu.Items.Add(new ToolStripSeparator());
                            // Backup
                            contextMenu.Items.Add(_MENU_ITEM_BACKUP, null, browserForm.BackupTarget);
                            contextMenu.Items.Add(new ToolStripSeparator());
                            break;
                    }
                    // Commandes communes : Delete, Properties
                    contextMenu.Items.Add(_MENU_ITEM_DELETE, null, browserForm.DeleteFileTarget);
                    // EVO_65 : properties
                    contextMenu.Items.Add(new ToolStripSeparator());
                    contextMenu.Items.Add(_MENU_ITEM_PROPERTIES, null, browserForm.PropertiesTarget);
                }
            }
            else if (viewType == ViewType.ContentListFlat || viewType == ViewType.ContentListHierarchical)
            {
                // BNK contents
                contextMenu.Items.Add(_MENU_ITEM_REPLACE_KEEP, null, browserForm.ReplaceKeepNameTarget);
                contextMenu.Items.Add(_MENU_ITEM_REPLACE_RENAME, null, browserForm.ReplaceRenameTarget);
                contextMenu.Items.Add(new ToolStripSeparator());
                contextMenu.Items.Add(_MENU_ITEM_EXTRACT, null, browserForm.ExtractBnkTarget);
                if (browserForm._SelectedPackedFilesCount == 1)
                    contextMenu.Items.Add(_MENU_ITEM_RENAME, null, browserForm.RenameTarget);
                //contextMenu.Items.Add(string.Format(_MENU_ITEM_OPEN_PACKED, defaultEditor), null, browserForm.ViewContentTarget);
                contextMenu.Items.Add(string.Format(_MENU_ITEM_EDIT_PACKED, defaultEditor), null, browserForm.EditContentTarget);
                /*contextMenu.Items.Add(new ToolStripSeparator());
                contextMenu.Items.Add(_MENU_ITEM_DELETE, null, browserForm.DeletePackedTarget);*/
                // EVO_65: properties
                contextMenu.Items.Add(new ToolStripSeparator());
                contextMenu.Items.Add(_MENU_ITEM_PROPERTIES, null, browserForm.PackedPropertiesTarget);
            }
            else if (viewType == ViewType.FolderTree)
            {
                // Arborescence de dossiers

                // Pas de clic droit géré
            }

            return contextMenu;
        }
        #endregion
    }
}
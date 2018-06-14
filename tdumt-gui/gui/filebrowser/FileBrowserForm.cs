using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Types;
using DjeFramework1.Common.Types.Forms;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.fileformats.binaries;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.graphics;
using TDUModdingLibrary.fileformats.sound;
using TDUModdingLibrary.fileformats.world;
using TDUModdingLibrary.support;
using TDUModdingLibrary.support.constants;
using TDUModdingTools.common;
using TDUModdingTools.common.handlers;
using TDUModdingTools.gui.filebrowser.common;
using TDUModdingTools.gui.filebrowser.editing;
using TDUModdingTools.gui.filebrowser.properties;
using TDUModdingTools.gui.settings;
using TDUModdingTools.gui.wizards.vehiclemanager;

namespace TDUModdingTools.gui.filebrowser
{
    /// <summary>
    /// TDU Modding Tools' File Browser
    /// </summary>
    public partial class FileBrowserForm : Form
    {
        #region Enums
        /// <summary>
        /// Indices des images dans la liste d'images (grande et petite taille)
        /// </summary>
        enum ItemPictures
        {
            OpenedFolder = 0,
            ClosedFolder = 1,
            Gear = 2,
            /*Disk = 3,*/
            Sheet = 4,
            Bnk = 5,
            Backup = 6,
            Texture = 7,
            Materials = 8,
            Geometry = 9,
            Sound = 10,
            Database = 11,
            OpenedPackedFolder = 12,
            ClosedPackedFolder = 13,
            OpenedExtFolder = 14,
            ClosedExtFolder = 15,
            Camera = 16
        }
        #endregion

        #region Constants
        /// <summary>
        /// List of file name patterns to display in file browser
        /// </summary>
        private static readonly Collection<string> _ALLOWED_FILENAME_PATTERNS = new Collection<string>();

        /// <summary>
        /// List of item pictures indexed by file pattern
        /// </summary>
        private static readonly Dictionary<string, ItemPictures> _PICTURES_BY_PATTERNS =
            new Dictionary<string, ItemPictures>();

        /// <summary>
        /// Hauteur par défaut du BNK Manager 
        /// </summary>
        private const int _MANAGER_DEFAULT_HEIGHT = 300;
        #endregion

        #region Properties
        /// <summary>
        /// Instance de la fenêtre
        /// </summary>
        public static FileBrowserForm Instance
        {
            get
            { 
                if (_Instance == null)
                    _Instance = new FileBrowserForm();
                return _Instance;
            }
        }
        private static FileBrowserForm _Instance;

        /// <summary>
        /// Dossier courant
        /// </summary>
        public string CurrentFolder
        {
            get
            {
                String currentPath = bnkStatusLabel.Text;

                if (string.IsNullOrEmpty(currentPath))
                    currentPath = null;
                else
                {
                    if (!currentPath.EndsWith(@"\"))
                        currentPath += @"\";
                }
                
                return currentPath;
            }
        }

        /// <summary>
        /// Fichier BNK actuellement sélectionné et ouvert
        /// </summary>
        public BNK CurrentBnkFile
        {
            get { return _CurrentBnkFile; }
        }
        private BNK _CurrentBnkFile;
        #endregion

        #region Cibles pour invocations à distance
        /// <summary>
        ///  Ouverture de fichier
        /// </summary>
        public EventHandler OpenFileTarget 
        {
             get { return bnkOpenToolStripButton_Click; }      
        }

        /// <summary>
        /// Suppression de fichier
        /// </summary>
        public EventHandler DeleteFileTarget
        {
            get { return bnkDeleteToolStripButton_Click; }
        }

        /// <summary>
        /// Packed file removal
        /// </summary>
        public EventHandler DeletePackedTarget
        {
            get { return fileDeleteToolStripButton_Click; }
        }

        /// <summary>
        /// Extraction de fichier BNK
        /// </summary>
        public EventHandler ExtractBnkTarget
        {
            get { return fileExtractToolStripButton_Click; }
        }

        /// <summary>
        /// Whole BNK extract
        /// </summary>
        public EventHandler ExtractAllBnkTarget
        {
            get { return _ExtractAllBnkContents;  }
        }

        /// <summary>
        /// Sauvegarde de fichier
        /// </summary>
        public EventHandler BackupTarget
        {
            get { return bnkBackupToolsStripButton_Click; }
        }

        /// <summary>
        /// Restauration de sauvegarde
        /// </summary>
        public EventHandler RestoreTarget
        {
            get { return bnkRestoreToolStripButton_Click; }
        }

        /// <summary>
        /// Afficher le fichier
        /// </summary>
        public EventHandler ViewContentTarget
        {
            get { return fileOpenToolStripButton_Click; }
        }

        /// <summary>
        /// Editer le fichier
        /// </summary>
        public EventHandler EditContentTarget
        {
            get { return fileEditToolStripButton_Click; }
        }

        /// <summary>
        /// Display selected file properties
        /// </summary>
        public EventHandler PropertiesTarget
        {
            get { return _DisplayProperties; }
        }

        /// <summary>
        /// Display selected packed file properties
        /// </summary>
        public EventHandler PackedPropertiesTarget
        {
            get { return _DisplayPackedProperties; }
        }
        #endregion

        #region Fields
        /// <summary>
        /// Dernier emplacement d'extraction
        /// </summary>
        private string _LastExtractLocation;

        /// <summary>
        /// Dernier emplacement de remplacement
        /// </summary>
        private string _LastReplaceLocation;

        /// <summary>
        /// Fenêtre de suivi des tâches d'édition
        /// </summary>
        private EditTasksForm _EditTasksWindow;

        /// <summary>
        /// Indicateur de conservation de la sélection dans la liste de fichiers après rafraichissement de l'arborescence
        /// </summary>
        private bool _KeepListSelection;

        /// <summary>
        /// Indicateur d'utilisation de la touche Shift sur la liste empaquetée
        /// </summary>
        private bool _ContentListShiftKeyPressed;
        #endregion

        /// <summary>
        /// Class constructor
        /// </summary>
        static FileBrowserForm()
        {
            // Display authorized patterns
            _ALLOWED_FILENAME_PATTERNS.Add(BNK.FILENAME_PATTERN);
            _ALLOWED_FILENAME_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + LibraryConstants.EXTENSION_BACKUP);
            _ALLOWED_FILENAME_PATTERNS.Add(MAP.FILENAME_PATTERN);
            _ALLOWED_FILENAME_PATTERNS.Add(XMB_WAV.FILENAME_PATTERN);
            _ALLOWED_FILENAME_PATTERNS.Add(_2DB.FILENAME_PATTERN);
            _ALLOWED_FILENAME_PATTERNS.Add(DDS.FILENAME_PATTERN);
            _ALLOWED_FILENAME_PATTERNS.Add(XMB.FILENAME_PATTERN);
            _ALLOWED_FILENAME_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + LibraryConstants.EXTENSION_PATCH_FILE);
            _ALLOWED_FILENAME_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + DB.Culture.FR);
            _ALLOWED_FILENAME_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + DB.Culture.GE);
            _ALLOWED_FILENAME_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + DB.Culture.CH);
            _ALLOWED_FILENAME_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + DB.Culture.KO);
            _ALLOWED_FILENAME_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + DB.Culture.SP);
            _ALLOWED_FILENAME_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + DB.Culture.IT);
            _ALLOWED_FILENAME_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + DB.Culture.US);
            _ALLOWED_FILENAME_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + DB.Culture.JA);
            _ALLOWED_FILENAME_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + LibraryConstants.EXTENSION_DB_FILE);
            _ALLOWED_FILENAME_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + LibraryConstants.EXTENSION_INI_FILE);
            _ALLOWED_FILENAME_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + LibraryConstants.EXTENSION_BIN_FILE);
            _ALLOWED_FILENAME_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + LibraryConstants.EXTENSION_XML_FILE);
            _ALLOWED_FILENAME_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + LibraryConstants.EXTENSION_STATION_FILE);
            _ALLOWED_FILENAME_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + VehicleManagerForm.EXTENSION_BACKUP);
            _ALLOWED_FILENAME_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + LibraryConstants.EXTENSION_IGE_FILE);
            _ALLOWED_FILENAME_PATTERNS.Add(DFE.FILE_DATA_DFE + String2.REGEX_PATTERN_STARTS_ENDS_WITH);

            // File icons
            _PICTURES_BY_PATTERNS.Add(MAP.FILENAME_PATTERN, ItemPictures.Gear);
            _PICTURES_BY_PATTERNS.Add(TduFile.BACKUP_FILENAME_PATTERN, ItemPictures.Backup);
            _PICTURES_BY_PATTERNS.Add(BNK.FILENAME_PATTERN, ItemPictures.Bnk);
            _PICTURES_BY_PATTERNS.Add(_2DB.FILENAME_PATTERN, ItemPictures.Texture);
            _PICTURES_BY_PATTERNS.Add(DDS.FILENAME_PATTERN, ItemPictures.Texture);
            _PICTURES_BY_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + LibraryConstants.EXTENSION_MATERIAL_FILE, ItemPictures.Materials);            
            _PICTURES_BY_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + LibraryConstants.EXTENSION_3D_GEOMETRY_FILE, ItemPictures.Geometry);
            _PICTURES_BY_PATTERNS.Add(String2.REGEX_PATTERN_EXTENSION + LibraryConstants.EXTENSION_3D_DESCRIPTION_FILE, ItemPictures.Geometry);
            _PICTURES_BY_PATTERNS.Add(XMB_WAV.FILENAME_PATTERN, ItemPictures.Sound);
            _PICTURES_BY_PATTERNS.Add(DB.FILENAME_PATTERN, ItemPictures.Database);
            _PICTURES_BY_PATTERNS.Add(DBResource.FILENAME_PATTERN, ItemPictures.Database);
            _PICTURES_BY_PATTERNS.Add(Cameras.FILENAME_PATTERN, ItemPictures.Camera);
        }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public FileBrowserForm()
        {
            InitializeComponent();

            // Instance
            _Instance = this;

            // EVO_32
            StatusBarLogManager.AddNewLog(this, contentsStatusLabel);

            _InitializeContents();
        }

        #region Evenements
        /// <summary>
        /// Signale que le dossier de départ de TDU a changé.
        /// </summary>
        public void MainFolderChanged()
        {
            _RefreshTreeView();
        }

        private void refreshTreeToolStripButton_Click(object sender, EventArgs e)
        {
            // Clic sur le menu "Refresh"

            // Sauvegarde du chemin du noeud actuel
            string currentPath = null;

            if (folderTreeView.SelectedNode != null)
                currentPath = folderTreeView.SelectedNode.FullPath;

            _RefreshTreeView();

            // Correction ANO : rétablit le noeud sélectionné
            if (currentPath != null)
            {
                _KeepListSelection = true;
                TreeView2.SelectNodeByPath(folderTreeView, currentPath);

                // Noeud non trouvé > on sélectionne le noeud racine
                /*if (folderTreeView.SelectedNode == null)
                    TreeView2.SelectNodeByPath(folderTreeView,_TREENODE_ROOT);*/
            }
        }

        private void folderTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Affiche les fichiers
            _RefreshFileList(e.Node, _KeepListSelection);
            _KeepListSelection = false;
        }

        private void bnkListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Double click in file list view
            if (e.Button == MouseButtons.Left && bnkListView.SelectedItems.Count == 1)
            {
                try
                {
                    string selectedFile = CurrentFolder + bnkListView.SelectedItems[0].Text;

                    // Les fichiers BNK sont gérés lors du simple clic : pour les autres fichiers
                    // on réalise l'édition directe
                    if (! Regex.IsMatch(selectedFile, BNK.FILENAME_PATTERN, RegexOptions.IgnoreCase))
                        FileHandler.GetHandler(selectedFile).Edit();
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void bnkListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bnkListView.SelectedItems.Count == 1)
            {
                Cursor = Cursors.WaitCursor;

                string selectedFile = CurrentFolder + bnkListView.SelectedItems[0].Text;

                try
                {
                    // Selon l'extension... on ne gère que les fichiers BNK sur simple clic
                    if (Regex.IsMatch(selectedFile, BNK.FILENAME_PATTERN, RegexOptions.IgnoreCase))
                        // Fichier BNK -> on affiche le contenu en partie inférieure
                        _LoadBnkFile(selectedFile);
                    else
                    {
                        // Effacement de la liste contenue dans le BNK Manager
                        _ClearContentLists();
                        StatusBarLogManager.ShowEvent(this, _STATUS_NO_BNK_SELECTED);
                        _CurrentBnkFile = null;
                    }

                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    string message = string.Format(_ERROR_LOAD_BNK_FAILED, selectedFile);
                    Exception newEx = new Exception(message, ex);

                    _CurrentBnkFile = null;
                    MessageBoxes.ShowError(this, newEx);
                }
            }
        }

        private void bnkOpenToolStripButton_Click(object sender, EventArgs e)
        {
            bnkListView_MouseDoubleClick(sender, new MouseEventArgs(MouseButtons.Left, 2, 0, 0, 0));
        }

        private void bnkBackupToolsStripButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                // Parcours de la liste de fichiers sélectionnés
                foreach (ListViewItem anotherItem in bnkListView.SelectedItems)
                    _BackupFile(CurrentFolder + anotherItem.Text);

                // Mise à jour de la liste
                _RefreshFileList(folderTreeView.SelectedNode, true);
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void bnkDeleteToolStripButton_Click(object sender, EventArgs e)
        {
            int fileCount = bnkListView.SelectedItems.Count;

            if (fileCount == 0)
                return;

            // Confirmation, when deleting multiple files. With a single file, using default Windows dialog.
            bool confirmationAsked = false;

            if (fileCount > 1)
            {
                string message = string.Format(_QUESTION_DELETE_FILES, fileCount);
                DialogResult dr =
                    MessageBoxes.ShowQuestion(this, message, MessageBoxButtons.YesNo);

                if (dr != DialogResult.Yes)
                    return;
                confirmationAsked = true;
            }

            try
            {
                bool deleteResult = false;

                Cursor = Cursors.WaitCursor;

                // Parcours de la liste de fichiers sélectionnés
                foreach (ListViewItem anotherItem in bnkListView.SelectedItems)
                {
                    string fileName = CurrentFolder + @"\" + anotherItem.Text;

                    // On enlève les attributs éventuels
                    File.SetAttributes(fileName, FileAttributes.Normal);

                    // On supprime
                    if (File2.MoveToTrash(fileName, !confirmationAsked))
                        deleteResult = true;
                }

                // Mise à jour de la liste, si nécessaire
                if (deleteResult)
                    _RefreshFileList(folderTreeView.SelectedNode, true);

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void bnkStatusLabel_Click(object sender, EventArgs e)
        {
            // Clic : ouverture de l'explorateur
            try
            {
                ProcessStartInfo explorerProcess = new ProcessStartInfo(CurrentFolder);
                Process.Start(explorerProcess);
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void bnkListView_KeyDown(object sender, KeyEventArgs e)
        {
            // Intercepte la frappe de touches spéciales
            if (e.Control)
            {
                if (e.KeyCode == Keys.A)
                    // CTRL+A : tout sélectionner
                    ListView2.SelectAll(bnkListView);
                else if (e.KeyCode == Keys.B)
                    // CTRL+B : copie de sauvegarde
                    bnkBackupToolsStripButton_Click(this, e);
                else if (e.KeyCode == Keys.N)
                    // CTRL+N : restauration de sauvegarde
                    bnkRestoreToolStripButton_Click(this, e);
                else if (e.KeyCode == Keys.G)
                    // CTRL+G : navigation vers le dossier
                    bnkStatusLabel_Click(this, e);
            }
            else if (e.Alt)
            {
                if (e.KeyCode == Keys.Return)
                    // ALT+ENTER: properties
                    _DisplayProperties(sender, e);
            }
            else if (e.KeyCode == Keys.Return)
                // ENTREE : ouvrir
                bnkOpenToolStripButton_Click(this, new EventArgs());
            else if (e.KeyCode == Keys.Delete)
                // Del : suppression
                bnkDeleteToolStripButton_Click(this, e);
            else if (e.KeyCode == Keys.F5)
                // F5 : actualisation
                refreshTreeToolStripButton_Click(this, e);
        }

        private void folderTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                // F5 : actualisation
                refreshTreeToolStripButton_Click(this, e);
            else if (e.KeyCode == Keys.G)
                // CTRL+G : navigation vers le dossier
                bnkStatusLabel_Click(this, e);
        }

        private void bnkManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Affiche ou masque le BNK Manager
            if (bnkManagerToolStripMenuItem.Checked)
                // Affichage
                hSplitContainer.SplitterDistance = Size.Height - _MANAGER_DEFAULT_HEIGHT;
            else
                // Disparition
                hSplitContainer.SplitterDistance = Size.Height;

            // EVO_93: module visibility
            Program.ApplicationSettings.BnkManagerVisible = bnkManagerToolStripMenuItem.Checked.ToString();
        }

        private void FileBrowserForm_SizeChanged(object sender, EventArgs e)
        {
            // Mise à jour de la disposition des autres contrôles
            if (!bnkManagerToolStripMenuItem.Checked)
                hSplitContainer.SplitterDistance = Size.Height;
        }

        private void editTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Affiche ou masque la fenêtre de suivi d'édition
            if (editTasksToolStripMenuItem.Checked)
            {
                // Affichage
                if (!_EditTasksWindow.Visible)
                    _EditTasksWindow.Show(this);
            }
            else
            {
                // Masquage
                if (_EditTasksWindow.Visible)
                    _EditTasksWindow.Hide();
            }

            // EVO_93: module visibility
            Program.ApplicationSettings.EditTasksVisible = editTasksToolStripMenuItem.Checked.ToString();
        }

        private void bnkRestoreToolStripButton_Click(object sender, EventArgs e)
        {
            if (bnkListView.SelectedItems.Count != 0)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    // Parcours de la liste de fichiers sélectionnés
                    bool isModified = false;

                    foreach (ListViewItem anotherItem in bnkListView.SelectedItems)
                    {
                        if (_RestoreFile(CurrentFolder + anotherItem.Text))
                            isModified = true;
                    }

                    // Mise à jour de la liste
                    if (isModified)
                        _RefreshFileList(folderTreeView.SelectedNode, true);
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Ouvre les options
            new SettingsForm(SettingsForm.SettingsTabPage.Main).ShowDialog(this);
        }

        private void FileBrowserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Closure request
            _Instance = null;
            // Enregistrement des paramètres
            _SaveLocalSettings();
            e.Cancel = false;
        }

        private void iconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // EVO_52
            bnkListView.View = View.LargeIcon;
            listToolStripMenuItem.Checked = false;
            largeIconsToolStripMenuItem.Checked = true;
            detailsToolStripMenuItem.Checked = false;
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // EVO_52
            bnkListView.View = View.List;
            listToolStripMenuItem.Checked = true;
            largeIconsToolStripMenuItem.Checked = false;
            detailsToolStripMenuItem.Checked = false;
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // EVO_52
            bnkListView.View = View.Details;
            listToolStripMenuItem.Checked = false;
            largeIconsToolStripMenuItem.Checked = false;
            detailsToolStripMenuItem.Checked = true;
        }

        private void disableFSCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // EVO_58 : patcher le bnk1.map
            DialogResult dr = MessageBoxes.ShowQuestion(this, _QUESTION_DISABLE_SFC, MessageBoxButtons.OKCancel);

            if (dr == DialogResult.OK)
            {
                bool isSuccess = false;

                Cursor = Cursors.WaitCursor;
                StatusBarLogManager.ShowEvent(this, _STATUS_DISABLING_FSC);

                try
                {
                    MAP leMap = TduFile.GetFile(Program.ApplicationSettings.TduMainFolder + LibraryConstants.FOLDER_BNK + MAP.FILE_MAP) as MAP;

                    if (leMap != null)
                    {
                        leMap.PatchIt(true);
                        isSuccess = true;
                    }
                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    StatusBarLogManager.ShowEvent(this, _STATUS_FSC_FAILED);

                    MessageBoxes.ShowError(this, ex);
                }

                // Mise à jour de la liste
                _RefreshFileList(folderTreeView.SelectedNode, false);

                // ANO_28 : affichage du message de succès
                if (isSuccess)
                    StatusBarLogManager.ShowEvent(this, _STATUS_FSC_DISABLED);
            }
        }

        private void bnkListView_MouseClick(object sender, MouseEventArgs e)
        {
            // Clic droit sur un élément de la liste > génération d'un menu contextuel
            if (e.Button != MouseButtons.Right || bnkListView.SelectedItems.Count == 0)
                return;

            // Nom du fichier sélectionné : si plusieurs fichiers sélectionnés, on demande les commandes génériques
            string fileName = "";

            if (bnkListView.SelectedItems.Count == 1)
                fileName = bnkStatusLabel.Text + @"\" + bnkListView.SelectedItems[0].Text;

            ContextMenuStrip contextMenuStrip = new FileBrowserContextMenuFactory().CreateContextMenu(this, FileBrowserContextMenuFactory.ViewType.FileList, fileName);
            contextMenuStrip.Show(bnkListView.PointToScreen(e.Location));
        }

        private void folderTreeView_MouseClick(object sender, MouseEventArgs e)
        {
            // Clic droit sur un élément de la liste > génération d'un menu contextuel
            if (e.Button != MouseButtons.Right || folderTreeView.SelectedNode != null)
            {
                folderTreeView.ContextMenuStrip = null;
                return;
            }

            ContextMenuStrip contextMenuStrip = new FileBrowserContextMenuFactory().CreateContextMenu(this, FileBrowserContextMenuFactory.ViewType.FolderTree, null);
            contextMenuStrip.Show(folderTreeView.PointToScreen(e.Location));
        }

        private void FileBrowserForm_VisibleChanged(object sender, EventArgs e)
        {
            // Window set to Visible
            if (Visible)
            {
                // EVO_93: edit tasks and bnk manager visibility
                editTasksToolStripMenuItem_Click(sender, new EventArgs());
                bnkManagerToolStripMenuItem_Click(sender, new EventArgs());
            }
        }

        private void _DisplayProperties(object sender, EventArgs e)
        {
            // Displays properties Dialog
            // EVO_149: multiple file support
            foreach (ListViewItem anotherItem in bnkListView.SelectedItems)
            {
                string fileName = bnkStatusLabel.Text + @"\" + anotherItem.Text;
                PropertiesDialog dialog = new PropertiesDialog(fileName);

                dialog.Show(this);
            }
        }

        /// <summary>
        /// Signale que la fenêtre de gestion des éditions a été fermée ou ouverte
        /// </summary>
        /// <param name="isVisible">true : visible, false : invisible</param>
        internal void SetEditWindowVisible(bool isVisible)
        {
            editTasksToolStripMenuItem.Checked = isVisible;
            editTasksToolStripMenuItem_Click(this, new EventArgs());
        }

        /// <summary>
        /// Signale que le contenu d'un fichier BNK a été modifié et nécessite rafraîchissement
        /// </summary>
        internal void SetBnkContentsChanged()
        {
            // Lists update
            _RefreshContentLists(true);
        }
        #endregion

        #region Méthodes privées
        /// <summary>
        /// Définit le contenu
        /// </summary>
        private void _InitializeContents()
        {
            // Mode d'affichage de la liste de fichiers
            View currentView = (View) Enum.Parse(typeof(View), Program.ApplicationSettings.FileBrowserViewMode);

            switch (currentView)
            {
                case View.List:
                    listToolStripMenuItem.Checked = true;
                    break;
                case View.LargeIcon:
                    largeIconsToolStripMenuItem.Checked = true;
                    break;
                case View.Details:
                    detailsToolStripMenuItem.Checked = true;
                    break;
                default:
                    currentView = View.LargeIcon;
                    largeIconsToolStripMenuItem.Checked = true;
                    break;
            }

            bnkListView.View = currentView;

            // Packed files display mode
            bool isFlatView = bool.Parse(Program.ApplicationSettings.BnkManagerFlatDisplayMode);

            fileViewFlatToolStripMenuItem.Checked = isFlatView;
            fileViewHierarchicalToolStripMenuItem.Checked = !isFlatView;

            _SetPackedFilesView();

            // Liste de dossiers
            _RefreshTreeView();

            // Fenêtre de suivi des éditions
            _EditTasksWindow = new EditTasksForm();

            // EVO_93: module visibility
            bnkManagerToolStripMenuItem.Checked = Boolean.Parse(Program.ApplicationSettings.BnkManagerVisible);
            editTasksToolStripMenuItem.Checked = Boolean.Parse(Program.ApplicationSettings.EditTasksVisible);
        }

        /// <summary>
        /// Actualise l'arborescence
        /// </summary>
        private void _RefreshTreeView()
        {
            // On efface tout
            folderTreeView.Nodes.Clear();
            bnkStatusLabel.Text = bnkStatusLabelCountSize.Text = "";

            StatusBarLogManager.ShowEvent(this, _STATUS_NO_BNK_SELECTED);

            // Le dossier des bnk est-il valide ?
            DirectoryInfo di = new DirectoryInfo(LibraryConstants.GetSpecialFolder(LibraryConstants.TduSpecialFolder.Bnk));

            if (!di.Exists)
            {
                // On efface tout
                bnkListView.Items.Clear();
                _ClearContentLists();

                MessageBoxes.ShowError(this, _ERROR_INVALID_TDU_PATH);
                return;
            }

            Cursor = Cursors.WaitCursor;

            // Noeud racine
            TreeNode rootNode = new TreeNode(_TREENODE_ROOT)
                                    {
                                        Tag = LibraryConstants.GetSpecialFolder(LibraryConstants.TduSpecialFolder.Bnk),
                                        ImageIndex = (int) ItemPictures.ClosedFolder,
                                        SelectedImageIndex = (int) ItemPictures.OpenedFolder
                                    };
            folderTreeView.Nodes.Add(rootNode);

            // Parcours des dossiers
            try
            {
                _AddFoldersToTreeView(rootNode);

                // On déploie le noeud racine
                rootNode.Expand();

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        /// <summary>
        /// Ajoute les dossiers au TreeView. Méthode récursive.
        /// </summary>
        /// <param name="parentNode">Noeud sous lequel attacher les sous dossiers.
        /// La propriété Tag doit mentionner le chemin courant.</param>
        private static void _AddFoldersToTreeView(TreeNode parentNode)
        {
            DirectoryInfo parentFolder = new DirectoryInfo((string)parentNode.Tag);
            DirectoryInfo[] folderList = parentFolder.GetDirectories();

            foreach (DirectoryInfo anotherFolder in folderList)
            {
                TreeNode newSubfolderNode = new TreeNode(anotherFolder.Name)
                                                {
                                                    Tag = anotherFolder.FullName,
                                                    ImageIndex = (int) ItemPictures.ClosedFolder,
                                                    SelectedImageIndex = (int) ItemPictures.OpenedFolder
                                                };

                // On ajoute le noeud
                parentNode.Nodes.Add(newSubfolderNode);

                // On ajoute les noeuds enfants
                _AddFoldersToTreeView(newSubfolderNode);
            }
        }

        /// <summary>
        /// Met à jour la liste de fichiers dans le répertoire courant
        /// </summary>
        /// <param name="selectedNode">Noeud sélectionné</param>
        /// <param name="keepSelection">true to keep previous item selected in file list</param>
        private void _RefreshFileList(TreeNode selectedNode, bool keepSelection)
        {
            // ANO 15 : sauvegarde de la sélection de fichier courante
            if (keepSelection)
                ListView2.StoreSelectedIndex(bnkListView);

            // Effacement des listes et contexte
            bnkListView.Items.Clear();
            _ClearContentLists();
            _CurrentBnkFile = null;

            // Mise à jour statuts
            bnkStatusLabel.Text = bnkStatusLabelCountSize.Text = "";

            StatusBarLogManager.ShowEvent(this, _STATUS_NO_BNK_SELECTED);

            if (selectedNode == null)
                return;

            try
            {
                Cursor = Cursors.WaitCursor;

                string currentFolder = (string)selectedNode.Tag;
                DirectoryInfo di = new DirectoryInfo(currentFolder);
                FileInfo[] fileList = di.GetFiles();
                long totalSize = 0;

                foreach (FileInfo anotherBnk in fileList)
                {
                    // File name check
                    bool isNameValid = false;

                    foreach (string anotherPattern in _ALLOWED_FILENAME_PATTERNS)
                    {
                        if (Regex.IsMatch(anotherBnk.Name, anotherPattern, RegexOptions.IgnoreCase))
                        {
                            isNameValid = true;
                            break;
                        }
                    }

                    if (isNameValid)
                    {
                        // On ajoute l'élément
                        ListViewItem newItem = new ListViewItem(anotherBnk.Name)
                        {
                            ImageIndex = _GetImageIndexFromFilename(anotherBnk.Name)
                        };

                        // L'image dépend de l'extension
                        // EVO_42 : nouvelles icônes

                        string typeDesc = TduFile.GetTypeDescription(anotherBnk.Name);

                        // EVO_57 : Infos supplémentaires pour la vue détaillée
                        // Taille
                        long currentSize = anotherBnk.Length;

                        newItem.SubItems.Add(currentSize.ToString());
                        totalSize += currentSize;
                        // Type
                        newItem.SubItems.Add(typeDesc);

                        // Tooltip !
                        newItem.ToolTipText = string.Format(_TOOLTIP_STD_FILE,
                            typeDesc,
                            anotherBnk.Length);

                        bnkListView.Items.Add(newItem);   
                    }
                }

                // Mise à jour statut
                bnkStatusLabel.Text = currentFolder;

                if (bnkListView.Items.Count == 0)
                    bnkStatusLabelCountSize.Text = _STATUS_COUNT_SIZE_NO;
                else
                    bnkStatusLabelCountSize.Text = string.Format(_STATUS_COUNT_SIZE_CONTENTS, bnkListView.Items.Count, totalSize);

                // Rétablit le fichier sélectionné
                if (keepSelection)
                {
                    ListView2.RestoreSelectedIndex(bnkListView);

                    // ANO_21 : le contenu du BNK est à actualiser
                    bnkListView_SelectedIndexChanged(this, new EventArgs());
                }

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        /// <summary>
        /// EVO_42 : Renvoie l'index d'image selon l'extension du fichier
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>L'index de l'image à utiliser</returns>
        private static int _GetImageIndexFromFilename(string fileName)
        {
            int imgIndex = (int)ItemPictures.Sheet;

            // New mapping
            if (!string.IsNullOrEmpty(fileName))
            {
                foreach (KeyValuePair<string, ItemPictures> itemPictures in _PICTURES_BY_PATTERNS)
                {
                    if (Regex.IsMatch(fileName, itemPictures.Key, RegexOptions.IgnoreCase))
                    {
                        imgIndex = (int) itemPictures.Value;
                        break;
                    }
                    
                }
            }

            return imgIndex;
        }

        /// <summary>
        /// Crée une copie de sauvegarde du fichier spécifié
        /// </summary>
        /// <param name="fileName">nom de fichier à sauvegarder</param>
        private void _BackupFile(string fileName)
        {
            // According to file type...
            if (Regex.IsMatch(fileName, BNK.FILENAME_PATTERN, RegexOptions.IgnoreCase ))
            {
                // Fichier BNK -> on crée une copie de sauvegarde par la méthode personnalisée
                // Chargement du fichier BNK
                BNK leBNK = TduFile.GetFile(fileName) as BNK;

                if (leBNK != null)
                    // Backup
                    leBNK.MakeBackup();
            }
            else if (Regex.IsMatch(fileName, TduFile.BACKUP_FILENAME_PATTERN, RegexOptions.IgnoreCase))
            {
                // Backup -> sauvegarde inutile
                string message = string.Format(_ERROR_BACKUP_OF_BACKUP, fileName);

                MessageBoxes.ShowWarning(this, message);
            }
            else
                // Pour les autres fichiers on effectue la sauvegarde par défaut
                Tools.BackupFile(fileName, fileName + "." + LibraryConstants.EXTENSION_BACKUP);
        }

        /// <summary>
        /// Restaure le fichier d'origine depuis la sauvegarde spécifiée
        /// </summary>
        /// <param name="fileName">nom du fichier de sauvegarde</param>
        private bool _RestoreFile(string fileName)
        {
            bool isModified = false;
            string message;

            if (Regex.IsMatch(fileName, TduFile.BACKUP_FILENAME_PATTERN, RegexOptions.IgnoreCase))
            {
                // Backup -> restauration
                string originalFileName = File2.ChangeExtensionOfFilename(fileName, null);
                message = string.Format(_QUESTION_RESTORE, originalFileName, fileName);
                DialogResult dr = MessageBoxes.ShowQuestion(this, message, MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    // Opération de restauration
                    Tools.RestoreFile(fileName, originalFileName);
                    isModified = true;
                }
            }
            else
            {
                // Pour les autres fichiers on interdit la restauration
                message = string.Format(_ERROR_NOT_A_BACKUP, fileName);
                MessageBoxes.ShowWarning(this, message);
            }

            return isModified;
        }

        /// <summary>
        /// Permet la sélection d'un fichier de remplacement
        /// </summary>
        /// <param name="defaultFileName">Nom de fichier par défaut</param>
        /// <returns>Le chemin du fichier sélectionné, null si l'opération a été annulée</returns>
        private string _SelectReplacementFile(string defaultFileName)
        {
            string returnedFileName = null;

            // Paramètres de la boîte de sélection
            string fileExtension = File2.GetExtensionFromFilename(defaultFileName);

            openFileDialog.Filter = string.Concat(GuiConstants.MakeSelectionFilter(fileExtension, ""), "|",
                                                  GuiConstants.FILTER_ALL_FILES);
            openFileDialog.FileName = defaultFileName;
            openFileDialog.Title = string.Format(_TITLE_REPLACE_SELECTION, defaultFileName);

            // Mémorisation du dernier emplacement
            if (_LastReplaceLocation == null)
            {
                // EVO_102: default location
                if (string.IsNullOrEmpty(Program.ApplicationSettings.DefaultEditNewFilesFolder))
                    _LastReplaceLocation = bnkStatusLabel.Text;
                else
                    _LastReplaceLocation = Program.ApplicationSettings.DefaultEditNewFilesFolder;
            }

            openFileDialog.InitialDirectory = _LastReplaceLocation;

            // Ouverture fenêtre
            DialogResult dr = openFileDialog.ShowDialog(this);

            if (dr == DialogResult.OK)
            {
                // Mémorisation du dernier emplacement
                FileInfo selectedFileInfo = new FileInfo(openFileDialog.FileName);
                _LastReplaceLocation = selectedFileInfo.DirectoryName;

                returnedFileName = openFileDialog.FileName;
            }

            return returnedFileName;
        }

        /// <summary>
        /// Se charge d'enregistrer les paramètres locaux à la fenêtre
        /// </summary>
        private void _SaveLocalSettings()
        {
            try
            {
                // Mode d'affichage de la liste de fichiers
                Program.ApplicationSettings.FileBrowserViewMode = bnkListView.View.ToString();
                Program.ApplicationSettings.BnkManagerFlatDisplayMode = _IsContentFlatView.ToString();
                // EVO_93: module visibility are updated automatically (on menu item click)
                Program.ApplicationSettings.Save();
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, new Exception(_ERROR_SAVING_SETTINGS, ex));
            }
        }

        /// <summary>
        /// Loads specified BNK file into BNK Manager
        /// </summary>
        /// <param name="selectedFile"></param>
        private void _LoadBnkFile(string selectedFile)
        {
            // Vérification : si le fichier n'est pas déjà chargé
            if (_CurrentBnkFile == null || !selectedFile.Equals(_CurrentBnkFile.FileName))
            {
                // Chargement du fichier BNK
                StatusBarLogManager.ShowEvent(this, _STATUS_BNK_LOADING);

                _CurrentBnkFile = TduFile.GetFile(selectedFile) as BNK;

                if (_CurrentBnkFile != null)
                {
                    // Mise à jour des listes
                    SetBnkContentsChanged();
                    // No selection in packed list
                    contentListView.SelectedItems.Clear();
                    contentTreeView.SelectedNode = null;
                }
            }
        }
        
        /// <summary>
        /// Extracts all contents from BNK current file
        /// </summary>
        private void _ExtractAllBnkContents(object sender, EventArgs e)
        {
            // Flat view only
            fileViewFlatToolStripMenuItem.Checked = true;
            fileViewFlatToolStripMenuItem_Click(sender, e);

            ListView2.SelectAll(contentListView);
            fileExtractToolStripButton_Click(sender, e);
        }
        #endregion
    }
}
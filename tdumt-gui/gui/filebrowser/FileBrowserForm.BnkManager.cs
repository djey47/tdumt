using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Types;
using DjeFramework1.Common.Types.Forms;
using DjeFramework1.Util.Enums;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.support.editing;
using TDUModdingTools.common;
using TDUModdingTools.common.handlers;
using TDUModdingTools.gui.filebrowser.common;
using TDUModdingTools.gui.filebrowser.properties;

namespace TDUModdingTools.gui.filebrowser
{
    /// <summary>
    /// FileBrowser:BnkManager part
    /// </summary>
    partial class FileBrowserForm
    {
        #region Properties
        /// <summary>
        /// Returns count of selected packed files
        /// </summary>
        internal int _SelectedPackedFilesCount
        {
            get { return contentListView.SelectedItems.Count; }
        }
        
        /// <summary>
        /// Returns true if content view is flat, false if view is hierarchical
        /// </summary>
        internal bool _IsContentFlatView
        {
            get { return fileViewFlatToolStripMenuItem.Checked; }
        }
        #endregion

        #region Events
        private void contentListView_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                try
                {
                    string[] listeFichiers = (string[])e.Data.GetData(DataFormats.FileDrop);
                    string selectedFile = listeFichiers[0];

                    // Selon l'extension... 
                    // EVO 17 : remplacement de fichiers par glisser-déplacer
                    Collection<String> failedFileNames;

                    Cursor = Cursors.WaitCursor;
                    if (! Regex.IsMatch(selectedFile, BNK.FILENAME_PATTERN, RegexOptions.IgnoreCase))
                    {
                        // Remplacement
                        bool success = true;

                        if (_CurrentBnkFile != null)
                        {
                            bool isAtLeastOneReplacing = false;

                            // BUG_33: gestion du remplacement de plusieurs fichiers
                            failedFileNames = new Collection<string>();

                            foreach (string anotherSelectedFile in listeFichiers)
                            {
                                bool replaceResult = _DirectReplace(anotherSelectedFile);

                                if (replaceResult)
                                    isAtLeastOneReplacing = true;
                                else
                                {
                                    failedFileNames.Add(anotherSelectedFile);
                                    success = false;
                                }
                            }

                            // BUG_33: mise en évidence des fichiers modifiés + mise à jour de la liste
                            if (isAtLeastOneReplacing)
                                SetBnkContentsChanged();

                            // Signals
                            string message;

                            if (success)
                            {
                                // EVO_32 : Signal de réussite
                                FileInfo fi = new FileInfo(selectedFile);

                                if (listeFichiers.Length == 1)
                                    message = string.Format(_STATUS_REPLACE_SUCCESS_SINGLE, fi.Name);
                                else
                                    message = string.Format(_STATUS_REPLACE_SUCCESS_MANY, listeFichiers.Length);
                            }
                            else
                                // EVO_32 : Signal d'échec
                                message = _STATUS_REPLACE_FAILED;

                            StatusBarLogManager.ShowEvent(this, message);
                        }
                    }
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

        private void contentListView_DragEnter(object sender, DragEventArgs e)
        {
            // Autorisé seulement si c'est un fichier
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void contentListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Double clic dans la liste de fichiers empaquetés
            if (e.Button == MouseButtons.Left && _GetSelectedFilePaths().Count == 1)
            {
                // EVO 49 : ajout de raccourcis
                if (_ContentListShiftKeyPressed)
                    // Touche Shift + double click : édition
                    fileEditToolStripButton_Click(sender, e);
                else
                    fileOpenToolStripButton_Click(sender, e);
            }
        }

        private void contentListView_MouseLeave(object sender, EventArgs e)
        {
            // EVO 49
            _ContentListShiftKeyPressed = false;
        }

        private void contentListView_MouseClick(object sender, MouseEventArgs e)
        {
            // Clic droit sur un élément de la liste > génération d'un menu contextuel
            Control currentControl;
            Collection<string> selectedFilePaths = _GetSelectedFilePaths();

            if (_IsContentFlatView)
                currentControl = contentListView;
            else
                currentControl = contentTreeView;

            if (e.Button == MouseButtons.Right && selectedFilePaths.Count != 0)
            {
                // Nom du fichier sélectionné : si plusieurs fichiers sélectionnés, on demande les commandes génériques
                string fileName = "";

                if (selectedFilePaths.Count == 1)
                {
                    string packedFileName = _CurrentBnkFile.GetPackedFileName(selectedFilePaths[0]);

                    fileName = bnkStatusLabel.Text + @"\" + packedFileName;
                }

                ContextMenuStrip contextMenuStrip = new FileBrowserContextMenuFactory().CreateContextMenu(this, FileBrowserContextMenuFactory.ViewType.ContentListFlat, fileName);
                contextMenuStrip.Show(currentControl.PointToScreen(e.Location));
            }
            else
                currentControl.ContextMenuStrip = null;
        }

        private void contentListView_KeyDown(object sender, KeyEventArgs e)
        {
            // Intercepte la frappe de touches spéciales
            if (e.Control)
            {
                if (e.KeyCode == Keys.A && _IsContentFlatView)
                    // CTRL+A : tout sélectionner
                    ListView2.SelectAll(contentListView);
                else if (e.KeyCode == Keys.E)
                    // CTRL+E : extraire
                    fileExtractToolStripButton_Click(this, e);
                else if (e.KeyCode == Keys.L)
                    // CTRL+L: load Bnk file
                    fileLoadBnkToolStripButton_Click(this, e);
                else if (e.KeyCode == Keys.R)
                {
                    if (e.Shift)
                        // CTRL+SHIFT+R : remplacer + renommer
                        changeNameToolStripMenuItem_Click(this, e);
                    else
                        // CTRL+R : remplacer
                        keepNameToolStripMenuItem_Click(this, e);
                }
            }
            else if (e.Alt)
            {
                if (e.KeyCode == Keys.Return)
                    //ALT+ENTER : PackedPropertiesTarget file properties
                    _DisplayPackedProperties(this, e);
            }
            else if (e.Shift)
                // EVO 49 : capture de la touche SHIFT
                _ContentListShiftKeyPressed = true;
            else if (e.KeyCode == Keys.Return)
                // ENTREE : visualiser
                fileOpenToolStripButton_Click(this, e);
            else if (e.KeyCode == Keys.F2)
                // F2: rename
                fileRenameToolStripButton_Click(this, e);
            //else if (e.KeyCode == Keys.Delete)
                // EVO_126 : Del: delete
                //fileDeleteToolStripButton_Click(this, e);
        }

        private void contentListView_KeyUp(object sender, KeyEventArgs e)
        {
            // EVO 49 : capture de la touche SHIFT
            if (e.KeyCode == Keys.ShiftKey)
                _ContentListShiftKeyPressed = false;
        }

        private void contentTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contentTreeView.SelectedNode = e.Node;

            contentListView_MouseClick(sender, new MouseEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta));
        }

        private void contentTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            contentListView_MouseDoubleClick(sender, new MouseEventArgs(e.Button, e.Clicks, e.X, e.Y, e.Delta));
        }

        private void fileRenameToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on 'Rename' button
            if (_GetSelectedFilePaths().Count == 1)
            {
                try
                {
                    if (_IsContentFlatView)
                        // Flat
                        contentListView.SelectedItems[0].BeginEdit();
                    else
                        // Hierarchical
                        contentTreeView.SelectedNode.BeginEdit();
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void contentListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            // After one name has been typed and validated
            if (e.Label != null)
            {
                ListView senderList = sender as ListView;

                if (senderList != null)
                {
                    ListViewItem currentItem = senderList.Items[e.Item];

                    // Exceptions are handled into next method
                    if (!_RenamePackedFile(currentItem.Tag.ToString(), e.Label))
                        // Restoring label on error
                        e.CancelEdit = true;
                }
            }
        }

        private void contentTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            // After one name has been typed and validated
            if (e.Label != null)
            {
                TreeView senderTree = sender as TreeView;

                if (senderTree != null)
                {
                    TreeNode currentNode = e.Node;
                    TreeNode extensionGroupNode = currentNode.Parent;
                    string newFileName = (e.Label + extensionGroupNode.Text);

                    // Exceptions are handled into next method
                    if (!_RenamePackedFile(currentNode.Tag.ToString(), newFileName))
                        // Restoring label on error
                        e.CancelEdit = true;    
                }
            }
        }

        private void contentTreeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            // Before typing a new node name
            // Renaming a folder or extension group node is not allowed
            if (e.Node.Tag == null)
                e.CancelEdit = true;
        }

        private void fileLoadBnkToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on 'Load...' button (Bnk manager) 
            try
            {
                openFileDialog.Filter = GuiConstants.FILTER_BNK_FILES;
                openFileDialog.Title = _TITLE_OPEN_BNK;

                DialogResult dr = openFileDialog.ShowDialog(this);

                if (dr == DialogResult.OK)
                    // Loading BNK file
                    _LoadBnkFile(openFileDialog.FileName);
            }
            catch (Exception ex)
            {
                string message = string.Format(_ERROR_LOAD_BNK_FAILED, openFileDialog.FileName);
                
                _CurrentBnkFile = null;
                MessageBoxes.ShowError(this, new Exception(message, ex));
            }
        }
        
        private void fileOpenToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on 'Open' button
            foreach (string currentPackedFilePath in _GetSelectedFilePaths())
            {
                Cursor = Cursors.WaitCursor;

                try
                {
                    _ViewPackedFile(currentPackedFilePath);
                }
                catch (Exception ex)
                {
                    string message = string.Format(_ERROR_SHOW_OR_EDIT_FAILED, _WORD_OPEN, File2.GetExtensionFromFilename(currentPackedFilePath))
                        + Environment.NewLine
                        + ex.Message;

                    MessageBoxes.ShowError(this, new Exception(message, ex));
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }
 
        private void fileEditToolStripButton_Click(object sender, EventArgs e)
        {
            // Clic sur le bouton d'édition
            Cursor = Cursors.WaitCursor;

            // EVO 45 : gestion de plusieurs demandes d'édition
            int successCount = 0;

            foreach (string currentPackedFileName in _GetSelectedFilePaths())
            {
                try
                {
                    // Affiche la fenêtre d'édition
                    if (!_EditTasksWindow.Visible)
                    {
                        editTasksToolStripMenuItem.Checked = true;
                        editTasksToolStripMenuItem_Click(sender, new EventArgs());
                    }

                    // Création de la tâche d'édition et ajout à la liste
                    _EditPackedFile(currentPackedFileName);

                    successCount++;
                }
                catch (Exception ex)
                {
                    string message = string.Format(_ERROR_SHOW_OR_EDIT_FAILED, _WORD_EDIT, File2.GetExtensionFromFilename(currentPackedFileName));
                    Exception newEx = new Exception(message, ex);

                    MessageBoxes.ShowError(this, newEx);
                }
            }

            if (successCount > 0)
            {
                string message = string.Format(_STATUS_EDIT_SUCCESS, successCount);

                StatusBarLogManager.ShowEvent(this, message);
            }

            Cursor = Cursors.Default;
        }

        private void fileExtractToolStripButton_Click(object sender, EventArgs e)
        {
            if (_GetSelectedFilePaths().Count == 0)
                // Aucune sélection > on sélectionne tout
                ListView2.SelectAll(contentListView);

            // Vérification de la sélection effective
            int fileCount = _GetSelectedFilePaths().Count;

            if (fileCount != 0)
            {
                try
                {
                    // Mémorisation du dernier emplacement d'extraction
                    if (_LastExtractLocation == null)
                        _LastExtractLocation = CurrentFolder;

                    folderBrowserDialog.SelectedPath = _LastExtractLocation;
                    folderBrowserDialog.Description =
                        string.Format(_LABEL_FOLDER_BROWSE_EXTRACT, fileCount);

                    DialogResult dr =
                        folderBrowserDialog.ShowDialog();

                    if (dr == DialogResult.OK)
                    {
                        _LastExtractLocation = folderBrowserDialog.SelectedPath;

                        // Parcours de la liste de fichiers sélectionnés
                        Cursor = Cursors.WaitCursor;

                        foreach (string currentPackedFileName in _GetSelectedFilePaths())
                            _ExtractPackedFile(currentPackedFileName, _LastExtractLocation);

                        Cursor = Cursors.Default;

                        // EVO_136: display files in explorer after extract, if wanted
                        if (bool.Parse(Program.ApplicationSettings.ExtractDisplayInExplorer))
                        {
                            ProcessStartInfo explorerProcess = new ProcessStartInfo(_LastExtractLocation);
                            Process.Start(explorerProcess);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void keepNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Clic sur le bouton de remplacement (mode "Keep Name")
            if (_GetSelectedFilePaths().Count != 0)
            {
                try
                {
                    Collection<string> newFileNames;
                    int modifiedCount = _ReplacePackedFiles(out newFileNames);

                    if (modifiedCount >= 1)
                    {
                        Cursor = Cursors.WaitCursor;

                        // BUG_18: rechargement
                        SetBnkContentsChanged();

                        // EVO_32 : message de réussite
                        string message = string.Format(_STATUS_REPLACE_SUCCESS_MANY, modifiedCount);

                        StatusBarLogManager.ShowEvent(this, message);

                        Cursor = Cursors.Default;
                    }
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

        private void changeNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Clic sur le bouton de remplacement (mode "Change Name")
            Collection<string> selectedFilePaths = _GetSelectedFilePaths();
            
            if (selectedFilePaths.Count > 0)
            {
                try
                {
                    Collection<string> newFileNames;
                    int modifiedCount = _ReplacePackedFiles(out newFileNames);

                    if (modifiedCount >= 1)
                    {
                        Cursor = Cursors.WaitCursor;

                        // Applies renaming with valid file names
                        int index = 0;

                        foreach (string anotherPath in selectedFilePaths)
                        {
                            string currentFileName = newFileNames[index++];

                            if (!string.IsNullOrEmpty(currentFileName))
                                _RenamePackedFile(anotherPath, new FileInfo(currentFileName).Name);
                        }

                        // Reloading is not needed as already done by _RenamePackedFile method

                        // EVO_32 : message de réussite
                        string message = string.Format(_STATUS_REPLACE_RENAME_SUCCESS_MANY, modifiedCount);

                        StatusBarLogManager.ShowEvent(this, message);

                        Cursor = Cursors.Default;
                    }
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

        private void fileViewFlatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Flat'
            fileViewHierarchicalToolStripMenuItem.Checked = false;
            _SetPackedFilesView();
        }

        private void fileViewHierarchicalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Hierarchical'
            fileViewFlatToolStripMenuItem.Checked = false;
            _SetPackedFilesView();
        }

        private void _DisplayPackedProperties(object sender, EventArgs e)
        {
            // Displays properties Dialog for packed file
            // EVO_149: multiple file support

            foreach (string anotherPath in _GetSelectedFilePaths())
            {           
                try
                {
                    Cursor = Cursors.WaitCursor;

                    // Preparing
                    string viewFileName = EditHelper.Instance.PrepareFile(_CurrentBnkFile, anotherPath);
                    PropertiesDialog dialog = new PropertiesDialog(viewFileName);

                    dialog.Show(this);
                }
                catch (Exception ex)
                {
                    string message = string.Format(_ERROR_PACK_PROPERTIES_FAILED, anotherPath);

                    MessageBoxes.ShowError(this, new Exception(message, ex));
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void fileSearchToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            // Contents of search box have changed
            try
            {
                Cursor = Cursors.WaitCursor;

                ListView2.FilterItems(contentListView, fileSearchToolStripTextBox.Text, SearchType.Contains,
                                      StringComparison.InvariantCultureIgnoreCase);
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

        private void fileDeleteToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on 'Delete' tool strip button
            // EVO_126: deletion support
            Collection<string> selectedFilePaths = _GetSelectedFilePaths();

            if (selectedFilePaths.Count > 0)
            {
                try
                {
                    // Warning message
                    DialogResult dr = MessageBoxes.ShowWarning(this, _WARNING_DELETION, MessageBoxButtons.OKCancel);

                    if (dr == DialogResult.OK)
                    {
                        Cursor = Cursors.WaitCursor;

                        foreach (string anotherPath in selectedFilePaths)
                            _CurrentBnkFile.DeletePackedFile(anotherPath);

                        // EVO_32 : success message
                        StatusBarLogManager.ShowEvent(this, _STATUS_DELETE_PACKED_SUCCESS);
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
                finally
                {
                    SetBnkContentsChanged();
                    Cursor = Cursors.Default;
                }
            }
        }
        #endregion

        #region Targets for remote invokes
        /// <summary>
        /// Replace with name kept
        /// </summary>
        public EventHandler ReplaceKeepNameTarget
        {
            get { return keepNameToolStripMenuItem_Click; }
        }

        /// <summary>
        /// Replace with name change
        /// </summary>
        public EventHandler ReplaceRenameTarget
        {
            get { return changeNameToolStripMenuItem_Click; }
        }

        /// <summary>
        /// Rename packed file
        /// </summary>
        public EventHandler RenameTarget
        {
            get { return fileRenameToolStripButton_Click; }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Met à jour la liste de fichiers empaquetés  dans le BNK spécifié
        /// </summary>
        /// <param name="keepSelection">true to ensure current selected item will be kept after refresh</param>
        private void _RefreshContentLists(bool keepSelection)
        {
            // Stores selected item if needed
            if (keepSelection)
            {
                ListView2.StoreSelectedIndex(contentListView);
                TreeView2.StoreSelectedIndex(contentTreeView);
            }

            // Lists clearing
            contentListView.Items.Clear();
            contentTreeView.Nodes.Clear();

            if (_CurrentBnkFile != null)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    // 1. ListView (flat)
                    foreach (string filePath in _CurrentBnkFile.GetPackedFilesPaths(null))
                    {
                        // File name
                        string fileName = _CurrentBnkFile.GetPackedFileName(filePath);

                        if (fileName == null)
                            throw new Exception(_ERROR_INVALID_DATA);

                        // Récupération des données fichier
                        ListViewItem anotherItem = new ListViewItem(fileName)
                                                       {
                                                           Tag = filePath,
                                                           ImageIndex = _GetImageIndexFromFilename(fileName),
                                                           ToolTipText = _GetPackedToolTip(filePath, false)
                                                       };

                        // Tag
                        // Image...
                        // EVO_42 : nouvelles icônes
                        // EVO_10 : Tooltip (type de fichier - taille)

                        contentListView.Items.Add(anotherItem);
                    }

                    // Applying filter, if any
                    fileSearchToolStripTextBox_TextChanged(this, new EventArgs());

                    // 2. TreeView (hierarchical)
                    BNK.FileInfoNode rootNode = _CurrentBnkFile.FileInfoHierarchyRoot;
                    TreeNode rootTreeNode = new TreeNode(_TREENODE_ROOT)
                                                {
                                                    ImageIndex = (int) ItemPictures.ClosedPackedFolder,
                                                    SelectedImageIndex = (int) ItemPictures.OpenedPackedFolder
                                                };

                    contentTreeView.Nodes.Add(rootTreeNode);

                    _AddPackedNodeToTreeView(rootNode, rootTreeNode);
                    contentTreeView.ExpandAll();

                    // Restoring selected item, if needed
                    if (keepSelection)
                    {
                        ListView2.RestoreSelectedIndex(contentListView);
                        TreeView2.RestoreSelectedIndex(contentTreeView);
                    }

                    // Mise à jour statut
                    string countSizeInfo = 
                        string.Format(_STATUS_COUNT_SIZE_CONTENTS, _CurrentBnkFile.PackedFilesCount, _CurrentBnkFile.Size);
                    string message = 
                        string.Format(_STATUS_BNK_CONTENTS,
                                                   _CurrentBnkFile.FileName,
                                                   countSizeInfo);

                    StatusBarLogManager.ShowEvent(this, message);

                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        /// <summary>
        /// Clears listview and treeview
        /// </summary>
        private void _ClearContentLists()
        {
            contentListView.Items.Clear();
            contentTreeView.Nodes.Clear();

            // Search box
            fileSearchToolStripTextBox.Text = "";
        }

        /// <summary>
        /// Add packed file info
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="parentTreeNode"></param>
        private void _AddPackedNodeToTreeView(BNK.FileInfoNode parentNode, TreeNode parentTreeNode)
        {
            if (parentNode != null)
            {
                TreeNode currentTreeNode = new TreeNode {Text = parentNode.name};

                if (parentNode.type == BNK.FileInfoNodeType.Folder)
                {
                    currentTreeNode.ImageIndex = (int)ItemPictures.ClosedPackedFolder;
                    currentTreeNode.SelectedImageIndex = (int)ItemPictures.OpenedPackedFolder;
                    currentTreeNode.ToolTipText = _TOOLTIP_FOLDER;
                }
                else if (parentNode.type == BNK.FileInfoNodeType.File)
                {
                    currentTreeNode.ImageIndex = _GetImageIndexFromFilename(parentNode.FileName);
                    currentTreeNode.SelectedImageIndex = _GetImageIndexFromFilename(parentNode.FileName);
                    currentTreeNode.Tag = parentNode.Path;
                    currentTreeNode.ToolTipText = _GetPackedToolTip(parentNode.Path, false);
                }
                else if (parentNode.type == BNK.FileInfoNodeType.ExtensionGroup)
                {
                    currentTreeNode.ImageIndex = (int)ItemPictures.ClosedExtFolder;
                    currentTreeNode.SelectedImageIndex = (int)ItemPictures.OpenedExtFolder;
                    currentTreeNode.ToolTipText = _TOOLTIP_EXTENSION_GROUP;
                    currentTreeNode.NodeFont = new Font(contentTreeView.Font, FontStyle.Bold);
                }

                parentTreeNode.Nodes.Add(currentTreeNode);

                foreach (BNK.FileInfoNode anotherNode in parentNode.childNodes)
                    _AddPackedNodeToTreeView(anotherNode, currentTreeNode);
            }

        }

        /// <summary>
        /// Defines current view for packed files
        /// </summary>
        private void _SetPackedFilesView()
        {
            contentTreeView.Visible = !_IsContentFlatView;
            contentListView.Visible = _IsContentFlatView;

            // Search box
            fileSearchToolStripTextBox.Enabled = _IsContentFlatView;
        }

        /// <summary>
        /// Updates contents of packed file tooltip 
        /// </summary>
        /// <param name="filePath">simple file name</param>
        /// <param name="isModified">true to indicate the file has been modified</param>
        private string _GetPackedToolTip(string filePath, bool isModified)
        {
            string returnedTip = null;

            if (_CurrentBnkFile != null && !string.IsNullOrEmpty(filePath))
            {
                long packedFileSize = _CurrentBnkFile.GetPackedFileSize(filePath);
                string packedFileTypeDescription = _CurrentBnkFile.GetPackedFileTypeDescription(filePath);
                string modifiedPart = isModified ? _TOOLTIP_PACKED_FILE_MODIFIED : "";

                returnedTip =
                    string.Format(_TOOLTIP_PACKED_FILE, packedFileTypeDescription, packedFileSize, modifiedPart);
            }

            return returnedTip;
        }

        /// <summary>
        /// Returns a list of selected file paths in packed files view (flat/hierarchical)
        /// </summary>
        /// <returns></returns>
        private Collection<string> _GetSelectedFilePaths()
        {
            Collection<string> returnedList = new Collection<string>();

            if (_IsContentFlatView)
            {
                // Flat > ListView
                foreach (ListViewItem selectedItem in contentListView.SelectedItems)
                    returnedList.Add(selectedItem.Tag.ToString());
            }
            else
                // Hierarchical > TreeView
                if (contentTreeView.SelectedNode != null && contentTreeView.SelectedNode.Tag != null)
                    returnedList.Add(contentTreeView.SelectedNode.Tag.ToString());

            return returnedList;
        }

        /// <summary>
        /// Starts viewing of specified packed file
        /// </summary>
        /// <param name="packedFilePath">Path of packed file to open</param>
        private void _ViewPackedFile(string packedFilePath)
        {
            if (_CurrentBnkFile != null && !string.IsNullOrEmpty(packedFilePath))
            {
                // Preparing
                string viewFileName = EditHelper.Instance.PrepareFile(_CurrentBnkFile, packedFilePath);

                // Viewing file
                FileHandler viewFile = FileHandler.GetHandler(viewFileName);

                viewFile.Edit();
            }
        }

        /// <summary>
        /// Starts editing of specified packed file
        /// </summary>
        /// <param name="packedFilePath">Path of packed file to edit</param>
        private void _EditPackedFile(string packedFilePath)
        {
            if (_CurrentBnkFile != null && !string.IsNullOrEmpty(packedFilePath))
            {
                // A task must be created
                EditHelper.Task currentTask;

                try
                {
                    currentTask = EditHelper.Instance.AddTask(_CurrentBnkFile, packedFilePath, false);

                    if (!currentTask.isValid)
                        throw new Exception();
                }
                catch (Exception ex)
                {
                    switch (ex.Message)
                    {
                        case EditHelper.ERROR_CODE_TASK_EXISTS:
                            MessageBoxes.ShowWarning(this, _WARNING_TASK_EXISTS);
                            return;
                        case EditHelper.ERROR_CODE_TEMP_FOLDER:
                            throw new Exception(_ERROR_CREATE_TEMP_FOLDER, ex);
                        default:
                            throw;
                    }
                }

                // Task created, now editing file
                FileHandler editFile = FileHandler.GetHandler(currentTask.extractedFile);

                editFile.Edit();
            }
        }

        /// <summary>
        /// Lance l'extraction du fichier empaqueté spécifié
        /// </summary>
        /// <param name="packedFilePath">Path of packed file to extract</param>
        /// <param name="targetFolder">Folder where packed file will be extracted</param>
        private void _ExtractPackedFile(string packedFilePath, string targetFolder)
        {
            // Extraction du fichier empaqueté dans le dossier cible
            string packedFileName = _CurrentBnkFile.GetPackedFileName(packedFilePath);
            string workFileName = string.Format(@"{0}\{1}",
                targetFolder,
                packedFileName);

            _CurrentBnkFile.ExtractPackedFile(packedFilePath, workFileName, false);
        }

        /// <summary>
        /// Starts renaming of specified packed file
        /// </summary>
        /// <param name="packedFilePath"></param>
        /// <param name="newFileName"></param>
        private bool _RenamePackedFile(string packedFilePath, string newFileName)
        {
            bool result = false;

            try
            {
                Cursor = Cursors.WaitCursor;

                _CurrentBnkFile.RenamePackedFile(packedFilePath, newFileName);

                SetBnkContentsChanged();
                StatusBarLogManager.ShowEvent(this, _STATUS_RENAME_SUCCESS);
                result = true;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            return result;
        }

        /// <summary>
        /// Starts replacing of selected packed files
        /// </summary>
        /// <param name="fileNames">(out parameter) list of new file names</param>
        /// <returns></returns>
        private int _ReplacePackedFiles(out Collection<string> fileNames)
        {
            int modifiedCount = 0;

            fileNames = new Collection<string>();

            foreach (string packedFilePath in _GetSelectedFilePaths())
            {
                // Sélection du fichier de remplacement
                string packedFileName = _CurrentBnkFile.GetPackedFileName(packedFilePath);
                string newPackedFile = _SelectReplacementFile(packedFileName);

                if (!string.IsNullOrEmpty(packedFileName) && newPackedFile != null)
                {
                    Cursor = Cursors.WaitCursor;

                    _CurrentBnkFile.ReplacePackedFile(packedFilePath, newPackedFile);
                    modifiedCount++;

                    Cursor = Cursors.Default;
                }

                fileNames.Add(newPackedFile);
            }

            return modifiedCount;
        }

        /// <summary>
        /// Effectue le remplacement direct d'un fichier empaqueté par le fichier spécifié
        /// </summary>
        /// <param name="selectedFile">Chemin du fichier de remplacement</param>
        /// <returns>true si le remplacement a réussi, false sinon</returns>
        private bool _DirectReplace(string selectedFile)
        {
            bool returnedResult = false;
            Exception ex = null;

            // EVO_17
            // Recherche d'un fichier de même nom
            if (_CurrentBnkFile != null && !string.IsNullOrEmpty(selectedFile))
            {
                FileInfo fi = new FileInfo(selectedFile);
                Collection<string> possiblePaths = _CurrentBnkFile.GetPackedFilesPaths(fi.Name);

                if (possiblePaths.Count == 1)
                {
                    // Remplacement
                    _CurrentBnkFile.ReplacePackedFile(possiblePaths[0], selectedFile);
                    returnedResult = true;
                }
                else if (possiblePaths.Count == 0)
                    ex = new Exception("Unable to replace file: " + fi.Name + " - a packed file with same name does not exist.");
                else 
                    ex = new Exception("Unable to replace file: " + fi.Name + " - more than one packed file exist with same name.");
            }

            if (ex != null)
                Exception2.PrintStackTrace(ex);

            return returnedResult;
        }
        #endregion
    }
}
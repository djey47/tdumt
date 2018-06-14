using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.MVC;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Types;
using DjeFramework1.Common.Types.Forms;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.support.editing;
using TDUModdingTools.common;
using TDUModdingTools.common.handlers;
using TDUModdingTools.gui.filebrowser.properties;

namespace TDUModdingTools.gui.filebrowser.editing
{
    /// <summary>
    /// Fenêtre de suivi des tâches d'édition
    /// </summary>
    public partial class EditTasksForm : Form, IChangesListener
    {
        #region Constantes
        /// <summary>
        /// Column index: type description ('?' label)
        /// </summary>
        private const int _COLUMN_INDEX_WORKING_FILE = 5;

        /// <summary>
        /// Question posée avant la suppression des tâches cochées
        /// </summary>
        private const string _QUESTION_DISCARD_CHANGES = "{0} task(s) will be lost.\r\nAre you sure ?";

        /// <summary>
        /// Message d'erreur si tentative d'annulation avec aucune tâche de cochée
        /// </summary>
        private const string _ERROR_DISCARD_CHECK = "Please check a task to delete.";

        /// <summary>
        /// Message d'erreur si tentative d'annulation avec aucune tâche de cochée
        /// </summary>
        private const string _ERROR_APPLY_CHECK = "Please check a task to apply.";

        /// <summary>
        /// Message de succès de l'opération
        /// </summary>
        private const string _INFO_APPLY_COMPLETE = "{0} edit change(s) successfully applied. BNK file saved.";
        #endregion

        #region Members
        /// <summary>
        /// Delegate for safe threading support (form operations from EditHelper)
        /// </summary>
        delegate void RefreshEditListCallBack();
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public EditTasksForm()
        {
            InitializeComponent();
        }

        #region Events
        private void EditTasksForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Signal de fermeture > mise à jour de l'état dans le navigateur
            FileBrowserForm.Instance.SetEditWindowVisible(false);

            // On ne fait que masquer la fenêtre, pas de suppression
            e.Cancel = true;
            Hide();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            // Clic on "open file" button
            // Relance l'édition du fichier avec l'éditeur par défaut
            if (editTasksListView.SelectedItems.Count == 0)
                return;

            try
            {
                string currentFile = editTasksListView.SelectedItems[0].SubItems[_COLUMN_INDEX_WORKING_FILE].Text;

                // On utilise l'action par défaut
                FileHandler file = FileHandler.GetHandler(currentFile);

                file.Edit();
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void folderButton_Click(object sender, EventArgs e)
        {
            // Click on "open location" button
            // Ouverture de l'explorateur vers le dossier spécifié
            if (editTasksListView.SelectedItems.Count == 0)
                return;

            try
            {
                string currentFile = editTasksListView.SelectedItems[0].SubItems[_COLUMN_INDEX_WORKING_FILE].Text;
                FileInfo fi = new FileInfo(currentFile);

                ProcessStartInfo explorerProcess = new ProcessStartInfo(fi.DirectoryName);
                Process.Start(explorerProcess);
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void discardButton_Click(object sender, EventArgs e)
        {
            // Click on "Discard" button
            // Supprime les tâches d'édition sélectionnées
            long checkedCount = ListView2.CheckedCount(editTasksListView);

            if (checkedCount == 0)
            {
                MessageBoxes.ShowWarning(this, _ERROR_DISCARD_CHECK);
                return;
            }

            string question = string.Format(_QUESTION_DISCARD_CHANGES, checkedCount);
            DialogResult dr = MessageBoxes.ShowQuestion(this, question, MessageBoxButtons.YesNo);
            
            if (dr != DialogResult.Yes)
                return;

            try
            {
                Cursor = Cursors.WaitCursor;

                // Parcours de la liste de tâches à l'écran
                foreach (ListViewItem anotherItem in editTasksListView.Items)
                {
                    if (anotherItem.Checked)
                    {
                        EditHelper.Task currentTask = (EditHelper.Task) anotherItem.Tag;

                        EditHelper.Instance.RemoveTask(currentTask);
                    }
                }

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            // Click on "Apply" button
            // Applique les modifications sur les tâches sélectionnées
            long checkedCount = ListView2.CheckedCount(editTasksListView);

            if (checkedCount == 0)
            {
                MessageBoxes.ShowWarning(this, _ERROR_APPLY_CHECK);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                // Parcours de la liste de tâches à l'écran
                foreach (ListViewItem anotherItem in editTasksListView.Items)
                {
                    if (anotherItem.Checked)
                    {
                        EditHelper.Task currentTask = (EditHelper.Task) anotherItem.Tag;

                        // EVO_74
                        if (currentTask.trackedFileHasChanged)
                        {
                            // Application des modifs si nécessaire
                            string newFileName = currentTask.extractedFile;
                            FileHandler editedFile = FileHandler.GetHandler(newFileName);

                            editedFile.Apply();

                            // EVO_71: externalization
                            EditHelper.Instance.ApplyChanges(currentTask);

                            // ANO_33: mise à jour de la liste
                            if (FileBrowserForm.Instance.CurrentBnkFile != null)
                            {
                                if (
                                    currentTask.parentBNK.FileName.Equals(
                                        FileBrowserForm.Instance.CurrentBnkFile.FileName))
                                    FileBrowserForm.Instance.SetBnkContentsChanged();
                            }

                            // EVO_47: On conserve la tâche pour cumuler les modifs sans avoir à la recréer.
                        }
                        else
                            checkedCount--;
                    }
                }

                // Mise à jour de la liste
                NotifyModelChanged();

                Cursor = Cursors.Default;

                if (checkedCount > 0)
                {
                    // EVO 32: Message de fin d'opération
                    string message = string.Format(_INFO_APPLY_COMPLETE, checkedCount);

                    StatusBarLogManager.ShowEvent(FileBrowserForm.Instance, message);
                }
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void editTasksListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Double click on an item > open file again
            editButton_Click(this, new EventArgs());
        }

        private void EditTasksForm_VisibleChanged(object sender, EventArgs e)
        {
            // Form has been hidden or showed
            if (Visible)
                // Ensures object is in listener list
                EditHelper.Instance.AttachToObserver(this);
            else
                // Detaches object from observer
                EditHelper.Instance.DetachFromObserver(this);
        }

        private void propertiesButton_Click(object sender, EventArgs e)
        {
            // Click on 'Properties...' button
            if (editTasksListView.SelectedItems.Count != 0)
            {
                try
                {
                    string currentFile = editTasksListView.SelectedItems[0].SubItems[_COLUMN_INDEX_WORKING_FILE].Text;
                    PropertiesDialog dialog = new PropertiesDialog(currentFile);

                    dialog.Show(this);
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }
        #endregion

        #region Implémentation IChangesListener
        public void NotifyModelChanged()
        {
            _RefreshEditList();
        }

        public void NotifyModelChanged(string changeDescription, params object[] parameters)
        {}
        #endregion

        #region Méthodes privées
        /// <summary>
        /// Met à jour la liste d'éditions
        /// </summary>
        private void _RefreshEditList()
        {
            // Threading support
            // @see http://msdn.microsoft.com/fr-fr/library/ms171728.aspx
            if (editTasksListView.InvokeRequired)
            {
                RefreshEditListCallBack refreshCallBack = _RefreshEditList;
                
                Invoke(refreshCallBack);
                return;
            }

            Collection<EditHelper.Task> taskList = EditHelper.Instance.Tasks;

            // Effacement de la liste
            editTasksListView.Items.Clear();

            Cursor = Cursors.AppStarting;

            try
            {
                // Parcours de la liste de tâches
                foreach (EditHelper.Task anotherTask in taskList)
                {
                    // New in 1.10: task must not be furtive to be displayed
                    if (!anotherTask.isFurtive)
                    {
                        // EVO_74: column order and title are changed
                        string fileName = anotherTask.parentBNK.GetPackedFileName(anotherTask.editedPackedFilePath);
                        ListViewItem li = new ListViewItem(fileName);
                        // Type description
                        li.SubItems.Add(
                            TduFile.GetTypeDescription(fileName));
                        // Date
                        li.SubItems.Add(anotherTask.startDate.ToString());
                        // Last modified
                        li.SubItems.Add(anotherTask.trackedLastFileWriteTime.ToString());
                        // BNK
                        li.SubItems.Add(anotherTask.parentBNK.FileName);
                        // Fichier de travail
                        li.SubItems.Add(anotherTask.extractedFile);
                        // Clé (pour conserver la référence)
                        li.Tag = anotherTask;
                        // Tooltip
                        li.ToolTipText = anotherTask.editedPackedFilePath;

                        // EVO_74: color for modified files
                        if (anotherTask.trackedFileHasChanged)
                            li.ForeColor = GuiConstants.COLOR_MODIFIED_ITEM;

                        editTasksListView.Items.Add(li);
                    }
                }

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }
        #endregion
    }
}
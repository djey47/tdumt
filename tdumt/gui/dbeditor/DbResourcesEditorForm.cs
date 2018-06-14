using System;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Dialogs.Search;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Types.Forms;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.database.util;
using TDUModdingLibrary.support.constants;
using TDUModdingLibrary.support.editing;
using TDUModdingTools.common;
using TDUModdingTools.gui.settings;

namespace TDUModdingTools.gui.dbeditor
{
    /// <summary>
    /// Fenêtre d'édition de la base de données
    /// </summary>
    public partial class DbResourcesEditorForm : Form
    {
        #region Constants
        /// <summary>
        /// Libellé d'information lorsque la recherche n'a pas été initialisée
        /// </summary>
        private const string _ENTRY_COUNT_START_TEXT = "Please load a topic.";

        /// <summary>
        /// Libellé d'information lorsque le fichier MAP a été chargé
        /// </summary>
        private const string _ENTRY_COUNT_TEXT = "Topic loaded with {0} entries (language: {1} ).";

        /// <summary>
        /// Text of last entry (display only)
        /// </summary>
        private const string _ENTRY_END = "-END-";

        /// <summary>
        /// Error message when editing database file
        /// </summary>
        private const string _ERROR_EDITING = "An error occured when loading selected topic.";

        /// <summary>
        /// Error message when trying to edit a database topic, already edited in other DB Editor
        /// </summary>
        private const string _ERROR_EDITED_TWICE = "Selected topic is currently edited in another editor.\r\nPlease terminate that task first or select another topic.";

        /// <summary>
        /// Error message when trying to add an id which already exists in database resource
        /// </summary>
        private const string _ERROR_ID_EXISTS = "This identifier already exists.\r\nPlease use a unique id.";

        /// <summary>
        /// Question when about to lose changes
        /// </summary>
        private const string _QUESTION_CONTINUE = "Unsaved changes will be lost!\r\nContinue?";
        
        /// <summary>
        /// Libellé d'information pour la boîte de recherche
        /// </summary>
        private const string _LABEL_SEARCH = "Find an entry...";

        /// <summary>
        /// Label for entry number
        /// </summary>
        private const string _LABEL_ENTRY_NUMBER = "#";

        /// <summary>
        /// Message apparaissant dans la barre d'état lorsque l'opération a réussi (file mode)
        /// </summary>
        private const string _STATUS_MOD_SUCCESS_1 = "Entry successfully modified. DB file saved.";

        /// <summary>
        /// Status message when operation succeeded (topic mode)
        /// </summary>
        private const string _STATUS_MOD_SUCCESS_2 = "Entry successfully modified.";

        /// <summary>
        /// Message appearing in status bar when insert operation succeeded (file mode)
        /// </summary>
        private const string _STATUS_INS_SUCCESS_1 = "Entry successfully inserted. DB file saved.";

        /// <summary>
        /// Message appearing in status bar when insert operation succeeded (topic mode)
        /// </summary>
        private const string _STATUS_INS_SUCCESS_2 = "Entry successfully inserted.";

        /// <summary>
        /// Status message when save operation succeeded
        /// </summary>
        private const string _STATUS_DB_UPDATE_SUCCESS = "DB file saved.";
        
        /// <summary>
        /// Message apparaissant dans la barre d'état lorsque l'opération de modification commence
        /// </summary>
        private const string _STATUS_EDITING = "Editing entry...";

        /// <summary>
        /// Message in status bar when insert operation begins
        /// </summary>
        private const string _STATUS_INSERTING = "Creating entry...";

        /// <summary>
        /// Status message when delete operation succeeded
        /// </summary>
        private const string _STATUS_DEL_SUCCESS = "Entry sucessfully deleted.";
        #endregion

        #region Attributs
        /// <summary>
        /// Le fichier MAP rattaché
        /// </summary>
        private DBResource _LeDB;

        /// <summary>
        /// Donnée en cours d'édition
        /// </summary>
        private DBResource.Entry _EditedEntry;

        /// <summary>
        /// EVO_91: in direct access mode, currently edit file is stored here
        /// </summary>
        private string _EditedFile;

        /// <summary>
        /// Instance of search box
        /// </summary>
        private static SearchBox _SearchBoxInstance;
        #endregion

        /// <summary>
        /// Constructeur paramétré. Le fichier spécifié est immédiatement chargé
        /// </summary>
        /// <param name="fileToEdit">Nom du fichier de base de données à ouvrir</param>
        public DbResourcesEditorForm(string fileToEdit)
        {
            InitializeComponent();

            entryCountLabel.Text = _ENTRY_COUNT_START_TEXT;

            // EVO 32 : support StatusLog
            StatusBarLogManager.AddNewLog(this, toolStripStatusLabel);

            // Initialisation de la liste de sections dans la BDD courante
            string[] allTopics = Enum.GetNames(typeof(DB.Topic));

            foreach (string anotherTopic in allTopics)
            {
                if (!DB.Topic.None.ToString().Equals(anotherTopic))
                    catComboBox.Items.Add(anotherTopic);
            }

            catComboBox.SelectedIndex = 0;

            if (fileToEdit != null)
            {
                // Fichier DB paramétré
                inputFilePath.Text = fileToEdit;
                openRadioButton.Checked = true;
                _SetSelectionControls(true);

                // Ouverture automatique
                refreshButton_Click(this, new EventArgs());
            }
            else
                _SetSelectionControls(false);
        }

        #region Méthodes privées
        /// <summary>
        /// Prépare le chargement d'une catégorie depuis la base de données courante
        /// </summary>
        /// <param name="category">Type de données recherché</param>
        /// <param name="culture">Code pays</param>
        private void _LoadFromCurrentDatabase(DB.Topic category, DB.Culture culture)
        {
            // Localisation du fichier BNK
            string bnkPath = Program.ApplicationSettings.TduMainFolder +  LibraryConstants.FOLDER_DB + DB.GetBNKFileName(culture);
            BNK bnk = TduFile.GetFile(bnkPath) as BNK;

            if (bnk != null && bnk.Exists)
            {
                string fileName = DB.GetFileName(culture, category);
                string filePath = bnk.GetPackedFilesPaths(fileName)[0];

                // EVO_91: using edit support from ModdingLibrary
                EditHelper.Task currentTask = EditHelper.Instance.AddTask(bnk, filePath, true);

                _EditedFile = currentTask.extractedFile;
            }
        }

        /// <summary>
        /// EVO_91: in topic mode, writes changes to BNK file back
        /// </summary>
        private void _SaveToCurrentDatabase()
        {
            EditHelper.Task currentTask = EditHelper.Instance.GetTask(_EditedFile);

            if (currentTask.isValid)
                EditHelper.Instance.ApplyChanges(currentTask);
        }

        /// <summary>
        /// Met à jour la liste des entrées
        /// </summary>
        /// <param name="keepSelection">true pour replacer le sélecteur à l'emplacement d'origine, false sinon</param>
        private void _UpdateEntryList(bool keepSelection)
        {
            // Sauvegarde de l'indice sélectionné
            if (keepSelection)
                ListView2.StoreSelectedIndex(entryList);

            // Vidage de liste
            entryList.Items.Clear();
            entryCountLabel.Text = _ENTRY_COUNT_START_TEXT;

            // Lecture de la structure
            foreach (DBResource.Entry anotherEntry in _LeDB.EntryList)
            {
                if (anotherEntry.isValid)
                {
                    ListViewItem anotherItem = new ListViewItem(string.Format("{0}", anotherEntry.index));

                    // Identifiant uniquement pour les données
                    if (anotherEntry.isComment)
                    {
                        anotherItem.SubItems.Add(string.Format("{0}", ""));

                        // Ligne en blanc sur fond vert
                        anotherItem.BackColor = GuiConstants.COLOR_BACK_COMMENT_ITEM;
                        anotherItem.ForeColor = GuiConstants.COLOR_FRONT_COMMENT_ITEM;
                    }
                    else
                        anotherItem.SubItems.Add(string.Format("{0}", anotherEntry.id == null ? "" : anotherEntry.id.Id));

                    anotherItem.SubItems.Add(string.Format("{0}", anotherEntry.value));

                    entryList.Items.Add(anotherItem);
                }
            }

            // End of list
            ListViewItem endItem = new ListViewItem(_ENTRY_END)
                                       {
                                           BackColor = GuiConstants.COLOR_BACK_COMMENT_ITEM,
                                           ForeColor = GuiConstants.COLOR_FRONT_COMMENT_ITEM
                                       };

            entryList.Items.Add(endItem);

            // Nombre d'entrées
            entryCountLabel.Text = string.Format(_ENTRY_COUNT_TEXT, _LeDB.EntryList.Count, _LeDB.CurrentCulture);

            // Restauration de la sélection
            if (keepSelection)
                ListView2.RestoreSelectedIndex(entryList);
        }

        /// <summary>
        /// Met à jour l'entrée à la ligne indiquée
        /// </summary>
        /// <param name="entry">Entrée à mettre à jour</param>
        private void _UpdateEntryFromLine(DBResource.Entry entry)
        {
            if ( !entry.isValid)
                return;

            try
            {
                // Recherche de l'item
                foreach (ListViewItem anotherItem in entryList.Items)
                {
                    int currentLine = int.Parse(anotherItem.Text);

                    if (currentLine == entry.index)
                    {
                        if (!_EditedEntry.isComment)
                            anotherItem.SubItems[1].Text = entry.id.Id;

                        anotherItem.SubItems[2].Text = entry.value;

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }
        
        /// <summary>
        /// Defines appearance of selection controls according to current mode
        /// </summary>
        /// <param name="isByFile">true to open a DB file, false to explore DB per category</param>
        private void _SetSelectionControls(bool isByFile)
        {
            inputFilePath.Enabled = isByFile;
            browseDBButton.Enabled = isByFile;
            catComboBox.Enabled = !isByFile;
            saveButton.Enabled = !isByFile;
        }

        /// <summary>
        /// If database files are modified, asks the user if he's willing to continue without applying changes
        /// </summary>
        /// <returns>true if user has accepted to continue or nothing has changed, else false</returns>
        private bool _ManagePendingChanges()
        {
            bool result = true;
            EditHelper.Task currentTask = EditHelper.Instance.GetTask(_EditedFile);

            if (currentTask.isValid && currentTask.trackedFileHasChanged)
            {
                DialogResult dr = MessageBoxes.ShowQuestion(this, _QUESTION_CONTINUE, MessageBoxButtons.YesNo);

                if (dr != DialogResult.Yes)
                    result = false;
            }

            return result;
        }

        /// <summary>
        /// Returns instance of Search Box
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static SearchBox _GetSearchBoxInstance(ListView listView, string message)
        {
            if (_SearchBoxInstance == null)
                _SearchBoxInstance = new ListViewSearchBox(listView, message);

            return _SearchBoxInstance;
        }
        #endregion

        #region Events
        private void DBEditorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _SearchBoxInstance = null;

            // Nettoyage log
            StatusBarLogManager.RemoveLog(this);

            // Task cleaning
            EditHelper.Task currentTask = EditHelper.Instance.GetTask(_EditedFile);

            EditHelper.Instance.RemoveTask(currentTask);
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            // Ouvre les options
            SettingsForm settingsForm = new SettingsForm(SettingsForm.SettingsTabPage.Main);

            settingsForm.ShowDialog(this);
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            // Contrôle de la saisie ...
            if (openRadioButton.Checked && inputFilePath.Text.Equals(""))
                return;

            // Pending changes ?
            if (_ManagePendingChanges())
            {
                // Récupération des infos
                try
                {
                    string dbFileName;
                    EditHelper.Task currentTask = EditHelper.Instance.GetTask(_EditedFile);

                    Cursor = Cursors.WaitCursor;

                    // Task update
                    if (currentTask.isValid)
                    {
                        EditHelper.Instance.RemoveTask(currentTask);
                        _EditedFile = null;
                    }

                    // Selon le type
                    if (openRadioButton.Checked)
                        // Par nom de fichier
                        dbFileName = inputFilePath.Text;
                    else
                    {
                        // Par catégorie
                        string category = catComboBox.SelectedItem.ToString();
                        DB.Topic topic = (DB.Topic)Enum.Parse(typeof(DB.Topic), category);

                        _LoadFromCurrentDatabase(topic, Program.ApplicationSettings.GetCurrentCulture());
                        dbFileName = _EditedFile;
                    }

                    _LeDB = TduFile.GetFile(dbFileName) as DBResource;

                    if (_LeDB != null)
                        // Mise à jour des entrées
                        _UpdateEntryList(false);

                    // Réinitialisation des champs d'édition
                    lineLabel.Text = _LABEL_ENTRY_NUMBER;
                    idTextBox.Text = valueTextBox.Text = "";

                    // Mise à jour statut
                    toolStripStatusLabel.Text = "";

                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    // EVO_91: Manage EditHelper's error codes
                    switch (ex.Message)
                    {
                        case EditHelper.ERROR_CODE_TASK_EXISTS:
                            ex = new Exception(_ERROR_EDITED_TWICE, ex);
                            break;
                        case EditHelper.ERROR_CODE_EXTRACT_FAILED:
                        case EditHelper.ERROR_CODE_INVALID_PACKED_FILE:
                        case EditHelper.ERROR_CODE_TEMP_FOLDER:
                            ex = new Exception( _ERROR_EDITING,ex);
                            break;
                    }

                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void browseDBButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = GuiConstants.FILTER_DB_ALL_FILES;

            DialogResult dr = openFileDialog1.ShowDialog(this);
            
            if (dr == DialogResult.OK)
            {
                inputFilePath.Text = openFileDialog1.FileName;
                openRadioButton.Checked = true;
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (_LeDB != null && _LeDB.EntryList != null)
            {
                try
                {
                    // On utilise la boite de dialogue de recherche
                    SearchBox searchBox = _GetSearchBoxInstance(entryList, _LABEL_SEARCH);

                    searchBox.Show(this);
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void modifyButton_Click(object sender, EventArgs e)
        {
            // Clic sur le bouton 'Modify'
            if (_LeDB == null 
                || entryList.SelectedItems.Count != 1 
                || entryList.SelectedIndices[0] == entryList.Items.Count - 1)
                return;

            // EVO 48 : mise à jour de la barre de statut
            StatusBarLogManager.ShowEvent(this, _STATUS_EDITING);

            // Récupération de la donnée à éditer
            int selectedLine = int.Parse(entryList.SelectedItems[0].Text);
            DBResource.Entry data = _LeDB.GetEntryFromLine(selectedLine);

            if (data.isValid)
            {
                _EditedEntry = data;

                // Mise à jour des champs d'édition
                lineLabel.Text = data.index.ToString();
                valueTextBox.Text = data.value;

                if (data.isComment)
                {
                    idTextBox.Text = "";
                    valueTextBox.Focus();
                }
                else
                {
                    idTextBox.Text = data.id.Id;
                    idTextBox.Focus();
                }
            }
        }

        private void entryList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Double click on an entry > Change values
            modifyButton_Click(sender, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
        }

        private void editOKButton_Click(object sender, EventArgs e)
        {
            // Clic sur le bouton 'OK'
            if (!_EditedEntry.isValid)
                return;

            // EVO_xx : line adding feature
            bool isAddingEntry = (_EditedEntry.id == null);

            // Prépare les données pour la mise à jour
            string newId = idTextBox.Text;
            string value = valueTextBox.Text;

            try
            {
                Cursor = Cursors.WaitCursor;

                // According to mode
                if (isAddingEntry)
                {
                    // Add
                    // Is this identifier a UID ?
                    if (_LeDB.GetEntryFromId(newId).isValid)
                    {
                        idTextBox.Focus();
                        throw new Exception(_ERROR_ID_EXISTS);
                    }

                    // Identifier update
                    if (!_EditedEntry.isComment)
                        _EditedEntry.id = new ResourceIdentifier(newId, _LeDB.CurrentTopic);

                    // Value update
                    _EditedEntry.value = value;

                    // Update file data
                    _LeDB.InsertEntry(_EditedEntry);
                    _LeDB.Save();

                    // List update
                    _UpdateEntryList(true);

                    // Signals
                    if (_EditedFile == null)
                        StatusBarLogManager.ShowEvent(this, _STATUS_INS_SUCCESS_1);
                    else
                        StatusBarLogManager.ShowEvent(this, _STATUS_INS_SUCCESS_2);
                }
                else
                {
                    // Modify
                    // Is update necessary ?
                    if (!newId.Equals(_EditedEntry.id.Id)
                        || !value.Equals(_EditedEntry.value))
                    {
                        // Identifier update
                        if (!_EditedEntry.isComment)
                            _EditedEntry.id = new ResourceIdentifier(newId, _LeDB.CurrentTopic);

                        // Value update
                        _EditedEntry.value = value;

                        // Met à jour les données du fichier
                        DBResource.Entry entryAfterUpdate = _LeDB.UpdateEntry(_EditedEntry);
                        _LeDB.Save();

                        // Met à jour la ligne dans le tableau
                        _UpdateEntryFromLine(entryAfterUpdate);

                        // Signals
                        if (_EditedFile == null)
                            StatusBarLogManager.ShowEvent(this, _STATUS_MOD_SUCCESS_1);
                        else
                            StatusBarLogManager.ShowEvent(this, _STATUS_MOD_SUCCESS_2);
                    }
                    else
                        StatusBarLogManager.ShowEvent(this, "");
                }

                // Clearing fields
                lineLabel.Text = "";
                idTextBox.Clear();
                valueTextBox.Clear();

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void catRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Click on mode 1
            _SetSelectionControls(false);
            catComboBox.Focus();
        }

        private void openRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Click on mode 2
            _SetSelectionControls(true);
            inputFilePath.Focus();
        }

        private void DBEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Closing window : changes...
            e.Cancel = !_ManagePendingChanges();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // Clic on 'Save changes' button
            EditHelper.Task currentTask = EditHelper.Instance.GetTask(_EditedFile);

            if (currentTask.trackedFileHasChanged)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    _SaveToCurrentDatabase();

                    // Signals
                    StatusBarLogManager.ShowEvent(this, _STATUS_DB_UPDATE_SUCCESS);

                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void insertToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on 'Insert' button
            if (_LeDB == null
                || entryList.SelectedItems.Count != 1)
                return;

            try
            {
                // EVO_48 : status bar update
                StatusBarLogManager.ShowEvent(this, _STATUS_INSERTING);

                // Getting current entry index
                int selectedLine;

                if (entryList.SelectedItems[0].Text.Equals(_ENTRY_END))
                    selectedLine = entryList.Items.Count;
                else
                    selectedLine = int.Parse(entryList.SelectedItems[0].Text);
                
                DBResource.Entry newData = new DBResource.Entry {isValid = true, index = selectedLine};

                _EditedEntry = newData;

                // Updating fields
                lineLabel.Text = selectedLine.ToString();
                valueTextBox.Clear();
                idTextBox.Clear();
                idTextBox.Focus();
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void deleteToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on 'Delete' button
            if (_LeDB == null
                || entryList.SelectedItems.Count != 1
                || entryList.SelectedIndices[0] == entryList.Items.Count - 1)
                return;

            try 
            {
                Cursor = Cursors.WaitCursor;

                // Getting current identifier and corresponding entry
                string id = entryList.SelectedItems[0].SubItems[1].Text;
                DBResource.Entry currentEntry = _LeDB.GetEntryFromId(id);

                // Removing item
                _LeDB.DeleteEntry(currentEntry);
                _LeDB.Save();

                // List update
                _UpdateEntryList(true);
                
                StatusBarLogManager.ShowEvent(this, _STATUS_DEL_SUCCESS);
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void entryList_KeyDown(object sender, KeyEventArgs e)
        {
            // Intercepts key strokes
            if (e.KeyCode == Keys.Delete)
                // Del : delete entry
                deleteToolStripButton_Click(this, e);
            else if (e.KeyCode == Keys.Insert)
                // Ins : add entry
                insertToolStripButton_Click(this, e);
        }
        #endregion
    }
}
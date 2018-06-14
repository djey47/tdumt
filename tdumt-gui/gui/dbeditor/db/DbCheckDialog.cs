using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Common.Types.Forms;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.support.editing;

namespace TDUModdingTools.gui.dbeditor.db
{
    public partial class DbCheckDialog : Form
    {
        #region Constants
        /// <summary>
        /// Label in top of folder browsing dialog box
        /// </summary>
        private const string _LABEL_BROWSE_FOLDER = "Please select parent folder of database files BACKUP set.";

        /// <summary>
        /// Error message when trying to fix without backup path
        /// </summary>
        private const string _ERROR_NO_BACKUP_PATH = "Please specifiy path of a clean database backup.";

        /// <summary>
        /// Format string for checking result
        /// </summary>
        private const string _FORMAT_CHECK_RESULT = "Database was checked. {0} issue(s) were found!";

        /// <summary>
        /// Format string for fixing result
        /// </summary>
        private const string _FORMAT_FIX_RESULT = "Fixing complete. {0} on {1} issue(s) fixed!";

        /// <summary>
        /// Status message when checking is in progress
        /// </summary>
        private const string _STATUS_CHECKING = "Checking database, please wait...";

        /// <summary>
        /// Status message when fixing is in progress
        /// </summary>
        private const string _STATUS_FIXING = "Fixing selected issues, please wait...";

        /// <summary>
        /// Default status message
        /// </summary>
        private const string _STATUS_READY = "Ready.";

        /// <summary>
        /// Status message when database is in perfect state
        /// </summary>
        private const string _STATUS_CHECK_PERFECT = "Wow, it seems your database is in perfect condition. Well done!";
        #endregion

        #region Private fields
        /// <summary>
        /// Folder path for database files
        /// </summary>
        private readonly string _DatabaseFolder;

        /// <summary>
        /// Loaded data
        /// </summary>
        private Dictionary<DB.Topic, TduFile[]> _LoadedData;

        /// <summary>
        /// Edit tasks for loaded data
        /// </summary>
        private Dictionary<DB.Topic, EditHelper.Task[]> _LoadedDataTasks;

        /// <summary>
        /// Index of list view groups
        /// </summary>
        private readonly Dictionary<string, ListViewGroup> _GroupIndex = new Dictionary<string, ListViewGroup>();
        #endregion

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="dbFolder"></param>
        public DbCheckDialog(string dbFolder)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(dbFolder))
            {
                _DatabaseFolder = dbFolder;
                _InitializeContents();
            }
        }

        #region Private methods
        /// <summary>
        /// Defines box contents
        /// </summary>
        private void _InitializeContents()
        {
            // Logging
            StatusBarLogManager.AddNewLog(this, toolStripStatusLabel);

            // ListView groups
            foreach (DB.Topic anotherTopic in Enum.GetValues(typeof(DB.Topic)))
            {
                if (anotherTopic != DB.Topic.None)
                    _AddGroup(anotherTopic.ToString());
            }
        }

        /// <summary>
        /// Adds specified group to ListView groups and updates group index
        /// </summary>
        /// <param name="groupName"></param>
        private void _AddGroup(string groupName)
        {
            if (!string.IsNullOrEmpty(groupName))
            {
                if (!_GroupIndex.ContainsKey(groupName))
                {
                    ListViewGroup newGroup = new ListViewGroup(groupName);

                    problemsListView.Groups.Add(newGroup);
                    _GroupIndex.Add(groupName, newGroup);
                }
            }
        }

        /// <summary>
        /// Checks specified topic in loaded data then returns eventual issues
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        private IEnumerable<DatabaseFixer.Corruption> _CheckTopic(DB.Topic topic)
        {
            Collection<DatabaseFixer.Corruption> returnedIssues = new Collection<DatabaseFixer.Corruption>();

            if (topic != DB.Topic.None)
            {
                // Getting data and resource
                DB dbTopic = _LoadedData[topic][0] as DB;
                DBResource dbResource = _LoadedData[topic][1] as DBResource;

                if (dbTopic == null || dbResource == null)
                    throw new Exception("Invalid topic can't be loaded: " + topic);

                // Values
                foreach (DB.Entry anotherEntry in dbTopic.Entries)
                {
                    // Structure checks
                    _CheckStructure(dbTopic, anotherEntry, returnedIssues);

                    foreach (DB.Cell anotherCell in anotherEntry.cells)
                    {
                        // According to data type...
                        switch (anotherCell.valueType)
                        {
                            case DB.ValueType.PrimaryKey:
                                // Check disabled
                                break;
                            case DB.ValueType.Reference:
                                _CheckReferenceValue(topic, anotherCell, returnedIssues);
                                break;
                            case DB.ValueType.ReferenceL:
                                _CheckResourceValue(topic, anotherCell, null, returnedIssues);
                                break;
                            case DB.ValueType.ValueInResourceH:
                                _CheckResourceValue(topic, anotherCell, dbResource, returnedIssues);
                                break;
                            case DB.ValueType.ValueInResource:
                                _CheckResourceValue(topic, anotherCell, dbResource, returnedIssues);
                                break;
                            case DB.ValueType.BitField:
                                // Check disabled
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            return returnedIssues;
        }

        /// <summary>
        /// Checks for bugs in entry structure
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="entry"></param>
        /// <param name="issues"></param>
        private static void _CheckStructure(DB topic, DB.Entry entry, Collection<DatabaseFixer.Corruption> issues)
        {
            if (topic != null)
            {
                // Cell count
                if (topic.Structure.Count != entry.cells.Count)
                {
                    DatabaseFixer.Corruption newCorruption = new DatabaseFixer.Corruption
                    {
                        culture = DB.Culture.Global,
                        entryId = entry.index,
                        kind =
                            DatabaseFixer.CorruptionKind.StructureMissingColumns,
                        referencedTopic = DB.Topic.None,
                        topic = topic.CurrentTopic,
                        comment = "Missing columns: " + (topic.Structure.Count - entry.cells.Count)
                    };

                    issues.Add(newCorruption);
                }
            }
        }

        /// <summary>
        /// Checks for specified reference and updates issues list
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="cell"></param>
        /// <param name="issues"></param>
        private void _CheckReferenceValue(DB.Topic topic, DB.Cell cell, Collection<DatabaseFixer.Corruption> issues)
        {
            if (cell.optionalRef != null && issues != null)
            {
                string topicId = cell.optionalRef;
                DB.Topic referencedTopic = DB.TopicPerTopicId[topicId];

                if (referencedTopic == DB.Topic.None)
                {
                    // Report unknown topic
                    DatabaseFixer.Corruption newCorruption = new DatabaseFixer.Corruption
                                                                 {
                                                                     corruptedCell = cell,
                                                                     corruptedValue = topicId,
                                                                     culture = DB.Culture.Global,
                                                                     entryId = cell.entryIndex,
                                                                     kind =
                                                                         DatabaseFixer.CorruptionKind.
                                                                         UnknownReferencedTopic,
                                                                     referencedTopic = DB.Topic.None,
                                                                     topic = topic
                                                                 };

                    issues.Add(newCorruption);
                }
                else
                {
                    // Getting referenced topic
                    TduFile[] databaseItems = _LoadedData[referencedTopic];
                    DB dbTopic = databaseItems[0] as DB;

                    if (dbTopic == null)
                        throw new Exception("Unable to get database information for topic: " + referencedTopic);

                    // Get referenced value
                    List<DB.Entry> referencedEntries = DatabaseHelper.SelectAllCellsFromTopicWherePrimaryKey(dbTopic, cell.value);

                    if (referencedEntries.Count == 0)
                    {
                        // Report missing reference
                        DatabaseFixer.Corruption newCorruption = new DatabaseFixer.Corruption
                        {
                            corruptedCell = cell,
                            corruptedValue = cell.value,
                            culture = DB.Culture.Global,
                            entryId = cell.entryIndex,
                            kind =
                                DatabaseFixer.CorruptionKind.MissingReference,
                            referencedTopic = dbTopic.CurrentTopic,
                            topic = topic
                        };

                        issues.Add(newCorruption);
                    }
                }
            }
        }

        /// <summary>
        /// Checks cell value for specified resource and updates issues list
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="cell"></param>
        /// <param name="dbResource"></param>
        /// <param name="issues"></param>
        /// <returns></returns>
        private void _CheckResourceValue(DB.Topic topic, DB.Cell cell, DBResource dbResource, Collection<DatabaseFixer.Corruption> issues)
        {
            if (dbResource == null)
            {
                // Resource from another topic
                if (cell.optionalRef != null)
                {
                    DB.Topic referencedTopic = DB.TopicPerTopicId[cell.optionalRef];

                    // Is topic to be loaded ?
                    TduFile[] databaseItems = _LoadedData[referencedTopic];

                    dbResource = databaseItems[1] as DBResource;

                    if (dbResource == null)
                        throw new Exception("Unable to get database resource information for referenced topic: " + referencedTopic);
                }
            }

            if (dbResource == null)
                throw new Exception("Unable to get database resource information for topic: " + topic);

            if (!dbResource.GetEntryFromId(cell.value).isValid)
            {
                // Report missing resource
                DatabaseFixer.Corruption newCorruption = new DatabaseFixer.Corruption
                {
                    corruptedCell = cell,
                    corruptedValue = cell.value,
                    culture = dbResource.CurrentCulture,
                    entryId = cell.entryIndex,
                    kind =
                        DatabaseFixer.CorruptionKind.MissingResource,
                    referencedTopic = dbResource.CurrentTopic,
                    topic = topic
                };

                issues.Add(newCorruption);
            }
        }

        /// <summary>
        /// Checks previously loaded data
        /// </summary>
        /// <returns></returns>
        private Collection<DatabaseFixer.Corruption> _CheckDatabase()
        {
            Collection<DatabaseFixer.Corruption> returnedIssues = new Collection<DatabaseFixer.Corruption>();

            foreach (DB.Topic anotherTopic in _LoadedData.Keys)
            {
                IEnumerable<DatabaseFixer.Corruption> topicIssues = _CheckTopic(anotherTopic);

                foreach (DatabaseFixer.Corruption anotherIssue in topicIssues)
                    returnedIssues.Add(anotherIssue);
            }

            return returnedIssues;
        }

        /// <summary>
        /// Loads database files
        /// </summary>
        /// <param name="dbPath"></param>
        /// <param name="isReadOnly"></param>
        /// <param name="tasks"></param>
        private static Dictionary<DB.Topic, TduFile[]> _LoadDatabase(string dbPath, bool isReadOnly, out Dictionary<DB.Topic, EditHelper.Task[]> tasks)
        {
            Dictionary<DB.Topic, TduFile[]> returnedData = new Dictionary<DB.Topic, TduFile[]>();
            Dictionary<DB.Topic, EditHelper.Task[]> returnedTasks = new Dictionary<DB.Topic, EditHelper.Task[]>();

            if (!string.IsNullOrEmpty(dbPath))
            {
                // Loading...
                EditHelper.Instance.ClearTasks();
                returnedData = DatabaseHelper.LoadDatabase(dbPath, Program.ApplicationSettings.GetCurrentCulture(), isReadOnly,
                                                           out returnedTasks);
            }

            tasks = returnedTasks;
            return returnedData;
        }
        #endregion

        #region Events
        private void checkAllToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on 'check all' button
            ListView2.CheckAll(problemsListView);
        }

        private void unCheckAllToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on 'uncheck all' button
            ListView2.UncheckAll(problemsListView);
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            // Click on 'Check' button
            if (!string.IsNullOrEmpty(_DatabaseFolder))
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    StatusBarLogManager.ShowEvent(this, _STATUS_CHECKING);

                    // Loading
                    _LoadedData = _LoadDatabase(_DatabaseFolder, false, out _LoadedDataTasks);

                    // Checking
                    Collection<DatabaseFixer.Corruption> corrupts = _CheckDatabase();

                    // Displaying found issues in ListView
                    problemsListView.Items.Clear();

                    foreach(DatabaseFixer.Corruption anotherIssue in corrupts)
                    {
                        ListViewGroup currentGroup = _GroupIndex[anotherIssue.topic.ToString()];
                        ListViewItem newItem = new ListViewItem(anotherIssue.entryId.ToString(), currentGroup);

                        newItem.SubItems.Add(anotherIssue.corruptedCell.name);
                        newItem.SubItems.Add(anotherIssue.kind.ToString());
                        newItem.SubItems.Add(anotherIssue.referencedTopic.ToString());
                        newItem.SubItems.Add(anotherIssue.corruptedValue);
                        newItem.SubItems.Add(anotherIssue.culture.ToString());
                        newItem.SubItems.Add(anotherIssue.comment);
                        newItem.Tag = anotherIssue;

                        problemsListView.Items.Add(newItem);
                    }

                    // All unchecked
                    ListView2.UncheckAll(problemsListView);

                    // Logging
                    string successMessage;
                    
                    if (corrupts.Count == 0)
                        successMessage = _STATUS_CHECK_PERFECT;
                    else
                        successMessage = string.Format(_FORMAT_CHECK_RESULT, corrupts.Count);

                    StatusBarLogManager.ShowEvent(this, successMessage);
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                    StatusBarLogManager.ShowEvent(this, _STATUS_READY);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void browseDBButton_Click(object sender, EventArgs e)
        {
            // Click on browse folder button
            folderBrowserDialog.Description = _LABEL_BROWSE_FOLDER;

            DialogResult dr = folderBrowserDialog.ShowDialog(this);

            if (dr == DialogResult.OK)
                backupLocationTextBox.Text = folderBrowserDialog.SelectedPath;
        }

        private void fixButton_Click(object sender, EventArgs e)
        {
            // Click on 'Fix selected issues' button*
            if (string.IsNullOrEmpty(backupLocationTextBox.Text))
                MessageBoxes.ShowError(this, _ERROR_NO_BACKUP_PATH);
            else
            {
                int issuesCount = (int) ListView2.CheckedCount(problemsListView);

                if (issuesCount > 0)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        StatusBarLogManager.ShowEvent(this, _STATUS_FIXING);
                        toolStripProgressBar.Value = 0;

                        // Progress bar : step count
                        toolStripProgressBar.Maximum = issuesCount*2 + 2 + _LoadedDataTasks.Count*2;

                        // Gather selected issues
                        Collection<DatabaseFixer.Corruption> issuesToFix = new Collection<DatabaseFixer.Corruption>();

                        foreach (ListViewItem anotherItem in problemsListView.Items)
                        {
                            if (anotherItem.Checked)
                            {
                                issuesToFix.Add((DatabaseFixer.Corruption) anotherItem.Tag);
                                toolStripProgressBar.PerformStep();
                            }
                        }

                        if (issuesToFix.Count != 0)
                        {
                            // Prepares fixer module
                            Dictionary<DB.Topic, EditHelper.Task[]> tasks;

                            DatabaseFixer.Data = _LoadedData;
                            toolStripProgressBar.PerformStep();

                            DatabaseFixer.BackupData = _LoadDatabase(backupLocationTextBox.Text, true, out tasks);
                            toolStripProgressBar.PerformStep();

                            // Fix it!
                            int fixCount = 0;

                            foreach (DatabaseFixer.Corruption anotherCorruption in issuesToFix)
                            {
                                try
                                {
                                    DatabaseFixer.Fix(anotherCorruption);
                                    fixCount++;
                                }
                                catch (Exception ex)
                                {
                                    Log.Error("A database issue could not be fixed.", ex);
                                }

                                toolStripProgressBar.PerformStep();
                            }

                            // Edit Task finalization
                            foreach (DB.Topic anotherTopic in _LoadedDataTasks.Keys)
                            {
                                foreach (EditHelper.Task anotherTask in _LoadedDataTasks[anotherTopic])
                                {
                                    EditHelper.Instance.ApplyChanges(anotherTask);
                                    toolStripProgressBar.PerformStep();
                                }
                            }

                            // Logging
                            Application.DoEvents();
                            string statusMessage = string.Format(_FORMAT_FIX_RESULT, fixCount, issuesToFix.Count);

                            MessageBoxes.ShowInfo(this, statusMessage);

                            // Reloading
                            loadButton_Click(this, e);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxes.ShowError(this, ex);
                        StatusBarLogManager.ShowEvent(this, _STATUS_READY);
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                        toolStripProgressBar.Value = 0;
                    }
                }
            }
        }
        #endregion

        private void DbCheckDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Dialog was closed

            // Cleaning
            EditHelper.Instance.ClearTasks();
        }
    }
}

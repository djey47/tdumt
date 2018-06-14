using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.Types.Collections;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingTools.gui.common;

namespace TDUModdingTools.gui.dbeditor.db
{
    /// <summary>
    /// Diaklog box to modify database resources
    /// </summary>
    public partial class MiniResourceEditorDialog : Form
    {
        #region Constants
        /// <summary>
        /// Format for information label
        /// </summary>
        private const string _FORMAT_INFO_LABEL = "Resource {0} from {1}({2}) : {3}";

        /// <summary>
        /// Message to display in values browser
        /// </summary>
        private const string _MESSAGE_BROWSE_RES_VALUES = "Please select a new resource value.";
        #endregion

        #region Members
        /// <summary>
        /// Current database topic
        /// </summary>
        private DB _DbTopic;

        /// <summary>
        /// Current database resource
        /// </summary>
        private DBResource _DbResource;

        /// <summary>
        /// Current resource identifier
        /// </summary>
        private string _ResourceId;

        /// <summary>
        /// Current resource value
        /// </summary>
        private string _ResourceValue;

        private int _ColumnId;

        private int _EntryId;
        #endregion

        private MiniResourceEditorDialog() {}

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="dbTopic"></param>
        /// <param name="entryId"></param>
        /// <param name="columnId"></param>
        /// <param name="dbResource"></param>
        /// <param name="linkedRes"></param>
        public MiniResourceEditorDialog(DB dbTopic,int entryId, int columnId, DBResource dbResource, string linkedRes)
        {
            InitializeComponent();

            _DbTopic = dbTopic;
            _DbResource = dbResource;
            _ResourceId = linkedRes;
            _ColumnId = columnId;
            _EntryId = entryId;

            _InitializeContents();
        }

        #region Private methods
        /// <summary>
        /// Defines dialog contents
        /// </summary>
        private void _InitializeContents()
        {
            _UpdateInfoLabel();

            // Resource value
            resourceValueTextBox.Text = _ResourceValue;

            selectResourceRadioButton.Checked = true;
        }

        /// <summary>
        /// Sets contents on information label
        /// </summary>
        private void _UpdateInfoLabel()
        {
            DB.Culture currentCulture = Program.ApplicationSettings.GetCurrentCulture();

            _ResourceValue = _DbResource.GetEntryFromId(_ResourceId).value;

            resourceInfoLabel.Text = string.Format(_FORMAT_INFO_LABEL, _ResourceId, _DbResource.CurrentTopic,currentCulture,
                                                   _ResourceValue);
            
        }

        /// <summary>
        /// Defines activation state of group controls
        /// </summary>
        private void _UpdateGroupControls()
        {
            resourceBrowseButton.Enabled = selectResourceRadioButton.Checked;
            resourceValueTextBox.Enabled = !selectResourceRadioButton.Checked;

            if (!selectResourceRadioButton.Checked)
                resourceValueTextBox.Focus();
        }
        #endregion

        #region Events
        private void selectResourceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Click on radio button
            _UpdateGroupControls();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            // Click on OK button > Apply then close
            try
            {
                // According to selected update mode
                if (selectResourceRadioButton.Checked)
                {
                    // Another resource id
                    string columnName = _DbTopic.Structure[_ColumnId].name;

                    DatabaseHelper.UpdateCellFromTopicWithEntryId(_DbTopic, columnName, _ResourceId, _EntryId);
                }
                else
                {
                    // Changed resource value
                    DBResource.Entry editedEntry = _DbResource.GetEntryFromId(_ResourceId);

                    editedEntry.value = resourceValueTextBox.Text;
                    _DbResource.UpdateEntry(editedEntry);
                }

                DialogResult = DialogResult.OK;
                Close();
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

        private void resourceBrowseButton_Click(object sender, EventArgs e)
        {
            // Click on browse button
            try
            {
                Cursor = Cursors.WaitCursor;

                // Preparing value list
                SortedStringCollection sortedValueList = new SortedStringCollection();
                Dictionary<string, string> index = new Dictionary<string, string>();

                foreach (DBResource.Entry anotherEntry in _DbResource.EntryList)
                {
                    if (anotherEntry.isValid && !anotherEntry.isComment)
                    {
                        string value = anotherEntry.value;

                        if (!sortedValueList.Contains(value))
                        {
                            sortedValueList.Add(value);
                            index.Add(value, anotherEntry.id.Id);
                        }
                    }
                }
            
                // Displaying browse dialog
                TableBrowsingDialog dialog = new TableBrowsingDialog(_MESSAGE_BROWSE_RES_VALUES, sortedValueList, index);
                DialogResult dr = dialog.ShowDialog();

                if (dr == DialogResult.OK && dialog.SelectedIndex != null)
                {
                    // Resource update
                    _ResourceId = dialog.SelectedIndex;
                    _UpdateInfoLabel();
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
        #endregion
    }
}

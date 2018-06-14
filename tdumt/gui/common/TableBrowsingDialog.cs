using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Dialogs.Search;
using TDUModdingTools.common;

namespace TDUModdingTools.gui.common
{
    /// <summary>
    /// Browser for database entries
    /// </summary>
    public partial class TableBrowsingDialog : Form
    {
        #region Constants
        /// <summary>
        /// Message for search box
        /// </summary>
        private const string _MESSAGE_SEARCH = "Which name are you looking for ?";

        /// <summary>
        /// Message for resource adding
        /// </summary>
        private const string _MESSAGE_ADD_TDUC = "It's not possible to add names here; though you can request for new models or versions in TDU:Central forum, to be added later in a patch update.\r\nYour web browser will now display corresponding page.";
        #endregion

        #region Properties
        /// <summary>
        /// Selected value
        /// </summary>
        public string SelectedValue
        {
            get { return _SelectedValue; }
        }
        private string _SelectedValue;

        /// <summary>
        /// Index of selected value
        /// </summary>
        public string SelectedIndex
        {
            get { return _SelectedIndex;  }
        }
        private string _SelectedIndex;

        /// <summary>
        /// Returs or defines the status of add button
        /// </summary>
        public bool IsAddButtonEnabled
        {
            get { return addToolStripButton.Enabled; }
            set { addToolStripButton.Enabled = value; }
        }
        #endregion

        #region Members
        /// <summary>
        /// Message to display
        /// </summary>
        private readonly string _Message;

        /// <summary>
        /// Value list
        /// </summary>
        private readonly IList<string> _Values;

        /// <summary>
        /// Association (value, index)
        /// </summary>
        private readonly Dictionary<string, string> _Index;

        /// <summary>
        /// Instance of search box
        /// </summary>
        private SearchBox _SearchBoxInstance;
        #endregion

        /// <summary>
        /// Main constructor
        /// </summary>
        public TableBrowsingDialog(string message, IList<string> values, Dictionary<string, string> index)
        {
            InitializeComponent();

            _Message = message;
            _Values = values;
            _Index = index;

            _InitializeContents();
        }

        #region Private methods
        /// <summary>
        /// Defines box contents
        /// </summary>
        private void _InitializeContents()
        {
            Cursor = Cursors.WaitCursor;

            // Value list : browsing source
            foreach (string value in _Values)
            {
                string index = null;

                if (_Index.ContainsKey(value))
                    index = _Index[value];

                ListViewItem li = new ListViewItem(value) {Tag = index};

                valueListView.Items.Add(li);
            }

            // Message
            messageLabel.Text = _Message;

            // Add button
            addToolStripButton.Enabled = false;

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Returns instance of Search Box
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private SearchBox _GetSearchBoxInstance(ListView listView, string message)
        {
            if (_SearchBoxInstance == null)
                _SearchBoxInstance = new ListViewSearchBox(listView, message);

            return _SearchBoxInstance;
        }
        #endregion

        #region Events
        private void getToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on 'Get value' button
            if (valueListView.SelectedItems.Count != 0)
            {
                _SelectedIndex = valueListView.SelectedItems[0].Tag as string;
                _SelectedValue = valueListView.SelectedItems[0].Text;

                DialogResult = DialogResult.OK;
            }
        }

        private void valueListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Double click on a value
            getToolStripButton_Click(sender, e);
        }

        private void searchToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on 'Search' button
            if (valueListView.Items.Count > 0)
            {
                SearchBox box = _GetSearchBoxInstance(valueListView, _MESSAGE_SEARCH);

                box.Show(this);
            }
        }

        private void valueListView_KeyDown(object sender, KeyEventArgs e)
        {
            // A key has been pressed into list > use selected brand if RETURN key 
            if (Keys.Enter.Equals(e.KeyCode))
                getToolStripButton_Click(sender, e);
        }


        private void addToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on 'Add...' button
            try
            {
             
                MessageBoxes.ShowInfo(this, _MESSAGE_ADD_TDUC);

                // Starts default web browser
                ProcessStartInfo editorProcess = new ProcessStartInfo(AppConstants.URL_RESOURCES_TDUC);

                Process.Start(editorProcess);


            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }
        #endregion
    }
}
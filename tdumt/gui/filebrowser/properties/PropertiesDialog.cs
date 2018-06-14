using System.IO;
using System.Windows.Forms;
using DjeFramework1.Common.Types.Forms;
using TDUModdingLibrary.fileformats;

namespace TDUModdingTools.gui.filebrowser.properties
{
    public partial class PropertiesDialog : Form
    {
        #region Constants
        /// <summary>
        /// Format for window title
        /// </summary>
        private const string _FORMAT_WINDOW_TITLE = "{0} properties...";

        /// <summary>
        /// Format for copied value to clipboard
        /// </summary>
        private const string _FORMAT_VALUE_COPY = "{0}: {1}";
        #endregion

        #region Members
        /// <summary>
        /// Name of file to display properties
        /// </summary>
        private readonly string _FileName;
        #endregion

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="fileName">Name of file to display properties</param>
        public PropertiesDialog(string fileName)
        {
            InitializeComponent();

            _FileName = fileName;

            _InitializeContents();
        }

#pragma warning disable UnusedPrivateMember
        /// <summary>
        /// Forbidden constructor
        /// </summary>
        private PropertiesDialog()
#pragma warning restore UnusedPrivateMember
        {
            InitializeComponent();
        }

        #region Private methods
        /// <summary>
        /// Defines window contents
        /// </summary>
        private void _InitializeContents()
        {
            // Window title
            FileInfo fi = new FileInfo(_FileName);

            Text = string.Format(_FORMAT_WINDOW_TITLE, fi.Name);

            // Loading file
            TduFile file = TduFile.GetFile(_FileName);

            // Properties
            if (file.Exists)
                ListView2.FillWithProperties(propertiesListView, file.Properties); 
        }
        #endregion

        private void copyButton_Click(object sender, System.EventArgs e)
        {
            // Click on 'Copy' button
            if (propertiesListView.SelectedItems.Count == 1)
            {
                string valueToCopy =
                    string.Format(_FORMAT_VALUE_COPY, propertiesListView.SelectedItems[0].Text,
                                  propertiesListView.SelectedItems[0].SubItems[1].Text);
            
                Clipboard.SetText(valueToCopy, TextDataFormat.Text);
            }
        }
    }
}
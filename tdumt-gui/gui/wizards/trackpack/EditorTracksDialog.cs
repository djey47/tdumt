using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Dialogs.Search;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Common.Types.Forms;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.world;
using TDUModdingLibrary.support.constants;

namespace TDUModdingTools.gui.wizards.trackpack
{
    public partial class EditorTracksDialog : Form
    {
        #region Constants
        /// <summary>
        /// Search box mesage
        /// </summary>
        private const string _LABEL_SEARCH = "";
        #endregion

        #region Properties
        /// <summary>
        /// TDU tracks which have been selected
        /// </summary>
        public IGE[] SelectedTracks { get; set; }
        #endregion

        #region Private fields
        /// <summary>
        /// Instance of search box
        /// </summary>
        private static SearchBox _SearchBoxInstance;

        /// <summary>
        /// Indicates if selection mode is active
        /// </summary>
        private readonly bool _IsSelectMode;
        #endregion

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="isSelectMode"></param>
        public EditorTracksDialog(bool isSelectMode)
        {
            InitializeComponent();

            _IsSelectMode = isSelectMode;
            _InitializeContents();
        }

        #region Private methods
        /// <summary>
        /// 
        /// </summary>
        private void _InitializeContents()
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                // Profile name
                profileName.Text = Program.ApplicationSettings.PlayerProfile;

                // OK button status
                okToolStripButton.Enabled = _IsSelectMode;

                // DFE list
                _RefreshIGEList();
            }
            catch (Exception ex)
            {
                 MessageBox.Show(this, ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }

        }

        /// <summary>
        /// Updates DFE files list view
        /// </summary>
        private void _RefreshIGEList()
        {
            //Checkboxes visibility
            igeListView.CheckBoxes = _IsSelectMode;

            igeListView.Items.Clear();

            Collection<IGE> _EditorChallenges = new Collection<IGE>();

            // Loading IGE tracks
            string profileFolder = string.Concat(LibraryConstants.GetSpecialFolder(LibraryConstants.TduSpecialFolder.Savegame), @"\", Program.ApplicationSettings.PlayerProfile);
            string igeFolder = string.Concat(profileFolder, LibraryConstants.FOLDER_IGE);

            if (Directory.Exists(igeFolder))
            {
                FileInfo[] igeFiles = new DirectoryInfo(igeFolder).GetFiles();

                foreach (FileInfo anotherTrack in igeFiles)
                {
                    IGE newIge = TduFile.GetFile(anotherTrack.FullName) as IGE;

                    if (newIge == null)
                        Log.Warning("Error when loading IGE track file: " + anotherTrack.FullName + ", skipping...");
                    else
                        _EditorChallenges.Add(newIge);
                }
            }

            // Filling editor tracks...
            int trackIndex = 1;

            foreach (IGE igeFile in _EditorChallenges)
            {
                // New list item
                ListViewItem lvi = new ListViewItem(trackIndex.ToString()) {Tag = igeFile};

                // Track names
                lvi.SubItems.Add(igeFile.TrackName);
                lvi.SubItems.Add(igeFile.Description);
                lvi.SubItems.Add(igeFile.TrackId);

                igeListView.Items.Add(lvi);

                trackIndex++;
            }
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
        private void searchToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on Search button
            try
            {
                Cursor = Cursors.WaitCursor;

                SearchBox searchBox = _GetSearchBoxInstance(igeListView, _LABEL_SEARCH);

                searchBox.Show(this);
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

        private void okToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on OK button
            try
            {
                Cursor = Cursors.WaitCursor;

                SelectedTracks = new IGE[ListView2.CheckedCount(igeListView)];

                // Browsing items...
                int index = 0;

                foreach (ListViewItem li in igeListView.Items)
                {
                    if (li.Checked)
                        SelectedTracks[index++] = li.Tag as IGE;
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
        #endregion
    }
}

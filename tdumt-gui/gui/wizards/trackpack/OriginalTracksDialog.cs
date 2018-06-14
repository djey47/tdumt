using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Dialogs.Search;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.world;

namespace TDUModdingTools.gui.wizards.trackpack
{
    public partial class OriginalTracksDialog : Form
    {
        #region Constants
        /// <summary>
        /// Search box mesage
        /// </summary>
        private const string _LABEL_SEARCH = "";

        /// <summary>
        /// N/A list item text
        /// </summary>
        private const string _ITEM_N_A = "/";

        /// <summary>
        /// None list item text
        /// </summary>
        private const string _ITEM_NONE = "None";

        /// <summary>
        /// IDs of dummy speed online tracks
        /// </summary>
        private readonly static Collection<string> _DUMMY_TRACK_IDS =
            new Collection<string> {_TRACK_ID_SPEEDONLINE1, _TRACK_ID_MASTERY, _TRACK_ID_TRAFFICMYLOVE};
        
        /// <summary>
        /// ID of dummy speed online track
        /// </summary>
        private const string _TRACK_ID_SPEEDONLINE1 = "data_dfe_535e399e939608c8ef036f359c8758e7";

        /// <summary>
        /// ID of speed online track which can't be replaced...
        /// </summary>
        private const string _TRACK_ID_MASTERY = "data_dfe_86b44fb3b51d9819268eb6febaa2a8b4";

        /// <summary>
        /// ID of speed online track which can't be replaced...
        /// </summary>
        private const string _TRACK_ID_TRAFFICMYLOVE = "data_dfe_51270a92918f3440f57d8db5a4b9a46b";
        #endregion

        #region Properties
        /// <summary>
        /// Track id which has been selected
        /// </summary>
        public int SelectedTrackId { get; set; }
        #endregion

        #region Private fields
        /// <summary>
        /// DB Resource : Houses
        /// </summary>
        private readonly DBResource _HousesResource;

        /// <summary>
        /// Box mode
        /// </summary>
        private readonly bool _IsSelectMode;

        /// <summary>
        /// Instance of search box
        /// </summary>
        private static SearchBox _SearchBoxInstance;

        /// <summary>
        /// List of default challenges for reference
        /// </summary>
        private readonly Collection<DFE> _DefaultChallenges;
        #endregion

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="defaultChallenges"></param>
        /// <param name="housesResource"></param>
        /// <param name="isSelectMode">true to allow track selection, else false</param>
        public OriginalTracksDialog(Collection<DFE> defaultChallenges, DBResource housesResource, bool isSelectMode)
        {
            InitializeComponent();

            _DefaultChallenges = defaultChallenges;
            _HousesResource = housesResource;
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
                // DFE list
                _RefreshDFEList();

                // OK button
                okToolStripButton.Enabled = _IsSelectMode;
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
        private void _RefreshDFEList()
        {
            dfeListView.Items.Clear();

            // If selection mode, a special item is added at beginning > None
            if(_IsSelectMode)
            {
                ListViewItem lvi = new ListViewItem("0");

                // Track names
                lvi.SubItems.Add(_ITEM_NONE);

                lvi.SubItems.Add(_ITEM_N_A);
                lvi.SubItems.Add(_ITEM_N_A);
                lvi.SubItems.Add(_ITEM_N_A);
                lvi.SubItems.Add(_ITEM_N_A);

                dfeListView.Items.Add(lvi);
            }

            // Filling tracks...
            int trackIndex = 1;

            foreach(DFE dfeFile in _DefaultChallenges)
            {
                // Select mode: only replace speed challenges
                if (!_IsSelectMode
                    || (_IsSelectMode
                        && ! _DUMMY_TRACK_IDS.Contains(dfeFile.TrackId)
                        && dfeFile.ChallengeType == DFE.GameType.Online 
                        && dfeFile.ChallengeMode == DFE.GameMode.Speed))
                {
                    // New list item
                    ListViewItem lvi = new ListViewItem(trackIndex.ToString()) {Tag = dfeFile};

                    // Track names
                    lvi.SubItems.Add(dfeFile.TrackName);

                    string translatedTrackName = _GetNameFromResource(dfeFile.TrackNameDbResource);

                    lvi.SubItems.Add(translatedTrackName);
                    lvi.SubItems.Add(dfeFile.ChallengeMode.ToString());
                    lvi.SubItems.Add(dfeFile.ChallengeType.ToString());
                    lvi.SubItems.Add(dfeFile.TrackId);

                    dfeListView.Items.Add(lvi);
                }

                trackIndex++;
            }
        }

        /// <summary>
        /// Returns track name from specified resource identifier
        /// </summary>
        /// <param name="trackNameDbResource"></param>
        /// <returns></returns>
        private string _GetNameFromResource(string trackNameDbResource)
        {
            string returnedName = TrackPackForm._DEFAULT_TRACK_NAME;

            if (_HousesResource != null && trackNameDbResource != null)
            {
                DBResource.Entry currentEntry = _HousesResource.GetEntryFromId(trackNameDbResource);

                returnedName = currentEntry.value;
            }

            return returnedName;
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

                SearchBox searchBox = _GetSearchBoxInstance(dfeListView, _LABEL_SEARCH);

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
            // Click on OK button (select mode)
            if (dfeListView.SelectedItems.Count == 1)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    int selectedIndex = int.Parse(dfeListView.SelectedItems[0].Text);

                    // If selection mode, first item is the none indicator
                    if (_IsSelectMode && selectedIndex == 0)
                        SelectedTrackId = -1;
                    else
                        SelectedTrackId = selectedIndex - 1;

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
        }

        private void OriginalTracksDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Removing search box instance
            _SearchBoxInstance = null;
        }
        #endregion
    }
}
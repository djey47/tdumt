using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Dialogs.Search;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Common.Types;
using DjeFramework1.Common.Types.Forms;
using DjeFramework1.Util.BasicStructures;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.world;
using TDUModdingLibrary.fileformats.world.helper;
using TDUModdingLibrary.support;
using TDUModdingLibrary.support.constants;
using TDUModdingTools.common;
using TDUModdingTools.gui.settings;

namespace TDUModdingTools.gui.wizards.trackpack
{
    public partial class TrackPackForm : Form
    {
        #region Constants
        /// <summary>
        /// Search box message
        /// </summary>
        private const string _LABEL_CUSTOM_SEARCH = "";

        /// <summary>
        /// List item when custom track does not replace any game challenge
        /// </summary>
        private const string _ITEM_REPLACES_NONE = "<None>";

        /// <summary>
        /// Separator used to indicate replacing information in custom track file name
        /// </summary>
        private const string _REPLACING_SEPARATOR = "_R_";

        /// <summary>
        /// Default track name if specified resource does not exist
        /// </summary>
        internal const string _DEFAULT_TRACK_NAME = "<Resource not found>";

        /// <summary>
        /// Status message when ready state
        /// </summary>
        private const string _STATUS_READY = "Ready.";

        /// <summary>
        /// Status message when setting process ended without errors
        /// </summary>
        private const string _STATUS_SET_OK = "Changes were applied succesfully.";

        /// <summary>
        /// Status message when track could not be activated (no replacement)
        /// </summary>
        private const string _STATUS_ACTIVATE_KO_NO_REPLACEMENT =
            "Unable to activate this track: it does not replace any game challenge. Please define one.";

        /// <summary>
        /// Status message when track could not be activated (already replaced challenge)
        /// </summary>
        private const string _STATUS_ACTIVATE_KO_ALREADY_REPLACED =
            "Unable to activate this track: another active track already replaces the same game challenge. Please change.";

        /// <summary>
        /// Status message when waypoint merging process ended without errors
        /// </summary>
        private const string _STATUS_MERGE_OK = "All waypoints were merged succesfully.";

        /// <summary>
        /// Status message when track deletion ended without errors
        /// </summary>
        private const string _STATUS_DELETE_OK = "Tracks was succesfully deleted.";

        /// <summary>
        /// Status message when track synchronization process ended without errors
        /// </summary>
        private const string _FORMAT_TRACK_SYNC_OK = "Track synchronization succesful: {0} over {1}";

        /// <summary>
        /// Status message when track restoration process ended without errors
        /// </summary>
        //private const string _FORMAT_TRACK_RESTORE_OK = "Track restoration succesful: {0}";

        /// <summary>
        /// Status message pattern when import (editor) process ended
        /// </summary>
        private const string _FORMAT_IMPORT_IGE_OK = "Import complete. {0} on {1} track(s) succesfully converted.";

        /// <summary>
        /// Status message pattern when export process ended
        /// </summary>
        private const string _FORMAT_EXPORT_OK = "Export complete. {0} track(s) succesfully packaged.";

        /// <summary>
        /// Label pattern for folder exploring
        /// </summary>
        private const string _FORMAT_TRACKS_FOLDER_LABEL = "Explore {0}...";

        /// <summary>
        /// Title for custom track selector box
        /// </summary>
        private const string _TITLE_PICK_TRACK_TO_MERGE = "Pick custom track containing waypoints to add...";

        /// <summary>
        /// 
        /// </summary>
        private const string _TITLE_EXPORT = "Export checked tracks...";

        /// <summary>
        /// Question to continue merging
        /// </summary>
        private const string _QUESTION_CONTINUE_MERGE = "Add more checkpoints?";

        /// <summary>
        /// 
        /// </summary>
        private const string _QUESTION_DELETE_TRACK = "Really delete this custom track?";

        /// <summary>
        /// 
        /// </summary>
        private const string _WARNING_CHECK_EXPORT = "You must check tracks to export!";

        /// <summary>
        /// 
        /// </summary>
        private const string _MSG_EXPORT = "Please a give a name for this new track pack:";

        /// <summary>
        /// 
        /// </summary>
        private const string _MSG_PATH_EXPORT = "Please give a location for this new track pack:";

        /// <summary>
        /// 
        /// </summary>
        private const string _NAME_EXPORT = "Untitled Track Pack";

        /// <summary>
        /// 
        /// </summary>
        private const string _ERROR_REPLACE_EXPORT = "At least one checked track does not replace any game challenge.\r\nPlease fix it then try again.";
        #endregion

        #region Properties
        /// <summary>
        /// Access to form instance
        /// </summary>
        public static Form Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new TrackPackForm();

                return _Instance;
            }
        }
        private static Form _Instance;
        #endregion

        #region Private fields
        /// <summary>
        /// DB Resource : Houses
        /// </summary>
        private DBResource _HousesResource;

        /// <summary>
        /// List of default challenges for reference
        /// </summary>
        private readonly Collection<DFE> _DefaultChallenges = new Collection<DFE>();

        /// <summary>
        /// Selected custom track
        /// </summary>
        private DFE _CurrentTrack;

        /// <summary>
        /// Currently replaced track
        /// </summary>
        private DFE _ReplacedChallenge;

        /// <summary>
        /// Indicates if current track has been changed
        /// </summary>
        private bool _IsChanged;
        
        /// <summary>
        /// Instance of search box
        /// </summary>
        private static SearchBox _SearchBoxInstance;
        #endregion

        /// <summary>
        /// Main constructor
        /// </summary>
        public TrackPackForm()
        {
            InitializeComponent();
            _LoadDatabase();
            _DefaultChallenges = TracksHelper.LoadDefaultChallenges(AppConstants.FOLDER_ORIGINAL_CHALLENGES);
            _InitializeContents();
        }

        #region Private methods
        /// <summary>
        /// Handles database loading
        /// </summary>
        private void _LoadDatabase()
        {
            DB.Culture currentCulture = Program.ApplicationSettings.GetCurrentCulture();
            string resourceBNKFileName = DB.GetBNKFileName(currentCulture);
            BNK resourceBNK = TduFile.GetFile(Tools.TduPath + LibraryConstants.FOLDER_DB + resourceBNKFileName) as BNK;

            if (resourceBNK != null)
            {
                // Extracting Houses resource
                string packedFileName = DB.GetFileName(currentCulture, DB.Topic.Houses);
                string packedFilePath = resourceBNK.GetPackedFilesPaths(packedFileName)[0];
                string tempLocation = File2.SetTemporaryFolder(null, LibraryConstants.FOLDER_TEMP, true);

                resourceBNK.ExtractPackedFile(packedFilePath, tempLocation, true);

                // Loading resource
                _HousesResource = TduFile.GetFile(tempLocation + @"\" + packedFileName) as DBResource;

                if (_HousesResource == null)
                    throw new Exception("Error while loading database resources (Houses) !");
            }
        }

        /// <summary>
        /// Defines window contents
        /// </summary>
        private void _InitializeContents()
        {
            Cursor = Cursors.WaitCursor;

            // Track details group
            replacedLinkLabel.Text = "";
            trackNameLabel.Text = "";
            cpLabel.Text = "";
            
            // Game modes
            foreach (DFE.GameMode anotherMode in Enum.GetValues(typeof(DFE.GameMode)))
            {
                if (anotherMode != DFE.GameMode.Unhandled)
                    gameModeComboBox.Items.Add(anotherMode.ToString());
            }
                
            // Game types
            foreach (DFE.GameType anotherType in Enum.GetValues(typeof(DFE.GameType)))
            {
                // Club challenges make game freeze if replace solo or multi spot
                if (anotherType != DFE.GameType.Unhandled && anotherType != DFE.GameType.Club)
                    typeComboBox.Items.Add(anotherType.ToString());
            }
                
            gameModeComboBox.Text = typeComboBox.Text = "";
            descriptionTextBox.Text = "";

            // With radars status
            withRadarsCheckBox.Enabled = false;

            // Eliminator status
            eliminatorPanel.Visible = false;

            // Driving aids
            foreach (DFE.DrivingAid anotherAid in Enum.GetValues(typeof(DFE.DrivingAid)))
            {
                if (anotherAid != DFE.DrivingAid.Unhandled)
                    drivingAidsComboBox.Items.Add(anotherAid.ToString());
            }

            // Custom list
            _RefreshCustomList();

            // Explore button
            string tracksFolder = AppConstants.FOLDER_TRACKS;
            string buttonLabel = string.Format(_FORMAT_TRACKS_FOLDER_LABEL, tracksFolder);

            exploreToolStripButton.Text = buttonLabel;

            // Flags
            _IsChanged = false;

            // Status log
            StatusBarLogManager.AddNewLog(this, statusLabel);
            StatusBarLogManager.ShowEvent(this, _STATUS_READY);

                Cursor = Cursors.Default;
            //}
        }

        /// <summary>
        /// Updates custom tracks list view
        /// </summary>
        private void _RefreshCustomList()
        {
            // Stores checked tracks
            ListView2.StoreCheckedItems(customTracksListView);

            // Stores selected index
            ListView2.StoreSelectedIndex(customTracksListView);

            customTracksListView.Items.Clear();

            // Get all custom tracks in tracks folder
            string tracksFolder = AppConstants.FOLDER_TRACKS;
            DirectoryInfo tracksDir = new DirectoryInfo(tracksFolder);

            if (!tracksDir.Exists)
                // Must be created
                tracksDir = Directory.CreateDirectory(tracksFolder);

            FileInfo[] allTracks = tracksDir.GetFiles();

            foreach (FileInfo anotherTrackFile in allTracks)
            {
                // Loading DFE file
                DFE dfeFile;

                try
                {
                    dfeFile = TduFile.GetFile(anotherTrackFile.FullName) as DFE;
    
                    if (dfeFile == null)
                        throw new Exception("Unknown track file format.");


                    ListViewItem li = new ListViewItem(dfeFile.TrackName) {Tag = dfeFile};

                    li.SubItems.Add(dfeFile.ChallengeMode.ToString());
                    li.SubItems.Add(dfeFile.ChallengeType.ToString());

                    // Gets replaced challenge from file name
                    DFE replacedChallenge = _GetReplacedChallenge(anotherTrackFile.Name);

                    if (replacedChallenge == null)
                        li.SubItems.Add(_ITEM_REPLACES_NONE);
                    else
                        li.SubItems.Add(_GetReplacedChallengeName(replacedChallenge));

                    // File name
                    li.SubItems.Add(new FileInfo(dfeFile.FileName).Name);

                    customTracksListView.Items.Add(li);
                }
                catch (Exception ex)
                {
                    Log.Error("Unable to load custom track: " + anotherTrackFile.Name, ex);
                }



                    



            }

            // Restores selected index
            ListView2.RestoreSelectedIndex(customTracksListView);

            // Restores checked tracks
            ListView2.RestoreCheckedItems(customTracksListView);
        }

        /// <summary>
        /// Returns default challenge replaced by specified custom track 
        /// </summary>
        /// <param name="customFileName"></param>
        /// <returns>Null if no challenge has been found</returns>
        private DFE _GetReplacedChallenge(string customFileName)
        {
            DFE returnedChallenge = null;

            if (!string.IsNullOrEmpty(customFileName))
            {
                // Extracts replacing information
                string[] splitted = customFileName.Split(new[] {_REPLACING_SEPARATOR}, 2, StringSplitOptions.RemoveEmptyEntries);

                if (splitted.Length == 2)
                {
                    int challengeIndex;
                    bool parsingResult = int.TryParse(splitted[1], out challengeIndex);

                    if (parsingResult && _DefaultChallenges.Count > challengeIndex)
                        returnedChallenge = _DefaultChallenges[challengeIndex];
                    else
                        Log.Warning("Invalid replacing information in file name: " + customFileName);
                }
            }

            return returnedChallenge;
        }

        /// <summary>
        /// Returns replaced challenge name to be displayed
        /// </summary>
        /// <param name="replacedChallenge"></param>
        /// <returns></returns>
        private string _GetReplacedChallengeName(DFE replacedChallenge)
        {
            string returnedName = _ITEM_REPLACES_NONE;

            if (replacedChallenge != null)
                returnedName = _GetNameFromResource(replacedChallenge.TrackNameDbResource);

            return returnedName;
        }

        /// <summary>
        /// Returns track name from specified resource identifier
        /// </summary>
        /// <param name="trackNameDbResource"></param>
        /// <returns></returns>
        private string _GetNameFromResource(string trackNameDbResource)
        {
            string returnedName = _DEFAULT_TRACK_NAME;

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

        /// <summary>
        /// Updates details for selected track
        /// </summary>
        private void _RefreshTrackDetails()
        {
            if (_CurrentTrack != null)
            {
                // Track name
                trackNameLabel.Text = _CurrentTrack.TrackName;

                // Replaced challenge
                _UpdateReplacementLabel(false);

                // Checkpoint count
                cpLabel.Text = _CurrentTrack.CheckpointCount.ToString();

                // Game mode + type
                gameModeComboBox.Text = _CurrentTrack.ChallengeMode.ToString();
                typeComboBox.Text = _CurrentTrack.ChallengeType.ToString();

                // With radars ?
                withRadarsCheckBox.Checked = _CurrentTrack.WithRadars;

                // Description
                descriptionTextBox.Text = _CurrentTrack.Description;

                // Gauge points
                gaugePointsUpDown.Value = _CurrentTrack.DrivingGaugePoints;

                // Marked route
                routeMarkingCheckBox.Checked = _CurrentTrack.MarkedRoute;

                // Gps
                useGPSCheckBox.Checked = _CurrentTrack.UseGPS;

                // Lap support
                withLapsCheckBox.Checked = _CurrentTrack.WithLaps;

                // Lap count
                lapsUpDown.Value = _CurrentTrack.LapCount;

                // Cockpit view
                forceCockpitCheckBox.Checked = _CurrentTrack.ForceCockpitView;

                // Traffic
                trafficCheckBox.Checked = _CurrentTrack.WithTraffic;

                // Traffic density
                int trafficLevel = (int)_CurrentTrack.TrafficLevel;

                if (trafficLevel == -1)
                    trafficLevel = 0;

                trafficLevelTrackBar.Value = trafficLevel;

                // Speed to reach
                speedToReachTextBox.Text = _CurrentTrack.SpeedToReach.ToString();

                // With driving gauge ?
                withDrivingGaugeCheckBox.Checked = _CurrentTrack.WithDrivingGauge;

                // With cops?
                withCopsCheckBox.Checked = _CurrentTrack.WithCops;

                // Cops density
                copsLevelTrackBar.Value = (int)_CurrentTrack.CopsLevel;

                // Driving aids
                drivingAidsComboBox.Text = _CurrentTrack.DrivingAids.ToString();

                // Bot count
                nbAIUpDown.Value = _CurrentTrack.BotCount;
            }
        }

        /// <summary>
        /// Changes challenge replacement label
        /// </summary>
        /// <param name="isNoReplacement">true to indicate the track won't replace any challenge</param>
        private void _UpdateReplacementLabel(bool isNoReplacement)
        {
            if (isNoReplacement)
                _ReplacedChallenge = null;
            else
            {
                string fileName = new FileInfo(_CurrentTrack.FileName).Name;

                if (_ReplacedChallenge == null)
                    _ReplacedChallenge = _GetReplacedChallenge(fileName);
            }

            replacedLinkLabel.Text = _GetReplacedChallengeName(_ReplacedChallenge);
        }

        /// <summary>
        /// 
        /// </summary>
        private void _SetRulesInformation()
        {
            // Force cockpit view
            _CurrentTrack.ForceCockpitView = forceCockpitCheckBox.Checked;

            // Solo rules
            // Driving aids
            _CurrentTrack.DrivingAids = (DFE.DrivingAid)Enum.Parse(typeof(DFE.DrivingAid), drivingAidsComboBox.Text);

            // Opponents count
            _CurrentTrack.BotCount = (uint)nbAIUpDown.Value;
        }

        /// <summary>
        /// Defines main information
        /// </summary>
        private void _SetMainInformation()
        {
            // Game mode
            _CurrentTrack.ChallengeMode = (DFE.GameMode)Enum.Parse(typeof(DFE.GameMode), gameModeComboBox.Text);

            // Game type
            _CurrentTrack.ChallengeType = (DFE.GameType)Enum.Parse(typeof(DFE.GameType), typeComboBox.Text);

            // With radars ?
            _CurrentTrack.WithRadars = withRadarsCheckBox.Checked;

            // Briefing
            _CurrentTrack.Description = descriptionTextBox.Text;

        }

        /// <summary>
        /// Defines objective information
        /// </summary>
        private void _SetObjectivesInformation()
        {
            // Driving gauge points
            _CurrentTrack.DrivingGaugePoints = (uint)gaugePointsUpDown.Value;

            // Traffic
            _CurrentTrack.WithTraffic = trafficCheckBox.Checked;
            _CurrentTrack.TrafficLevel = (uint)trafficLevelTrackBar.Value;

            // Marked route
            _CurrentTrack.MarkedRoute = routeMarkingCheckBox.Checked;

            // GPS support
            _CurrentTrack.UseGPS = useGPSCheckBox.Checked;

            // Laps
            _CurrentTrack.WithLaps = withLapsCheckBox.Checked;
            _CurrentTrack.LapCount = (uint)lapsUpDown.Value;

            // Speed to reach
            _CurrentTrack.SpeedToReach = uint.Parse(speedToReachTextBox.Text);

            // Solo objectives
            // With driving gauge?
            _CurrentTrack.WithDrivingGauge = withDrivingGaugeCheckBox.Checked;

            // With cops?
            _CurrentTrack.WithCops = withCopsCheckBox.Checked;
            _CurrentTrack.CopsLevel = (uint)copsLevelTrackBar.Value;
        }

        /// <summary>
        /// Defines replacing information
        /// </summary>
        private void _SetReplacingInformation()
        {
            FileInfo fi = new FileInfo(_CurrentTrack.FileName);
            string fileName = fi.Name;
            string[] splitted = fileName.Split(new[] { _REPLACING_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
            int replacedChallengeIndex = -1;

            if (_ReplacedChallenge != null)
                replacedChallengeIndex = _DefaultChallenges.IndexOf(_ReplacedChallenge);

            string newFileName;

            if (replacedChallengeIndex == -1)
                // Removing replacing information
                newFileName = splitted[0];
            else
                // Creating or updating
                newFileName = string.Concat(splitted[0], _REPLACING_SEPARATOR, replacedChallengeIndex);

            string renamingTarget = fi.DirectoryName + @"\" + newFileName;

            if (!_CurrentTrack.FileName.Equals(renamingTarget))
            {
                File.Move(_CurrentTrack.FileName, renamingTarget);
                _CurrentTrack.FileName = renamingTarget;
            }
        }

        /// <summary>
        /// Converts specified IGE challenges to DFE files
        /// </summary>
        /// <param name="igeFiles"></param>
        private static Couple<int> _ConvertIGEtoDFE(IGE[] igeFiles)
        {
            Couple<int> returnedCount = new Couple<int>(0,0);

            if (igeFiles != null)
            {
                int totalCount = igeFiles.Length;
                int successCount = 0;

                foreach (IGE anotherIGE in igeFiles)
                {
                    try
                    {
                        DFE convertedTrack = anotherIGE;

                        // New file name
                        string tracksFolder = AppConstants.FOLDER_TRACKS;
                        //string newFileName = new FileInfo(convertedTrack.FileName).Name;
                        string targetFileName = string.Concat(tracksFolder, @"\", DFE.FILE_DATA_DFE, File2.GetValidFileName(convertedTrack.TrackName, false)
                                                              /*File2.GetNameFromFilename(newFileName)*/);

                        convertedTrack.SaveAs(targetFileName);
                        successCount++;
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Unable to convert IGE track to DFE.", ex);
                    }
                }

                returnedCount = new Couple<int>(successCount, totalCount);
            }

            return returnedCount;
        }

        /// <summary>
        /// Export specified track items
        /// </summary>
        /// <param name="trackItems"></param>
        /// <param name="packName"></param>
        /// <param name="targetFolder"></param>
        private static void _ExportTracks(IEnumerable<ListViewItem> trackItems, string packName, string targetFolder)
        {
            if (trackItems != null && !string.IsNullOrEmpty(packName) && !string.IsNullOrEmpty(targetFolder) && Directory.Exists(targetFolder))
            {
                // Directories
                string packFolder = string.Concat(targetFolder, @"\", File2.GetValidFileName(packName, true));
                string tracksFolder = string.Concat(packFolder, LibraryConstants.FOLDER_TRACKS);
                string defaultFolder = string.Concat(packFolder, LibraryConstants.FOLDER_DEFAULT);

                Directory.CreateDirectory(packFolder);
                Directory.CreateDirectory(tracksFolder);

                // Libraries
                string frameworkFileName = string.Concat(Tools.WorkingPath, DjeFramework1.LibraryProperties.FILE_LIBRARY);
                string tduLibFileName = string.Concat(Tools.WorkingPath, TDUModdingLibrary.LibraryProperties.FILE_LIBRARY);

                File.Copy(frameworkFileName, string.Concat(packFolder,DjeFramework1.LibraryProperties.FILE_LIBRARY), true);
                File.Copy(tduLibFileName, string.Concat(packFolder,TDUModdingLibrary.LibraryProperties.FILE_LIBRARY), true);

                // EXE files
                string tpFileName = string.Concat(Tools.WorkingPath, LibraryConstants.FOLDER_TRACKS, AppConstants.FILE_EXE_TRACKPACK);

                File.Copy(tpFileName, string.Concat(packFolder, AppConstants.FILE_EXE_TRACKPACK), true);

                // Exported tracks
                foreach(ListViewItem anotherItem in trackItems)
                {
                    DFE customTrack = anotherItem.Tag as DFE;

                    if (customTrack != null)
                    {
                        FileInfo fi = new FileInfo(customTrack.FileName);
                        File.Copy(customTrack.FileName, string.Concat(tracksFolder, @"\", fi.Name), true);
                    }         
                }

                // Default challenges
                File2.CopyDirectory(string.Concat(Tools.WorkingPath, LibraryConstants.FOLDER_XML, LibraryConstants.FOLDER_DEFAULT, LibraryConstants.FOLDER_CHALLENGES),defaultFolder);
            }
        }
        #endregion

        #region Events
        private void TrackPackForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Closure request
            _SearchBoxInstance = null;
            _Instance = null;

            e.Cancel = false;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Settings' button > open Options dialog
            SettingsForm settingsForm = new SettingsForm(SettingsForm.SettingsTabPage.TrackPack);

            settingsForm.ShowDialog(this);
        }

        private void searchToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on Search button
            try
            {
                Cursor = Cursors.WaitCursor;

                SearchBox searchBox = _GetSearchBoxInstance(customTracksListView, _LABEL_CUSTOM_SEARCH);

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

        private void fromTDUGenuineEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on Import > from TDU Editor button
            try
            {
                Cursor = Cursors.WaitCursor;

                EditorTracksDialog dlg = new EditorTracksDialog(true);
                DialogResult dr = dlg.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    Couple<int> convertedCount = _ConvertIGEtoDFE(dlg.SelectedTracks);

                    // Refresh custom tracks
                    _RefreshCustomList();

                    string message = string.Format(_FORMAT_IMPORT_IGE_OK, convertedCount.FirstValue, convertedCount.SecondValue);

                    StatusBarLogManager.ShowEvent(this, message);
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

        private void customTracksListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Another custom track has been selected
            if (customTracksListView.SelectedItems.Count == 1)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    _CurrentTrack = customTracksListView.SelectedItems[0].Tag as DFE;
                    _ReplacedChallenge = null;

                    _RefreshTrackDetails();
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

        private void replacedLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on replaced track link > display genuine track list
            try
            {
                Cursor = Cursors.WaitCursor;

                OriginalTracksDialog dlg = new OriginalTracksDialog(_DefaultChallenges, _HousesResource, true);
                DialogResult dr = dlg.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    int selectedTrackIndex = dlg.SelectedTrackId;

                    if (selectedTrackIndex == -1)
                        // No replacement
                        _ReplacedChallenge = null;
                    else
                        _ReplacedChallenge = _DefaultChallenges[selectedTrackIndex];
                    
                    // Replacement label update
                    _UpdateReplacementLabel(_ReplacedChallenge == null);

                    // Flag
                    _IsChanged = true;  
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

        private void setButton_Click(object sender, EventArgs e)
        {
            // Click on Set button
            if (_CurrentTrack != null && _IsChanged)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    // Replacing
                    _SetReplacingInformation();

                    // Main
                    _SetMainInformation();

                    // Objectives
                    _SetObjectivesInformation();

                    // Rules
                    _SetRulesInformation();

                    // Final save
                    _CurrentTrack.Save();

                    // Flag
                    _IsChanged = false;

                    // Custom list update
                    _RefreshCustomList();

                    StatusBarLogManager.ShowEvent(this, _STATUS_SET_OK);
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

        private void trackTimer_Tick(object sender, EventArgs e)
        {
            // Timer interval reached
            // For each selected custom track, locate replaced name in DFE folder
            foreach (ListViewItem anotherItem in customTracksListView.Items)
            {
                try
                {
                    DFE currentTrack = anotherItem.Tag as DFE;

                    if (currentTrack != null)
                    {
                        FileInfo customTrackInfo = new FileInfo(currentTrack.FileName);
                        DFE replacedChallenge = _GetReplacedChallenge(customTrackInfo.Name);

                        if (replacedChallenge != null)
                        {
                            FileInfo replacedChallengeInfo = new FileInfo(replacedChallenge.FileName);
                            string tduDfeFileName = string.Concat(LibraryConstants.GetSpecialFolder(LibraryConstants.TduSpecialFolder.Data_DFE), replacedChallengeInfo.Name);
                            FileInfo dfeTrackInfo = new FileInfo(tduDfeFileName);

                            if (anotherItem.Checked)
                            {
                                // Checked > synchronization
                                if (dfeTrackInfo.Exists && dfeTrackInfo.LastWriteTime != customTrackInfo.LastWriteTime)
                                {
                                    File.Copy(currentTrack.FileName, tduDfeFileName, true);

                                    string message = string.Format(_FORMAT_TRACK_SYNC_OK, currentTrack.TrackName, replacedChallenge.TrackName);

                                    StatusBarLogManager.ShowEvent(this, message);
                                    SystemSounds.Exclamation.Play();
                                }
                            }
                            /*else
                            {
                                // Unchecked > restoration
                                // Disabled beacause of conflicts
                                if (dfeTrackInfo.Exists && dfeTrackInfo.LastWriteTime != replacedChallengeInfo.LastWriteTime)
                                {
                                    File.Copy(replacedChallenge.FileName, tduDfeFileName, true);

                                    string message = string.Format(_FORMAT_TRACK_RESTORE_OK, replacedChallenge.TrackName);

                                    StatusBarLogManager.ShowEvent(this, message);
                                    SystemSounds.Exclamation.Play();
                                }
                            }*/
                        }
                    }

                }
                catch (Exception ex)
                {
                    Log.Error("A track synchronization issue has occured.", ex);
                }
            }
        }

        private void onOffButton_Click(object sender, EventArgs e)
        {
            // Click on ON/OFF button
            if (trackTimer.Enabled)
            {
                trackTimer.Stop();
                activatorPictureBox.BackColor = Color.FromArgb(255, 128, 128);
            }
            else
            {
                trackTimer.Start();
                activatorPictureBox.BackColor = Color.FromArgb(128, 255, 128);
            }
        }

        /// <summary>
        /// Sets information as changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setInformationChanged(object sender, EventArgs e)
        {
            _IsChanged = true;

            // Misc. fixes: road marking and traffic can't be enabled at the same time
            if (trafficCheckBox.Checked && routeMarkingCheckBox.Checked)
                trafficCheckBox.Checked = routeMarkingCheckBox.Checked = false;

            // Eliminator status
            if (sender == lapsUpDown || sender == withLapsCheckBox)
                eliminatorPanel.Visible = (lapsUpDown.Value == 0 && withLapsCheckBox.Checked);

            // With radar
            if (sender == gameModeComboBox)
                withRadarsCheckBox.Enabled = (gameModeComboBox.Text.Equals(DFE.GameMode.Speed.ToString()));
        }

        private void exploreToolStripButton_Click(object sender, EventArgs e)
        {
            // Clic : opens explorer on tracks folder
            try
            {
                ProcessStartInfo explorerProcess = new ProcessStartInfo(AppConstants.FOLDER_TRACKS);

                Process.Start(explorerProcess);
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }


        private void customTracksListView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // A track is being checked > perform control over replacing
            if (e.NewValue == CheckState.Checked)
            {
                // Does it replace any challenge ?
                ListViewItem item = customTracksListView.Items[e.Index];
                string replaced = item.SubItems[3].Text;

                if (_ITEM_REPLACES_NONE.Equals(replaced))
                {
                    e.NewValue = CheckState.Unchecked;
                    StatusBarLogManager.ShowEvent(this, _STATUS_ACTIVATE_KO_NO_REPLACEMENT);
                }
                else
                {
                    // Does another track replace the same challenge ?
                    foreach (ListViewItem anotherItem in customTracksListView.Items)
                    {
                        if (anotherItem != item && anotherItem.Checked)
                        {
                            string anotherReplaced = anotherItem.SubItems[3].Text;

                            if (anotherReplaced.Equals(replaced))
                            {
                                e.NewValue = CheckState.Unchecked;
                                StatusBarLogManager.ShowEvent(this, _STATUS_ACTIVATE_KO_ALREADY_REPLACED);
                            }
                        }
                    }
                }
            }
        }

        private void deleteTrackToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on 'delete button'
            if (customTracksListView.SelectedItems.Count == 1
                && MessageBoxes.ShowQuestion(this,_QUESTION_DELETE_TRACK, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;

                try
                {
                    ListViewItem selectedItem = customTracksListView.SelectedItems[0];
                    DFE selectedTrack = selectedItem.Tag as DFE;

                    if (selectedTrack != null)
                    {
                        File.Delete(selectedTrack.FileName);

                        // Refresh
                        _RefreshCustomList();

                        StatusBarLogManager.ShowEvent(this, _STATUS_DELETE_OK);
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

        private void checkpointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on Import Checkpoints...
            if (customTracksListView.SelectedItems.Count == 1)
            {
                Cursor = Cursors.WaitCursor;

                try
                {
                    // Select original (DFE challenge only)
                    DFE originalChallenge = customTracksListView.SelectedItems[0].Tag as DFE;

                    if (originalChallenge != null)
                    {
                        // Select IGE/DFE file(s) to append
                        bool isNotFinished = true;
                        string profileFolder = string.Concat(LibraryConstants.GetSpecialFolder(LibraryConstants.TduSpecialFolder.Savegame), @"\", Program.ApplicationSettings.PlayerProfile);
                        string igeFolder = string.Concat(profileFolder, LibraryConstants.FOLDER_IGE);

                        openFileDialog.Title = _TITLE_PICK_TRACK_TO_MERGE;
                        openFileDialog.Filter = GuiConstants.FILTER_DFE_IGE_FILES;
                        openFileDialog.InitialDirectory = igeFolder;

                        while (isNotFinished)
                        {
                            // File selection
                            DialogResult dr = openFileDialog.ShowDialog(this);

                            if (dr == DialogResult.OK)
                            {
                                // Appends waypoints to end of original challenge
                                DFE additionalTrack = TduFile.GetFile(openFileDialog.FileName) as DFE;

                                if (additionalTrack == null)
                                    throw new Exception("Unable to load custom track: " + openFileDialog.FileName);

                                // Creating new Waypoint data
                                originalChallenge.MergeCheckpoints(additionalTrack);
                            }

                            // Continue ?
                            dr = MessageBoxes.ShowQuestion(this, _QUESTION_CONTINUE_MERGE, MessageBoxButtons.YesNo);

                            if (dr == DialogResult.No)
                                isNotFinished = false;
                        }

                        // End, saving DFE track
                        originalChallenge.Save();

                        // Refresh
                        _RefreshCustomList();

                        StatusBarLogManager.ShowEvent(this, _STATUS_MERGE_OK);
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

        private void eliminatorPanel_VisibleChanged(object sender, EventArgs e)
        {
            // Eliminator panel visibility has changed > update driving gauge appearance
            withDrivingGaugeCheckBox.Enabled = !eliminatorPanel.Visible;
        }

        private void originalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on Display original challenges button
            try
            {
                Cursor = Cursors.WaitCursor;

                OriginalTracksDialog dlg = new OriginalTracksDialog(_DefaultChallenges, _HousesResource, false);

                dlg.ShowDialog(this);
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

        private void tDUEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on Display editor challenges button
            try
            {
                Cursor = Cursors.WaitCursor;

                EditorTracksDialog dlg = new EditorTracksDialog(false);

                // Non modal window to always keep an eye
                dlg.Show(this);
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

        private void exportToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on Export button...
            Collection<ListViewItem> checkedItems = ListView2.GetCheckedItems(customTracksListView);

            Cursor = Cursors.WaitCursor;

            try
            {
                if (checkedItems.Count == 0)
                    MessageBoxes.ShowWarning(this, _WARNING_CHECK_EXPORT);
                else
                {
                    // All tracks must have replacing information
                    foreach(ListViewItem anotherItem in checkedItems)
                    {
                        DFE currentTrack = anotherItem.Tag as DFE;

                        if (currentTrack != null)
                        {
                            FileInfo fi = new FileInfo(currentTrack.FileName);
                            DFE replacedChallenge = _GetReplacedChallenge(fi.Name);

                            if (replacedChallenge == null)
                            {
                                throw new Exception(_ERROR_REPLACE_EXPORT);
                            }
                        }
                    }


                    // Asks for track name
                    PromptBox pBox = new PromptBox(_TITLE_EXPORT, _MSG_EXPORT, _NAME_EXPORT);
                    DialogResult dr = pBox.ShowDialog(this);

                    if (dr == DialogResult.OK)
                    {
                        // Displays export target folder
                        folderBrowserDialog.Description = _MSG_PATH_EXPORT;
                        dr = folderBrowserDialog.ShowDialog(this);

                        if (dr == DialogResult.OK)
                        {
                            _ExportTracks(checkedItems, pBox.ReturnValue, folderBrowserDialog.SelectedPath);

                            string msg = string.Format(_FORMAT_EXPORT_OK, checkedItems.Count);

                            StatusBarLogManager.ShowEvent(this, msg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this,ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion
    }
}
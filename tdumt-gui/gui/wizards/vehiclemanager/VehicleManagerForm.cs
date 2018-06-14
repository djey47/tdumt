using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Dialogs.Search;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Types.Collections;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.fileformats.binaries;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.support.constants;
using TDUModdingLibrary.support.editing;
using TDUModdingTools.common;
using TDUModdingTools.gui.wizards.vehiclemanager.cameraIK;
using TDUModdingTools.gui.wizards.vehiclemanager.common;

namespace TDUModdingTools.gui.wizards.vehiclemanager
{
    /// <summary>
    /// Main form for Vehicle Manager
    /// EVO_56
    /// </summary>
    public partial class VehicleManagerForm : Form
    {
        #region Constants
        /// <summary>
        /// Error when loading database
        /// </summary>
        private const string _ERROR_LOADING_DATABASE = "Unable to load TDU database.";

        /// <summary>
        /// Error when loading cameras
        /// </summary>
        private const string _ERROR_LOADING_CAMERAS = "Unable to load TDU cameras.";

        /// <summary>
        /// Error when loading vehicle
        /// </summary>
        private const string _ERROR_LOADING_VEHICLE = "Unable to load requested vehicle data.";

        /// <summary>
        /// Conflict error
        /// </summary>
        private const string _ERROR_LOADING_VEHICLE_CONFLICT = "Please close all other editors before modifying slots.";

        /// <summary>
        /// Error message when dropping of an invalid BNK file
        /// </summary>
        private const string _ERROR_DROP_BNK_INVALID = "Dropped BNK file is invalid.";

        /// <summary>
        /// Question when about to loose changes
        /// </summary>
        private const string _QUESTION_LOOSE_CHANGES =
            "Current vehicle has been modified but not saved yet.\r\nAre you sure ?";

        /// <summary>
        /// Error message upon save error
        /// </summary>
        private const string _ERROR_SAVING = "Unable to save database changes.";

        /// <summary>
        /// Status message when loading OK
        /// </summary>
        private const string _STATUS_LOADING_DATABASE_SUCCESS = "Database was loaded succesfully. Please Edit (or double-click) a vehicle slot in top list.";

        /// <summary>
        /// Status message when setting BNK is OK
        /// </summary>
        private const string _STATUS_SETTING_BNK_OK = "BNK file was set succesfully.";

        /// <summary>
        /// Status message when vehicle loading OK
        /// </summary>
        private const string _STATUS_VEHICLE_READY = "Selected vehicle is ready for modifications.";

        /// <summary>
        /// Status message when saving is OK
        /// </summary>
        private const string _STATUS_SAVING_SUCCESS = "Vehicle was saved succesfully.";

        /// <summary>
        /// Status message while selected car is loading
        /// </summary>
        private const string _STATUS_LOADING_VEHICLE = "Loading vehicle now. Please wait...";

        /// <summary>
        /// Status message when vehicle removal process ended without errors
        /// </summary>
        private const string _STATUS_REMOVING_VEHICLE_FROM_DEALER_OK =
            "Selected slot was cleared succesfully.";
        
        /// <summary>
        /// Status message when vehicle setting process ended without errors
        /// </summary>
        private const string _STATUS_SETTING_VEHICLE_ON_DEALER_OK =
            "Current vehicle was set on selected slot succesfully.";

        /// <summary>
        /// Status message when vehicle stting down process ended without errors
        /// </summary>
        private const string _STATUS_DOWN_VEHICLE_FROM_DEALER_OK =
            "Current vehicle was put on next slot succesfully.";
        
        /// <summary>
        /// Status message when vehicle stting up process ended without errors
        /// </summary>
        private const string _STATUS_UP_VEHICLE_FROM_DEALER_OK =
            "Current vehicle was put on previous slot succesfully.";

        /// <summary>
        /// Status message when tuning brand setting process ended without errors
        /// </summary>
        private const string _STATUS_CHANGING_TUNING_BRAND_OK = "Tuning brand was changed succesfully.";

        /// <summary>
        /// Status message when tuning spot setting process ended without errors
        /// </summary>
        private const string _STATUS_CHANGING_TUNING_SPOT_OK = "Tuning spot was changed succesfully.";

        /// <summary>
        /// Status message when drive setting process ended without errors
        /// </summary>
        private const string _STATUS_CHANGING_DRIVE_OK = "Transmission options were changed succesfully.";

        /// <summary>
        /// Status message when gearbox setting process ended without errors
        /// </summary>
        private const string _STATUS_CHANGING_BOX_OK = "Gearbox options were changed succesfully.";

        /// <summary>
        /// Status message when engine location setting process ended without errors
        /// </summary>
        private const string _STATUS_CHANGING_ENGINE_OK = "Engine settings were changed succesfully.";

        /// <summary>
        /// Status message when bodywork setting process ended without errors
        /// </summary>
        private const string _STATUS_CHANGING_BODYWORK_OK = "Bodywork settings were changed succesfully.";

        /// <summary>
        /// Search box message
        /// </summary>
        private const string _MESSAGE_SEARCH_VEHICLE = "Which vehicle are you looking for?";

        /// <summary>
        /// Format string for vehicle name label
        /// </summary>
        private const string _FORMAT_VEHICLE_NAME = "{0} / {1}";

        /// <summary>
        /// 'None' slot label
        /// </summary>
        private const string _LABEL_SLOT_NONE = "None";

        /// <summary>
        /// Prime ' symbol
        /// </summary>
        private const string _SYMBOL_PRIME = "'";
        #endregion

        #region Enums
        /// <summary>
        /// All image indexes in ImageList
        /// </summary>
        private enum _ImageIndex
        {
            Car = 0,
            Bike = 1,
            NonModdableCar = 2,
            NonModdableBike = 3
        }
        #endregion

        #region Properties
        /// <summary>
        /// Returns the form's instance
        /// </summary>
        internal static VehicleManagerForm Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new VehicleManagerForm();

                return _Instance;
            }
        }
        private static VehicleManagerForm _Instance;

        /// <summary>
        /// Reference of vehicle which is currently modified
        /// </summary>
        internal string CurrentVehicle
        {
            get { return _CurrentVehicle; }
        }
        private string _CurrentVehicle;

        /// <summary>
        /// Indicates if vehicle has been modified
        /// </summary>
        private bool _IsModified
        {
            get { return (_IsDatabaseModified || _IsCameraModified); }
        }
        #endregion

        #region Members - common part
        /// <summary>
        /// Indicates if current vehicle database has been modified
        /// </summary>
        private bool _IsDatabaseModified;

        /// <summary>
        /// Indicates if current vehicle camera has been modified
        /// </summary>
        private bool _IsCameraModified;

        /// <summary>
        /// Indicates if original vehicle names should be swhown
        /// </summary>
        private bool _IsShowOriginalNames = true;

        /// <summary>
        /// Current edit task for DB physics file
        /// </summary>
        private EditHelper.Task _CurrentPhysicsTask;

        /// <summary>
        /// Current edit task for DB physics file (resources)
        /// </summary>
        private EditHelper.Task _CurrentPhysicsResourceTask;

        /// <summary>
        /// Current edit task for DB car shops file
        /// </summary>
        private EditHelper.Task _CurrentCarShopsTask;

        /// <summary>
        /// Current edit task for DB car shops file (resources)
        /// </summary>
        private EditHelper.Task _CurrentCarShopsResourceTask;

        /// <summary>
        /// Current edit task for DB car rims file
        /// </summary>
        private EditHelper.Task _CurrentCarRimsTask;

        /// <summary>
        /// Current edit task for DB rims file
        /// </summary>
        private EditHelper.Task _CurrentRimsTask;

        /// <summary>
        /// Current edit task for DB rims file (resources)
        /// </summary>
        private EditHelper.Task _CurrentRimsResourceTask;

        /// <summary>
        /// Current edit task for DB brands file
        /// </summary>
        private EditHelper.Task _CurrentBrandsTask;

        /// <summary>
        /// Current edit task for DB brands file (resources)
        /// </summary>
        private EditHelper.Task _CurrentBrandsResourceTask;

        /// <summary>
        /// Current edit task for DB car packs file
        /// </summary>
        private EditHelper.Task _CurrentCarPacksTask;

        /// <summary>
        /// Current edit task for DB colors file (resources)
        /// </summary>
        private EditHelper.Task _CurrentCarColorsResourceTask;

        /// <summary>
        /// Current edit task for DB colors file
        /// </summary>
        private EditHelper.Task _CurrentCarColorsTask;

        /// <summary>
        /// Current edit task for DB interior file
        /// </summary>
        private EditHelper.Task _CurrentInteriorTask;

        /// <summary>
        /// Current edit task for DB interior file (resources)
        /// </summary>
        private EditHelper.Task _CurrentInteriorResourceTask;       

        /// <summary>
        /// Current slot name
        /// </summary>
        private string _CurrentSlotName;

        /// <summary>
        /// Instance of search box
        /// </summary>
        private static SearchBox _SearchBoxInstance;
        #endregion

        #region Members - database access
        /// <summary>
        /// Database table: CarRims
        /// </summary>
        private DB _CarRimsTable;
        
        /// <summary>
        /// Database table: Rims
        /// </summary>
        private DB _RimsTable;

        /// <summary>
        /// Database resource: Rims
        /// </summary>
        private DBResource _RimsResource;

        /// <summary>
        /// Database table: CarPhysicsData
        /// </summary>
        private DB _PhysicsTable;

        /// <summary>
        /// Database resource: CarPhysicsData
        /// </summary>
        private DBResource _PhysicsResource;

        /// <summary>
        /// Database table: Brands
        /// </summary>
        private DB _BrandsTable;

        /// <summary>
        /// Database resource: Brands
        /// </summary>
        private DBResource _BrandsResource;

        /// <summary>
        /// Database table: CarShops
        /// </summary>
        private DB _CarShopsTable;

        /// <summary>
        /// Database resource: CarShops
        /// </summary>
        private DBResource _CarShopsResource;

        /// <summary>
        /// Database table: CarPacks
        /// </summary>
        private DB _CarPacksTable;

        /// <summary>
        /// Database table: CarColors
        /// </summary>
        private DB _CarColorsTable;

        /// <summary>
        /// Database resource: CarColors
        /// </summary>
        private DBResource _CarColorsResource;

        /// <summary>
        /// Database table: Interior
        /// </summary>
        private DB _InteriorTable;

        /// <summary>
        /// Database resource: Interior
        /// </summary>
        private DBResource _InteriorResource;
        #endregion

        #region Members - camera
        /// <summary>
        /// Camera information
        /// </summary>
        private Cameras _CameraData;
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public VehicleManagerForm()
        {
            InitializeComponent();

            // Vehicle slots reference
            VehicleSlotsHelper.InitReference(AppConstants.FOLDER_XML);

            try
            {
                _LoadReferenceCameras();
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, new Exception(_ERROR_LOADING_CAMERAS, ex));
            }

            _InitializeContents();

            try
            {
                _LoadDatabase();
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, new Exception(_ERROR_LOADING_DATABASE, ex));
            }

            // Getting setting for list mod
            bool originalMode = bool.Parse(Program.ApplicationSettings.VehicleManagerOriginalDisplayMode);

            _IsShowOriginalNames = !originalMode;

            if (originalMode)
                originalNamesRadioButton.Checked = true;
            else
                moddedNamesRadioButton.Checked = true;
        }

        #region Private methods
        /// <summary>
        /// Defines window's contents
        /// </summary>
        private void _InitializeContents()
        {
            StatusBarLogManager.AddNewLog(this, toolStripStatusLabel);

            // Installer
            extModelBox.AllowDrop = intModelBox.AllowDrop
                                    = rimsFrontBox.AllowDrop 
                                    = rimsRearBox.AllowDrop
                                    = gaugesLowBox.AllowDrop 
                                    = gaugesHighBox.AllowDrop
                                    = soundBox.AllowDrop
                                    = true;

            // Camera-IK tab
            _InitializeCameraIKContents();

            // Datasheet: brand logo
            string defaultImageName = _GetBrandLogoName(SharedConstants.DEFAULT_REF_BRANDS_DB_VAL);
            Image defaultImage = Image.FromFile(defaultImageName);

            brandPictureBox.InitialImage = brandPictureBox.ErrorImage = brandPictureBox.Image =
                defaultImage;

            // Lock
            vehicleEditTabControl.Enabled = false;
        }

        /// <summary>
        /// Loads database and reference data
        /// </summary>
        private void _LoadDatabase()
        {
            Cursor = Cursors.WaitCursor;

            // colors
            ColorsHelper.InitReference(AppConstants.FOLDER_XML);

            StatusBarLogManager.ShowEvent(this,_STATUS_LOADING_DATABASE_SUCCESS);

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Loads camera.bin data
        /// </summary>
        private void _LoadReferenceCameras()
        {
            Cursor = Cursors.WaitCursor;

            // Parsing ref XML file and writing to reference
            VehicleSlotsHelper.InitReference(AppConstants.FOLDER_XML);

            // View selector dialog
            _CamViewSelectorDialog = new CamSelectorDialog(VehicleSlotsHelper.DefaultCameras);

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Updates vehicle list according to slots reference
        /// </summary>
        private void _RefreshVehicleList()
        {
            Cursor = Cursors.WaitCursor;

            vehicleListView.Items.Clear();
            vehicleListView.Groups.Clear();

            // Taking list mode into account (original/modded names)
            Dictionary<string, string> originalNamesByModdedNames = null;
            SortedStringCollection vehicleNames = new SortedStringCollection();

            if (_IsShowOriginalNames)
            {
                foreach (string vehicleName in VehicleSlotsHelper.SlotReference.Keys)
                    vehicleNames.Add(vehicleName);
            }
            else
            {
                // Loading database for read-only
                DB.Culture currentCulture = Program.ApplicationSettings.GetCurrentCulture();

                    // Physics
                TduFile[] loadedFiles = DatabaseHelper.LoadTopicForReadOnly(DB.Topic.CarPhysicsData, currentCulture);
                DB physics = loadedFiles[0] as DB;
                DBResource rPhysics = loadedFiles[1] as DBResource;

                    // Brands
                loadedFiles = DatabaseHelper.LoadTopicForReadOnly(DB.Topic.Brands, currentCulture);

                DB brands = loadedFiles[0] as DB;
                DBResource rBrands = loadedFiles[1] as DBResource;

                originalNamesByModdedNames = new Dictionary<string, string>();

                foreach (KeyValuePair<string, string> anotherPair in VehicleSlotsHelper.SlotReference)
                {
                    // Brand name
                    string brandName =
                        NamesHelper.GetVehicleBrandName(anotherPair.Value, physics, brands, rBrands);
                    // Vehicle name
                    string vehicleName =
                        NamesHelper.GetVehicleFullName(anotherPair.Value, false, physics, rPhysics,
                                                         brands, rBrands);
                    string moddedName = (brandName.ToUpper() + " " + vehicleName).Trim();

                    // To maintain 2 identical names
                    while (originalNamesByModdedNames.ContainsKey(moddedName))
                        moddedName += _SYMBOL_PRIME;

                    originalNamesByModdedNames.Add(moddedName, anotherPair.Key);
                    vehicleNames.Add(moddedName);
                }
            }

            // Reference browsing
            foreach (string vehicleName in vehicleNames)
            {
                ListViewItem li = new ListViewItem(vehicleName);

                // Group by brand (first word in fact)
                string currentBrand = vehicleName.Split(' ')[0];
                ListViewGroup currentGroup = new ListViewGroup(currentBrand, currentBrand);

                if (!vehicleListView.Groups.Contains(currentGroup))
                    vehicleListView.Groups.Add(currentGroup);

                li.Group = vehicleListView.Groups[currentBrand];

                // Computing vehicle name
                string slotName = null;

                if (_IsShowOriginalNames)
                    slotName = vehicleName;
                else if (originalNamesByModdedNames != null && originalNamesByModdedNames.ContainsKey(vehicleName))
                    slotName = originalNamesByModdedNames[vehicleName];

                if (slotName != null && VehicleSlotsHelper.SlotReference.ContainsKey(slotName))
                {
                    string vehicleRef = VehicleSlotsHelper.SlotReference[slotName];

                    // Tag
                    li.Tag = vehicleRef;

                    VehicleSlotsHelper.VehicleInfo currentInfo = VehicleSlotsHelper.VehicleInformation[vehicleRef];

                    // Non-moddable vehicles are faded
                    // Image depending on car or bike type
                    if (currentInfo.isBike)
                    {
                        if (currentInfo.isModdable)
                            li.ImageIndex = (int)_ImageIndex.Bike;
                        else
                            li.ImageIndex = (int)_ImageIndex.NonModdableBike;
                    }
                    else
                    {
                        if (currentInfo.isModdable)
                            li.ImageIndex = (int) _ImageIndex.Car;
                        else
                            li.ImageIndex = (int)_ImageIndex.NonModdableCar;
                    }
                            
                    vehicleListView.Items.Add(li);
                }
            }

            Cursor = Cursors.Default;
        }
        
        /// <summary>
        /// Loads vehicle data from database and reference
        /// </summary>
        /// <param name="slotRef">Reference of vehicle slot to load</param>
        private void _LoadVehicleData(string slotRef)
        {
            if (string.IsNullOrEmpty(slotRef))
                return;

            Cursor = Cursors.WaitCursor;

            try
            {
                StatusBarLogManager.ShowEvent(this, _STATUS_LOADING_VEHICLE);

                // Cleaning tasks
                _ClearCurrentTasks();

                _CurrentVehicle = slotRef;
                _CurrentSlotName = VehicleSlotsHelper.SlotReferenceReverse[slotRef];

                // Loading database
                DB.Culture currentCulture = Program.ApplicationSettings.GetCurrentCulture();
                EditHelper.Task[] currentTasks;
                string dbBnkFile = Program.ApplicationSettings.TduMainFolder + LibraryConstants.FOLDER_DB + DB.GetBNKFileName(DB.Culture.Global);
                BNK bnkFile = TduFile.GetFile(dbBnkFile) as BNK;
                string resourceBnkFile = Program.ApplicationSettings.TduMainFolder + LibraryConstants.FOLDER_DB + DB.GetBNKFileName(currentCulture);
                BNK rBnkFile = TduFile.GetFile(resourceBnkFile) as BNK;
                
                // 1.CarRims
                TduFile[] currentFiles = DatabaseHelper.LoadTopicForEdit(DB.Topic.CarRims, DB.Culture.Global, bnkFile, rBnkFile, out currentTasks);

                _CurrentCarRimsTask = currentTasks[0];
                _CarRimsTable = currentFiles[0] as DB;

                // 2.Rims
                currentFiles = DatabaseHelper.LoadTopicForEdit(DB.Topic.Rims, currentCulture, bnkFile, rBnkFile, out currentTasks);
                _CurrentRimsTask = currentTasks[0];
                _CurrentRimsResourceTask = currentTasks[1];
                _RimsTable = currentFiles[0] as DB;
                _RimsResource = currentFiles[1] as DBResource;

                // 3.CarPhysicsData
                currentFiles = DatabaseHelper.LoadTopicForEdit(DB.Topic.CarPhysicsData, currentCulture, bnkFile, rBnkFile, out currentTasks);
                _CurrentPhysicsTask = currentTasks[0];
                _CurrentPhysicsResourceTask = currentTasks[1];
                _PhysicsTable = currentFiles[0] as DB;
                _PhysicsResource = currentFiles[1] as DBResource;

                // 4.Brands
                currentFiles = DatabaseHelper.LoadTopicForEdit(DB.Topic.Brands, currentCulture, bnkFile, rBnkFile, out currentTasks);
                _CurrentBrandsTask = currentTasks[0];
                _CurrentBrandsResourceTask = currentTasks[1];
                _BrandsTable = currentFiles[0] as DB;
                _BrandsResource = currentFiles[1] as DBResource;

                // 5.CarShops
                currentFiles = DatabaseHelper.LoadTopicForEdit(DB.Topic.CarShops, currentCulture, bnkFile, rBnkFile, out currentTasks);
                _CurrentCarShopsTask = currentTasks[0];
                _CurrentCarShopsResourceTask = currentTasks[1];
                _CarShopsTable = currentFiles[0] as DB;
                _CarShopsResource = currentFiles[1] as DBResource;

                // 6.CarPacks
                currentFiles = DatabaseHelper.LoadTopicForEdit(DB.Topic.CarPacks, DB.Culture.Global, bnkFile, rBnkFile, out currentTasks);
                _CurrentCarPacksTask = currentTasks[0];
                _CarPacksTable = currentFiles[0] as DB;

                // 7.Colors
                currentFiles = DatabaseHelper.LoadTopicForEdit(DB.Topic.CarColors, currentCulture, bnkFile, rBnkFile, out currentTasks);
                _CurrentCarColorsTask = currentTasks[0];
                _CurrentCarColorsResourceTask = currentTasks[1];
                _CarColorsTable = currentFiles[0] as DB;
                _CarColorsResource = currentFiles[1] as DBResource;

                // 8.Interior
                currentFiles = DatabaseHelper.LoadTopicForEdit(DB.Topic.Interior, currentCulture, bnkFile, rBnkFile, out currentTasks);
                _CurrentInteriorTask = currentTasks[0];
                _CurrentInteriorResourceTask = currentTasks[1];
                _InteriorTable = currentFiles[0] as DB;
                _InteriorResource = currentFiles[1] as DBResource;

                // 9.Colors id reference
                ColorsHelper.InitIdReference(_CarColorsResource, _InteriorResource);

                // 10.Cameras
                string camBinFile = LibraryConstants.GetSpecialFile(LibraryConstants.TduSpecialFile.CamerasBin);

                _CameraData = TduFile.GetFile(camBinFile) as Cameras;

                /* GUI */
                // Install Tab
                _InitializeInstallContents();

                // Camera-IK Tab
                _RefreshCameraIKContents();

                // Datasheet tab
                _InitializeDatasheetContents();

                // Dealers tab
                _InitializeDealersContents();

                // Tuner tab
                _InitializeTunerContents();

                // Physics tab
                _InitializePhysicsContents();

                // Colors tab
                _InitializeColorsContents();

                // Modification flags
                _IsDatabaseModified = false;
                _IsCameraModified = false;

                // Clearing state vars
                _CurrentAvailabilitySpot = null;
                _CurrentTuningBrand = null;

                // Vehicle name display
                _UpdateSlotAndModName();

                // Tabs are enabled
                vehicleEditTabControl.Enabled = true;

                StatusBarLogManager.ShowEvent(this, _STATUS_VEHICLE_READY);
            }
            catch (Exception ex)
            {
                // All tasks must be cleaned
                _ClearCurrentTasks();

                // Processing special error messages
                if (EditHelper.ERROR_CODE_TASK_EXISTS.Equals(ex.Message))
                    MessageBoxes.ShowError(this, new Exception(_ERROR_LOADING_VEHICLE_CONFLICT, ex));   
                else
                    MessageBoxes.ShowError(this, new Exception(_ERROR_LOADING_VEHICLE, ex));   

                StatusBarLogManager.ShowEvent(this, "");
            }

            Cursor = Cursors.Default;      
        }

        /// <summary>
        /// Updates label for slot and vehicle name
        /// </summary>
        private void _UpdateSlotAndModName()
        {
            string moddedName = modelNameLabel.Text.Replace("\r\n", " ");

            vehicleLabel.Text = string.Format(_FORMAT_VEHICLE_NAME, _CurrentSlotName, moddedName);
        }

        /// <summary>
        /// Ensures all vehicle manager related tasks will be deleted
        /// </summary>
        private void _ClearCurrentTasks()
        {
            EditHelper.Instance.RemoveTask(_CurrentCarShopsTask);
            EditHelper.Instance.RemoveTask(_CurrentCarShopsResourceTask);
            EditHelper.Instance.RemoveTask(_CurrentPhysicsTask);
            EditHelper.Instance.RemoveTask(_CurrentPhysicsResourceTask);
            EditHelper.Instance.RemoveTask(_CurrentBrandsTask);
            EditHelper.Instance.RemoveTask(_CurrentBrandsResourceTask);
            EditHelper.Instance.RemoveTask(_CurrentCarRimsTask);
            EditHelper.Instance.RemoveTask(_CurrentRimsTask);
            EditHelper.Instance.RemoveTask(_CurrentRimsResourceTask);
            EditHelper.Instance.RemoveTask(_CurrentCarPacksTask);
            EditHelper.Instance.RemoveTask(_CurrentCarColorsTask);
            EditHelper.Instance.RemoveTask(_CurrentCarColorsResourceTask);
            EditHelper.Instance.RemoveTask(_CurrentInteriorTask);
            EditHelper.Instance.RemoveTask(_CurrentInteriorResourceTask);

            // BUG_: modify flag
            _IsDatabaseModified = false;
        }

        /// <summary>
        /// Asks for confirmation before losing current changes
        /// </summary>
        /// <returns>true if user wants to continue, else false</returns>
        private bool _ManagePendingChanges()
        {
            bool returnedResult = true;

            if (_IsModified)
            {
                DialogResult dr = MessageBoxes.ShowQuestion(this, _QUESTION_LOOSE_CHANGES, MessageBoxButtons.YesNo);

                returnedResult = (dr == DialogResult.Yes);
            }

            return returnedResult;
        }
        
        /// <summary>
        /// Switches vehicle list mode
        /// </summary>
        /// <param name="isOriginalMode"></param>
        private void _ChangeVehicleListMode(bool isOriginalMode)
        {
            if (isOriginalMode != _IsShowOriginalNames)
            {
                _IsShowOriginalNames = isOriginalMode;

                // Setting update
                Program.ApplicationSettings.VehicleManagerOriginalDisplayMode = _IsShowOriginalNames.ToString();
                Program.ApplicationSettings.Save();

                // Refreshing list
                try
                {
                    _RefreshVehicleList();
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
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
        private void VehicleManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Closing request
            _SearchBoxInstance = null;

            e.Cancel = true;

            if (_ManagePendingChanges())
            {
                _ClearCurrentTasks();
                vehicleEditTabControl.Enabled = false;
                vehicleEditTabControl.SelectedIndex = 0;
                vehicleLabel.Text = _LABEL_SLOT_NONE;

                Hide();
            }
        }

        private void changeToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on 'Edit' button
            if (vehicleListView.SelectedItems.Count == 1 && _ManagePendingChanges())
            {
                ListViewItem selectedItem = vehicleListView.SelectedItems[0];

                _LoadVehicleData(selectedItem.Tag as string);
            }
        }

        private void searchToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on 'Search' button
            SearchBox searchBox = _GetSearchBoxInstance(vehicleListView, _MESSAGE_SEARCH_VEHICLE);

            searchBox.Show(this);
        }

        private void vehicleListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Double click on vehicle list
            changeToolStripButton_Click(sender, e);
        }

        private void VehicleManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Window has been closed
            _SearchBoxInstance = null;
            _ClearCurrentTasks();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // Click on 'Save' button
            if (_IsModified)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    if (_IsDatabaseModified)
                    {
                        // 1. Saving database files
                        _PhysicsTable.Save();
                        _CarShopsTable.Save();
                        _CarPacksTable.Save();
                        _CarColorsTable.Save();
                        _InteriorTable.Save();

                        // 2. Applying edit tasks
                        EditHelper.Instance.ApplyChanges(_CurrentPhysicsTask);
                        EditHelper.Instance.ApplyChanges(_CurrentCarShopsTask);
                        EditHelper.Instance.ApplyChanges(_CurrentCarPacksTask);
                        EditHelper.Instance.ApplyChanges(_CurrentCarColorsTask);
                        EditHelper.Instance.ApplyChanges(_CurrentInteriorTask);

                        _IsDatabaseModified = false;
                    }

                    if (_IsCameraModified)
                    {
                        // 3. Cameras
                        _CameraData.Save();

                        _IsCameraModified = false;
                    }

                    StatusBarLogManager.ShowEvent(this, _STATUS_SAVING_SUCCESS);
                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, new Exception(_ERROR_SAVING, ex));
                }
            }
        }

        private void originalNamesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Original names un/checked
            _ChangeVehicleListMode(originalNamesRadioButton.Checked);
        }

        private void moddedNamesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Modded names un/checked
            _ChangeVehicleListMode(!moddedNamesRadioButton.Checked);
        }
        #endregion
    }
}
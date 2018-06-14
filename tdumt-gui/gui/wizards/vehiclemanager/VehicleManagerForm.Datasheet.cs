using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Types.Collections;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.fileformats.database.util;
using TDUModdingLibrary.support.constants;
using TDUModdingTools.common;
using TDUModdingTools.gui.common;
using TDUModdingTools.gui.wizards.vehiclemanager.common;

namespace TDUModdingTools.gui.wizards.vehiclemanager
{
    /// <summary>
    /// VehicleManagerForm - Datasheet part
    /// </summary>
    partial class VehicleManagerForm
    {
        #region Constants
        /// <summary>
        /// Diameter symbol
        /// </summary>
        private const char _SYMBOL_DIAMETER = 'ø';

        /// <summary>
        /// Format string for model name
        /// </summary>
        private const string _FORMAT_MODEL_NAME = "{0}\r\n{1}";

        /// <summary>
        /// Format string for brake characteristics
        /// </summary>
        private static readonly string _FORMAT_BRAKES_LABEL = "{0} - " + _SYMBOL_DIAMETER + "{1}";

        /// <summary>
        /// Format string for brand name
        /// </summary>
        private const string _FORMAT_BRAND_NAME = "{0} [{1}]";

        /// <summary>
        /// Message to display in brands browser
        /// </summary>
        private const string _MESSAGE_BROWSE_BRANDS = "Please select new manufacturer for this vehicle.";

        /// <summary>
        /// Message to display in names browser
        /// </summary>
        private const string _MESSAGE_BROWSE_PHYSICS_NAMES = "Please select new name.";

        /// <summary>
        /// Message to display in engine types browser
        /// </summary>
        private const string _MESSAGE_BROWSE_ENGINE_TYPES = "Please select new engine type for this vehicle.";

        /// <summary>
        /// Message to display in tires types browser
        /// </summary>
        private const string _MESSAGE_BROWSE_TIRES = "Please select new tires for this vehicle.";

        /// <summary>
        /// Message to display in front brakes browser
        /// </summary>
        private const string _MESSAGE_BROWSE_FRONT_BRAKES = "Please select new front brakes characteristics for this vehicle.";
        
        /// <summary>
        /// Message to display in rear brakes browser
        /// </summary>
        private const string _MESSAGE_BROWSE_REAR_BRAKES = "Please select new rear brakes characteristics for this vehicle.";

        /// <summary>
        /// Message to display in displacement prompt box
        /// </summary>
        private const string _MESSAGE_ENTER_DISPLACEMENT = "Please enter new displacement, in cubic centimeters.";

        /// <summary>
        ///  Message to display in front brake diameter prompt box
        /// </summary>
        private const string _MESSAGE_ENTER_FRONT_DIAMETER = "Please enter front brakes diameter, in millimeters.";

        /// <summary>
        ///  Message to display in rear brake diameter prompt box
        /// </summary>
        private const string _MESSAGE_ENTER_REAR_DIAMETER = "Please enter rear brakes diameter, in millimeters.";
        
        /// <summary>
        /// Status message when manufacturer change process ended without errors
        /// </summary>
        private const string _STATUS_CHANGING_MANUFACTURER_OK = "Manufacturer was changed succesfully.";

        /// <summary>
        /// Status message when displacement change process ended without errors
        /// </summary>
        private const string _STATUS_CHANGING_DISPLACEMENT_OK = "Displacement was changed succesfully.";
        
        /// <summary>
        /// Status message when brake diameter change process ended without errors
        /// </summary>
        private const string _STATUS_CHANGING_BRAKE_DIAMETER_OK = "Brake diameter was changed succesfully.";

        /// <summary>
        /// Status message when brake characteristics change process ended without errors
        /// </summary>
        private const string _STATUS_CHANGING_BRAKE_CHAR_OK = "Brake characteristics were changed succesfully.";

        /// <summary>
        /// Status message when engine type change process ended without errors
        /// </summary>
        private const string _STATUS_CHANGING_ENGINE_TYPE_OK = "Engine type was changed succesfully.";

        /// <summary>
        /// Status message when tires change process ended without errors
        /// </summary>
        private const string _STATUS_CHANGING_TIRES_OK = "Tires were changed succesfully.";

        /// <summary>
        /// Status message when name change process ended without errors
        /// </summary>
        private const string _STATUS_CHANGING_RENAMING_OK = "Vehicle name was changed succesfully.";

        /// <summary>
        /// Status message when performance data change process ended without errors
        /// </summary>
        private const string _STATUS_CHANGING_PERF_OK = "Performance information was changed succesfully.";

        /// <summary>
        /// Title for brake diameter prompt box
        /// </summary>
        private const string _TITLE_BRAKES_DIAMETER = "Brakes diameter...";

        /// <summary>
        /// Warning message when max speed has been modified by another tool, above recommended value
        /// </summary>
        private const string _WARN_MODDED_MAX_SPEED =
            "It seems you have max speed modded above regular value.\r\nAs a result, speed limiter feature has been disabled.\r\nTo enable it again, please restore original max speed of current vehicle.";
        #endregion

        #region Members
        /// <summary>
        /// Indicates if current vehicle name uses RealName instead of (Model + Version names)
        /// </summary>
        private bool _IsVehicleWithRealName;

        /// <summary>
        /// Indicates if speed limiter feature is enabled
        /// </summary>
        private bool _IsSpeedLimiterEnabled;
        #endregion

        #region Events
        private void modelChangeButton_Click(object sender, EventArgs e)
        {
            // Click on 'Model>Change' button
            // Displays context menu
            modelChangeButton.ContextMenuStrip.Show(modelChangeButton.PointToScreen(new Point()));
        }

        private void techChangeButton_Click(object sender, EventArgs e)
        {
            // Click on 'Specs>Change' button
            // Displays context menu
            techChangeButton.ContextMenuStrip.Show(techChangeButton.PointToScreen(new Point()));
        }

        private void manufToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Manufacturer' menu item
            try
            {
                bool isModified = _ChangeManufacturer();

                if (isModified)
                {
                    Cursor = Cursors.WaitCursor;

                    // Reloading
                    _InitializeDatasheetContents();

                    // Vehicle names
                    _UpdateSlotAndModName();

                    // Modification flag
                    _IsDatabaseModified = true;

                    StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_MANUFACTURER_OK);

                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void modToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Model' menu item
            try
            {
                bool isModified = _ChangeModelOrVersion(true);

                if (isModified)
                {
                    Cursor = Cursors.WaitCursor;

                    // Reloading
                    _InitializeDatasheetContents();

                    // Vehicle names
                    _UpdateSlotAndModName();

                    // Modification flag
                    _IsDatabaseModified = true;

                    StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_RENAMING_OK);

                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void verToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Model' menu item
            try
            {
                bool isModified = _ChangeModelOrVersion(false);

                if (isModified)
                {
                    Cursor = Cursors.WaitCursor;

                    // Reloading
                    _InitializeDatasheetContents();

                    // Vehicle names
                    _UpdateSlotAndModName();

                    // Modification flag
                    _IsDatabaseModified = true;

                    StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_RENAMING_OK);

                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void displacementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Displacement' menu item
            try
            {
                PromptBox pBox = new PromptBox("Engine displacement...",_MESSAGE_ENTER_DISPLACEMENT, engineDisplacementLabel.Text);
                DialogResult dr = pBox.ShowDialog(this);

                if (dr == DialogResult.OK && pBox.IsValueChanged)
                {
                    Cursor = Cursors.WaitCursor;

                    // Changing displacement
                    string newValue = pBox.ReturnValue;

                    DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, SharedConstants.DISPLACEMENT_PHYSICS_DB_COLUMN, _CurrentVehicle, newValue);

                    // Reloading
                    _InitializeDatasheetContents();

                    // Modification flag
                    _IsDatabaseModified = true;

                    StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_DISPLACEMENT_OK);

                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void diameterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Brakes>Front>Diameter' menu item
            try
            {
                _ChangeBrakesDiameter(true);
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void characteristicsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Click on 'Brakes>Rear>Characteristics' menu item
            try
            {
                _ChangeBrakesCharacteristics(false);
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void diameterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Click on 'Brakes>Rear>Diameter' menu item
            try
            {
                _ChangeBrakesDiameter(false);
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void engineTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Engine type' menu item
            try
            {
                bool isModified = _ChangeEngineType();

                if (isModified)
                {
                    Cursor = Cursors.WaitCursor;

                    // Reloading
                    _InitializeDatasheetContents();

                    // Modification flag
                    _IsDatabaseModified = true;

                    StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_ENGINE_TYPE_OK);

                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void tiresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Tires' menu item
            try
            {
                bool isModified = _ChangeTires();

                if (isModified)
                {
                    Cursor = Cursors.WaitCursor;

                    // Reloading
                    _InitializeDatasheetContents();

                    // Modification flag
                    _IsDatabaseModified = true;

                    StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_TIRES_OK);

                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void characteristicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Brakes>Front>Characteristics' menu item
            try
            {
                _ChangeBrakesCharacteristics(true);
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void setVehiclePerformanceButton_Click(object sender, EventArgs e)
        {
            // Click on 'Set' button (performance datasheet)
            try
            {
                Cursor = Cursors.WaitCursor;

                _ChangePerformanceData();

                // Modification flag
                _IsDatabaseModified = true;

                // Reloading: not needed
                //_InitializeDatasheetContents();

                StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_PERF_OK);
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

        private void kmhSpeedRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Another unit has been selected > update
            _UpdateSpeedLimiter();
        }

        private void physicsDriveRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Wheel drive has changed > state of primacy text box
            physicsPrimacyTextBox.Enabled = physicsDriveAllRadioButton.Checked;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Defines tab contents
        /// </summary>
        private void _InitializeDatasheetContents()
        {
            // Brand name
            string brandName =
                NamesHelper.GetVehicleBrandName(_CurrentVehicle, _PhysicsTable, _BrandsTable, _BrandsResource);

            // Real name : if it's mentioned, other names are not used !
            DB.Cell realNameCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.REAL_NAME_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            if (DatabaseConstants.NOT_AVAILABLE_NAME_PHYSICS_DB_RESID.Equals(realNameCell.value))
                // Model + Version name
                _IsVehicleWithRealName = false;
            else
                // Real name
                _IsVehicleWithRealName = true;

            // Brand logo
            DB.Cell brandRefCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.CAR_BRAND_PHYSICS_DB_COLUMN, 
                                                                    _PhysicsTable, _CurrentVehicle)[0];
            string brandLogoName = _GetBrandLogoName(brandRefCell.value);

            if (File.Exists(brandLogoName))
                brandPictureBox.Image = Image.FromFile(brandLogoName);
            else
                brandPictureBox.Image = brandPictureBox.InitialImage;

            // Vehicle name
            string vehicleName =
                NamesHelper.GetVehicleFullName(_CurrentVehicle, false, _PhysicsTable, _PhysicsResource,
                                                         _BrandsTable, _BrandsResource);
            modelNameLabel.Text = string.Format(_FORMAT_MODEL_NAME, brandName.ToUpper(), vehicleName);

            // Engine type
            DB.Cell currentCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.ENGINE_TYPE_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            engineTypeLabel.Text = DatabaseHelper.GetResourceValueFromCell(currentCell, _PhysicsResource);

            // Displacement
            currentCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.DISPLACEMENT_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            engineDisplacementLabel.Text = currentCell.value;

            // Max power and rpm
            currentCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.POWER_BHP_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            engineMaxPowerLabel.Text = currentCell.value;
            currentCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.POWER_RPM_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            engineRpmPowerLabel.Text = currentCell.value;

            // Max torque and rpm
            currentCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.TORQUE_NM_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            engineMaxTorqueLabel.Text = currentCell.value;
            currentCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.TORQUE_RPM_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            engineRpmTorqueLabel.Text = currentCell.value;

            // Weight
            currentCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.WEIGHT_KG_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            weightLabel.Text = currentCell.value;

            // Tires
            currentCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.TYRES_TYPE_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            tiresTypeLabel.Text = DatabaseHelper.GetResourceValueFromCell(currentCell, _PhysicsResource);

            // Brakes
            currentCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.BRAKES_CHAR_FRONT_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];

            string brakesChar = DatabaseHelper.GetResourceValueFromCell(currentCell, _PhysicsResource);
            
            currentCell = 
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.BRAKES_DIM_FRONT_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            frontBrakesLabel.Text = string.Format(_FORMAT_BRAKES_LABEL, brakesChar, currentCell.value);

            currentCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.BRAKES_CHAR_REAR_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            brakesChar = DatabaseHelper.GetResourceValueFromCell(currentCell, _PhysicsResource);

            currentCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.BRAKES_DIM_REAR_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            rearBrakesLabel.Text = string.Format(_FORMAT_BRAKES_LABEL, brakesChar, currentCell.value);

            // Performance : max speed
            currentCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.TOP_SPEED_KMH_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];

            ushort slotTopSpeed = VehicleSlotsHelper.VehicleInformation[_CurrentVehicle].topSpeed;
            ushort currentTopSpeed = ushort.Parse(currentCell.value);

            // BUG_93: max speed modifications outside TDUMT
            _IsSpeedLimiterEnabled = (slotTopSpeed != 0);

            if (currentTopSpeed > slotTopSpeed)
            {
                MessageBoxes.ShowWarning(this, _WARN_MODDED_MAX_SPEED);
                _IsSpeedLimiterEnabled = false;
            }

            topSpeedTrackBar.Enabled = _IsSpeedLimiterEnabled;

            if (slotTopSpeed == 0)
                topSpeedTrackBar.Maximum = currentTopSpeed;
            else
                topSpeedTrackBar.Maximum = slotTopSpeed;

            if (_IsSpeedLimiterEnabled)
                topSpeedTrackBar.Value = currentTopSpeed;
            else
                topSpeedTrackBar.Value = topSpeedTrackBar.Maximum;

            _UpdateSpeedLimiter(); 

            // Acceleration : 0 to 100 kph
            currentCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.ACCELERATION_0_100_KMH_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            datasheetAccelerationKmhTextBox.Text = currentCell.value;

            // Acceleration : 0 to 100 mph
            currentCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.ACCELERATION_0_100_MPH_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            datasheetAccelerationMphTextBox.Text = currentCell.value;

            // Acceleration : 0-400m
            currentCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.QUARTER_MILE_SEC_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            datasheetAcceleration400MTextBox.Text = currentCell.value;

            // Acceleration : 0-1000m
            currentCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.T0_TO_1000M_SEC_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            datasheetAcceleration1000MTextBox.Text = currentCell.value;
        }

        /// <summary>
        /// Defines contents of speed limiter label
        /// </summary>
        private void _UpdateSpeedLimiter()
        {
            bool isMetric = (kmhSpeedRadioButton.Checked);

            ushort convertedValue = (ushort) topSpeedTrackBar.Value;

            if (!isMetric)
                convertedValue = (ushort) (convertedValue / 1.6);

            maxSpeedLabel.Text = convertedValue.ToString();
        }

        /// <summary>
        /// Returns brand logo file name according to specified brand reference
        /// </summary>
        /// <param name="brandReference"></param>
        /// <returns></returns>
        private static string _GetBrandLogoName(string brandReference)
        {
            string returnedName = null;

            if (!string.IsNullOrEmpty(brandReference))
                returnedName = string.Concat(AppConstants.FOLDER_LOGOS, brandReference, '.', LibraryConstants.EXTENSION_PNG_FILE);

            return returnedName;
        }

        /// <summary>
        /// Handles changing of vehicle's manufacturer
        /// </summary>
        /// <returns>true if changes were applied, else false</returns>
        private bool _ChangeManufacturer()
        {
            bool returnedValue = false;

            // Preparing manufacturer list
            SortedStringCollection sortedManufList = new SortedStringCollection();
            List<DB.Cell[]> allBrandRefAndNames = DatabaseHelper.SelectCellsFromTopic(_BrandsTable, DatabaseConstants.REF_DB_COLUMN, SharedConstants.ID_BRANDS_DB_COLUMN, SharedConstants.NAME_BRANDS_DB_COLUMN, SharedConstants.BITFIELD_DB_COLUMN);
            Dictionary<string, string> index = new Dictionary<string, string>();

            foreach (DB.Cell[] anotherEntry in allBrandRefAndNames)
            {
                // Bitfield
                // * b0 active brand if true
                // * b1 clothes if true
                bool[] currentBitField = DatabaseHelper.ParseBitField(anotherEntry[3]);

                // Inactive and clothes brands are ignored
                if (currentBitField[0] && !currentBitField[1])
                {
                    string currentRef = DatabaseHelper.GetResourceValueFromCell(anotherEntry[0], _BrandsResource);
                    string brandId = DatabaseHelper.GetResourceValueFromCell(anotherEntry[1], _BrandsResource);
                    string brandName = DatabaseHelper.GetResourceValueFromCell(anotherEntry[2], _BrandsResource);
                    string currentName = string.Format(_FORMAT_BRAND_NAME, brandName, brandId);

                    // ?? Names are not included
                    if (!SharedConstants.ERROR_DB_RESVAL.Equals(currentName) &&
                        !DatabaseConstants.NOT_AVAILABLE_NAME_BRANDS_DB_RESID.Equals(anotherEntry[2].value) &&
                        !sortedManufList.Contains(currentName))
                    {
                        sortedManufList.Add(currentName);
                        index.Add(currentName, currentRef);
                    }
                }
            }

            // Displaying browse dialog
            TableBrowsingDialog dialog = new TableBrowsingDialog(_MESSAGE_BROWSE_BRANDS, sortedManufList, index);
            DialogResult dr = dialog.ShowDialog();

            if (dr == DialogResult.OK && dialog.SelectedIndex != null)
            {
                // Applying changes
                DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, SharedConstants.BRAND_PHYSICS_DB_COLUMN, _CurrentVehicle, dialog.SelectedIndex);

                returnedValue = true;
            }

            return returnedValue;
        }

        /// <summary>
        /// Handles changing of vehicle's name (realname / model / version)
        /// </summary>
        /// <param name="isModelChange">true for model change, else version change</param>
        /// <returns>true if changes were applied, else false</returns>
        private bool _ChangeModelOrVersion(bool isModelChange)
        {
            // Preparing name list
            SortedStringCollection sortedNameList = new SortedStringCollection();
            Dictionary<string, string> index = new Dictionary<string, string>();

            foreach (DBResource.Entry anotherEntry in _PhysicsResource.EntryList)
            {
                // 60 first entries and comments are ignored
                if (!anotherEntry.isComment && anotherEntry.index > 63)
                {
                    string currentRef = anotherEntry.id.Id;
                    string currentName = anotherEntry.value;

                    if (currentName != null && !sortedNameList.Contains(currentName))
                    {
                        // Empty symbol is allowed
                        if (SharedConstants.ERROR_DB_RESVAL.Equals(currentName) && DatabaseConstants.NOT_AVAILABLE_NAME_PHYSICS_DB_RESID.Equals(currentRef)
                            || !SharedConstants.ERROR_DB_RESVAL.Equals(currentName))
                        {
                            sortedNameList.Add(currentName);
                            index.Add(currentName, currentRef);
                        }
                    }
                }
            }

            // Displaying browse dialog
            TableBrowsingDialog dialog = new TableBrowsingDialog(_MESSAGE_BROWSE_PHYSICS_NAMES, sortedNameList, index) { IsAddButtonEnabled = true };
            DialogResult dr = dialog.ShowDialog();

            if (dr == DialogResult.OK && dialog.SelectedIndex != null)
            {
                // Applying changes
                if (_IsVehicleWithRealName)
                    // RealName used only
                    DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, SharedConstants.REAL_NAME_PHYSICS_DB_COLUMN, _CurrentVehicle, dialog.SelectedIndex);
                else
                {
                    // Model or version name
                    if (isModelChange)
                        DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, SharedConstants.MODEL_NAME_PHYSICS_DB_COLUMN, _CurrentVehicle, dialog.SelectedIndex);
                    else
                        DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, SharedConstants.VERSION_NAME_PHYSICS_DB_COLUMN, _CurrentVehicle, dialog.SelectedIndex);
                }

                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Allows to change brake diameter
        /// </summary>
        /// <param name="isFrontBrakes"></param>
        private void _ChangeBrakesDiameter(bool isFrontBrakes)
        {
            string currentValue = (isFrontBrakes
                                       ? frontBrakesLabel.Text.Split('ø')[1]
                                       : rearBrakesLabel.Text.Split('ø')[1]);
            string message = (isFrontBrakes ? _MESSAGE_ENTER_FRONT_DIAMETER : _MESSAGE_ENTER_REAR_DIAMETER);
            PromptBox pBox = new PromptBox(_TITLE_BRAKES_DIAMETER, message, currentValue);
            DialogResult dr = pBox.ShowDialog(this);

            if (dr == DialogResult.OK && pBox.IsValueChanged)
            {
                Cursor = Cursors.WaitCursor;

                // Changing displacement
                string columnName = (isFrontBrakes
                                         ? SharedConstants.BRAKES_DIM_FRONT_PHYSICS_DB_COLUMN
                                         : SharedConstants.BRAKES_DIM_REAR_PHYSICS_DB_COLUMN);
                string newValue = pBox.ReturnValue;

                DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, columnName, _CurrentVehicle, newValue);

                // Reloading
                _InitializeDatasheetContents();

                // Modification flag
                _IsDatabaseModified = true;

                StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_BRAKE_DIAMETER_OK);

                Cursor = Cursors.Default;
            }
        }
        
        /// <summary>
        /// Allows to change brake characteristics
        /// </summary>
        /// <param name="isFrontBrakes"></param>
        private void _ChangeBrakesCharacteristics(bool isFrontBrakes)
        {
            // Preparing type list
            SortedStringCollection sortedBrakesList = new SortedStringCollection();
            Dictionary<string, string> index = new Dictionary<string, string>();

            // Browsing all physics resource values
            foreach (DBResource.Entry entry in _PhysicsResource.EntryList)
            {
                if (entry.isValid && !entry.isComment)
                {
                    // Filter over physics resource
                    if (entry.id.Id.EndsWith(DBResource.SUFFIX_PHYSICS_BRAKES_CHAR)
                        && !sortedBrakesList.Contains(entry.value))
                    {
                        sortedBrakesList.Add(entry.value);
                        index.Add(entry.value, entry.id.Id);
                    }
                }
            }

            // Displaying browse dialog
            string message = (isFrontBrakes ? _MESSAGE_BROWSE_FRONT_BRAKES : _MESSAGE_BROWSE_REAR_BRAKES);
            TableBrowsingDialog dialog = new TableBrowsingDialog(message, sortedBrakesList, index);
            DialogResult dr = dialog.ShowDialog();

            if (dr == DialogResult.OK && dialog.SelectedIndex != null)
            {
                Cursor = Cursors.WaitCursor;

                // Applying changes
                string columnName = (isFrontBrakes
                                         ? SharedConstants.BRAKES_CHAR_FRONT_PHYSICS_DB_COLUMN
                                         : SharedConstants.BRAKES_CHAR_REAR_PHYSICS_DB_COLUMN);

                DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, columnName, _CurrentVehicle, dialog.SelectedIndex);

                // Reloading
                _InitializeDatasheetContents();

                // Modification flag
                _IsDatabaseModified = true;

                StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_BRAKE_CHAR_OK);

                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles changing of vehicle's engine type
        /// </summary>
        private bool _ChangeEngineType()
        {
            bool returnedValue = false;

            // Preparing type list
            SortedStringCollection sortedTypeList = new SortedStringCollection();
            Dictionary<string, string> index = new Dictionary<string, string>();

            // Browsing all physics resource values
            foreach (DBResource.Entry entry in _PhysicsResource.EntryList)
            {
                if (entry.isValid && !entry.isComment)
                {
                    // Filter over physics resource
                    if ( entry.id.Id.EndsWith(DBResource.SUFFIX_PHYSICS_ENGINE_TYPE)
                        && !sortedTypeList.Contains(entry.value))
                    {
                        sortedTypeList.Add(entry.value);
                        index.Add(entry.value, entry.id.Id);
                    }
                }
            }

            // Displaying browse dialog
            TableBrowsingDialog dialog = new TableBrowsingDialog(_MESSAGE_BROWSE_ENGINE_TYPES, sortedTypeList, index);
            DialogResult dr = dialog.ShowDialog();

            if (dr == DialogResult.OK && dialog.SelectedIndex != null)
            {
                // Applying changes
                DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, SharedConstants.ENGINE_TYPE_PHYSICS_DB_COLUMN, _CurrentVehicle, dialog.SelectedIndex);

                returnedValue = true;
            }

            return returnedValue;
        }
        
        /// <summary>
        /// Handles changing of vehicle's tires (no effect in game ?)
        /// </summary>
        /// <returns></returns>
        private bool _ChangeTires()
        {
            bool returnedValue = false;

            // Preparing type list
            SortedStringCollection sortedTireList = new SortedStringCollection();
            Dictionary<string, string> index = new Dictionary<string, string>();

            // Browsing all physics resource values
            foreach (DBResource.Entry entry in _PhysicsResource.EntryList)
            {
                if (entry.isValid && !entry.isComment)
                {
                    // Filter over physics resource
                    if (entry.id.Id.EndsWith(DBResource.SUFFIX_PHYSICS_TIRES)
                        && !sortedTireList.Contains(entry.value))
                    {
                        sortedTireList.Add(entry.value);
                        index.Add(entry.value, entry.id.Id);
                    }
                }
            }

            // Displaying browse dialog
            TableBrowsingDialog dialog = new TableBrowsingDialog(_MESSAGE_BROWSE_TIRES, sortedTireList, index);
            DialogResult dr = dialog.ShowDialog();

            if (dr == DialogResult.OK && dialog.SelectedIndex != null)
            {
                // Applying changes
                DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, SharedConstants.TYRES_TYPE_PHYSICS_DB_COLUMN, _CurrentVehicle, dialog.SelectedIndex);

                returnedValue = true;
            }

            return returnedValue;
        }

        /// <summary>
        /// Handles chaing of vehicle's performance information
        /// </summary>
        private void _ChangePerformanceData()
        {
            // Acceleration : 0 to 100kmh
            if (!string.IsNullOrEmpty(datasheetAccelerationKmhTextBox.Text))
            {
                DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable,
                                                                  SharedConstants.
                                                                      ACCELERATION_0_100_KMH_PHYSICS_DB_COLUMN,
                                                                  _CurrentVehicle,
                                                                  datasheetAccelerationKmhTextBox.Text);
            }
            // Acceleration : 0 to 100mph
            if (!string.IsNullOrEmpty(datasheetAccelerationMphTextBox.Text))
            {
                DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable,
                                                                  SharedConstants.
                                                                      ACCELERATION_0_100_MPH_PHYSICS_DB_COLUMN,
                                                                  _CurrentVehicle,
                                                                  datasheetAccelerationMphTextBox.Text);
            }
            // Acceleration : 0-400m
            if (!string.IsNullOrEmpty(datasheetAcceleration400MTextBox.Text))
            {
                DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable,
                                                                  SharedConstants.
                                                                      QUARTER_MILE_SEC_PHYSICS_DB_COLUMN,
                                                                  _CurrentVehicle,
                                                                  datasheetAcceleration400MTextBox.Text);
            }
            // Acceleration : 0-1000m
            if (!string.IsNullOrEmpty(datasheetAcceleration1000MTextBox.Text))
            {
                DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable,
                                                                  SharedConstants.
                                                                      T0_TO_1000M_SEC_PHYSICS_DB_COLUMN,
                                                                  _CurrentVehicle,
                                                                  datasheetAcceleration1000MTextBox.Text);
            }
            // Top speed
            if (_IsSpeedLimiterEnabled)
            {
                DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable,
                                                                  SharedConstants.TOP_SPEED_KMH_PHYSICS_DB_COLUMN,
                                                                  _CurrentVehicle,
                                                                  topSpeedTrackBar.Value.ToString());
            }
        }
        #endregion
    }
}
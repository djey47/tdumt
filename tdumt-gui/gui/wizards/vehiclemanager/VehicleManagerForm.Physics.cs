using System;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Support.Traces;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.support;
using TDUModdingTools.gui.wizards.vehiclemanager.common;
using TDUModdingTools.gui.wizards.vehiclemanager.physics;

namespace TDUModdingTools.gui.wizards.vehiclemanager
{
	partial class VehicleManagerForm
	{
        #region Constants
        /// <summary>
        /// Maximum value for engine RPM
        /// </summary>
	    private const int _MAX_RPM = 20000;

        /// <summary>
        /// Error message when setting gearbox physics with incorrect gear ratio(s)
        /// </summary>
	    private const string _ERROR_INCORRECT_GEAR_RATIOS =
	        "One or more gear ratios are not set.\r\nPlease edit values.";
        #endregion

        #region Members
        /// <summary>
        /// Array of current gear ratios
        /// </summary>
	    private string[] _CurrentRatiosArray;

        /// <summary>
        /// Array of current ride height (front / rear)
        /// </summary>
	    private string[] _CurrentRideHeightArray;

        /// <summary>
        /// Array of current ride height (front / rear)
        /// </summary>
        private string[] _CurrentSuspensionArray;
        #endregion

        #region Events
        private void physicsDriveSetButton_Click(object sender, EventArgs e)
        {
            // Click on 'Set' button (wheel drive)
            try
            {
                Cursor = Cursors.WaitCursor;

                _ChangeDriveOptions();

                // Modification flag
                _IsDatabaseModified = true;

                StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_DRIVE_OK);

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void physicsGearboxSetButton_Click(object sender, EventArgs e)
        {
            // Click on 'Set' button (gearbox)
            try
            {
                Cursor = Cursors.WaitCursor;

                _ChangeBoxOptions();
                _ChangeGears();

                // Modification flag
                _IsDatabaseModified = true;

                StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_BOX_OK);

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void physicsEngineSetButton_Click(object sender, EventArgs e)
        {
            // Click on 'Set' button (engine location)
            try
            {
                Cursor = Cursors.WaitCursor;

                _ChangeEngineLocation();
                _ChangeEngineOptions();
                _ChangeSupercharger();

                // Modification flag
                _IsDatabaseModified = true;

                // Reloading
                _InitializePhysicsContents();

                StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_ENGINE_OK);

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }
	    
        private void physicsEditGearRatiosLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on 'Edit gear ratios' link label
            try
            {
                GearRatiosDialog dlg =
                    new GearRatiosDialog(int.Parse(physicsGearCountComboBox.Text), _CurrentRatiosArray);
                DialogResult dr = dlg.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    _CurrentRatiosArray = dlg.RatiosArray;
                }
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void physicsRearHeightRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Click on rear height radio button
            if (physicsRearHeightRadioButton.Checked)
                _UpdateRideHeightContents();
        }

        private void physicsFrontHeightRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Click on front height radio button
            if (physicsFrontHeightRadioButton.Checked)
                _UpdateRideHeightContents();
        }

        private void physicsRearSuspRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Click on rear suspension radio button
            if (physicsRearSuspRadioButton.Checked)
                _UpdateSuspensionContents();
        }

        private void physicsFrontSuspHeightRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Click on front suspension radio button
            if (physicsFrontSuspRadioButton.Checked)
                _UpdateSuspensionContents();
        }
        private void physicsRideHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            // New value in ride height field
            _UpdateRideHeight(true, physicsRideHeightTextBox.Text);
        }

        private void physicsRideHeightMaxTextBox_TextChanged(object sender, EventArgs e)
        {
            // New value in max ride height field
            _UpdateRideHeight(false, physicsRideHeightMaxTextBox.Text);
        }

        private void physicsSuspHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            // New value in suspension height field
            _UpdateSuspension(true, physicsSuspHeightTextBox.Text);
        }

        private void physicsSuspRateTextBox_TextChanged(object sender, EventArgs e)
        {
            // New value in suspension height field
            _UpdateSuspension(false, physicsSuspRateTextBox.Text);
        }

        private void physicsBodyworkSetButton_Click(object sender, EventArgs e)
        {
            // Click on Bodywork>Set button
            try
            {
                Cursor = Cursors.WaitCursor;

                _ChangeRideHeight();
                _ChangeSuspension();
                _ChangeBodyType();
                _ChangeDimensions();

                // Modification flag
                _IsDatabaseModified = true;

                // Reloading
                _InitializePhysicsContents();

                StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_BODYWORK_OK);

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }
	    #endregion
 
        #region Private methods
        /// <summary>
        /// Defines tab contents
        /// </summary>
        private void _InitializePhysicsContents()
        {
            // TRANSMISSION > Drive type
            DB.Cell driveTypeRefCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.DRIVE_PHYSICS_DB_COLUMN, _PhysicsTable,
                                                                   _CurrentVehicle)[0];
            string driveTypeRef = driveTypeRefCell.value;

            switch (driveTypeRef)
            {
                case SharedConstants.FWD_DRIVE_PHYSICS_DB_RESID:
                    physicsDriveFrontRadioButton.Checked = true;
                    break;
                case SharedConstants.RWD_DRIVE_PHYSICS_DB_RESID:
                    physicsDriveRearRadioButton.Checked = true;
                    break;
                case SharedConstants.AWD_DRIVE_PHYSICS_DB_RESID:
                case SharedConstants.FOURWD_DRIVE_PHYSICS_DB_RESID:
                    physicsDriveAllRadioButton.Checked = true;
                    break;
            }

            // TRANSMISSION > Primacy
            DB.Cell primacyCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.TRANSMISSION_PRIMACY_PHYSICS_DB_COLUMN, _PhysicsTable,
                                                                   _CurrentVehicle)[0];
            physicsPrimacyTextBox.Text = primacyCell.value;

            // GEARBOX > Box type
            DB.Cell boxTypeRefCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.GEARBOX_PHYSICS_DB_COLUMN, _PhysicsTable,
                                                                   _CurrentVehicle)[0];
            string boxTypeRef = boxTypeRefCell.value;

            switch (boxTypeRef)
            {
                case SharedConstants.SEQ_BOX_PHYSICS_DB_RESID:
                    boxSequentialRadioButton.Checked = true;
                    break;
                case SharedConstants.STICK_BOX_PHYSICS_DB_RESID:
                    boxStickRadioButton.Checked = true;
                    break;
                // EVO_170
                /*case SharedConstants.SEMI_BOX_PHYSICS_DB_RESID:
                    boxSemiRadioButton.Checked = true;
                    break;*/
                //
                case SharedConstants.AUTO_BOX_PHYSICS_DB_RESID:
                    boxAutoRadioButton.Checked = true;
                    break;
            }

            // GEARBOX > Gear count
            DB.Cell gearCountCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.NB_GEARS_PHYSICS_DB_COLUMN, _PhysicsTable,
                                                                   _CurrentVehicle)[0];
            int gearCount;
            bool isSuccess = int.TryParse(gearCountCell.value, out gearCount);

            if (isSuccess && gearCount > 0 && gearCount < 8)
                physicsGearCountComboBox.Text = gearCount.ToString();
            else
                physicsGearCountComboBox.Text = "";

            // GEARBOX > Gear ratios
            _CurrentRatiosArray = new string[8];

            for (int i = 0 ; i <= gearCount ; i++)
            {
                 string columnName = 
                     (i == 0 ? 
                        SharedConstants.FINAL_DRIVE_RATIO_PHYSICS_DB_COLUMN 
                        : string.Format(SharedConstants.GEAR_RATIO_PATTERN_PHYSICS_DB_COLUMN, i));

                DB.Cell ratioCell =
                    DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(columnName, _PhysicsTable, _CurrentVehicle)[0];

                _CurrentRatiosArray[i] = ratioCell.value;
            }

            // GEARBOX > Inertia
            DB.Cell boxInertiaCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.GEARBOX_INERTIA_PHYSICS_DB_COLUMN, _PhysicsTable,
                                                                   _CurrentVehicle)[0];
            physicsBoxInertiaTextBox.Text = boxInertiaCell.value;

            // ENGINE > Engine location
            DB.Cell engineLocRefCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.ENGINE_LOCALISATION_PHYSICS_DB_COLUMN, _PhysicsTable,
                                                                   _CurrentVehicle)[0];
            string engineLocRef = engineLocRefCell.value;

            switch (engineLocRef)
            {
                case SharedConstants.FRONT_ENGINE_LOC_PHYSICS_DB_RESID:
                    physicsFrontEngineRadioButton.Checked = true;
                    break;
                case SharedConstants.REAR_ENGINE_LOC_PHYSICS_DB_RESID:
                    physicsRearEngineRadioButton.Checked = true;
                    break;
                case SharedConstants.CENTER_ENGINE_LOC_PHYSICS_DB_RESID:
                    physicsCenterEngineRadioButton.Checked = true;
                    break;
            }

            // ENGINE > Redline
            DB.Cell redlineCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.RED_LINE_PHYSICS_DB_COLUMN, _PhysicsTable,
                                                                   _CurrentVehicle)[0];

            physicsRedlineTextbox.Text = redlineCell.value;

            // ENGINE > Ignition RPM
            DB.Cell ignitionRpmCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.IGNITION_RPM_PHYSICS_DB_COLUMN, _PhysicsTable,
                                                                   _CurrentVehicle)[0];

            physicsIgnitionRpmTextBox.Text = ignitionRpmCell.value;

            // ENGINE > Inertia
            DB.Cell engineInertiaCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.ENGINE_INERTIA_PHYSICS_DB_COLUMN, _PhysicsTable,
                                                                   _CurrentVehicle)[0];

            physicsEngineInertiaTextBox.Text = engineInertiaCell.value;

            // ENGINE > Supercharger
            DB.Cell turboCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.NB_TURBOS_PHYSICS_DB_COLUMN, _PhysicsTable,
                                                                   _CurrentVehicle)[0];
            int turboCount;
            
            isSuccess = int.TryParse(turboCell.value, out turboCount);

            superchargerNonePhysicsRadioButton.Checked = true;

            if (isSuccess && turboCount > 0)
                // Turbo and Twin-Turbo are the same thing in-game
                superchargerOnePhysicsRadioButton.Checked = true;

            // ENGINE > ignition times
            DB.Cell ignitionTimeCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(
                    SharedConstants.IGNITION_TIME_IGNITE_PHYSICS_DB_COLUMN, _PhysicsTable, _CurrentVehicle)[0];

            physicsIgnitionTimeTextBox.Text = ignitionTimeCell.value;

            DB.Cell revUpTimeCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(
                    SharedConstants.IGNITION_TIME_REVUP_PHYSICS_DB_COLUMN, _PhysicsTable, _CurrentVehicle)[0];

            physicsRevUpTimeTextBox.Text = revUpTimeCell.value;

            DB.Cell revDownTimeCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(
                    SharedConstants.IGNITION_TIME_REVDOWN_PHYSICS_DB_COLUMN, _PhysicsTable, _CurrentVehicle)[0];

            physicsRevDownTimeTextBox.Text = revDownTimeCell.value;

            // Bodywork: ride height
            _CurrentRideHeightArray = new string[4];

            DB.Cell frontHeightCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.RIDE_HEIGHT_FRONT_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            DB.Cell rearHeightCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.RIDE_HEIGHT_REAR_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            DB.Cell frontMaxHeightCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.RIDE_HEIGHT_MAX_FRONT_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            DB.Cell rearMaxHeightCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.RIDE_HEIGHT_MAX_REAR_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];

            _CurrentRideHeightArray[0] = frontHeightCell.value;
            _CurrentRideHeightArray[1] = rearHeightCell.value;
            _CurrentRideHeightArray[2] = frontMaxHeightCell.value;
            _CurrentRideHeightArray[3] = rearMaxHeightCell.value;

            _UpdateRideHeightContents();

            // Suspension
            _CurrentSuspensionArray = new string[4];

            DB.Cell frontSuspHeightCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.SUSPENSION_LENGTH_FRONT_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            DB.Cell rearSuspHeightCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.SUSPENSION_LENGTH_REAR_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            DB.Cell frontSuspRateCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.SUSPENSION_RATE_FRONT_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            DB.Cell rearSuspRateCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.SUSPENSION_RATE_REAR_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];

            _CurrentSuspensionArray[0] = frontSuspHeightCell.value;
            _CurrentSuspensionArray[1] = rearSuspHeightCell.value;
            _CurrentSuspensionArray[2] = frontSuspRateCell.value;
            _CurrentSuspensionArray[3] = rearSuspRateCell.value;

            _UpdateSuspensionContents();

            // Bodywork: body type
            DB.Cell bodyTypeCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.CAR_BODY_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            string currentBodyType = bodyTypeCell.value;

            foreach (var anotherItem in physicsBodyTypeComboBox.Items)
            {
                if (anotherItem.ToString().Contains(currentBodyType))
                {
                    physicsBodyTypeComboBox.Text = anotherItem.ToString();
                    break;
                }
            }

            // Bodywork: dimensions
            DB.Cell lengthCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.LENGTH_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable, _CurrentVehicle)[0];
            DB.Cell widthCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.WIDTH_PHYSICS_DB_COLUMN,
                                                       _PhysicsTable, _CurrentVehicle)[0];
            DB.Cell heightCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.HEIGHT_PHYSICS_DB_COLUMN,
                                                       _PhysicsTable, _CurrentVehicle)[0];

            physicsLengthDimTextBox.Text = lengthCell.value;
            physicsWidthDimTextBox.Text = widthCell.value;
            physicsHeightDimTextBox.Text = heightCell.value;
        }

        /// <summary>
        /// Defines contents for ride height controls
        /// </summary>
        private void _UpdateRideHeightContents()
	    {
            if (physicsFrontHeightRadioButton.Checked)
            {
                physicsRideHeightTextBox.Text = _CurrentRideHeightArray[0];
                physicsRideHeightMaxTextBox.Text = _CurrentRideHeightArray[2];
            }
            else
            {
                physicsRideHeightTextBox.Text = _CurrentRideHeightArray[1];
                physicsRideHeightMaxTextBox.Text = _CurrentRideHeightArray[3];
            }
        }

	    /// <summary>
        /// Defines contents for suspension controls
        /// </summary>
        private void _UpdateSuspensionContents()
	    {
            if (physicsFrontSuspRadioButton.Checked)
            {
                physicsSuspHeightTextBox.Text = _CurrentSuspensionArray[0];
                physicsSuspRateTextBox.Text = _CurrentSuspensionArray[2];
            }
            else
            {
                physicsSuspHeightTextBox.Text = _CurrentSuspensionArray[1];
                physicsSuspRateTextBox.Text = _CurrentSuspensionArray[3];
            }
	    }

        /// <summary>
        /// Updates ride height upon value changes
        /// </summary>
        /// <param name="isDefaultHeight"></param>
        /// <param name="newValue"></param>
        private void _UpdateRideHeight(bool isDefaultHeight, string newValue)
        {
            int index;
            bool isFront = physicsFrontHeightRadioButton.Checked;

            if (isDefaultHeight && isFront)
                index = 0;
            else if (isDefaultHeight)
                index = 1;
            else if (isFront)
                index = 2;
            else
                index = 3;

            _CurrentRideHeightArray[index] = newValue;
        }

        /// <summary>
        /// Updates suspension upon value changes
        /// </summary>
        /// <param name="isHeight"></param>
        /// <param name="newValue"></param>
        private void _UpdateSuspension(bool isHeight, string newValue)
        {
            int index;
            bool isFront = physicsFrontSuspRadioButton.Checked;

            if (isHeight && isFront)
                index = 0;
            else if (isHeight)
                index = 1;
            else if (isFront)
                index = 2;
            else
                index = 3;

            _CurrentSuspensionArray[index] = newValue;
        }

        /// <summary>
        /// Allows to change drive type of current vehicle
        /// </summary>
        private void _ChangeDriveOptions()
        {
            // Getting drive type reference
            string driveReference = SharedConstants.NO_DRIVE_PHYSICS_DB_RESID;
            
            if (physicsDriveRearRadioButton.Checked)
                driveReference = SharedConstants.RWD_DRIVE_PHYSICS_DB_RESID;
            else if (physicsDriveAllRadioButton.Checked)
                driveReference = SharedConstants.FOURWD_DRIVE_PHYSICS_DB_RESID;
            else if (physicsDriveFrontRadioButton.Checked)
                driveReference = SharedConstants.FWD_DRIVE_PHYSICS_DB_RESID;
            
            // Primacy
            int primacy;
            if (int.TryParse(physicsPrimacyTextBox.Text, out primacy))
            {
                if (primacy < 0 || primacy > 100)
                    primacy = 0;
            }
            else
                primacy = 0;

            physicsPrimacyTextBox.Text = primacy.ToString();

            // Applying changes in database
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable,
                                                              SharedConstants.DRIVE_PHYSICS_DB_COLUMN, _CurrentVehicle,
                                                              driveReference);
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable,
                                                              SharedConstants.TRANSPRIMACY_PHYSICS_DB_COLUMN, _CurrentVehicle,
                                                              primacy.ToString());
        }

        /// <summary>
        /// Allows to change gearbox type of current vehicle
        /// </summary>
        private void _ChangeBoxOptions()
        {
            // Box type
            string boxType;

            if (boxSequentialRadioButton.Checked)
                boxType = SharedConstants.SEQ_BOX_PHYSICS_DB_RESID;
            else if (boxStickRadioButton.Checked)
                boxType = SharedConstants.STICK_BOX_PHYSICS_DB_RESID;
            // EVO_170
            /*else if (boxSemiRadioButton.Checked)
                boxType = SharedConstants.SEMI_BOX_PHYSICS_DB_RESID;*/
            //
            else
                boxType = SharedConstants.AUTO_BOX_PHYSICS_DB_RESID;

            // Inertia
            int inertia;
            if (int.TryParse(physicsBoxInertiaTextBox.Text, out inertia))
            {
                if (inertia < 0 || inertia > 100)
                    inertia = 0;
            }
            else
                inertia = 0;

            physicsBoxInertiaTextBox.Text = inertia.ToString();

            // Applying changes in database
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable,
                                                              SharedConstants.GEARBOX_PHYSICS_DB_COLUMN, _CurrentVehicle,
                                                              boxType);
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable,
                                                              SharedConstants.GEARBOX_INERTIA_PHYSICS_DB_COLUMN, _CurrentVehicle,
                                                              inertia.ToString());
        }

        /// <summary>
        /// Allows to change gears (count+ratios) of current vehicle
        /// </summary>
        private void _ChangeGears()
        {
            // Checks if all gear ratios are OK
            int gearCount = int.Parse(physicsGearCountComboBox.Text);

            for (int i = 0; i < gearCount + 1; i++)
            {
                if (string.IsNullOrEmpty(_CurrentRatiosArray[i]) || _CurrentRatiosArray[i].Equals("0"))
                    throw new Exception(_ERROR_INCORRECT_GEAR_RATIOS);
            } 
            
            // Gear count
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable,
                                                                  SharedConstants.NB_GEARS_PHYSICS_DB_COLUMN, _CurrentVehicle,
                                                                  physicsGearCountComboBox.Text);

            // Ratios
            int count = 0;

            foreach (string anotherRatio in _CurrentRatiosArray)
            {
                string columnName = ((count == 0)
                                         ? SharedConstants.FINAL_DRIVE_RATIO_PHYSICS_DB_COLUMN
                                         : string.Format(SharedConstants.GEAR_RATIO_PATTERN_PHYSICS_DB_COLUMN, count));

                if (!string.IsNullOrEmpty(anotherRatio))
                    DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable,
                                                              columnName, _CurrentVehicle,
                                                              anotherRatio);
                count++;
            }        
        }

        /// <summary>
        /// Allows to change engine location of current vehicle
        /// </summary>
        private void _ChangeEngineLocation()
        {
            string engineLocation;

            if (physicsFrontEngineRadioButton.Checked)
                engineLocation = SharedConstants.FRONT_ENGINE_LOC_PHYSICS_DB_RESID;
            else if (physicsCenterEngineRadioButton.Checked)
                engineLocation = SharedConstants.CENTER_ENGINE_LOC_PHYSICS_DB_RESID;
            else
                engineLocation = SharedConstants.REAR_ENGINE_LOC_PHYSICS_DB_RESID;

            // Applying change in database
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable,
                                                              SharedConstants.ENGINE_LOCALISATION_PHYSICS_DB_COLUMN, _CurrentVehicle,
                                                              engineLocation);
        }

        /// <summary>
        /// Changes maximum RPM for current vehicle
        /// </summary>
        private void _ChangeEngineOptions()
        {
            // Redline
            if (!string.IsNullOrEmpty(physicsRedlineTextbox.Text))
            {
                try
                {
                    ushort newRedLine = ushort.Parse(physicsRedlineTextbox.Text);

                    // Checking limits
                    if (newRedLine > 0 && newRedLine <= _MAX_RPM)
                        DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable,
                                                                          SharedConstants.RED_LINE_PHYSICS_DB_COLUMN,
                                                                          _CurrentVehicle,
                                                                          newRedLine.ToString());
                }
                catch (Exception)
                {
                    Log.Info("Invalid RedLine value provided: " + physicsRedlineTextbox.Text);
                }
            }

            // Ignition RPM
            if (!string.IsNullOrEmpty(physicsRedlineTextbox.Text) 
                && !string.IsNullOrEmpty(physicsIgnitionRpmTextBox.Text))
            {
                try
                {
                    ushort newRedLine = ushort.Parse(physicsRedlineTextbox.Text);
                    ushort newIgnitionRpm = ushort.Parse(physicsIgnitionRpmTextBox.Text);

                    // Checking limits
                    if (newIgnitionRpm > 0 && newIgnitionRpm <= newRedLine && newIgnitionRpm <= _MAX_RPM)
                        DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable,
                                                                          SharedConstants.IGNITION_RPM_PHYSICS_DB_COLUMN,
                                                                          _CurrentVehicle,
                                                                          newIgnitionRpm.ToString());
                }
                catch (Exception)
                {
                    Log.Info("Invalid RPM values provided: " + physicsRedlineTextbox.Text + " - " + physicsIgnitionRpmTextBox.Text);
                }
            }

            // Inertia
            int inertia;
            if (int.TryParse(physicsEngineInertiaTextBox.Text, out inertia))
            {
                if (inertia < 0 || inertia > 100)
                    inertia = 0;
            }
            else
                inertia = 0;

            physicsEngineInertiaTextBox.Text = inertia.ToString();

            // Applying change in database
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable,
                                                              SharedConstants.ENGINE_INERTIA_PHYSICS_DB_COLUMN, _CurrentVehicle,
                                                              inertia.ToString());

            // Ignition times
            physicsIgnitionTimeTextBox.Text = physicsIgnitionTimeTextBox.Text.Replace('.', ',');
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable,
                                                              SharedConstants.IGNITION_TIME_IGNITE_PHYSICS_DB_COLUMN, _CurrentVehicle,
                                                              physicsIgnitionTimeTextBox.Text);
            physicsRevUpTimeTextBox.Text = physicsRevUpTimeTextBox.Text.Replace('.', ',');
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable,
                                                              SharedConstants.IGNITION_TIME_REVUP_PHYSICS_DB_COLUMN, _CurrentVehicle,
                                                              physicsRevUpTimeTextBox.Text);
            physicsRevDownTimeTextBox.Text = physicsRevDownTimeTextBox.Text.Replace('.', ',');
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable,
                                                              SharedConstants.IGNITION_TIME_REVDOWN_PHYSICS_DB_COLUMN, _CurrentVehicle,
                                                              physicsRevDownTimeTextBox.Text);
        }

        /// <summary>
        /// Changes supercharger type for current vehicle
        /// </summary>
        private void _ChangeSupercharger()
        {
            int turboCount = 0;

            if (superchargerOnePhysicsRadioButton.Checked)
                turboCount = 1;
            // Turbo and Twin-Turbo are the same thing in-game
            /*else if (superchargerTwinPhysicsRadioButton.Checked)
                turboCount = 2;*/

            // Updating database
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable,
                                                              SharedConstants.NB_TURBOS_PHYSICS_DB_COLUMN,
                                                              _CurrentVehicle,
                                                              turboCount.ToString());
        }

        /// <summary>
        /// Changes suspension settings into database
        /// </summary>
        private void _ChangeSuspension()
        {
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, SharedConstants.SUSPENSION_LENGTH_FRONT_PHYSICS_DB_COLUMN, _CurrentVehicle, _CurrentSuspensionArray[0]);
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, SharedConstants.SUSPENSION_LENGTH_REAR_PHYSICS_DB_COLUMN, _CurrentVehicle, _CurrentSuspensionArray[1]);
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, SharedConstants.SUSPENSION_RATE_FRONT_PHYSICS_DB_COLUMN, _CurrentVehicle, _CurrentSuspensionArray[2]);
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, SharedConstants.SUSPENSION_RATE_REAR_PHYSICS_DB_COLUMN, _CurrentVehicle, _CurrentSuspensionArray[3]);
        }

        /// <summary>
        /// Changes ride height settings into database
        /// </summary>
        private void _ChangeRideHeight()
        {
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, SharedConstants.RIDE_HEIGHT_FRONT_PHYSICS_DB_COLUMN, _CurrentVehicle, _CurrentRideHeightArray[0]);
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, SharedConstants.RIDE_HEIGHT_REAR_PHYSICS_DB_COLUMN, _CurrentVehicle, _CurrentRideHeightArray[1]);
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, SharedConstants.RIDE_HEIGHT_MAX_FRONT_PHYSICS_DB_COLUMN, _CurrentVehicle, _CurrentRideHeightArray[2]);
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, SharedConstants.RIDE_HEIGHT_MAX_REAR_PHYSICS_DB_COLUMN, _CurrentVehicle, _CurrentRideHeightArray[3]);
        }

        /// <summary>
        /// Allows to set vehicle body type into database
        /// </summary>
        private void _ChangeBodyType()
        {
            string selectedBodyType = physicsBodyTypeComboBox.Text.Split(Tools.SYMBOL_VALUE_SEPARATOR)[1];

            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, SharedConstants.CAR_BODY_PHYSICS_DB_COLUMN, _CurrentVehicle, selectedBodyType);
        }

        /// <summary>
        /// Sets dimension information into database
        /// </summary>
        private void _ChangeDimensions()
        {
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, SharedConstants.LENGTH_PHYSICS_DB_COLUMN, _CurrentVehicle, physicsLengthDimTextBox.Text);
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, SharedConstants.WIDTH_PHYSICS_DB_COLUMN, _CurrentVehicle, physicsWidthDimTextBox.Text);
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_PhysicsTable, SharedConstants.HEIGHT_PHYSICS_DB_COLUMN, _CurrentVehicle, physicsHeightDimTextBox.Text);
        }
        #endregion
    }
}
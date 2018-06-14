using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Types;
using DjeFramework1.Util.BasicStructures;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.support;
using TDUModdingLibrary.support.constants;
using TDUModdingTools.common;
using TDUModdingTools.gui.wizards.vehiclemanager.common;

namespace TDUModdingTools.gui.wizards.vehiclemanager
{
    /// <summary>
    /// VehicleManagerForm - Install part
    /// </summary>
    public partial class VehicleManagerForm
    {
        #region Constants
        /// <summary>
        /// Particular extension for backup files by Vehicle Manager
        /// </summary>
        public const string EXTENSION_BACKUP = "VMBAK";

        /// <summary>
        /// Error message when trying to copy a file which has been already modified
        /// </summary>
        private const string _ERROR_BACKUP_EXISTS = "Previous backup already exists";

        /// <summary>
        /// Error message when trying to restore a file which does not have a backup
        /// </summary>
        private const string _ERROR_BACKUP_NOT_EXISTS = "Backup does not exist";

        /// <summary>
        /// Error message when trying to update a file which has not been modded before
        /// </summary>
        private const string _ERROR_NOTHING_TO_UPDATE =
            "This component can't be updated.\r\nPlease install a mod onto it first";

        /// <summary>
        /// Error message when installing components
        /// </summary>
        private const string _ERROR_INSTALLING = "Unable to install specified files.";

        /// <summary>
        /// Error message when restoring components
        /// </summary>
        private const string _ERROR_RESTORING = "Unable to restore specified files.";

        /// <summary>
        /// Error message when restoring components
        /// </summary>
        private const string _ERROR_UPDATING = "Unable to update specified files.";

        /// <summary>
        /// Status message while components are being installed
        /// </summary>
        private const string _STATUS_UPDATING = "Updating components now. Please wait...";
        
        /// <summary>
        /// Status message while components are being installed
        /// </summary>
        private const string _STATUS_INSTALLING = "Installing components now. Please wait...";

        /// <summary>
        /// Status message while components are being installed
        /// </summary>
        private const string _STATUS_RESTORING = "Restoring components now. Please wait...";

        /// <summary>
        /// Status message when install process ended without errors
        /// </summary>
        private const string _STATUS_INSTALL_OK = "Component install complete!";

        /// <summary>
        /// Status message when restore process ended without errors
        /// </summary>
        private const string _STATUS_RESTORE_OK = "Original component restore complete!";

        /// <summary>
        /// Status message when install process ended without errors
        /// </summary>
        private const string _STATUS_UPDATE_OK = "Component update complete!";

        /// <summary>
        /// Browsing dialog message (ext model)
        /// </summary>
        private const string _MESSAGE_INSTALL_EXT_MODEL = "Select a BNK file for exterior model...";

        /// <summary>
        /// Browsing dialog message (int model)
        /// </summary>
        private const string _MESSAGE_INSTALL_INT_MODEL = "Select a BNK file for interior model...";

        /// <summary>
        /// Browsing dialog message (front rims)
        /// </summary>
        private const string _MESSAGE_INSTALL_FRONT_RIMS = "Select a BNK file for front rims...";

        /// <summary>
        /// Browsing dialog message (rear rims)
        /// </summary>
        private const string _MESSAGE_INSTALL_REAR_RIMS = "Select a BNK file for rear rims...";

        /// <summary>
        /// Browsing dialog message (low gauges)
        /// </summary>
        private const string _MESSAGE_INSTALL_LOW_GAUGES = "Select a BNK file for low HUD...";

        /// <summary>
        /// Browsing dialog message (high gauges)
        /// </summary>
        private const string _MESSAGE_INSTALL_HIGH_GAUGES = "Select a BNK file for high HUD...";

        /// <summary>
        /// Browsing dialog message (sound)
        /// </summary>
        private const string _MESSAGE_INSTALL_SOUND = "Select a BNK file for sound...";
        #endregion

        #region Members
        /// <summary>
        /// Exterior model file
        /// </summary>
        internal string _InstallExtModelFile;

        /// <summary>
        /// Interior model file
        /// </summary>
        internal string _InstallIntModelFile;

        /// <summary>
        /// Front rims file
        /// </summary>
        internal string _InstallFrontRimsFile;

        /// <summary>
        /// Rear rims file
        /// </summary>
        internal string _InstallRearRimsFile;

        /// <summary>
        /// Low gauges file
        /// </summary>
        internal string _InstallLowGaugesFile;

        /// <summary>
        /// High gauges file
        /// </summary>
        internal string _InstallHighGaugesFile;

        /// <summary>
        /// Sound file
        /// </summary>
        internal string _InstallSoundFile;
        #endregion

        #region Events
        /// <summary>
        /// Manages the drag enter event for all picture boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _ManageDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop);
                string selectedFile = fileList[0];

                if (Regex.IsMatch(selectedFile, BNK.FILENAME_PATTERN,RegexOptions.IgnoreCase))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// Manages the drop event for all picture boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _ManageDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                Cursor = Cursors.WaitCursor;

                // Testing dropped file
                string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop);
                string bnkFileName = fileList[0];

                try
                {
                    BNK bnkFile = TduFile.GetFile(bnkFileName) as BNK;

                    if (bnkFile == null || !bnkFile.Exists)
                        throw new Exception();
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, new Exception(_ERROR_DROP_BNK_INVALID, ex));
                }

                // According to sender
                if (sender == extModelBox)
                {
                    _InstallExtModelFile = bnkFileName;
                    extModelCheckBox.Checked = true;
                }
                else if (sender == intModelBox)
                {
                    _InstallIntModelFile = bnkFileName;
                    intModelCheckBox.Checked = true;
                }
                else if (sender == rimsFrontBox)
                {
                    _InstallFrontRimsFile = bnkFileName;
                    rimsFrontCheckBox.Checked = true;
                }
                else if (sender == rimsRearBox)
                {
                    _InstallRearRimsFile = bnkFileName;
                    rimsRearCheckBox.Checked = true;
                }
                else if (sender == gaugesLowBox)
                {
                    _InstallLowGaugesFile = bnkFileName;
                    gaugesLowCheckBox.Checked = true;
                }
                else if (sender == gaugesHighBox)
                {
                    _InstallHighGaugesFile = bnkFileName;
                    gaugesHighCheckBox.Checked = true;
                }
                else if (sender == soundBox)
                {
                    _InstallSoundFile = bnkFileName;
                    soundCheckBox.Checked = true;
                }

                StatusBarLogManager.ShowEvent(this, _STATUS_SETTING_BNK_OK);
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Checks or unchecks all install check boxes
        /// </summary>
        /// <param name="isChecked"></param>
        internal void _ManageInstallCheckBoxes(bool isChecked)
        {
            extModelCheckBox.Checked = intModelCheckBox.Checked
                = rimsFrontCheckBox.Checked = rimsRearCheckBox.Checked
                = gaugesLowCheckBox.Checked = gaugesHighCheckBox.Checked = soundCheckBox.Checked
                = isChecked;
        }

        private void checkAllButton_Click(object sender, EventArgs e)
        {
            // Click on 'check all' button
            _ManageInstallCheckBoxes(true);
        }

        private void checkNoneButton_Click(object sender, EventArgs e)
        {
            // Click on 'check none' button
            _ManageInstallCheckBoxes(false);
        }

        private void installButton_Click(object sender, EventArgs e)
        {
            // Click on 'install' button
            try
            {
                Cursor = Cursors.WaitCursor;
                StatusBarLogManager.ShowEvent(this, _STATUS_INSTALLING);

                _InstallOrUpdateComponents(true);

                StatusBarLogManager.ShowEvent(this, _STATUS_INSTALL_OK);
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, new Exception(_ERROR_INSTALLING, ex));
            }
        }

        private void restoreButton_Click(object sender, EventArgs e)
        {
            // Click on 'Restore' button
            try
            {
                Cursor = Cursors.WaitCursor;
                StatusBarLogManager.ShowEvent(this, _STATUS_RESTORING);

                _RestoreComponents();

                StatusBarLogManager.ShowEvent(this, _STATUS_RESTORE_OK);
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, new Exception(_ERROR_RESTORING, ex));
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            // Click on 'Update' button
            try
            {
                Cursor = Cursors.WaitCursor;
                StatusBarLogManager.ShowEvent(this, _STATUS_UPDATING);

                _InstallOrUpdateComponents(false);

                StatusBarLogManager.ShowEvent(this, _STATUS_UPDATE_OK);
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, new Exception(_ERROR_UPDATING, ex));
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Defines tab contents
        /// </summary>
        private void _InitializeInstallContents()
        {
            if (_CurrentVehicle == null)
                return;

            rimsSetsComboBox.Items.Clear();

            // Unchecking all checkboxes
            _ManageInstallCheckBoxes(false);

            // Clearing all part file names
            _InstallExtModelFile =
                _InstallFrontRimsFile =
                _InstallHighGaugesFile =
                _InstallIntModelFile =
                _InstallLowGaugesFile =
                _InstallRearRimsFile =
                _InstallSoundFile =
                null;

            // Getting all rim sets for current vehicle
            Couple<string> cond1 = new Couple<string>(SharedConstants.CAR_CARRIMS_DB_COLUMN, _CurrentVehicle);
            List<DB.Cell> allVehicleSets =
                DatabaseHelper.SelectCellsFromTopicWhereCellValues(SharedConstants.RIMS_CARRIMS_DB_COLUMN, _CarRimsTable, cond1);

            // Browsing found rims sets
            foreach (DB.Cell anotherCell in allVehicleSets)
            {
                // Using rims Table
                string rimReference = anotherCell.value;
                DB.Cell rimNameCell =
                    DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.DISPLAY_NAME_RIMS_DB_COLUMN, _RimsTable, rimReference)[0];
                string rimText = DatabaseHelper.GetResourceValueFromCell(rimNameCell, _RimsResource);

                rimsSetsComboBox.Items.Add(string.Format(SharedConstants.FORMAT_REF_NAME_COUPLE, rimText,rimReference ));
            }

            // First set selected
            if (rimsSetsComboBox.Items.Count > 0)
                rimsSetsComboBox.Text = rimsSetsComboBox.Items[0].ToString();
        }

        /// <summary>
        /// Manages install process
        /// </summary>
        /// <param name="isInstall">true to install components, false to update them</param>
        private void _InstallOrUpdateComponents(bool isInstall)
        {
            // Getting slot file names from database
            // Model
            DB.Cell vehicleFileNameCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.CAR_FILE_NAME_PHYSICS_DB_COLUMN, _PhysicsTable, _CurrentVehicle)[0];
            string modelFileName = DatabaseHelper.GetResourceValueFromCell(vehicleFileNameCell, _PhysicsResource);
            // Rims: front
            string selectedSet = rimsSetsComboBox.Text.Split(Tools.SYMBOL_VALUE_SEPARATOR)[1];
            DB.Cell rimPathNameCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.RSC_PATH_RIMS_DB_COLUMN, _RimsTable, selectedSet)[0];
            DB.Cell frontRimFileNameCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.RSC_FILE_NAME_FRONT_RIMS_DB_COLUMN, _RimsTable, selectedSet)[0];
            DB.Cell rearRimFileNameCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.RSC_FILE_NAME_REAR_RIMS_DB_COLUMN, _RimsTable, selectedSet)[0];
            string rimPathName = DatabaseHelper.GetResourceValueFromCell(rimPathNameCell, _RimsResource);
            string frontRimsFileName = DatabaseHelper.GetResourceValueFromCell(frontRimFileNameCell, _RimsResource);
            string rearRimsFileName = DatabaseHelper.GetResourceValueFromCell(rearRimFileNameCell, _RimsResource);
            // Hud
            DB.Cell hudFileNameCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.HUD_FILE_NAME_PHYSICS_DB_COLUMN, _PhysicsTable, _CurrentVehicle)[0];
            string hudFileName = DatabaseHelper.GetResourceValueFromCell(hudFileNameCell, _PhysicsResource);
            // Sound is based upon model

            // Retrieving files according to checked components...
            // Models
            string targetPath = Program.ApplicationSettings.TduMainFolder + LibraryConstants.FOLDER_VEHICLES_MODELS;

            if (extModelCheckBox.Checked)
            {
                // Exterior
                if (_InstallExtModelFile == null)
                    _InstallExtModelFile = _BrowseBnkFile(_MESSAGE_INSTALL_EXT_MODEL);

                string extModelFileName = targetPath + string.Format(SharedConstants._FORMAT_EXT_MODEL_NAME, modelFileName);

                if (isInstall)
                    _BackupAndCopy(_InstallExtModelFile, extModelFileName);
                else
                    _Replace(extModelFileName, _InstallExtModelFile);
            }
            if (intModelCheckBox.Checked)
            {
                // Interior
                if (_InstallIntModelFile == null)
                    _InstallIntModelFile = _BrowseBnkFile(_MESSAGE_INSTALL_INT_MODEL);

                string intModelFileName = targetPath + string.Format(SharedConstants._FORMAT_INT_MODEL_NAME, modelFileName);

                if (isInstall)
                    _BackupAndCopy(_InstallIntModelFile, intModelFileName);
                else
                    _Replace(intModelFileName, _InstallIntModelFile);
            }

            // Rims
            targetPath = Program.ApplicationSettings.TduMainFolder + LibraryConstants.FOLDER_PARENT_VEHICLES_RIMS + rimPathName +
                         @"\";
            if (rimsFrontCheckBox.Checked)
            {
                // Front
                if (_InstallFrontRimsFile == null)
                    _InstallFrontRimsFile = _BrowseBnkFile(_MESSAGE_INSTALL_FRONT_RIMS);

                string rimFileName = targetPath + string.Format(SharedConstants._FORMAT_EXT_MODEL_NAME, frontRimsFileName);

                if (isInstall)
                    _BackupAndCopy(_InstallFrontRimsFile, rimFileName);
                else
                    _Replace(rimFileName,_InstallFrontRimsFile);
            }
            if (rimsRearCheckBox.Checked && !frontRimsFileName.Equals(rearRimsFileName))
            {
                // Rear
                if (_InstallRearRimsFile == null)
                    _InstallRearRimsFile = _BrowseBnkFile(_MESSAGE_INSTALL_REAR_RIMS);

                string rimFileName = targetPath + string.Format(SharedConstants._FORMAT_EXT_MODEL_NAME, rearRimsFileName);

                if (isInstall)
                    _BackupAndCopy(_InstallRearRimsFile, rimFileName);
                else
                    _Replace(rimFileName,_InstallRearRimsFile);
            }

            // Gauges
            if (gaugesLowCheckBox.Checked)
            {
                // Low
                targetPath = Program.ApplicationSettings.TduMainFolder + LibraryConstants.FOLDER_VEHICLES_GAUGES_LOW;
                if (_InstallLowGaugesFile == null)
                    _InstallLowGaugesFile = _BrowseBnkFile(_MESSAGE_INSTALL_LOW_GAUGES);

                string gaugesFileName = targetPath + string.Format(SharedConstants._FORMAT_EXT_MODEL_NAME, hudFileName);

                if (isInstall)
                    _BackupAndCopy(_InstallLowGaugesFile, gaugesFileName);
                else
                    _Replace(gaugesFileName,_InstallLowGaugesFile);
            }
            if (gaugesHighCheckBox.Checked)
            {
                // High
                targetPath = Program.ApplicationSettings.TduMainFolder + LibraryConstants.FOLDER_VEHICLES_GAUGES_HIGH;
                if (_InstallHighGaugesFile == null)
                    _InstallHighGaugesFile = _BrowseBnkFile(_MESSAGE_INSTALL_HIGH_GAUGES);

                string gaugesFileName = targetPath + string.Format(SharedConstants._FORMAT_EXT_MODEL_NAME, hudFileName);

                if (isInstall)
                    _BackupAndCopy(_InstallHighGaugesFile, gaugesFileName);
                else
                    _Replace(gaugesFileName,_InstallHighGaugesFile);
            }

            // Sound
            if (soundCheckBox.Checked)
            {
                targetPath = Program.ApplicationSettings.TduMainFolder + LibraryConstants.FOLDER_VEHICLES_SOUNDS;
                if (_InstallSoundFile == null)
                    _InstallSoundFile = _BrowseBnkFile(_MESSAGE_INSTALL_SOUND);

                string soundFileName = targetPath + string.Format(SharedConstants._FORMAT_SOUND_NAME, modelFileName);

                if (isInstall)
                    _BackupAndCopy(_InstallSoundFile, soundFileName);
                else
                    _Replace(soundFileName, _InstallSoundFile);
            }

            // Removes all file information
            _InstallExtModelFile =
                _InstallFrontRimsFile =
                _InstallHighGaugesFile =
                _InstallIntModelFile =
                _InstallLowGaugesFile =
                _InstallRearRimsFile =
                _InstallSoundFile =
                null;
        }

        /// <summary>
        /// Manages restore process
        /// </summary>
        private void _RestoreComponents()
        {
            // Getting slot file names from database
            // Model
            DB.Cell vehicleFileNameCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.CAR_FILE_NAME_PHYSICS_DB_COLUMN, _PhysicsTable, _CurrentVehicle)[0];
            string modelFileName = DatabaseHelper.GetResourceValueFromCell(vehicleFileNameCell, _PhysicsResource);
            // Rims: front
            string selectedSet = rimsSetsComboBox.Text.Split(Tools.SYMBOL_VALUE_SEPARATOR)[1];
            DB.Cell rimPathNameCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.RSC_PATH_RIMS_DB_COLUMN, _RimsTable, selectedSet)[0];
            DB.Cell frontRimFileNameCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.RSC_FILE_NAME_FRONT_RIMS_DB_COLUMN, _RimsTable, selectedSet)[0];
            DB.Cell rearRimFileNameCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.RSC_FILE_NAME_REAR_RIMS_DB_COLUMN, _RimsTable, selectedSet)[0];
            string rimPathName = DatabaseHelper.GetResourceValueFromCell(rimPathNameCell, _RimsResource);
            string frontRimsFileName = DatabaseHelper.GetResourceValueFromCell(frontRimFileNameCell, _RimsResource);
            string rearRimsFileName = DatabaseHelper.GetResourceValueFromCell(rearRimFileNameCell, _RimsResource);
            // Hud
            DB.Cell hudFileNameCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.HUD_FILE_NAME_PHYSICS_DB_COLUMN, _PhysicsTable, _CurrentVehicle)[0];
            string hudFileName = DatabaseHelper.GetResourceValueFromCell(hudFileNameCell, _PhysicsResource);
            // Sound is based upon model

            // Retrieving files according to checked components...
            // Models
            string targetPath = Program.ApplicationSettings.TduMainFolder + LibraryConstants.FOLDER_VEHICLES_MODELS;

            if (extModelCheckBox.Checked)
            {
                // Exterior
                string extModelFileName = targetPath + string.Format(SharedConstants._FORMAT_EXT_MODEL_NAME, modelFileName);

                _RestoreFile(extModelFileName);
            }
            if (intModelCheckBox.Checked)
            {
                // Interior
                string intModelFileName = targetPath + string.Format(SharedConstants._FORMAT_INT_MODEL_NAME, modelFileName);

                _RestoreFile(intModelFileName);
            }

            // Rims
            targetPath = Program.ApplicationSettings.TduMainFolder + LibraryConstants.FOLDER_PARENT_VEHICLES_RIMS + rimPathName +
                         @"\";
            if (rimsFrontCheckBox.Checked)
            {
                // Front
                string rimFileName = targetPath + string.Format(SharedConstants._FORMAT_EXT_MODEL_NAME, frontRimsFileName);

                _RestoreFile(rimFileName);
            }
            if (rimsRearCheckBox.Checked && !frontRimsFileName.Equals(rearRimsFileName))
            {
                // Rear
                string rimFileName = targetPath + string.Format(SharedConstants._FORMAT_EXT_MODEL_NAME, rearRimsFileName);

                _RestoreFile(rimFileName);
            }

            // Gauges
            if (gaugesLowCheckBox.Checked)
            {
                // Low
                targetPath = Program.ApplicationSettings.TduMainFolder + LibraryConstants.FOLDER_VEHICLES_GAUGES_LOW;

                string gaugesFileName = targetPath + string.Format(SharedConstants._FORMAT_EXT_MODEL_NAME, hudFileName);

                _RestoreFile(gaugesFileName);
            }
            if (gaugesHighCheckBox.Checked)
            {
                // High
                targetPath = Program.ApplicationSettings.TduMainFolder + LibraryConstants.FOLDER_VEHICLES_GAUGES_HIGH;

                string gaugesFileName = targetPath + string.Format(SharedConstants._FORMAT_EXT_MODEL_NAME, hudFileName);

                _RestoreFile(gaugesFileName);
            }

            // Sound
            if (soundCheckBox.Checked)
            {
                targetPath = Program.ApplicationSettings.TduMainFolder + LibraryConstants.FOLDER_VEHICLES_SOUNDS;

                string soundFileName = targetPath + string.Format(SharedConstants._FORMAT_SOUND_NAME, modelFileName);

                _RestoreFile(soundFileName);
            }
        }

        /// <summary>
        /// Provides a dialog box to get BNK file
        /// </summary>
        /// <param name="message">message to display</param>
        /// <returns></returns>
        private string _BrowseBnkFile(string message)
        {
            string returnValue = null;

            // Parameters
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = GuiConstants.FILTER_BNK_ALL_FILES;
            openFileDialog.Title = message;

            DialogResult dr = openFileDialog.ShowDialog(this);

            if (dr == DialogResult.OK)
                returnValue = openFileDialog.FileName;

            return returnValue;
        }

        /// <summary>
        /// Backups target file then replaces it by file to install
        /// </summary>
        /// <param name="fileToInstall"></param>
        /// <param name="targetFileName"></param>
        private static void _BackupAndCopy(string fileToInstall, string targetFileName)
        {
            if (string.IsNullOrEmpty(fileToInstall) || string.IsNullOrEmpty(targetFileName))
                return;

            try
            {
                // Does backup already exist ?
                string backupFileName = targetFileName + "." + EXTENSION_BACKUP;

                if (File.Exists(backupFileName))
                    throw new Exception(_ERROR_BACKUP_EXISTS + ": " + backupFileName);

                // Removing read-only attribute
                File2.RemoveAttribute(targetFileName, FileAttributes.ReadOnly);

                // Backup
                Tools.BackupFile(targetFileName, backupFileName);

                // Copy
                File.Copy(fileToInstall, targetFileName, true);
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowWarning(_Instance, ex.Message);
            }
        }

        /// <summary>
        /// Backups target file then replaces it by file to install
        /// </summary>
        /// <param name="targetFileName">File to replace</param>
        /// <param name="fileToInstall">New file</param>
        private static void _Replace(string targetFileName, string fileToInstall)
        {
            if (string.IsNullOrEmpty(fileToInstall) || string.IsNullOrEmpty(targetFileName))
                return;

            try
            {
                // Does backup already exist ?
                string backupFileName = targetFileName + "." + EXTENSION_BACKUP;

                if (!File.Exists(backupFileName))
                    throw new Exception(_ERROR_NOTHING_TO_UPDATE + ": " + targetFileName);

                // Removing read-only attribute
                File2.RemoveAttribute(targetFileName, FileAttributes.ReadOnly);

                // Copy
                File.Copy(fileToInstall, targetFileName, true);
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowWarning(_Instance, ex.Message);
            }
        }

        /// <summary>
        /// Renames backup over modified file
        /// </summary>
        private static void _RestoreFile(string targetFileName)
        {
            if (string.IsNullOrEmpty(targetFileName))
                return;

            try
            {
                // Does backup exist ?
                string backupFileName = targetFileName + "." + EXTENSION_BACKUP;

                if (!File.Exists(backupFileName))
                    throw new Exception(_ERROR_BACKUP_NOT_EXISTS + ": " + backupFileName);

                Tools.RestoreFile(backupFileName, targetFileName);
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowWarning(_Instance, ex.Message);
            }
        }
        #endregion
    }
}
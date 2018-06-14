using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Common.Types.Forms;
using TDUModdingLibrary.fileformats.binaries;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.fileformats.database.util;
using TDUModdingLibrary.support;
using TDUModdingTools.gui.wizards.vehiclemanager.cameraIK;

namespace TDUModdingTools.gui.wizards.vehiclemanager
{
    /// <summary>
    /// Class part to handle Camera-IK tab
    /// </summary>
    partial class VehicleManagerForm
    {
        #region Constants
        /// <summary>
        /// Label for unknown camera/ik
        /// </summary>
        private const string _LABEL_UNKNOWN = "Unknown";

        /// <summary>
        /// Label for custom camera
        /// </summary>
        private const string _LABEL_CUSTOM = "Custom";

        /// <summary>
        /// Label for default camera view
        /// </summary>
        private const string _LABEL_DEFAULT_CAM_VIEW = "Default view";

        /// <summary>
        /// Label when custom cam isn't set
        /// </summary>
        private const string _LABEL_NO_CAM = "-";

        /// <summary>
        /// Custom set name format string
        /// </summary>
        private readonly static string _FORMAT_CAMIK_SET_NAME = "{0}"+ Tools.SYMBOL_VALUE_SEPARATOR + "{1}";

        /// <summary>
        /// Custom camera view format string
        /// </summary>
        private const string _FORMAT_CAM_VIEW = "{0}->{1}";

        /// <summary>
        /// Status message when easy camera and ik setting process ended without errors
        /// </summary>
        private const string _STATUS_SETTING_EASY_CAMIK_OK =
            "Camera and IK were set on selected slot succesfully.";

        /// <summary>
        /// Status message when resetting camera and ik setting process ended without errors
        /// </summary>
        private const string _STATUS_RESETTING_CAMIK_OK =
            "Camera and IK were set back to defaults succesfully.";

        /// <summary>
        /// Status message when camera setting process ended without errors
        /// </summary>
        private const string _STATUS_SETTING_CAM_OK =
            "Camera was set on selected slot succesfully.";

        /// <summary>
        /// Status message when custom camera setting process ended without errors
        /// </summary>
        private const string _STATUS_SETTING_CUSTOM_CAM_OK =
            "Custom camera was set on selected slot succesfully.";

        /// <summary>
        /// Status message when custom camera un-setting process ended without errors
        /// </summary>
        private const string _STATUS_UNSETTING_CUSTOM_CAM_OK =
            "Default camera was set on selected slot succesfully.";

        /// <summary>
        /// Status message when IK setting process ended without errors
        /// </summary>
        private const string _STATUS_SETTING_IK_OK =
            "IK was set on selected slot succesfully.";

        /// <summary>
        /// Status message when custom view setting process ended without errors
        /// </summary>
        private const string _STATUS_CHOOSING_VIEW_OK = "Current view was set succesfully.";

        /// <summary>
        /// Status message when custom view reseting process ended without errors
        /// </summary>
        private const string _STATUS_RESETTING_VIEW_OK = "Default views were restored succesfully.";

        /// <summary>
        /// Status message when custom view position setting process ended without errors
        /// </summary>
        private const string _STATUS_CHANGING_VIEW_POS_OK = "Camera position was changed succesfully.";

        /// <summary>
        /// Error message when own camera was not found
        /// </summary>
        private const string _ERROR_NEW_CAM_NOT_FOUND =
            "Error in camera information: unable to get data for own camera.";

        /// <summary>
        /// Left side camera position
        /// </summary>
        private static readonly string _CAMERA_COCKPIT_POSITION_LEFT = Cameras.Position.Left.ToString();

        /// <summary>
        /// Middle camera position
        /// </summary>
        private static readonly string _CAMERA_COCKPIT_POSITION_MIDDLE = Cameras.Position.Middle.ToString();

        /// <summary>
        /// Right side camera position
        /// </summary>
        private static readonly string _CAMERA_COCKPIT_POSITION_RIGHT = Cameras.Position.Right.ToString();
        #endregion

        #region Members
        /// <summary>
        /// View selector dialog
        /// </summary>
        private CamSelectorDialog _CamViewSelectorDialog;
        #endregion

        #region Events
        private void easySetButton_Click(object sender, EventArgs e)
        {
            // Click on 'Set' button (easy way)
            if (!string.IsNullOrEmpty(easyVehicleComboBox.Text))
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    // Action
                    _SetEasyCamIK(_CurrentVehicle, easyVehicleComboBox.Text, _PhysicsTable);

                    // Refresh
                    _RefreshCameraIKContents();

                    // Modification flag
                    _IsDatabaseModified = true;

                    StatusBarLogManager.ShowEvent(this, _STATUS_SETTING_EASY_CAMIK_OK);

                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void customSetButton_Click(object sender, EventArgs e)
        {
            // Click on 'Apply' button (custom mode)
            // Displays context menu
            customSetButton.ContextMenuStrip.Show(customSetButton.PointToScreen(new Point()));
        }

        private void resetCameraIKButton_Click(object sender, EventArgs e)
        {
            // Click on 'Reset All' button
            try
            {
                Cursor = Cursors.WaitCursor;

                // If custom camera is used, it is unchecked
                if (useOwnCamCheckBox.Checked)
                    useOwnCamCheckBox.Checked = false;

                // Action
                _ResetCamIK(_CurrentVehicle, _PhysicsTable);

                // Refresh
                _RefreshCameraIKContents();

                // Modification flag
                _IsDatabaseModified = true;

                StatusBarLogManager.ShowEvent(this, _STATUS_RESETTING_CAMIK_OK);

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void cameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Apply>Camera'
            if (!string.IsNullOrEmpty(availableCamIKComboBox.Text))
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    // If custom camera is used, it is unchecked
                    if (useOwnCamCheckBox.Checked)
                        useOwnCamCheckBox.Checked = false;

                    // Action
                    _SetCamOrIK(_CurrentVehicle, availableCamIKComboBox.Text, _PhysicsTable, true);

                    // Refresh
                    _RefreshCameraIKContents();

                    // Modification flag
                    _IsDatabaseModified = true;

                    StatusBarLogManager.ShowEvent(this, _STATUS_SETTING_CAM_OK);

                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void ikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Apply>IK'
            if (!string.IsNullOrEmpty(availableCamIKComboBox.Text))
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    // Action
                    _SetCamOrIK(_CurrentVehicle, availableCamIKComboBox.Text, _PhysicsTable, false);

                    // Refresh
                    _RefreshCameraIKContents();

                    // Modification flag
                    _IsDatabaseModified = true;

                    StatusBarLogManager.ShowEvent(this, _STATUS_SETTING_IK_OK);

                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }
   
        private void useCustomCamCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Click on 'Use own camera set' checkbox
            if (useOwnCamCheckBox.Checked)
            {
                // Vehicle now uses its own camera
                try
                {
                    Cursor = Cursors.WaitCursor;

                    // Action
                    VehicleSlotsHelper.VehicleInfo vi = VehicleSlotsHelper.VehicleInformation[_CurrentVehicle];

                    VehicleSlotsHelper.ChangeCameraById(_CurrentVehicle, vi.newCamera, _PhysicsTable);

                    // Refresh
                    _RefreshCameraIKContents();

                    // Modification flag
                    _IsDatabaseModified = true;

                    StatusBarLogManager.ShowEvent(this, _STATUS_SETTING_CUSTOM_CAM_OK);

                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
            else
            {
                // Restoring default camera
                Cursor = Cursors.WaitCursor;

                // Action
                VehicleSlotsHelper.VehicleInfo vi = VehicleSlotsHelper.VehicleInformation[_CurrentVehicle];
                short defaultCamera = short.Parse(vi.defaultCamera);

                VehicleSlotsHelper.ChangeCameraById(_CurrentVehicle, defaultCamera.ToString(), _PhysicsTable);

                // Refresh
                _RefreshCameraIKContents();

                // Modification flag
                _IsDatabaseModified = true;

                StatusBarLogManager.ShowEvent(this, _STATUS_UNSETTING_CUSTOM_CAM_OK);

                Cursor = Cursors.Default;
            }
        }

        private void viewChooseButton_Click(object sender, EventArgs e)
        {
            // Click on 'Choose front/rear...' buttons
            if (camSetDetailsListView.SelectedItems.Count == 1)
            {
                try
                {
                    bool isForFront = (sender == viewChooseFrontButton);
                    DialogResult dr = _CamViewSelectorDialog.ShowDialog(this);

                    if (dr == DialogResult.OK)
                    {
                        Cursor = Cursors.WaitCursor;

                        Cameras.ViewType currentViewType = (Cameras.ViewType) Enum.Parse(typeof(Cameras.ViewType), camSetDetailsListView.SelectedItems[0].Tag.ToString());

                        if (_CamViewSelectorDialog.ChosenCameraId == null && _CamViewSelectorDialog.ChosenViewId == null)
                            // View deletion
                            _RemoveCameraView(customCameraIdLabel.Text, currentViewType, isForFront);
                        else
                        {
                            // View customization
                            Cameras.ViewType chosenViewType = (Cameras.ViewType) Enum.Parse(typeof(Cameras.ViewType), _CamViewSelectorDialog.ChosenViewId);

                            _CustomizeCameraView(customCameraIdLabel.Text,
                                                 currentViewType,
                                                 _CamViewSelectorDialog.ChosenCameraId,
                                                 chosenViewType, isForFront);
                        }

                        // Refresh
                        _RefreshCameraIKContents();

                        // Modification flag
                        _IsCameraModified = true;

                        StatusBarLogManager.ShowEvent(this, _STATUS_CHOOSING_VIEW_OK);
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

        private void viewResetAllButton_Click(object sender, EventArgs e)
        {
            // Click on 'Reset All...' button
            if (camSetDetailsListView.Items.Count > 0)
            {
                try
                {
                    // Hood front
                    _CustomizeCameraView(customCameraIdLabel.Text,
                                         Cameras.ViewType.Hood,
                                         customCameraIdLabel.Text,
                                         Cameras.ViewType.Hood, true);

                    // Hood back
                    _CustomizeCameraView(customCameraIdLabel.Text,
                                         Cameras.ViewType.Hood,
                                         customCameraIdLabel.Text,
                                         Cameras.ViewType.Hood_Back, false);

                    // Cockpit front
                    _CustomizeCameraView(customCameraIdLabel.Text,
                                         Cameras.ViewType.Cockpit,
                                         customCameraIdLabel.Text,
                                         Cameras.ViewType.Cockpit, true);

                    // Cockpit back
                    _CustomizeCameraView(customCameraIdLabel.Text,
                                         Cameras.ViewType.Cockpit,
                                         customCameraIdLabel.Text,
                                         Cameras.ViewType.Cockpit_Back, false);

                    // Refresh
                    _RefreshCameraIKContents();

                    // Modification flag
                    _IsCameraModified = true;

                    StatusBarLogManager.ShowEvent(this, _STATUS_RESETTING_VIEW_OK);
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

        private void cockSetButton_Click(object sender, EventArgs e)
        {
            // Click on Set cock position button
            try
            {
                Cursor = Cursors.WaitCursor;

                Cameras.Position sourcePos =
                    (Cameras.Position) Enum.Parse(typeof (Cameras.Position), cockSourceComboBox.Text);
                Cameras.Position targetPos =
                    (Cameras.Position)Enum.Parse(typeof(Cameras.Position), cockTargetComboBox.Text);
                Cameras.CamEntry currentEntry = _CameraData.GetEntryByCameraId(customCameraIdLabel.Text);

                if (currentEntry.isValid)
                {
                    VehicleSlotsHelper.CustomizeCameraPosition(_CameraData, currentEntry, Cameras.ViewType.Cockpit,
                                                               sourcePos, targetPos);

                    // Refresh
                    _RefreshCameraIKContents();

                    // Modification flag
                    _IsCameraModified = true;

                    StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_VIEW_POS_OK);
                }
            }
            catch(Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Defines tab contents
        /// </summary>
        private void _InitializeCameraIKContents()
        {
            // Available ids
            easyVehicleComboBox.Items.Clear();
            availableCamIKComboBox.Items.Clear();

            foreach (KeyValuePair<string, string> pair in VehicleSlotsHelper.CamReference)
            {
                string itemText = string.Format(_FORMAT_CAMIK_SET_NAME, pair.Value, pair.Key);
                
                // Easy list only shows moddable vehicles
                string vehicleRef = VehicleSlotsHelper.SlotReference[pair.Value];
                VehicleSlotsHelper.VehicleInfo info = VehicleSlotsHelper.VehicleInformation[vehicleRef];

                if (info.isModdable)
                    easyVehicleComboBox.Items.Add(itemText);

                // Classic list
                availableCamIKComboBox.Items.Add(itemText);
            }

            // Cockpit positions
            cockSourceComboBox.Items.Add(_CAMERA_COCKPIT_POSITION_LEFT);
            cockSourceComboBox.Items.Add(_CAMERA_COCKPIT_POSITION_MIDDLE);
            cockSourceComboBox.Items.Add(_CAMERA_COCKPIT_POSITION_RIGHT);
            cockTargetComboBox.Items.Add(_CAMERA_COCKPIT_POSITION_LEFT);
            cockTargetComboBox.Items.Add(_CAMERA_COCKPIT_POSITION_MIDDLE);
            cockTargetComboBox.Items.Add(_CAMERA_COCKPIT_POSITION_RIGHT);
        }

        /// <summary>
        /// Refreshes tab contents
        /// </summary>
        private void _RefreshCameraIKContents()
        {
            // Current camera
            DB.Cell cameraRefCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(DatabaseConstants.CAMERA_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable,
                                                                   _CurrentVehicle)[0];
            string cameraRef = cameraRefCell.value;

            try
            {
                string customCamId = VehicleSlotsHelper.VehicleInformation[_CurrentVehicle].newCamera;

                if (cameraRef.Equals(customCamId))
                {
                    cameraLabel.Text = _LABEL_CUSTOM;
                    useOwnCamCheckBox.Checked = true;
                }
                else
                {
                    cameraLabel.Text = VehicleSlotsHelper.CamReference[cameraRef];
                    useOwnCamCheckBox.Checked = false;
                }
            }
            catch (Exception)
            {
                cameraLabel.Text = _LABEL_UNKNOWN;
                useOwnCamCheckBox.Checked = false;
            }

            // Current IK
            DB.Cell ikRefCell =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(DatabaseConstants.SAME_IK_PHYSICS_DB_COLUMN,
                                                                   _PhysicsTable,
                                                                   _CurrentVehicle)[0];
            string ikRef = ikRefCell.value;

            try
            {
                ikLabel.Text = VehicleSlotsHelper.IKReference[ikRef];
            }
            catch (Exception)
            {
                ikLabel.Text = _LABEL_UNKNOWN;
            }

            // New set id (own)
            VehicleSlotsHelper.VehicleInfo currentInfo = VehicleSlotsHelper.VehicleInformation[_CurrentVehicle];

            // No new camera for this slot; checkbox disabled
            useOwnCamCheckBox.Enabled = (currentInfo.newCamera != null);

            ListView2.StoreSelectedIndex(camSetDetailsListView);
            camSetDetailsListView.Items.Clear();

            if (cameraRef == currentInfo.defaultCamera || cameraRef == currentInfo.newCamera)
            {
                advancedCameraControlsPanel.Enabled = true;
                customCameraIdLabel.Text = cameraRef;
                useOwnCamCheckBox.Checked = (cameraRef == currentInfo.newCamera);

                // Set customization: only hood and cockpit views are handled
                Cameras.CamEntry customCam = _CameraData.GetEntryByCameraId(cameraRef);

                if (customCam.isValid)
                {
                    useOwnCamCheckBox.Enabled = true;

                    // Hood
                    ListViewItem customItem = new ListViewItem(Cameras.ViewType.Hood.ToString())
                                                  {
                                                      UseItemStyleForSubItems = false,
                                                      Font = new Font(camSetDetailsListView.Font, FontStyle.Bold),
                                                      Tag = Cameras.ViewType.Hood
                                                  };
                    Cameras.View currentView = Cameras.GetViewByType(customCam, Cameras.ViewType.Hood);
                    string customViewLabel = _GetCustomViewLabel(currentView);

                    customItem.SubItems.Add(customViewLabel);

                    currentView = Cameras.GetViewByType(customCam, Cameras.ViewType.Hood_Back);
                    customViewLabel = _GetCustomViewLabel(currentView);
                    customItem.SubItems.Add(customViewLabel);

                    camSetDetailsListView.Items.Add(customItem);

                    // Cockpit
                    customItem = new ListViewItem(Cameras.ViewType.Cockpit.ToString())
                                     {
                                         UseItemStyleForSubItems = false,
                                         Font = new Font(camSetDetailsListView.Font, FontStyle.Bold),
                                         Tag = Cameras.ViewType.Cockpit
                                     };
                    currentView = Cameras.GetViewByType(customCam, Cameras.ViewType.Cockpit);
                    customViewLabel = _GetCustomViewLabel(currentView);
                    customItem.SubItems.Add(customViewLabel);

                    currentView = Cameras.GetViewByType(customCam, Cameras.ViewType.Cockpit_Back);
                    customViewLabel = _GetCustomViewLabel(currentView);
                    customItem.SubItems.Add(customViewLabel);

                    camSetDetailsListView.Items.Add(customItem);

                    // Cockpit positions
                    Cameras.View cockpitView = Cameras.GetViewByType(customCam, Cameras.ViewType.Cockpit);

                    cockSourceComboBox.Text = cockpitView.source.ToString();
                    cockTargetComboBox.Text = cockpitView.target.ToString();
                }
                else
                {
                    Log.Warning(_ERROR_NEW_CAM_NOT_FOUND);

                    // No new camera for this slot; checkbox disabled
                    useOwnCamCheckBox.Enabled = false;
                }
            }
            else
            {
                // Other camera can't be modified
                customCameraIdLabel.Text = _LABEL_NO_CAM;
                advancedCameraControlsPanel.Enabled = false;
            }

            ListView2.RestoreSelectedIndex(camSetDetailsListView);

        }

        /// <summary>
        /// Sets new view on a custom camera
        /// </summary>
        /// <param name="cameraId"></param>
        /// <param name="viewType"></param>
        /// <param name="newCameraId"></param>
        /// <param name="newViewType"></param>
        /// <param name="isForFront"></param>
        private void _CustomizeCameraView(string cameraId, Cameras.ViewType viewType, string newCameraId, Cameras.ViewType newViewType, bool isForFront)
        {
            if (!string.IsNullOrEmpty(cameraId) && !string.IsNullOrEmpty(newCameraId))
            {
                // Getting entry
                Cameras.CamEntry currentEntry = _CameraData.GetEntryByCameraId(cameraId);

                if (currentEntry.isValid)
                {
                    // Goal is to replace current view (front or back one) with specified
                    Cameras.CamEntry baseEntry = VehicleSlotsHelper.DefaultCameras.GetEntryByCameraId(newCameraId);
                    Cameras.ViewType currentViewType = (isForFront ? viewType : viewType + 20 );

                    if (baseEntry.isValid)
                        VehicleSlotsHelper.CustomizeCameraView(_CameraData, currentEntry, currentViewType, baseEntry, newViewType);
                }
            }
        }

        /// <summary>
        /// Removes specified view from camera
        /// </summary>
        /// <param name="cameraId"></param>
        /// <param name="viewType"></param>
        /// <param name="isForFront"></param>
        private void _RemoveCameraView(string cameraId, Cameras.ViewType viewType, bool isForFront)
        {
            if (!string.IsNullOrEmpty(cameraId) && viewType != Cameras.ViewType.Unknown)
            {
                // Getting entry
                Cameras.CamEntry currentEntry = _CameraData.GetEntryByCameraId(cameraId);

                if (currentEntry.isValid)
                {
                    Cameras.ViewType currentViewType = (isForFront ? viewType : viewType + 20);

                    _CameraData.RemoveView(currentEntry, currentViewType);
                }
            }        
        }

        /// <summary>
        /// Returns label for custom view
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        private static string _GetCustomViewLabel(Cameras.View view)
        {
            string returnedLabel = _LABEL_DEFAULT_CAM_VIEW;

            if (view.isValid)
            {
                // Getting view information
                ushort parentCameraId = view.parentCameraId;
                ushort cameraId = view.cameraId;
                Cameras.ViewType viewType = view.type;

                if (parentCameraId != 0)
                {
                    cameraId = view.parentCameraId;
                    viewType = view.parentType;
                }

                string parentVehicleName = null;

                if (VehicleSlotsHelper.CamReference.ContainsKey(cameraId.ToString()))
                    parentVehicleName = VehicleSlotsHelper.CamReference[cameraId.ToString()];
                else if (VehicleSlotsHelper.NewCamReference.ContainsKey(cameraId.ToString()))
                    parentVehicleName = VehicleSlotsHelper.NewCamReference[cameraId.ToString()];

                if (parentVehicleName != null)
                    returnedLabel = string.Format(_FORMAT_CAM_VIEW, parentVehicleName, viewType);
            }

            return returnedLabel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="cameraIK"></param>
        /// <param name="physicsTable"></param>
        private static void _SetEasyCamIK(string vehicle, string cameraIK, DB physicsTable)
        {
            string chosenObject = cameraIK.Split(Tools.SYMBOL_VALUE_SEPARATOR)[0];

            VehicleSlotsHelper.ChangeCameraByVehicleName(vehicle, chosenObject, physicsTable);
            VehicleSlotsHelper.ChangeIKByVehicleName(vehicle, chosenObject, physicsTable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="physicsTable"></param>
        private static void _ResetCamIK(string vehicle, DB physicsTable)
        {
            VehicleSlotsHelper.VehicleInfo vi = VehicleSlotsHelper.VehicleInformation[vehicle];

            VehicleSlotsHelper.ChangeCameraById(vehicle, vi.defaultCamera, physicsTable);
            VehicleSlotsHelper.ChangeIKById(vehicle, vi.defaultIK, physicsTable);
        }

        /// <summary>
        /// Sets camera or IK for specified slot according to combo list item
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="cameraOrIK"></param>
        /// <param name="physicsTable"></param>
        /// <param name="isCam"></param>
        private static void _SetCamOrIK(string vehicle, string cameraOrIK, DB physicsTable, bool isCam)
        {
            string chosenObject = cameraOrIK.Split(Tools.SYMBOL_VALUE_SEPARATOR)[0];

            if (isCam)
                VehicleSlotsHelper.ChangeCameraByVehicleName(vehicle, chosenObject, physicsTable);
            else
                VehicleSlotsHelper.ChangeIKByVehicleName(vehicle, chosenObject, physicsTable);
        }        
        #endregion
    }
}
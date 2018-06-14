using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Types.Forms;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.fileformats.database.util;
using TDUModdingLibrary.support;
using TDUModdingTools.gui.wizards.vehiclemanager.common;

namespace TDUModdingTools.gui.wizards.vehiclemanager
{
    public partial class VehicleManagerForm
    {
        #region Constants
        /// <summary>
        /// Name for a free slot
        /// </summary>
        private const string _FREE_SLOT_NAME = "Free slot";

        /// <summary>
        /// List of all invalid spot references
        /// </summary>
        private static readonly string[] LIST_INVALID_SPOTS = new[]
            {"57043918", "53973915", "54123921",
                "54234917","59224204", "60364261",
                "58864208", "60506209", "56126206",
                "58866207", "55127205", "57897202",
                "59507207", "60579206", "60149206",
                "58228203", "60578211", "60928209",
                "60372259", "58230202", "59970204",
                "55830206", "60505210", "55125206", 
                "58865209", "59157604", "55133204"};
        #endregion

        #region Members
        /// <summary>
        /// Reference of spot (dealer/V-rent) which is currently displayed
        /// </summary>
        private string _CurrentAvailabilitySpot;
        #endregion

        #region Private methods
        /// <summary>
        /// Defines tab contents
        /// </summary>
        private void _InitializeDealersContents()
        {
            sellingComboBox.Items.Clear();
            rentingComboBox.Items.Clear();
            allSellingRentingComboBox.Items.Clear();
            spotVehiclesListView.Items.Clear();

            List<DB.Entry> allShops = DatabaseHelper.SelectAllCellsFromTopic(_CarShopsTable);

            foreach (DB.Entry currentEntry in allShops)
            {
                // Bitfield
                //bool[] currentBitfield = DatabaseHelper.ParseBitField(currentEntry[17]);

                // Add to all spots
                string currentShopRef = currentEntry.primaryKey;
                DB.Cell shopLabelCell =
                    DatabaseHelper.GetCellFromEntry(_CarShopsTable, currentEntry,
                                                    SharedConstants.LIBELLE_CARSHOPS_DB_COLUMN);
                string currentShopLabel = DatabaseHelper.GetResourceValueFromCell(shopLabelCell, _CarShopsResource);

                // All invalid spots are filtered
                List<string> invalidSpots = new List<string>(LIST_INVALID_SPOTS);

                if (!invalidSpots.Contains(currentShopRef))
                {
                    string currentItemLabel = string.Format(SharedConstants.FORMAT_REF_NAME_COUPLE, currentShopLabel, currentShopRef);

                    allSellingRentingComboBox.Items.Add(currentItemLabel);

                    // Vehicle refs for this spot
                    List<string> refs = new List<string>();

                    for (int col = 3; col < 18; col++)
                        refs.Add(currentEntry.cells[col].value);

                    // Does current vehicle belong to this spot ?
                    if (refs.Contains(_CurrentVehicle))
                    {
                        if (SharedConstants.VRENT_CARSHOPS_DB_RESID.Equals(shopLabelCell.value))
                            // V-RENT
                            rentingComboBox.Items.Add(currentItemLabel);
                        else
                            // Standard dealer
                            sellingComboBox.Items.Add(currentItemLabel);
                    }
                }
            }
        }

        /// <summary>
        /// Updates spot vehicle list according to current spot
        /// </summary>
        private void _RefreshDealerVehicleList()
        {
            if (!string.IsNullOrEmpty(_CurrentAvailabilitySpot))
            {
                spotVehiclesListView.Items.Clear();

                // Getting spot reference
                if (_CurrentAvailabilitySpot.Contains(Tools.SYMBOL_VALUE_SEPARATOR.ToString()))
                    _CurrentAvailabilitySpot = _CurrentAvailabilitySpot.Split(Tools.SYMBOL_VALUE_SEPARATOR)[1];

                // Vehicles from this spot
                DB.Entry allVehicles = DatabaseHelper.SelectAllCellsFromTopicWherePrimaryKey(_CarShopsTable, _CurrentAvailabilitySpot)[0];

                for (int col = 3; col < 18; col++)
                {
                    int slotIndex = col - 2;
                    ListViewItem li = new ListViewItem(slotIndex.ToString());
                    string vehicleRef = allVehicles.cells[col].value;
                    string vehicleName;

                    // Compact 1 == free slot
                    if (DatabaseConstants.COMPACT1_PHYSICS_DB_RESID.Equals(vehicleRef))
                        vehicleName = _FREE_SLOT_NAME;
                    else
                        vehicleName = NamesHelper.GetVehicleFullName(allVehicles.cells[col].value, true, _PhysicsTable, _PhysicsResource, _BrandsTable,
                                                                 _BrandsResource);

                    // Current vehicle line is in bold
                    li.Font = (vehicleRef.Equals(_CurrentVehicle) ? 
                        new Font(spotVehiclesListView.Font, FontStyle.Bold) 
                        : new Font(spotVehiclesListView.Font, FontStyle.Regular));

                    li.Tag = vehicleRef;
                    li.SubItems.Add(vehicleName);
                    spotVehiclesListView.Items.Add(li);
                }
            }
        }

        /// <summary>
        /// Erases vehicle from selected slot
        /// </summary>
        /// <param name="listViewItem"></param>
        private void _RemoveVehicleFromSlot(ListViewItem listViewItem)
        {
            if (listViewItem == null)
                return;

            string slotIndex = listViewItem.Text;
            string columnName = string.Format(DatabaseConstants.SLOT_CARSHOPS_PATTERN_DB_COLUMN,slotIndex);

            // Updating database
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_CarShopsTable, columnName, _CurrentAvailabilitySpot, DatabaseConstants.COMPACT1_PHYSICS_DB_RESID);
        }

        /// <summary>
        /// Puts current vehicle onto selected slot
        /// </summary>
        /// <param name="listViewItem"></param>
        private void _SetVehicleOnSlot(ListViewItem listViewItem)
        {
            if (listViewItem == null)
                return;

            string slotIndex = listViewItem.Text;
            string columnName = string.Format(DatabaseConstants.SLOT_CARSHOPS_PATTERN_DB_COLUMN, slotIndex);

            // Updating database
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_CarShopsTable, columnName, _CurrentAvailabilitySpot, _CurrentVehicle);
        }

        /// <summary>
        /// Selects spot with specified reference in the right combo box
        /// </summary>
        /// <param name="spotRef"></param>
        private void _SelectSpotFromReference(string spotRef)
        {
            if (string.IsNullOrEmpty(spotRef))
                return;

            bool isFound = false;

            // Selling point
            foreach (string anotherItem in sellingComboBox.Items)
            {
                if (anotherItem.EndsWith(spotRef))
                {
                    sellingComboBox.SelectedItem = anotherItem;
                    isFound = true;
                    break;
                }
            }

            // Renting point
            if (!isFound)
            {
                foreach (string anotherItem in rentingComboBox.Items)
                {
                    if (anotherItem.EndsWith(spotRef))
                    {
                        rentingComboBox.SelectedItem = anotherItem;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Moves selected vehicle to next slot
        /// </summary>
        /// <param name="listViewItem"></param>
        /// <returns></returns>
        private bool _IncreaseVehicleSlot(ListViewItem listViewItem)
        {
            bool isOK = false;

            if (listViewItem != null)
            {
                int slotIndex = int.Parse(listViewItem.Text);

                if (slotIndex < 15)
                {
                    _SwapVehicleSlots(slotIndex, slotIndex + 1);
                    isOK = true;
                }
            }

            return isOK;
        }

        /// <summary>
        /// Moves selected vehicle to previous slot
        /// </summary>
        /// <param name="listViewItem"></param>
        /// <returns></returns>
        private bool _DecreaseVehicleSlot(ListViewItem listViewItem)
        {
            bool isOK = false;

            if (listViewItem != null)
            {
                int slotIndex = int.Parse(listViewItem.Text);

                if (slotIndex > 1)
                {
                    _SwapVehicleSlots(slotIndex, slotIndex - 1);
                    isOK = true;
                }
            }

            return isOK;
        }
        
        /// <summary>
        /// Swaps values vetween provided vehicle slots indexes
        /// </summary>
        /// <param name="slotIndex1"></param>
        /// <param name="slotIndex2"></param>
        private void _SwapVehicleSlots(int slotIndex1, int slotIndex2)
        {
            string columnName1 = string.Format(DatabaseConstants.SLOT_CARSHOPS_PATTERN_DB_COLUMN, slotIndex1);
            string columnName2 = string.Format(DatabaseConstants.SLOT_CARSHOPS_PATTERN_DB_COLUMN, slotIndex2);
            string value1 =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(columnName1, _CarShopsTable, _CurrentAvailabilitySpot)
                    [0].value;
            string value2 =
                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(columnName2, _CarShopsTable, _CurrentAvailabilitySpot)
                    [0].value;

            // Updating database
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_CarShopsTable, columnName1, _CurrentAvailabilitySpot,
                                                              value2);
            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(_CarShopsTable, columnName2, _CurrentAvailabilitySpot,
                                                              value1);
        }
        #endregion

        #region Events
        private void sellingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Another selling point has been selected
            try
            {
                Cursor = Cursors.WaitCursor;

                rentingComboBox.Font = new Font(rentingComboBox.Font, FontStyle.Regular);
                allSellingRentingComboBox.Font = new Font(allSellingRentingComboBox.Font, FontStyle.Regular);
                sellingComboBox.Font = new Font(sellingComboBox.Font, FontStyle.Bold);
                _CurrentAvailabilitySpot = sellingComboBox.Text;
                _RefreshDealerVehicleList();

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void rentingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Another renting point has been selected
            try
            {
                Cursor = Cursors.WaitCursor;

                rentingComboBox.Font = new Font(rentingComboBox.Font, FontStyle.Bold);
                allSellingRentingComboBox.Font = new Font(allSellingRentingComboBox.Font, FontStyle.Regular);
                sellingComboBox.Font = new Font(sellingComboBox.Font, FontStyle.Regular);
                _CurrentAvailabilitySpot = rentingComboBox.Text;
                _RefreshDealerVehicleList();

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void removeVehicleLocationButton_Click(object sender, EventArgs e)
        {
            // Click on 'Remove' button
            if (spotVehiclesListView.SelectedItems.Count == 1)
            {
                ListViewItem selectedItem = spotVehiclesListView.SelectedItems[0];

                try
                {
                    Cursor = Cursors.WaitCursor;

                    _RemoveVehicleFromSlot(selectedItem);

                    // Reloading
                    _InitializeDealersContents();
                    _SelectSpotFromReference(_CurrentAvailabilitySpot);

                    // Modification flag
                    _IsDatabaseModified = true;

                    StatusBarLogManager.ShowEvent(this, _STATUS_REMOVING_VEHICLE_FROM_DEALER_OK);
                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void setVehicleLocationButton_Click(object sender, EventArgs e)
        {
            // Click on 'Set' button
            if (spotVehiclesListView.SelectedItems.Count == 1)
            {
                ListViewItem selectedItem = spotVehiclesListView.SelectedItems[0];

                try
                {
                    Cursor = Cursors.WaitCursor;

                    _SetVehicleOnSlot(selectedItem);

                    // Reloading
                    _InitializeDealersContents();
                    _SelectSpotFromReference(_CurrentAvailabilitySpot);

                    // Modification flag
                    _IsDatabaseModified = true;

                    StatusBarLogManager.ShowEvent(this, _STATUS_SETTING_VEHICLE_ON_DEALER_OK);
                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void allSellingRentingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Another global point has been selected
            try
            {
                Cursor = Cursors.WaitCursor;

                rentingComboBox.Font = new Font(rentingComboBox.Font, FontStyle.Regular);
                allSellingRentingComboBox.Font = new Font(allSellingRentingComboBox.Font, FontStyle.Bold);
                sellingComboBox.Font = new Font(sellingComboBox.Font, FontStyle.Regular);
                _CurrentAvailabilitySpot = allSellingRentingComboBox.Text;
                _RefreshDealerVehicleList();

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void spotVehicleDownButton_Click(object sender, EventArgs e)
        {
            // Click on 'Vehicle down' button
            if (spotVehiclesListView.SelectedItems.Count == 1)
            {
                ListViewItem selectedItem = spotVehiclesListView.SelectedItems[0];

                if (!DatabaseConstants.COMPACT1_PHYSICS_DB_RESID.Equals(selectedItem.Tag))
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;

                        bool isOK = _IncreaseVehicleSlot(selectedItem);

                        if (isOK)
                        {
                            // Reloading
                            ListView2.StoreSelectedIndex(spotVehiclesListView);
                            _RefreshDealerVehicleList();
                            ListView2.RestoreSelectedIndex(spotVehiclesListView);

                            // Selecting right line
                            ListView2.SelectNextItem(spotVehiclesListView);

                            // Modification flag
                            _IsDatabaseModified = true;

                            StatusBarLogManager.ShowEvent(this, _STATUS_DOWN_VEHICLE_FROM_DEALER_OK);
                        }

                        Cursor = Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        MessageBoxes.ShowError(this, ex);
                    }
                }
            }
        }

        private void spotVehicleUpButton_Click(object sender, EventArgs e)
        {
            // Click on 'Vehicle up' button
            if (spotVehiclesListView.SelectedItems.Count == 1)
            {
                ListViewItem selectedItem = spotVehiclesListView.SelectedItems[0];

                if (!DatabaseConstants.COMPACT1_PHYSICS_DB_RESID.Equals(selectedItem.Tag))
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;

                        bool isOK = _DecreaseVehicleSlot(selectedItem);

                        if (isOK)
                        {
                            // Reloading
                            ListView2.StoreSelectedIndex(spotVehiclesListView);
                            _RefreshDealerVehicleList();
                            ListView2.RestoreSelectedIndex(spotVehiclesListView);

                            // Selecting right line
                            ListView2.SelectPreviousItem(spotVehiclesListView);

                            // Modification flag
                            _IsDatabaseModified = true;

                            StatusBarLogManager.ShowEvent(this, _STATUS_UP_VEHICLE_FROM_DEALER_OK);
                        }

                        Cursor = Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        MessageBoxes.ShowError(this, ex);
                    }
                }
            }
        }
        #endregion
    }
}
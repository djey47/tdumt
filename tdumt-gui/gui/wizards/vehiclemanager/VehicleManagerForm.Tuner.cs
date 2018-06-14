using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Types.Collections;
using DjeFramework1.Util.BasicStructures;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.fileformats.database.util;
using TDUModdingLibrary.support;
using TDUModdingTools.gui.common;
using TDUModdingTools.gui.wizards.vehiclemanager.common;

namespace TDUModdingTools.gui.wizards.vehiclemanager
{
    partial class VehicleManagerForm
    {
        #region Constants
        /// <summary>
        /// List of all tuner spot references
        /// </summary>
        private static readonly string[] _LIST_TUNER_SPOTS = new string[]
            {"603479649", "54019598", "60250598",
                "58148602", "61011606", "58127603",
                "54133599"};

        /// <summary>
        /// Message to display when browsing tuning brands
        /// </summary>
        private const string _MESSAGE_BROWSE_TUNING_BRANDS = "Please select new tuning brand for this vehicle.";
        #endregion

        #region Members
        /// <summary>
        /// Reference of tuning brand which is currently selected
        /// </summary>
        private string _CurrentTuningBrand = null;
        #endregion

        #region Events
        private void tuningBrandsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Another tuning brand has been selected
            string selectedText = tuningBrandsComboBox.Text;

            try
            {
                Cursor = Cursors.WaitCursor;

                _SelectRightSpotInList(selectedText);

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void tuneBrandNameChangeButton_Click(object sender, EventArgs e)
        {
            // Click on 'Change...' button
            if (string.IsNullOrEmpty(tuningBrandsComboBox.Text))
                return;

            try
            {
                bool isModified = _ChangeTuningBrand();

                if (isModified)
                {
                    Cursor = Cursors.WaitCursor;

                    // Reloading
                    _InitializeTunerContents();

                    // Modification flag
                    _IsDatabaseModified = true;

                    StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_TUNING_BRAND_OK);

                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void setVehicleTuningSpotButton_Click(object sender, EventArgs e)
        {
            // Click on 'Set' button
            if (_CurrentTuningBrand == null || string.IsNullOrEmpty(tuningSpotsComboBox.Text))
                return;

            try
            {
                Cursor = Cursors.WaitCursor;

                _ChangeTuningSpot();

                // Reloading
                _InitializeTunerContents();

                // Modification flag
                _IsDatabaseModified = true;

                StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_TUNING_SPOT_OK);

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
        private void _InitializeTunerContents()
        {
            // Tuning brands list
            tuningBrandsComboBox.Items.Clear();

            Couple<string> cond1 = new Couple<string>(SharedConstants.CAR_CARPACKS_DB_COLUMN, _CurrentVehicle);
            List<DB.Cell> brandCells = DatabaseHelper.SelectCellsFromTopicWhereCellValues(SharedConstants.BRAND_CARPACKS_DB_COLUMN, _CarPacksTable, cond1);

            foreach (DB.Cell anotherBrandReference in brandCells)
            {
                // Searching brand in brands table
                DB.Cell currentBrand =
                    DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.NAME_BRANDS_DB_COLUMN,
                                                                       _BrandsTable, anotherBrandReference.value)[0];

                // Searching brand name in brands resource
                string brandName = DatabaseHelper.GetResourceValueFromCell(currentBrand, _BrandsResource);

                tuningBrandsComboBox.Items.Add(
                    string.Format(SharedConstants.FORMAT_REF_NAME_COUPLE, brandName, anotherBrandReference.value));
            }

            // Tuning spots list
            tuningSpotsComboBox.Items.Clear();

            // Getting all tuner shops
            foreach (string shopRef in _LIST_TUNER_SPOTS)
            {
                DB.Cell shopCell = DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(SharedConstants.LIBELLE_CARSHOPS_DB_COLUMN, _CarShopsTable, shopRef)[0];
                string shopLibelle = DatabaseHelper.GetResourceValueFromCell(shopCell, _CarShopsResource);

                tuningSpotsComboBox.Items.Add(string.Format(SharedConstants.FORMAT_REF_NAME_COUPLE, shopLibelle, shopCell.value));
            }

            // Selecting first brand
            if (tuningBrandsComboBox.Items.Count > 0)
                tuningBrandsComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Select right spot according to selected tuning brand
        /// </summary>
        /// <param name="selectedText"></param>
        private void _SelectRightSpotInList(string selectedText)
        {
            // Getting brand reference
            string brandRef = selectedText.Split(Tools.SYMBOL_VALUE_SEPARATOR)[1];

            _CurrentTuningBrand = brandRef;

            // Selecting current shop
            List<DB.Entry> allShops =
                DatabaseHelper.SelectAllCellsFromTopicWhereCellValue(_CarPacksTable, SharedConstants.CAR_CARPACKS_DB_COLUMN,
                                                                  _CurrentVehicle);
            DB.Cell currentShopCell = new DB.Cell();

            foreach (DB.Entry anotherEntry in allShops)
            {
                DB.Cell brandCell =
                    DatabaseHelper.GetCellFromEntry(_CarPacksTable, anotherEntry,
                                                    SharedConstants.BRAND_CARPACKS_DB_COLUMN);

                if (brandRef.Equals(brandCell.value))
                {
                    currentShopCell = DatabaseHelper.GetCellFromEntry(_CarPacksTable, anotherEntry, SharedConstants.LIBELLE_CARSHOPS_DB_COLUMN);
                    break;
                }
            }

            string shopName = DatabaseHelper.GetResourceValueFromCell(currentShopCell, _CarShopsResource);

            tuningSpotsComboBox.Text =
                string.Format(SharedConstants.FORMAT_REF_NAME_COUPLE, shopName, currentShopCell.value);
        }

        /// <summary>
        /// Enables selecting a new tuning brand
        /// </summary>
        /// <returns></returns>
        private bool _ChangeTuningBrand()
        {
            bool result = false;

            if (!string.IsNullOrEmpty(tuningBrandsComboBox.Text))
            {
                string newBrandRef = _GetBrandReference();

                if (newBrandRef != null)
                {
                    // Applying changes
                    string currentBrandRef = tuningBrandsComboBox.Text.Split(Tools.SYMBOL_VALUE_SEPARATOR)[1];

                    // Building conditions
                    Couple<string> cond1 = new Couple<string>(SharedConstants.CAR_CARPACKS_DB_COLUMN, _CurrentVehicle);
                    Couple<string> cond2 = new Couple<string>(SharedConstants.BRAND_CARPACKS_DB_COLUMN, currentBrandRef);

                    DatabaseHelper.UpdateCellFromTopicWhereCellValues(_CarPacksTable,
                                                                  SharedConstants.BRAND_CARPACKS_DB_COLUMN,
                                                                  newBrandRef, cond1, cond2);
                }
                result = true;
            }
            return result;
        }
        
        /// <summary>
        /// Enables selecting a new tuning spot
        /// </summary>
        private void _ChangeTuningSpot()
        {
            if (string.IsNullOrEmpty(tuningBrandsComboBox.Text)
                || string.IsNullOrEmpty(tuningSpotsComboBox.Text))
                return;

            string currentVehicle = _CurrentVehicle;
            string currentBrandRef = _CurrentTuningBrand;

            // Getting spot name reference
            string spotNameRef = tuningSpotsComboBox.Text.Split(Tools.SYMBOL_VALUE_SEPARATOR)[1];

            // Building conditions
            Couple<string> cond1 = new Couple<string>(SharedConstants.CAR_CARPACKS_DB_COLUMN, currentVehicle);
            Couple<string> cond2 = new Couple<string>(SharedConstants.BRAND_CARPACKS_DB_COLUMN, currentBrandRef);

            DatabaseHelper.UpdateCellFromTopicWhereCellValues(_CarPacksTable,
                                                                  SharedConstants.LIBELLE_CARPACKS_DB_COLUMN,
                                                                  spotNameRef, cond1, cond2);
        }
        
        /// <summary>
        /// Allow to choose a brand reference
        /// </summary>
        /// <returns></returns>
        private string _GetBrandReference()
        {
            string returnedRef = null;

            // Preparing manufacturer list
            SortedStringCollection sortedManufList = new SortedStringCollection();
            List<DB.Cell[]> allBrandRefAndNames = DatabaseHelper.SelectCellsFromTopic(_BrandsTable, DatabaseConstants.REF_DB_COLUMN, SharedConstants.NAME_BRANDS_DB_COLUMN, SharedConstants.BITFIELD_DB_COLUMN);
            Dictionary<string, string> index = new Dictionary<string, string>();

            foreach (DB.Cell[] anotherEntry in allBrandRefAndNames)
            {
                // Bitfield
                // * b0 active brand if true
                // * b1 clothes if true
                bool[] currentBitField = DatabaseHelper.ParseBitField(anotherEntry[2]);

                // Inactive and clothes brands are ignored
                if (currentBitField[0] && !currentBitField[1])
                {
                    string currentRef = DatabaseHelper.GetResourceValueFromCell(anotherEntry[0], _BrandsResource);
                    string currentName = DatabaseHelper.GetResourceValueFromCell(anotherEntry[1], _BrandsResource);

                    if (currentName != null && !SharedConstants.ERROR_DB_RESVAL.Equals(currentName) &&
                        !sortedManufList.Contains(currentName))
                    {
                        sortedManufList.Add(currentName);
                        index.Add(currentName, currentRef);
                    }
                }
            }

            // Displaying browse dialog
            TableBrowsingDialog dialog = new TableBrowsingDialog(_MESSAGE_BROWSE_TUNING_BRANDS, sortedManufList, index);
            DialogResult dr = dialog.ShowDialog();

            if (dr == DialogResult.OK && dialog.SelectedIndex != null)
                returnedRef = dialog.SelectedIndex;

            return returnedRef;
        }
        #endregion
    }
}
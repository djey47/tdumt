using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Common.Types.Collections;
using DjeFramework1.Util.BasicStructures;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.fileformats.database.util;
using TDUModdingLibrary.support;
using TDUModdingTools.gui.common;
using TDUModdingTools.gui.wizards.vehiclemanager.common;

namespace TDUModdingTools.gui.wizards.vehiclemanager.colors
{
    public partial class InteriorSetDialog : Form
    {
        #region Constants
        /// <summary>
        /// Format for color name label
        /// </summary>
        private const string _FORMAT_COLOR_NAME_LABEL = "{0} ({1})";

        /// <summary>
        /// Message displayed in manufacturer browsing dialog
        /// </summary>
        private const string _MESSAGE_BROWSE_MANUFACTURERS = "Please select a new manufacturer for current interior set...";

        /// <summary>
        /// Message to display in set names browser
        /// </summary>
        private const string _MESSAGE_BROWSE_SET_NAMES = "Please select a name for this new interior set.";

        /// <summary>
        /// Message to display in interior colors browser
        /// </summary>
        private const string _MESSAGE_BROWSE_COLORS = "Please select new interior color for this set.";

        /// <summary>
        /// Message to display in interior materials browser
        /// </summary>
        private const string _MESSAGE_BROWSE_MATERIALS = "Please select new interior material for this set.";

        /// <summary>
        /// Warning message to be displayed when trying to remove an interior color set
        /// </summary>
        private const string _WARNING_INTERIOR_SET_REMOVAL = "Current set will also be removed from all exterior sets.\r\nThis operation cannot be undone.";
        #endregion

        #region Properties
        /// <summary>
        /// Interior color set which has been selected
        /// </summary>
        public string SelectedInteriorSet
        {
            get { return _SelectedInteriorSet; }
        }
        private string _SelectedInteriorSet = null;
        #endregion

        #region Private members
        /// <summary>
        /// Interior set identifier
        /// </summary>
        private string _InteriorSetId = null;

        /// <summary>
        /// Interior table
        /// </summary>
        private readonly DB _InteriorTable;

        /// <summary>
        /// Interior resources
        /// </summary>
        private readonly DBResource _InteriorResource;

        /// <summary>
        /// Brands table
        /// </summary>
        private readonly DB _BrandsTable;

        /// <summary>
        /// Brands resources
        /// </summary>
        private readonly DBResource _BrandsResource;

        /// <summary>
        /// CarColors table
        /// </summary>
        private readonly DB _CarColorsTable;

        /// <summary>
        /// Set name code
        /// </summary>
        private string _SetNameCode = null;

        /// <summary>
        /// Manufacturer code
        /// </summary>
        private string _BrandCode = null;

        /// <summary>
        /// Code for main color
        /// </summary>
        private string _Color1Code = null;

        /// <summary>
        /// Code for secondary color
        /// </summary>
        private string _Color2Code = null;

        /// <summary>
        /// Code for material
        /// </summary>
        private string _MaterialCode = null;
        #endregion

        /// <summary>
        /// Unique constructor
        /// </summary>
        /// <param name="interiorTable"></param>
        /// <param name="interiorResource"></param>
        /// <param name="brandsTable"></param>
        /// <param name="brandsResource"></param>
        /// <param name="carColorsTable"></param>
        /// <param name="interiorSetId"></param>
        public InteriorSetDialog(DB interiorTable, DBResource interiorResource, DB brandsTable, DBResource brandsResource, DB carColorsTable, string interiorSetId)
        {
            InitializeComponent();

            _InteriorTable = interiorTable;
            _BrandsTable = brandsTable;
            _InteriorSetId = interiorSetId;
            _InteriorResource = interiorResource;
            _BrandsResource = brandsResource;
            _CarColorsTable = carColorsTable;

            try
            {
                Cursor = Cursors.WaitCursor;
                _InitializeContents();
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(null, ex);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #region Private methods
        /// <summary>
        /// Defines window contents
        /// </summary>
        private void _InitializeContents()
        {
            // Getting cooresponding line...
            DB.Entry setEntry = DatabaseHelper.SelectAllCellsFromTopicWherePrimaryKey(_InteriorTable, _InteriorSetId)[0];

            _UpdateSetList(setEntry);
        }

        /// <summary>
        /// Updates interior color set list
        /// </summary>
        /// <param name="currentSetEntry"></param>
        private void _UpdateSetList(DB.Entry currentSetEntry)
        {
            interiorColorSetsComboBox.Items.Clear();

            // Getting all sets
            List<DB.Entry> allEntries = DatabaseHelper.SelectAllCellsFromTopic(_InteriorTable);
            string selectedItem = null;

            foreach (DB.Entry anotherEntry in allEntries)
            {
                string entryRef =
                    DatabaseHelper.GetCellFromEntry(_InteriorTable, anotherEntry, DatabaseConstants.REF_DB_COLUMN).value;
                DB.Cell nameRefCell =
                    DatabaseHelper.GetCellFromEntry(_InteriorTable, anotherEntry,
                                                    SharedConstants.INTERIOR_NAME_INTERIOR_DB_COLUMN);
                string setName =
                    DatabaseHelper.GetResourceValueFromCell(nameRefCell, _InteriorResource);
                DB.Cell manufacturerCell =
                    DatabaseHelper.GetCellFromEntry(_InteriorTable, anotherEntry,
                                                    SharedConstants.MANUFACTURER_ID_INTERIOR_DB_COLUMN);
                string manufacturerName =
                    DatabaseHelper.GetResourceValueFromCell(manufacturerCell, _BrandsResource);
                string itemText = string.Format(VehicleManagerForm._FORMAT_COLOR_INT_SET_ITEM, setName, manufacturerName, entryRef);

                interiorColorSetsComboBox.Items.Add(itemText);

                if (currentSetEntry.primaryKey.Equals(entryRef))
                    selectedItem = itemText;
            }

            // Current set is selected
            if (selectedItem != null)
                interiorColorSetsComboBox.SelectedItem = selectedItem;
        }

        /// <summary>
        /// Returns a couple of set code/name
        /// </summary>
        /// <returns></returns>
        private Couple<string> _PickSetManufacturer()
        {
            // Preparing manufacturer list
            Couple<string> returnedCodeName = null;
            SortedStringCollection sortedNameList = new SortedStringCollection();
            Dictionary<string, string> index = new Dictionary<string, string>();
            List<DB.Cell[]> brandCells = DatabaseHelper.SelectCellsFromTopic(_BrandsTable, SharedConstants.ID_BRANDS_DB_COLUMN);

            foreach (DB.Cell[] cells in brandCells)
            {
                DB.Cell currentCell = cells[0];
                string brandCode = DatabaseHelper.GetResourceValueFromCell(currentCell, _BrandsResource);

                if (!index.ContainsKey(brandCode))
                {
                    sortedNameList.Add(brandCode);
                    index.Add(brandCode, currentCell.value);
                }
            }

            // Displaying browse dialog
            TableBrowsingDialog dialog = new TableBrowsingDialog(_MESSAGE_BROWSE_MANUFACTURERS, sortedNameList, index);
            DialogResult dr = dialog.ShowDialog();

            if (dr == DialogResult.OK && dialog.SelectedIndex != null)
                returnedCodeName = new Couple<string>(dialog.SelectedIndex, dialog.SelectedValue);

            return returnedCodeName;
        }

        /// <summary>
        /// Refreshes information about current interior set
        /// </summary>
        /// <param name="setRef"></param>
        private void _UpdateInteriorSetInformation(string setRef)
        {
            DB.Entry setEntry = DatabaseHelper.SelectAllCellsFromTopicWherePrimaryKey(_InteriorTable, setRef)[0];

            // Name
            DB.Cell nameRefCell =
                DatabaseHelper.GetCellFromEntry(_InteriorTable, setEntry,
                                                SharedConstants.INTERIOR_NAME_INTERIOR_DB_COLUMN);

            _SetNameCode = nameRefCell.value;

            // Manufacturer
            DB.Cell manufacturerRefCell =
                DatabaseHelper.GetCellFromEntry(_InteriorTable, setEntry,
                                                SharedConstants.MANUFACTURER_ID_INTERIOR_DB_COLUMN);
            string manufacturerCode =
                DatabaseHelper.GetResourceValueFromCell(manufacturerRefCell, _BrandsResource);

            interiorManufacturerLinkLabel.Text = manufacturerCode;
            _BrandCode = manufacturerRefCell.value;

            // Price
            string setPrice =
                DatabaseHelper.GetCellFromEntry(_InteriorTable, setEntry,
                                                SharedConstants.PRICE_DOLLAR_INTERIOR_DB_COLUMN).value;
            interiorPriceTextBox.Text = setPrice;

            // Color 1
            DB.Cell mainColorRefCell =
                DatabaseHelper.GetCellFromEntry(_InteriorTable, setEntry,
                                                SharedConstants.INTERIOR_COLOR_1_INTERIOR_DB_COLUMN);
            string colorCode =
                DatabaseHelper.GetResourceValueFromCell(mainColorRefCell, _InteriorResource);

            colorsMainLinkLabel.Text = _GetColorLabel(colorCode);
            _Color1Code = mainColorRefCell.value;

            // Color 2
            DB.Cell secColorRefCell =
                DatabaseHelper.GetCellFromEntry(_InteriorTable, setEntry,
                                                SharedConstants.INTERIOR_COLOR_2_INTERIOR_DB_COLUMN);
            colorCode =
                DatabaseHelper.GetResourceValueFromCell(secColorRefCell, _InteriorResource);
            colorsSecondaryLinkLabel.Text = _GetColorLabel(colorCode);
            _Color2Code = secColorRefCell.value;

            // Material
            DB.Cell materialRefCell =
                DatabaseHelper.GetCellFromEntry(_InteriorTable, setEntry,
                                                SharedConstants.MATERIAL_INTERIOR_DB_COLUMN);
            string materialCode =
                DatabaseHelper.GetResourceValueFromCell(materialRefCell, _InteriorResource);

            materialLinkLabel.Text = _GetColorLabel(materialCode);
            _MaterialCode = materialRefCell.value;

            // If 'None' set is selected, all controls are locked
            bool isToBeEnabled = !(SharedConstants.NO_COLOR_INTERIOR_DB_VAL.Equals(setRef));

            interiorColorsRemoveSetButton.Enabled =
            interiorManufacturerLinkLabel.Enabled =
            interiorPriceTextBox.Enabled =
            colorsMainLinkLabel.Enabled =
            colorsSecondaryLinkLabel.Enabled =
            materialLinkLabel.Enabled = isToBeEnabled;
        }

        /// <summary>
        /// Returns correct color label for specified color code
        /// </summary>
        /// <param name="colorCode"></param>
        /// <returns></returns>
        private static string _GetColorLabel(string colorCode)
        {
            string returnedLabel = null;

            if (!string.IsNullOrEmpty(colorCode))
            {
                string colorName =
                    ColorsHelper.GetColorName(colorCode);

                returnedLabel = string.Format(_FORMAT_COLOR_NAME_LABEL, colorName, colorCode);
            }

            return returnedLabel;
        }

        /// <summary>
        /// Handles interior color set adding
        /// </summary>
        /// <returns></returns>
        private bool _AddInteriorSet()
        {
            bool returnedResult = false;

            // Preparing name list
            SortedStringCollection sortedNameList = new SortedStringCollection();
            Dictionary<string, string> index = new Dictionary<string, string>();

            foreach (DBResource.Entry anotherEntry in _InteriorResource.EntryList)
            {
                if (anotherEntry.isValid && !anotherEntry.isComment)
                {
                    if (anotherEntry.id.Id.EndsWith(SharedConstants.NAME_INTERIOR_CATEGORY))
                    {
                        string colorName = anotherEntry.value;

                        if (!sortedNameList.Contains(colorName))
                        {
                            sortedNameList.Add(colorName);
                            index.Add(colorName, anotherEntry.id.Id);
                        }
                    }
                }
            }

            // Displaying browse dialog
            TableBrowsingDialog dialog = new TableBrowsingDialog(_MESSAGE_BROWSE_SET_NAMES, sortedNameList, index);
            DialogResult dr = dialog.ShowDialog();

            if (dr == DialogResult.OK && dialog.SelectedIndex != null)
            {
                // Applying changes
                string value = DatabaseHelper.BuildFullLineValue(_InteriorTable,
                                                                 "",
                                                                 SharedConstants.AC_MANUFACTURER_ID_BRANDS_DB_VAL,
                                                                 dialog.SelectedIndex,
                                                                 SharedConstants.NOT_AVAILABLE_INTERIOR_COLOR_DB_RESID,
                                                                 SharedConstants.NOT_AVAILABLE_INTERIOR_COLOR_DB_RESID,
                                                                 SharedConstants.NOT_AVAILABLE_INTERIOR_COLOR_DB_RESID,
                                                                 "0");

                _InteriorSetId = DatabaseHelper.InsertAllCellsIntoTopic(_InteriorTable, 0, value);

                returnedResult = true;
            }

            return returnedResult;
        }

        /// <summary>
        /// Handles removal of current interior set
        /// </summary>
        /// <returns></returns>
        private bool _RemoveInteriorSet()
        {
            bool returnedResult = false;

            if (interiorColorSetsComboBox.SelectedItem != null)
            {
                // Confirmation
                DialogResult dr =
                    MessageBoxes.ShowWarning(this, _WARNING_INTERIOR_SET_REMOVAL, MessageBoxButtons.OKCancel);

                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    // Removes current set references in CarColors
                    string currentSetId = interiorColorSetsComboBox.Text.Split(Tools.SYMBOL_VALUE_SEPARATOR)[2];

                    DatabaseHelper.RemoveValueFromAnyColumn(_CarColorsTable, currentSetId, SharedConstants.NO_COLOR_INTERIOR_DB_VAL);

                    // Deletion query (declaration)
                    DatabaseHelper.DeleteFromTopicWhereIdentifier(_InteriorTable, currentSetId);

                    returnedResult = true;
                }
            }

            return returnedResult;
        }

        /// <summary>
        /// Allows to pick an interior color/material
        /// </summary>
        /// <returns></returns>
        private static string _PickInteriorColor(bool isForMaterial)
        {
            // Preparing color list
            string returnedColor = null;
            SortedStringCollection sortedColorList = new SortedStringCollection();
            Dictionary<string, string> index = new Dictionary<string, string>();
            Dictionary<string, string> currentReference;
            string currentMessage;

            if (isForMaterial)
            {
                currentReference = ColorsHelper.MaterialsReference;
                currentMessage = _MESSAGE_BROWSE_MATERIALS;
            }
            else
            {
                currentReference = ColorsHelper.InteriorReference;
                currentMessage = _MESSAGE_BROWSE_COLORS;
            }

            foreach (KeyValuePair<string, string> pair in currentReference)
            {
                string colorName = string.Format(_FORMAT_COLOR_NAME_LABEL, pair.Value, pair.Key);

                if (ColorsHelper.IdByCodeReference.ContainsKey(pair.Key))
                {
                    string colorId = ColorsHelper.IdByCodeReference[pair.Key];

                    sortedColorList.Add(colorName);
                    index.Add(colorName, colorId);
                }
            }

            // Displaying browse dialog
            TableBrowsingDialog dialog = new TableBrowsingDialog(currentMessage, sortedColorList, index);
            DialogResult dr = dialog.ShowDialog();

            if (dr == DialogResult.OK && dialog.SelectedIndex != null)
                returnedColor = dialog.SelectedIndex;

            return returnedColor;

        }
        #endregion

        #region Events
        private void interiorManufacturerLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on manufacturer link
            try
            {
                Couple<string> newSetCodeName = _PickSetManufacturer();

                if (newSetCodeName != null)
                {
                    interiorManufacturerLinkLabel.Text = newSetCodeName.SecondValue;
                    _BrandCode = newSetCodeName.FirstValue;
                }
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void interiorColorSetsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Another interior set has been selected
            if (!string.IsNullOrEmpty(interiorColorSetsComboBox.Text))
            {
                string setRef = interiorColorSetsComboBox.Text.Split(Tools.SYMBOL_VALUE_SEPARATOR)[2];

                _UpdateInteriorSetInformation(setRef);
                _InteriorSetId = setRef;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            // Click on 'OK' button
            try
            {
                Cursor = Cursors.WaitCursor;

                // Selected color set : does it exist ?
                // 'None' color set is never updated
                if (!SharedConstants.NO_COLOR_INTERIOR_DB_VAL.Equals(_InteriorSetId))
                {
                    // Builds line to insert/update
                    // Price
                    string newPrice = "0";
                    
                    try
                    {
                        int price = int.Parse(interiorPriceTextBox.Text);

                        // Checking limits
                        if (price >= 0)
                            newPrice = price.ToString();
                    }
                    catch (Exception)
                    {
                        Log.Info("Invalid interior price value provided: " + interiorPriceTextBox.Text);
                    }

                    string fullLine = DatabaseHelper.BuildFullLineValue(_InteriorTable,
                        _InteriorSetId,
                        _BrandCode,
                        _SetNameCode,
                        _Color1Code,
                        _Color2Code,
                        _MaterialCode,
                        newPrice);

                    // Always updating as new set is always added before                    
                    bool isSetIdExists = DatabaseHelper.ContainsPrimaryKey(_InteriorTable, _InteriorSetId);

                    if (isSetIdExists)
                    {
                        // Update mode
                        DatabaseHelper.UpdateAllCellsFromTopicWherePrimaryKey(_InteriorTable, _InteriorSetId,
                                                                              fullLine);
                    }

                    _SelectedInteriorSet = _InteriorSetId;
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

        private void interiorColorsAddSetButton_Click(object sender, EventArgs e)
        {
            // Click on Add button
            try
            {
                if (_AddInteriorSet())
                {
                    // Reloading
                    DB.Entry setEntry = DatabaseHelper.SelectAllCellsFromTopicWherePrimaryKey(_InteriorTable, _InteriorSetId)[0];

                    _UpdateSetList(setEntry);
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

        private void interiorColorsRemoveSetButton_Click(object sender, EventArgs e)
        {
            // Click on 'Remove' button
            try
            {
                if (_RemoveInteriorSet())
                {
                    // Reloading
                    DB.Entry setEntry = DatabaseHelper.SelectAllCellsFromTopicWherePrimaryKey(_InteriorTable, SharedConstants.NO_COLOR_INTERIOR_DB_VAL)[0];

                    _UpdateSetList(setEntry);
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

        private void colorsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on color/material link
            if (!string.IsNullOrEmpty(_InteriorSetId))
            {
                try
                {
                    string colorCode = _PickInteriorColor(sender == materialLinkLabel);

                    if (colorCode != null)
                    {
                        Cursor = Cursors.WaitCursor;

                        // Reloading
                        string colorAcronym = _InteriorResource.GetEntryFromId(colorCode).value;
                        string newColorLabel = _GetColorLabel(colorAcronym);

                        if (sender == colorsMainLinkLabel)
                        {
                            _Color1Code = colorCode;
                            colorsMainLinkLabel.Text = newColorLabel;
                        }
                        else if (sender == colorsSecondaryLinkLabel)
                        {
                            _Color2Code = colorCode;
                            colorsSecondaryLinkLabel.Text = newColorLabel;
                        }
                        else if (sender == materialLinkLabel)
                        {
                            _MaterialCode = colorCode;
                            materialLinkLabel.Text = newColorLabel;
                        }
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
        #endregion
    }
}
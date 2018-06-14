using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Common.Types.Collections;
using DjeFramework1.Common.Types.Forms;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.support;
using TDUModdingTools.gui.common;
using TDUModdingTools.gui.wizards.vehiclemanager.colors;
using TDUModdingTools.gui.wizards.vehiclemanager.common;

namespace TDUModdingTools.gui.wizards.vehiclemanager
{
    partial class VehicleManagerForm
    {
        #region Constants
        /// <summary>
        /// Label format for exterior color set item
        /// </summary>
        internal static readonly string _FORMAT_COLOR_EXT_SET_ITEM = "{0}" + Tools.SYMBOL_VALUE_SEPARATOR + "{1}";

        /// <summary>
        /// Label format for interior color set item
        /// </summary>
        internal static readonly string _FORMAT_COLOR_INT_SET_ITEM = "{0}" + Tools.SYMBOL_VALUE_SEPARATOR + "{1}" +
                                                                     Tools.SYMBOL_VALUE_SEPARATOR + "{2}";

        /// <summary>
        /// Format for color name label
        /// </summary>
        private const string _FORMAT_COLOR_NAME_LABEL = "({0}) {1}";

        /// <summary>
        /// Format for interior color name label
        /// </summary>
        private const string _FORMAT_INT_COLOR_SET_LABEL = "({0}) {1}";

        /// <summary>
        /// Default label for color link
        /// </summary>
        private const string _LABEL_DEFAULT_COLOR_LINK = "...";

        /// <summary>
        /// Label for N/A color
        /// </summary>
        private const string _LABEL_UNAVAILABLE_COLOR = "Unavailable";

        /// <summary>
        /// Label for removed color
        /// </summary>
        private const string _LABEL_NO_COLOR = "No color";

        /// <summary>
        /// Status message when exterior main color change process ended without errors
        /// </summary>
        private const string _STATUS_CHANGING_MAIN_EXTERIOR_COLOR_OK = "Main exterior color was changed succesfully.";

        /// <summary>
        /// Status message when exterior secondary color change process ended without errors
        /// </summary>
        private const string _STATUS_CHANGING_SEC_EXTERIOR_COLOR_OK =
            "Secondary exterior color was changed succesfully.";

        /// <summary>
        /// Status message when calipers color change process ended without errors
        /// </summary>
        private const string _STATUS_CHANGING_CALIPERS_COLOR_OK = "Calipers color was changed succesfully.";

        /// <summary>
        /// Status message when color set price change process ended without errors
        /// </summary>
        private const string _STATUS_CHANGING_SET_PRICE_OK = "Current color set price was changed succesfully.";

        /// <summary>
        /// Status message when color set add process ended without errors
        /// </summary>
        private const string _STATUS_ADDING_SET_OK = "New color set was added succesfully.";

        /// <summary>
        /// Status message when color set rename process ended without errors
        /// </summary>
        private const string _STATUS_RENAMING_SET_OK = "Color set was renamed succesfully.";

        /// <summary>
        /// Status message when color set removal process ended without errors
        /// </summary>
        private const string _STATUS_REMOVING_SET_OK = "Color set was removed succesfully.";

        /// <summary>
        /// Status message when interior setting down process ended without errors
        /// </summary>
        private const string _STATUS_DOWN_INTERIOR_SET_OK =
            "Current interior set was put on next slot succesfully.";

        /// <summary>
        /// Status message when exterior setting down process ended without errors
        /// </summary>
        private const string _STATUS_DOWN_EXTERIOR_SET_OK =
            "Current exterior set was put on next slot succesfully.";

        /// <summary>
        /// Status message when interior setting up process ended without errors
        /// </summary>
        private const string _STATUS_UP_INTERIOR_SET_OK =
            "Current interior set was put on previous slot succesfully.";

        /// <summary>
        /// Status message when exterior setting up process ended without errors
        /// </summary>
        private const string _STATUS_UP_EXTERIOR_SET_OK =
            "Current exterior set was put on previous slot succesfully.";

        /// <summary>
        /// Status message when interior setting process ended without errors
        /// </summary>
        private const string _STATUS_CHOOSING_INTERIOR_SET_OK =
            "New interior set was applied succesfully.";

        /// <summary>
        /// Status message when interior removal process ended without errors
        /// </summary>
        private const string _STATUS_REMOVING_INTERIOR_SET_OK =
            "Selected interior set was removed succesfully.";

        /// <summary>
        /// Status message when interior copy process ended without errors
        /// </summary>
        private const string _STATUS_COPY_INTERIOR_SET_OK =
            "Selected interior sets were succesfully copied.";

        /// <summary>
        /// Status message when exterior copy process ended without errors
        /// </summary>
        private const string _STATUS_COPY_EXTERIOR_SET_OK =
            "Current exterior set was succesfully copied.";
        
        /// <summary>
        /// Status message when interior paste process ended without errors
        /// </summary>
        private const string _STATUS_PASTE_INTERIOR_SET_OK =
            "Copied interior sets were succesfully pasted.";

        /// <summary>
        /// Status message when exterior paste process ended without errors
        /// </summary>
        private const string _STATUS_PASTE_EXTERIOR_SET_OK =
            "Copied exterior set was succesfully pasted.";

        /// <summary>
        /// Message to display in colors browser
        /// </summary>
        private const string _MESSAGE_BROWSE_COLORS = "Please select new color for this set.";

        /// <summary>
        /// Message to display in set names browser
        /// </summary>
        private const string _MESSAGE_BROWSE_NEW_SET_NAMES = "Please select a name for this new set.";

        /// <summary>
        /// Message to display in set names browser
        /// </summary>
        private const string _MESSAGE_BROWSE_SET_NAMES = "Please select a new name for this set.";
        #endregion

        #region Members
        /// <summary>
        /// Internal id of current color set
        /// </summary>
        private string _CurrentColorSetId;

        /// <summary>
        /// List of copied interior set ids
        /// </summary>
        private readonly Collection<string> _CopiedInteriorSetIds = new Collection<string>();

        /// <summary>
        /// Identifier of copied exterior set
        /// </summary>
        private string _CopiedExteriorSetId;
        #endregion

        #region Private methods

        /// <summary>
        /// Defines tab contents
        /// </summary>
        private void _InitializeColorsContents()
        {
            // Color sets
            _CurrentColorSetId = null;

            colorsSetsComboBox.Items.Clear();

            List<DB.Entry> allSets = DatabaseHelper.SelectAllCellsFromTopicWhereCellValue(_CarColorsTable,
                                                                                          SharedConstants.
                                                                                              CAR_CARCOLORS_DB_COLUMN,
                                                                                          _CurrentVehicle);
            foreach (DB.Entry anotherEntry in allSets)
            {
                DB.Cell setColorNameCell =
                    DatabaseHelper.GetCellFromEntry(_CarColorsTable, anotherEntry,
                                                    SharedConstants.COLOR_NAME_CARCOLORS_DB_COLUMN);
                string colorCode = DatabaseHelper.GetResourceValueFromCell(setColorNameCell, _CarColorsResource);
                string itemText = string.Format(_FORMAT_COLOR_EXT_SET_ITEM, colorCode, anotherEntry.index);

                colorsSetsComboBox.Items.Add(itemText);
            }

            // Price
            colorsPriceTextBox.Text = "";

            // Color links
            colorsMainLinkLabel.Text =
                colorsSecondaryLinkLabel.Text = colorsCalipersLinkLabel.Text = _LABEL_DEFAULT_COLOR_LINK;

            // Interior colors
            colorsInteriorListView.Items.Clear();

            // Interior/Exterior paste buttons
            _UpdateColorPasteButtons();
        }

        /// <summary>
        /// Defines selected set information
        /// </summary>
        private void _LoadColorInformation()
        {
            if (_CurrentColorSetId == null)
                return;

            // Whole data
            int setId = int.Parse(_CurrentColorSetId);
            DB.Entry setEntry = _CarColorsTable.Entries[setId];

            // Price
            DB.Cell priceCell =
                DatabaseHelper.GetCellFromEntry(_CarColorsTable, setEntry,
                                                SharedConstants.PRICE_DOLLAR_CARCOLORS_DB_COLUMN);

            colorsPriceTextBox.Text = priceCell.value;

            // Exterior colors
            _UpdateExteriorColors(setEntry);

            // Interior colors
            _UpdateInteriorColors(setEntry);
        }

        /// <summary>
        /// Refreshes interior colors
        /// </summary>
        /// <param name="setEntry"></param>
        private void _UpdateInteriorColors(DB.Entry setEntry)
        {
            colorsInteriorListView.Items.Clear();

            // Parsing interior color cells
            for (int cellIndex = 1; cellIndex <= 15; cellIndex++)
            {
                string columnName = string.Format(SharedConstants.INTERIOR_CARCOLORS_PATTERN_DB_COLUMN, cellIndex);

                DB.Cell currentCell = DatabaseHelper.GetCellFromEntry(_CarColorsTable, setEntry, columnName);
                string colorRef = DatabaseHelper.GetResourceValueFromCell(currentCell, _CarColorsResource);
                List<DB.Entry> interiorEntries =
                    DatabaseHelper.SelectAllCellsFromTopicWherePrimaryKey(_InteriorTable, colorRef);
                DB.Entry fullInteriorEntry;

                // BUG_78: if interior color does not exist...
                if (interiorEntries.Count == 0)
                {
                    Log.Warning("Interior color " + colorRef + " is not available! Ignoring...");
                    colorRef = SharedConstants.NO_COLOR_INTERIOR_DB_VAL;
                    fullInteriorEntry =
                        DatabaseHelper.SelectAllCellsFromTopicWherePrimaryKey(_InteriorTable, colorRef)[0];
                }
                else
                    fullInteriorEntry = interiorEntries[0];

                // Color name
                DB.Cell interiorNameCell =
                    DatabaseHelper.GetCellFromEntry(_InteriorTable, fullInteriorEntry,
                                                    SharedConstants.INTERIOR_NAME_INTERIOR_DB_COLUMN);
                string name = DatabaseHelper.GetResourceValueFromCell(interiorNameCell, _InteriorResource);
                string brandRef =
                    DatabaseHelper.GetCellFromEntry(_InteriorTable, fullInteriorEntry,
                                                    SharedConstants.MANUFACTURER_ID_INTERIOR_DB_COLUMN).value;
                string manufName = _BrandsResource.GetEntryFromId(brandRef).value;
                string colorName = string.Format(_FORMAT_INT_COLOR_SET_LABEL, manufName, name);

                // Colors
                DB.Cell interiorColorCell =
                    DatabaseHelper.GetCellFromEntry(_InteriorTable, fullInteriorEntry,
                                                    SharedConstants.INTERIOR_COLOR_1_INTERIOR_DB_COLUMN);
                string interiorColor1Code = DatabaseHelper.GetResourceValueFromCell(interiorColorCell, _InteriorResource);
                string interiorColor1Name = ColorsHelper.GetColorName(interiorColor1Code);

                interiorColorCell = DatabaseHelper.GetCellFromEntry(_InteriorTable, fullInteriorEntry,
                                                                    SharedConstants.INTERIOR_COLOR_2_INTERIOR_DB_COLUMN);

                string interiorColor2Code = DatabaseHelper.GetResourceValueFromCell(interiorColorCell, _InteriorResource);
                string interiorColor2Name = ColorsHelper.GetColorName(interiorColor2Code);

                // Material
                DB.Cell materialCell =
                    DatabaseHelper.GetCellFromEntry(_InteriorTable, fullInteriorEntry,
                                                    SharedConstants.MATERIAL_INTERIOR_DB_COLUMN);
                string materialCode = DatabaseHelper.GetResourceValueFromCell(materialCell, _InteriorResource);
                string materialName = ColorsHelper.GetColorName(materialCode);

                // Adding item to list
                ListViewItem li = new ListViewItem(cellIndex.ToString()) {Tag = colorRef};

                li.SubItems.Add(colorName);
                li.SubItems.Add(string.Format(_FORMAT_COLOR_NAME_LABEL, interiorColor1Code, interiorColor1Name));
                li.SubItems.Add(string.Format(_FORMAT_COLOR_NAME_LABEL, interiorColor2Code, interiorColor2Name));
                li.SubItems.Add(string.Format(_FORMAT_COLOR_NAME_LABEL, materialCode, materialName));
                colorsInteriorListView.Items.Add(li);
            }
        }

        /// <summary>
        /// Refreshes exterior colors
        /// </summary>
        /// <param name="setEntry"></param>
        private void _UpdateExteriorColors(DB.Entry setEntry)
        {
            // Main
            DB.Cell mainColorCell = DatabaseHelper.GetCellFromEntry(_CarColorsTable, setEntry,
                                                                    SharedConstants.COLOR_ID1_CARCOLORS_DB_COLUMN);
            string colorCode = DatabaseHelper.GetResourceValueFromCell(mainColorCell, _CarColorsResource);
            string colorName = ColorsHelper.GetColorName(colorCode);

            colorsMainLinkLabel.Text = string.Format(_FORMAT_COLOR_NAME_LABEL, colorCode, colorName);

            // Secondary (optional)
            DB.Cell secondaryColorCell = DatabaseHelper.GetCellFromEntry(_CarColorsTable, setEntry,
                                                                         SharedConstants.COLOR_ID2_CARCOLORS_DB_COLUMN);

            if (SharedConstants.NOT_AVAILABLE_COLOR_CARCOLORS_DB_RESID.Equals(secondaryColorCell.value))
                colorsSecondaryLinkLabel.Text = _LABEL_UNAVAILABLE_COLOR;
            else
            {
                colorCode = DatabaseHelper.GetResourceValueFromCell(secondaryColorCell, _CarColorsResource);
                colorName = ColorsHelper.GetColorName(colorCode);
                colorsSecondaryLinkLabel.Text = string.Format(_FORMAT_COLOR_NAME_LABEL, colorCode, colorName);
            }

            // Calipers (optional)
            DB.Cell calipersColorCell = DatabaseHelper.GetCellFromEntry(_CarColorsTable, setEntry,
                                                                        SharedConstants.CALLIPERS_CARCOLORS_DB_COLUMN);

            if (SharedConstants.NOT_AVAILABLE_CALIPERS_COLOR_CARCOLORS_DB_RESID.Equals(calipersColorCell.value))
                colorsCalipersLinkLabel.Text = _LABEL_UNAVAILABLE_COLOR;
            else
            {
                colorCode = DatabaseHelper.GetResourceValueFromCell(calipersColorCell, _CarColorsResource);
                colorName = ColorsHelper.GetColorName(colorCode);
                colorsCalipersLinkLabel.Text = string.Format(_FORMAT_COLOR_NAME_LABEL, colorCode, colorName);
            }
        }

        /// <summary>
        /// Allows to change main exterior color
        /// </summary>
        private bool _ChangeMainColor()
        {
            bool returnedValue = false;
            string newColorId = _PickPaintColor(false);

            if (newColorId != null)
            {
                // Applying changes
                int entryId = int.Parse(_CurrentColorSetId);

                DatabaseHelper.UpdateCellFromTopicWithEntryId(_CarColorsTable,
                                                              SharedConstants.COLOR_ID1_CARCOLORS_DB_COLUMN,
                                                              newColorId, entryId);
                returnedValue = true;
            }

            return returnedValue;
        }

        /// <summary>
        /// Allows to change secondary exterior color
        /// </summary>
        private bool _ChangeSecondaryColor()
        {
            bool returnedValue = false;
            string newColorId = _PickPaintColor(false);

            if (newColorId != null)
            {
                // Applying changes
                int entryId = int.Parse(_CurrentColorSetId);

                DatabaseHelper.UpdateCellFromTopicWithEntryId(_CarColorsTable,
                                                              SharedConstants.COLOR_ID2_CARCOLORS_DB_COLUMN,
                                                              newColorId, entryId);
                returnedValue = true;
            }

            return returnedValue;
        }

        /// <summary>
        /// Allows to change secondary exterior color
        /// </summary>
        private bool _ChangeCalipersColor()
        {
            bool returnedValue = false;
            string newColorId = _PickPaintColor(true);

            if (newColorId != null)
            {
                // Applying changes
                int entryId = int.Parse(_CurrentColorSetId);

                DatabaseHelper.UpdateCellFromTopicWithEntryId(_CarColorsTable,
                                                              SharedConstants.CALLIPERS_CARCOLORS_DB_COLUMN,
                                                              newColorId, entryId);
                returnedValue = true;
            }

            return returnedValue;
        }

        /// <summary>
        /// Allows to change price of current color set
        /// </summary>
        private void _ChangeColorSetPrice()
        {
            if (!string.IsNullOrEmpty(colorsPriceTextBox.Text))
            {
                try
                {
                    int newPrice = int.Parse(colorsPriceTextBox.Text);

                    // Checking limits
                    if (newPrice >= 0)
                    {
                        int entryId = int.Parse(_CurrentColorSetId);

                        DatabaseHelper.UpdateCellFromTopicWithEntryId(_CarColorsTable,
                                                                      SharedConstants.PRICE_DOLLAR_CARCOLORS_DB_COLUMN,
                                                                      colorsPriceTextBox.Text,
                                                                      entryId);
                    }
                }
                catch (Exception)
                {
                    Log.Info("Invalid color price value provided: " + colorsPriceTextBox.Text);
                }
            }
        }

        /// <summary>
        /// Allows to choose a paint color (exterior/calipers)
        /// </summary>
        /// <param name="isForCalipers"></param>
        /// <returns></returns>
        private static string _PickPaintColor(bool isForCalipers)
        {
            // Preparing color list
            string returnedColor = null;
            SortedStringCollection sortedColorList = new SortedStringCollection();
            Dictionary<string, string> index = new Dictionary<string, string>();
            Dictionary<string, string> currentReference = (isForCalipers
                                                               ? ColorsHelper.CalipersReference
                                                               : ColorsHelper.ExteriorReference);

            // [BUG_101] Ability to specify 'no color'
            string colorName = string.Format(_FORMAT_COLOR_NAME_LABEL, "", _LABEL_NO_COLOR);

            sortedColorList.Add(colorName);
            index.Add(colorName, SharedConstants.NOT_AVAILABLE_COLOR_CARCOLORS_DB_RESID);
            //

            foreach (KeyValuePair<string, string> pair in currentReference)
            {
                colorName = string.Format(_FORMAT_COLOR_NAME_LABEL, pair.Key, pair.Value);

                if (ColorsHelper.IdByCodeReference.ContainsKey(pair.Key))
                {
                    string colorId = ColorsHelper.IdByCodeReference[pair.Key];

                    sortedColorList.Add(colorName);
                    index.Add(colorName, colorId);
                }
            }

            // Displaying browse dialog
            TableBrowsingDialog dialog = new TableBrowsingDialog(_MESSAGE_BROWSE_COLORS, sortedColorList, index);
            DialogResult dr = dialog.ShowDialog();

            if (dr == DialogResult.OK && dialog.SelectedIndex != null)
                returnedColor = dialog.SelectedIndex;

            return returnedColor;
        }

        /// <summary>
        /// Handles color set adding
        /// </summary>
        /// <returns></returns>
        private bool _AddColorSet()
        {
            bool returnedResult = false;

            // Color set name
            string setName = _BrowseExteriorNames(true);

            if (setName != null)
            {
                Cursor = Cursors.WaitCursor;

                // Applying changes
                string value = DatabaseHelper.BuildFullLineValue(_CarColorsTable,
                                                                 _CurrentVehicle,
                                                                 SharedConstants.NOT_AVAILABLE_COLOR_CARCOLORS_DB_RESID,
                                                                 setName,
                                                                 SharedConstants.NOT_AVAILABLE_COLOR_CARCOLORS_DB_RESID,
                                                                 SharedConstants.NOT_AVAILABLE_COLOR_CARCOLORS_DB_RESID,
                                                                 "0",
                                                                 "0",
                                                                 SharedConstants.NO_COLOR_INTERIOR_DB_VAL,
                                                                 SharedConstants.NO_COLOR_INTERIOR_DB_VAL,
                                                                 SharedConstants.NO_COLOR_INTERIOR_DB_VAL,
                                                                 SharedConstants.NO_COLOR_INTERIOR_DB_VAL,
                                                                 SharedConstants.NO_COLOR_INTERIOR_DB_VAL,
                                                                 SharedConstants.NO_COLOR_INTERIOR_DB_VAL,
                                                                 SharedConstants.NO_COLOR_INTERIOR_DB_VAL,
                                                                 SharedConstants.NO_COLOR_INTERIOR_DB_VAL,
                                                                 SharedConstants.NO_COLOR_INTERIOR_DB_VAL,
                                                                 SharedConstants.NO_COLOR_INTERIOR_DB_VAL,
                                                                 SharedConstants.NO_COLOR_INTERIOR_DB_VAL,
                                                                 SharedConstants.NO_COLOR_INTERIOR_DB_VAL,
                                                                 SharedConstants.NO_COLOR_INTERIOR_DB_VAL,
                                                                 SharedConstants.NO_COLOR_INTERIOR_DB_VAL,
                                                                 SharedConstants.NO_COLOR_INTERIOR_DB_VAL);

                DatabaseHelper.InsertAllCellsIntoTopic(_CarColorsTable, value);
                returnedResult = true;
            }

            return returnedResult;
        }

        /// <summary>
        /// Handles color set renaming
        /// </summary>
        /// <returns></returns>
        private bool _RenameColorSet()
        {
            bool returnedResult = false;

            if (!string.IsNullOrEmpty(_CurrentColorSetId))
            {
                // New color set name
                string setName = _BrowseExteriorNames(false);

                if (setName != null)
                {
                    Cursor = Cursors.WaitCursor;

                    // Getting current entry id
                    int id = int.Parse(_CurrentColorSetId);

                    // Applying changes
                    DatabaseHelper.UpdateCellFromTopicWithEntryId(_CarColorsTable, SharedConstants.COLOR_NAME_CARCOLORS_DB_COLUMN, setName, id);

                    returnedResult = true;
                }  
            }

            return returnedResult;
        }

        /// <summary>
        /// Displays a dialog box containing all exterior color names
        /// </summary>
        /// <returns>null if no set has been selected</returns>
        private string _BrowseExteriorNames(bool isAdding)
        {
            string setName = null;

            // Preparing name list
            SortedStringCollection sortedNameList = new SortedStringCollection();
            Dictionary<string, string> index = new Dictionary<string, string>();

            foreach (DBResource.Entry anotherEntry in _CarColorsResource.EntryList)
            {
                if (anotherEntry.isValid && !anotherEntry.isComment)
                {
                    if (anotherEntry.id.Id.EndsWith(SharedConstants.NAME_EXTERIOR_CATEGORY))
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
            string message = (isAdding ? _MESSAGE_BROWSE_SET_NAMES : _MESSAGE_BROWSE_NEW_SET_NAMES);

            TableBrowsingDialog dialog = new TableBrowsingDialog(message, sortedNameList, index);
            DialogResult dr = dialog.ShowDialog();

            if (dr == DialogResult.OK && dialog.SelectedIndex != null)
                setName = dialog.SelectedIndex;

            return setName;
        }

        /// <summary>
        /// Handles color set removal
        /// </summary>
        /// <returns></returns>
        private bool _RemoveColorSet()
        {
            bool returnedResult = false;

            if (!string.IsNullOrEmpty(_CurrentColorSetId))
            {
                // Is this id currently copied ?
                if (_CurrentColorSetId.Equals(_CopiedExteriorSetId))
                {
                    _CopiedExteriorSetId = null;
                    _UpdateColorPasteButtons();
                }

                // Applying changes
                int id = int.Parse(_CurrentColorSetId);

                DatabaseHelper.DeleteFromTopicWithEntryId(_CarColorsTable, id);

                returnedResult = true;
            }

            return returnedResult;
        }

        /// <summary>
        /// Handles moving exterior color set down or up
        /// </summary>
        /// <param name="isDown">true to move down, false to move up</param>
        /// <returns></returns>
        private bool _MoveColorSet(bool isDown)
        {
            bool returnedResult = false;

            if (!string.IsNullOrEmpty(_CurrentColorSetId))
            {
                int id1 = -1;
                int id2 = int.Parse(_CurrentColorSetId);

                if (isDown && colorsSetsComboBox.SelectedIndex < colorsSetsComboBox.Items.Count - 1)
                {                
                    int nextComboId = colorsSetsComboBox.SelectedIndex + 1;
                    string nextColorSet = colorsSetsComboBox.Items[nextComboId].ToString();

                    id1 = int.Parse(nextColorSet.Split(Tools.SYMBOL_VALUE_SEPARATOR)[1]);
                    returnedResult = true;
                }
                else if (!isDown && colorsSetsComboBox.SelectedIndex > 0)
                {
                    int previousComboId = colorsSetsComboBox.SelectedIndex - 1;
                    string previousColorSet = colorsSetsComboBox.Items[previousComboId].ToString();

                    id1 = int.Parse(previousColorSet.Split(Tools.SYMBOL_VALUE_SEPARATOR)[1]);
                    returnedResult = true;
                }


                // Applying changes    
                if (returnedResult)
                {
                    // If copied id, remove it from copy area as it's not the same...
                    if (_CurrentColorSetId.Equals(_CopiedExteriorSetId))
                        _CopiedExteriorSetId = null;

                    DatabaseHelper.SwapEntriesWithEntryId(_CarColorsTable, id1, id2);
                }   
            }

            return returnedResult;
        }

        /// <summary>
        /// Handles interior color set removal
        /// </summary>
        /// <param name="interiorSlot"></param>
        private void _RemoveInteriorColorSet(int interiorSlot)
        {
            // Getting current ids 
            int exteriorSetId = int.Parse(_CurrentColorSetId);
            string columnName = string.Format(SharedConstants.INTERIOR_CARCOLORS_PATTERN_DB_COLUMN, interiorSlot);

            DatabaseHelper.UpdateCellFromTopicWithEntryId(_CarColorsTable, columnName,
                                                          SharedConstants.NO_COLOR_INTERIOR_DB_VAL, exteriorSetId);
        }

        /// <summary>
        /// Moves selected vehicle to next slot
        /// </summary>
        /// <param name="listViewItem"></param>
        /// <returns></returns>
        private bool _IncreaseInteriorSlot(ListViewItem listViewItem)
        {
            bool isOK = false;

            if (listViewItem != null)
            {
                int slotIndex = int.Parse(listViewItem.Text);

                if (slotIndex < 15)
                {
                    _SwapInteriorSlots(slotIndex, slotIndex + 1);
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
        private bool _DecreaseInteriorSlot(ListViewItem listViewItem)
        {
            bool isOK = false;

            if (listViewItem != null)
            {
                int slotIndex = int.Parse(listViewItem.Text);

                if (slotIndex > 0)
                {
                    _SwapInteriorSlots(slotIndex, slotIndex - 1);
                    isOK = true;
                }
            }

            return isOK;
        }

        /// <summary>
        /// Swaps values between provided interior slots indexes
        /// </summary>
        /// <param name="slotIndex1"></param>
        /// <param name="slotIndex2"></param>
        private void _SwapInteriorSlots(int slotIndex1, int slotIndex2)
        {
            // Getting current id 
            int setId = int.Parse(_CurrentColorSetId);

            string columnName1 = string.Format(SharedConstants.INTERIOR_CARCOLORS_PATTERN_DB_COLUMN, slotIndex1);
            string columnName2 = string.Format(SharedConstants.INTERIOR_CARCOLORS_PATTERN_DB_COLUMN, slotIndex2);
            string value1 =
                DatabaseHelper.SelectCellsFromTopicWithEntryId(columnName1, _CarColorsTable, setId)[0].value;
            string value2 =
                DatabaseHelper.SelectCellsFromTopicWithEntryId(columnName2, _CarColorsTable, setId)[0].value;

            // Updating database
            DatabaseHelper.UpdateCellFromTopicWithEntryId(_CarColorsTable, columnName1, value2, setId);
            DatabaseHelper.UpdateCellFromTopicWithEntryId(_CarColorsTable, columnName2, value1, setId);
        }

        /// <summary>
        /// Adds specified interior color reference to copy-list
        /// </summary>
        /// <param name="currentInteriorRef"></param>
        private void _AddInteriorSetToCopy(string currentInteriorRef)
        {
            if (!string.IsNullOrEmpty(currentInteriorRef)
                && !SharedConstants.NO_COLOR_INTERIOR_DB_VAL.Equals(currentInteriorRef))
                _CopiedInteriorSetIds.Add(currentInteriorRef);

            // Paste button appearance
            _UpdateColorPasteButtons();
        }

        /// <summary>
        /// Defines appearance of paste buttons according to copy-list contents
        /// </summary>
        private void _UpdateColorPasteButtons()
        {
            colorsInteriorPasteButton.Enabled = (_CopiedInteriorSetIds.Count != 0);
            colorsExteriorPasteButton.Enabled = (_CopiedExteriorSetId != null);
        }

        /// <summary>
        /// Puts specified interior set id to next free slot
        /// </summary>
        /// <param name="anotherId"></param>
        private void _PasteInteriorSet(string anotherId)
        {
            if (!string.IsNullOrEmpty(anotherId))
            {
                bool isFinished = false;

                for (int i = 1; !isFinished && i <= 15; i++)
                {
                    int entryId = int.Parse(_CurrentColorSetId);
                    string cellName = string.Format(SharedConstants.INTERIOR_CARCOLORS_PATTERN_DB_COLUMN, i);
                    DB.Cell currentCell =
                        DatabaseHelper.SelectCellsFromTopicWithEntryId(cellName, _CarColorsTable, entryId)[0];

                    if (SharedConstants.NO_COLOR_INTERIOR_DB_VAL.Equals(currentCell.value))
                    {
                        // Free cell !
                        DatabaseHelper.UpdateCellFromTopicWithEntryId(_CarColorsTable, cellName, anotherId, entryId);
                        isFinished = true;
                    }
                }
            }
        }

        /// <summary>
        /// Puts copied exterior set id to next free slot
        /// </summary>
        private bool _PasteExteriorSet()
        {
            bool returnedResult = false;

            if (!string.IsNullOrEmpty(_CopiedExteriorSetId))
            {
                // Get copied value
                int copiedId = int.Parse(_CopiedExteriorSetId);
                DB.Entry copiedEntry =
                    DatabaseHelper.SelectAllCellsFromTopicWhereId(_CarColorsTable, new[] {copiedId})[0];

                if (copiedEntry.cells != null && copiedEntry.cells.Count > 0)
                {
                    // Applying changes
                    List<DB.Cell> cells = copiedEntry.cells;
                    string value = DatabaseHelper.BuildFullLineValue(_CarColorsTable,
                                                                     _CurrentVehicle,
                                                                     cells[1].value,
                                                                     cells[2].value,
                                                                     cells[3].value,
                                                                     cells[4].value,
                                                                     cells[5].value,
                                                                     cells[6].value,
                                                                     cells[7].value,
                                                                     cells[8].value,
                                                                     cells[9].value,
                                                                     cells[10].value,
                                                                     cells[11].value,
                                                                     cells[12].value,
                                                                     cells[13].value,
                                                                     cells[14].value,
                                                                     cells[15].value,
                                                                     cells[16].value,
                                                                     cells[17].value,
                                                                     cells[18].value,
                                                                     cells[19].value,
                                                                     cells[20].value,
                                                                     cells[21].value);

                    DatabaseHelper.InsertAllCellsIntoTopic(_CarColorsTable, value);
                    returnedResult = true;
                }
            }

            return returnedResult;
        }
        #endregion

        #region Events

        private void colorsSetsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Another color set has been selected
            string selectedSet = colorsSetsComboBox.Text;

            if (!string.IsNullOrEmpty(selectedSet))
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    _CurrentColorSetId = selectedSet.Split(Tools.SYMBOL_VALUE_SEPARATOR)[1];
                    _LoadColorInformation();
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

        private void colorsMainLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on main color link
            if (!string.IsNullOrEmpty(colorsSetsComboBox.Text))
            {
                try
                {
                    bool isModified = _ChangeMainColor();

                    if (isModified)
                    {
                        Cursor = Cursors.WaitCursor;

                        // Reloading
                        _LoadColorInformation();

                        // Modification flag
                        _IsDatabaseModified = true;

                        StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_MAIN_EXTERIOR_COLOR_OK);

                        Cursor = Cursors.Default;
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void colorsSecondaryLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on secondary color link
            if (!string.IsNullOrEmpty(colorsSetsComboBox.Text))
            {
                try
                {
                    bool isModified = _ChangeSecondaryColor();

                    if (isModified)
                    {
                        Cursor = Cursors.WaitCursor;

                        // Reloading
                        _LoadColorInformation();

                        // Modification flag
                        _IsDatabaseModified = true;

                        StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_SEC_EXTERIOR_COLOR_OK);

                        Cursor = Cursors.Default;
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void colorsCalipersLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on calipers color link
            if (!string.IsNullOrEmpty(colorsSetsComboBox.Text))
            {
                try
                {
                    bool isModified = _ChangeCalipersColor();

                    if (isModified)
                    {
                        Cursor = Cursors.WaitCursor;

                        // Reloading
                        _LoadColorInformation();

                        // Modification flag
                        _IsDatabaseModified = true;

                        StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_CALIPERS_COLOR_OK);

                        Cursor = Cursors.Default;
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void colorsPriceSetButton_Click(object sender, EventArgs e)
        {
            // Click on set button (color set price)
            if (!string.IsNullOrEmpty(colorsSetsComboBox.Text))
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    _ChangeColorSetPrice();

                    // Modification flag
                    _IsDatabaseModified = true;

                    // Reloading
                    _LoadColorInformation();

                    StatusBarLogManager.ShowEvent(this, _STATUS_CHANGING_SET_PRICE_OK);

                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void colorsAddSetButton_Click(object sender, EventArgs e)
        {
            // Click on Add button
            try
            {
                if (_AddColorSet())
                {
                    // Modification flag
                    _IsDatabaseModified = true;

                    // Reloading
                    _InitializeColorsContents();

                    // Selecting recently added color set
                    colorsSetsComboBox.SelectedIndex = colorsSetsComboBox.Items.Count - 1;

                    StatusBarLogManager.ShowEvent(this, _STATUS_ADDING_SET_OK);
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

        private void colorsRemoveSetButton_Click(object sender, EventArgs e)
        {
            // Click on Remove button
            try
            {
                if (_RemoveColorSet())
                {
                    // Modification flag
                    _IsDatabaseModified = true;

                    // Reloading
                    _InitializeColorsContents();

                    StatusBarLogManager.ShowEvent(this, _STATUS_REMOVING_SET_OK);
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

        private void colorsInteriorChooseButton_Click(object sender, EventArgs e)
        {
            // Click on 'Choose' button
            if (colorsInteriorListView.SelectedItems.Count == 1)
            {
                // Opening interior set dialog...
                string currentInteriorSetId = colorsInteriorListView.SelectedItems[0].Tag.ToString();
                InteriorSetDialog dlg =
                    new InteriorSetDialog(_InteriorTable, _InteriorResource, _BrandsTable, _BrandsResource,
                                          _CarColorsTable,
                                          currentInteriorSetId);
                DialogResult dr = dlg.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;

                        // Applying selected interior set
                        string colName =
                            string.Format(SharedConstants.INTERIOR_CARCOLORS_PATTERN_DB_COLUMN,
                                          colorsInteriorListView.SelectedItems[0].Text);

                        DatabaseHelper.UpdateCellFromTopicWithEntryId(_CarColorsTable, colName, dlg.SelectedInteriorSet,
                                                                      int.Parse(_CurrentColorSetId));

                        // Refreshing set list 
                        int setId = int.Parse(_CurrentColorSetId);
                        DB.Entry setEntry = _CarColorsTable.Entries[setId];

                        ListView2.StoreSelectedIndex(colorsInteriorListView);
                        _UpdateInteriorColors(setEntry);
                        ListView2.RestoreSelectedIndex(colorsInteriorListView);

                        // Modification flag
                        _IsDatabaseModified = true;

                        StatusBarLogManager.ShowEvent(this, _STATUS_CHOOSING_INTERIOR_SET_OK);
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
        }

        private void colorsInteriorListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Double click on interior set > edit
            if (colorsInteriorListView.SelectedItems.Count == 1)
                colorsInteriorChooseButton_Click(sender, new EventArgs());
        }

        private void colorsInteriorSetDownButton_Click(object sender, EventArgs e)
        {
            // Click on 'Interior set down' button
            if (colorsInteriorListView.SelectedItems.Count == 1)
            {
                ListViewItem selectedItem = colorsInteriorListView.SelectedItems[0];

                try
                {
                    Cursor = Cursors.WaitCursor;

                    bool isOK = _IncreaseInteriorSlot(selectedItem);

                    if (isOK)
                    {
                        // Reloading
                        ListView2.StoreSelectedIndex(colorsInteriorListView);

                        // Refreshing set list 
                        int setId = int.Parse(_CurrentColorSetId);
                        DB.Entry setEntry = _CarColorsTable.Entries[setId];

                        _UpdateInteriorColors(setEntry);

                        ListView2.RestoreSelectedIndex(colorsInteriorListView);

                        // Selecting right line
                        ListView2.SelectNextItem(colorsInteriorListView);

                        // Modification flag
                        _IsDatabaseModified = true;

                        StatusBarLogManager.ShowEvent(this, _STATUS_DOWN_INTERIOR_SET_OK);
                    }

                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void colorsInteriorSetUpButton_Click(object sender, EventArgs e)
        {
            // Click on 'Interior set up' button
            if (colorsInteriorListView.SelectedItems.Count == 1)
            {
                ListViewItem selectedItem = colorsInteriorListView.SelectedItems[0];

                try
                {
                    Cursor = Cursors.WaitCursor;

                    bool isOK = _DecreaseInteriorSlot(selectedItem);

                    if (isOK)
                    {
                        // Reloading
                        ListView2.StoreSelectedIndex(colorsInteriorListView);

                        // Refreshing set list 
                        int setId = int.Parse(_CurrentColorSetId);
                        DB.Entry setEntry = _CarColorsTable.Entries[setId];

                        _UpdateInteriorColors(setEntry);

                        ListView2.RestoreSelectedIndex(colorsInteriorListView);

                        // Selecting right line
                        ListView2.SelectPreviousItem(colorsInteriorListView);

                        // Modification flag
                        _IsDatabaseModified = true;

                        StatusBarLogManager.ShowEvent(this, _STATUS_UP_INTERIOR_SET_OK);
                    }

                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void colorsInteriorRemoveButton_Click(object sender, EventArgs e)
        {
            if (colorsInteriorListView.SelectedItems.Count == 1)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    _RemoveInteriorColorSet(int.Parse(colorsInteriorListView.SelectedItems[0].Text));

                    // Refreshing set list 
                    int setId = int.Parse(_CurrentColorSetId);
                    DB.Entry setEntry = _CarColorsTable.Entries[setId];

                    ListView2.StoreSelectedIndex(colorsInteriorListView);
                    _UpdateInteriorColors(setEntry);
                    ListView2.RestoreSelectedIndex(colorsInteriorListView);

                    // Modification flag
                    _IsDatabaseModified = true;

                    StatusBarLogManager.ShowEvent(this, _STATUS_REMOVING_INTERIOR_SET_OK);
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

        private void colorsInteriorCopyButton_Click(object sender, EventArgs e)
        {
            // Click on 'Copy' button (interior colors)

            // BUG_96 Must clear list before copying again
            if (colorsInteriorListView.SelectedItems.Count != 0)
            {
                Cursor = Cursors.WaitCursor;
                _CopiedInteriorSetIds.Clear();

                try
                {
                    foreach (ListViewItem anotherItem in colorsInteriorListView.SelectedItems)
                    {
                        string currentInteriorRef = anotherItem.Tag as string;

                        _AddInteriorSetToCopy(currentInteriorRef);
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                    _CopiedInteriorSetIds.Clear();
                }
                finally
                {
                    Cursor = Cursors.Default;
                }

                if (_CopiedInteriorSetIds.Count != 0)
                    StatusBarLogManager.ShowEvent(this, _STATUS_COPY_INTERIOR_SET_OK);
            }
        }

        private void colorsInteriorPasteButton_Click(object sender, EventArgs e)
        {
            // Click on 'Paste' button (interior colors)
            bool isFailed = false;

            foreach (string anotherId in _CopiedInteriorSetIds)
            {
                Cursor = Cursors.WaitCursor;

                try
                {
                    _PasteInteriorSet(anotherId);
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                    isFailed = true;
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }

            // Refreshing set list 
            int setId = int.Parse(_CurrentColorSetId);
            DB.Entry setEntry = _CarColorsTable.Entries[setId];

            ListView2.StoreSelectedIndex(colorsInteriorListView);
            _UpdateInteriorColors(setEntry);
            ListView2.RestoreSelectedIndex(colorsInteriorListView);

            // Modification flag
            _IsDatabaseModified = true;

            if (!isFailed)
                StatusBarLogManager.ShowEvent(this, _STATUS_PASTE_INTERIOR_SET_OK);
        }

        private void colorsRenameSetButton_Click(object sender, EventArgs e)
        {
            // Click on rename exterior color set button
            try
            {
                if (_RenameColorSet())
                {
                    // Modification flag
                    _IsDatabaseModified = true;

                    // Preserves current selection
                    int selectedIndex = colorsSetsComboBox.SelectedIndex;

                    // Reloading
                    _InitializeColorsContents();

                    // Restoring previous selection
                    colorsSetsComboBox.SelectedIndex = selectedIndex;

                    StatusBarLogManager.ShowEvent(this, _STATUS_RENAMING_SET_OK);
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

        private void exteriorSetDownButton_Click(object sender, EventArgs e)
        {
            // Click on down arrow for exterior color set
            try
            {
                Cursor = Cursors.WaitCursor;

                if (_MoveColorSet(true))
                {
                    // Modification flag
                    _IsDatabaseModified = true;

                    // Follows current item
                    int selectedIndex = colorsSetsComboBox.SelectedIndex + 1;

                    // Reloading
                    _InitializeColorsContents();

                    // Restoring selection
                    colorsSetsComboBox.SelectedIndex = selectedIndex;

                    StatusBarLogManager.ShowEvent(this, _STATUS_DOWN_EXTERIOR_SET_OK);
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

        private void exteriorSetUpButton_Click(object sender, EventArgs e)
        {
            // Click on down arrow for exterior color set
            try
            {
                Cursor = Cursors.WaitCursor;

                if (_MoveColorSet(false))
                {
                    // Modification flag
                    _IsDatabaseModified = true;

                    // Follows current item
                    int selectedIndex = colorsSetsComboBox.SelectedIndex - 1;

                    // Reloading
                    _InitializeColorsContents();

                    // Restoring selection
                    colorsSetsComboBox.SelectedIndex = selectedIndex;

                    StatusBarLogManager.ShowEvent(this, _STATUS_UP_EXTERIOR_SET_OK);
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

        private void colorsExteriorCopyButton_Click(object sender, EventArgs e)
        {
            // Click on 'Copy' button (exterior colors)
            if (!string.IsNullOrEmpty(_CurrentColorSetId))
            {
                _CopiedExteriorSetId = _CurrentColorSetId;

                // Paste button appearance
                _UpdateColorPasteButtons();

                StatusBarLogManager.ShowEvent(this, _STATUS_COPY_EXTERIOR_SET_OK);
            }
        }

        private void colorsExteriorPasteButton_Click(object sender, EventArgs e)
        {
            // Click on 'Paste' button (exterior colors)
            if (!string.IsNullOrEmpty(_CopiedExteriorSetId))
            {
                Cursor = Cursors.WaitCursor;

                try
                {
                    if (_PasteExteriorSet())
                    {
                        // Modification flag
                        _IsDatabaseModified = true;

                        // Reloading
                        _InitializeColorsContents();

                        // Selecting recently added color set
                        colorsSetsComboBox.SelectedIndex = colorsSetsComboBox.Items.Count - 1;

                        StatusBarLogManager.ShowEvent(this, _STATUS_PASTE_EXTERIOR_SET_OK);
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
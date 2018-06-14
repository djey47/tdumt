using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Dialogs.Search;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.support;
using TDUModdingLibrary.support.constants;
using TDUModdingTools.gui.dbeditor.db;
using TDUModdingTools.gui.settings;

namespace TDUModdingTools.gui.dbeditor
{
    public partial class DbEditorForm : Form
    {
        #region Constants
        /// <summary>
        /// Id column header
        /// </summary>
        private const string _ID_COLUMN_HEADER = "#";

        /// <summary>
        /// Default resource cell contents
        /// </summary>
        private const string _DEFAULT_RESOURCE_VALUE = "<N/A>";

        /// <summary>
        /// Default reference cell contents
        /// </summary>
        private const string _DEFAULT_REFERENCE_VALUE = "<N/A>";

        /// <summary>
        /// Format for resource cell
        /// </summary>
        private const string _FORMAT_RESOURCE_CELL = "{0} ({1})";

        /// <summary>
        /// Format for reference cell
        /// </summary>
        private const string _FORMAT_REFERENCE_CELL = "{0} ({1})";

        /// <summary>
        /// Format for table information
        /// </summary>
        private const string _FORMAT_TABLE_INFORMATION = "{0}: {1} line(s) - {2} column(s)";

        /// <summary>
        /// Format for link tag
        /// </summary>
        private static readonly string _FORMAT_TAG_LINK = "{0}" + Tools.SYMBOL_VALUE_SEPARATOR + "{1}";

        /// <summary>
        /// Label in top of folder browsing dialog box
        /// </summary>
        private const string _LABEL_BROWSE_FOLDER = "Please select parent folder of database files set.";

        /// <summary>
        /// Default label for table information
        /// </summary>
        private const string _LABEL_DB_NOT_LOADED = "Database not loaded. Please select source then click Load button.";

        /// <summary>
        /// Label for search box
        /// </summary>
        private const string _LABEL_SEARCH = "Find a value in current topic...";

        /// <summary>
        /// Status message when database preparing process ended without errors
        /// </summary>
        private const string _STATUS_LOADING_OK = "Database was prepared succesfully. Now ready.";

        /// <summary>
        /// Status message when value copying process ended without errors
        /// </summary>
        private const string _STATUS_COPY_OK = "Selection was copied succesfully to clipboard.";

        /// <summary>
        /// Status message when attempting to navigate without valid reference
        /// </summary>
        private const string _STATUS_NOT_REF_CELL = "Selected cell value is not a reference to another table.";

        /// <summary>
        /// Status message when attempting to edit resource without valid reference
        /// </summary>
        private const string _STATUS_NOT_RES_CELL = "Selected cell value is not a resource.";

        /// <summary>
        /// Status message when resource editing process 
        /// </summary>
        private const string _STATUS_EDIT_RES_OK = "Selected resource was modified succesfully. Database saved.";

        /// <summary>
        /// Tooltip message fore linked references
        /// </summary>
        private const string _TOOLTIP_LINK = "Ref to another topic. ALT+click to navigate";

        /// <summary>
        /// Tooltip message for resources
        /// </summary>
        private const string _TOOLTIP_RESOURCE = "Resource in language file. CTRL+E to edit.";

        /// <summary>
        /// Tooltip message for resources (type 2)
        /// </summary>
        private const string _TOOLTIP_RESOURCE_2 = "Resource in language file (variant). CTRL+E to edit.";

        /// <summary>
        /// Tooltip message for far resources
        /// </summary>
        private const string _TOOLTIP_RESOURCE_FAR = "Resource in other language files. CTRL+E to edit.";

        /// <summary>
        /// Error message when referenced item was not found in database
        /// </summary>
        private const string _ERROR_REF_NOT_FOUND =
            "Referenced item has not been found into target topic. Please use check feature to fix your database.";

        /// <summary>
        /// Error message when referenced resource was not found in database
        /// </summary>
        private const string _ERROR_RES_NOT_FOUND =
            "Referenced resource has not been found into target topic. Please use check feature to fix your database.";
        #endregion

        #region Cell styles
        /// <summary>
        /// Data grid font
        /// </summary>
        private static readonly Font _FONT_DATA = new Font("Lucida Console", 9);

        /// <summary>
        /// Style for identifier cells
        /// </summary>
        private static readonly DataGridViewCellStyle _STYLE_CELL_ID = new DataGridViewCellStyle
        {
            Font = new Font(_FONT_DATA, FontStyle.Bold),
            BackColor = Color.LightGray
        };

        /// <summary>
        /// Style for standard cells
        /// </summary>
        private static readonly DataGridViewCellStyle _STYLE_CELL_STD = new DataGridViewCellStyle
        {
            Font = new Font(_FONT_DATA, FontStyle.Regular)
        };

        /// <summary>
        /// Style for resource cells
        /// </summary>
        private static readonly DataGridViewCellStyle _STYLE_CELL_RESOURCE = new DataGridViewCellStyle
        {
            Font = new Font(_FONT_DATA, FontStyle.Regular),
            ForeColor = Color.Green
        };

        /// <summary>
        /// Style for resource cells when error
        /// </summary>
        private static readonly DataGridViewCellStyle _STYLE_CELL_RESOURCE_ERROR = new DataGridViewCellStyle
        {
            Font = new Font(_FONT_DATA, FontStyle.Regular),
            ForeColor = Color.Red
        };

        /// <summary>
        /// Style for far resource cells
        /// </summary>
        private static readonly DataGridViewCellStyle _STYLE_CELL_RESOURCE_FAR = new DataGridViewCellStyle
        {
            Font = new Font(_FONT_DATA, FontStyle.Regular),
            ForeColor = Color.DarkGreen
        };

        /// <summary>
        /// Style for primary key cells
        /// </summary>
        private static readonly DataGridViewCellStyle _STYLE_CELL_PK = new DataGridViewCellStyle
        {
            Font = new Font(_FONT_DATA, FontStyle.Regular),
            ForeColor = Color.Orange
        };

        /// <summary>
        /// Style for link cells
        /// </summary>
        private static readonly DataGridViewCellStyle _STYLE_CELL_LINK = new DataGridViewCellStyle
        {
            Font = new Font(_FONT_DATA, FontStyle.Underline),
            ForeColor = Color.Blue
        };

        /// <summary>
        /// Style for link cells when error
        /// </summary>
        private static readonly DataGridViewCellStyle _STYLE_CELL_LINK_ERROR = new DataGridViewCellStyle
        {
            Font = new Font(_FONT_DATA, FontStyle.Underline),
            ForeColor = Color.White,
            BackColor = Color.Red
        };

        /// <summary>
        /// Style for bitfield cells
        /// </summary>
        private static readonly DataGridViewCellStyle _STYLE_CELL_BITFIELD = new DataGridViewCellStyle
        {
            Font = new Font(_FONT_DATA, FontStyle.Regular),
            ForeColor = Color.DodgerBlue
        };

        /// <summary>
        /// Style for marked cells (regular)
        /// </summary>
        private static readonly DataGridViewCellStyle _STYLE_CELL_MARKED = new DataGridViewCellStyle(_STYLE_CELL_STD)
        {
            BackColor = Color.Yellow
        };

        /// <summary>
        /// Style for marked cells (links)
        /// </summary>
        private static readonly DataGridViewCellStyle _STYLE_CELL_LINK_MARKED = new DataGridViewCellStyle(_STYLE_CELL_LINK)
        {
            BackColor = Color.Yellow
        };

        /// <summary>
        /// Style for marked cells (res)
        /// </summary>
        private static readonly DataGridViewCellStyle _STYLE_CELL_RESOURCE_MARKED = new DataGridViewCellStyle(_STYLE_CELL_RESOURCE)
        {
            BackColor = Color.Yellow
        };

        /// <summary>
        /// Style for marked cells (far res)
        /// </summary>
        private static readonly DataGridViewCellStyle _STYLE_CELL_RESOURCE_FAR_MARKED = new DataGridViewCellStyle(_STYLE_CELL_RESOURCE_FAR)
        {
            BackColor = Color.Yellow
        };
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
                    _Instance = new DbEditorForm();

                return _Instance;
            }
        }
        private static Form _Instance;
        #endregion

        #region Private fields
        /// <summary>
        /// Instance of db BNK
        /// </summary>
        private BNK _DatabaseBnk;

        /// <summary>
        /// Instance of res BNK
        /// </summary>
        private BNK _ResourceBnk;

        /// <summary>
        /// Used database folder
        /// </summary>
        private string _DatabaseFolder;

        /// <summary>
        /// Database
        /// </summary>
        private readonly Dictionary<DB.Topic, TduFile[]> _Data = new Dictionary<DB.Topic, TduFile[]>();

        /// <summary>
        /// Alt key indicator
        /// </summary>
        private bool _IsAltKeyPressed;

        /// <summary>
        /// Instance of search box
        /// </summary>
        private static SearchBox _SearchBoxInstance;
        #endregion

        public DbEditorForm()
        {
            InitializeComponent();
            _InitializeContents();
        }

        #region Private methods
        /// <summary>
        /// Returns instance of Search Box
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static SearchBox _GetSearchBoxInstance(DataGridView gridView, string message)
        {
            if (_SearchBoxInstance == null)
                _SearchBoxInstance = new DataGridViewSearchBox(gridView, message);

            return _SearchBoxInstance;
        }

        /// <summary>
        /// Defines window contents
        /// </summary>
        private void _InitializeContents()
        {
            // StatusLog support
            StatusBarLogManager.AddNewLog(this, toolStripStatusLabel);

            // DB source
            _SetSelectionControls();

            // Table information
            topicInfoLabel.Text = _LABEL_DB_NOT_LOADED;
        }

        /// <summary>
        /// Defines appearance of selection controls according to current mode
        /// </summary>
        private void _SetSelectionControls()
        {
            bool isByFolder = dbSourceFolderRadioButton.Checked;

            inputFolderPath.Enabled = isByFolder;
            browseDBButton.Enabled = isByFolder;
        }

        /// <summary>
        /// Initializes or refreshes table section for specified topic
        /// </summary>
        /// <param name="topic"></param>
        private void _RefreshTableSection(DB.Topic topic)
        {
            // Loads topic and resource if necessary
            if (topic != DB.Topic.None)
            {
                TabPage topicTabPage = topicTabControl.TabPages[topic.ToString()];

                // Is topic to be loaded ?
                TduFile[] databaseItems = _Data[topic];
                DB dbTopic;
                DBResource dbResource;

                if (databaseItems == null)
                {
                    // db and resources must be loaded
                    dbTopic = _LoadDatabaseTopic(topic);
                    dbResource = _LoadDatabaseResource(topic, Program.ApplicationSettings.GetCurrentCulture());
                    
                    _Data[topic] = new TduFile[] { dbTopic, dbResource};
                }
                else
                {
                    // they have already been loaded
                    dbTopic = databaseItems[0] as DB;
                    dbResource = databaseItems[1] as DBResource;
                }

                // Routine checks
                if (dbTopic == null)
                    // Current topic has not been loaded successfully > error
                    throw new Exception("Unable to load topic: " + topic);

                if (dbResource == null)
                    // Non serious issue
                    Log.Warning("Unable to load resource for topic: " + topic + ". Advanced features can't be used.");

                // Stores current line
                int currentLine = -1;
                DataGridView currentGridView = _GetCurrentGridView();

                if (currentGridView != null && currentGridView.SelectedCells.Count > 0)
                    currentLine = currentGridView.SelectedCells[0].RowIndex;

                // Creates GridView
                DataGridView topicGridView = _InitializeTopicGridView(dbTopic, dbResource);

                topicTabPage.Controls.Clear();
                topicTabPage.Controls.Add(topicGridView);

                // Displays right page
                topicTabControl.SelectedTab = topicTabPage;

                // Focus!
                topicGridView.Focus();

                // Current line restored
                if (currentLine != -1)
                    topicGridView.FirstDisplayedScrollingRowIndex = currentLine;
            }
        }

        /// <summary>
        /// Loads database resource for provided topic and culture
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        private DBResource _LoadDatabaseResource(DB.Topic topic, DB.Culture culture)
        {
            DBResource loadedResource = null;

            // BNK file
            if (_ResourceBnk == null)
            {
                string bnkFileName = string.Concat(_DatabaseFolder, DB.GetBNKFileName(culture));

                _ResourceBnk = TduFile.GetFile(bnkFileName) as BNK;
            }

            if (_ResourceBnk != null && _ResourceBnk.Exists)
            {
                string tempFolder = File2.SetTemporaryFolder(null, LibraryConstants.FOLDER_TEMP, true);
                string packedFileName = DB.GetFileName(culture, topic);
                string packedFilePath = _ResourceBnk.GetPackedFilesPaths(packedFileName)[0];

                _ResourceBnk.ExtractPackedFile(packedFilePath, tempFolder, true);

                // Loading extracted file...
                loadedResource = TduFile.GetFile(tempFolder + @"\" + packedFileName) as DBResource;
            }

            return loadedResource;
        }

        /// <summary>
        /// Loads database for provided topic
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        private DB _LoadDatabaseTopic(DB.Topic topic)
        {
            const DB.Culture culture = DB.Culture.Global;
            DB loadedData = null;

            // BNK file
            if (_DatabaseBnk == null)
            {
                string bnkFileName = string.Concat(_DatabaseFolder, DB.GetBNKFileName(culture));

                _DatabaseBnk = TduFile.GetFile(bnkFileName) as BNK;
            }

            if (_DatabaseBnk != null && _DatabaseBnk.Exists)
            {
                string tempFolder = File2.SetTemporaryFolder(null, LibraryConstants.FOLDER_TEMP, true);
                string packedFileName = DB.GetFileName(culture, topic);
                string packedFilePath = _DatabaseBnk.GetPackedFilesPaths(packedFileName)[0];

                _DatabaseBnk.ExtractPackedFile(packedFilePath, tempFolder, true);

                // Loading extracted file...
                loadedData = TduFile.GetFile(tempFolder + @"\" + packedFileName) as DB;
            }

            return loadedData;
        }
        
        /// <summary>
        /// Initializes a ListView control to display specified data
        /// </summary>
        /// <param name="dbTopic"></param>
        /// <param name="dbResource"></param>
        /// <returns></returns>
        private DataGridView _InitializeTopicGridView(DB dbTopic, DBResource dbResource)
        {
            DataGridView returnedGridView = new DataGridView
                                                {
                                                    BackgroundColor = SystemColors.AppWorkspace,
                                                    BorderStyle = BorderStyle.None,
                                                    GridColor = SystemColors.Control,
                                                    Dock = DockStyle.Fill,
                                                    AllowUserToAddRows = false,
                                                    AllowUserToDeleteRows = false,
                                                    AllowUserToOrderColumns = false
                                                };
            // Events
            returnedGridView.CellContentClick += topicGridView_CellContentClick;
            returnedGridView.KeyDown += topicGridView_KeyDown;
            returnedGridView.KeyUp += topicGridView_KeyUp;

            if (dbTopic != null)
            {   
                // Column headers
                DataGridViewColumn idHeader = new DataGridViewColumn
                                                  {
                                                      HeaderText = _ID_COLUMN_HEADER,
                                                      Width = 50,
                                                      CellTemplate = new DataGridViewTextBoxCell(),
                                                      AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                                                      ReadOnly = true                             
                                                  };

                returnedGridView.Columns.Add(idHeader);

                foreach(DB.Cell anotherColumn in dbTopic.Structure)
                {
                    DataGridViewColumn newHeader = new DataGridViewColumn
                                                       {
                                                           HeaderText = anotherColumn.name,
                                                           Width = 100,
                                                           CellTemplate = new DataGridViewTextBoxCell(),
                                                           AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                                                           ReadOnly = true
                                                       };

                    returnedGridView.Columns.Add(newHeader);
                }

                // Values
                foreach (DB.Entry anotherEntry in dbTopic.Entries)
                {
                    DataGridViewRow newRow = new DataGridViewRow();
                    DataGridViewCell idCell = new DataGridViewTextBoxCell {Value = anotherEntry.index, Style = _STYLE_CELL_ID};

                    newRow.Cells.Add(idCell);

                    foreach (DB.Cell anotherCell in anotherEntry.cells)
                    {
                        // According to data type...
                        DataGridViewCellStyle newStyle = _STYLE_CELL_STD;
                        string cellValue = anotherCell.value;
                        string toolTip = "";
                        string tag = null;

                        switch(anotherCell.valueType)
                        {
                            // Primary Key
                            case DB.ValueType.PrimaryKey:
                                cellValue = anotherCell.value;
                                newStyle = _STYLE_CELL_PK;
                                break;
                            case DB.ValueType.Reference:
                                cellValue = _GetReferenceValue(anotherCell, out tag);

                                // Errors handling
                                if (_DEFAULT_REFERENCE_VALUE.Equals(cellValue))
                                    newStyle = _STYLE_CELL_LINK_ERROR;
                                else
                                    newStyle = _STYLE_CELL_LINK;

                                toolTip = _TOOLTIP_LINK;
                                break;
                            case DB.ValueType.ReferenceL:
                                cellValue = _GetResourceValue(anotherCell, null, out tag);

                                // Errors handling
                                if (_DEFAULT_RESOURCE_VALUE.Equals(cellValue))
                                    newStyle = _STYLE_CELL_RESOURCE_ERROR;
                                else
                                    newStyle = _STYLE_CELL_RESOURCE_FAR;

                                toolTip = _TOOLTIP_RESOURCE_FAR;
                                break;
                            case DB.ValueType.ValueInResourceH:
                                cellValue = _GetResourceValue(anotherCell, dbResource, out tag);

                                // Errors handling
                                if (_DEFAULT_RESOURCE_VALUE.Equals(cellValue))
                                    newStyle = _STYLE_CELL_RESOURCE_ERROR;
                                else
                                    newStyle = _STYLE_CELL_RESOURCE;

                                toolTip = _TOOLTIP_RESOURCE_2;
                                break;
                            case DB.ValueType.ValueInResource:
                                cellValue = _GetResourceValue(anotherCell, dbResource, out tag);
                                newStyle = _STYLE_CELL_RESOURCE;
                                toolTip = _TOOLTIP_RESOURCE;
                                break;
                            case DB.ValueType.BitField:
                                newStyle = _STYLE_CELL_BITFIELD;
                                break;
                            default:
                                break;
                        }

                        DataGridViewCell newDgCell = new DataGridViewTextBoxCell
                            {
                                Value = cellValue,
                                Style = newStyle,
                                ToolTipText = toolTip,
                                Tag = tag
                            };
                       
                        newRow.Cells.Add(newDgCell);
                    }

                    returnedGridView.Rows.Add(newRow);
                }
            }

            return returnedGridView;
        }

        /// <summary>
        /// Returns formatted cell value for specified reference
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="tag">returned tag information</param>
        /// <returns></returns>
        private string _GetReferenceValue(DB.Cell cell, out string tag)
        {
            string returnedValue = _DEFAULT_REFERENCE_VALUE;
            string returnedTag = null;

            if (cell.optionalRef == null)
            {
                // Report missing optional reference
                Log.Warning("Optional reference is missing in cell: " + cell.name + ", entry id=" + cell.entryIndex);
            }
            else
            {
                string topicId = cell.optionalRef;
                DB.Topic referencedTopic = DB.TopicPerTopicId[topicId];

                if (referencedTopic == DB.Topic.None)
                {
                    // Report unknown topic
                    Log.Warning("Unable to get referenced topic with id=" + topicId);
                }
                else
                {
                    // Is topic to be loaded ?
                    TduFile[] databaseItems = _Data[referencedTopic];
                    DB dbTopic;

                    if (databaseItems == null)
                    {
                        // db and resources must be loaded
                        dbTopic = _LoadDatabaseTopic(referencedTopic);

                        DBResource dbResource = _LoadDatabaseResource(referencedTopic, Program.ApplicationSettings.GetCurrentCulture());

                        _Data[referencedTopic] = new TduFile[] { dbTopic, dbResource };
                    }
                    else
                    {
                        // they have already been loaded
                        dbTopic = databaseItems[0] as DB;
                    }

                    // Get referenced value
                    List<DB.Entry> referencedEntries = DatabaseHelper.SelectAllCellsFromTopicWherePrimaryKey(dbTopic, cell.value);

                    if (referencedEntries.Count == 0)
                    {
                        // Report missing reference
                        Log.Warning("Unable to get reference with id=" + cell.value + " from topic " + referencedTopic);
                    }
                    else
                    {
                        // Not used for now
                        //DB.Entry refrencedEntry = referencedEntries[0];
                        returnedTag = string.Format(_FORMAT_TAG_LINK, referencedTopic, cell.value);
                        returnedValue = string.Format(_FORMAT_REFERENCE_CELL, referencedTopic, cell.value);
                    }
                }
            }

            tag = returnedTag;
            return returnedValue;
        }

        /// <summary>
        /// Returns formatted cell value for specified resource
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="dbResource"></param>
        /// <param name="tag">returned tag information</param>
        /// <returns></returns>
        private string _GetResourceValue(DB.Cell cell, DBResource dbResource, out string tag)
        {
            string returnedValue = _DEFAULT_RESOURCE_VALUE;
            string returnedTag = null;

            if (dbResource == null)
            {
                // Resource from another topic
                if (cell.optionalRef != null)
                {
                    DB.Topic referencedTopic = DB.TopicPerTopicId[cell.optionalRef];

                    // Is topic to be loaded ?
                    TduFile[] databaseItems = _Data[referencedTopic];

                    if (databaseItems == null)
                    {
                        // db and resources must be loaded
                        DB dbTopic = _LoadDatabaseTopic(referencedTopic);
                        dbResource = _LoadDatabaseResource(referencedTopic, Program.ApplicationSettings.GetCurrentCulture());

                        _Data[referencedTopic] = new TduFile[] { dbTopic, dbResource };
                    }
                    else
                    {
                        // they have already been loaded
                        dbResource = databaseItems[1] as DBResource;
                    }
                }
            }

            if (dbResource == null)
            {
                // Report missing resource
                Log.Warning("Unable to get resource with id=" + cell.value + " from topic id=" + cell.optionalRef);
            }
            else
            {
                DBResource.Entry currentEntry = dbResource.GetEntryFromId(cell.value);

                if (currentEntry.isValid)
                {
                    string translatedValue = currentEntry.value;

                    returnedValue = string.Format(_FORMAT_RESOURCE_CELL, translatedValue, cell.value);
                }
                else
                {
                    // Report missing resource
                    Log.Warning("Unable to get resource with id=" + cell.value + " from " + dbResource.CurrentTopic + " - " + dbResource.CurrentCulture);
                }

                returnedTag = string.Format(_FORMAT_TAG_LINK, dbResource.CurrentTopic, cell.value);
            }

            tag = returnedTag;
            return returnedValue;
        }

        /// <summary>
        /// Returns currently displayed topic
        /// </summary>
        /// <returns></returns>
        private DB.Topic _GetCurrentTopic()
        {
            DB.Topic returnedTopic = DB.Topic.None;

            if (topicTabControl.SelectedTab != null)
            {
                string currentPage = topicTabControl.SelectedTab.Text;

                returnedTopic = (DB.Topic) Enum.Parse(typeof (DB.Topic), currentPage, false);
            }

            return returnedTopic;
        }

        /// <summary>
        /// Displays selected topic and reference
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="reference"></param>
        private void _NavigateTo(DB.Topic topic, string reference)
        {
            if (topic != DB.Topic.None)
            {
                // Linked tab
                TabPage tp = _GetTabPage(topic);

                if (tp != null)
                {
                    topicTabControl.SelectedTab = tp;

                    // If reference is specified, searches corresponding line
                    if (!string.IsNullOrEmpty(reference))
                    {
                        DataGridView currentGridView = _GetCurrentGridView();
                        bool isFound = false;
                        
                        foreach(DataGridViewRow anotherRow in currentGridView.Rows)
                        {
                            // REF is the 2nd column
                            if (anotherRow.Cells.Count >= 2)
                            {
                                DataGridViewCell currentCell = anotherRow.Cells[1];

                                if (reference.Equals(currentCell.Value))
                                {
                                    currentGridView.ClearSelection();
                                    // Selecting whole line
                                    anotherRow.Selected = true;
                                    // Displaying this line
                                    currentGridView.FirstDisplayedScrollingRowIndex = anotherRow.Index;

                                    isFound = true;
                                    break;
                                }
                            }
                        }

                        if (!isFound)
                            throw new Exception(_ERROR_REF_NOT_FOUND);
                    }
                }
            }
        }

        /// <summary>
        /// Return tab page corrsponding to specified topic
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        private TabPage _GetTabPage(DB.Topic topic)
        {
            TabPage returnedPage = null;

            foreach(TabPage anotherPage in topicTabControl.TabPages)
            {
                if (topic.ToString().Equals(anotherPage.Text))
                {
                    returnedPage = anotherPage;
                    break;
                }
            }

            return returnedPage;
        }

        /// <summary>
        /// Displays dialog box to modify a resource
        /// </summary>
        /// <param name="entryId"></param>
        /// <param name="columnId"></param>
        /// <param name="sourceTopic"></param>
        /// <param name="linkedTopic"></param>
        /// <param name="linkedResId"></param>
        private void _ModifyResource(int entryId, int columnId, DB.Topic sourceTopic, DB.Topic linkedTopic, string linkedResId)
        {
            // Gets information from loaded data
            try
            {
                Cursor = Cursors.WaitCursor;

                TduFile[] loadedData = _Data[sourceTopic];

                // Linked resource can be from current topic or another one
                TduFile[] linkedData = _Data[linkedTopic];
                DBResource linkedResource = linkedData[1] as DBResource;
                DB dbTopic = loadedData[0] as DB;

                if (dbTopic != null && linkedResource != null)
                {
                    MiniResourceEditorDialog dialog = new MiniResourceEditorDialog(dbTopic, entryId, columnId,
                                                                                   linkedResource, linkedResId);
                    DialogResult dr = dialog.ShowDialog(this);

                    if (dr == DialogResult.OK)
                    {
                        // Saving changes
                        dbTopic.Save();
                        linkedResource.Save();

                        // Re-packing
                        string packedName = new FileInfo(dbTopic.FileName).Name;
                        string packedPath = _DatabaseBnk.GetPackedFilesPaths(packedName)[0];

                        _DatabaseBnk.ReplacePackedFile(packedPath, dbTopic.FileName);

                        packedName = new FileInfo(linkedResource.FileName).Name;
                        packedPath = _ResourceBnk.GetPackedFilesPaths(packedName)[0];

                        _ResourceBnk.ReplacePackedFile(packedPath, linkedResource.FileName);

                        // Refresh current table
                        _RefreshTableSection(_GetCurrentTopic());

                        StatusBarLogManager.ShowEvent(this, _STATUS_EDIT_RES_OK);
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

        /// <summary>
        /// Returns selected grid view
        /// </summary>
        /// <returns></returns>
        private DataGridView _GetCurrentGridView()
        {
            DataGridView returnedGridView = null;

            if (topicTabControl.SelectedTab != null)
            {
                Control.ControlCollection controls = topicTabControl.SelectedTab.Controls;

                if (controls.Count > 0)
                    returnedGridView = controls[0] as DataGridView;
            }

            return returnedGridView;
        }

        /// <summary>
        /// Returns table information to display
        /// </summary>
        /// <param name="currentTopic"></param>
        /// <returns></returns>
        private string _GetTableInformation(DB.Topic currentTopic)
        {
            string returnedInfo = "";

            if (_Data.ContainsKey(currentTopic))
            {
                // Gets loaded data
                DB topicTable = _Data[currentTopic][0] as DB;

                if (topicTable != null)
                {
                    int entryCount = topicTable.EntryCount;
                    int columnCount = topicTable.ColumnCount;

                    returnedInfo = string.Format(_FORMAT_TABLE_INFORMATION, currentTopic, entryCount, columnCount);
                }
            }

            return returnedInfo;
        }

        /// <summary>
        /// Returns true is specified cell has been marked
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static bool _IsCellMarked(DataGridViewCell cell)
        {
            bool isMarked = false;

            if (cell != null)
                isMarked = (cell.Style.Equals(_STYLE_CELL_MARKED) || cell.Style.Equals(_STYLE_CELL_LINK_MARKED) ||
                            cell.Style.Equals(_STYLE_CELL_RESOURCE_MARKED) || cell.Style.Equals(_STYLE_CELL_RESOURCE_FAR_MARKED));

            return isMarked;
        }

        /// <summary>
        /// Prepares database loading from specified folder, if any. Otherwise, current database is used
        /// </summary>
        private void _PrepareDatabase()
        {
            string databaseFolder = _GetDatabaseFolder();

            // Validation
            if (Directory.Exists(databaseFolder))
            {
                _DatabaseFolder = databaseFolder;
                _DatabaseBnk = null;
                _ResourceBnk = null;
                _Data.Clear();
                topicTabControl.TabPages.Clear();

                // Creates existing topics
                for (int i = 1; i <= 18; i++)
                {
                    DB.Topic currentTopic = (DB.Topic)i;

                    // DATA
                    _Data.Add(currentTopic, null);

                    // GUI
                    topicTabControl.TabPages.Add(currentTopic.ToString(), currentTopic.ToString());
                }

                // Refreshes first tab
                _RefreshTableSection(DB.Topic.Achievements);

                // Table information label
                topicInfoLabel.Text = _GetTableInformation(DB.Topic.Achievements);
            }
            else
                throw new Exception("Invalid database folder!");
        }

        /// <summary>
        /// Returns database folder according to loading mode
        /// </summary>
        private string _GetDatabaseFolder()
        {
            string returnedFolder;

            // According to chosen mode...
            if (dbSourceCurrentRadioButton.Checked)
                returnedFolder = string.Concat(LibraryConstants.GetSpecialFolder(LibraryConstants.TduSpecialFolder.Database));
            else
                returnedFolder = inputFolderPath.Text + @"\";

            return returnedFolder;
        }
        #endregion

        #region Events
        private void settingsButton_Click(object sender, EventArgs e)
        {
            // Click on 'Settings' button > open Options dialog
            SettingsForm settingsForm = new SettingsForm(SettingsForm.SettingsTabPage.Main);

            settingsForm.ShowDialog(this);
        }

        private void dbSourceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Click on mode 1 : Current
            _SetSelectionControls();
        }

        private void openRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Click on mode 2 : From folder
            _SetSelectionControls();
            inputFolderPath.Focus();
        }

        private void DbEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Closure request
            _Instance = null;
            _SearchBoxInstance = null;

            // Cleaning status log
            StatusBarLogManager.RemoveLog(this);

            e.Cancel = false;
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            // Click on Load button
            Cursor = Cursors.WaitCursor;

            try
            {
                _PrepareDatabase();

                StatusBarLogManager.ShowEvent(this, _STATUS_LOADING_OK);

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

        private void topicTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Another tab page has been selected
            if (topicTabControl.SelectedTab != null)
            {
                Cursor = Cursors.WaitCursor;

                try
                {
                    DB.Topic currentTopic = _GetCurrentTopic();

                    // Optimization : rebuilds table only if not already loaded
                    if (topicTabControl.SelectedTab.Controls.Count == 0)
                        _RefreshTableSection(currentTopic);

                    // Table information label
                    topicInfoLabel.Text = _GetTableInformation(currentTopic);
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

        private void browseDBButton_Click(object sender, EventArgs e)
        {
            // Click on browse folder button
            folderBrowserDialog.Description = _LABEL_BROWSE_FOLDER;

            DialogResult dr = folderBrowserDialog.ShowDialog(this);

            if (dr == DialogResult.OK)
                inputFolderPath.Text = folderBrowserDialog.SelectedPath;
        }

        private void selectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Mark > Selected'
            DataGridView currentGridView = _GetCurrentGridView();

            if (currentGridView != null)
            {
                try
                {
                    foreach (DataGridViewCell selectedCell in currentGridView.SelectedCells)
                    {
                        if (!_IsCellMarked(selectedCell))
                        {
                            if (_STYLE_CELL_RESOURCE.Equals(selectedCell.Style))
                                selectedCell.Style = _STYLE_CELL_RESOURCE_MARKED;
                            else if(_STYLE_CELL_RESOURCE.Equals(selectedCell.Style))
                                selectedCell.Style = _STYLE_CELL_RESOURCE_FAR_MARKED;
                            else if(_STYLE_CELL_LINK.Equals(selectedCell.Style))
                                selectedCell.Style = _STYLE_CELL_LINK_MARKED;
                            else
                                selectedCell.Style = _STYLE_CELL_MARKED;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Edit > Copy > Selected'. Copies selected values to clipboard
            DataGridView currentGridView = _GetCurrentGridView();

            if (currentGridView != null)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    StringBuilder sb = new StringBuilder();

                    foreach (DataGridViewCell selectedCell in currentGridView.SelectedCells)
                    {
                        sb.Append(selectedCell.Value);
                        sb.Append(Tools.SYMBOL_VALUE_SEPARATOR);
                    }

                    Clipboard.SetText(sb.ToString());

                    StatusBarLogManager.ShowEvent(this, _STATUS_COPY_OK);
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

        private void clearAllMarksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Mark > Remove all marks'
            DataGridView currentGridView = _GetCurrentGridView();

            if (currentGridView != null)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    _RefreshTableSection(_GetCurrentTopic());
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

        private void navigateToReferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click to Edit > Navigate to reference
            DataGridView currentGridView = _GetCurrentGridView();

            if (currentGridView != null && currentGridView.SelectedCells.Count == 1)
            {
                // Must be ref to another table
                DataGridViewCell currentCell = currentGridView.SelectedCells[0];

                if (currentCell.Style.Equals(_STYLE_CELL_LINK) 
                    || currentCell.Style.Equals(_STYLE_CELL_LINK_ERROR) 
                    || currentCell.Style.Equals(_STYLE_CELL_LINK_MARKED))
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;

                        DB.Topic linkedTopic;
                        string linkedRef;

                        // Getting information in tag
                        if (currentCell.Tag == null)
                            throw new Exception(_ERROR_REF_NOT_FOUND);
                       
                        string[] tagInfo = ((string) currentCell.Tag).Split(Tools.SYMBOL_VALUE_SEPARATOR);

                        if (tagInfo.Length == 2)
                        {
                            linkedTopic = (DB.Topic) Enum.Parse(typeof (DB.Topic), tagInfo[0]);
                            linkedRef = tagInfo[1];
                        }
                        else
                            throw new Exception(_ERROR_REF_NOT_FOUND);
    
                        // Go!
                        _NavigateTo(linkedTopic, linkedRef);
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
                else
                    StatusBarLogManager.ShowEvent(this, _STATUS_NOT_REF_CELL);
            }
        }

        private void topicGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ALT+Click on a cell value : Link management
            if (_IsAltKeyPressed)
                navigateToReferenceToolStripMenuItem_Click(sender, new EventArgs());
        }

        private void topicGridView_KeyUp(object sender, KeyEventArgs e)
        {
            // Key has been dropped
            _IsAltKeyPressed = false;

            ((DataGridView) sender).Cursor = Cursors.Default;
        }

        private void topicGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // Key has been pressed
            if (e.Alt)
            {
                _IsAltKeyPressed = true;

                ((DataGridView) sender).Cursor = Cursors.Hand;
            }
        }

        private void editResourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click to Edit > Modify resource
            DataGridView currentGridView = _GetCurrentGridView();

            if (currentGridView != null && currentGridView.SelectedCells.Count == 1)
            {
                // Must be ref to resource
                DataGridViewCell currentCell = currentGridView.SelectedCells[0];

                if (currentCell.Style.Equals(_STYLE_CELL_RESOURCE) 
                    || currentCell.Style.Equals(_STYLE_CELL_RESOURCE_FAR)
                    || currentCell.Style.Equals(_STYLE_CELL_RESOURCE_ERROR))
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;

                        // Getting information in tag
                        if (currentCell.Tag == null)
                            throw new Exception(_ERROR_RES_NOT_FOUND);

                        string[] tagInfo = ((string)currentCell.Tag).Split(Tools.SYMBOL_VALUE_SEPARATOR);

                        if (tagInfo.Length == 2)
                        {
                            DB.Topic linkedTopic = (DB.Topic)Enum.Parse(typeof(DB.Topic), tagInfo[0]);
                            string linkedRes = tagInfo[1];

                            // Displays edit box
                            int entryId = currentCell.RowIndex;
                            int columnId = currentCell.ColumnIndex - 1;

                            _ModifyResource(entryId, columnId, _GetCurrentTopic(), linkedTopic, linkedRes);
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
                else
                    StatusBarLogManager.ShowEvent(this, _STATUS_NOT_RES_CELL);
            }

        }

        private void searchToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on Search button
            DataGridView currentGridView = _GetCurrentGridView();

            if (currentGridView != null)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    SearchBox searchBox = _GetSearchBoxInstance(_GetCurrentGridView(), _LABEL_SEARCH);

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
        }

        private void checkDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Check database' menu item
            string databaseFolder = _GetDatabaseFolder();

            if (!string.IsNullOrEmpty(databaseFolder))
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    DbCheckDialog dlg = new DbCheckDialog(databaseFolder);
                    DialogResult dr = dlg.ShowDialog(this);

                    if (dr == DialogResult.OK)
                        // Changes have been made to database > reloading
                        loadButton_Click(sender, e);
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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Support.Traces.Appenders;
using DjeFramework1.Common.Types;
using DjeFramework1.Common.Types.Forms;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.binaries;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.fileformats.specific;
using TDUModdingLibrary.support.constants;
using TDUModdingLibrary.support.patcher;
using TDUModdingLibrary.support.patcher.instructions;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingTools.common;
using TDUModdingTools.gui.settings;
using TDUModdingTools.gui.wizards.patcheditor.parametervalue;
using DjeFramework1.Common.Support.Traces;

namespace TDUModdingTools.gui.wizards.patcheditor
{
    /// <summary>
    /// EVO_24: wizard to write patchs
    /// </summary>
    public partial class PatchEditorForm : Form
    {
        #region Enums
        /// <summary>
        /// Tab pages for patch editor
        /// </summary>
        private enum EditorTabPages
        {
            Properties = 0, 
            Report = 1,
            Debug = 2
        }
        #endregion

        #region Constants
        /// <summary>
        /// Suffix used for automatic uninstaller patch generation
        /// </summary>
        private const string _SUFFIX_UNINSTALL = "-uninstall";

        /// <summary>
        /// Error message when saving has failed
        /// </summary>
        private const string _ERROR_SAVING_PATCH = "An error occurred while saving patch file.";

        /// <summary>
        /// Error when trying to deploy into an invalid path
        /// </summary>
        private const string _ERROR_DEPLOY_LOCATION = "Specified deploy location is invalid.";

        /// <summary>
        /// Error when trying to test without at least one deployment
        /// </summary>
        private const string _ERROR_PATCH_NOT_DEPLOYED = "Please *generate* this patch before testing it.";

        /// <summary>
        /// Error when trying to test while another test is in progress
        /// </summary>
        private const string _ERROR_TEST_IN_PROGRESS = "You're already testing current patch. Please close ModAndPlay! before.";

        /// <summary>
        /// Error message when testing has failed
        /// </summary>
        private const string _ERROR_EXECUTE_PATCH = "Unable to test current patch.";

        /// <summary>
        /// Question when losing modified data
        /// </summary>
        private const string _QUESTION_LOST_CHANGES = "Some changes remain unsaved! Are you sure?";

        /// <summary>
        /// Status message after a succesful loading
        /// </summary>
        private const string _STATUS_LOAD_SUCCESS = "Patch succesfully loaded.";

        /// <summary>
        /// Status message after a succesful saving
        /// </summary>
        private const string _STATUS_SAVE_SUCCESS = "Patch succesfully saved.";

        /// <summary>
        /// Status message after a succesful instruction saving
        /// </summary>
        private const string _STATUS_SAVE_INSTR_SUCCESS = "Instruction succesfully saved.";

        /// <summary>
        /// Status message when beginning a new patch
        /// </summary>
        private const string _STATUS_READY = "Ready.";

        /// <summary>
        /// Status message when having converted uninstall patch from install one
        /// </summary>
        private const string _STATUS_UNINSTALLER_READY = "Uninstall patch was created succesfully. Now ready.";

        /// <summary>
        /// Status message when patch is running
        /// </summary>
        private const string _STATUS_PATCH_RUNNING = "Patch testing in progress...";
       
        /// <summary>
        /// Status message when patch has finished running
        /// </summary>
        private const string _STATUS_PATCH_RUNNING_DONE = "Testing finished. See report and debug pages for details...";

        /// <summary>
        /// Status message when patch has finished running upon errors
        /// </summary>
        private const string _STATUS_PATCH_ERROR = "Testing failure!";
        
        /// <summary>
        /// Status message when deploy halted because of an error
        /// </summary>
        private const string _STATUS_DEPLOY_FAILED = "Patch couldn't be deployed.";
        
        /// <summary>
        /// Status message when deploy succeeded
        /// </summary>
        private const string _STATUS_DEPLOY_SUCCESS = "Patch succesfully deployed to {0}. Don't forget to copy your own files to this location!";

        /// <summary>
        /// Status message when import succeeded
        /// </summary>
        private const string _STATUS_IMPORT_SUCCESS = "Instruction(s) succesfully imported.";

        /// <summary>
        /// Status message when import succeeded
        /// </summary>
        private const string _STATUS_EXPORT_SUCCESS = "Instruction(s) succesfully exported.";

        /// <summary>
        /// Yes
        /// </summary>
        private const string _WORD_YES = "Yes";

        /// <summary>
        /// No
        /// </summary>
        private const string _WORD_NO = "No";

        /// <summary>
        /// Messsage displayed when entering parameter value
        /// </summary>
        private const string _MESSAGE_ENTER_PARAM_VALUE = "Enter value for '{0}' parameter.";

        /// <summary>
        /// Message displayed when browsing for a deploy location
        /// </summary>
        private const string _MESSAGE_BROWSE_DEPLOY = "Please select location where current patch will be deployed.";

        /// <summary>
        /// Open file dialog title when creating uninstaller patch
        /// </summary>
        private const string _TITLE_OPEN_INSTALLER_PATCH = "Please select an installer patch to start from...";

        /// <summary>
        /// Open file dialog title when importing instruction
        /// </summary>
        private const string _TITLE_OPEN_INSTRUCTIONS = "Please select exported file containing instructions...";

        /// <summary>
        /// Open file dialog title when exporting instruction
        /// </summary>
        private const string _TITLE_SAVE_XML = "Please give a name to exported file...";

        /// <summary>
        /// Label for instruction chooser dialog box
        /// </summary>
        private const string _LABEL_INSTRUCTION_CHOOSER = "Please choose instruction(s) to import...";
        #endregion

        #region Properties
        /// <summary>
        /// Patch currently opened in Patch Editor
        /// </summary>
        public PCH CurrentPatch
        {
            get { return _CurrentPatch; }
        }
        private PCH _CurrentPatch;

        /// <summary>
        /// Singleton access to help dialog
        /// </summary>
        private InstructionHelpDialog _HelpDialog;
        #endregion

        #region Members
        /// <summary>
        /// Instance of this form
        /// </summary>
        private static PatchEditorForm _Instance;

        /// <summary>
        /// Changes indicator for current patch
        /// </summary>
        private bool _IsModified;

        /// <summary>
        /// Patch instruction currently edited
        /// </summary>
        private PatchInstruction _CurrentInstruction;

        /// <summary>
        /// Folder where patch was most recently deployed
        /// </summary>
        private string _LastDeployLocation;

        /// <summary>
        /// Current instance of TDU Mod And Play ! app
        /// </summary>
        private Process _CurrentModAndPlayInstance;
        #endregion

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="fileName">Patch file name to edit</param>
        private PatchEditorForm(string fileName)
        {
            InitializeComponent();

            // Instruction panel: instruction list
            string[] allNames = Enum.GetNames(typeof (PatchInstruction.InstructionName));

            foreach (string anotherName in allNames)
                typesComboBox.Items.Add(anotherName);

            // Loading
            if (string.IsNullOrEmpty(fileName))
            {
                // Empty workspace
                _InitializeContents();
                _ManageActionToolstripState(false);     
            }     
            else
                _LoadPatch(fileName);


            _ManagePanelState(false);

            // Modifying flag
            _SetInstructionModified(false);

            // EVO_32
            StatusBarLogManager.AddNewLog(this, mainStatusLabel);
        }

        #region Private methods
        /// <summary>
        /// Updates GUI contents according to loaded PCH file
        /// </summary>
        private void _InitializeContents()
        {
            // BUG_72 : Help box
            _HelpDialog = new InstructionHelpDialog(this);

            // Changes indicator
            _IsModified = false;

            // Current instruction
            _CurrentInstruction = null;

            // Patch file name label
            _UpdateFileNameLabel();

            // Deploy location
            _LastDeployLocation = null;
        }

        /// <summary>
        /// Defines the contents of instruction panel corresponding to current instruction
        /// </summary>
        private void _InitializeInstructionContents()
        {
            if (_CurrentInstruction != null)
            {
                PatchInstruction newInstruction = PatchInstruction.MakeInstruction(typesComboBox.Text);

                if (newInstruction != null)
                {
                    // Controls enabled
                    _ManagePanelState(true);

                    // Properties
                    PatchInstruction.CopyProperties(_CurrentInstruction, newInstruction, false);
                    _CurrentInstruction = newInstruction;
                    _InitializeParameters();

                    // Checkboxes
                    enabledCheckbox.Checked = _CurrentInstruction.Enabled;
                    failOnErrorCheckbox.Checked = _CurrentInstruction.FailOnError;

                    // Group
                    installGroupComboBox.Text = _CurrentInstruction.Group.name;

                    // Comment
                    commentTextBox.Text = _CurrentInstruction.Comment;

                    // Modifier flag
                    _SetInstructionModified(true);
                }
            }
        }

        /// <summary>
        /// Defines parameter list compatible with current instruction
        /// </summary>
        private void _InitializeParameters()
        {
            ListViewItem newItem;
            PatchInstructionParameter paramInstance;
            bool keepSelection = false;

            // Keeping previous selection, if any
            if (parametersListView.SelectedIndices.Count == 1)
            {
                ListView2.StoreSelectedIndex(parametersListView);
                keepSelection = true;
            }

            parametersListView.Items.Clear();
            foreach (string paramName in _CurrentInstruction.SupportedParameters)
            {
                paramInstance = PatchInstructionParameter.MakeParameter(paramName);

                if (paramInstance != null)
                {
                    newItem = new ListViewItem(paramInstance.Name);

                    // Retrieving parameter value
                    string paramValue = "";

                    if (_CurrentInstruction.Parameters.ContainsKey(paramName))
                    {
                        paramValue = _CurrentInstruction.Parameters[paramName].Value;
                    }
                    newItem.SubItems.Add(paramValue);

                    newItem.SubItems.Add(paramInstance.Description);
                    parametersListView.Items.Add(newItem);
                }
            }

            // Restoring previous selection
            if (keepSelection)
                ListView2.RestoreSelectedIndex(parametersListView);
        }

        /// <summary>
        /// Sets the patch file name label to reflect actual file name
        /// </summary>
        private void _UpdateFileNameLabel()
        {
            if (_CurrentPatch != null)
                patchNameLabel.Text = _CurrentPatch.FileName;
        }

        /// <summary>
        /// Loads specified patch file
        /// </summary>
        /// <param name="fileName">Name of patch file to load</param>
        private void _LoadPatch(string fileName)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                _CurrentPatch = TduFile.GetFile(fileName) as PCH;
                _InitializeContents();
                _UpdateInstructionList();
                _ManageActionToolstripState(true);

                // Install group list
                if (_CurrentPatch != null)
                {
                    installGroupComboBox.Items.Clear();

                    foreach (PCH.InstallGroup anotherGroup in _CurrentPatch.Groups)
                        installGroupComboBox.Items.Add(anotherGroup.name);
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
        /// Fills the instruction list
        /// </summary>
        private void _UpdateInstructionList()
        {
            if (_CurrentPatch != null)
            {
                ListViewItem newItem;
                bool keepSelection = false;

                // Keeping previous selection, if any
                if (instructionListView.SelectedIndices.Count == 1)
                {
                    ListView2.StoreSelectedIndex(instructionListView);
                    keepSelection = true;
                }

                // Clearing all lists
                instructionListView.Items.Clear();
                parametersListView.Items.Clear();

                foreach (PatchInstruction instruction in _CurrentPatch.PatchInstructions)
                {
                    newItem = new ListViewItem(instruction.Order.ToString()) {UseItemStyleForSubItems = false};

                    // Colors
                    if (!instruction.Enabled)
                        newItem.ForeColor = GuiConstants.COLOR_DISABLED_ITEM;

                    // Bold style on 1st column
                    newItem.Font = new Font(instructionListView.Font, FontStyle.Bold);

                    ListViewItem.ListViewSubItem subItem =
                        new ListViewItem.ListViewSubItem(newItem, instruction.Name, newItem.ForeColor, newItem.BackColor,
                                                         new Font(instructionListView.Font, FontStyle.Regular));
                    newItem.SubItems.Add(subItem);

                    subItem = new ListViewItem.ListViewSubItem(newItem, (instruction.FailOnError ? _WORD_YES : _WORD_NO), newItem.ForeColor, newItem.BackColor,
                                                         new Font(instructionListView.Font, FontStyle.Regular));
                    newItem.SubItems.Add(subItem);

                    subItem = new ListViewItem.ListViewSubItem(newItem, instruction.Group.name, newItem.ForeColor, newItem.BackColor,
                                                         new Font(instructionListView.Font, FontStyle.Regular));
                    newItem.SubItems.Add(subItem);

                    subItem = new ListViewItem.ListViewSubItem(newItem, instruction.Comment, newItem.ForeColor, newItem.BackColor,
                                                         new Font(instructionListView.Font, FontStyle.Regular));
                    newItem.SubItems.Add(subItem);

                    instructionListView.Items.Add(newItem);
                }

                // Restoring previous selection
                if (keepSelection)
                    ListView2.RestoreSelectedIndex(instructionListView);
            }
        }

        /// <summary>
        /// Defines the patch state
        /// </summary>
        /// <param name="isModified">true = modified, else false</param>
        private void _SetPatchModified(bool isModified)
        {
            // Patch name label
            if (isModified)
            {
                if (!_IsModified)
                    patchNameLabel.Text += GuiConstants.SYMBOL_MODIFIED_FILE;
            }
            else
            {
                if (patchNameLabel.Text.EndsWith(GuiConstants.SYMBOL_MODIFIED_FILE))
                    patchNameLabel.Text = patchNameLabel.Text.Substring(0, patchNameLabel.Text.Length - 1);

                _SetInstructionModified(false);
            }

            _IsModified = isModified;
        }

        /// <summary>
        /// Defines the current instruction state
        /// </summary>
        /// <param name="isModified">true = modified, else false</param>
        private void _SetInstructionModified(bool isModified)
        {
            // Save instruction icon
            saveArgsToolStripButton.Enabled = isModified;
        }

        /// <summary>
        /// Ensures user won't loose his changes before going out
        /// </summary>
        private bool _ConfirmErase()
        {
            if (_IsModified)
            {
                DialogResult dr = MessageBoxes.ShowQuestion(this, _QUESTION_LOST_CHANGES, MessageBoxButtons.YesNo);

                if (dr != DialogResult.Yes)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Defines the enabled state of the bottom panel
        /// </summary>
        /// <param name="isEnabled">true to enable controls, else false</param>
        private void _ManagePanelState(bool isEnabled)
        {
            // Instruction tab
            argsTabPage.Enabled = isEnabled;

            // When disabling, controls must be empty
            if (!isEnabled)
            {
                typesComboBox.Text = commentTextBox.Text = "";
                parametersListView.Items.Clear();
                enabledCheckbox.Checked = false;
                failOnErrorCheckbox.Checked = false;

                _SetInstructionModified(false);
            }
        }

        /// <summary>
        /// Defines the enabled state of the left-side toolstrip
        /// </summary>
        /// <param name="isEnabled">true to enable controls, else false</param>
        private void _ManageActionToolstripState(bool isEnabled)
        {
            foreach (ToolStripItem item in actionInstrToolStrip.Items)
                item.Enabled = isEnabled;
        }

        /// <summary>
        /// Adds a new instruction to the list (default: backupFile)
        /// </summary>
        private void _AddInstruction()
        {
            PatchInstruction newPi = new NothingI {Order = instructionListView.Items.Count + 1};

            _CurrentPatch.SetInstruction(newPi);
            _UpdateInstructionList();

            // Item selected
            instructionListView.SelectedIndices.Clear();
            instructionListView.SelectedIndices.Add(newPi.Order - 1);
            instructionListView.EnsureVisible(newPi.Order - 1);

            // Modifier flag
            _SetPatchModified(true);
        }

        /// <summary>
        /// Deletes instruction with specified order
        /// </summary>
        /// <param name="order">Order of instruction to remove</param>
        private void _DeleteInstruction(int order)
        {
            _CurrentPatch.DeleteInstructionAt(order + 1);
            _UpdateInstructionList();
            _CurrentInstruction = null;

            // Item selected
            instructionListView.SelectedIndices.Clear();

            if (order >= 0 && order < _CurrentPatch.PatchInstructions.Count)
            {
                instructionListView.SelectedIndices.Add(order);
                instructionListView.EnsureVisible(order);
            }
            else if ( order > 0 && order == _CurrentPatch.PatchInstructions.Count)
            {
                instructionListView.SelectedIndices.Add(order - 1);
                instructionListView.EnsureVisible(order - 1);
            }

            // Modifier flags
            _SetPatchModified(true);
            _SetInstructionModified(false);

            // If all instructions deleted, bottom panel back to default
            if (_CurrentPatch.PatchInstructions.Count == 0)
                _ManagePanelState(false);
        }

        /// <summary>
        /// Duplicates instruction with specified order then add it to the end of the list
        /// </summary>
        /// <param name="order">instruction order to duplicate, 0-based</param>
        private void _DuplicateInstruction(int order)
        {
            PatchInstruction pi = _CurrentPatch.GetInstruction(order + 1);
            PatchInstruction clonePi = pi.Clone() as PatchInstruction;

            if (clonePi != null)
            {
                clonePi.Order = instructionListView.Items.Count + 1;
                _CurrentPatch.SetInstruction(clonePi);
                _UpdateInstructionList();

                // Item selected
                instructionListView.SelectedIndices.Clear();
                instructionListView.SelectedIndices.Add(clonePi.Order - 1);
                instructionListView.EnsureVisible(clonePi.Order - 1);

                // Modifier flag
                _SetPatchModified(true);
            }
        }

        /// <summary>
        /// Swaps an instruction from a base-0 (listview) order with preceding or following instruction
        /// </summary>
        /// <param name="order">Instruction to move</param>
        /// <param name="withNext">true to swap with following instruction, false with preceding</param>
        private void _SwitchInstruction(int order, bool withNext)
        {
            // Controls
            int otherOrder;

            if (order < 0 || order >= _CurrentPatch.PatchInstructions.Count)
                return;

            if (withNext)
            {
                // Down
                if (order == _CurrentPatch.PatchInstructions.Count - 1)
                    return;
                otherOrder = order + 1;
            }
            else
            {
                // Up
                if (order == 0)
                    return;
                otherOrder = order - 1;
            }

            _CurrentPatch.SwitchInstructions(order + 1, otherOrder + 1);
            _UpdateInstructionList();

            // Selection
            instructionListView.SelectedIndices.Clear();
            instructionListView.SelectedIndices.Add(otherOrder);

            // Modifier flags
            _SetPatchModified(true);
            _SetInstructionModified(false);
        }

        /// <summary>
        /// Manages deployment of current patch (ie copy ModAndPlay executable and patch file to embed into an archived mod)
        /// </summary>
        /// <param name="isInstaller">true to deploy an install patch, false to deploy an uninstall one</param>
        private void _DeployCurrentPatch(bool isInstaller)
        {
            if (_CurrentPatch != null)
            {
                // Patch must be saved first
                EventArgs args = new EventArgs();

                saveArgsToolStripButton_Click(this, args);
                savePatchToolStripMenuItem_Click(this, args);

                folderBrowserDialog.Description = _MESSAGE_BROWSE_DEPLOY;

                // Using stored folder
                string storedFolder = Program.ApplicationSettings.PatchEditorLastDeployLocation;

                if (string.IsNullOrEmpty(storedFolder))
                    storedFolder = Environment.GetFolderPath(folderBrowserDialog.RootFolder);

                folderBrowserDialog.SelectedPath = storedFolder;

                DialogResult dr = folderBrowserDialog.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    string deployLocation = folderBrowserDialog.SelectedPath;

                    if (Directory.Exists(deployLocation))
                    {
                        // Stores deploy location
                        Program.ApplicationSettings.PatchEditorLastDeployLocation = deployLocation;
                        Program.ApplicationSettings.Save();
                    }
                    else
                        throw new Exception(_ERROR_DEPLOY_LOCATION);

                    // Patch must be saved
                    savePatchToolStripMenuItem_Click(this, new EventArgs());

                    Cursor = Cursors.WaitCursor;

                    // Executable copy (install only)
                    if (isInstaller)
                        File.Copy(
                            Application.StartupPath + LibraryConstants.FOLDER_PATCHS + AppConstants.FILE_EXE_MODANDPLAY,
                            deployLocation + @"\" + _CurrentPatch.InstallerFileName, true);

                    // Patch copy
                    Directory.CreateDirectory(deployLocation + LibraryConstants.FOLDER_PATCHS);

                    if (isInstaller)
                        File.Copy(_CurrentPatch.FileName,
                                  string.Concat(deployLocation, LibraryConstants.FOLDER_PATCHS,
                                                LibraryConstants.FILE_PATCH_INSTALL), true);
                    else
                        File.Copy(_CurrentPatch.FileName,
                                  string.Concat(deployLocation, LibraryConstants.FOLDER_PATCHS,
                                                LibraryConstants.FILE_PATCH_UNINSTALL), true);

                    // Libraries and references copy (install only)
                    if (isInstaller)
                    {
                        File.Copy(Application.StartupPath + DjeFramework1.LibraryProperties.FILE_LIBRARY,
                                  deployLocation + DjeFramework1.LibraryProperties.FILE_LIBRARY, true);
                        File.Copy(Application.StartupPath + TDUModdingLibrary.LibraryProperties.FILE_LIBRARY,
                                  deployLocation + TDUModdingLibrary.LibraryProperties.FILE_LIBRARY, true);

                        Directory.CreateDirectory(deployLocation + LibraryConstants.FOLDER_XML);
                        File.Copy(AppConstants.FOLDER_XML + VehicleSlotsHelper.FILE_SLOTS_REF_XML,
                                  string.Concat(deployLocation, LibraryConstants.FOLDER_XML,
                                                VehicleSlotsHelper.FILE_SLOTS_REF_XML),
                                  true);
                        File.Copy(AppConstants.FOLDER_XML + ColorsHelper.FILE_COLORS_REF_XML,
                                  string.Concat(deployLocation, LibraryConstants.FOLDER_XML,
                                                ColorsHelper.FILE_COLORS_REF_XML),
                                  true);

                        Directory.CreateDirectory(deployLocation + LibraryConstants.FOLDER_XML +
                                                  LibraryConstants.FOLDER_DEFAULT);
                        File.Copy(
                            AppConstants.FOLDER_XML + LibraryConstants.FOLDER_DEFAULT + Cameras.FILE_CAMERAS_BIN,
                            string.Concat(deployLocation, LibraryConstants.FOLDER_XML, LibraryConstants.FOLDER_DEFAULT,
                                          Cameras.FILE_CAMERAS_BIN),
                            true);
                    }

                    // All went OK, stroing deploy location
                    _LastDeployLocation = deployLocation;

                    Cursor = Cursors.Default;
                }
            }
        }
        
        /// <summary>
        /// Patch testing has finished
        /// </summary>
        private void _StopTesting()
        {
            _CurrentModAndPlayInstance = null;
            testingProbeTimer.Enabled = false;

            // Refreshing logs (report + debug)
            try
            {
                Cursor = Cursors.AppStarting;

                // Report
                TextBoxAppender appender = new TextBoxAppender(logTextBox,
                                                               bool.Parse(
                                                                   Program.ApplicationSettings.
                                                                       PatchEditorReportAutoScroll));
                string currentLogFile = _LastDeployLocation + LibraryConstants.FILE_LOG_PATCH;

                if (File.Exists(currentLogFile))
                {
                    string currentFileContents = File.ReadAllText(currentLogFile);

                    appender.AppendLine(currentFileContents);     
  
                }

                // Debug
                appender = new TextBoxAppender(debugTextBox, true);
                currentLogFile = _LastDeployLocation + LibraryConstants.FILE_MNP_LOG_DEBUG;

                if (File.Exists(currentLogFile))
                {
                    string currentFileContents = File.ReadAllText(currentLogFile);

                    appender.AppendLine(currentFileContents); 
                }
            }
            catch (Exception ex)
            {
                Log.Warning("Reporting error! " + ex);          
            }
            finally
            {
                Cursor = Cursors.Default;
                ProgressBar2.StopAnimate(mainProgressBar);
            }

            // Updating status
            StatusBarLogManager.ShowEvent(this, _STATUS_PATCH_RUNNING_DONE);
        }

        /// <summary>
        /// Patch testing in progress
        /// </summary>
        private static void _HandleTesting()
        {
            // Nothing to do for now
        } 

        /// <summary>
        /// Displays a list of instructions in provided patch file to import some
        /// </summary>
        /// <param name="patchFile"></param>
        private void _ImportPCH(string patchFile)
        {
            if (!string.IsNullOrEmpty(patchFile))
            {
                Cursor = Cursors.WaitCursor;

                // Loading patch file
                PCH patch = TduFile.GetFile(patchFile) as PCH;

                if (patch == null)
                    throw new Exception("Invalid TDUMT patch:" + patchFile);

                // Displaying patch contents window
                PatchInstructionsDialog dialog = new PatchInstructionsDialog(patch, _LABEL_INSTRUCTION_CHOOSER);
                DialogResult dr = dialog.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    foreach(PatchInstruction anotherInstruction in dialog.SelectedInstructions)
                        _CurrentPatch.ImportInstruction(anotherInstruction);
                }
            }           
        }
        #endregion

        #region Events
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Close' item
            Close();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Opens settings
            SettingsForm settingsForm = new SettingsForm(SettingsForm.SettingsTabPage.PatchEditor);

            settingsForm.ShowDialog(this);
        }

        private void openPatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Open' menu item
            if (!_ConfirmErase())
                return;

            // File location
            openFileDialog.Filter = GuiConstants.FILTER_PCH_ALL_FILES;
            openFileDialog.InitialDirectory = AppConstants.FOLDER_PATCHS;
            openFileDialog.Title = null;

            DialogResult dr = openFileDialog.ShowDialog(this);

            if (dr == DialogResult.OK)
            {
                _LoadPatch(openFileDialog.FileName);

                if (_CurrentPatch != null)
                {
                    _SetPatchModified(false);
                    StatusBarLogManager.ShowEvent(this, _STATUS_LOAD_SUCCESS);
                }
            }
        }

        private void savePatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Save' menu item
            if (_CurrentPatch == null)
                return;
            
            try
            {
                Cursor = Cursors.WaitCursor;

                // Current instruction
                if (_CurrentInstruction != null)
                    saveArgsToolStripButton_Click(sender, e);

                _CurrentPatch.Save();
                _SetPatchModified(false);
                StatusBarLogManager.ShowEvent(this, _STATUS_SAVE_SUCCESS);

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                StatusBarLogManager.ShowEvent(this, _ERROR_SAVING_PATCH);
                MessageBoxes.ShowError(this, ex.Message);
            }
        }

        private void savePatchAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Save as' menu item
            if (_CurrentPatch == null)
                return;

            saveFileDialog.Filter = GuiConstants.FILTER_PCH_ALL_FILES;
            saveFileDialog.InitialDirectory = AppConstants.FOLDER_PATCHS;

            DialogResult dr = saveFileDialog.ShowDialog(this);

            if (dr == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    // Current instruction
                    if (_CurrentInstruction != null)
                        saveArgsToolStripButton_Click(sender, e);

                    _CurrentPatch.SaveAs(saveFileDialog.FileName);
                    _SetPatchModified(false);
                    _UpdateFileNameLabel();
                    StatusBarLogManager.ShowEvent(this, _STATUS_SAVE_SUCCESS);

                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    StatusBarLogManager.ShowEvent(this, _ERROR_SAVING_PATCH);
                    MessageBoxes.ShowError(this, ex.Message);
                }
            }

        }

        private void PatchEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Before closing form...
            if (!_ConfirmErase())
                e.Cancel = true;
            else
            {
                // Form is to be closed
                StatusBarLogManager.RemoveLog(this);
                ProgressBar2.StopAnimate(mainProgressBar);

                // Ending test process if any
                if (_CurrentModAndPlayInstance != null)
                {
                    try
                    {
                        _CurrentModAndPlayInstance.Kill();
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Unable to kill MNP testing process.", ex);
                    }
                }

                _Instance = null;
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Properties' menu item
            if (_CurrentPatch != null)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    DialogResult dr = new PatchPropertiesDialog(_CurrentPatch).ShowDialog(this);

                    if (dr == DialogResult.OK)
                    {
                        _SetPatchModified(true);

                        // Instructions update
                        _UpdateInstructionList();
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
        
        private void runPatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Run patch' menu item
            if (CurrentPatch != null)
            {
                // - Test in progress?
                // - Was it recently deployed or modified?
                if (_CurrentModAndPlayInstance != null)
                    MessageBoxes.ShowError(this, _ERROR_TEST_IN_PROGRESS);
                else if (_LastDeployLocation == null || _IsModified)
                    MessageBoxes.ShowError(this, _ERROR_PATCH_NOT_DEPLOYED);
                else
                {
                    // Report display
                    tabControl.SelectedIndex = (int) EditorTabPages.Report;

                    StatusBarLogManager.ShowEvent(this, _STATUS_PATCH_RUNNING);

                    // EVO_162 Running deployed patch
                    try
                    {
                        // EVO_110: using report clearing setting
                        try
                        {
                            if (bool.Parse(Program.ApplicationSettings.PatchEditorReportClear))
                            {
                                logTextBox.Clear();
                                File.Delete(_LastDeployLocation + LibraryConstants.FILE_LOG_PATCH);
                                File.Delete(_LastDeployLocation + LibraryConstants.FILE_MNP_LOG_DEBUG);
                            }
                        } 
                        catch (Exception)
                        {
                            Log.Warning("Issue while deleting log files.");
                        }
                        //

                        ProcessStartInfo process = new ProcessStartInfo(_LastDeployLocation + @"\" + _CurrentPatch.InstallerFileName)
                        {
                            WorkingDirectory = _LastDeployLocation
                        };

                        _CurrentModAndPlayInstance = Process.Start(process);

                        // Progress Bar animation
                        ProgressBar2.StartAnimate(mainProgressBar);

                        // Starting probe timer
                        testingProbeTimer.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBoxes.ShowError(this, new Exception(_ERROR_EXECUTE_PATCH, ex));
                        StatusBarLogManager.ShowEvent(this, _STATUS_PATCH_ERROR);
                    }                    
                    //           
                }
            }
        }

        private void clearReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Clear Report' menu item
            // According to active tab ...
            if (tabControl.SelectedIndex == (int)EditorTabPages.Properties || tabControl.SelectedIndex == (int)EditorTabPages.Report)
                logTextBox.Clear();
            else if (tabControl.SelectedIndex == (int)EditorTabPages.Debug)
                debugTextBox.Clear();
        }

        private void newInstructionToolStripButton_Click(object sender, EventArgs e)
        {
            // Shortcut to 'Add new instruction' menu item
            addNewInstructionToolStripMenuItem_Click(sender, e);
        }

        private void addNewInstructionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Add new instruction' menu item
            if (_CurrentPatch == null)
                return;

            try
            {
                _AddInstruction();
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this,ex);
            }
        }

        private void clearReportToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on clear report tool strip button
            clearReportToolStripMenuItem_Click(sender, e);
        }

        private void instructionListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // An instruction has been selected
            if (instructionListView.SelectedIndices.Count == 1)
            {
                int instructionOrder = instructionListView.SelectedIndices[0] + 1;

                try
                {
                    _CurrentInstruction = _CurrentPatch.GetInstruction(instructionOrder);

                    // Current instruction type
                    if (typesComboBox.Text.Equals(_CurrentInstruction.Name))
                        // Enforces update
                        typesComboBox_SelectedIndexChanged(this, new EventArgs());
                    else
                        typesComboBox.Text = _CurrentInstruction.Name;
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
            else
                // Panel must be disabled
                _ManagePanelState(false);
        }

        private void typesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Another instruction type has been selected
            try
            {
                _InitializeInstructionContents();
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void saveArgsToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on Disk (Save) button (bottom panel)
            if (_CurrentPatch != null && _CurrentInstruction != null)
            {
                try
                {
                    // Properties
                    _CurrentInstruction.Enabled = enabledCheckbox.Checked;
                    _CurrentInstruction.FailOnError = failOnErrorCheckbox.Checked;
                    _CurrentInstruction.Comment = commentTextBox.Text;

                    // Install group
                    string groupName = PCH.REQUIRED_GROUP_NAME;

                    if (!string.IsNullOrEmpty(installGroupComboBox.Text))
                    {
                        groupName = installGroupComboBox.Text;

                        // Group list update
                        if (!installGroupComboBox.Items.Contains(groupName))
                            installGroupComboBox.Items.Add(groupName);
                    }

                    _CurrentInstruction.Group = _CurrentPatch.GetGroupFromName(groupName);

                    _CurrentPatch.SetInstruction(_CurrentInstruction);

                    _UpdateInstructionList();

                    // Modifier flags
                    _SetPatchModified(true);
                    _SetInstructionModified(false);

                    // Feedback
                    StatusBarLogManager.ShowEvent(this, _STATUS_SAVE_INSTR_SUCCESS);
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void parametersListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Double-click on an instruction parameter
            if (parametersListView.SelectedIndices.Count != 1)
                return;

            try
            {
                // Input box preparing...
                string paramName = parametersListView.SelectedItems[0].Text;
                string paramValue = parametersListView.SelectedItems[0].SubItems[1].Text;
                string message = string.Format(_MESSAGE_ENTER_PARAM_VALUE, paramName);
                PatchInstructionParameter param = PatchInstructionParameter.MakeParameter(paramName);
                ParameterValueDialog box = new ParameterValueDialog(message, param, paramValue);
                DialogResult dr = box.ShowDialog(this);

                if (dr == DialogResult.OK && box.IsValueChanged)
                {
                    _CurrentInstruction.SetParameter(paramName, box.ReturnValue);
                    _InitializeParameters();
                    _SetInstructionModified(true);
                }
            } 
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void deleteInstructionToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on thrash button : shortcut to menu
            deleteInstructionToolStripMenuItem_Click(this, e);
        }

        private void deleteInstructionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on "Delete instruction" menu item
            if (_CurrentPatch != null && instructionListView.SelectedIndices.Count == 1)
            {
                try
                {
                    int order = instructionListView.SelectedIndices[0];

                    _DeleteInstruction(order);
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }                
            }
        }

        private void failOnErrorCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            // Check or uncheck
            _SetInstructionModified(true);
        }

        private void enabledCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            // Check or uncheck
            _SetInstructionModified(true);
        }

        private void moveInstructionUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Move instruction up' menu item
            if (CurrentPatch != null && instructionListView.SelectedIndices.Count == 1)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    int selectedOrder = instructionListView.SelectedIndices[0];

                    _SwitchInstruction(selectedOrder, false);

                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }                
            }
        }

        private void moveInstructionDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'Move instruction down' menu item
            if (CurrentPatch != null && instructionListView.SelectedIndices.Count == 1)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    int selectedOrder = instructionListView.SelectedIndices[0];

                    _SwitchInstruction(selectedOrder, true);

                    Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void moveUpToolStripButton_Click(object sender, EventArgs e)
        {
            // Shortcut to 'Move up' menu item
            moveInstructionUpToolStripMenuItem_Click(sender, e);
        }

        private void moveDownToolStripButton_Click(object sender, EventArgs e)
        {
            // Shortcut to 'Move down' menu item
            moveInstructionDownToolStripMenuItem_Click(sender, e);
        }

        private void duplicateInstructionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on "Duplicate instruction" menu item
            if (_CurrentPatch != null && instructionListView.SelectedIndices.Count == 1)
            {
                try
                {
                    int order = instructionListView.SelectedIndices[0];

                    _DuplicateInstruction(order);
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on "Import instruction(s)" menu item
            if(_CurrentPatch != null)
            {
                try
                {
                    // EVO_152 : pch import support
                    // File selection
                    openFileDialog.Filter = GuiConstants.FILTER_XML_PCH_ALL_FILES;
                    openFileDialog.Title = _TITLE_OPEN_INSTRUCTIONS;
                    openFileDialog.FileName = "";

                    DialogResult dr = openFileDialog.ShowDialog(this);

                    if (dr == DialogResult.OK)
                    {
                        bool isSuccess = false;

                        // According to file extension
                        if (LibraryConstants.EXTENSION_PATCH_FILE.Equals(File2.GetExtensionFromFilename(openFileDialog.FileName).ToUpper()))
                        {
                            _ImportPCH(openFileDialog.FileName);
                            isSuccess = true;
                        }
                        else
                        {
                            Cursor = Cursors.WaitCursor;

                            string readData;

                            // Getting file data
                            using (
                                TextReader reader =
                                    new StreamReader(new FileStream(openFileDialog.FileName, FileMode.Open)))
                            {
                                readData = reader.ReadToEnd();
                            }

                            if (readData != null)
                            {
                                _CurrentPatch.ImportInstruction(readData);
                                isSuccess = true;
                            }
                        }

                        if (isSuccess)
                        {
                            // Refresh
                            _UpdateInstructionList();

                            // Selection
                            instructionListView.SelectedIndices.Clear();
                            instructionListView.SelectedIndices.Add(instructionListView.Items.Count - 1);

                            // Modifier flags
                            _SetPatchModified(true);
                            _SetInstructionModified(false);

                            // Status update
                            StatusBarLogManager.ShowEvent(this, _STATUS_IMPORT_SUCCESS);
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

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on "Export instruction(s)" menu item
            if (_CurrentPatch != null && instructionListView.SelectedItems.Count > 0)
            {
                try
                {
                    // File selection
                    saveFileDialog.Filter = GuiConstants.FILTER_XML_ALL_FILES;
                    saveFileDialog.Title = _TITLE_SAVE_XML;
                    saveFileDialog.FileName = "";

                    DialogResult dr = saveFileDialog.ShowDialog(this);

                    if (dr == DialogResult.OK)
                    {
                        Cursor = Cursors.WaitCursor;

                        List<PatchInstruction> instructionsToExport = new List<PatchInstruction>();

                        // Browsing selected instructions...
                        foreach (int anotherIndex in instructionListView.SelectedIndices)
                        {
                            PatchInstruction currentInstruction = _CurrentPatch.GetInstruction(anotherIndex + 1);

                            instructionsToExport.Add(currentInstruction);                            
                        }

                        // XML file
                        XmlWriter writer = new XmlTextWriter(saveFileDialog.FileName, Encoding.UTF8);

                        _CurrentPatch.ExportInstruction(instructionsToExport, writer);

                        // Status update
                        StatusBarLogManager.ShowEvent(this, _STATUS_EXPORT_SUCCESS);
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

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            // Clic on 'Help' toolbar button > displays online help
            try
            {
                // Launches default browser
                ProcessStartInfo editorProcess = new ProcessStartInfo(LibraryConstants.URL_OFFLINE_DOC);

                Process.Start(editorProcess);
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void duplicateInstructionToolStripButton_Click(object sender, EventArgs e)
        {
            // Click on copy button : shortcut to menu
            duplicateInstructionToolStripMenuItem_Click(this, e);
        }

        private void installerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'New>Installer' menu item
            if (_ConfirmErase())
            {
                try
                {
                    // New file pathname
                    saveFileDialog.Filter = GuiConstants.FILTER_PCH_ALL_FILES;
                    saveFileDialog.InitialDirectory = AppConstants.FOLDER_PATCHS;

                    DialogResult dr = saveFileDialog.ShowDialog(this);

                    if (dr == DialogResult.OK)
                    {
                        // If file already exists, it is deleted
                        File.Delete(saveFileDialog.FileName);

                        _LoadPatch(saveFileDialog.FileName);

                        if (_CurrentPatch != null)
                        {
                            // Properties window
                            propertiesToolStripMenuItem_Click(this, new EventArgs());

                            _SetPatchModified(true);
                            StatusBarLogManager.ShowEvent(this, _STATUS_READY);
                        }
                    }
                }
                catch (Exception ex )
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void uninstallerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on 'New>Uninstaller' menu item
            if (!_ConfirmErase())
                return;

            try
            {
                // Existing installer patch pathname
                openFileDialog.Filter = GuiConstants.FILTER_PCH_ALL_FILES;
                openFileDialog.InitialDirectory = AppConstants.FOLDER_PATCHS;
                openFileDialog.Title = _TITLE_OPEN_INSTALLER_PATCH;

                DialogResult dr = openFileDialog.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    FileInfo installerPatchInfo = new FileInfo(openFileDialog.FileName);
                    string installerFileName = @"\" + File2.GetNameFromFilename(installerPatchInfo.Name);
                    string uninstallerPatchFileName =
                        string.Concat(installerPatchInfo.DirectoryName, installerFileName, _SUFFIX_UNINSTALL, ".",
                                      LibraryConstants.EXTENSION_PATCH_FILE);

                    // If file already exists, it is deleted
                    File.Delete(uninstallerPatchFileName);

                    // Converts it to uninstall file
                    PCH installerPatch = TduFile.GetFile(openFileDialog.FileName) as PCH;

                    PatchHelper.ConvertInstallerToUninstaller(installerPatch, uninstallerPatchFileName);

                    _LoadPatch(uninstallerPatchFileName);

                    if (_CurrentPatch != null)
                    {
                        _SetPatchModified(false);
                        StatusBarLogManager.ShowEvent(this, _STATUS_UNINSTALLER_READY);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void installerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Click on 'Deploy patch>Installer' menu item
            if (_CurrentPatch != null)
            {
                try
                {
                    _DeployCurrentPatch(true);

                    string message = string.Format(_STATUS_DEPLOY_SUCCESS, folderBrowserDialog.SelectedPath);

                    StatusBarLogManager.ShowEvent(this, message);
                }
                catch (Exception ex)
                {
                    StatusBarLogManager.ShowEvent(this, _STATUS_DEPLOY_FAILED);
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void uninstallerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Click on 'Deploy patch>Uninstaller' menu item
            if (_CurrentPatch != null)
            {
                try
                {
                    _DeployCurrentPatch(false);

                    string message = string.Format(_STATUS_DEPLOY_SUCCESS, folderBrowserDialog.SelectedPath);

                    StatusBarLogManager.ShowEvent(this, message);
                }
                catch (Exception ex)
                {
                    StatusBarLogManager.ShowEvent(this, _STATUS_DEPLOY_FAILED);
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void aboutInstructionLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on help link > displays help dialog
            if (_CurrentInstruction != null)
            {
                try
                {
                    _HelpDialog.ShowHelp(_CurrentInstruction);
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void commentTextBox_TextChanged(object sender, EventArgs e)
        {
            // New comment entered
            _SetInstructionModified(true);
        }

        private void installGroupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Group changed
            _SetInstructionModified(true);
        }

        private void testingProbeTimer_Tick(object sender, EventArgs e)
        {
            // Refresh interval for this probe
            if (_CurrentModAndPlayInstance == null)
                testingProbeTimer.Enabled = false;
            else if (_CurrentModAndPlayInstance.HasExited)
                // Test window was closed
                _StopTesting();
            else
                // OK status
                _HandleTesting();
        }

        private void openLastDeployLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on Open last deploy location item
            string lastDeployLocation = Program.ApplicationSettings.PatchEditorLastDeployLocation;

            if (!string.IsNullOrEmpty(lastDeployLocation))
            {
                try
                {
                    ProcessStartInfo explorerProcess = new ProcessStartInfo(lastDeployLocation);
                    Process.Start(explorerProcess);
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Provides access to this form
        /// </summary>
        /// <param name="fileName">patch file to open</param>
        /// <returns></returns>
        public static PatchEditorForm GetInstance(string fileName)
        {
            if (_Instance == null)
                _Instance = new PatchEditorForm(fileName);
            else if (!string.IsNullOrEmpty(fileName))
            {
                // Opens specified file
                if (_Instance._ConfirmErase())
                    _Instance._LoadPatch(fileName);
            }
            return _Instance;
        }
        #endregion
    }
}
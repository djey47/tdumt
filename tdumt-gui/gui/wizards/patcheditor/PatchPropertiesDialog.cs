using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.Types;
using DjeFramework1.Common.Types.Collections;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.fileformats.specific;
using TDUModdingLibrary.support;
using TDUModdingLibrary.support.constants;
using TDUModdingTools.common;
using TDUModdingTools.gui.common;
using System.Drawing;
using TDUModdingTools.Properties;

namespace TDUModdingTools.gui.wizards.patcheditor
{
    public partial class PatchPropertiesDialog : Form
    {
        #region Constants
        /// <summary>
        /// Slot dialog box main message
        /// </summary>
        private const string _MESSAGE_BROWSE_SLOTS = "Please select through available slots...";

        /// <summary>
        /// Custom role setting message
        /// </summary>
        private const string _MESSAGE_SET_ROLE_CUSTOM = "Which role to give ?";
        
        /// <summary>
        /// Name setting message
        /// </summary>
        private const string _MESSAGE_SET_NAME_CUSTOM = "Which person is it ?";

        /// <summary>
        /// Patch editor title
        /// </summary>
        private const string _TITLE_PATCH_EDITOR = "Patch Editor...";

        /// <summary>
        /// Picture browser title
        /// </summary>
        private const string _TITLE_BROWSE_PIC = "Open picture file...";

        /// <summary>
        /// Prefix for custom role name
        /// </summary>
        private const string _CUSTOM_PREFIX = "(custom) ";

        /// <summary>
        /// Index in credit list for first custom role
        /// </summary>
        private const int _CUSTOM_ROLES_START_INDEX = 6;
        #endregion

        #region Members
        /// <summary>
        /// Current patch
        /// </summary>
        private readonly PCH _CurrentPatch;

        /// <summary>
        /// Current dependancies
        /// </summary>
        private readonly Dictionary<string, string> _CurrentDependencies = new Dictionary<string, string>();

        /// <summary>
        /// Current checked states
        /// </summary>
        private readonly Dictionary<string, bool> _CurrentCheckedStates = new Dictionary<string, bool>();

        /// <summary>
        /// Current excluded groups
        /// </summary>
        private readonly Dictionary<string, Collection<string>> _CurrentExclusions = new Dictionary<string, Collection<string>>();

        /// <summary>
        /// List of group names to delete
        /// </summary>
        private readonly Collection<string> _GroupsToRemove = new Collection<string>();
        #endregion

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="currentPatch">Patch to display properties</param>
        public PatchPropertiesDialog(PCH currentPatch)
        {
            InitializeComponent();
            _CurrentPatch = currentPatch;

            // Dependancies and checked states
            foreach (PCH.InstallGroup anotherGroup in _CurrentPatch.Groups)
            {
                string currentGroupName = anotherGroup.name;

                // Dependencies
                if (anotherGroup.parentName != null)
                    _CurrentDependencies.Add(currentGroupName, anotherGroup.parentName);

                // Prechecked state
                _CurrentCheckedStates.Add(currentGroupName, anotherGroup.isDefaultChecked);

                // Exclusions
                if (anotherGroup.excludedGroupNames == null)
                    _CurrentExclusions.Add(currentGroupName, new Collection<string>());
                else
                    _CurrentExclusions.Add(currentGroupName, anotherGroup.excludedGroupNames);
            }

            _InitializeContents();
        }

        #region Private methods
        /// <summary>
        /// Defines Form contents
        /// </summary>
        private void _InitializeContents()
        {
            if (_CurrentPatch != null)
            {
                _UpdateMainTab();

                _UpdatePictureTab();

                // EVO_131
                _UpdateRoleInformationTable();

                // EVO_134
                _UpdateGroupTab();
            }
        }

        /// <summary>
        /// Defines main tab contents
        /// </summary>
        private void _UpdateMainTab()
        {
            nameTextBox.Text = _CurrentPatch.Name;
            versionTextBox.Text = _CurrentPatch.Version;
            authorTextBox.Text = _CurrentPatch.Author;
            dateTextBox.Text = _CurrentPatch.Date;
            // EVO_142
            freeCommentTextBox.Text = _CurrentPatch.Free;
            slotRefTextBox.Text = _CurrentPatch.SlotRef;
            // EVO_167
            installerFileNameTextBox.Text = _CurrentPatch.InstallerFileName;
            // EVO_175_176
            infoWebsiteTextBox.Text = _CurrentPatch.InfoURL;
        }

        /// <summary>
        /// Defines group tab contents
        /// </summary>
        private void _UpdateGroupTab()
        {
            // Custom required group name
            requiredNameTextBox.Text = _CurrentPatch.CustomRequiredName;

            // Groups list box
            groupComboBox.Items.Clear();

            foreach (PCH.InstallGroup anotherGroup in _CurrentPatch.Groups)
            {
                string anotherName = anotherGroup.name;

                if (anotherName != null
                    && !anotherName.Equals(PCH.REQUIRED_GROUP_NAME)
                    && !_GroupsToRemove.Contains(anotherName))
                    groupComboBox.Items.Add(anotherGroup.name);
            }

            // Prechecked
            precheckedCheckBox.Enabled = false;
        }

        /// <summary>
        /// Fills roles table
        /// </summary>
        private void _UpdateRoleInformationTable()
        {
            ListViewItem item;

            creditsListView.Items.Clear();

            int roleIndex = 0;

            foreach(KeyValuePair<string, string> roleInformation in _CurrentPatch.Roles)
            {
                string roleName = roleInformation.Key;

                // Custom roles
                if (roleIndex >= _CUSTOM_ROLES_START_INDEX)
                    roleName = _CUSTOM_PREFIX + roleName;

                item = new ListViewItem(roleName);

                item.SubItems.Add(roleInformation.Value);
                creditsListView.Items.Add(item);
                roleIndex++;
            }
        }

        /// <summary>
        /// Saves role information into patch properties
        /// </summary>
        private void _CommitRoles()
        {
            _CurrentPatch.Roles.Clear();

            int roleIndex = 0;

            foreach (ListViewItem lvi in creditsListView.Items)
            {
                // Custom roles handling
                string roleName = lvi.Text;

                if (roleIndex >= _CUSTOM_ROLES_START_INDEX)
                    roleName = _CleanupCustomName(roleName);

                _CurrentPatch.Roles.Add(roleName, lvi.SubItems[1].Text);
                roleIndex++;
            }
        }

        /// <summary>
        /// Saves group dependency into patch properties
        /// </summary>
        private void _CommitGroups()
        {
            // Group removal
            foreach(string anotherGroupName in _GroupsToRemove)
                _CurrentPatch.RemoveGroup(anotherGroupName);

            // Custom required group name
            if (string.IsNullOrEmpty(requiredNameTextBox.Text))
                _CurrentPatch.CustomRequiredName = PCH.REQUIRED_GROUP_NAME;
            else
                _CurrentPatch.CustomRequiredName = requiredNameTextBox.Text;
           
            // Dependencies
            foreach(KeyValuePair<string, string> anotherGroup in _CurrentDependencies)
            {
                PCH.InstallGroup newGroup = new PCH.InstallGroup
                                                {
                                                    name = anotherGroup.Key,
                                                    parentName = anotherGroup.Value
                                                };
                _CurrentPatch.UpdateGroup(newGroup);
            }

            // Checked states
            foreach (KeyValuePair<string, bool> anotherState in _CurrentCheckedStates)
            {
                PCH.InstallGroup aGroup = _CurrentPatch.GetGroupFromName(anotherState.Key);

                aGroup.isDefaultChecked = anotherState.Value;
                _CurrentPatch.UpdateGroup(aGroup);
            }

            // Exclusions
            foreach (KeyValuePair<string, Collection<string>> anotherExclusion in _CurrentExclusions)
            {
                PCH.InstallGroup aGroup = _CurrentPatch.GetGroupFromName(anotherExclusion.Key);

                aGroup.excludedGroupNames = anotherExclusion.Value;
                _CurrentPatch.UpdateGroup(aGroup);
            }
        }

        /// <summary>
        /// Saves main attributes into patch properties
        /// </summary>
        private void _CommitMainAttributes()
        {
            _CurrentPatch.Name = nameTextBox.Text;
            _CurrentPatch.Version = versionTextBox.Text;
            _CurrentPatch.Author = authorTextBox.Text;
            _CurrentPatch.Date = dateTextBox.Text;
            // EVO_142
            _CurrentPatch.Free = freeCommentTextBox.Text;
            _CurrentPatch.SlotRef = slotRefTextBox.Text;
            // EVO_167
            string installerFileName = installerFileNameTextBox.Text;

            if (string.IsNullOrEmpty(installerFileName))
                installerFileName = PCH.INSTALLER_FILE_NAME;
            else
            {
                const string executableSuffix = ("." + LibraryConstants.EXTENSION_EXE_FILE);

                if (!installerFileName.ToUpper().EndsWith(executableSuffix))
                    installerFileName += executableSuffix;
            }

            _CurrentPatch.InstallerFileName = installerFileName;
            // EVO_175_176
            _CurrentPatch.InfoURL = infoWebsiteTextBox.Text;
        }

        /// <summary>
        /// Refreshes dependency group list 
        /// </summary>
        /// <param name="currentGroup"></param>
        private void _UpdateDependencyGroups(string currentGroup)
        {
            requiredGroupComboBox.Items.Clear();

            // Empty element
            requiredGroupComboBox.Items.Add("");

            foreach (PCH.InstallGroup anotherGroup in _CurrentPatch.Groups)
            {
                string anotherName = anotherGroup.name;

                // Prevents cycles. Current group and required groups must no be displayed.
                if (anotherName != null 
                    && !anotherName.Equals(currentGroup)
                    && !anotherName.Equals(PCH.REQUIRED_GROUP_NAME)
                    && !_GroupsToRemove.Contains(anotherName)
                    && !(_CurrentDependencies.ContainsKey(anotherName)
                        && _CurrentDependencies[anotherName].Equals(currentGroup)))
                    requiredGroupComboBox.Items.Add(anotherGroup.name);
            }

            // Selects current dependancy
            string currentDependancy = "";

            if (_CurrentDependencies.ContainsKey(currentGroup))
                currentDependancy = _CurrentDependencies[currentGroup];

            requiredGroupComboBox.Text = currentDependancy;
        }
        
        /// <summary>
        /// Refreshes exclusion group list 
        /// </summary>
        /// <param name="currentGroup"></param>
        private void _UpdateExcludedGroups(string currentGroup)
        {
            excludedGroupsCheckedListBox.Items.Clear();

            foreach (PCH.InstallGroup anotherGroup in _CurrentPatch.Groups)
            {
                string anotherName = anotherGroup.name;
                string dependencyName = null;
                
                if (_CurrentDependencies.ContainsKey(currentGroup))
                    dependencyName = _CurrentDependencies[currentGroup];

                // Dependency group can't be excluded
                if (anotherName != null
                    && !anotherName.Equals(currentGroup)
                    && !anotherName.Equals(PCH.REQUIRED_GROUP_NAME)
                    && !_GroupsToRemove.Contains(anotherName)
                    && !anotherName.Equals(dependencyName))
                {
                    // Checks currently excluded groups
                    Collection<string> excludedGroupNames = _CurrentExclusions[currentGroup];

                    if (excludedGroupNames != null && excludedGroupNames.Contains(anotherName))
                        excludedGroupsCheckedListBox.Items.Add(anotherGroup.name, true);
                    else
                        excludedGroupsCheckedListBox.Items.Add(anotherGroup.name);                        
                }
            }          
        }

        /// <summary>
        /// Defines Picture tab contents
        /// </summary>
        private void _UpdatePictureTab()
        {
            // Searching for picture in deploy folder
            string deployFolder = Program.ApplicationSettings.PatchEditorLastDeployLocation;

            if (!string.IsNullOrEmpty(deployFolder) && Directory.Exists(deployFolder))
            {
                string pictureFile = deployFolder + LibraryConstants.FILE_MNP_BACKGROUND_PICTURE;

                notGeneratedLabel.Visible = false;

                if (File.Exists(pictureFile))
                {
                    string tempFolder = File2.SetTemporaryFolder(null, LibraryConstants.FOLDER_TEMP, true);
                    string workingFile = tempFolder + LibraryConstants.FILE_MNP_BACKGROUND_PICTURE;

                    File.Copy(pictureFile, workingFile, true);
                    picPanel.BackgroundImage = Image.FromFile(workingFile);
                }
                else
                    picPanel.BackgroundImage = Resources.nopic;
            }
            else
            {
                picPanel.BackgroundImage = null;
                notGeneratedLabel.Visible = true;
            }
        }

        /// <summary>
        /// Returns name without custom label
        /// </summary>
        /// <param name="customName"></param>
        private static string _CleanupCustomName(string customName)
        {
            string returnedName = "";
            
            if (!string.IsNullOrEmpty(customName) && customName.StartsWith(_CUSTOM_PREFIX))
                returnedName = customName.Substring(_CUSTOM_PREFIX.Length);

            return returnedName;
        }
        #endregion

        #region Events
        private void cancelButton_Click(object sender, EventArgs e)
        {
            // Click on 'cancel' button
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            // Click on 'ok' button
            try
            {
                Cursor = Cursors.WaitCursor;

                _CommitMainAttributes();

                // EVO_131
                _CommitRoles();

                // EVO_134
                _CommitGroups();

                DialogResult = DialogResult.OK;
                Close();
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

        private void browseSlotsButton_Click(object sender, EventArgs e)
        {
            // Click on '...' button for slots
            try
            {
                Cursor = Cursors.WaitCursor;

                // Preparing slot list
                SortedStringCollection sortedSlotList = new SortedStringCollection();
                Dictionary<string, string> index = new Dictionary<string, string>();

                // Misc slot
                sortedSlotList.Add(Tools.NAME_MISC_SLOT);
                index.Add(Tools.NAME_MISC_SLOT, Tools.KEY_MISC_SLOT);

                // Vehicle slots
                VehicleSlotsHelper.InitReference(Tools.WorkingPath + LibraryConstants.FOLDER_XML);

                foreach (KeyValuePair<string, string> pair in VehicleSlotsHelper.SlotReference)
                {
                    sortedSlotList.Add(pair.Key);
                    index.Add(pair.Key, pair.Value);
                }

                Cursor = Cursors.Default;

                // Displaying browse dialog
                TableBrowsingDialog dialog = new TableBrowsingDialog(_MESSAGE_BROWSE_SLOTS, sortedSlotList, index);
                DialogResult dr = dialog.ShowDialog();

                if (dr == DialogResult.OK && dialog.SelectedIndex != null)
                    slotRefTextBox.Text = dialog.SelectedIndex;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void setRoleButton_Click(object sender, EventArgs e)
        {
            // Click on 'Set role...' button (custom)
            try
            {
                if (creditsListView.SelectedItems.Count == 1)
                {
                    ListViewItem selectedRole = creditsListView.SelectedItems[0];
                    int roleIndex = selectedRole.Index;

                    if (roleIndex >= _CUSTOM_ROLES_START_INDEX)
                    {
                        string currentRole = _CleanupCustomName(selectedRole.Text);
                        PromptBox pBox = new PromptBox(_TITLE_PATCH_EDITOR, _MESSAGE_SET_ROLE_CUSTOM, currentRole);

                        if (pBox.ShowDialog(this) == DialogResult.OK && !pBox.ReturnValue.Equals(currentRole))
                        {
                            Cursor = Cursors.WaitCursor;

                            _CurrentPatch.SetRoleName(roleIndex, pBox.ReturnValue);

                            // Refresh
                            _UpdateRoleInformationTable();
                        }
                    }
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

        private void setNameButton_Click(object sender, EventArgs e)
        {
            // Click on 'Set name...' button
            try
            {
                if (creditsListView.SelectedItems.Count == 1)
                {
                    ListViewItem selectedRole = creditsListView.SelectedItems[0];
                    int roleIndex = selectedRole.Index;
                    string currentName = selectedRole.SubItems[1].Text;
                    PromptBox pBox = new PromptBox(_TITLE_PATCH_EDITOR, _MESSAGE_SET_NAME_CUSTOM, currentName);

                    if (pBox.ShowDialog(this) == DialogResult.OK && !pBox.ReturnValue.Equals(currentName))
                    {
                        Cursor = Cursors.WaitCursor;

                        _CurrentPatch.SetRoleAuthorName(roleIndex, pBox.ReturnValue);

                        // Refresh
                        _UpdateRoleInformationTable();
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
        
        private void groupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Another group has been selected > dependancy list update
            try
            {
                Cursor = Cursors.WaitCursor;

                string selectedGroupName = groupComboBox.SelectedItem.ToString();

                // Default checking
                precheckedCheckBox.Enabled = true;
                precheckedCheckBox.Checked = _CurrentCheckedStates[selectedGroupName];

                // Dependencies
                _UpdateDependencyGroups(selectedGroupName);

                // Exclusions
                _UpdateExcludedGroups(selectedGroupName);
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

        private void requiredGroupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Another group has been selected
            try
            {
                Cursor = Cursors.WaitCursor;

                string currentDependancy = requiredGroupComboBox.Text;
                string currentGroup = groupComboBox.Text;

                if ("".Equals(currentDependancy))
                    _CurrentDependencies.Remove(currentGroup);
                else
                {
                    if (_CurrentDependencies.ContainsKey(currentGroup))
                        _CurrentDependencies[currentGroup] = currentDependancy;
                    else
                        _CurrentDependencies.Add(currentGroup, currentDependancy);
                }

                // Exclusion list must be updated
                _UpdateExcludedGroups(currentGroup);
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

        private void precheckedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Prechecked has been checked > updates temporary list
            try
            {
                _CurrentCheckedStates[groupComboBox.SelectedItem.ToString()] = (precheckedCheckBox.Checked);                
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void excludedGroupsCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // An exclusion group has been checked > updates temporary list
            try
            {
                Cursor = Cursors.WaitCursor;

                string currentExcludedGroup = excludedGroupsCheckedListBox.Items[e.Index].ToString();
                string currentGroup = groupComboBox.SelectedItem.ToString();

                if (e.NewValue == CheckState.Checked)
                {
                    if (!_CurrentExclusions[currentGroup].Contains(currentExcludedGroup))
                        _CurrentExclusions[currentGroup].Add(currentExcludedGroup);

                    // Also adds to other side
                    if (!_CurrentExclusions[currentExcludedGroup].Contains(currentGroup))
                        _CurrentExclusions[currentExcludedGroup].Add(currentGroup);
                }
                else
                {
                    _CurrentExclusions[currentGroup].Remove(currentExcludedGroup);

                    // Also removes from other side
                    _CurrentExclusions[currentExcludedGroup].Remove(currentGroup);
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

        private void groupDeleteButton_Click(object sender, EventArgs e)
        {
            // Click on trash button to delete current group
            if (!string.IsNullOrEmpty(groupComboBox.Text))
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    string currentGroup = groupComboBox.SelectedItem.ToString();

                    // Deleting self items
                    _GroupsToRemove.Add(currentGroup);
                    _CurrentCheckedStates.Remove(currentGroup);
                    _CurrentDependencies.Remove(currentGroup);
                    _CurrentExclusions.Remove(currentGroup);

                    // Deleting dependencies to this group
                    Collection<string> dependenciesToRemove = new Collection<string>();

                    foreach (string anotherGroup in _CurrentDependencies.Keys)
                    {
                        string currentDependency = _CurrentDependencies[anotherGroup];

                        if (currentGroup.Equals(currentDependency))
                            dependenciesToRemove.Add(anotherGroup);
                    }

                    foreach (string anotherGroup in dependenciesToRemove)
                        _CurrentDependencies.Remove(anotherGroup);

                    // Deleting exclusions of this group
                    Collection<string> exclusionsToRemove = new Collection<string>();

                    foreach (string anotherGroup in _CurrentExclusions.Keys)
                    {
                        Collection<string> exclusions = _CurrentExclusions[anotherGroup];

                        if (exclusions != null && exclusions.Contains(currentGroup))
                            exclusionsToRemove.Add(anotherGroup);
                    }

                    foreach (string anotherGroup in exclusionsToRemove)
                        _CurrentExclusions[anotherGroup].Remove(currentGroup);

                    // Contents need to be updated
                    _UpdateGroupTab();
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

        private void removePicButton_Click(object sender, EventArgs e)
        {
            // Click on remove picture button
            string picFile = Program.ApplicationSettings.PatchEditorLastDeployLocation + LibraryConstants.FILE_MNP_BACKGROUND_PICTURE;

            if (File.Exists(picFile))
            {
                try
                {
                    File.Delete(picFile);

                    // Refresh
                    _UpdatePictureTab();
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

        private void browsePicButton_Click(object sender, EventArgs e)
        {
            // Click on browse picture button
            try
            {
                openFileDialog.Filter = GuiConstants.FILTER_PNG_FILES;
                openFileDialog.Title = _TITLE_BROWSE_PIC;

                DialogResult dr = openFileDialog.ShowDialog(this);   
       
                if (dr == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    string targetFile = Program.ApplicationSettings.PatchEditorLastDeployLocation +
                                        LibraryConstants.FILE_MNP_BACKGROUND_PICTURE;

                    File.Copy(openFileDialog.FileName, targetFile, true);

                    // Refresh
                    _UpdatePictureTab();
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
        #endregion
    }
}
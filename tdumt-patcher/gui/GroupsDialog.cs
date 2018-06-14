using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using TDUModdingLibrary.fileformats.specific;

namespace TDUModAndPlay.gui
{
    /// <summary>
    /// Dialog box for ability to choose which groups to install
    /// </summary>
    public partial class GroupsDialog : Form
    {
        #region Constants
        /// <summary>
        /// Format string for child group
        /// </summary>
        private const string _FORMAT_REQUIRES_GROUP = "{0} - requires {1}";
        #endregion

        #region Properties
        /// <summary>
        /// List of chosen groups
        /// </summary>
        public List<string> ChosenGroups { get; set; }
        #endregion

        #region Private properties
        /// <summary>
        /// Name of current mod
        /// </summary>
        private string _ModName { get { return _Patch.Name; }}

        /// <summary>
        /// List of available groups
        /// </summary>
        private IEnumerable<PCH.InstallGroup> _Groups { get { return _Patch.Groups; }}

        /// <summary>
        /// Custom name for required group
        /// </summary>
        private string _CustomRequiredGroupName { get { return _Patch.CustomRequiredName;  } }
        #endregion

        #region Private fields
        /// <summary>
        /// Current patch
        /// </summary>
        private readonly PCH _Patch;
        #endregion

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="currentPatch"></param>
        public GroupsDialog(PCH currentPatch)
        {
            InitializeComponent();

            if (currentPatch != null)
            {
                _Patch = currentPatch;

                _InitializeContents();
            }
        }

        #region Events
        private void installButton_Click(object sender, EventArgs e)
        {
            // Click on 'install' button, preparing chosen groups list
            ChosenGroups = new List<string>();

            foreach (ListViewItem anotherLvi in groupListView.Items)
            {
                if (anotherLvi.Checked)
                {
                    PCH.InstallGroup currentGroup = (PCH.InstallGroup) anotherLvi.Tag;

                    ChosenGroups.Add(currentGroup.name);
                }
            }
        }

        private void groupListView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // A checkbox will be checked/unchecked soon....
            try
            {
                ListViewItem currentItem = groupListView.Items[e.Index];
                PCH.InstallGroup currentGroup = (PCH.InstallGroup) currentItem.Tag;

                if (e.NewValue == CheckState.Unchecked && PCH.REQUIRED_GROUP_NAME.Equals(currentGroup.name))
                    e.NewValue = e.CurrentValue;
                else if (!PCH.REQUIRED_GROUP_NAME.Equals(currentGroup.name))
                {
                    if (e.NewValue == CheckState.Checked)
                    {
                        // Checks required group
                        string requiredGroupName = currentGroup.parentName;

                        if (!string.IsNullOrEmpty(requiredGroupName) && groupListView.Items.ContainsKey(requiredGroupName))
                        {
                            ListViewItem searchedItem = groupListView.Items[requiredGroupName];

                            if (!searchedItem.Checked)
                                searchedItem.Checked = true;
                        }

                        // Unchecks all excluded groups
                        Collection<string> excludedGroups = currentGroup.excludedGroupNames;

                        foreach (string anotherGroupName in excludedGroups)
                        {
                            if (!string.IsNullOrEmpty(anotherGroupName) && groupListView.Items.ContainsKey(anotherGroupName))
                            {
                                ListViewItem searchedItem = groupListView.Items[anotherGroupName];

                                if (searchedItem.Checked)
                                    searchedItem.Checked = false;
                            }
                        }
                    }
                    else
                    {
                        // Unchecks all groups depending on it
                        foreach(ListViewItem anotherLvi in groupListView.Items)
                        {
                            if (anotherLvi.Tag != null)
                            {
                                PCH.InstallGroup anotherGroup = (PCH.InstallGroup)anotherLvi.Tag;

                                if (!string.IsNullOrEmpty(anotherGroup.parentName) 
                                    && anotherGroup.parentName.Equals(currentGroup.name) 
                                    && anotherLvi.Checked)
                                    anotherLvi.Checked = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Defines window contents
        /// </summary>
        private void _InitializeContents()
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                // Title
                Text = _ModName;

                // Group list
                foreach(PCH.InstallGroup anotherGroup in _Groups)
                {
                    // Computes group level
                    string groupName = _GetGroupName(anotherGroup);
                    bool isRequiredGroup = (PCH.REQUIRED_GROUP_NAME.Equals(anotherGroup.name));
                    bool isChecked = (isRequiredGroup || anotherGroup.isDefaultChecked);
                    ListViewItem lvi = new ListViewItem(groupName)
                                           {
                                               Checked = isChecked,
                                               Tag = anotherGroup,
                                               Name = anotherGroup.name
                                           };

                    // Required group is first item and grayed by default
                    if (isRequiredGroup)
                    {
                        // Custom name support
                        if (_CustomRequiredGroupName != null)
                            lvi.Text = _CustomRequiredGroupName;

                        lvi.ForeColor = Color.Maroon;
                        groupListView.Items.Insert(0, lvi);
                    }
                    else
                        groupListView.Items.Add(lvi);
                }
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

        /// <summary>
        /// Returns displayed group name
        /// </summary>
        /// <param name="installGroup"></param>
        /// <returns></returns>
        private static string _GetGroupName(PCH.InstallGroup installGroup)
        {
            string returnedGroupName;

            if (string.IsNullOrEmpty(installGroup.parentName))
                returnedGroupName = installGroup.name;
            else
                returnedGroupName = string.Format(_FORMAT_REQUIRES_GROUP, installGroup.name, installGroup.parentName);

            return returnedGroupName;
        }
        #endregion
    }
}
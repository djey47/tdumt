using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using TDUModdingLibrary.fileformats.binaries;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.support;

namespace TDUModdingTools.gui.wizards.vehiclemanager.cameraIK
{
    /// <summary>
    /// Dialog handling choice of camera set
    /// </summary>
    public partial class CamSelectorDialog : Form
    {
        #region Constants
        /// <summary>
        /// Defaults tree node
        /// </summary>
        private const string _NODE_DEFAULTS = "<USE DEFAULT VIEW>";
        #endregion

        #region Properties
        /// <summary>
        /// Id of selected view
        /// </summary>
        internal string ChosenViewId
        {
            get { return _ChosenViewId; }
        }
        private string _ChosenViewId;

        /// <summary>
        /// Id of selected camera
        /// </summary>
        internal string ChosenCameraId
        {
            get { return _ChosenCameraId; }
        }
        private string _ChosenCameraId;
        #endregion

        #region Members
        /// <summary>
        /// Camera information
        /// </summary>
        private readonly Cameras _CamerasData;
        #endregion

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="camerasData"></param>
        public CamSelectorDialog(Cameras camerasData)
        {
            InitializeComponent();

            if (camerasData != null)
            {
                _CamerasData = camerasData;

                _InitializeContents();
            }
        }

        #region Private methods
        /// <summary>
        /// Defines window contents
        /// </summary>
        private void _InitializeContents()
        {
            Cursor = Cursors.WaitCursor;

            // Views treeview
            viewsTreeView.Nodes.Clear();

            // Bugs spotted > default view disabled for now
            /*TreeNode newNode = new TreeNode(_NODE_DEFAULTS)
                                   {
                                       NodeFont = new Font(viewsTreeView.Font, FontStyle.Bold),
                                       Tag = -1
                                   };

            viewsTreeView.Nodes.Add(newNode);*/

            try
            {
                // Browsing available cameras...
                foreach (KeyValuePair<string, string> cameraRef in VehicleSlotsHelper.CamReferenceReverse)
                {
                    TreeNode newNode = new TreeNode(cameraRef.Key);

                    // Browsing current set...
                    Cameras.CamEntry currentEntry = _CamerasData.GetEntryByCameraId(cameraRef.Value);

                    if (currentEntry.isValid && currentEntry.views.Count > 0)
                    {
                        // EVO_169 View label now sorted
                        SortedDictionary<string, Cameras.View> viewsByType =
                            new SortedDictionary<string, Cameras.View>();

                        foreach (Cameras.View anotherView in currentEntry.views)
                            viewsByType.Add(anotherView.type.ToString(), anotherView);

                        foreach (KeyValuePair<string, Cameras.View> viewInfomation in viewsByType)
                        {
                            TreeNode newChildNode = new TreeNode(viewInfomation.Key)
                                                        {
                                                            NodeFont = new Font(viewsTreeView.Font, FontStyle.Bold),
                                                            Tag =
                                                                cameraRef.Value + Tools.SYMBOL_VALUE_SEPARATOR +
                                                                (int) viewInfomation.Value.type
                                                        };

                            newNode.Nodes.Add(newChildNode);
                        }
                        //

                        viewsTreeView.Nodes.Add(newNode);
                    }
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
        #endregion

        #region Events
        private void okButton_Click(object sender, EventArgs e)
        {
            // Click on 'Set...' button
            TreeNode selectedNode = viewsTreeView.SelectedNode;

            if (selectedNode != null && selectedNode.Tag != null)
            {
                if (selectedNode.Tag.Equals(-1))
                    // Back to defaults: must delete selected view
                    _ChosenCameraId = _ChosenViewId = null;
                else
                {
                    // New assignment
                    string nodeTag = selectedNode.Tag.ToString();

                    _ChosenCameraId = nodeTag.Split(Tools.SYMBOL_VALUE_SEPARATOR)[0];
                    _ChosenViewId = nodeTag.Split(Tools.SYMBOL_VALUE_SEPARATOR)[1];
                }

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void viewsTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Double-click on a node
            okButton_Click(this, new EventArgs());
        }
        #endregion
    }
}
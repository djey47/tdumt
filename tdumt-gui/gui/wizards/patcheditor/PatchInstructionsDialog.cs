using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TDUModdingLibrary.fileformats.specific;
using TDUModdingLibrary.support.patcher.instructions;

namespace TDUModdingTools.gui.wizards.patcheditor
{
    public partial class PatchInstructionsDialog : Form
    {
        #region Constants
        /// <summary>
        /// Error message when missing patch file
        /// </summary>
        private const string _ERROR_PATCH_MISSING = "No patch was specified to display instructions!";

        /// <summary>
        /// Default label in top of the window
        /// </summary>
        private const string _DEFAULT_LABEL = "Patch contents:";
        #endregion

        #region Properties
        /// <summary>
        /// Array of selected instructions
        /// </summary>
        public PatchInstruction[] SelectedInstructions { get; set; }
        #endregion

        #region Members

        /// <summary>
        /// Current patch
        /// </summary>
        private readonly PCH _CurrentPatch;

        /// <summary>
        /// Custom label to display on top
        /// </summary>
        private string _CustomLabel;
        #endregion


        /// <summary>
        /// Forbidden constructor
        /// </summary>
        private PatchInstructionsDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="patch"></param>
        /// <param name="customLabel"></param>
        public PatchInstructionsDialog(PCH patch, string customLabel)
        {
            InitializeComponent();

            if (patch == null)
                throw new Exception(_ERROR_PATCH_MISSING);

            _CurrentPatch = patch;
            _CustomLabel = customLabel;
            _InitializeContents();
        }

        /// <summary>
        /// Defines window contents
        /// </summary>
        private void _InitializeContents()
        {
            // Instruction list
            foreach(PatchInstruction anotherInstruction in _CurrentPatch.PatchInstructions)
            {
                ListViewItem lvi = new ListViewItem(anotherInstruction.Order.ToString()) {Tag = anotherInstruction};

                lvi.SubItems.Add(anotherInstruction.Name);
                lvi.SubItems.Add(anotherInstruction.Group.name);
                lvi.SubItems.Add(anotherInstruction.Comment);
                lvi.SubItems.Add(anotherInstruction.Description);

                instructionListView.Items.Add(lvi);
            }

            // Custom label
            string message = _DEFAULT_LABEL;

            if (!string.IsNullOrEmpty(_CustomLabel))
                message = _CustomLabel;

            customLabel.Text = message;

            // File name label
            fileNameLabel.Text = _CurrentPatch.Name;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            // Click on OK button > return selected instructions
            List<PatchInstruction> selectedInstructions = new List<PatchInstruction>(); 

            foreach(ListViewItem anotherItem in instructionListView.Items)
            {
                if (anotherItem.Checked)
                    selectedInstructions.Add((PatchInstruction) anotherItem.Tag);
            }

            SelectedInstructions = selectedInstructions.ToArray();

            DialogResult = DialogResult.OK;
            Close();
        }

    }
}
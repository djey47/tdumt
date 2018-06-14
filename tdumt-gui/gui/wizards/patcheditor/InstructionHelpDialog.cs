using System.Windows.Forms;
using TDUModdingLibrary.support.patcher.instructions;

namespace TDUModdingTools.gui.wizards.patcheditor
{
    /// <summary>
    /// Help window
    /// </summary>
    public partial class InstructionHelpDialog : Form
    {
        #region Members
        /// <summary>
        /// Parent form
        /// </summary>
        private readonly Form _ParentForm;
        #endregion

        /// <summary>
        /// Main constructor
        /// </summary>
        public InstructionHelpDialog(Form parent)
        {
            InitializeComponent();

            _ParentForm = parent;
        }

        #region Events
        private void InstructionHelpDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Do not remove controls, just hide this window
            // BUG_72 : if owner closes, it must be also closed
            if (e.CloseReason != CloseReason.FormOwnerClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
        #endregion

        #region Private methods
        #endregion

        #region Public methods
        /// <summary>
        /// Displays help about specified instruction
        /// </summary>
        /// <param name="instruction"></param>
        public void ShowHelp(PatchInstruction instruction)
        {
            string nameLabel = "";
            string descriptionLabel = "";

            if (instruction != null)
            {
                nameLabel = instruction.Name;
                descriptionLabel = instruction.Description;
            }

            nameGroupBox.Text = nameLabel;
            instructionDescriptionHelpTextBox.Text = descriptionLabel;

            // Displays if needed
            if (!Visible)
                Show(_ParentForm);
        }
        #endregion
    }
}
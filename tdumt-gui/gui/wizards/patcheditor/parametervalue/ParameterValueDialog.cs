using System;
using System.Drawing;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using TDUModdingLibrary.support.patcher.parameters;

namespace TDUModdingTools.gui.wizards.patcheditor.parametervalue
{
    /// <summary>
    /// EVO_68 : new dialog box to enter parameter values, supporting variables
    /// </summary>
    public partial class ParameterValueDialog : PromptBox
    {
        #region Properties
        /// <summary>
        /// Returns or sets the current instruction parameter
        /// </summary>
        public PatchInstructionParameter CurrentParameter
        {
            get { return _CurrentParameter; }
        }
        private readonly PatchInstructionParameter _CurrentParameter;
        #endregion


        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <param name="param">Parameter info</param>
        /// <param name="paramValue">Initial value of parameter</param>
        public ParameterValueDialog(string message, PatchInstructionParameter param, string paramValue)
            : base(Application.ProductName, message, paramValue)
        {
            InitializeComponent();

            _CurrentParameter = param;

            // OK button is activated by Enter key
            OKOnEnterKey = true;

            // If current parameter has no value provider, corresponding link is disabled
            knownValuesLinkLabel.Enabled = (param.ValuesProvider != null);
        }

        #region Events
        private void insertVariableLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on 'Insert variable...' link
            // EVO_36 : options
            // Génération de menu contextuel
            try
            {
                // Creates a context menu then show it
                insertVariableLinkLabel.ContextMenuStrip = new VariableContextMenuFactory().CreateContextMenu(this, promptTextBox);
                insertVariableLinkLabel.ContextMenuStrip.Show(insertVariableLinkLabel.PointToScreen(new Point()));
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void knownValuesLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on 'Known values...' link
            // EVO_89
            try
            {
                if (_CurrentParameter != null && _CurrentParameter.ValuesProvider != null)
                {
                    Cursor = Cursors.WaitCursor;

                    ParameterKnownValuesDialog knownValuesDlg = new ParameterKnownValuesDialog(_CurrentParameter.ValuesProvider, promptTextBox, true);

                    knownValuesDlg.Show(this);

                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void insertTabLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on Insert Tab link
            int selStart = promptTextBox.SelectionStart;

            promptTextBox.Text = promptTextBox.Text.Insert(selStart, "\t");
            promptTextBox.SelectionStart = (selStart + 1);
        }
        #endregion
    }
}
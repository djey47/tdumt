using System.Windows.Forms;
using TDUModdingLibrary.support.patcher.parameters.providers;

namespace TDUModdingTools.gui.wizards.patcheditor.parametervalue
{
    // EVO_89 : new form to give a list of known values to make parameter input easier
    public partial class ParameterKnownValuesDialog : Form
    {
        #region Members
        /// <summary>
        /// Provider currently used to build known values list
        /// </summary>
        private readonly IValuesProvider currentProvider;

        /// <summary>
        /// Textbox to write selected value to
        /// </summary>
        private readonly TextBox textBox;

        /// <summary>
        /// Flag to specify if value must be appended or not
        /// </summary>
        private readonly bool isValueAppended;
        #endregion

        /// <summary>
        /// Forbidden constructor
        /// </summary>
        internal ParameterKnownValuesDialog() {}

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="provider">Provider for known values</param>
        /// <param name="targetInputBox">Text box to write selected value</param>
        /// <param name="isAppended">true to append selected value to textbox, false to replace existing value</param>
        public ParameterKnownValuesDialog(IValuesProvider provider, TextBox targetInputBox, bool isAppended)
        {
            InitializeComponent();

            currentProvider = provider;
            textBox = targetInputBox;
            isValueAppended = isAppended;
            _InitializeContents();
        }

        #region Private methods
        /// <summary>
        /// Defines window contents
        /// </summary>
        private void _InitializeContents()
        {
            // Values list
            valuesListBox.Items.Clear();

            foreach (string s in currentProvider.Values)
                valuesListBox.Items.Add(s);
        }
        #endregion

        #region Events
        private void valuesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // A value has been selected
            if (textBox != null && valuesListBox.SelectedItem != null)
            {
                string selectedValue = valuesListBox.SelectedItem.ToString();

                // Gets corresponding identifier
                if (isValueAppended)
                    textBox.Text += currentProvider.GetValueFromLabel(selectedValue);
                else
                    textBox.Text = currentProvider.GetValueFromLabel(selectedValue);

                Close();
            }
        }
        #endregion
    }
}
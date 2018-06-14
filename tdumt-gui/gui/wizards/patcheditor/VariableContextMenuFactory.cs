using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Tools;
using TDUModdingLibrary.support.patcher.vars;

namespace TDUModdingTools.gui.wizards.patcheditor
{
    class VariableContextMenuFactory : IContextMenuFactory
    {
        #region IContextMenuFactory Members

        /// <summary>
        /// Creates a context menu applied to a Form
        /// </summary>
        /// <param name="baseForm">Form instance</param>
        /// <param name="parameters">Parameters to use. [0] : textBox instance to write variable value to.</param>
        /// <returns>null if input parameters are invalid</returns>
        public ContextMenuStrip CreateContextMenu(Form baseForm, params object[] parameters)
        {
            if (parameters == null || parameters.Length != 1)
                return null;

            TextBox parameterTextBox = parameters[0] as TextBox;
            ContextMenuStrip newMenu = new ContextMenuStrip();

            if (parameterTextBox == null)
                return null;

            // Getting all variables
            string[] allVariables = Enum.GetNames(typeof (PatchVariable.VariableName));

            foreach (string anotherVariableName in allVariables)
            {
                ToolStripItem newItem = new ToolStripMenuItem(anotherVariableName);
                PatchVariable varInstance = PatchVariable.MakeVariable(anotherVariableName);

                if (varInstance != null)
                {
                    newItem.Click += delegate(object sender, EventArgs args)
                                         {
                                                 parameterTextBox.SelectedText = varInstance.Code;
                                                 parameterTextBox.Focus();
                                         };
                    newItem.ToolTipText = varInstance.Description;
                }

                newMenu.Items.Add(newItem);
            }

            return newMenu;
        }
        #endregion
    }
}

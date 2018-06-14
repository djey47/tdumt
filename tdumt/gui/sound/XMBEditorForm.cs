using System;
using System.Windows.Forms;
using TDUModdingTools.common;

namespace TDUModdingTools.gui.sound
{
    public partial class XMBEditorForm : Form
    {
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public XMBEditorForm()
        {
            InitializeComponent();
        }

        #region Evenements
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Sélection du fichier
            openFileDialog.Filter = GuiConstants.FILTER_XMB_WAV_FILES;
            openFileDialog.FilterIndex = 3;
            DialogResult dr = openFileDialog.ShowDialog(this);

            if (dr == DialogResult.OK)
            {

            }

        }
        #endregion
    }
}

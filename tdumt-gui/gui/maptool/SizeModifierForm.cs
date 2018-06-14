using System;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using TDUModdingLibrary.fileformats.banks;

namespace TDUModdingTools.gui.maptool
{
    public partial class SizeModifierForm : Form
    {
        #region Attributs
        /// <summary>
        /// L'entrée à visualiser/modifier
        /// </summary>
        private MAP.Entry theEntry;

        /// <summary>
        /// Le fichier à modifier, éventuellement
        /// </summary>
        private MAP theFile;
        #endregion

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="entryToModify"></param>
        /// <param name="fileToModify"></param>
        public SizeModifierForm(MAP.Entry entryToModify, MAP fileToModify)
        {
            theEntry = entryToModify;
            theFile = fileToModify;

            InitializeComponent();

            initializeContents();
        }

        #region Méthodes privées
        /// <summary>
        /// Initialise le contenu de la fenêtre
        /// </summary>
        private void initializeContents()
        {
            idTextbox.Text = theEntry.fileId.ToString();
            size1TextBox.Text = theEntry.firstSize.ToString();
            size2TextBox.Text = theEntry.secondSize.ToString();
        }
        #endregion

        #region Evénements
        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Vérifications
                uint size1 = uint.Parse(size1TextBox.Text);
                uint size2 = uint.Parse(size2TextBox.Text);
                if (theEntry.firstSize == size1 && theEntry.secondSize == size2)
                {
                    // Pas de modification
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                // Sauvegarde
                Cursor = Cursors.WaitCursor;
                theFile.UpdateEntrySizes(theEntry.fileId, size1, size2);
            
                // Fermeture
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }
        #endregion
    }
}
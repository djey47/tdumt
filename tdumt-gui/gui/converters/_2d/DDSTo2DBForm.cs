using System;
using System.IO;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Types;
using DjeFramework1.Common.Types.Forms;
using TDUModdingLibrary.converters;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.support;
using TDUModdingLibrary.support.constants;
using TDUModdingTools.common;

namespace TDUModdingTools.gui.converters._2d
{
    public partial class DDSTo2DBForm : Form
    {
        #region Constants
        /// <summary>
        /// Message de succès de conversion
        /// </summary>
        private const string _STATUS_SUCCESS = "{0} successfully converted to 2DB.";

        /// <summary>
        /// Message d'erreur si fichier d'origine requis mais non indiqué
        /// </summary>
        private const string _ERROR_NO_ORIGINAL_FILE = "Original 2DB file not specified.";

        /// <summary>
        /// Suffix used for new files
        /// </summary>
        private const string _SUFFIX_NEW_FILE = "_new.";
        #endregion

        /// <summary>
        /// Constructeur principal
        /// </summary>
        public DDSTo2DBForm()
        {
            InitializeComponent();

            // EVO 32 : StatusLog
            StatusBarLogManager.AddNewLog(this, toolStripStatusLabel);
        }

        #region Méthodes privées
        /// <summary>
        /// Met à jour le champ cible selon le nom de fichier indiqué.
        /// (si ce champ est vide)
        /// </summary>
        /// <param name="fileName">nom de fichier</param>
        private void _SetChampCible(string fileName)
        {
            string fileNameWithoutExt = File2.GetNameFromFilename(fileName);

            new2DBFilePath.Text = string.Concat(fileNameWithoutExt, _SUFFIX_NEW_FILE, LibraryConstants.EXTENSION_2DB_FILE);
        }
        #endregion

        #region Evénements
        private void GoButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                FileInfo originalFileInfo = null;

                if (!string.IsNullOrEmpty(original2DBFilePath.Text))
                    originalFileInfo = new FileInfo(original2DBFilePath.Text);

                // If file already exists, it is renamed
                string newFileName = new2DBFilePath.Text;

                Tools.RenameIfNecessary(newFileName, LibraryConstants.SUFFIX_OLD_FILE);
                
                // Texture name override
                string textureName = GraphicsConverters.KEEP_ORIGINAL_TEXTURE_NAME;

                // BUG_74
                if (overrideNameCheckBox.Checked && !string.IsNullOrEmpty(textureNameTextBox.Text))
                    textureName = textureNameTextBox.Text;

                // Mipmap count enforcement
                int mipmapCount = GraphicsConverters.KEEP_ORIGINAL_MIPMAP_COUNT;

                if (forceMipmapCheckBox.Checked)
                {
                    bool res = int.TryParse(mipMapTextBox.Text, out mipmapCount);

                    if (!res)
                        mipmapCount = GraphicsConverters.KEEP_ORIGINAL_MIPMAP_COUNT;
                }

                // Selon le mode...
                if (keepHeaderCheckbox.Checked)
                {
                    // Original header
                    if (originalFileInfo == null || !originalFileInfo.Exists)
                        throw new Exception(_ERROR_NO_ORIGINAL_FILE);

                    GraphicsConverters.DDSTo2DB(original2DBFilePath.Text, sourceDDSFilePath.Text, newFileName, textureName, mipmapCount);
                }
                else
                    // New 2DB file
                    GraphicsConverters.DDSTo2DB(sourceDDSFilePath.Text, newFileName, textureName, mipmapCount);

                // EVO_32
                FileInfo fi = new FileInfo(sourceDDSFilePath.Text);
                string message = string.Format(_STATUS_SUCCESS, fi.Name);

                StatusBarLogManager.ShowEvent(this, message);

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void browseOriginalButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = GuiConstants.FILTER_2DB_ALL_FILES;

            DialogResult dr = openFileDialog1.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                original2DBFilePath.Text = openFileDialog1.FileName;
            }
        }

        private void sourceBrowseButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = GuiConstants.FILTER_DDS_ALL_FILES;

            DialogResult dr = openFileDialog1.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                sourceDDSFilePath.Text = openFileDialog1.FileName;
                // Ajout du 16/02/2008 : on fixe une valeur par défaut dans le champ cible
                _SetChampCible(openFileDialog1.FileName);
            }
        }

        private void newBrowseButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = GuiConstants.FILTER_2DB_ALL_FILES;

            DialogResult dr = saveFileDialog1.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                new2DBFilePath.Text = saveFileDialog1.FileName;
            }
        }

        private void original2DBFilePath_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                try
                {
                    string[] listeFichiers = (string[])e.Data.GetData(DataFormats.FileDrop);

                    if (listeFichiers.Length == 1)
                    {
                        original2DBFilePath.Text = listeFichiers[0];
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void filePath_DragEnter(object sender, DragEventArgs e)
        {
            // Autorisé seulement si c'est un fichier
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void sourceDDSFilePath_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                try
                {
                    string[] listeFichiers = (string[])e.Data.GetData(DataFormats.FileDrop);

                    if (listeFichiers.Length == 1)
                    {
                        sourceDDSFilePath.Text = listeFichiers[0];
                        // Ajout du 17/02/2008 : on fixe une valeur par défaut dans le champ cible
                        _SetChampCible(listeFichiers[0]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }
        
        private void DDSTo2DBForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Nettoyage log
            StatusBarLogManager.RemoveLog(this);
        }

        private void keepHeaderCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            // Header type selection
            original2DBFilePath.Enabled 
                = originalBrowseButton.Enabled 
                = file3InfoButton.Enabled
                = keepHeaderCheckbox.Checked;
        }

        private void file1InfoButton_Click(object sender, EventArgs e)
        {
            // EVO_65: properties
            try
            {
                TduFile textureFile = TduFile.GetFile(sourceDDSFilePath.Text);

                if (textureFile.Exists)
                    ListView2.FillWithProperties(propertiesListView, textureFile.Properties);
            } 
            catch (Exception ex)
            {
                Exception2.PrintStackTrace(ex);
            }
        }

        private void file2InfoButton_Click(object sender, EventArgs e)
        {
            // EVO_65: properties
            try 
            {
                TduFile textureFile = TduFile.GetFile(new2DBFilePath.Text);

                if (textureFile.Exists)
                    ListView2.FillWithProperties(propertiesListView, textureFile.Properties);
            }
            catch (Exception ex)
            {
                Exception2.PrintStackTrace(ex);
            }
        }

        private void file3InfoButton_Click(object sender, EventArgs e)
        {
            // EVO_65: properties
            try 
            {
                TduFile textureFile = TduFile.GetFile(original2DBFilePath.Text);

                if (textureFile.Exists)
                    ListView2.FillWithProperties(propertiesListView, textureFile.Properties);
            } 
            catch (Exception ex)
            {
                Exception2.PrintStackTrace(ex);
            }
        }

        private void overrideNameCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // 'Override' checkbox checked/unchecked
            textureNameTextBox.Enabled = overrideNameCheckBox.Checked;
        }

        private void forceMipmapCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // 'Force mipmap count' checkbox checked/unchecked
            mipMapTextBox.Enabled = forceMipmapCheckBox.Checked;
        }
        #endregion
    }
}
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Types;
using DjeFramework1.Common.Types.Forms;
using TDUModdingLibrary.converters;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.support;
using TDUModdingLibrary.support.constants;
using TDUModdingTools.common;

namespace TDUModdingTools.gui.converters._2d
{
    public partial class _2DBToDDSForm : Form
    {
        #region Constantes
        /// <summary>
        /// Message de succès de conversion multiple
        /// </summary>
        private const string _STATUS_SUCCESS_MULTIPLE = "{0} 2DBs successfully converted to DDS.";

        /// <summary>
        /// Message de succès de conversion simple
        /// </summary>
        private const string _STATUS_SUCCESS_SINGLE = "{0} successfully converted to DDS.";

        /// <summary>
        /// Error message when trying to open a non-BNK file
        /// </summary>
        private const string _ERROR_INVALID_BNK_FILE = "Specified file is not a valid BNK.";

        /// <summary>
        /// Label for folder browsing dialog box
        /// </summary>
        private const string _LABEL_FOLDER_BROWSE = "Please select where to extract found textures:";
        #endregion

        #region Attributs
        /// <summary>
        /// Dernier emplacement cible sélectionné
        /// </summary>
        private string _LastTargetLocation;
        #endregion

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public _2DBToDDSForm()
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
            newDDSFilePath.Text = File2.ChangeExtensionOfFilename(fileName, LibraryConstants.EXTENSION_DDS_FILE);
        }

        /// <summary>
        /// Réalise la conversion multiple
        /// </summary>
        private void _MultipleConvert()
        {
            // Vérifications
            if (string.IsNullOrEmpty(sourceBNKTextBox.Text) || string.IsNullOrEmpty(targetFolderTextBox.Text))
                return;

            Cursor = Cursors.WaitCursor;

            // Parcours des fichiers et extraction des 2DB
            BNK bnkFile = TduFile.GetFile(sourceBNKTextBox.Text) as BNK;

            if (bnkFile == null)
                throw new Exception(_ERROR_INVALID_BNK_FILE);

            Collection<string> textureFiles = bnkFile.GetPackedFilesPathsByExtension(LibraryConstants.EXTENSION_2DB_FILE);
            string tempFolder = File2.SetTemporaryFolder(null, LibraryConstants.FOLDER_TEMP, true);

            foreach (string anotherFilePath in textureFiles)
            {
                // 1.Extraction depuis le BNK vers le dossier temporaire
                bnkFile.ExtractPackedFile(anotherFilePath, tempFolder, true);

                // 2.Conversion des fichiers 2DB>DDS
                string fileName = tempFolder + @"\" + bnkFile.GetPackedFileName(anotherFilePath);
                FileInfo fi = new FileInfo(fileName);
                string newFileName = targetFolderTextBox.Text + @"\" + File2.ChangeExtensionOfFilename(fi.Name, LibraryConstants.EXTENSION_DDS_FILE);

                // If file already exists, it is renamed
                Tools.RenameIfNecessary(newFileName, LibraryConstants.SUFFIX_OLD_FILE);
                
                GraphicsConverters._2DBToDDS(fileName, newFileName);
            }

            // EVO 32
            string message = string.Format(_STATUS_SUCCESS_MULTIPLE, textureFiles.Count);
            StatusBarLogManager.ShowEvent(this, message);

            // On efface le champ source
            sourceBNKTextBox.Text = "";

            // Mise à jour de l'emplacement des 2DB
            _2DBLocationLink.Text = tempFolder;

            Cursor = Cursors.Default;
        }

        /// <summary>
        /// Réalise la conversion simple
        /// </summary>
        private void _SingleConvert()
        {
            // Vérifications
            if (!string.IsNullOrEmpty(source2DBFilePath.Text) && !string.IsNullOrEmpty(newDDSFilePath.Text))
            {
                Cursor = Cursors.WaitCursor;

                // If file already exists, it is renamed
                Tools.RenameIfNecessary(newDDSFilePath.Text, LibraryConstants.SUFFIX_OLD_FILE);

                GraphicsConverters._2DBToDDS(source2DBFilePath.Text, newDDSFilePath.Text);

                Cursor = Cursors.Default;

                // EVO_32
                FileInfo fi = new FileInfo(source2DBFilePath.Text);
                string message = string.Format(_STATUS_SUCCESS_SINGLE, fi.Name);

                StatusBarLogManager.ShowEvent(this, message);
            }
        }
        #endregion

        #region Evénements
        private void _2DBToDDSForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Nettoyage log
            StatusBarLogManager.RemoveLog(this);
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            try
            {
                // EVO 12 : prise en charge de conversion multiple
                if (converterTabControl.SelectedIndex == 0)
                    // Mode simple
                    _SingleConvert();
                else
                    // Mode multiple
                    _MultipleConvert();
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void sourceBrowseButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = GuiConstants.FILTER_2DB_ALL_FILES;

            DialogResult dr = openFileDialog1.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                source2DBFilePath.Text = openFileDialog1.FileName;
                // Ajout du 16/02/2008 : on fixe une valeur par défaut dans le champ cible
                _SetChampCible(openFileDialog1.FileName);
            }
        }

        private void newBrowseButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = GuiConstants.FILTER_DDS_ALL_FILES;

            DialogResult dr = saveFileDialog1.ShowDialog(this);
            
            if (dr == DialogResult.OK)
                newDDSFilePath.Text = saveFileDialog1.FileName;
        }

        private void source2DBFilePath_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                try
                {
                    string[] listeFichiers = (string[])e.Data.GetData(DataFormats.FileDrop);

                    if (listeFichiers.Length == 1)
                    {
                        source2DBFilePath.Text = listeFichiers[0];
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

        private void source2DBFilePath_DragEnter(object sender, DragEventArgs e)
        {
            // Autorisé seulement si c'est un fichier
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        // ONGLET Multiple //
        private void sourceBNKBrowseButton_Click(object sender, EventArgs e)
        {
            // Clic sur le bouton de sélection du BNK

            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = GuiConstants.FILTER_BNK_ALL_FILES;

            DialogResult dr = openFileDialog1.ShowDialog(this);
            if (dr == DialogResult.OK)
                sourceBNKTextBox.Text = openFileDialog1.FileName;
        }

        private void sourceBNKTextBox_DragEnter(object sender, DragEventArgs e)
        {
            // Autorisé seulement si c'est un fichier
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void sourceBNKTextBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                try
                {
                    string[] listeFichiers = (string[])e.Data.GetData(DataFormats.FileDrop);

                    if (listeFichiers.Length == 1)
                        sourceBNKTextBox.Text = listeFichiers[0];
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void targetBrowseButton_Click(object sender, EventArgs e)
        {
            // Clic sur le bouton de sélection d'emplacement cible
            if (_LastTargetLocation != null)
                folderBrowserDialog.SelectedPath = _LastTargetLocation;

            folderBrowserDialog.Description = _LABEL_FOLDER_BROWSE;

            DialogResult dr = folderBrowserDialog.ShowDialog(this);
            
            if (dr == DialogResult.OK)
            {
                _LastTargetLocation = folderBrowserDialog.SelectedPath;
                targetFolderTextBox.Text = _LastTargetLocation;
            }
        }

        private void converterTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // On change d'onglet : on vide le message en barre d'état
            StatusBarLogManager.ShowEvent(this, "");
        }

        private void _2DBLocationLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on 2DB location link
            if (string.IsNullOrEmpty(_2DBLocationLink.Text))
                return;

            try
            {
                ProcessStartInfo explorerProcess = new ProcessStartInfo(_2DBLocationLink.Text);
                Process.Start(explorerProcess);
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void file1InfoButton_Click(object sender, EventArgs e)
        {
            // EVO_65: properties
            try
            {
                TduFile textureFile = TduFile.GetFile(source2DBFilePath.Text);

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
                TduFile textureFile = TduFile.GetFile(newDDSFilePath.Text);

                if (textureFile.Exists)
                    ListView2.FillWithProperties(propertiesListView, textureFile.Properties);
            }
            catch (Exception ex)
            {
                Exception2.PrintStackTrace(ex);
            }
        }

        private void fileInfo4Button_Click(object sender, EventArgs e)
        {
            // EVO_65: properties
            try
            {
                TduFile bnkFile = TduFile.GetFile(sourceBNKTextBox.Text);

                if (bnkFile.Exists)
                    ListView2.FillWithProperties(propertiesListView, bnkFile.Properties);
            }
            catch (Exception ex)
            {
                Exception2.PrintStackTrace(ex);
            }
        }
        #endregion
    }
}
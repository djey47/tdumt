using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.support;
using TDUModdingLibrary.support.constants;
using TDUModdingTools.common;
using TDUModdingTools.common.settings;

namespace TDUModdingTools.gui.settings
{
    public partial class SettingsForm : Form
    {
        #region Enums
        /// <summary>
        /// Tab pages for settings
        /// </summary>
        internal enum SettingsTabPage
        {
            Main = 0,
            FileEditing = 1,
            LaunchSettings = 2,
            PatchEditor = 3,
            TrackPack = 4
        };
        #endregion

        #region Constants
        /// <summary>
        /// Title of browse file dialog (before running TDU)
        /// </summary>
        private const string _TITLE_BROWSE_FILE_BEFORE = "Browse for a file to run before TDU...";
        
        /// <summary>
        /// Title of browse file dialog (after running TDU)
        /// </summary>
        private const string _TITLE_BROWSE_FILE_AFTER = "Browse for a file to run just after TDU starts...";

        /// <summary>
        /// Message dans la boite de sélection de dossier TDU
        /// </summary>
        private const string _DIALOG_TDU_FOLDER = "Please select TDU root folder\r\n(e.g. " + LibraryConstants.FOLDER_DEFAULT_INSTALL +")";

        /// <summary>
        /// Message in folder browsing dialog (new files)
        /// </summary>
        private const string _DIALOG_NEW_FILES_FOLDER = "Please select default folder for new files.";

        /// <summary>
        /// Elément permettant de créer une nouvelle config de lancement
        /// </summary>
        private const string _CONFIG_LIST_NEW_ITEM = "<New configuration...>";

        /// <summary>
        /// Message à faire apparaître lors de la saisie du nom de config
        /// </summary>
        private const string _CONFIG_NAME_MESSAGE = "Please enter a name for this configuration.";

        /// <summary>
        /// Message d'erreur lors de l'enregistrement des modifs config
        /// </summary>
        private const string _ERROR_LAUNCH_CONFIG_SAVE = "Unable to modify current launch configuration.";

        /// <summary>
        /// Message d'erreur lorsqu'une nom de config existe déjà
        /// </summary>
        private const string _ERROR_CONFIG_NAME_EXISTS = "A launch configuration already exists with the same name.";

        /// <summary>
        /// Message d'erreur lorsque le nom de config est une chaîne vide
        /// </summary>
        private const string _ERROR_INVALID_CONFIG_NAME = "Please enter a valid name for this configuration.";

        /// <summary>
        /// Error message when trying to fix Registry
        /// </summary>
        private const string _ERROR_REG_FIX_FAILED = "Unable to fix registry for TDU.";

        /// <summary>
        /// Error message when failed to fix Registry without rights
        /// </summary>
        private const string _ERROR_REG_FIX_FAILED_RIGHTS = "You don't have rights to fix registry for TDU.";

        /// <summary>
        /// Error message when trying to delete radial.cdb
        /// </summary>
        private const string _ERROR_RADIAL_FAILED = "Unable to delete radial.cdb file.\r\nIt might have deleted already.";

        /// <summary>
        /// Error message when profile information could not be found
        /// </summary>
        private const string _ERROR_PROFILE =
            "Unable to load profile information. Please launch TDU at least once to initialize it.";

        /// <summary>
        /// Message when Registry has been fixed
        /// </summary>
        private const string _INFO_REG_FIX_SUCCESS = "Fixing complete.";

        /// <summary>
        /// Message when radial.cdb has been deleted
        /// </summary>
        private const string _INFO_RADIAL_SUCCESS = "radial.cdb file succesfully cleared.";
        #endregion

        #region Attributs
        /// <summary>
        /// Liste de config de lancement
        /// </summary>
        private Collection<LaunchConfiguration> _TemporaryConfigList;
        #endregion

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="tabPage">Onglet à afficher</param>
        internal SettingsForm(SettingsTabPage tabPage)
        {
            InitializeComponent();
            _InitializeContents();

            // Affiche l'onglet spécifié
            int tabNum = (int) tabPage;

            tabControl.SelectedIndex = tabNum;
        }

        #region Méthodes privées
        /// <summary>
        /// Définit le contenu de la fenêtre
        /// </summary>
        private void _InitializeContents()
        {
            TduModdingToolsSettings currentSettings = Program.ApplicationSettings;

            // Contenus

                // Liste de langues
            foreach (string languageName in DBResource.LanguageList)
                languageList.Items.Add(languageName);

                // Liste de modules
            moduleList.Items.Add("");

            foreach (string moduleName in MainForm.StartupModuleList)
                moduleList.Items.Add(moduleName);

            // Affiche les valeurs stockées dans la configuration
                // Main
            rootTextBox.Text = currentSettings.TduMainFolder;
            moduleList.SelectedItem = currentSettings.StartupModule;
            languageList.SelectedItem = currentSettings.TduLanguage;
            // EVO_73: debug mode
            debugModeCheckBox.Checked = (bool.Parse(currentSettings.DebugModeEnabled));

                // File Editing
            // EVO_102 : default location for new files
            if (string.IsNullOrEmpty(currentSettings.DefaultEditNewFilesFolder))
            {
                editNewFilesFolderCurrentRadioButton.Checked = true;
                editNewFilesFolderCurrentRadioButton_CheckedChanged(this, new EventArgs());
            }
            else
            {
                editNewFilesFolderHardRadioButton.Checked = true;
                editNewFilesFolderHardRadioButton_CheckedChanged(this, new EventArgs());
                editFolderNewFilesTextBox.Text = currentSettings.DefaultEditNewFilesFolder;
            }
            // EVO_136: display files in explorer after extract
            editExtractShowFilesCheckBox.Checked = (bool.Parse(currentSettings.ExtractDisplayInExplorer));

                // Launch Configuration
            Collection<LaunchConfiguration> configList = currentSettings.GetLaunchConfigList();

            _TemporaryConfigList = new Collection<LaunchConfiguration>(configList);
            configComboBox.Items.Add(_CONFIG_LIST_NEW_ITEM);

            foreach (LaunchConfiguration anotherConfig in _TemporaryConfigList)
                configComboBox.Items.Add(anotherConfig.Name);

            if (configComboBox.Items.Count > 1)
                configComboBox.SelectedIndex = 1;
            else
                configComboBox.SelectedIndex = 0;

            // Patch Editor
            reportAutoScrollCheckBox.Checked = bool.Parse(currentSettings.PatchEditorReportAutoScroll);
                // EVO_110 : report clearing
            reportClearingCheckBox.Checked = bool.Parse(currentSettings.PatchEditorReportClear);

            // [EVO_172] Track Pack
            string[] profileNames = new string[0];
            
            // [BUG_103] Error when savegame folder not found
            try
            {
                profileNames = Tools.GetPlayerProfiles();
            } 
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, new Exception(_ERROR_PROFILE, ex));
            }
            //

            foreach (string anotherProfile in profileNames)
                profilesComboBox.Items.Add(anotherProfile);

            // Selected profile
            if (string.IsNullOrEmpty(currentSettings.PlayerProfile))
            {
                if (profilesComboBox.Items.Count > 0)
                    profilesComboBox.Text = profilesComboBox.Items[0].ToString();
            }
            else
                profilesComboBox.Text = currentSettings.PlayerProfile;
        }

        /// <summary>
        /// Modifie la configuration sélectionnée
        /// </summary>
        /// <param name="selected">Numéro de la configuration à modifier</param>
        private void _AlterLaunchConfiguration(int selected)
        {
            try
            {
                // Récupération de la configuration
                LaunchConfiguration currentConfig = _TemporaryConfigList[selected - 1];

                _UpdateConfiguration(ref currentConfig);
            }
            catch (Exception ex)
            {
                throw new Exception(_ERROR_LAUNCH_CONFIG_SAVE, ex);
            }
        }

        /// <summary>
        /// Updates specified launch configuration according to actual form data
        /// </summary>
        /// <param name="currentConfig">Launch configuration to update</param>
        private void _UpdateConfiguration(ref LaunchConfiguration currentConfig)
        {
            if (currentConfig != null)
            {
                currentConfig.CleanRadial = radialCheckBox.Checked;
                currentConfig.FpsDisplayed = fpsRadioButton.Checked;
                currentConfig.PosDisplayed = posRadioButton.Checked;
                currentConfig.WindowedMode = windowedRadioButton.Checked;
                currentConfig.PreviousCommand = runBeforeTextBox.Text;
                currentConfig.NextCommand = runAfterTextBox.Text;
            }
        }

        /// <summary>
        /// Defines launch form contents according to specified launch configuration if available
        /// </summary>
        /// <param name="currentConfig">Launch configuration to provide contents, or null to use default values</param>
        private void _InitializeLaunchFormContents(LaunchConfiguration currentConfig)
        {
            bool isDefault = (currentConfig == null);

            standardRadioButton.Checked = true;
            windowedRadioButton.Checked = (isDefault ? false : currentConfig.WindowedMode);
            fpsRadioButton.Checked = (isDefault ? false : currentConfig.FpsDisplayed);
            posRadioButton.Checked = (isDefault ? false : currentConfig.PosDisplayed);
            radialCheckBox.Checked = (isDefault ? false : currentConfig.CleanRadial);
            runBeforeTextBox.Text = (isDefault ? "" : currentConfig.PreviousCommand);
            runAfterTextBox.Text = (isDefault ? "" : currentConfig.NextCommand);
        }

        /// <summary>
        /// Ajoute une configuration de lancement
        /// </summary>
        private void _AddLaunchConfiguration()
        {
            // Nom de la configuration
            PromptBox pBox = new PromptBox(Application.ProductName, _CONFIG_NAME_MESSAGE, null);

            pBox.ShowDialog(this);

            if (pBox.DialogResult == DialogResult.OK)
            {
                // Nouvelle config
                LaunchConfiguration newConf = new LaunchConfiguration();

                // Nettoyage du nom pour éviter les conflits avec le sérialiseur
                string name = pBox.ReturnValue;

                if (!string.IsNullOrEmpty(name))
                    name = name.Trim();

                // Une chaîne vide n'est pas autorisée
                if (string.IsNullOrEmpty(name))
                    throw new Exception(_ERROR_INVALID_CONFIG_NAME);

                name = name.Replace('|', '!');
                name = name.Replace('¤', '*');

                // Vérification de l'existence d'une config de même nom
                foreach (LaunchConfiguration anotherConf in _TemporaryConfigList)
                {
                    if (anotherConf.Name.Equals(name))
                        throw new Exception(_ERROR_CONFIG_NAME_EXISTS);
                }

                newConf.Name = name;
                _UpdateConfiguration(ref newConf);
                
                // Ajout à la liste IHM
                configComboBox.Items.Add(newConf.Name);

                // Ajout à la config
                _TemporaryConfigList.Add(newConf);

                // Sélection de la config ajoutée
                configComboBox.SelectedIndex = configComboBox.Items.Count - 1;
            }
        }

        /// <summary>
        /// Supprime la configuration de lancement sélectionnée
        /// </summary>
        /// <param name="selected"></param>
        private void _RemoveLaunchConfig(int selected)
        {
            // Suppression IHM
            configComboBox.Items.RemoveAt(selected);

            // Suppression config
            _TemporaryConfigList.RemoveAt(selected - 1);

            // Sélection config par défaut
            configComboBox.SelectedIndex = 1;
        }

        /// <summary>
        /// Permits to select a file to run before ou after TDU starts. Updates specified TextBox.
        /// </summary>
        /// <param name="textBox">TextBox to set contents</param>
        /// <param name="isForBefore">true to browse for a 'before' command, false for 'after' </param>
        private void _SelectFileToRun(TextBox textBox, bool isForBefore)
        {
            if (textBox != null)
            {
                openFileDialog.Title = isForBefore ? _TITLE_BROWSE_FILE_BEFORE : _TITLE_BROWSE_FILE_AFTER;
                openFileDialog.Filter = GuiConstants.FILTER_ALL_FILES;
                openFileDialog.FileName = "";

                DialogResult dr = openFileDialog.ShowDialog(this);

                if (dr == DialogResult.OK)
                    textBox.Text = openFileDialog.FileName;
            }
        }
        #endregion

        #region Events - Common
        private void OKButton_Click(object sender, EventArgs e)
        {
            TduModdingToolsSettings currentSettings = Program.ApplicationSettings;

            try
            {
                // Sauvegarde des données
                Cursor = Cursors.WaitCursor;

                    // Main
                currentSettings.TduMainFolder = rootTextBox.Text;
                currentSettings.StartupModule = (moduleList.SelectedItem == null ? "" : moduleList.SelectedItem.ToString());
                currentSettings.TduLanguage = (languageList.SelectedItem == null ? "" : languageList.SelectedItem.ToString());
                // EVO_73: debug mode
                currentSettings.DebugModeEnabled = (debugModeCheckBox.Checked.ToString());

                    // File Editing
                // EVO_102
                if (editNewFilesFolderCurrentRadioButton.Checked)
                    currentSettings.DefaultEditNewFilesFolder = "";
                else 
                    currentSettings.DefaultEditNewFilesFolder = editFolderNewFilesTextBox.Text;
                // EVO_136: display files in explorer after extract
                currentSettings.ExtractDisplayInExplorer = (editExtractShowFilesCheckBox.Checked.ToString());

                    // Launch Configuration
                // Ensures current configuration has been saved
                saveConfigButton_Click(this, new EventArgs());
                currentSettings.TduLaunchConfigurations = (LaunchConfigurationConverter.ConvertToString(_TemporaryConfigList));

                    // Patch Editor
                currentSettings.PatchEditorReportAutoScroll = (reportAutoScrollCheckBox.Checked.ToString());
                        // EVO_110
                currentSettings.PatchEditorReportClear = (reportClearingCheckBox.Checked.ToString());

                    // [EVO_172] Track Pack
                currentSettings.PlayerProfile = profilesComboBox.Text;

                currentSettings.Save();

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }

            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Program.ApplicationSettings.Cancel();
            Close();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Demande de fermeture de la fenêtre
            Program.ApplicationSettings.Cancel();
        }
        #endregion

        #region Events - Main tab
        private void rootBrowseButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = rootTextBox.Text;
            folderBrowserDialog.Description = _DIALOG_TDU_FOLDER; 

            DialogResult dr =
                folderBrowserDialog.ShowDialog(this);

            if (dr == DialogResult.OK)
                rootTextBox.Text = folderBrowserDialog.SelectedPath;
        }
        
        private void fixRegistryButton_Click(object sender, EventArgs e)
        {
            // Click on 'Fix registry' button (main tab)
            try
            {
                Tools.FixRegistry(rootTextBox.Text);
                MessageBoxes.ShowInfo(this, _INFO_REG_FIX_SUCCESS);
            }
            catch (UnauthorizedAccessException uaEx)
            {
                MessageBoxes.ShowError(this, new Exception(_ERROR_REG_FIX_FAILED_RIGHTS, uaEx));
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, new Exception(_ERROR_REG_FIX_FAILED, ex));
            }
        }

        private void clearRadialButton_Click(object sender, EventArgs e)
        {
            // Click on 'Clear radial' button (main tab)
            try
            {
                Tools.DeleteRadial();
                MessageBoxes.ShowInfo(this, _INFO_RADIAL_SUCCESS);
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, new Exception(_ERROR_RADIAL_FAILED, ex));
            }
        }
        #endregion

        #region Events - Launcher tab
        private void configComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Sélection d'une autre configuration dans la liste : mise à jour des paramètres
            int selectedConfig = configComboBox.SelectedIndex;

            if (selectedConfig != -1)
            {
                if (selectedConfig == 0)
                    // Ajout de config
                    _InitializeLaunchFormContents(null);
                else
                {
                    // Récupération de la configuration
                    selectedConfig -= 1;

                    LaunchConfiguration currentConfig = _TemporaryConfigList[selectedConfig];

                    if (currentConfig != null)
                        _InitializeLaunchFormContents(currentConfig);
                }
            }
        }

        private void removeConfigButton_Click(object sender, EventArgs e)
        {
            // Clic sur le bouton de suppression

            int selected = configComboBox.SelectedIndex;

            if (selected > 1)
            {
                try
                {
                    _RemoveLaunchConfig(selected);
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void saveConfigButton_Click(object sender, EventArgs e)
        {
            // Clic sur le bouton d'enregistrement

            // En modif ou en ajout ?
            int selected = configComboBox.SelectedIndex;

            if (selected != -1)
            {
                try
                {
                    if (selected == 0)
                        // Ajout
                        _AddLaunchConfiguration();
                    else
                        // Modification
                        _AlterLaunchConfiguration(selected);
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }
            
        private void runBeforeBrowseButton_Click(object sender, EventArgs e)
        {
            // Click on browse button (run before)
            _SelectFileToRun(runBeforeTextBox, true);
        }

        private void runAfterBrowseButton_Click(object sender, EventArgs e)
        {
            // Click on browse button (run after)
            _SelectFileToRun(runAfterTextBox, false);
        }
        #endregion

        #region Events - File Editing tab
        private void editFolderNewFilesBrowseButton_Click(object sender, EventArgs e)
        {
            // Click on browse button
            folderBrowserDialog.SelectedPath = editFolderNewFilesTextBox.Text;
            folderBrowserDialog.Description = _DIALOG_NEW_FILES_FOLDER;

            DialogResult dr =
                folderBrowserDialog.ShowDialog(this);

            if (dr == DialogResult.OK)
                editFolderNewFilesTextBox.Text = folderBrowserDialog.SelectedPath;
        }

        private void editNewFilesFolderCurrentRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Click on current radio button
            // Disabling lower fields
            if (editNewFilesFolderCurrentRadioButton.Checked)
                editFolderNewFilesTextBox.Enabled = editFolderNewFilesBrowseButton.Enabled = false;
        }

        private void editNewFilesFolderHardRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Click on specify radio button
            // Enabling lower fields
            if (editNewFilesFolderHardRadioButton.Checked)
            {
                editFolderNewFilesTextBox.Enabled = editFolderNewFilesBrowseButton.Enabled = true;

                // Focus on text box
                editFolderNewFilesTextBox.Focus();
            }
        }
        #endregion
    }
}
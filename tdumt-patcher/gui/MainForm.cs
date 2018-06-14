using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Common.Support.Traces.Appenders;
using DjeFramework1.Common.Types;
using TDUModAndPlay.common;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.fileformats.specific;
using TDUModdingLibrary.installer;
using TDUModdingLibrary.support;
using TDUModdingLibrary.support.constants;
using TDUModdingLibrary.support.patcher;

namespace TDUModAndPlay.gui
{
    /// <summary>
    /// ModAndPlay! Application's main form
    /// </summary>
    public partial class MainForm : Form
    {
        #region Constants
        /// <summary>
        /// Message format for contrib label
        /// </summary>
        private const string _FORMAT_LABEL_CONTRIBUTOR = "{0} - {1}";

        /// <summary>
        /// Message format for project label
        /// </summary>
        private const string _FORMAT_LABEL_PROJECT = "{0} {1}";

        /// <summary>
        /// Message format for version label
        /// </summary>
        private const string _FORMAT_LABEL_VERSION = "V{0}";
        
        /// <summary>
        /// Message in the TDU folder selection dialog
        /// </summary>
        private const string _TITLE_DIALOG_TDU_FOLDER = "Please select folder where TDU is installed\r\n(e.g. " + LibraryConstants.FOLDER_DEFAULT_INSTALL + ")";

        /// <summary>
        /// Error message when default patch file is unavailable
        /// </summary>
        private const string _ERROR_PATCH_FILE_NOT_FOUND = "Patch file '" + LibraryConstants.FILE_PATCH_INSTALL + "' not found!";

        /// <summary>
        /// Error message when patch file is invalid
        /// </summary>
        private const string _ERROR_INVALID_PATCH_FILE = "Provided patch file is invalid.\r\nPlease contact author of this mod.";

        /// <summary>
        /// Error message when trying to install a mod over a megapack slot, whereas megapack has not been bought
        /// </summary>
        private const string _ERROR_MOD_NOT_ALLOWED =
            "Current mod can't be installed with ModAndPlay! as you don't have TDU Megapack.\r\nPlease install it manually.";

        /// <summary>
        /// 
        /// </summary>
        private const string _ERROR_ANOTHER_MOD_INSTALLED =
            "'{0}' mod is installed on the same slot. Please remove it first, by using its own auto-installer/uninstaller.";

        /// <summary>
        /// Warning message when uninstall patch file can't be used
        /// </summary>
        private const string _WARN_NO_UNINSTALL = "Uninstall patch not found or invalid. Uninstall has been disabled";

        /// <summary>
        /// Message when install patch was applied without probs
        /// </summary>
        private const string _MESSAGE_PATCH_SUCCESS = "Mod succesfully installed. Enjoy!";

        /// <summary>
        /// Message when uninstall patch was applied without probs
        /// </summary>
        private const string _MESSAGE_PATCH_UNINSTALL_SUCCESS = "Mod succesfully uninstalled!";

        /// <summary>
        /// Message when patch has already been applied before (or previous install attempt failed)
        /// </summary>
        private const string _MESSAGE_ALREADY_INSTALLED = "This mod has already been installed.";

        /// <summary>
        /// Message when current mod is not active
        /// </summary>
        private const string _MESSAGE_NOT_INSTALLED = "This mod is not currently installed.";
        #endregion

        #region Membres
        /// <summary>
        /// Install patch to be applied
        /// </summary>
        private PCH _CurrentInstallPatch;

        /// <summary>
        /// Uninstall patch to be applied
        /// </summary>
        private PCH _CurrentUninstallPatch;
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            _InitializeContents();
        }

        #region Private methods
        /// <summary>
        /// Defines window contents
        /// </summary>
        private void _InitializeContents()
        {
            // Background image, is specified (640x360)
            string pictureFile = Application.StartupPath + LibraryConstants.FILE_MNP_BACKGROUND_PICTURE;

            if (File.Exists(pictureFile))
                mainPictureBox.ImageLocation = pictureFile;
            else
                mainPictureBox.Image = Properties.Resources.tduzonda;

            // TDU install location
            string tduPath = Tools.TduPath;

            if (string.IsNullOrEmpty(tduPath))
                tduPath = LibraryConstants.FOLDER_DEFAULT_INSTALL;

            // Language list
            // EVO_147 removed
            /*foreach (string anotherLanguage in DBResource.LanguageList)
                gameCultureComboBox.Items.Add(anotherLanguage);

            gameCultureComboBox.Text = DBResource.LanguageList[DBResource.LanguageList.Length - 1];*/

            // TDU path
            tduPathTextbox.Text = tduPath;

            // Patch file
            try
            {
                // Install and uninstall are loaded by default
                string installPatchFile =
                    string.Concat(Application.StartupPath, LibraryConstants.FOLDER_PATCHS, LibraryConstants.FILE_PATCH_INSTALL);
                string uninstallPatchFile =
                    string.Concat(Application.StartupPath, LibraryConstants.FOLDER_PATCHS, LibraryConstants.FILE_PATCH_UNINSTALL);

                _LoadData(installPatchFile, uninstallPatchFile);

                // Form title update
                Text += _CurrentInstallPatch.Name;
            }
            catch (FileNotFoundException fnfEx)
            {
                // Critical error
                throw new Exception(_ERROR_PATCH_FILE_NOT_FOUND, fnfEx);
            }

            // Version label
            versionLabel.Text = string.Format(_FORMAT_LABEL_VERSION, Application.ProductVersion);

            // Contextual controls
            _UpdateContextualControls();

            // EVO_131 : Credits
            int roleIndex = 0;

            foreach (KeyValuePair<string, string> roleInformation in _CurrentInstallPatch.Roles)
            {
                Label roleLabel = null;
                Label authorLabel = null;

                switch(roleIndex)
                {
                    case 0:
                        roleLabel = role1Label;
                        authorLabel = author1Label;
                        break;
                    case 1:
                        roleLabel = role2Label;
                        authorLabel = author2Label;
                        break;
                    case 2:
                        roleLabel = role3Label;
                        authorLabel = author3Label;
                        break;
                    case 3:
                        roleLabel = role4Label;
                        authorLabel = author4Label;
                        break;
                    case 4:
                        roleLabel = role5Label;
                        authorLabel = author5Label;
                        break;
                    case 5:
                        roleLabel = role6Label;
                        authorLabel = author6Label;
                        break;
                    case 6:
                        // If role name not changed, information does not appear
                        if (PCH.ROLE_CUSTOM1.Equals(roleInformation.Key))
                            role7Label.Visible = author7Label.Visible = false;

                        roleLabel = role7Label;
                        authorLabel = author7Label;
                        break;
                    case 7:
                        // If role name not changed, information does not appear
                        if (PCH.ROLE_CUSTOM2.Equals(roleInformation.Key))
                            role8Label.Visible = author8Label.Visible = false;

                        roleLabel = role8Label;
                        authorLabel = author8Label;
                        break;
                }

                if (roleLabel != null && authorLabel != null)
                {
                    roleLabel.Text = roleInformation.Key;
                    authorLabel.Text = roleInformation.Value;
                }

                roleIndex++;
            }
        }

        /// <summary>
        /// Redefines contextual controls
        /// </summary>
        private void _UpdateContextualControls()
        {
            // Info label (bottom)
            string info;

            // Warning if this mod is already installed
            bool isInstalled = InstallHelper.IsModInstalled(_CurrentInstallPatch.SlotRef, _CurrentInstallPatch.Name);

            if (isInstalled)
                info = _MESSAGE_ALREADY_INSTALLED;
            else
                info = _MESSAGE_NOT_INSTALLED;

            infoLabel.Text = info;

            // Uninstall button
            bool isUninstallAvailable = false;

            if (_CurrentUninstallPatch != null && isInstalled)
                    isUninstallAvailable = true;

            uninstallButton.Enabled = isUninstallAvailable;
        }

        /// <summary>
        /// Loads patch file data from specified file
        /// </summary>
        /// <param name="installPatchFile">Install patch file to load</param>
        /// <param name="uninstallPatchFile">Uninstall patch file to load (optional - can be null)</param>
        private void _LoadData(string installPatchFile, string uninstallPatchFile)
        {
            // Install file loading
            PCH currentPatch = TduFile.GetFile(installPatchFile) as PCH;

            if (currentPatch == null)
                throw new Exception(_ERROR_INVALID_PATCH_FILE);
            if (!currentPatch.Exists)
                throw new FileNotFoundException("", installPatchFile);

            _CurrentInstallPatch = currentPatch;

            // Uninstall file loading
            currentPatch = null;

            if (!string.IsNullOrEmpty(uninstallPatchFile))
                currentPatch = TduFile.GetFile(uninstallPatchFile) as PCH;

            if (currentPatch == null || !currentPatch.Exists)
                Log.Info(_WARN_NO_UNINSTALL);
            else
                _CurrentUninstallPatch = currentPatch;

            // Patch info
            // Name - version
            if (string.IsNullOrEmpty(_CurrentInstallPatch.Version))
                titleLabel.Text = _CurrentInstallPatch.Name;
            else
                titleLabel.Text = string.Format(_FORMAT_LABEL_PROJECT, _CurrentInstallPatch.Name, _CurrentInstallPatch.Version);

            // Author - date
            if (string.IsNullOrEmpty(_CurrentInstallPatch.Date))
                contribLabel.Text = _CurrentInstallPatch.Author;
            else
                contribLabel.Text = string.Format(_FORMAT_LABEL_CONTRIBUTOR, _CurrentInstallPatch.Author, _CurrentInstallPatch.Date);
         
            // EVO_142 : Free label
            freeLabel.Text = _CurrentInstallPatch.Free;
        }

        /// <summary>
        /// Manages component install/uninstall
        /// </summary>
        /// <param name="isInstall">true if install / false if uninstall</param>
        private void _Process(bool isInstall)
        {
            // Current culture
            //string selectedCulture = gameCultureComboBox.Text.Substring(0, 2);
            //DB.Culture currentCulture = (DB.Culture) Enum.Parse(typeof (DB.Culture), selectedCulture);
            // EVO_147
            const DB.Culture currentCulture = DB.Culture.US;

            // Selects right patch according to install/uninstall case
            PCH currentPatch = (isInstall ? _CurrentInstallPatch : _CurrentUninstallPatch);

            // TDU folder (override)
            Tools.TduPath = tduPathTextbox.Text;

            // Authorization checks
            // If specified ref matches a car pack vehicle, TDU version must be '1.66+megapack'
            if (Tools.InstalledTduVersion != Tools.TduVersion.V1_66_Megapack)
            {
                // Loading reference
                try
                {
                    VehicleSlotsHelper.InitReference(Application.StartupPath + LibraryConstants.FOLDER_XML);

                    // Getting vehicle information...
                    if (!Tools.KEY_MISC_SLOT.Equals(_CurrentInstallPatch.SlotRef)
                        && VehicleSlotsHelper.VehicleInformation.ContainsKey(currentPatch.SlotRef))
                    {
                        VehicleSlotsHelper.VehicleInfo info =
                            VehicleSlotsHelper.VehicleInformation[currentPatch.SlotRef];

                        if (info.isAddon)
                        {
                            MessageBoxes.ShowWarning(this, _ERROR_MOD_NOT_ALLOWED);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                    return;
                }
            }

            // If another mod is installed on current slot...
            // Exception for Patch 1.67d
            if (isInstall
                && InstallHelper.IsAnotherModInstalled(currentPatch.SlotRef, currentPatch.Name)
                && !AppConstants.SLOT_COMMUNITY_PATCH.Equals(currentPatch.SlotRef))
            {
                MessageBoxes.ShowWarning(this, string.Format(_ERROR_ANOTHER_MOD_INSTALLED, InstallHelper.GetInstalledModName(currentPatch.SlotRef)));
                return;
            }

            // EVO_134 Group handling, if necessary
            List<string> chosenInstallGroups = new List<string> {PCH.REQUIRED_GROUP_NAME};

            if (isInstall && currentPatch.Groups.Count > 1)
            {
                GroupsDialog dlg = new GroupsDialog(_CurrentInstallPatch);
                DialogResult dr = dlg.ShowDialog(this);

                if (dr == DialogResult.OK)
                    chosenInstallGroups = dlg.ChosenGroups;
                else
                    return;
            }

            // Patch logger init
            string logFile = Application.StartupPath + LibraryConstants.FILE_LOG_PATCH;
            Log patchLog = new Log(logFile);

            patchLog.Appenders.Add(new ConsoleAppender());

            // EVO_100: progress bar init
            progressPanel.Visible = true;
            infoLabel.Visible = false;

            mainProgressBar.Minimum = 0;
            mainProgressBar.Maximum = currentPatch.PatchInstructions.Count;
            mainProgressBar.Step = 1;
            mainProgressBar.Value = 0;
            PatchHelper.ProgressBar = mainProgressBar;

            // Using new helper
            try
            {
                InstallHelper.RunAll(currentPatch, patchLog, currentCulture, isInstall, chosenInstallGroups);
                Application.DoEvents();

                // Showing gui messages
                foreach (string message in PatchHelper.Messages)
                    MessageBoxes.ShowWarning(this, message);

                // OK
                MessageBoxes.ShowInfo(this, isInstall ? _MESSAGE_PATCH_SUCCESS : _MESSAGE_PATCH_UNINSTALL_SUCCESS);

                // Refreshing window contents
                //_LoadData(_CurrentInstallPatch.FileName, (_CurrentUninstallPatch == null) ? null : _CurrentUninstallPatch.FileName);

                // Contextual information has to be updated
                _UpdateContextualControls();
            }
            catch (Exception ex)
            {
                // Showing messages first
                foreach (string message in PatchHelper.Messages)
                    MessageBoxes.ShowWarning(this, message);

                if (PatchHelper.Messages.Count == 0)
                    MessageBoxes.ShowError(this, ex);
            }
            finally
            {
                // Hiding progress bar
                progressPanel.Visible = false;
            }
        }
        #endregion

        #region Events
        private void parcourirLnk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on 'browse' link
            folderBrowserDialog.Description = _TITLE_DIALOG_TDU_FOLDER;

            DialogResult dr = folderBrowserDialog.ShowDialog(this);

            if (dr == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;

                try
                {
                    tduPathTextbox.Text = folderBrowserDialog.SelectedPath;

                    // TDU path update
                    Tools.TduPath = tduPathTextbox.Text;

                    // Window update : buttons and info label
                    _UpdateContextualControls();
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void installerBtn_Click(object sender, EventArgs e)
        {
            // Click on right arrow button > Install
            if (!string.IsNullOrEmpty(tduPathTextbox.Text))
            {
                Cursor = Cursors.WaitCursor;

                try
                {
                    _Process(true);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Main window is closed > cleaning...
            try
            {
                File2.RemoveTemporaryFolder(null, LibraryConstants.FOLDER_TEMP);
            }
            catch (Exception)
            {
                Log.Info("Unable to remove temporary folder - does not matter.");
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            // Click on 'ok' button
            homePanel.Visible = false;
            infoLabel.Visible = true;
            homeButtonPanel.Visible = false;
            installButtonPanel.Visible = true;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            // Click on left arrow button > Uninstall
            if (!string.IsNullOrEmpty(tduPathTextbox.Text))
            {
                Cursor = Cursors.WaitCursor;

                try
                {
                    _Process(false);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }
        #endregion

        private void mainPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            // Click on picture : displays web site if any
            if (_CurrentInstallPatch != null && !string.IsNullOrEmpty(_CurrentInstallPatch.InfoURL))
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    // Hides picture
                    mainPictureBox.Visible = false;

                    // Sets browser
                    webBrowser.Url = new Uri(_CurrentInstallPatch.InfoURL);
                    webBrowser.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }
    }
}
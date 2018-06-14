using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.support;
using TDUModdingLibrary.support.constants;
using TDUModdingLibrary.support.editing;
using TDUModdingTools.common;
using TDUModdingTools.common.settings;
using TDUModdingTools.gui.converters._2d;
using TDUModdingTools.gui.dbeditor;
using TDUModdingTools.gui.filebrowser;
using TDUModdingTools.gui.launcher;
using TDUModdingTools.gui.settings;
using TDUModdingTools.gui.sound;
using TDUModdingTools.gui.wizards.patcheditor;
using TDUModdingTools.gui.wizards.trackpack;
using TDUModdingTools.gui.wizards.vehiclemanager;
using TDUModdingTools.Properties;

namespace TDUModdingTools.gui
{
    /// <summary>
    /// Formulaire principal de l'appli - control panel
    /// </summary>
    public partial class MainForm : Form
    {
        #region Constantes
        /// <summary>
        /// Types de modules
        /// </summary>
        public enum ModuleType { 
            DBResourcesEditor,
            DBEditor,
            DDSTo2DBConverter,
            FileBrowser,
            PatchEditor,
            TrackPack,
            VehicleManager,
            x2DBToDDSConverter,
            XMBEditor,
            None
        };

        /// <summary>
        /// Nom de la configuration par défaut
        /// </summary>
        private const string _LAUNCH_CONFIG_DEFAULT_NAME = "Default";

        /// <summary>
        /// Le lancement de la commande pre-TDU a échoué
        /// </summary>
        private const string _ERROR_EXECUTE_PREV_COMMAND = "Pre-TDU command couldn't be executed.";

        /// <summary>
        /// Le lancement de la commande post-TDU a échoué
        /// </summary>
        private const string _ERROR_EXECUTE_NEXT_COMMAND = "Post-TDU command couldn't be executed.";

        /// <summary>
        /// Le nettoyage du radial a échoué
        /// </summary>
        private const string _ERROR_RADIAL = "An error occurred when deleting radial.cdb file.";

        /// <summary>
        /// Le lancement de la commande TDU a échoué
        /// </summary>
        private const string _ERROR_EXECUTE_TDU_PROCESS = "Test Drive Unlimited executable couldn't be launched. Please check your settings.";

        /// <summary>
        /// Message when launch configuration is missing
        /// </summary>
        private const string _ERROR_NO_LAUNCH_CONFIG = "No launch configuration was specified.";

        /// <summary>
        /// Message when a critical error prevents a module to start
        /// </summary>
        private const string _ERROR_NEW_MODULE = "Unable to load module: {0}.";

        /// <summary>
        /// Titre de la boîte de dialogue "a propos de..."
        /// </summary>
        private const string _TITLE_ABOUT_BOX = "About...";

        /// <summary>
        /// Titre de la fenêtre principale
        /// </summary>
        private const string _TITLE_MAIN_FORM = "TDU Modding Tools";

        /// <summary>
        /// Bribe added to main form title when debug is enabled
        /// </summary>
        private const string _TITLE_BRIBE_DEBUG = " (Debug Mode !)";

        /// <summary>
        /// 1.45 version
        /// </summary>
        private const string _TEXT_VERSION_GENUINE = "1.45 (genuine)";

        /// <summary>
        /// 1.66 version
        /// </summary>
        private const string _TEXT_VERSION_PATCH = "1.66 (Jul.07 patch)";

        /// <summary>
        /// Megapack version
        /// </summary>
        private const string _TEXT_VERSION_MEGAPACK = "1.66 Megapack (Apr.08)";

        /// <summary>
        /// Unknown version
        /// </summary>
        private const string _TEXT_VERSION_UNKNOWN = "not available";

        /// <summary>
        /// Additional information for about dialog box
        /// </summary>
        private const string _FORMAT_ABOUT_TDU_VERSION = "Your TDU version: {0}.";
        #endregion

        #region Properties
        /// <summary>
        /// Instance de la fenêtre principale
        /// </summary>
        public static MainForm Instance
        {
            get { return _Instance; }
        }
        private static MainForm _Instance;

        /// <summary>
        /// Indicateur de vue basique / avancée
        /// </summary>
        public bool IsBasicView
        {
            get { return _isBasicView; }
        }
        private bool _isBasicView = true;

        /// <summary>
        /// Liste de modules utilisables pour le démarrage auto
        /// </summary>
        public static Collection<string> StartupModuleList
        {
            get { return _StartupModuleList; }
        }
        private static readonly Collection<string> _StartupModuleList;

        /// <summary>
        /// Resharper preconization
        /// </summary>
        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
        #endregion
        
        #region Cibles pour invocations à distance
        /// <summary>
        /// Lancement de TDU
        /// </summary>
        public EventHandler TduLaunchTarget
        {
            get { return _LaunchTDU; }
        }

        /// <summary>
        /// Affichage des paramètres de lancement TDU
        /// </summary>
        public EventHandler LaunchSettingsTarget
        {
            get { return _DisplayLaunchSettings; }
        }
        #endregion

        #region Attributs privés
        #endregion

        /// <summary>
        /// Static fields initializer
        /// </summary>
        static MainForm()
        {
            // Startup module list
            _StartupModuleList = new Collection<string>
                                     {
                                         ModuleType.DBEditor.ToString(),
                                         ModuleType.DBResourcesEditor.ToString(),
                                         ModuleType.FileBrowser.ToString(),
                                         ModuleType.PatchEditor.ToString(),
                                         ModuleType.TrackPack.ToString(),
                                         ModuleType.VehicleManager.ToString()
                                     };
        }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            _Instance = this;

            // Titre
            Text = _TITLE_MAIN_FORM;

            // EVO_73: Debug mode notify
            if (bool.Parse(Program.ApplicationSettings.DebugModeEnabled))
                Text += _TITLE_BRIBE_DEBUG;

            // EVO_40 : lancement automatique d'un module
            try
            {
                string moduleName = Program.ApplicationSettings.StartupModule;

                if (StartupModuleList.Contains(moduleName))
                {
                    ModuleType moduleType = (ModuleType)Enum.Parse(typeof(ModuleType), moduleName);

                    LoadTool(moduleType, null);
                }
            }
            catch (Exception ex)
            {
                // Exception silencieuse
                Exception2.PrintStackTrace(new Exception("Unable to launch startup module.", ex));
            }

            // EVO_36 : initialisation des configurations de lancement
            string launchConfigurationsValue = Program.ApplicationSettings.TduLaunchConfigurations;

            if (string.IsNullOrEmpty(launchConfigurationsValue))
            {
                Collection<LaunchConfiguration> launchConfigs = new Collection<LaunchConfiguration>();
                LaunchConfiguration launchConfig = new LaunchConfiguration
                                                       {
                                                           Default = true,
                                                           Name = _LAUNCH_CONFIG_DEFAULT_NAME
                                                       };

                launchConfigs.Add(launchConfig);
                
                Program.ApplicationSettings.TduLaunchConfigurations = LaunchConfigurationConverter.ConvertToString(launchConfigs);
                Program.ApplicationSettings.Save();
            }

            // View mode
            _isBasicView = !bool.Parse(Program.ApplicationSettings.AdvancedMode);
            _SetView();
        }

        #region Méthodes publiques
        /// <summary>
        /// Ajoute l'outil spécifié
        /// </summary>
        /// <param name="moduleType">Type de module à afficher</param>
        /// <param name="filesToEdit">Fichier(s) à éditer</param>
        internal void LoadTool(ModuleType moduleType, params string[] filesToEdit)
        {
            Form newForm = null;

            Cursor = Cursors.WaitCursor;

            try
            {
                if (filesToEdit == null || filesToEdit.Length == 0)
                    filesToEdit = new string[] { null, null };

                switch (moduleType)
                {
                    case ModuleType.DDSTo2DBConverter:
                        newForm = new DDSTo2DBForm();
                        break;
                    case ModuleType.x2DBToDDSConverter:
                        newForm = new _2DBToDDSForm();
                        break;
                    case ModuleType.FileBrowser:
                        // Une seule instance du FileBrowser est autorisée
                        newForm = FileBrowserForm.Instance;
                        break;
                    case ModuleType.XMBEditor:
                        newForm = new XMBEditorForm();
                        break;
                    case ModuleType.DBResourcesEditor:
                        newForm = new DbResourcesEditorForm(filesToEdit[0]);
                        break;
                    case ModuleType.PatchEditor:
                        // 1 instance allowed
                        newForm = PatchEditorForm.GetInstance(filesToEdit[0]);
                       break;
                    case ModuleType.VehicleManager:
                        newForm = VehicleManagerForm.Instance;
                        break;
                    case ModuleType.TrackPack:
                        newForm = TrackPackForm.Instance;
                        break;
                    case ModuleType.DBEditor:
                        newForm = DbEditorForm.Instance;
                        break;
                    default:
                        break;
                }

                if (newForm != null)
                {
                    if (newForm.Visible)
                    {
                        if (newForm.WindowState == FormWindowState.Minimized)
                            newForm.WindowState = FormWindowState.Normal;

                        newForm.Focus();
                    }
                    else
                        newForm.Show();
                }

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                string message = string.Format(_ERROR_NEW_MODULE, moduleType);

                MessageBoxes.ShowError(this, new Exception(message, ex));
            }
        }
        #endregion

        #region Events
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Click on file > Exit menu item
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Preparing data...
            // EVO_83 : tdu version displayed
            string versionText = _TEXT_VERSION_UNKNOWN;

            switch (Tools.InstalledTduVersion)
            {
                case Tools.TduVersion.V1_45:
                    versionText = _TEXT_VERSION_GENUINE;
                    break;
                case Tools.TduVersion.V1_66:
                    versionText = _TEXT_VERSION_PATCH;
                    break;
                case Tools.TduVersion.V1_66_Megapack:
                    versionText = _TEXT_VERSION_MEGAPACK;
                    break;
                case Tools.TduVersion.Unknown:
                    versionText = _TEXT_VERSION_UNKNOWN;
                    break;
            }

            // Preparing about box...
            AboutBox aboutBox = new AboutBox
                                    {
                                        CustomImage = Resources.tdumt,
                                        CustomTitle = _TITLE_ABOUT_BOX,
                                        CustomMessage = AppConstants.PRODUCT_CUSTOM,
                                        CustomDate = AppConstants.PRODUCT_DATE,
                                        CustomInformation = string.Format(_FORMAT_ABOUT_TDU_VERSION, versionText)
                                    };

            aboutBox.ShowDialog(this);
        }

        private void DDS2DBButton_Click(object sender, EventArgs e)
        {
            // DDS to 2DB converter
            LoadTool(ModuleType.DDSTo2DBConverter, null);
        }

        private void DBDDSButton_Click(object sender, EventArgs e)
        {
            // 2DB to DDS converter
            LoadTool(ModuleType.x2DBToDDSConverter, null);
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            // Initialisation

                // Gestion de la vue basique/avancée
            _SetView(); 
        }

        private void basicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // On passe à la vue basique
            if (!IsBasicView)
            {
                _isBasicView = true;
                _SetView();
            }

            // Settings
            Program.ApplicationSettings.AdvancedMode = (!_isBasicView).ToString();
        }

        private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // On passe à la vue avancée
            if (IsBasicView)
            {
                _isBasicView = false;
                _SetView();
            }

            // Settings
            Program.ApplicationSettings.AdvancedMode = (!_isBasicView).ToString();
        }
        
        private void fileBrowserButton_Click(object sender, EventArgs e)
        {
            LoadTool(ModuleType.FileBrowser, null);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Ouvre les options
            SettingsForm settingsForm = new SettingsForm(SettingsForm.SettingsTabPage.Main);

            settingsForm.ShowDialog(this);
        }

        private void userGuidetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Clic sur l'élément de menu "User Guide"
            try
            {
                // Lance le navigateur par défaut
                ProcessStartInfo editorProcess = new ProcessStartInfo(LibraryConstants.URL_OFFLINE_DOC);

                Process.Start(editorProcess);
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void getUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // EVO_82.
            // Click on "Get updates (online)" menu item > opens browser to official TDU Central thread
            try
            {
                ProcessStartInfo editorProcess = new ProcessStartInfo(AppConstants.URL_UPDATES);

                Process.Start(editorProcess);
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void DBButton_Click(object sender, EventArgs e)
        {
            // Click on "DB Editor" button
            LoadTool(ModuleType.DBResourcesEditor);
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Suppression des tâches d'édition
            EditHelper.Instance.ClearTasks();

            // Nettoyage des fichiers temporaires
            try
            {
                File2.RemoveTemporaryFolder(null, LibraryConstants.FOLDER_TEMP);
            }
            catch(Exception)
            {
                // Silent exception!
                //Exception2.PrintStackTrace(ex);
                Log.Info("Unable to remove temporary folder - may be gone, already.");
            }

            // Settings
            Program.ApplicationSettings.Save();
        }

        private void launchConfButton_Click(object sender, EventArgs e)
        {
            // EVO_36 : options
            // Génération de menu contextuel
            try
            {
                launchConfButton.ContextMenuStrip = new LaunchContextMenuFactory().CreateContextMenu(this);
                launchConfButton.ContextMenuStrip.Show(launchConfButton.PointToScreen(new Point()));
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void patchEditorButton_Click(object sender, EventArgs e)
        {
            // Wizard: patchs editor
            LoadTool(ModuleType.PatchEditor);
        }

        private void vehicleManagerButton_Click(object sender, EventArgs e)
        {
            // EVO_56
            // Click on 'Vehicle Manager' button
            LoadTool(ModuleType.VehicleManager);
        }

        private void trackPackButton_Click(object sender, EventArgs e)
        {
            // Click on 'Track Pack' button
            LoadTool(ModuleType.TrackPack);
        }
        #endregion

        #region Méthodes privées
        /// <summary>
        /// Définit la vue de la fenêtre principale
        /// </summary>
        private void _SetView()
        {
            // Menu de choix de vue
            advancedToolStripMenuItem.Checked = !IsBasicView;
            basicToolStripMenuItem.Checked = IsBasicView;

            // Boutons
            dbResButton.Visible = !IsBasicView;
            patchEditorButton.Visible = !IsBasicView;
            dbButton.Visible = !IsBasicView;
        }

        /// <summary>
        /// Gère le lancement de TDU avec une configuration particulière
        /// </summary>
        /// <param name="config">Config to use</param>
        private static void _LaunchTDU(LaunchConfiguration config)
        {
            if (config == null)
                throw new Exception(_ERROR_NO_LAUNCH_CONFIG);

            // Commande avant lancement
            if (!string.IsNullOrEmpty(config.PreviousCommand))
            {
                try
                {
                    Log.Info("Launching previous command: " + config.PreviousCommand + "...");

                    // Double-quotes are automatically added for right interpretation
                    ProcessStartInfo process = new ProcessStartInfo("\"" + config.PreviousCommand + "\"")
                    {
                        WorkingDirectory =
                            new FileInfo(config.PreviousCommand).DirectoryName
                    };

                    Process.Start(process);
                }
                catch (Exception ex)
                {
                    throw new Exception(_ERROR_EXECUTE_PREV_COMMAND, ex);
                }
            }

            // Nettoyage du radial ?
            if (config.CleanRadial)
            {
                try
                {
                    Log.Info("Cleaning radial.cdb...");
                    Tools.DeleteRadial();
                }
                catch (Exception ex)
                {
                    throw new Exception(_ERROR_RADIAL, ex);
                }
            }

            // Lancement de TDU
            string command = @"{0}" + LibraryConstants.FILE_TDU_EXE;
            string parameter = "";

            if (config.FpsDisplayed)
                parameter = Tools.EXEC_SWITCH_FRAMERATE;
            else if (config.WindowedMode)
                parameter = Tools.EXEC_SWITCH_WINDOWED;
            else if (config.PosDisplayed)
                // EVO_164 pos command handling
                parameter = Tools.EXEC_SWITCH_POSITION;
            //
            command = string.Format(command, Program.ApplicationSettings.TduMainFolder);

            try
            {
                ProcessStartInfo tduProcess = new ProcessStartInfo(command, parameter);

                Log.Info("Starting TDU: " + command + " " + parameter + " ...");

                // BUG_42: working folder used to prevent TDU from failing
                tduProcess.WorkingDirectory = Program.ApplicationSettings.TduMainFolder;
                Process.Start(tduProcess);
            }
            catch (Exception ex)
            {
                throw new Exception(_ERROR_EXECUTE_TDU_PROCESS, ex);
            }

            // Commande après lancement
            if (!string.IsNullOrEmpty(config.NextCommand))
            {
                try
                {
                    Log.Info("Launching next command: " + config.PreviousCommand + "...");

                    // Double-quotes are automatically added for right interpretation
                    ProcessStartInfo process = new ProcessStartInfo("\"" + config.NextCommand + "\"")
                    {
                        WorkingDirectory = new FileInfo(config.NextCommand).DirectoryName
                    };

                    Process.Start(process);
                }
                catch (Exception ex)
                {
                    throw new Exception(_ERROR_EXECUTE_NEXT_COMMAND, ex);
                }
            }
        }

        /// <summary>
        /// Gère le lancement de TDU avec une configuration particulière. Utilisé pour l'invocation à distance.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _LaunchTDU(object sender, EventArgs e)
        {
            // Récupération de la configuration demandée
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            if (item != null)
            {
                string configName = item.Text;
                Collection<LaunchConfiguration> configList = Program.ApplicationSettings.GetLaunchConfigList();
                LaunchConfiguration selectedConf = LaunchConfiguration.GetConfigurationByName(configList, configName);

                try
                {
                    _LaunchTDU(selectedConf);
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        /// <summary>
        /// Affiche les paramètres pour la configuration de lancement TDU. Utilisé pour l'invocation à distance.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _DisplayLaunchSettings(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(SettingsForm.SettingsTabPage.LaunchSettings);

            settingsForm.ShowDialog(this);
        }

        private void dbButton_Click_1(object sender, EventArgs e)
        {
            // Click on "Database" button
            LoadTool(ModuleType.DBEditor);
        }
        #endregion
    }
}
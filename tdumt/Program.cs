using System;
using System.IO;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using TDUModdingLibrary.support;
using TDUModdingTools.common;
using TDUModdingTools.common.settings;
using TDUModdingTools.gui;

namespace TDUModdingTools
{
    /// <summary>
    /// Classe d'entrée de l'application.
    /// </summary>
    static class Program
    {
        #region Constantes


        #endregion

        #region Properties
        /// <summary>
        /// Donne l'accès aux paramètres de l'appli
        /// </summary>
        public static TduModdingToolsSettings ApplicationSettings
        {
            get { return _ApplicationSettings; }
        }
        private static TduModdingToolsSettings _ApplicationSettings;
        #endregion

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // Visual settings
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // BUG_40: requesting administrator privileges
                _InitializeRights();
                _InitializeSettings();
                _InitializeLog();
                _InitializeMessages();
                _InitializeModdingLibrary();

                // Displays Main Form
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format(AppConstants.FORMAT_ERROR_CRITICAL, ex.Message);

                MessageBoxes.ShowError(null, new Exception(errorMessage, ex));
            }
        }

        #region Méthodes privées
        /// <summary>
        /// Initialise les paramètres de l'application
        /// </summary>
        private static void _InitializeSettings()
        {
            _ApplicationSettings = new TduModdingToolsSettings();

            // BUG_30 : dossier de paramètres
            string settingsFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\" + Application.ProductName;

            if (!Directory.Exists(settingsFolder))
                Directory.CreateDirectory(settingsFolder);

            _ApplicationSettings.ConfigFilePath = settingsFolder + @"\settings.xml";
            _ApplicationSettings.Load();
        }
        
        /// <summary>
        /// Ensures user has administrator rights to continue
        /// </summary>
        private static void _InitializeRights()
        {
            // FIXME : on Vista if app has admin privileges, all drag and drop sources must be also elevated...
            // thus drag and drop won't work
            // BUG_50
            /*if (!RightsManagement.HasAdminRights)
                // Display a warning window but allows to continue
                MessageBoxes.ShowWarning(null, _WARNING_RESTRICTED_USER);*/
        }

        /// <summary>
        /// Sets all global appenders for instant logging support
        /// </summary>
        private static  void _InitializeLog()
        {
            // EVO_73 : if debug is enabled, a file appender is added for exceptions
            if ( bool.Parse(_ApplicationSettings.DebugModeEnabled) )
                Tools.EnableDebugMode(Application.StartupPath + AppConstants.FILE_LOG_DEBUG);
        }
        
        /// <summary>
        /// Sets all info required by Modding Lib
        /// </summary>
        private static void _InitializeModdingLibrary()
        {
            // Initializing working path for Modding Library
            Tools.WorkingPath = Application.StartupPath;

            // Initializing TDU Folder for Modding Library, from current settings
            if (!string.IsNullOrEmpty(_ApplicationSettings.TduMainFolder))
                Tools.TduPath = _ApplicationSettings.TduMainFolder;
        }

        private static void _InitializeMessages()
        {
            // EVO_159
            MessageBoxes.AdditionalMessageOnError = AppConstants.ERROR_ADDITIONAL;
            //
        }
        #endregion
    }
}
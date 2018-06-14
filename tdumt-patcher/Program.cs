using System;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Windows32.Security;
using TDUModAndPlay.gui;
using TDUModdingLibrary.support;
using TDUModdingLibrary.support.constants;

namespace TDUModAndPlay
{
    static class Program
    {
        #region Constants
        /// <summary>
        /// Message when a critical error has occurred
        /// </summary>
        private const string _ERROR_CRITICAL = "Sorry, but a critical error has occured:";

        /// <summary>
        /// Message format for a critical error
        /// </summary>
        private const string _ERROR_CRITICAL_FORMAT = "{0}\r\n{1}";

        /// <summary>
        /// Restricted user warning and question
        /// </summary>
        private const string _QUESTION_RESTRICTED_USER = "This application requires administrator rights to install mods correctly.\r\nContinue anyway?";
        #endregion

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Rights checking
                DialogResult dr = DialogResult.Yes;

                if (!RightsManagement.HasAdminRights)
                    dr = MessageBoxes.ShowQuestion(null, _QUESTION_RESTRICTED_USER, MessageBoxButtons.YesNo);

                if (dr != DialogResult.No)
                {
                    _InitializeLog();
                    _InitializeModdingLibrary();

                    // Main loop
                    Application.Run(new MainForm());
                }
            }
            catch (Exception ex)
            {
                // Critical error
                string message = string.Format(_ERROR_CRITICAL_FORMAT, _ERROR_CRITICAL, ex.Message);

                MessageBoxes.ShowError(null, new Exception(message, ex));
            }
        }

        #region Private methods
        /// <summary>
        /// Sets all info required by Modding Lib
        /// </summary>
        private static void _InitializeModdingLibrary()
        {
            // Initializing working path for Modding Library
            Tools.WorkingPath = Application.StartupPath;
        }

        /// <summary>
        /// Sets all global appenders for instant logging support
        /// </summary>
        private static void _InitializeLog()
        {
            // Mod&Play is always in debug mode...
            Tools.EnableDebugMode(Application.StartupPath + LibraryConstants.FILE_MNP_LOG_DEBUG);
        }
        #endregion
    }
}
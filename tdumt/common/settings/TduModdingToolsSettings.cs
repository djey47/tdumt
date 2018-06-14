using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using DjeFramework1.Common.Support.ApplicationSettings;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.support;
using TDUModdingTools.gui;
using TDUModdingTools.gui.filebrowser;

namespace TDUModdingTools.common.settings
{
    /// <summary>
    /// Liste les paramètres de l'application
    /// </summary>
    class TduModdingToolsSettings : CustomSettingsBase
    {
        #region Parameters
        /// <summary>
        /// Dossier d'installation de Test Drive Unlimited
        /// </summary>
        [DefaultSettingValue("")]
        public string TduMainFolder
        {
            get { return (string) _GetParameter("TduMainFolder"); }
            set { _SetParameter("TduMainFolder", value); }
        }

        /// <summary>
        /// Module avec lequel démarrer l'application
        /// </summary>
        [DefaultSettingValue("")]
        public string StartupModule
        {
            get { return (string)_GetParameter("StartupModule"); }
            set { _SetParameter("StartupModule", value); }
        }

        /// <summary>
        /// Mode d'affichage de la liste
        /// </summary>
        [DefaultSettingValue("LargeIcon")]
        public string FileBrowserViewMode
        {
            get { return (string)_GetParameter("FileBrowserViewMode"); }
            set { _SetParameter("FileBrowserViewMode", value); }
        }

        /// <summary>
        /// Liste des configs de lancement
        /// </summary>
        [DefaultSettingValue("")]
        public string TduLaunchConfigurations
        {
            get { return (string)_GetParameter("TduLaunchConfigurations"); }
            set { _SetParameter("TduLaunchConfigurations", value); }
        }

        /// <summary>
        /// Langue par défaut pour la consultation de la base de données
        /// </summary>
        [DefaultSettingValue("US - English")]
        public string TduLanguage
        {
            get { return (string)_GetParameter("TduLanguage"); }
            set { _SetParameter("TduLanguage", value); }
        }

        /// <summary>
        /// Indicates if the execution report must scroll automatically after a new line has been written
        /// </summary>
        [DefaultSettingValue("true")]
        public string PatchEditorReportAutoScroll
        {
            get { return (string)_GetParameter("PatchEditorReportAutoScroll"); }
            set { _SetParameter("PatchEditorReportAutoScroll", value); }
        }

        /// <summary>
        /// Indicates if the execution report must be cleared before running patch
        /// </summary>
        [DefaultSettingValue("false")]
        public string PatchEditorReportClear
        {
            // EVO_110
            get { return (string)_GetParameter("PatchEditorReportClear"); }
            set { _SetParameter("PatchEditorReportClear", value); }
        }

        /// <summary>
        /// Strores the last deploy location for a patch
        /// </summary>
        [DefaultSettingValue("")]
        public string PatchEditorLastDeployLocation
        {
            get { return (string)_GetParameter("PatchEditorLastDeployLocation"); }
            set { _SetParameter("PatchEditorLastDeployLocation", value); }
        }

        /// <summary>
        /// Indicates if debug mode must be enabled
        /// </summary>
        [DefaultSettingValue("false")]
        public string DebugModeEnabled
        {
            // EVO_73
            get { return (string) _GetParameter("DebugModeEnabled"); }
            set { _SetParameter("DebugModeEnabled", value); }
        }

        /// <summary>
        /// Indicates if Bnk Manager must be visible
        /// </summary>
        [DefaultSettingValue("true")]
        public string BnkManagerVisible
        {
            // EVO_93
            get { return (string)_GetParameter("BnkManagerVisible"); }
            set { _SetParameter("BnkManagerVisible", value); }
        }

        /// <summary>
        /// Indicates if edit tasks window must be visible
        /// </summary>
        [DefaultSettingValue("false")]
        public string EditTasksVisible
        {
            // EVO_93
            get { return (string)_GetParameter("EditTasksVisible"); }
            set { _SetParameter("EditTasksVisible", value); }
        }

        /// <summary>
        /// Indicates if the tools must run in Advanced mode
        /// </summary>
        [DefaultSettingValue("false")]
        public string AdvancedMode
        {
            get { return (string)_GetParameter("AdvancedMode"); }
            set { _SetParameter("AdvancedMode", value); }
        }

        /// <summary>
        /// Indicates which folder to display first when replacing files in FileBrowser
        /// </summary>
        [DefaultSettingValue("")]
        public string DefaultEditNewFilesFolder
        {
            // EVO_102
            get { return (string)_GetParameter("DefaultEditNewFilesFolder"); }
            set { _SetParameter("DefaultEditNewFilesFolder", value); }
        }

        /// <summary>
        /// Indicates which display mode to use with Vehicle Manager (original/modded)
        /// </summary>
        [DefaultSettingValue("true")]
        public string VehicleManagerOriginalDisplayMode
        {
            get { return (string)_GetParameter("VehicleManagerOriginalDisplayMode"); }
            set { _SetParameter("VehicleManagerOriginalDisplayMode", value); }
        }

        /// <summary>
        /// Indicates which display mode to use with Vehicle Manager (original/modded)
        /// </summary>
        [DefaultSettingValue("true")]
        public string BnkManagerFlatDisplayMode
        {
            get { return (string)_GetParameter("BnkManagerFlatDisplayMode"); }
            set { _SetParameter("BnkManagerFlatDisplayMode", value); }
        }

        /// <summary>
        /// Indicates whether explorer should be opened after extract
        /// </summary>
        [DefaultSettingValue("true")]
        public string ExtractDisplayInExplorer
        {
            get { return (string)_GetParameter("ExtractDisplayInExplorer"); }
            set { _SetParameter("ExtractDisplayInExplorer", value); }
        }

        /// <summary>
        /// Player profile to use
        /// </summary>
        [DefaultSettingValue("")]
        public string PlayerProfile
        {
            get { return (string)_GetParameter("PlayerProfile"); }
            set { _SetParameter("PlayerProfile", value); }
        }
        #endregion

        #region Attributs
        /// <summary>
        /// Permet le suivi de modifs sur le dossier racine
        /// </summary>
        private string tduRoot;
        #endregion

        /// <summary>
        /// Constructeur principal
        /// </summary>
        public TduModdingToolsSettings()
        {
            SettingsLoaded += SettingsLoadedEventHandler;
            SettingChanging += SettingChangingEventHandler;
            SettingsSaving += SettingsSavingEventHandler;
        }

        #region Méthodes publiques
        /// <summary>
        /// Renvoie la langue actuelle pour la base de données
        /// </summary>
        public DB.Culture GetCurrentCulture()
        {
            string cultureCode = TduLanguage.Substring(0, 2);

            DB.Culture culture = (DB.Culture)Enum.Parse(typeof(DB.Culture), cultureCode);
            return culture;
        }

        /// <summary>
        /// Renvoie la liste de configurations de lancement
        /// </summary>
        /// <returns></returns>
        public Collection<LaunchConfiguration> GetLaunchConfigList()
        {
            return LaunchConfigurationConverter.ConvertFromString(TduLaunchConfigurations);
        }

        /// <summary>
        /// Overloads loading for specific processing
        /// </summary>
        public new void Load()
        {
            // Call to parent
            base.Load();

            Save();
        }
        #endregion

        #region Events
        /// <summary>
        /// Les paramètres ont changé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
        {
            if (e.SettingName == "TduMainFolder" && tduRoot == null)
                tduRoot = ((TduModdingToolsSettings)sender).TduMainFolder;
        }

        /// <summary>
        /// Les paramètres ont été sauvegardés
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
        {
            TduModdingToolsSettings set = ((TduModdingToolsSettings)sender);

            // BUG_10, BUG_82 : la mise à jour du dossier racine doit rafraîchir la liste dans le browser
            if ( MainForm.Instance != null
                && !set.TduMainFolder.Equals(tduRoot))
            {
                // Updating Modding Library
                Tools.TduPath = set.TduMainFolder;

                FileBrowserForm browser = FileBrowserForm.Instance;

                if (browser != null && browser.Visible)
                    browser.MainFolderChanged();

                tduRoot = null;
            }
        }

        /// <summary>
        /// Les paramètres ont été chargés
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsLoadedEventHandler(object sender, SettingsLoadedEventArgs e)
        { }
        #endregion
    }
}
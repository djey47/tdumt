using System;
using System.Windows.Forms;
using TDUModdingLibrary.support.constants;

namespace TDUModdingTools.common
{
    /// <summary>
    /// Application-shared constants (excluding GUI data)
    /// </summary>
    internal static class AppConstants
    {
        #region Known folders
        /// <summary>
        /// Full folder name for patchs management
        /// </summary>
        public static readonly string FOLDER_PATCHS = Application.StartupPath +  LibraryConstants.FOLDER_PATCHS;

        /// <summary>
        /// Full folder name for XML contents
        /// </summary>
        public static readonly string FOLDER_XML = Application.StartupPath +  LibraryConstants.FOLDER_XML;

        /// <summary>
        /// Full folder name for logos pictures
        /// </summary>
        public static readonly string FOLDER_LOGOS = Application.StartupPath + LibraryConstants.FOLDER_LOGOS;

        /// <summary>
        /// Full folder name for custom tracks
        /// </summary>
        public static readonly string FOLDER_TRACKS = Application.StartupPath + LibraryConstants.FOLDER_TRACKS;

        /// <summary>
        /// Full folder name for original challenges
        /// </summary>
        public static readonly string FOLDER_ORIGINAL_CHALLENGES = string.Concat(Application.StartupPath, LibraryConstants.FOLDER_XML, LibraryConstants.FOLDER_DEFAULT, LibraryConstants.FOLDER_CHALLENGES);
        #endregion

        #region Internal files
        /// <summary>
        /// XML key name for MAPs
        /// </summary>
        public const string FILE_MAP_KEY = @"\map-key.xml";

        /// <summary>
        /// Name of TDU Mod And play executable
        /// </summary>
        public const string FILE_EXE_MODANDPLAY = @"\TDU Mod And Play!.exe";

        /// <summary>
        /// Name of TrackPack client executable
        /// </summary>
        public const string FILE_EXE_TRACKPACK = @"\TDU TrackPack Client.exe";

        /// <summary>
        /// File name pour debug log
        /// </summary>
        public const string FILE_LOG_DEBUG = @"\ModdingToolsDebug.log";
        #endregion

        #region Product info
        /// <summary>
        /// About... custom date
        /// </summary>
        public const string PRODUCT_DATE = "November, 2014";

        /// <summary>
        /// About... custom message
        /// </summary>
        public readonly static string PRODUCT_CUSTOM = "-Credits-\r\nDesign, coding, testing: Djey\r\nDatabase: 2CVSUPERGT\r\nLogos: 2CVSUPERGT-christophe31fr-xiorxorn\r\nAnd many thanks to TDU communities.";
        #endregion

        #region Web
        /// <summary>
        /// Official online thread URL
        /// </summary>
        public const string URL_UPDATES = @"http://forum.turboduck.net/threads/3739-Djey-TDU-Modding-Tools-1161-Extras";

        /// <summary>
        /// Resource query topic on TDU:C
        /// </summary>
        public const string URL_RESOURCES_TDUC = @"http://forum.turboduck.net/threads/12707-TDU-Community-Patches-16xx-your-name-requests";
        #endregion

        #region File parts
        #endregion

        #region Error messages
        /// <summary>
        /// Format pour le message d'erreur critique
        /// </summary>
        public const string FORMAT_ERROR_CRITICAL = "Sorry, but a critical error happened: {0}";

        /// <summary>
        /// Additional error message
        /// </summary>
        public static readonly string ERROR_ADDITIONAL = string.Concat(
            "Please download and have a look at TDUMT documentation to solve common issues:",
            Environment.NewLine,
            LibraryConstants.URL_OFFLINE_DOC,
            Environment.NewLine,
            "You'll be told also how to report new bugs. Thank you!");
        #endregion
    }
}
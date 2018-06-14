using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.graphics;
using TDUModdingLibrary.fileformats.specific;
using TDUModdingTools.common.handlers.database;
using TDUModdingTools.common.handlers.specific;

namespace TDUModdingTools.common.handlers
{
    /// <summary>
    /// Generic class for handling of files in Modding Tools.
    /// </summary>
    abstract class FileHandler
    {
        #region Constants
        /// <summary>
        /// Default editor name
        /// </summary>
        private const string _DEFAULT_EDITOR_NAME = "default application";

        /// <summary>
        /// Error message when applying changes
        /// </summary>
        protected const string _ERROR_APPLY = "Error when applying changes from '{0}' to '{1}'";
        #endregion

        #region Properties
        /// <summary>
        /// Associated TDUFile to this handler
        /// </summary>
        public TduFile TheTDUFile
        {
            get { return _TheTDUFile; }
        }
        private TduFile _TheTDUFile;

        /// <summary>
        /// Default editor name for the type
        /// </summary>
        public static string DefaultEditor
        {
            get { return _DEFAULT_EDITOR_NAME; }
        }

        /// <summary>
        /// Attached file name
        /// </summary>
        public string FileName
        {
            get
            {
                return _TheTDUFile == null ? null : _TheTDUFile.FileName;
            }
        }
        #endregion

        #region Methods to implement
        /// <summary>
        /// Réalise l'édition du fichier. A redéfinir dans chaque sous-classe si nécessaire.
        /// </summary>
        public abstract void Edit();

        /// <summary>
        /// Met à jour les modifications réalisées sur ce fichier.
        /// Utile pour les types de fichiers nécessitant une re-conversion (ex: DDS)
        /// A redéfinir dans chaque sous-classe
        /// </summary>
        /// <param name="paramList">Paramètres éventuels</param>
        public abstract void Apply(params object[] paramList);
        #endregion

        #region Public methods
        /// <summary>
        /// Returns the right FileHandler according to specified file.
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <returns>RegulardHandler instance if it's from an unsupported type</returns>
        public static FileHandler GetHandler(string fileName)
        {
            // TDU File
            TduFile tduFile = TduFile.GetFile(fileName);
            FileHandler handler = new RegularHandler();

            // New mapping management

            // DB
            if (Regex.IsMatch(fileName, DB.FILENAME_PATTERN, RegexOptions.IgnoreCase))
            {
                handler = new DBHandler();
            }
            // BNK
            else if (Regex.IsMatch(fileName, BNK.FILENAME_PATTERN, RegexOptions.IgnoreCase))
                handler = new BNKHandler();
            // DDS
            else if (Regex.IsMatch(fileName, DDS.FILENAME_PATTERN, RegexOptions.IgnoreCase))
                handler = new DDSHandler();
            // 2DB
            else if (Regex.IsMatch(fileName, _2DB.FILENAME_PATTERN, RegexOptions.IgnoreCase))
                handler = new _2DBHandler();
            // MAP
            else if (Regex.IsMatch(fileName, MAP.FILENAME_PATTERN, RegexOptions.IgnoreCase))
                handler = new MAPHandler();
            // PCH
            else if (Regex.IsMatch(fileName, PCH.FILENAME_PATTERN, RegexOptions.IgnoreCase))
                handler = new PCHHandler();
            // DB Resources
            else if (Regex.IsMatch(fileName, DBResource.FILENAME_PATTERN, RegexOptions.IgnoreCase))
                handler = new DBResourceHandler();
            // DFE/IGE
            /*else if (Regex.IsMatch(fileName, DFE.FILENAME_PATTERN, RegexOptions.IgnoreCase)
                || Regex.IsMatch(fileName, IGE.FILENAME_PATTERN, RegexOptions.IgnoreCase))
                handler = new TrackHandler();*/

            handler._TheTDUFile = tduFile;

            return handler;
        }
        #endregion

        #region Protected methods
        /// <summary>
        /// Utility method launching specified file as a new process
        /// </summary>
        /// <param name="fileName">File to run</param>
        protected static void __SystemRun(string fileName)
        {
            if (File.Exists(fileName))
            {
                ProcessStartInfo appliProcess = new ProcessStartInfo(fileName);
                Process.Start(appliProcess);
            }
        }
        #endregion
    }
}
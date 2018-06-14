using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using DjeFramework1.Common.Support.Meta;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.fileformats.binaries;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.graphics;
using TDUModdingLibrary.fileformats.physics;
using TDUModdingLibrary.fileformats.sound;
using TDUModdingLibrary.fileformats.specific;
using TDUModdingLibrary.fileformats.world;
using TDUModdingLibrary.fileformats.xml;
using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.fileformats
{
    /// <summary>
    /// Represents a TDU datafile. Always get a TDUFile by using the static method "GetFile" to have correct data initialization.
    /// </summary>
    public abstract class TduFile
    {
        #region Constants
        /// <summary>
        /// Pattern for unknown extension
        /// </summary>
        private const string _UNKNOWN_FILENAME_PATTERN = @"\?";

        /// <summary>
        /// Pattern for file name (to redefine)
        /// </summary>
        public const string FILENAME_PATTERN = String2.REGEX_PATTERN_STARTS_ENDS_WITH;

        /// <summary>
        /// Pattern for backup file name
        /// </summary>
        public const string BACKUP_FILENAME_PATTERN = String2.REGEX_PATTERN_EXTENSION + LibraryConstants.EXTENSION_BACKUP;
        #endregion

        #region Properties
        /// <summary>
        /// Nom du fichier
        /// </summary>
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }
        protected string _FileName = "";

        /// <summary>
        /// Taille du fichier
        /// </summary>
        public uint Size
        {
            get { return _FileSize; }
        }
        protected uint _FileSize;

        /// <summary>
        /// Booléen indiquant si le fichier existe ou pas
        /// </summary>
        public bool Exists
        {
            get { return _FileExists; }
        }
        protected bool _FileExists;

        /// <summary>
        /// Specific properties
        /// </summary>
        public List<Property> Properties
        {
            get { return _Properties; }
        }
        protected List<Property> _Properties = new List<Property>();

        /// <summary>
        /// Returns true if current file has been modified since last loading
        /// </summary>
        public bool IsModified
        {
            get
            {
                bool result = false;

                if (Exists)
                {
                    DateTime lwt = new FileInfo(FileName).LastWriteTime;
                    DateTime lla = _LastLoadedAt;

                    //Log.Info("DEBUG : now=" + DateTime.Now + " - lwt=" + lwt + " - lla=" + lla + " - " + _FileName);

                    result = (lwt > lla);
                }

                return result;
            }
        }
        #endregion

        #region Fields
        /// <summary>
        /// Date and time when file was last loaded
        /// </summary>
        private DateTime _LastLoadedAt;

        /// <summary>
        /// File types descriptions list, indexed by filename pattern
        /// </summary>
        private static readonly Dictionary<string, string> _FileDescriptionList = new Dictionary<string, string>
                                       {
                                           // Handled types
                                           { Cameras.FILENAME_PATTERN,"Binary data for ingame cameras"},
                                           { AIConfig.FILENAME_PATTERN,"XML encrypted file for AI settings"},
                                           { _2DB.FILENAME_PATTERN, "Texture"},
                                           { BTRQ.FILENAME_PATTERN, "Vehicle physics data"},
                                           { XMB_WAV.FILENAME_PATTERN, "Sound file"},
                                           { XMB.FILENAME_PATTERN, "Sound processing data"},
                                           { BNK.FILENAME_PATTERN, "TDU archive"},
                                           { DDS.FILENAME_PATTERN, "Direct Draw Surface (texture)"},
                                           { MAP.FILENAME_PATTERN, "TDU Mapped Protection System"},
                                           { PCH.FILENAME_PATTERN, "Modding Tools Patch"},
                                           { DB.FILENAME_PATTERN, "Encrypted database file"},
                                           { IGE.FILENAME_PATTERN, "TDU Editor track"},
                                           { DFE.FILENAME_PATTERN, "Game challenge"},
                                           // Unhandled ones
                                           {_UNKNOWN_FILENAME_PATTERN, "Unknown"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, LibraryConstants.EXTENSION_MATERIAL_FILE), "Materials"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, LibraryConstants.EXTENSION_3D_DESCRIPTION_FILE), "3D model description"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, LibraryConstants.EXTENSION_3D_GEOMETRY_FILE),"3D model geometry"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION,  LibraryConstants.EXTENSION_INI_FILE), "Settings"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION,  "SPT"), "Spot (special location) info"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION,  LibraryConstants.EXTENSION_BIN_FILE), "Binary data"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, "ANM"), "Model animation"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, "CIN"), "Cinematic"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, "SCE"), "Scene"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, LibraryConstants.EXTENSION_BACKUP), "Backup"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, "VMF"), "User interface description"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, LibraryConstants.EXTENSION_XML_FILE), "eXtended Markeup Language"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, LibraryConstants.EXTENSION_VEHICLE_MANAGER_BACKUP),"Vehicle Manager backup"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, LibraryConstants.EXTENSION_STATION_FILE), "Embedded radio station"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, "PRT"), "Particle"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, DBResource.EXTENSION_FR), "French database resource file"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, DBResource.EXTENSION_GE), "German database resource file"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, DBResource.EXTENSION_US), "English database resource file"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, DBResource.EXTENSION_CH), "Chinese database resource file"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, DBResource.EXTENSION_JA), "Japanese database resource file"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, DBResource.EXTENSION_KO), "Korean database resource file"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, DBResource.EXTENSION_SP), "Spanish database resource file"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, DBResource.EXTENSION_IT), "Italian database resource file"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, "FNT"), "Font"},
                                           {string.Format(String2.REGEX_PATTERN_EXTENSION, "DHK"), "Damage and collision data"},
                                       };
        #endregion

        #region Méthodes privées
        /// <summary>
        /// Reads file data. To override if necessary. This method should not be called from subclasses !
        /// </summary>
        protected abstract void _ReadData();

        /// <summary>
        /// Updates common information when loading is finished
        /// </summary>
        /// <param name="fileName"></param>
        private void _FinalizeLoading(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);

            // Mise à jour des attributs communs
            _FileName = fi.FullName;

            if (fi.Exists)
            {
                _FileSize = (uint)fi.Length;
                _FileExists = true;


                _LastLoadedAt = DateTime.Now;

                //Log.Info("DEBUG: setting lla=" + _LastLoadedAt);

                // Common properties
                // EVO_149: Type description property added
                Property.ComputeValueDelegate fileNameDelegate = () => _FileName;
                Property.ComputeValueDelegate fileSizeDelegate = () => _FileSize.ToString();
                Property.ComputeValueDelegate fileTypeDelegate = () => GetTypeDescription(_FileName);

                Properties.Add(new Property("Full path", "Common", fileNameDelegate));
                Properties.Add(new Property("Type", "Common", fileTypeDelegate));
                Properties.Add(new Property("Size (bytes)", "Common", fileSizeDelegate));
            }
        }
        #endregion

        #region Méthodes publiques
        /// <summary>
        /// Saves the current file to disk. To implement if necessary.
        /// </summary>
        public abstract void Save();

        /// <summary>
        /// Saves the current file to a new location/file name
        /// </summary>
        /// <param name="pFileName">New file name to save to</param>
        public void SaveAs(string pFileName)
        {
            _FileName = pFileName;
            Save();
        }

        /// <summary>
        /// Crée une copie de sauvegarde du fichier
        /// </summary>
        /// <returns>Le nom du fichier de sauvegarde généré</returns>
        public string MakeBackup()
        {
            StringBuilder sbFileName = new StringBuilder(_FileName + "." + LibraryConstants.EXTENSION_BACKUP);

            while (new FileInfo(sbFileName.ToString()).Exists)
            {
                sbFileName.Append(".");
                sbFileName.Append(LibraryConstants.EXTENSION_BACKUP);
            }
            File.Copy(_FileName, sbFileName.ToString(), true);

            return sbFileName.ToString();
        }

        /// <summary>
        /// This method reloads current file from disk only if it has been modified since last loading
        /// </summary>
        public void ReloadIfNecessary()
        {
            // BUG_89: tracking multiple reloadings
            // This issue has no solution with current design, as LoadLoadedAt refers to each BNK instance. If instances changes, data is not OK with previous ones.
            Object lockObject = new Object();

            //Log.Info("DEBUG: querying lock for reloading (if necessary)... " + _FileName);

            lock (lockObject)
            {
                //Log.Info("DEBUG: lock succesfully acquired.");

                if (IsModified)
                {
                    Log.Info("DEBUG: reloading necessary, in progress... " + _FileName);
                    //Log.Info(Environment.StackTrace);

                    _ReadData();
                    _FinalizeLoading(_FileName);
                }
            }

            //Log.Info("DEBUG: releasing lock...");
        }

        /// <summary>
        /// Returns the right TDUFile according to specified file.
        /// </summary>
        /// <param name="fileName">file name, without path</param>
        /// <returns>null if file is from an unsupported type</returns>
        public static TduFile GetFile(string fileName)
        {
            TduFile tduFile = new Regular();
            FileInfo fi = new FileInfo(fileName);

            // New mapping management

            // Cameras
            if (Regex.IsMatch(fileName, Cameras.FILENAME_PATTERN, RegexOptions.IgnoreCase))
            {
                if (fi.Exists)
                    tduFile = new Cameras(fileName);
                else
                    tduFile = new Cameras();
            }
            // AIConfig
            else if (Regex.IsMatch(fileName, AIConfig.FILENAME_PATTERN, RegexOptions.IgnoreCase))
            {
                if (fi.Exists)
                    tduFile = new AIConfig(fileName);
                else
                    tduFile = new AIConfig();
            }
            // DB
            else if (Regex.IsMatch(fileName, DB.FILENAME_PATTERN, RegexOptions.IgnoreCase))
            {
                if (fi.Exists)
                    tduFile = new DB(fileName);
                else
                    tduFile = new DB();
            }
            // BNK
            else if (Regex.IsMatch(fileName, BNK.FILENAME_PATTERN, RegexOptions.IgnoreCase))
                tduFile = new BNK(fileName);
            // DDS
            else if (Regex.IsMatch(fileName, DDS.FILENAME_PATTERN, RegexOptions.IgnoreCase))
            {
                if (fi.Exists)
                    tduFile = new DDS(fileName);
                else
                    tduFile = new DDS();
            }
            // 2DB
            else if (Regex.IsMatch(fileName, _2DB.FILENAME_PATTERN, RegexOptions.IgnoreCase)
                || Regex.IsMatch(fileName, _2DB.FILENAME_OLD_PATTERN, RegexOptions.IgnoreCase))
            {
                if (fi.Exists)
                    tduFile = new _2DB(fileName);
                else
                    tduFile = new _2DB();
            }
            // MAP
            else if (Regex.IsMatch(fileName, MAP.FILENAME_PATTERN, RegexOptions.IgnoreCase))
                if (fi.Exists)
                    tduFile = new MAP(fileName);
                else
                    tduFile = new MAP();
            // XMB
            else if (Regex.IsMatch(fileName, XMB.FILENAME_PATTERN, RegexOptions.IgnoreCase))
            {
                if (fi.Exists)
                    tduFile = new XMB(fileName);
                else
                    tduFile = new XMB();
            }
            // WAV + XMB_WAV
            else if (Regex.IsMatch(fileName, XMB_WAV.FILENAME_PATTERN, RegexOptions.IgnoreCase))
            {
                try
                {
                    if (fi.Exists)
                        tduFile = new XMB_WAV(fileName);
                    else
                        tduFile = new XMB_WAV();
                }
                catch (FormatException)
                {
                    // standard WAV file
                }
            }
            // PCH
            else if (Regex.IsMatch(fileName, PCH.FILENAME_PATTERN, RegexOptions.IgnoreCase))
                tduFile = new PCH(fileName);
            // DB Resources
            else if (Regex.IsMatch(fileName, DBResource.FILENAME_PATTERN, RegexOptions.IgnoreCase))
            {
                if (fi.Exists)
                    tduFile = new DBResource(fileName);
                else
                    tduFile = new DBResource();
            }
            // DFE
            else if (Regex.IsMatch(fileName, DFE.FILENAME_PATTERN, RegexOptions.IgnoreCase))
                tduFile = new DFE(fileName);
            // IGE
            else if (Regex.IsMatch(fileName, IGE.FILENAME_PATTERN, RegexOptions.IgnoreCase))
                tduFile = new IGE(fileName);
            // Regular by default
            else
                tduFile = new Regular();

            // To update common information
            tduFile._FinalizeLoading(fileName);

            return tduFile;
        }

        /// <summary>
        /// Méthode utilitaire renvoyant la description du type de fichier selon le nom de fichier fourni
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>La description du type de fichier requis</returns>
        public static string GetTypeDescription(string fileName)
        {
            string returnedTypeDescription = null;

            // By checking all patterns
            foreach (KeyValuePair<string, string> anotherPattern in _FileDescriptionList)
            {
                if (Regex.IsMatch(fileName, anotherPattern.Key, RegexOptions.IgnoreCase))
                {
                    returnedTypeDescription = anotherPattern.Value;
                    break;
                }
            }

            // Unknown management
            if (returnedTypeDescription == null)
                returnedTypeDescription = _FileDescriptionList[_UNKNOWN_FILENAME_PATTERN];

            return returnedTypeDescription;
        }

        /// <summary>
        /// Returns name of file to be watched during editing
        /// </summary>
        /// <param name="fileName">Original file name</param>
        /// <returns>Name of file to be tracked (by default = original file name)</returns>
        public static string GetTrackedFileName(string fileName)
        {
            // BUG_48: added for accurate tracking support (e.g 2DB files)
            string resultFileName = null;

            if (!string.IsNullOrEmpty(fileName))
            {
                // 2DB file processing is particular...
                if (Regex.IsMatch(fileName, _2DB.FILENAME_PATTERN, RegexOptions.IgnoreCase))
                    resultFileName = File2.ChangeExtensionOfFilename(fileName, LibraryConstants.EXTENSION_DDS_FILE);
                else
                    resultFileName = fileName;
            }

            return resultFileName;
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Security;
using System.Text;
using DjeFramework1.Common.Calculations;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Common.Support.Traces.Appenders;
using DjeFramework1.Common.Types;
using DjeFramework1.Util.BasicStructures;
using DjeFramework1.Windows64.Core;
using Microsoft.Win32;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.support
{
    /// <summary>
    /// Misc. tools and information for TDUModdingTools usage
    /// </summary>
    public static class Tools
    {
        #region Enums
        /// <summary>
        /// List of TDU versions
        /// </summary>
        public enum TduVersion
        {
            Unknown,
            V1_45,
            V1_66,
            V1_66_Megapack
        }
        #endregion

        #region Constants - Misc.
        /// <summary>
        /// Symbol used for separating values
        /// </summary>
        public const char SYMBOL_VALUE_SEPARATOR = '|';

        /// <summary>
        /// Symbol used for separating values
        /// </summary>
        public const string SYMBOL_VALUE_SEPARATOR2 = "||";

        /// <summary>
        /// Symbol used for separating values
        /// </summary>
        public const string SYMBOL_VALUE_SEPARATOR3 = "=";

        /// <summary>
        /// Name for misc. slot
        /// </summary>
        public const string NAME_MISC_SLOT = "# Other mod #";

        /// <summary>
        /// Key for misc. slot
        /// </summary>
        public const string KEY_MISC_SLOT = "00000000";

        /// <summary>
        /// Key for remapping information (special slot)
        /// </summary>
        public const string KEY_REMAPPING_SLOT = "REMAPPING";

        /// <summary>
        /// Lowest id for camera set ids
        /// </summary>
        public const ushort LOWEST_CUSTOM_CAMERA_SET_ID = 3000;
        #endregion

        #region Constants - Command line parameters.
        /// <summary>
        /// Command line switch to display framerate
        /// </summary>
        public const string EXEC_SWITCH_FRAMERATE = "-fps";

        /// <summary>
        /// Command line switch to display TDU in a window
        /// </summary>
        public const string EXEC_SWITCH_WINDOWED = "-w";

        /// <summary>
        /// Command line switch to display coordinates information
        /// </summary>
        public const string EXEC_SWITCH_POSITION = "-pos";
        #endregion

        #region Constants - Registry
        /// <summary>
        /// Registry key for TDU data (32bit OS)
        /// </summary>
        private const string _REGKEY_TDU_32 = @"SOFTWARE\Atari\TDU\";

        /// <summary>
        /// Registry key for TDU data (64bit OS)
        /// </summary>
        private const string _REGKEY_TDU_64 = @"SOFTWARE\Wow6432Node\Atari\TDU\";

        /// <summary>
        /// Registry value for TDU install location
        /// </summary>
        private const string _REGVALUE_TDU_INSTALL_PATH = @"install_path";

        /// <summary>
        /// Registry value for TDU install location
        /// </summary>
        private const string _REGVALUE_TDU_GAME_VERSION = @"Game_version";

        /// <summary>
        /// Registry data value for 1.45 TDU
        /// </summary>
        private const string _REGDATA_1_45_VERSION = @"VMC1.45A_MC1.2";

        /// <summary>
        /// Registry data value for 1.66 TDU
        /// </summary>
        private const string _REGDATA_1_66_VERSION = @"Patch 1.66A";

        /// <summary>
        /// Registry data value for 1.66 TDU + megapack
        /// </summary>
        private const string _REGDATA_1_66_MEGAPACK_VERSION = @"Patch 1.66A + Bonus Pack";
        #endregion

        #region Constants - error messages
        /// <summary>
        /// Message d'erreur de sauvegarde
        /// </summary>
        private const string _ERROR_BACKUP = "Backup of '{0}' to '{1}' failed.";

        /// <summary>
        /// Message d'erreur de sauvegarde : paramètres erronés.
        /// </summary>
        private const string _ERROR_BACKUP_PARAMS = "Backup command is not valid.";

        /// <summary>
        /// Message d'erreur de restauration
        /// </summary>
        private const string _ERROR_RESTORE = "Restoring of '{0}' from '{1}' failed.";

        /// <summary>
        /// Message d'erreur de sauvegarde : paramètres erronés.
        /// </summary>
        private const string _ERROR_RESTORE_PARAMS = "Restore command is not valid.";

        /// <summary>
        /// Error message when unable to find tdu folder in Windows registry.
        /// </summary>
        private const string _ERROR_SEARCH_TDU_LOCATION = "Unable to find registry information for TDU folder. Please install the game correctly or fix it with Modding Tools.";

        /// <summary>
        /// Error message when unable to find tdu version in Windows registry.
        /// </summary>
        private const string _ERROR_SEARCH_TDU_VERSION = "Unable to find information for TDU version. Please install the game correctly.";
        #endregion

        #region Properties
        /// <summary>
        /// TDU version
        /// </summary>
        public static TduVersion InstalledTduVersion
        {
            get
            {
                // Searches for TDU version again if version is still unknown
                if (_InstalledTduVersion == TduVersion.Unknown)
                    _InstalledTduVersion = _SearchForTduVersion();
                return _InstalledTduVersion;
            }
        }
        private static TduVersion _InstalledTduVersion = TduVersion.Unknown;

        /// <summary>
        /// Current working path, should be initialized when main application starts
        /// </summary>
        public static string WorkingPath { get; set; }

        /// <summary>
        /// Initial TDU install path. Can be overriden to a custom location by using setter.
        /// </summary>
        public static string TduPath
        {
            get
            {
                // Searches for TDU path again if path is still unknown
                if (string.IsNullOrEmpty(_TduPath))
                    _TduPath = _SearchForTduInstallPath();
                return _TduPath;
            }
            set
            {
                _TduPath = value;
            }
        }
        private static string _TduPath;
        #endregion

        #region Public methods
        /// <summary>
        /// Renvoie le checksum d'une séquence d'octets (type CRC-32).
        /// </summary>
        /// <param name="sequence">séquence pour laquelle le checksum doit être calculé</param>
        /// <param name="isTDUChecksum">true pour obtenir le checksum utilisé dans TDU, false sinon</param>
        /// <returns></returns>
        public static long GetChecksum(byte[] sequence, bool isTDUChecksum)
        {
            Crc32 crcObject = new Crc32();
            long crc = crcObject.CRC(sequence);

            if (isTDUChecksum)
                crc = ~crc;

            return crc;
        }

        /// <summary>
        /// Crée une copie de sauvegarde du fichier spécifié.
        /// Les dates de création/modification restent inchangées.
        /// </summary>
        /// <param name="fileName">Nom du fichier à sauvegarder</param>
        /// <param name="copyFileName">Nom de la copie</param>
        public static void BackupFile(string fileName, string copyFileName)
        {
            try
            {
                // Vérifications
                if (fileName == null || copyFileName == null || fileName.Equals(copyFileName))
                    throw new Exception(_ERROR_BACKUP_PARAMS);

                // Copie avec préservation de la date de modification
                FileInfo sourceInfo = new FileInfo(fileName);
                DateTime modDate = sourceInfo.LastWriteTime;

                File.Copy(fileName, copyFileName, true);
                File2.RemoveAttribute(copyFileName, FileAttributes.ReadOnly);

                new FileInfo(copyFileName) {LastWriteTime = modDate};

                File2.AddAttribute(copyFileName, FileAttributes.ReadOnly);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format(_ERROR_BACKUP, fileName, copyFileName);
                throw new Exception(errorMessage, ex);
            }
        }

        /// <summary>
        /// Restaure un fichier depuis la copie de sauvegarde
        /// </summary>
        /// <param name="backupFileName">Nom de la copie</param>
        /// <param name="originalFileName">Nom du fichier d'origine</param>
        public static void RestoreFile(string backupFileName, string originalFileName)
        {
            try
            {
                // Vérifications
                if (backupFileName == null || originalFileName == null || backupFileName.Equals(originalFileName))
                    throw new Exception(_ERROR_RESTORE_PARAMS);

                // Suppression du fichier actuel
                try
                {
                    File.SetAttributes(originalFileName, FileAttributes.Normal);
                    File.Delete(originalFileName);
                }
                catch (FileNotFoundException fnfEx)
                {
                    // ANO_26 : Si le fichier n'existe pas, on continue
                    Exception2.PrintStackTrace(fnfEx);
                }

                // Renommage de la sauvegarde
                File.Move(backupFileName, originalFileName);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format(_ERROR_RESTORE, originalFileName, backupFileName);
                throw new Exception(errorMessage, ex);
            }
        }

        /// <summary>
        /// Se charge de nettoyer cette saleté de radial.cdb
        /// Retourne la taille du fichier s'il est présent.
        /// </summary>
        /// <returns>La taille de radial.cdb, ou -1 s'il est absent</returns>
        public static long DeleteRadial()
        {
            string radialFileName = LibraryConstants.GetSpecialFile(LibraryConstants.TduSpecialFile.RadialCrap);
            long radialSize = -1;

            if (File.Exists(radialFileName))
            {
                radialSize = new FileInfo(radialFileName).Length;
                File.Delete(radialFileName);
            }

            return radialSize;
        }

        /// <summary>
        /// Transforme un nom d'objet ou de fichier de façon à ce qu'il respecte une longueur de 8 caractères maxi et son unicité.
        /// This transformation is used with 3D meshes, materials, texture names.
        /// </summary>
        /// <param name="objectName">nom à convertir, sans chemin s'il s'agit d'un fichier</param>
        /// <returns>Un nom unique, ou null s'il s'agit d'une chaîne vide ou nulle</returns>
        public static string NormalizeName(string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return null;

            // Récupération du nom sans extension
            string name = File2.GetNameFromFilename(objectName);

            if (name.Length <= 8)
            {
                // Length <= 8: string is filled with 0s
                for (int i = name.Length; i < 8; i++)
                    name += "\0";
                // BUG_: name is returned
                return name;
            }

            // Récupération des caractères dépassant le 8e
            char[] nameChars = name.Substring(0, 8).ToCharArray();
            char[] overFlow = name.Substring(8).ToCharArray();

            // Pour chaque caractère dépassant, on va ajouter sa valeur ASCII aux caractères de début de chaîne
            // EVO_84: found how to manage >16 characters : modulo 8
            for (int pos = 0; pos < overFlow.Length; pos++)
                nameChars[pos % 8] = (char)(nameChars[pos % 8] + overFlow[pos]);

            return String2.CharArrayToString(nameChars);
        }

        /// <summary>
        /// Met en évidence les caractères étendus dans un nom de fichier pour une présentation propre
        /// </summary>
        /// <param name="objectName">nom de l'objet</param>
        /// <returns>Le même nom, mais avec une présentation compréhensible des caractères étendus. Ou null si le nom est null ou vide.</returns>
        public static string OutlineExtendedCharacters(string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return null;

            // Détection des caractères étendus
            Collection<char> extendedChars = new Collection<char>();
            const int extendedThreshold = 'A' * 2;

            foreach (char c in objectName)
            {
                if (c >= extendedThreshold)
                    extendedChars.Add(c);
            }

            if (extendedChars.Count > 0)
            {
                // Construction de la description des caractères étendus
                const string extendedFormat = "[{0}]{1}";
                StringBuilder sb = new StringBuilder(extendedChars.Count);

                for (int i = 0; i < extendedChars.Count; i++)
                {
                    int currentValue = extendedChars[i];
                    sb.Append(currentValue);

                    if (i != extendedChars.Count - 1)
                        sb.Append(' ');
                }

                return string.Format(extendedFormat, sb, objectName.Substring(extendedChars.Count));
            }

            return objectName;
        }

        /// <summary>
        /// Repairs data into Windows registry (TDU install location and TDU version)
        /// </summary>
        /// <param name="tduInstallPath">Location where TDU is installed, e.g. C:\Program Files\Atari\Test Drive Unlimited</param>
        public static void FixRegistry(string tduInstallPath)
        {
            if (string.IsNullOrEmpty(tduInstallPath) || !Directory.Exists(tduInstallPath))
                throw new Exception("Install path is invalid: " + tduInstallPath);

            // Retrieving TDU version according to Bnk1.map file size
            TduVersion currentVersion = InstalledTduVersion;

            // Writing values into Windows registry
            try
            {
                string version = "";
                RegistryKey key = Registry.LocalMachine;

                // Main key
                // 64bit OS support
                string keyName = (System64.Is64BitOs() ?
                               _REGKEY_TDU_64
                               : _REGKEY_TDU_32);

                key = key.OpenSubKey(keyName, true);

                if (key == null)
                {
                    // 64bit OS support
                    key = Registry.LocalMachine.CreateSubKey(keyName);

                    if (key == null)
                        throw new Exception("TDU registry key cannot be created: " + keyName);
                }

                // Install path
                key.SetValue(_REGVALUE_TDU_INSTALL_PATH, tduInstallPath);

                // TDU version
                switch (currentVersion)
                {
                    case TduVersion.Unknown:
                    case TduVersion.V1_45:
                        version = _REGDATA_1_45_VERSION;
                        break;
                    case TduVersion.V1_66:
                        version = _REGDATA_1_66_VERSION;
                        break;
                    case TduVersion.V1_66_Megapack:
                        version = _REGDATA_1_66_MEGAPACK_VERSION;
                        break;
                }

                key.SetValue(_REGVALUE_TDU_GAME_VERSION, version);
            }
            catch (SecurityException sEx)
            {
                throw new UnauthorizedAccessException("You don't have rights to fix registry for TDU.", sEx);    
            }
            catch(UnauthorizedAccessException uaEx)
            {
                throw new UnauthorizedAccessException("You don't have rights to fix registry for TDU.", uaEx);                
            }
            catch (Exception ex)
            {
                throw new Exception("Internal error: unable to fix registry for TDU.", ex);
            }
        }

        /// <summary>
        /// Utility method extracting (id,value) couples from a string.
        /// Typical value pattern:  [id1]|[value1]||[id2]|[value2]||...
        /// </summary>
        /// <param name="values">string to extract couples from</param>
        /// <returns></returns>
        public static List<Couple<string>> ParseCouples(string values)
        {
            List<Couple<string>> couples = new List<Couple<string>>();

            if (values != null)
            {
                string[] coupleArray =
                    values.Split(new[] {SYMBOL_VALUE_SEPARATOR2}, StringSplitOptions.RemoveEmptyEntries);

                foreach (string couple in coupleArray)
                {
                    string[] array = couple.Split(SYMBOL_VALUE_SEPARATOR);

                    if (array.Length == 2)
                    {
                        Couple<string> currentCouple = new Couple<string>(array[0], array[1]);

                        couples.Add(currentCouple);
                    }
                }
            }

            return couples;
        }

        /// <summary>
        /// Renames specified file with specified suffix if it already exists
        /// </summary>
        /// <param name="fileName">Name of file to check</param>
        /// <param name="suffix">Suffix to use when renaming</param>
        public static void RenameIfNecessary(string fileName, string suffix)
        {
            if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(suffix))
            {
                string originalFileName = fileName.Clone() as string;

                while (File.Exists(fileName))
                    fileName += suffix;

                if (originalFileName != null && !fileName.Equals(originalFileName))
                    File.Move(originalFileName, fileName);
            }
        }

        /// <summary>
        /// Activates debug mode by creating a file + console appender
        /// </summary>
        /// <param name="debugLogFile"></param>
        public static void EnableDebugMode(string debugLogFile)
        {
            if (!string.IsNullOrEmpty(debugLogFile))
            {
                ILogAppender debugAppender = new FileAppender(debugLogFile);

                Log.GlobalAppenders.Add(debugAppender);

                // Console
                ILogAppender consoleAppender = new ConsoleAppender();

                Log.GlobalAppenders.Add(consoleAppender);
            }
        }
      
        /// <summary>
        /// Returns all player profile names located into Documents\TDU folder
        /// </summary>
        /// <returns></returns>
        public static string[] GetPlayerProfiles()
        {
            string savegamePath = LibraryConstants.GetSpecialFolder(LibraryConstants.TduSpecialFolder.Savegame);
            DirectoryInfo savegameDirectory = new DirectoryInfo(savegamePath);

            if (savegameDirectory.Exists)
            {
                // Quick and easy way: take all subfolders as there should not be any custom folder here...
                DirectoryInfo[] subDirectories = savegameDirectory.GetDirectories();
                string[] returnedProfiles = new string[subDirectories.Length];
                int i = 0;

                foreach (DirectoryInfo anotherSubDir in subDirectories)
                    returnedProfiles[i++] = anotherSubDir.Name;

                return returnedProfiles;
            }

            throw new Exception("Unable to locate TDU savegames: " + savegamePath);
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Tries to retrieve TDU install folder by seeking into Windows registry.
        /// </summary>
        /// <returns>TDU install location or an empty string if not found.</returns>
        internal static string _SearchForTduInstallPath()
        {
            string installPath = "";

            try
            {
                RegistryKey key = Registry.LocalMachine;

                // 64bit OS support
                string keyName = (System64.Is64BitOs() ?
                               _REGKEY_TDU_64
                               : _REGKEY_TDU_32);

                key = key.OpenSubKey(keyName);

                if (key == null)
                    throw new Exception("TDU registry key not found under LOCAL_MACHINE: " + keyName);

                installPath = key.GetValue(_REGVALUE_TDU_INSTALL_PATH) as string;
            }
            catch (Exception ex)
            {
                Exception2.PrintStackTrace(new Exception(_ERROR_SEARCH_TDU_LOCATION, ex));
            }

            return installPath ?? "";
        }

        /// <summary>
        /// Tries to retrieve TDU version by seeking into Windows registry first, or by alternative ways if it fails.
        /// </summary>
        /// <returns>TDU installed version or Unknown value if not found.</returns>
        internal static TduVersion _SearchForTduVersion()
        {
            TduVersion installedVersion = TduVersion.Unknown;

            // Registry search
            try
            {
                RegistryKey key = Registry.LocalMachine;

                // 64bit OS support
                string keyName = (System64.Is64BitOs() ?
                               _REGKEY_TDU_64
                               : _REGKEY_TDU_32);

                key = key.OpenSubKey(keyName);

                if (key == null)
                    throw new Exception("TDU registry key not found under LOCAL_MACHINE: " + keyName);

                string version = key.GetValue(_REGVALUE_TDU_GAME_VERSION) as string;

                if (version == null)
                    throw new Exception("TDU version value not found: " + _REGVALUE_TDU_GAME_VERSION);
                
                switch (version)
                {
                    case _REGDATA_1_45_VERSION:
                        installedVersion = TduVersion.V1_45;
                        break;
                    case _REGDATA_1_66_VERSION:
                        installedVersion = TduVersion.V1_66;
                        break;
                    case _REGDATA_1_66_MEGAPACK_VERSION:
                        installedVersion = TduVersion.V1_66_Megapack;
                        break;
                    default:
                        throw new Exception("Invalid version data in registry: " + installedVersion);
                }
            }
            catch (Exception ex)
            {
                // Silent
                Exception2.PrintStackTrace(new Exception(_ERROR_SEARCH_TDU_VERSION, ex));
            }

            // Alternative ways...
            if (installedVersion == TduVersion.Unknown)
            {
                Log.Info("Searching TDU version from Bnk1.map file size...");

                try
                {
                    // Searching for version according to Bnk1.map file size
                    installedVersion = _SearchForTduVersionFromMap();
                }
                catch (Exception ex)
                {
                    // Silent
                    Exception2.PrintStackTrace(new Exception(_ERROR_SEARCH_TDU_VERSION, ex));
                }
            }

            // If nothing works...
            if (installedVersion == TduVersion.Unknown)
                Log.Warning(_ERROR_SEARCH_TDU_VERSION);

            return installedVersion;
        }

        /// <summary>
        /// Tries to retrieve TDU version by looking at Bnk1.map file size.
        /// </summary>
        /// <returns>TDU installed version or UNKNOWN value if not found.</returns>
        private static TduVersion _SearchForTduVersionFromMap()
        {
            TduVersion installedVersion = TduVersion.Unknown;

            // Locating Bnk1.map file
            string mapFileName = LibraryConstants.GetSpecialFile(LibraryConstants.TduSpecialFile.BnkMap);

            // DEBUG
            Log.Info("_SearchForTduVersionFromMap, Bnk1.map:" + mapFileName);

            // According to MAP size...
            FileInfo mapInfo = new FileInfo(mapFileName);

            if (mapInfo.Exists)
            {
                long mapSize = mapInfo.Length;

                Log.Info("Bnk1.map file found, size=" + mapSize);

                if (mapSize < MAP.SIZE_1_66_MAP)
                    installedVersion = TduVersion.V1_45;
                else if (mapSize < MAP.SIZE_1_66_MEGAPACK_MAP)
                    installedVersion = TduVersion.V1_66;
                else
                    installedVersion = TduVersion.V1_66_Megapack;
            }

            return installedVersion;
        }
        #endregion

    }
}
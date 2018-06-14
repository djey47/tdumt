using System;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.fileformats.binaries;
using TDUModdingLibrary.fileformats.xml;

namespace TDUModdingLibrary.support.constants
{
    /// <summary>
    /// Static class providing misc constants
    /// </summary>
    public static class LibraryConstants
    {
        #region Types
        /// <summary>
        /// Represents all TDU's special folders
        /// </summary>
        public enum TduSpecialFolder
        {
            AvatarClothes,
            Bnk,
            Data_DFE,
            Database,
            FrontEndAllRes,
            FrontEndHiRes,
            FX,
            GaugesLow,
            GaugesHigh,
            LevelHawai,
            Savegame,
            VehicleTraffic,
            VehicleModels,
            VehicleRims,
            VehicleSounds
        }

        /// <summary>
        /// Represents all TDU's special folders
        /// </summary>
        public enum TduSpecialFile
        {
            RadialCrap,
            BnkMap,
            CamerasBin,
            AIConfig
        }
        #endregion

        #region Constants - file names
        /// <summary>
        /// Nom du fichier radial
        /// </summary>
        public const string FILE_RADIAL = @"\radial.cdb";

        /// <summary>
        /// Nom du fichier exécutable Test Drive Unlimited
        /// </summary>
        public const string FILE_TDU_EXE = @"\TestDriveUnlimited.exe";

        /// <summary>
        /// Default patch file name for install with M&P!
        /// </summary>
        public const string FILE_PATCH_INSTALL = @"\install.pch";

        /// <summary>
        /// Default patch file for uninstall with M&P!
        /// </summary>
        public const string FILE_PATCH_UNINSTALL = @"\uninstall.pch";

        /// <summary>
        /// File name pour patch log
        /// </summary>
        public const string FILE_LOG_PATCH = @"\patch.log";

        /// <summary>
        /// Debug log file name for MoadAndPlay! installer
        /// </summary>
        public const string FILE_MNP_LOG_DEBUG = @"\ModAndPlayDebug.log";

        /// <summary>
        /// File name for background picture
        /// </summary>
        public const string FILE_MNP_BACKGROUND_PICTURE = @"\image.png";

        /// <summary>
        /// File name suffix for old files
        /// </summary>
        public const string SUFFIX_OLD_FILE = "_old";
        #endregion

        #region Constants - Folder names
        /// <summary>
        /// Nom du dossier utilisé dans Application Data (stockage clés, friends, radial...)
        /// </summary>
        public const string FOLDER_APPLICATION_DATA = @"\Test Drive Unlimited\";

        /// <summary>
        /// Nom du dossier de données pour TDU
        /// </summary>
        public const string FOLDER_BNK = @"\Euro\Bnk\";

        /// <summary>
        /// Folder name for tracks
        /// </summary>
        public const string FOLDER_DATA_DFE = @"\Data_DFE\";

        /// <summary>
        /// Nom du dossier de database pour TDU
        /// </summary>
        public const string FOLDER_DB = FOLDER_BNK + @"Database\";

        /// <summary>
        /// Vehicle models folder name
        /// </summary>
        public const string FOLDER_VEHICLES_MODELS = FOLDER_BNK + @"Vehicules\";

        /// <summary>
        /// Hawai folder name
        /// </summary>
        public const string FOLDER_LEVEL_HAWAI = FOLDER_BNK + @"Level\Hawai\";

        /// <summary>
        /// Vehicle rims parent folder name
        /// </summary>
        public const string FOLDER_PARENT_VEHICLES_RIMS = FOLDER_VEHICLES_MODELS + @"Rim\";

        /// <summary>
        /// Traffic vehicles parent folder name
        /// </summary>
        public const string FOLDER_PARENT_VEHICLES_TRAFFIC = FOLDER_VEHICLES_MODELS + @"Traffic\";

        /// <summary>
        /// Vehicle gauges (lowres) folder name
        /// </summary>
        public const string FOLDER_VEHICLES_GAUGES_LOW = FOLDER_BNK + @"FrontEnd\LowRes\Gauges\";

        /// <summary>
        /// Vehicle gauges (highres) folder name
        /// </summary>
        public const string FOLDER_VEHICLES_GAUGES_HIGH = FOLDER_BNK + @"FrontEnd\HiRes\Gauges\";

        /// <summary>
        /// Vehicle sounds folder name
        /// </summary>
        public const string FOLDER_VEHICLES_SOUNDS = FOLDER_BNK + @"Sound\Vehicules\";

        /// <summary>
        /// Common frontend textures folder name
        /// </summary>
        public const string FOLDER_FRONTEND_ALLRES = FOLDER_BNK + @"FrontEnd\AllRes\";

        /// <summary>
        /// Menus highres textures folder name
        /// </summary>
        public const string FOLDER_FRONTEND_HIRES = FOLDER_BNK + @"FrontEnd\HiRes\";

        /// <summary>
        /// Avatar clothes folder name
        /// </summary>
        public const string FOLDER_AVATAR_CLOTHES = FOLDER_BNK + @"Avatar\CLOTHES\";

        /// <summary>
        /// Avatar clothes folder name
        /// </summary>
        public const string FOLDER_FX = FOLDER_BNK + @"FX\";

        /// <summary>
        /// Install default folder
        /// </summary>
        public const string FOLDER_DEFAULT_INSTALL = @"C:\Program Files\Atari\Test Drive Unlimited\";

        /// <summary>
        /// System temporary folder for modding operations
        /// </summary>
        public const string FOLDER_TEMP = @"\TduModdingLibrary\";

        /// <summary>
        /// Folder for reference XML data
        /// </summary>
        public const string FOLDER_XML = @"\xml\";

        /// <summary>
        /// Folder for default files in reference
        /// </summary>
        public const string FOLDER_DEFAULT = @"\default\";

        /// <summary>
        /// Folder for original challenges
        /// </summary>
        public const string FOLDER_CHALLENGES = @"\challenges\";

        /// <summary>
        /// Folder for patch storage
        /// </summary>
        public const string FOLDER_PATCHS = @"\patchs\";

        /// <summary>
        /// Folder name for mods storage
        /// </summary>
        public const string FOLDER_MODS = @"\Mods\";

        /// <summary>
        /// Folder name for logo picture storage
        /// </summary>
        public const string FOLDER_LOGOS = @"\Logos\";

        /// <summary>
        /// Folder name for custom tracks storage
        /// </summary>
        public const string FOLDER_TRACKS = @"\tracks\";

        /// <summary>
        /// Folder name for IGE tracks storage
        /// </summary>
        public const string FOLDER_IGE = @"\IGE\";

        /// <summary>
        /// Savegames parent folder
        /// </summary>
        public const string FOLDER_PARENT_SAVEGAME = @"\Test Drive Unlimited\savegame";
        #endregion

        #region Constants - URLs
        /// <summary>
        /// Offline documentation URL (TDUMT and general modding)
        /// </summary>
        public const string URL_OFFLINE_DOC = @"http://bit.ly/qniFQd";
        #endregion

        #region File extensions
        /// <summary>
        /// Extension d'une copie de sauvegarde
        /// </summary>
        public const string EXTENSION_BACKUP = "BAK";

        /// <summary>
        /// Extension des fichiers sonores
        /// </summary>
        public const string EXTENSION_WAV_FILE = "WAV";

        /// <summary>
        /// Extension des fichiers de config INI
        /// </summary>
        public const string EXTENSION_INI_FILE = "INI";

        /// <summary>
        /// Extension des fichiers de config BIN
        /// </summary>
        public const string EXTENSION_BIN_FILE = "BIN";

        /// <summary>
        /// Extension des fichiers XML
        /// </summary>
        public const string EXTENSION_XML_FILE = "XML";

        /// <summary>
        /// Extension des fichiers de données pour les stations de radio
        /// </summary>
        public const string EXTENSION_STATION_FILE = "STATION";

        /// <summary>
        /// Extension de fichiers de materials
        /// </summary>
        public const string EXTENSION_MATERIAL_FILE = "2DM";

        /// <summary>
        /// Extension de fichiers de descriptions de mesh
        /// </summary>
        public const string EXTENSION_3D_DESCRIPTION_FILE = "3DD";

        /// <summary>
        /// Extension de fichiers de géométrie
        /// </summary>
        public const string EXTENSION_3D_GEOMETRY_FILE = "3DG";

        /// <summary>
        /// Extension of backup copy created by Vehicle Manager
        /// </summary>
        public const string EXTENSION_VEHICLE_MANAGER_BACKUP = "VMBAK";

        /// <summary>
        /// Extension for Portable Network Graphics image file
        /// </summary>
        public const string EXTENSION_PNG_FILE = "PNG";

        /// <summary>
        /// Extension for windows executable files
        /// </summary>
        public const string EXTENSION_EXE_FILE = "EXE";

        /// <summary>
        /// Extension for text files
        /// </summary>
        public const string EXTENSION_TXT_FILE = "TXT";

        /// <summary>
        /// Extension for encrypted database fies
        /// </summary>
        public const string EXTENSION_DB_FILE = "DB";

        /// <summary>
        /// Extension for direct draw surface files
        /// </summary>
        public const string EXTENSION_DDS_FILE = "DDS";

        /// <summary>
        /// Extension for TDU archives
        /// </summary>
        public const string EXTENSION_BNK_FILE = "BNK";

        /// <summary>
        /// Extension for 2DB backups
        /// </summary>
        public const string EXTENSION_2DB_OLD_FILE = "OLDB";

        /// <summary>
        /// Extension for 2DB texture files
        /// </summary>
        public const string EXTENSION_2DB_FILE = "2DB";

        /// <summary>
        /// Extension for patches
        /// </summary>
        public const string EXTENSION_PATCH_FILE = "PCH";

        /// <summary>
        /// Extension for file mapping data
        /// </summary>
        public const string EXTENSION_MAP_FILE = "MAP";

        /// <summary>
        /// Extension des fichiers XMB
        /// </summary>
        public const string EXTENSION_XMB_FILE = "XMB";

        /// <summary>
        /// Extension for TDU editor tracks
        /// </summary>
        public const string EXTENSION_IGE_FILE = "IGE";
        #endregion

        #region File wildcards filters
        /// <summary>
        /// Filter for DFE track files
        /// </summary>
        public const string FILTER_DFE = "data_dfe_*";
        #endregion

        #region Properties
        #endregion

        #region Public methods
        /// <summary>
        /// Returns real tdu folder path
        /// </summary>
        /// <param name="folderType"></param>
        /// <returns></returns>
        public static string GetSpecialFolder(TduSpecialFolder folderType)
        {
            string returnedFolder = null;

            switch (folderType)
            {
                case TduSpecialFolder.AvatarClothes:
                    returnedFolder = string.Concat(Tools.TduPath,
                                                    FOLDER_AVATAR_CLOTHES);
                    break;
                case TduSpecialFolder.Bnk:
                    returnedFolder = string.Concat(Tools.TduPath,
                                                    FOLDER_BNK);
                    break;
                case TduSpecialFolder.Database:
                    returnedFolder = string.Concat(Tools.TduPath,
                                                    FOLDER_DB);
                    break;
                case TduSpecialFolder.FrontEndAllRes:
                    returnedFolder = string.Concat(Tools.TduPath,
                                                    FOLDER_FRONTEND_ALLRES);
                    break;
                case TduSpecialFolder.FrontEndHiRes:
                    returnedFolder = string.Concat(Tools.TduPath,
                                                    FOLDER_FRONTEND_HIRES);
                    break;
                case TduSpecialFolder.GaugesHigh:
                    returnedFolder = string.Concat(Tools.TduPath,
                                                   FOLDER_VEHICLES_GAUGES_HIGH);
                    break;
                case TduSpecialFolder.GaugesLow:
                    returnedFolder = string.Concat(Tools.TduPath,
                                                   FOLDER_VEHICLES_GAUGES_LOW);
                    break;
                case TduSpecialFolder.LevelHawai:
                    returnedFolder = string.Concat(Tools.TduPath,
                                                   FOLDER_LEVEL_HAWAI);
                    break;
                case TduSpecialFolder.VehicleTraffic:
                    returnedFolder = string.Concat(Tools.TduPath,
                                                    FOLDER_PARENT_VEHICLES_TRAFFIC);
                    break;
                case TduSpecialFolder.VehicleModels:
                    returnedFolder = string.Concat(Tools.TduPath,
                                                   FOLDER_VEHICLES_MODELS);
                    break;
                case TduSpecialFolder.VehicleRims:
                    returnedFolder = string.Concat(Tools.TduPath,
                                                    FOLDER_PARENT_VEHICLES_RIMS);
                    break;
                case TduSpecialFolder.FX:
                    returnedFolder =  string.Concat(Tools.TduPath,
                                                    FOLDER_FX);
                    break;
                case TduSpecialFolder.Savegame:
                    string myDocsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    returnedFolder = string.Concat(myDocsFolder, FOLDER_PARENT_SAVEGAME);
                    break;
                case TduSpecialFolder.Data_DFE:
                    returnedFolder = string.Concat(Tools.TduPath,
                               FOLDER_DATA_DFE);
                    break;
            }

            return returnedFolder;
        }

        /// <summary>
        /// Returns real tdu file path
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public static string GetSpecialFile(TduSpecialFile fileType)
        {
            string returnedFile = null;

            switch (fileType)
            {
                case TduSpecialFile.RadialCrap:
                    returnedFile = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                        FOLDER_APPLICATION_DATA,
                        FILE_RADIAL);
                    break;
                case TduSpecialFile.BnkMap:
                    returnedFile = string.Concat(GetSpecialFolder(TduSpecialFolder.Bnk),
                        MAP.FILE_MAP);
                    break;
                case TduSpecialFile.CamerasBin:
                    returnedFile = string.Concat(GetSpecialFolder(TduSpecialFolder.Database),
                        Cameras.FILE_CAMERAS_BIN);
                    break;
                case TduSpecialFile.AIConfig:
                    returnedFile = string.Concat(GetSpecialFolder(TduSpecialFolder.Database), 
                        AIConfig.FILE_AICONFIG_XML);
                    break;
            }

            return returnedFile;
        }
        #endregion
    }
}
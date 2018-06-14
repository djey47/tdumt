namespace TDUModdingTools.gui.filebrowser
{
    /// <summary>
    /// REsources constants for FileBrowser
    /// </summary>
    public partial class FileBrowserForm
    {
        /// <summary>
        /// File Browser : libellé pour barre de statut : pas de fichier BNK sélectionné
        /// </summary>
        private const string _STATUS_NO_BNK_SELECTED = "No BNK file selected. You may also load from any location by using 'Load...' button.";

        /// <summary>
        /// Label for status bar : file count and size
        /// </summary>
        private const string _STATUS_COUNT_SIZE_CONTENTS = ": {0} files - {1} bytes";

        /// <summary>
        /// Label for status bar : file count and size
        /// </summary>
        private const string _STATUS_COUNT_SIZE_NO = ": no file in this folder.";

        /// <summary>
        /// File Browser : libellé pour barre de statut : infos sur le fichier BNK sélectionné
        /// </summary>
        private const string _STATUS_BNK_CONTENTS = "{0} {1}";

        /// <summary>
        /// File Browser: status bar label : bnk file is currently being loaded
        /// </summary>
        private const string _STATUS_BNK_LOADING = "Loading BNK file, please wait...";

        /// <summary>
        /// File Browser : libellé pour l'info bulle sur un fichier empaqueté
        /// </summary>
        private const string _TOOLTIP_PACKED_FILE = "{0} - {1} bytes {2}";

        /// <summary>
        /// File Browser : libellé pour l'info bulle sur un fichier 'libre'
        /// </summary>
        private const string _TOOLTIP_STD_FILE = "{0} - {1} bytes";

        /// <summary>
        /// File Browser : libellé pour l'info bulle sur un fichier empaqueté
        /// </summary>
        private const string _TOOLTIP_PACKED_FILE_MODIFIED = " (modified)";

        /// <summary>
        /// éditer
        /// </summary>
        private const string _WORD_EDIT = "edit";

        /// <summary>
        /// ouvrir
        /// </summary>
        private const string _WORD_OPEN = "open";

        /// <summary>
        /// Libellé de la racine de l'arborescence
        /// </summary>
        private const string _TREENODE_ROOT = @"\";

        /// <summary>
        /// Message d'avertissement lors de la création d'une tâche qui existe déjà.
        /// </summary>
        private const string _WARNING_TASK_EXISTS = "This edit task already exists.\r\nPlease discard or apply changes first.";

        /// <summary>
        /// Warning message when attempting to delete a packed file
        /// </summary>
        private const string _WARNING_DELETION = "Deleting packed file(s) is an advanced feature and can't be undone.";

        /// <summary>
        /// Message d'erreur de création de dossier temporaire
        /// </summary>
        private const string _ERROR_CREATE_TEMP_FOLDER = "Unable to create temporary folder on disk.";

        /// <summary>
        /// Message d'erreur avant sauvegarde d'une sauvegarde
        /// </summary>
        private const string _ERROR_BACKUP_OF_BACKUP = "File is already a backup:\r\n{0}";

        /// <summary>
        /// Message d'erreur si tentative de restauration depuis un fichier qui n'est pas une sauvegarde
        /// </summary>
        private const string _ERROR_NOT_A_BACKUP = "File is not a backup:\r\n{0}";

        /// <summary>
        /// Message d'erreur si le chemin de TDU dans les paramètres est invalide
        /// </summary>
        private const string _ERROR_INVALID_TDU_PATH = "TDU main folder not set properly. Please check your settings.";

        /// <summary>
        /// Message d'erreur si l'édition a échoué
        /// </summary>
        private const string _ERROR_SHOW_OR_EDIT_FAILED = "Unable to {0} this '{1}' file.";

        /// <summary>
        /// Error message when packed file properties are not available
        /// </summary>
        private const string _ERROR_PACK_PROPERTIES_FAILED = "Unable to display properties for packed file: {0}.";

        /// <summary>
        /// Message d'erreur si le chargement d'un BNK a échoué
        /// </summary>
        private const string _ERROR_LOAD_BNK_FAILED = "Unable to load '{0}'.";

        /// <summary>
        /// Message d'erreur si la sauvegarde de paramètres a échoué
        /// </summary>
        private const string _ERROR_SAVING_SETTINGS = "File Browser settings couldn't be saved.";

        /// <summary>
        /// Error message when BNK data is invalid
        /// </summary>
        private const string _ERROR_INVALID_DATA = "Internal BNK data is invalid. Please re-load it.";

        /// <summary>
        /// Question posée avant la restauration d'un fichier
        /// </summary>
        private const string _QUESTION_RESTORE = "Restore original file:\r\n{0}\r\nfrom backup:\r\n{1} ?";

        /// <summary>
        /// Asked question before sending many files to trash
        /// </summary>
        private const string _QUESTION_DELETE_FILES = "Really send these {0} items to recycle bin?";

        /// <summary>
        /// Question posée avant la désactivition du système de contrôle
        /// </summary>
        private const string _QUESTION_DISABLE_SFC = "This will patch your Bnk1.map to prevent TDU from checking file sizes. Side-effects are not known yet, so be careful !";

        /// <summary>
        /// Question after a successful file extraction
        /// </summary>
        /*private const string _QUESTION_BROWSE_EXTRACTED_FILES =
            "File(s) extracted successfully.\r\nDisplay with Explorer ?";*/

        /// <summary>
        /// Message de statut après remplacement de plusieurs fichiers.
        /// </summary>
        private const string _STATUS_REPLACE_SUCCESS_MANY = "All {0} packed file(s) successfully replaced. BNK file saved.";

        /// <summary>
        /// Message de statut après remplacement de plusieurs fichiers.
        /// </summary>
        private const string _STATUS_REPLACE_RENAME_SUCCESS_MANY = "All {0} packed file(s) successfully replaced and renamed. BNK file saved.";

        /// <summary>
        /// Message de statut après remplacement d'un fichier.
        /// </summary>
        private const string _STATUS_REPLACE_SUCCESS_SINGLE = "File '{0}' was replaced. BNK file saved.";

        /// <summary>
        /// Message de statut après remplacement
        /// </summary>
        private const string _STATUS_REPLACE_FAILED = "Warning ! File(s) couldn't be repacked.";

        /// <summary>
        /// Message de statut après remplacement
        /// </summary>
        private const string _STATUS_EDIT_SUCCESS = "{0} edit task(s) successfully created.";

        /// <summary>
        /// Message de statut pendant la désactivation
        /// </summary>
        private const string _STATUS_DISABLING_FSC = "Patching Bnk1.map file, please wait...";

        /// <summary>
        /// Message de statut après désactivation
        /// </summary>
        private const string _STATUS_FSC_DISABLED = "File Size Control disabled. Backup: Bnk1.map.bak.";

        /// <summary>
        /// Message de statut après désactivation ratée
        /// </summary>
        private const string _STATUS_FSC_FAILED = "Unable to disable File Size Control.";

        /// <summary>
        /// Status message after succesfull renaming
        /// </summary>
        private const string _STATUS_RENAME_SUCCESS = "Packed file succesfully renamed.";

        /// <summary>
        /// Status message after succesfull deletion
        /// </summary>
        private const string _STATUS_DELETE_PACKED_SUCCESS = "Packed file(s) succesfully deleted.";

        /// <summary>
        /// Titre de boîte de dialogue pour sélection d'un fichier de remplacement (dans un BNK)
        /// </summary>
        private const string _TITLE_REPLACE_SELECTION = "Please select a file to replace '{0}'";

        /// <summary>
        /// Dialog box title for selecting a Bnk file to load
        /// </summary>
        private const string _TITLE_OPEN_BNK = "Please select a file to load into Bnk Manager";

        /// <summary>
        /// Label for folder browsing dialog box (BNK contents extract)
        /// </summary>
        private const string _LABEL_FOLDER_BROWSE_EXTRACT = "Please select where to extract {0} packed file(s):";

        /// <summary>
        /// Tooltip for tree view packed folder
        /// </summary>
        private const string _TOOLTIP_FOLDER = "Packed folder";

        /// <summary>
        /// Tooltip for tree view packed extension groupe
        /// </summary>
        private const string _TOOLTIP_EXTENSION_GROUP = "Packed extension group";
    }
}
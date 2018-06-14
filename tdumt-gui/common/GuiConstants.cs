using System.Drawing;

namespace TDUModdingTools.common
{
    /// <summary>
    /// Constantes partagées pour l'IHM
    /// </summary>
    internal class GuiConstants
    {
        #region File selection filters
        /// <summary>
        /// Format string for file selection filter
        /// </summary>
        private const string _FORMAT_SELECTION_FILTER = "{0} (*.{1})|*.{1}";

        /// <summary>
        /// Filtre pour tous les fichiers
        /// </summary>
        public const string FILTER_ALL_FILES = "All files|*.*";

        /// <summary>
        /// Filtre pour les fichiers BNK ainsi que les autres
        /// </summary>
        public const string FILTER_BNK_ALL_FILES = "TDU Bank (*.bnk)|*.BNK|All files|*.*";

        /// <summary>
        /// Filter for BNK files only
        /// </summary>
        public const string FILTER_BNK_FILES = "TDU Bank (*.bnk)|*.BNK";

        /// <summary>
        /// Filtre pour les fichiers MAP ainsi que les autres
        /// </summary>
        public const string FILTER_MAP_ALL_FILES = "TDU File Mapper (*.map)|*.MAP|All files|*.*";

        /// <summary>
        /// Filtre pour les fichiers 2DB ainsi que les autres
        /// </summary>
        public const string FILTER_2DB_ALL_FILES = "TDU Texture (*.2db)|*.2DB|All files|*.*";

        /// <summary>
        /// Filtre pour les fichiers DDS ainsi que les autres
        /// </summary>
        public const string FILTER_DDS_ALL_FILES = "DirectDraw Surface (*.dds)|*.DDS|All files|*.*";

        /// <summary>
        /// Filtre pour les fichiers XML ainsi que les autres
        /// </summary>
        public const string FILTER_XML_ALL_FILES = "XML File (*.xml)|*.XML|All files|*.*";

        /// <summary>
        /// Filter for XML, PCH files, and others
        /// </summary>
        public const string FILTER_XML_PCH_ALL_FILES = "XML File (*.xml)|*.XML|TDUMT patch (*.pch)|*.PCH|All files|*.*";

        /// <summary>
        /// Filtre pour les fichiers de couleurs
        /// </summary>
        public const string FILTER_CAR_COLORS_DB_ALL_FILES = "French Data|TDU_CarColors.fr|Chinese Data|TDU_CarColors.ch|English Data|TDU_CarColors.us|German Data|TDU_CarColors.ge|Italian Data|TDU_CarColors.it|Japanese Data|TDU_CarColors.jp|Korean Data|TDU_CarColors.ko|Spanish Data|TDU_CarColors.sp|All files|*.*";

        /// <summary>
        /// Filter for brand data files
        /// </summary>
        public const string FILTER_BRANDS_DB_ALL_FILES = "French Data|TDU_Brands.fr|Chinese Data|TDU_Brands.ch|English Data|TDU_Brands.us|German Data|TDU_Brands.ge|Italian Data|TDU_Brands.it|Japanese Data|TDU_Brands.jp|Korean Data|TDU_Brands.ko|Spanish Data|TDU_Brands.sp|All files|*.*";

        /// <summary>
        /// Filter for physics data files
        /// </summary>
        public const string FILTER_CAR_PHYSICS_DB_ALL_FILES = "French Data|TDU_CarPhysicsData.fr|Chinese Data|TDU_CarPhysicsData.ch|English Data|TDU_CarPhysicsData.us|German Data|TDU_CarPhysicsData.ge|Italian Data|TDU_CarPhysicsData.it|Japanese Data|TDU_CarPhysicsData.jp|Korean Data|TDU_CarPhysicsData.ko|Spanish Data|TDU_CarPhysicsData.sp|All files|*.*";

        /// <summary>
        /// Filtre pour les fichiers de bases de données multilingues ainsi que les autres
        /// </summary>
        public const string FILTER_DB_ALL_FILES = "French Data|*.FR|Chinese Data|*.CH|English Data|*.US|German Data|*.GE|Italian Data|*.IT|Japanese Data|*.JP|Korean Data|*.KO|Spanish Data|*.SP|All files|*.*";

        /// <summary>
        /// Filtre pour les fichiers XMB ou WAV uniquement
        /// </summary>
        public const string FILTER_XMB_WAV_FILES = "Sound file (*.wav)|*.WAV|Sound processing data file (*.xmb)|*.XMB|Supported files|*.WAV;*.XMB";

        /// <summary>
        /// Filter for PCH files and all others
        /// </summary>
        public const string FILTER_PCH_ALL_FILES = "Patch File (*.pch)|*.PCH|All files|*.*";

        /// <summary>
        /// Filter for PCH files and all others
        /// </summary>
        public const string FILTER_PNG_FILES = "Image File (*.png)|*.PNG";

        /// <summary>
        /// Filter for DFE files only
        /// </summary>
        public const string FILTER_DFE_FILES = "TDU challenges|data_dfe_*";

        /// <summary>
        /// Filter for DFE or IGE files
        /// </summary>
        public const string FILTER_DFE_IGE_FILES = "TDU custom tracks|*.IGE|" + FILTER_DFE_FILES;
        #endregion

        #region Colors
        /// <summary>
        /// Couleur des éléments modifiés
        /// </summary>
        public static Color COLOR_MODIFIED_ITEM = Color.Blue;

        /// <summary>
        /// Couleur des éléments invalides
        /// </summary>
        public static Color COLOR_INVALID_ITEM = Color.Red;

        /// <summary>
        /// Couleur des éléments valides
        /// </summary>
        public static Color COLOR_VALID_ITEM = Color.Green;

        /// <summary>
        /// Couleur de fond des éléments de commentaires
        /// </summary>
        public static Color COLOR_BACK_COMMENT_ITEM = Color.SeaGreen;

        /// <summary>
        /// Couleur de texte des éléments de commentaires
        /// </summary>
        public static Color COLOR_FRONT_COMMENT_ITEM = Color.White;

        /// <summary>
        /// Text color for disabled items
        /// </summary>
        public static Color COLOR_DISABLED_ITEM = Color.LightGray;

        /// <summary>
        /// Text color for items in conflict
        /// </summary>
        public static Color COLOR_CONFLICTED_ITEM = Color.Orange;
        #endregion

        #region Symbols
        /// <summary>
        /// Modified file symbol
        /// </summary>
        public const string SYMBOL_MODIFIED_FILE = "*";
        #endregion

        #region Public methods
        /// <summary>
        /// Returns custom selection filter according to given file extension and description
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static string MakeSelectionFilter(string extension, string description)
        {
            string returnedFilter = "";

            if (!string.IsNullOrEmpty(extension) && description != null)
                returnedFilter = string.Format(_FORMAT_SELECTION_FILTER, description, extension);

            return returnedFilter;
        }
        #endregion
    }
}
namespace TDUModdingLibrary.fileformats.database.util
{
    /// <summary>
    /// Class providing shared constants for database access
    /// </summary>
    public class DatabaseConstants
    {
        #region Shared column names
        /* COMMON */
        /// <summary>
        /// REF table column
        /// </summary>
        public const string REF_DB_COLUMN = "REF";

        /* CAR SHOPS */
        /// <summary>
        /// SLOT_? pattern car shops table column
        /// </summary>
        public const string SLOT_CARSHOPS_PATTERN_DB_COLUMN = "SLOT_{0}";
        #endregion

        #region Column names
        /* CAR PHYSICS */
        /// <summary>
        /// Camera physics table column
        /// </summary>
        public const string CAMERA_PHYSICS_DB_COLUMN = "Camera";

        /// <summary>
        /// Same_IK physics table column
        /// </summary>
        public const string SAME_IK_PHYSICS_DB_COLUMN = "Same_IK";
        #endregion

        #region Known database values
        #endregion

        #region Known database resource identifiers
        /* CAR PHYSICS */
        /// <summary>
        /// Reference for N/A name in physics
        /// </summary>
        public const string NOT_AVAILABLE_NAME_PHYSICS_DB_RESID = "53338427";

        /// <summary>
        /// Reference for Compact1 vehicle model in physics
        /// </summary>
        public const string COMPACT1_PHYSICS_DB_RESID = "61085282";

        /* BRANDS */
        /// <summary>
        /// Reference for N:1 name in brands
        /// </summary>
        public const string NOT_AVAILABLE_NAME_BRANDS_DB_RESID = "6108357";
        #endregion

        #region Known resource values
        /// <summary>
        /// Resource value to display when requested identifier has not been found
        /// </summary>
        public const string DEFAULT_RESOURCE_VALUE = "<Resource not found>";
        #endregion
    }
}
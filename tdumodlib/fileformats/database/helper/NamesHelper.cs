using TDUModdingLibrary.fileformats.database.helper;

namespace TDUModdingLibrary.fileformats.database.helper
{
    /// <summary>
    /// Static class helping with naming information in TDU brands, physics, shops... encrypted files
    /// </summary>
    // EVO_79
    public static class NamesHelper
    {
        #region Constants
        /// <summary>
        /// Car_Brand physics table column
        /// </summary>
        private const string _BRAND_PHYSICS_DB_COLUMN = "Car_Brand";

        /// <summary>
        /// Manufacturer_Name brands table column
        /// </summary>
        private const string _NAME_BRANDS_DB_COLUMN = "Manufacturer_Name";

        /// <summary>
        /// Car_RealName physics table column
        /// </summary>
        private const string _REAL_NAME_PHYSICS_DB_COLUMN = "Car_RealName";

        /// <summary>
        /// Reference for N/A name in physics
        /// </summary>
        private const string _NOT_AVAILABLE_NAME_PHYSICS_DB_REF = "53338427";

        /// <summary>
        /// Model_Name physics table column
        /// </summary>
        private const string _MODEL_NAME_PHYSICS_DB_COLUMN = "Model_Name";

        /// <summary>
        /// Version_Name physics table column
        /// </summary>
        private const string _VERSION_NAME_PHYSICS_DB_COLUMN = "Version_Name";

        /// <summary>
        /// Format string for model name
        /// </summary>
        private const string _FORMAT_MODEL_NAME = "{0} {1} {2}";
        #endregion

        #region DB queries
        /// <summary>
        /// Returns brand name of specified vehicle by querying database
        /// </summary>
        /// <param name="vehicleRef"></param>
        /// <param name="physicsTable"></param>
        /// <param name="brandsTable"></param>
        /// <param name="brandsResource"></param>
        /// <returns></returns>
        public static string GetVehicleBrandName(string vehicleRef, DB physicsTable, DB brandsTable, DBResource brandsResource)
        {
            string returnedName = "";

            if (vehicleRef != null && physicsTable != null && brandsTable != null && brandsResource != null)
            {
                DB.Cell brandReferenceCell =
                    DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(_BRAND_PHYSICS_DB_COLUMN,
                                                                       physicsTable, vehicleRef)[0];

                returnedName = GetBrandName(brandReferenceCell.value, brandsTable, brandsResource);
            }

            return returnedName;
        }

        /// <summary>
        /// Returns full name of specified vehicle by querying database
        /// * isBrandDisplayed = true : [BRAND] [Model] [Version] or [RealName]
        /// * isBrandDisplayed = false : [Model] [Version] or [RealName]
        /// </summary>
        /// <param name="vehicleRef"></param>
        /// <param name="isBrandDisplayed"></param>
        /// <param name="physicsTable"></param>
        /// <param name="physicsResource"></param>
        /// <param name="brandsTable"></param>
        /// <param name="brandsResource"></param>
        /// <returns></returns>
        public static string GetVehicleFullName(string vehicleRef, bool isBrandDisplayed, DB physicsTable, DBResource physicsResource, DB brandsTable, DBResource brandsResource)
        {
            string returnedName = "";

            if (vehicleRef != null && physicsTable != null && physicsResource != null && brandsResource != null && brandsTable != null)
            {
                // Real name : if it's mentioned, other names are not used !
                DB.Cell realNameCell =
                    DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(_REAL_NAME_PHYSICS_DB_COLUMN,
                                                                       physicsTable, vehicleRef)[0];

                if (_NOT_AVAILABLE_NAME_PHYSICS_DB_REF.Equals(realNameCell.value))
                {
                    // Brand name
                    string brandName = "";

                    if (isBrandDisplayed)
                        brandName = GetVehicleBrandName(vehicleRef, physicsTable, brandsTable, brandsResource).ToUpper();

                    // Model name
                    DB.Cell modelNameCell =
                        DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(_MODEL_NAME_PHYSICS_DB_COLUMN,
                                                                           physicsTable, vehicleRef)[0];
                    string modelName = "";

                    if (!_NOT_AVAILABLE_NAME_PHYSICS_DB_REF.Equals(modelNameCell.value))
                        modelName = DatabaseHelper.GetResourceValueFromCell(modelNameCell, physicsResource);

                    // Version name
                    DB.Cell versionNameCell =
                        DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(_VERSION_NAME_PHYSICS_DB_COLUMN,
                                                                           physicsTable, vehicleRef)[0];
                    string versionName = "";

                    if (!_NOT_AVAILABLE_NAME_PHYSICS_DB_REF.Equals(versionNameCell.value))
                        versionName = DatabaseHelper.GetResourceValueFromCell(versionNameCell, physicsResource);

                    if (isBrandDisplayed)
                        returnedName = string.Format(_FORMAT_MODEL_NAME, brandName, modelName, versionName);
                    else
                        returnedName = string.Format(_FORMAT_MODEL_NAME, modelName, versionName, "");
                }
                else
                {
                    // Real name
                    string realName = DatabaseHelper.GetResourceValueFromCell(realNameCell, physicsResource);

                    returnedName = string.Format(_FORMAT_MODEL_NAME, realName, "", "");
                }
            }

            return returnedName;
        }

        /// <summary>
        /// Returns name of specified brand by querying database
        /// </summary>
        /// <param name="brandReference"></param>
        /// <param name="brandsTable"></param>
        /// <param name="brandsResource"></param>
        /// <returns></returns>
        public static string GetBrandName(string brandReference, DB brandsTable, DBResource brandsResource)
        {
            string returnedName = "";

            if (!string.IsNullOrEmpty(brandReference) && brandsTable != null && brandsResource != null)
            {
                DB.Cell brandNameCell =
                    DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(_NAME_BRANDS_DB_COLUMN, brandsTable, brandReference)[0];

                returnedName = DatabaseHelper.GetResourceValueFromCell(brandNameCell, brandsResource);
            }

            return returnedName;
        }
        #endregion
    }
}
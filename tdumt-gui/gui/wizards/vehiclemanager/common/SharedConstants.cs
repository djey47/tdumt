using TDUModdingLibrary.support;
using TDUModdingLibrary.support.constants;

namespace TDUModdingTools.gui.wizards.vehiclemanager.common
{
    /// <summary>
    /// Static class providing constants used through Vehicle Manager
    /// </summary>
    static class SharedConstants
    {
        #region String formats
        /// <summary>
        /// Format for item in list
        /// </summary>
        internal static readonly string FORMAT_REF_NAME_COUPLE = "{0}" + Tools.SYMBOL_VALUE_SEPARATOR + "{1}";
        #endregion

        #region File name formats
        /// <summary>
        /// Exterior model / HUD / rims file name format
        /// </summary>
        internal const string _FORMAT_EXT_MODEL_NAME = "{0}." + LibraryConstants.EXTENSION_BNK_FILE;

        /// <summary>
        /// Interior model file name format
        /// </summary>
        internal const string _FORMAT_INT_MODEL_NAME = "{0}_I." + LibraryConstants.EXTENSION_BNK_FILE;

        /// <summary>
        /// Sound file name format
        /// </summary>
        internal const string _FORMAT_SOUND_NAME = "{0}_audio." + LibraryConstants.EXTENSION_BNK_FILE;
        #endregion

        #region Database column names
        /* CAR PHYSICS*/
        /// <summary>
        /// Car_Brand physics table column
        /// </summary>
        internal const string BRAND_PHYSICS_DB_COLUMN = "Car_Brand";

        /// <summary>
        /// Car_File_Name physics table column
        /// </summary>
        internal const string CAR_FILE_NAME_PHYSICS_DB_COLUMN = "Car_File_Name";

        /// <summary>
        /// Car_Brand physics table column
        /// </summary>
        internal const string CAR_BRAND_PHYSICS_DB_COLUMN = "Car_Brand";

        /// <summary>
        /// Car_Model physics table column
        /// </summary>
        internal const string CAR_MODEL_PHYSICS_DB_COLUMN = "Car_Model";

        /// <summary>
        /// Car_Version physics table column
        /// </summary>
        internal const string CAR_VERSION_PHYSICS_DB_COLUMN = "Car_Version";

        /// <summary>
        /// Model_Name physics table column
        /// </summary>
        internal const string MODEL_NAME_PHYSICS_DB_COLUMN = "Model_Name";

        /// <summary>
        /// Version_Name physics table column
        /// </summary>
        internal const string VERSION_NAME_PHYSICS_DB_COLUMN = "Version_Name";

        /// <summary>
        /// Car_RealName physics table column
        /// </summary>
        internal const string REAL_NAME_PHYSICS_DB_COLUMN = "Car_RealName";

        /// <summary>
        /// HUD_File_Name physics table column
        /// </summary>
        internal const string HUD_FILE_NAME_PHYSICS_DB_COLUMN = "HUD_File_Name";

        /// <summary>
        /// Drive physics table column
        /// </summary>
        internal const string DRIVE_PHYSICS_DB_COLUMN = "Drive";

        /// <summary>
        /// Transmission_Primacy physics table column
        /// </summary>
        internal const string TRANSMISSION_PRIMACY_PHYSICS_DB_COLUMN = "Transmission_Primacy";

        /// <summary>
        /// GearBox physics table column
        /// </summary>
        internal const string GEARBOX_PHYSICS_DB_COLUMN = "GearBox";

        /// <summary>
        /// Nb_Gears physics table column
        /// </summary>
        internal const string NB_GEARS_PHYSICS_DB_COLUMN = "Nb_Gears";

        /// <summary>
        /// Final_Drive_Ratio physics table column
        /// </summary>
        internal const string FINAL_DRIVE_RATIO_PHYSICS_DB_COLUMN = "Final_Drive_Ratio";

        /// <summary>
        /// Gear_ratio_? pattern physics table column
        /// </summary>
        internal const string GEAR_RATIO_PATTERN_PHYSICS_DB_COLUMN = "Gear_ratio_{0}";

        /// <summary>
        /// Gearbox_Inertia physics table column
        /// </summary>
        internal const string GEARBOX_INERTIA_PHYSICS_DB_COLUMN = "Gearbox_Inertia";

        /// <summary>
        /// Transmission_Primacy physics table column
        /// </summary>
        internal const string TRANSPRIMACY_PHYSICS_DB_COLUMN = "Transmission_Primacy";

        /// <summary>
        /// Engine_Localisation physics table column
        /// </summary>
        internal const string ENGINE_LOCALISATION_PHYSICS_DB_COLUMN = "Engine_Localisation";

        /// <summary>
        /// Red_Line physics table column
        /// </summary>
        internal const string RED_LINE_PHYSICS_DB_COLUMN = "Red_Line";

        /// <summary>
        /// Ignition_RPM physics table column
        /// </summary>
        internal const string IGNITION_RPM_PHYSICS_DB_COLUMN = "Ignition_RPM";

        /// <summary>
        /// Engine_Inertia physics table column
        /// </summary>
        internal const string ENGINE_INERTIA_PHYSICS_DB_COLUMN = "Engine_Inertia";

        /// <summary>
        /// Nb_Turbos physics table column
        /// </summary>
        internal const string NB_TURBOS_PHYSICS_DB_COLUMN = "Nb_Turbos";

        /// <summary>
        /// Engine_Type physics table column
        /// </summary>
        internal const string ENGINE_TYPE_PHYSICS_DB_COLUMN = "Engine_Type";

        /// <summary>
        /// Displacement physics table column
        /// </summary>
        internal const string DISPLACEMENT_PHYSICS_DB_COLUMN = "Displacement";

        /// <summary>
        /// Power_bhp physics table column
        /// </summary>
        internal const string POWER_BHP_PHYSICS_DB_COLUMN = "Power_bhp";

        /// <summary>
        /// Power_RPM physics table column
        /// </summary>
        internal const string POWER_RPM_PHYSICS_DB_COLUMN = "Power_RPM";
     
        /// <summary>
        /// Power_bhp physics table column
        /// </summary>
        internal const string TORQUE_NM_PHYSICS_DB_COLUMN = "Torque_Nm";

        /// <summary>
        /// Power_RPM physics table column
        /// </summary>
        internal const string TORQUE_RPM_PHYSICS_DB_COLUMN = "Torque_RPM";
        
        /// <summary>
        /// Weight_Kg physics table column
        /// </summary>
        internal const string WEIGHT_KG_PHYSICS_DB_COLUMN = "Weight_Kg";

        /// <summary>
        /// Tires_Type physics table column
        /// </summary>
        internal const string TYRES_TYPE_PHYSICS_DB_COLUMN = "Tires_Type";

        /// <summary>
        /// Brakes_Characteristics_Front physics table column
        /// </summary>
        internal const string BRAKES_CHAR_FRONT_PHYSICS_DB_COLUMN = "Brakes_Characteristics_Front";

        /// <summary>
        /// Brakes_Characteristics_Rear physics table column
        /// </summary>
        internal const string BRAKES_CHAR_REAR_PHYSICS_DB_COLUMN = "Brakes_Characteristics_Rear";

        /// <summary>
        /// Brakes_Dim_Front physics table column
        /// </summary>
        internal const string BRAKES_DIM_FRONT_PHYSICS_DB_COLUMN = "Brakes_Dim_Front";
        
        /// <summary>
        /// Brakes_Dim_Rear physics table column
        /// </summary>
        internal const string BRAKES_DIM_REAR_PHYSICS_DB_COLUMN = "Brakes_Dim_Rear";

        /// <summary>
        /// Ride_Height_Front physics table column
        /// </summary>
        internal const string RIDE_HEIGHT_FRONT_PHYSICS_DB_COLUMN = "Ride_Height_Front";
        
        /// <summary>
        /// Ride_Height_Rear physics table column
        /// </summary>
        internal const string RIDE_HEIGHT_REAR_PHYSICS_DB_COLUMN = "Ride_Height_Rear";

        /// <summary>
        /// Ride_Height_Max_Front physics table column
        /// </summary>
        internal const string RIDE_HEIGHT_MAX_FRONT_PHYSICS_DB_COLUMN = "Ride_Height_Max_Front";

        /// <summary>
        /// Ride_Height_Max_Rear physics table column
        /// </summary>
        internal const string RIDE_HEIGHT_MAX_REAR_PHYSICS_DB_COLUMN = "Ride_Height_Max_Rear";
        
        /// <summary>
        /// Suspension_Lenght_Front physics table column
        /// </summary>
        internal const string SUSPENSION_LENGTH_FRONT_PHYSICS_DB_COLUMN = "Suspension_Lenght_Front";

        /// <summary>
        /// Suspension_Lenght_Rear physics table column
        /// </summary>
        internal const string SUSPENSION_LENGTH_REAR_PHYSICS_DB_COLUMN = "Suspension_Lenght_Rear";

        /// <summary>
        /// Suspension_Rate_Front physics table column
        /// </summary>
        internal const string SUSPENSION_RATE_FRONT_PHYSICS_DB_COLUMN = "Suspension_Rate_Front";

        /// <summary>
        /// Suspension_Rate_Rear physics table column
        /// </summary>
        internal const string SUSPENSION_RATE_REAR_PHYSICS_DB_COLUMN = "Suspension_Rate_Rear";

        /// <summary>
        /// Top_Speed_kmh physics table column
        /// </summary>
        internal const string TOP_SPEED_KMH_PHYSICS_DB_COLUMN = "Top_Speed_kmh";

        /// <summary>
        /// t0_to_1000m_sec physics table column
        /// </summary>
        internal const string T0_TO_1000M_SEC_PHYSICS_DB_COLUMN = "t0_to_1000m_sec";

        /// <summary>
        /// Quarter_Mile_sec physics table column
        /// </summary>
        internal const string QUARTER_MILE_SEC_PHYSICS_DB_COLUMN = "Quarter_Mile_sec";

        /// <summary>
        /// Acceleration_0_100_kmh physics table column
        /// </summary>
        internal const string ACCELERATION_0_100_KMH_PHYSICS_DB_COLUMN = "Acceleration_0_100_kmh";

        /// <summary>
        /// Acceleration_0_100_kmh physics table column
        /// </summary>
        internal const string ACCELERATION_0_100_MPH_PHYSICS_DB_COLUMN = "Acceleration_0_100_mph";

        /// <summary>
        /// Ignition_Time_Ignite physics table column
        /// </summary>
        internal const string IGNITION_TIME_IGNITE_PHYSICS_DB_COLUMN = "Ignition_Time_Ignite";

        /// <summary>
        /// Ignition_Time_RevUp physics table column
        /// </summary>
        internal const string IGNITION_TIME_REVUP_PHYSICS_DB_COLUMN = "Ignition_Time_RevUp";

        /// <summary>
        /// Ignition_Time_RevDown physics table column
        /// </summary>
        internal const string IGNITION_TIME_REVDOWN_PHYSICS_DB_COLUMN = "Ignition_Time_RevDown";

        /// <summary>
        /// Car_Body physics table column
        /// </summary>
        internal const string CAR_BODY_PHYSICS_DB_COLUMN = "Car_Body";

        /// <summary>
        /// Length physics table column
        /// </summary>
        internal const string LENGTH_PHYSICS_DB_COLUMN = "Lenght";

        /// <summary>
        /// Width physics table column
        /// </summary>
        internal const string WIDTH_PHYSICS_DB_COLUMN = "Width";

        /// <summary>
        /// Height physics table column
        /// </summary>
        internal const string HEIGHT_PHYSICS_DB_COLUMN = "Height";

        /* BRANDS */
        /// <summary>
        /// Manufacturer_Name brands table column
        /// </summary>
        internal const string NAME_BRANDS_DB_COLUMN = "Manufacturer_Name";

        /// <summary>
        /// Manufacturer_ID brands table column
        /// </summary>
        internal const string ID_BRANDS_DB_COLUMN = "Manufacturer_ID";

        /* CAR RIMS */
        /// <summary>
        /// Rims car rims table column
        /// </summary>
        internal const string RIMS_CARRIMS_DB_COLUMN = "Rims";

        /// <summary>
        /// Car car rims table column
        /// </summary>
        internal const string CAR_CARRIMS_DB_COLUMN = "Car";

        /* CAR COLORS */
        /// <summary>
        /// Car car colors table column
        /// </summary>
        internal const string CAR_CARCOLORS_DB_COLUMN = "Car";

        /// <summary>
        /// Color_Name car colors table column
        /// </summary>
        internal const string COLOR_NAME_CARCOLORS_DB_COLUMN = "Color_Name";

        /// <summary>
        /// Price_dollar car colors table column
        /// </summary>
        internal const string PRICE_DOLLAR_CARCOLORS_DB_COLUMN = "Price_dollar";

        /// <summary>
        /// Color_ID_1 car colors table column
        /// </summary>
        internal const string COLOR_ID1_CARCOLORS_DB_COLUMN = "Color_ID_1";

        /// <summary>
        /// Color_ID_2 car colors table column
        /// </summary>
        internal const string COLOR_ID2_CARCOLORS_DB_COLUMN = "Color_ID_2";

        /// <summary>
        /// Callipers car colors table column
        /// </summary>
        internal const string CALLIPERS_CARCOLORS_DB_COLUMN = "Callipers";

        /// <summary>
        /// Interior_? pattern car colors table column
        /// </summary>
        internal const string INTERIOR_CARCOLORS_PATTERN_DB_COLUMN = "Interior_{0}";        

        /* INTERIOR */
        /// <summary>
        /// Interior_Name interior table column
        /// </summary>
        internal const string INTERIOR_NAME_INTERIOR_DB_COLUMN = "Interior_Name";

        /// <summary>
        /// Manufacturer_ID interior table column
        /// </summary>
        internal const string MANUFACTURER_ID_INTERIOR_DB_COLUMN = "Manufacturer_ID";

        /// <summary>
        /// Interior_Color_1 interior table column
        /// </summary>
        internal const string INTERIOR_COLOR_1_INTERIOR_DB_COLUMN = "Interior_Color_1";

        /// <summary>
        /// Interior_Color_2 interior table column
        /// </summary>
        internal const string INTERIOR_COLOR_2_INTERIOR_DB_COLUMN = "Interior_Color_2";

        /// <summary>
        /// Material interior table column
        /// </summary>
        internal const string MATERIAL_INTERIOR_DB_COLUMN = "Material";

        /// <summary>
        /// Price_dollar interior table column
        /// </summary>
        internal const string PRICE_DOLLAR_INTERIOR_DB_COLUMN = "Price_dollar";

        /* CAR PACKS */
        /// <summary>
        /// Car car packs table column
        /// </summary>
        internal const string CAR_CARPACKS_DB_COLUMN = "Car";

        /// <summary>
        /// Brands car packs table column
        /// </summary>
        internal const string BRAND_CARPACKS_DB_COLUMN = "Brand";

        /// <summary>
        /// Libelle car packs table column
        /// </summary>
        internal const string LIBELLE_CARPACKS_DB_COLUMN = "Libelle";

        /* CAR SHOPS */
        /// <summary>
        /// Libelle car shops table column
        /// </summary>
        internal const string LIBELLE_CARSHOPS_DB_COLUMN = "Libelle";

        /* RIMS */
        /// <summary>
        /// DisplayName rims table column
        /// </summary>
        internal const string DISPLAY_NAME_RIMS_DB_COLUMN = "DisplayName";

        /// <summary>
        /// Rsc_Path rims table column
        /// </summary>
        internal const string RSC_PATH_RIMS_DB_COLUMN = "Rsc_Path";

        /// <summary>
        /// Rsc_File_Name_Front rims table column
        /// </summary>
        internal const string RSC_FILE_NAME_FRONT_RIMS_DB_COLUMN = "Rsc_File_Name_Front";

        /// <summary>
        /// Rsc_File_Name_Front rims table column
        /// </summary>
        internal const string RSC_FILE_NAME_REAR_RIMS_DB_COLUMN = "Rsc_File_Name_Rear";

        /* Common */
        /// <summary>
        /// BitField table column
        /// </summary>
        internal const string BITFIELD_DB_COLUMN = "BitField";
        #endregion

        #region Known database resource identifiers
        /* CAR PHYSICS */
        /// <summary>
        /// Reference for FWD drive physics
        /// </summary>
        internal const string FWD_DRIVE_PHYSICS_DB_RESID = "56356917";

        /// <summary>
        /// Reference for RWD drive physics
        /// </summary>
        internal const string RWD_DRIVE_PHYSICS_DB_RESID = "55356917";

        /// <summary>
        /// Reference for AWD drive physics
        /// </summary>
        internal const string AWD_DRIVE_PHYSICS_DB_RESID = "58356917";
        
        /// <summary>
        /// Reference for no drive physics
        /// </summary>
        internal const string NO_DRIVE_PHYSICS_DB_RESID = "57356917";

        /// <summary>
        /// Reference for 4WD drive physics
        /// </summary>
        internal const string FOURWD_DRIVE_PHYSICS_DB_RESID = "54356917";

        /// <summary>
        /// Reference for sequential gearbox
        /// </summary>
        internal const string SEQ_BOX_PHYSICS_DB_RESID = "563531";

        /// <summary>
        /// Reference for semi-automatic gearbox
        /// </summary>
        internal const string SEMI_BOX_PHYSICS_DB_RESID = "553531";

        /// <summary>
        /// Reference for automatic gearbox
        /// </summary>
        internal const string AUTO_BOX_PHYSICS_DB_RESID = "543531";

        /// <summary>
        /// Reference for stick gearbox
        /// </summary>
        internal const string STICK_BOX_PHYSICS_DB_RESID = "573531";

        /// <summary>
        /// Reference for front engine location
        /// </summary>
        internal const string FRONT_ENGINE_LOC_PHYSICS_DB_RESID = "543629";

        /// <summary>
        /// Reference for center engine location
        /// </summary>
        internal const string CENTER_ENGINE_LOC_PHYSICS_DB_RESID = "553629";

        /// <summary>
        /// Reference for rear engine location
        /// </summary>
        internal const string REAR_ENGINE_LOC_PHYSICS_DB_RESID = "563629";

        /* CAR SHOPS */
        /// <summary>
        /// Reference for V-RENT spot name
        /// </summary>
        internal const string VRENT_CARSHOPS_DB_RESID = "5873697";

        /* CAR COLORS */
        /// <summary>
        /// Reference for unavailable color code
        /// </summary>
        internal const string NOT_AVAILABLE_COLOR_CARCOLORS_DB_RESID = "53356127";

        /// <summary>
        /// Reference for unavailable calipers color code
        /// </summary>
        internal const string NOT_AVAILABLE_CALIPERS_COLOR_CARCOLORS_DB_RESID = "53356127";

        /* INTERIOR */
        /// <summary>
        /// Reference for unavailable interior/material color code
        /// </summary>
        internal const string NOT_AVAILABLE_INTERIOR_COLOR_DB_RESID = "53364643";
        #endregion

        #region Known database resource categories
        /// <summary>
        /// Category code (suffix) for interior color set names
        /// </summary>
        internal const string NAME_INTERIOR_CATEGORY = "5512";

        /// <summary>
        /// Category code (suffix) for exterior color set names
        /// </summary>
        internal const string NAME_EXTERIOR_CATEGORY = "6457";
        #endregion

        #region Known database values
        /// <summary>
        /// No interior color
        /// </summary>
        internal const string NO_COLOR_INTERIOR_DB_VAL = "11319636";

        /* BRANDS */
        /// <summary>
        /// AC manufacturer identifier
        /// </summary>
        internal const string AC_MANUFACTURER_ID_BRANDS_DB_VAL = "55338337";

        /// <summary>
        /// Default brand reference
        /// </summary>
        internal const string DEFAULT_REF_BRANDS_DB_VAL = "000";
        #endregion

        #region Known database resource values
        /// <summary>
        /// No information
        /// </summary>
        internal const string ERROR_DB_RESVAL = "??";
        #endregion
    }
}
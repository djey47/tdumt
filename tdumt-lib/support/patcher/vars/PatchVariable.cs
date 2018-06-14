using System;
using System.Runtime.Remoting;
using DjeFramework1.Common.Types;

namespace TDUModdingLibrary.support.patcher.vars
{
    /// <summary>
    /// Class which is an abstract view of a patch variable. Variables are interpreted when running patch only.
    /// </summary>
    public abstract class PatchVariable
    {
        #region Enums
        /// <summary>
        /// All enumerated variable names
        /// </summary>
        public enum VariableName
        {
            patchPath,
            tduPath,
            tempPath,
            desktopPath,
            bnkAvatarClothesPath,
            bnkFrontEndAllResPath,
            bnkFrontEndHiResPath,
            bnkFXPath,
            bnkLevelHawaiPath,
            bnkPath,
            bnkGaugesLowPath,
            bnkGaugesHighPath,
            bnkSoundVehiclePath,
            bnkVehiclePath,
            bnkVehicleRimPath,
            bnkVehicleTrafficPath,
            dbPath
        }
        #endregion

        #region Constants
        /// <summary>
        /// Markeup to delimitate variables
        /// </summary>
        public const string VARIABLE_MARKEUP = "#";

        /// <summary>
        /// Format of any variable code
        /// </summary>
        private const string _FORMAT_VARIABLE_CODE = VARIABLE_MARKEUP + "{0}" + VARIABLE_MARKEUP;

        /// <summary>
        /// Format of any variable class name
        /// </summary>
        private const string _FORMAT_VARIABLE_CLASS = "TDUModdingLibrary.support.patcher.vars.{0}V";
        #endregion

        #region Properties (syntax-related)
        /// <summary>
        /// Variable name
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Variable description
        /// </summary>
        public abstract string Description { get; }
        
        /// <summary>
        /// Variable code to be used in parameters
        /// </summary>
        public string Code
        {
            get { return string.Format(_FORMAT_VARIABLE_CODE, Name); }
        }       
        #endregion

        #region Public methods
        /// <summary>
        /// Returns a variable according to specified name
        /// </summary>
        /// <param name="varName">Name of variable to generate</param>
        /// <returns>Corresponding variable</returns>
        public static PatchVariable MakeVariable(string varName)
        {
            PatchVariable resultV = null;

            if (!string.IsNullOrEmpty(varName))
            {
                varName = String2.ToPascalCase(varName);

                string fullClassName = string.Format(_FORMAT_VARIABLE_CLASS, varName);
                ObjectHandle dynamicObject = Activator.CreateInstance(null, fullClassName);

                if (dynamicObject != null)
                    resultV = dynamicObject.Unwrap() as PatchVariable;
            }

            return resultV;
        }
        
        /// <summary>
        /// Returns full name: #varName# for specified variable name
        /// </summary>
        /// <param name="var"></param>
        /// <returns></returns>
        public static string GetFullName(VariableName var)
        {
            return string.Format(_FORMAT_VARIABLE_CODE, var);
        }
        #endregion

        #region Methods to implement
        /// <summary>
        /// Which value the variable should refer to
        /// </summary>
        internal abstract string GetValue();
        #endregion
    }
}
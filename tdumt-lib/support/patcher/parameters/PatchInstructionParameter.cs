using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.support.patcher.parameters.providers;
using TDUModdingLibrary.support.patcher.vars;

namespace TDUModdingLibrary.support.patcher.parameters
{
    /// <summary>
    /// Represents an abstract view of a parameter to be used by a patch instruction
    /// </summary>
    public abstract class PatchInstructionParameter : ICloneable
    {
        #region Enums
        /// <summary>
        /// All enumerated parameter names 
        /// </summary>
        public enum ParameterName
        {
            bnkFile,
            bnkRemapFolderName,
            cameraIKIdentifier,
            createFolder,
            databaseId,
            destination,
            distributionValues,
            fileName,
            fileType,
            lockCode,
            makeBackup,
            mappedFileName,
            newCockpitView,
            newCockpitBackView,
            newFile,
            newHoodView,
            newHoodBackView,
            patchDirectory,
            resourceFileName,
            resourceValues,
            rimName,
            shopsDatabaseId,
            slotFullName,
            trafficVehicleId,
            value,
            valueAddress,
            valueLength,
            vehicleDatabaseId,
            viewSource,
            viewTarget
        }
        #endregion

        #region Constants
        /// <summary>
        /// Format of any parameter class name
        /// </summary>
        private const string _FORMAT_PARAMETER_CLASS = "TDUModdingLibrary.support.patcher.parameters.{0}IP";
        #endregion

        #region Properties (syntax-related)
        /// <summary>
        /// Parameter name
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Parameter description
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Default values provider
        /// </summary>
        public abstract IValuesProvider DefaultValuesProvider { get; }
        #endregion

        #region Properties (user-defined)
        /// <summary>
        /// Indicates or sets current parameter value
        /// </summary>
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        private string _Value;

        /// <summary>
        /// Enforces current values provider
        /// </summary>
        public IValuesProvider ValuesProvider
        {
            get
            {
                if (_CurrentValuesProvider == null)
                    _CurrentValuesProvider = DefaultValuesProvider;
                
                return _CurrentValuesProvider;
            }
            set { _CurrentValuesProvider = value; }
        }
        private IValuesProvider _CurrentValuesProvider;
        #endregion  
    
        #region Public methods
        /// <summary>
        /// Returns a parameter according to the specified name
        /// </summary>
        /// <param name="parameterName">Name of parameter to generate</param>
        /// <returns>Corresponding parameter</returns>
        public static PatchInstructionParameter MakeParameter(string parameterName)
        {
            PatchInstructionParameter resultP = null;

            if (!string.IsNullOrEmpty(parameterName))
            {
                parameterName = String2.ToPascalCase(parameterName);
                
                string fullClassName = string.Format(_FORMAT_PARAMETER_CLASS, parameterName);
                //ObjectHandle dynamicObject = Activator.CreateInstance(null, fullClassName);
                // To solve culture issues ??
                ObjectHandle dynamicObject = Activator.CreateInstance(null, fullClassName, false, BindingFlags.CreateInstance, null, null, CultureInfo.InvariantCulture, null, null);

                if (dynamicObject != null)
                    resultP = dynamicObject.Unwrap() as PatchInstructionParameter;
            }

            return resultP;
        }

        /// <summary>
        /// Processes given parameter value to interpretate inserted patch variables 
        /// </summary>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public static string EvaluateParameter(string paramValue)
        {
            if (string.IsNullOrEmpty(paramValue))
                return paramValue;

            // Retrieves variable(s) in specified value
            Collection<int> markeupPositions = new Collection<int>();
            int currentVarIndex = 0;

            while ( currentVarIndex != -1 && currentVarIndex < paramValue.Length )
            {
                currentVarIndex = paramValue.IndexOf(PatchVariable.VARIABLE_MARKEUP, currentVarIndex);

                if (currentVarIndex != -1)
                {
                    markeupPositions.Add(currentVarIndex);
                    currentVarIndex++;
                }
            }

            for (int i = 0 ; i < markeupPositions.Count - 1; i = i + 2)
            {
                string currentVarName = paramValue.Substring(markeupPositions[i] + 1, markeupPositions[i + 1] - markeupPositions[i] - 1);
                PatchVariable var = PatchVariable.MakeVariable(currentVarName);

                if (var != null)
                    paramValue = paramValue.Replace(var.Code, var.GetValue());                   
            }

            return paramValue;
        }
        #endregion

        #region ICloneable Members
        ///<summary>
        ///
        ///                    Creates a new object that is a copy of the current instance.
        ///                
        ///</summary>
        ///
        ///<returns>
        ///
        ///                    A new object that is a copy of this instance.
        ///                
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public object Clone()
        {
            PatchInstructionParameter parameterClone = MakeParameter(Name);
            
            if (_Value != null)
                parameterClone._Value = _Value.Clone() as string;

            return parameterClone;
        }
        #endregion
    }
}
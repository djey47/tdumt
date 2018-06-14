using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.support.patcher.exceptions;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.fileformats.specific;
using ParameterInfo = TDUModdingLibrary.support.patcher.parameters.util.ParameterInfo;

namespace TDUModdingLibrary.support.patcher.instructions
{
    /// <summary>
    /// Represents an abstract view of a patch instruction.
    /// </summary>
    public abstract class PatchInstruction : ICloneable
    {
        #region Enums
        /// <summary>
        /// All enumerated instruction names 
        /// </summary>
        public enum InstructionName
        {
            bnkUnmap,
            bnkRemap,
            checkPatch,
            customizeViews,
            customizeViewsUnlimited,
            changeCockpitView,
            installFile,
            installPackedFile,
            nothing,
            removeAllLinesFromDatabase,
            removeVehicleFromSpots,
            setTrafficDistribution,
            setTrafficPerKilometer,
            setVehicleOnSpots,
            uninstallFile,
            uninstallPackedFile,
            updateDatabase,
            updateResource
        }

        /// <summary>
        /// All enumerated condition names
        /// </summary>
        public enum ConditionName
        {
            alwaysFalse, alwaysTrue, patchedTdu, megapackTdu
        }
        #endregion

        #region Constants
        /// <summary>
        /// Format of any instruction class name
        /// </summary>
        private const string _FORMAT_INSTRUCTION_CLASS = "TDUModdingLibrary.support.patcher.instructions.{0}I";

        /// <summary>
        /// Message when an exception occurs
        /// </summary>
        private const string _MSG_SPOTTED_EXCEPTION =  "Exception spotted: {0}";
        #endregion

        #region Properties (syntax-related)
        /// <summary>
        /// Instruction name
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Instruction description
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// List of supported parameters
        /// </summary>
        // EVO_81: property added
        internal abstract Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo> SupportedParameterInformation { get; }

        /// <summary>
        /// List of supported parameters names
        /// </summary>
        // EVO_81: property modified
        public string[] SupportedParameters
        {
            get
            {
                string[] nameList = new string[SupportedParameterInformation.Keys.Count];
                int index = 0;

                foreach (PatchInstructionParameter.ParameterName name in SupportedParameterInformation.Keys)
                    nameList[index++] = name.ToString();

                return nameList;
            }
        }
        #endregion

        #region Properties (user-defined)
        /// <summary>
        /// Indicates or set if current instruction should be applied
        /// </summary>
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }
        private bool _Enabled = true;

        /// <summary>
        /// Indicates or set current instruction order
        /// </summary>
        public int Order
        {
            get { return _Order; }
            set { _Order = value; }
        }
        private int _Order;

        /// <summary>
        /// Indicates or set if the execution should stop if an error occurs
        /// </summary>
        public bool FailOnError
        {
            get {return _FailOnError; }
            set { _FailOnError = value; }
        }
        private bool _FailOnError = true;

        /// <summary>
        /// Group this instruction belongs to
        /// </summary>
        public PCH.InstallGroup Group
        {
            get { return _Group; }
            set { _Group = value; }
        }
        private PCH.InstallGroup _Group;

        /// <summary>
        /// List of instruction parameters
        /// </summary>
        public Dictionary<string,PatchInstructionParameter> Parameters
        {
            get { return _Parameters; }
        }
        private Dictionary<string,PatchInstructionParameter> _Parameters = new Dictionary<string, PatchInstructionParameter>();

        /// <summary>
        /// Type of this instruction
        /// </summary>
        public InstructionName Type
        {
            get { return _Type; }
        }
        private InstructionName _Type = InstructionName.nothing;

        /// <summary>
        /// Author's comment about current instruction
        /// </summary>
        public string Comment
        {
            get { return _Comment;  }
            set { _Comment = value; }
        }
        private string _Comment = "";
        #endregion

        #region Public methods
        /// <summary>
        /// Manages instruction execution
        /// </summary>
        /// <param name="currentLogger">Logger to write execution events (optional)</param>
        /// <returns>A run result</returns>
        public PatchHelper.RunResult Run(Log currentLogger)
        {
            PatchHelper.RunResult runResult = PatchHelper.RunResult.OK;

            try
            {
                // Instruction
                _Process();
            }
            catch (Exception ex)
            {
                // Error logged
                Exception2.PrintStackTrace(ex);

                if (currentLogger != null)
                    currentLogger.WriteEvent(string.Format(_MSG_SPOTTED_EXCEPTION, ex.Message));

                if (FailOnError)
                    runResult = PatchHelper.RunResult.RunWithErrors;
                else
                    runResult = PatchHelper.RunResult.RunWithWarnings;
            }

            return runResult;
        }

        /// <summary>
        /// Defines a parameter value if it's supported by current instruction. If parameter does not exist, it's created.
        /// </summary>
        /// <param name="paramName">Parameter name</param>
        /// <param name="paramValue">Parameter value</param>
        /// <exception cref="Exception">When specified parameter is not supported</exception>
        public void SetParameter(string paramName, string paramValue)
        {
            if (string.IsNullOrEmpty(paramName))
                return;

            // Existing parameter ?
            if (Parameters.ContainsKey(paramName))
            {
                PatchInstructionParameter currentPip = Parameters[paramName];

                currentPip.Value = paramValue;
            }
            else 
            {
                // Compatibility test
                bool isCompatible = false;

                foreach (string s in SupportedParameters)
                {
                    if (s.Equals(paramName))
                    {
                        // OK
                        PatchInstructionParameter newPip = PatchInstructionParameter.MakeParameter(paramName);

                        newPip.Value = paramValue;
                        Parameters.Add(paramName, newPip);
                        
                        isCompatible = true;
                        break;
                    }
                }

                if (!isCompatible)
                    throw new Exception("Error: '" + paramName + "' parameter is not supported by'" + Name +
                                        "' instruction.");
            }
        }

        /// <summary>
        /// Copy properties from a patch instruction to another. References are copied, not values.
        /// </summary>
        /// <param name="piSource">Base instruction</param>
        /// <param name="piDestination">Target instruction</param>
        /// <param name="removeUnsupportedParameters">true to only keep parameters supported by target, false to keep all</param>
        public static void CopyProperties(PatchInstruction piSource, PatchInstruction piDestination, bool removeUnsupportedParameters)
        {
            if (piSource != null && piDestination != null)
            {
                piDestination._Enabled = piSource._Enabled;
                piDestination._FailOnError = piSource._FailOnError;
                piDestination._Order = piSource._Order;
                piDestination._Comment = piSource._Comment;
                piDestination._Group = piSource._Group;

                // Parameters: depending on boolean parameter
                piDestination._Parameters = piSource._Parameters;

                if (removeUnsupportedParameters)
                    piDestination.RemoveUnsupportedParameters();
            }
        }

        /// <summary>
        /// Returns an instruction according to specified type name
        /// </summary>
        /// <param name="instructionType">Type of instruction to generate</param>
        /// <returns>Corresponding instruction</returns>
        public static PatchInstruction MakeInstruction(string instructionType)
        {
            PatchInstruction resultPI = null;

            if (!string.IsNullOrEmpty(instructionType))
            {
                instructionType = String2.ToPascalCase(instructionType);
                
                string fullClassName = string.Format(_FORMAT_INSTRUCTION_CLASS, instructionType);

                // To solve culture issues ??
                ObjectHandle dynamicObject = Activator.CreateInstance(null, fullClassName, false, BindingFlags.CreateInstance, null, null,CultureInfo.InvariantCulture, null, null  );

                if (dynamicObject != null)
                {
                    resultPI = dynamicObject.Unwrap() as PatchInstruction;

                    if (resultPI != null)
                        resultPI._Type = (InstructionName) Enum.Parse(typeof (InstructionName), instructionType, true);
                }
            }

            return resultPI;
        }
        
        /// <summary>
        /// Utility method allowing to return an instruction with different type, but keeping all compatible parameters
        /// </summary>
        /// <param name="instruction"></param>
        /// <param name="newInstructionName"></param>
        /// <returns></returns>
        public static PatchInstruction ChangeInstruction(PatchInstruction instruction, InstructionName newInstructionName)
        {
            PatchInstruction changedInstruction = null;

            if (instruction != null)
            {
                changedInstruction = MakeInstruction(newInstructionName.ToString());
                changedInstruction._Order = instruction._Order;
                changedInstruction._Parameters = instruction._CloneParameters();
                changedInstruction._Comment = instruction._Comment;
                changedInstruction._Group = instruction._Group;
                changedInstruction.RemoveUnsupportedParameters();
            }

            return changedInstruction;
        }
        #endregion

        #region Internal methods
        /// <summary>
        /// Ensures that all unsupported parameters are removed from the list
        /// </summary>
        internal void RemoveUnsupportedParameters()
        {
            Dictionary<string, PatchInstructionParameter> finalList = new Dictionary<string, PatchInstructionParameter>();

            // Parameter browsing...
            foreach (KeyValuePair<string, PatchInstructionParameter> pair in _Parameters)
            {
                foreach (string s in SupportedParameters)
                {
                    if (s.Equals(pair.Key))
                    {
                        finalList.Add(pair.Key, pair.Value);
                        break;
                    }
                }
            }

            _Parameters = finalList;
        }

        /// <summary>
        /// Utility method allowing to define parameters used by each instruction
        /// </summary>
        /// <param name="parameterInformation">all parameters</param>
        /// <returns>List of specified parameters</returns>
        internal Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo> _DefineParameters(params ParameterInfo[] parameterInformation)
        {
            Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo> resultList = new Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo>();

            if (parameterInformation != null)
            {
                foreach (ParameterInfo info in parameterInformation)
                    resultList.Add(info.Name, info);
            }

            return resultList;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Retrieves the parameter with given name. Enables mandatory parameters checks.
        /// </summary>
        /// <param name="parameterName">Name of parameter to get</param>
        /// <returns>The parameter, or null if this parameter does not exist</returns>
        /// <exception cref="MissingParameterException">If mandatory parameter is missing</exception>
        protected string _GetParameter(PatchInstructionParameter.ParameterName parameterName)
        {
            return _GetParameter(parameterName, true);
        }

        /// <summary>
        /// 
        /// {</summary>
        /// <param name="parameterName"></param>
        /// <param name="evaluateVariable"></param>
        /// <returns></returns>
        protected string _GetParameter(PatchInstructionParameter.ParameterName parameterName, bool evaluateVariable)
        {
            bool requiredParameter = false;
            string returnedValue = null;

            try
            {
                // Parameter information
                ParameterInfo info = SupportedParameterInformation[parameterName];

                requiredParameter = info.Required;

                // Retrieving parameter value...
                PatchInstructionParameter resultPip = _Parameters[parameterName.ToString()];

                if (resultPip != null)
                {
                    if (evaluateVariable)
                        returnedValue = PatchInstructionParameter.EvaluateParameter(resultPip.Value);
                    else
                        returnedValue = resultPip.Value;
                }
                else if (requiredParameter)
                    throw new MissingParameterException(parameterName);
            }
            catch (Exception ex)
            {
                Exception2.PrintStackTrace(ex);

                if (requiredParameter)
                    throw new MissingParameterException(parameterName, ex);
            }

            return returnedValue;
        }

        /// <summary>
        /// Creates a value copy of current parameters
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, PatchInstructionParameter> _CloneParameters()
        {
            Dictionary<string, PatchInstructionParameter> returnClone = new Dictionary<string, PatchInstructionParameter>();

            foreach (KeyValuePair<string, PatchInstructionParameter> valuePair in _Parameters)
            {
                string parameterName = valuePair.Key.Clone() as string;
                PatchInstructionParameter parameterValue = valuePair.Value.Clone() as PatchInstructionParameter;

                if (parameterName != null)
                    returnClone.Add(parameterName, parameterValue);
            }

            return returnClone;
        }
        #endregion

        #region Method(s) to implement
        /// <summary>
        /// What the instruction should do
        /// </summary>
        protected abstract void _Process();
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
            PatchInstruction clonePi = MakeInstruction(_Type.ToString());

            clonePi._Enabled = _Enabled;
            clonePi._FailOnError = _FailOnError;
            clonePi._Order = _Order;
            clonePi._Parameters = _CloneParameters();
            clonePi._Comment = _Comment.Clone() as string;
            clonePi._Group = _Group;

            return clonePi;
        }
        #endregion
    }
}
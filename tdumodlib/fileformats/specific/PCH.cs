using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using DjeFramework1.Common.Support.Meta;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.support;
using TDUModdingLibrary.support.patcher.instructions;
using TDUModdingLibrary.support.patcher.parameters;
using DjeFramework1.Common.Support.Traces;

namespace TDUModdingLibrary.fileformats.specific
{
    /// <summary>
    /// Represents a patch file for installing mods
    /// </summary>
    public class PCH:TduFile
    {
        #region Constants
        /// <summary>
        /// Pattern for file name (to redefine)
        /// </summary>
        public new static readonly string FILENAME_PATTERN = string.Format(String2.REGEX_PATTERN_EXTENSION, "PCH");

        /// <summary>
        /// Required group name
        /// </summary>
        public const string REQUIRED_GROUP_NAME = "Required";

        /// <summary>
        /// Default filename for installer executable
        /// </summary>
        public const string INSTALLER_FILE_NAME = "tdu_mod_installer.exe";

        /// <summary>
        /// Default patch name
        /// </summary>
        private const string _DEFAULT_NAME = "Untitled";
        
        /// <summary>
        /// Root node (opening)
        /// </summary>
        private const string _ROOT_NODE_START = "<patcher>";

        /// <summary>
        /// Root node (closing)
        /// </summary>
        private const string _ROOT_NODE_END = "</patcher>";

        /// <summary>
        /// Node for patch properties
        /// </summary>
        private const string _PROPERTIES_NODE = "properties";

        /// <summary>
        /// Node for patch instructions
        /// </summary>
        private const string _INSTRUCTIONS_NODE = "instructions";

        /// <summary>
        /// Node for a single instruction
        /// </summary>
        private const string _SINGLE_INSTRUCTION_NODE = "instruction";

        /// <summary>
        /// Node for a single parameter
        /// </summary>
        private const string _SINGLE_PARAMETER_NODE = "parameter";
        
        /// <summary>
        /// Node for a single parameter
        /// </summary>
        private const string _PARAMETER_NODE = "parameter";

        /// <summary>
        /// Node for role list
        /// </summary>
        private const string _ROLES_NODE = "roles";

        /// <summary>
        /// Node for group list
        /// </summary>
        private const string _GROUPS_NODE = "groups";

        /// <summary>
        /// Node for exclusion list
        /// </summary>
        private const string _EXCLUSIONS_NODE = "exclusions";

        /// <summary>
        /// Node for a single role
        /// </summary>
        private const string _ROLE_NODE = "role";

        /// <summary>
        /// Node for a single dependancy
        /// </summary>
        private const string _DEPENDENCY_NODE = "dependancy";

        /// <summary>
        /// Node for a single exclusion
        /// </summary>
        private const string _EXCLUSION_NODE = "exclusion";

        /// <summary>
        /// Attribute: project name
        /// </summary>
        private const string _NAME_ATTRIBUTE = "name";

        /// <summary>
        /// Attribute: author name
        /// </summary>
        private const string _AUTHOR_ATTRIBUTE = "author";
        
        /// <summary>
        /// Attribute: patch date
        /// </summary>
        private const string _DATE_ATTRIBUTE = "date";

        /// <summary>
        /// Attribute: version
        /// </summary>
        private const string _VERSION_ATTRIBUTE = "version";

        /// <summary>
        /// Attribute: free
        /// </summary>
        private const string _FREE_ATTRIBUTE = "free";

        /// <summary>
        /// Attribute: ref
        /// </summary>
        private const string _SLOT_REF_ATTRIBUTE = "ref";

        /// <summary>
        /// Attribute: infoURL
        /// </summary>
        private const string _INFO_URL_ATTRIBUTE = "infoURL";

        /// <summary>
        /// Attribute: installerFileName
        /// </summary>
        private const string _INSTALLER_FILE_NAME_ATTRIBUTE = "installerFileName";

        /// <summary>
        /// Attribute: customRequired
        /// </summary>
        private const string _CUSTOM_REQUIRED_ATTRIBUTE = "customRequired";

        /// <summary>
        /// Attribute: type
        /// </summary>
        private const string _TYPE_ATTRIBUTE = "type";

        /// <summary>
        /// Attribute: failOnError
        /// </summary>
        private const string _FAIL_ON_ERROR_ATTRIBUTE = "failOnError";

        /// <summary>
        /// Attribute: enabled
        /// </summary>
        private const string _ENABLED_ATTRIBUTE = "enabled";

        /// <summary>
        /// Attribute: value
        /// </summary>
        private const string _VALUE_ATTRIBUTE = "value";

        /// <summary>
        /// Attribute: comment
        /// </summary>
        private const string _COMMENT_ATTRIBUTE = "comment";

        /// <summary>
        /// Attribute: group
        /// </summary>
        private const string _GROUP_ATTRIBUTE = "group";

        /// <summary>
        /// Attribute: requiredGroup
        /// </summary>
        private const string _REQUIRED_ATTRIBUTE = "requiredGroup";

        /// <summary>
        /// Attribute: defaultChecked
        /// </summary>
        private const string _CHECKED_ATTRIBUTE = "defaultChecked";

        /// <summary>
        /// Attribute: what
        /// </summary>
        private const string _WHAT_ATTRIBUTE = "what";

        /// <summary>
        /// Error message when saving patch to PCH file
        /// </summary>
        private const string _ERROR_SAVING_PATCH = "Unable to write patch file contents.";

        /// <summary>
        /// Error message when loading patch from PCH file
        /// </summary>
        private const string _ERROR_LOADING_PATCH = "Unable to read patch file contents.";

        /// <summary>
        /// Modeler role
        /// </summary>
        private const string _ROLE_MODELER = "Modeling";

        /// <summary>
        /// Converter role
        /// </summary>
        private const string _ROLE_CONVERTER = "Convert";

        /// <summary>
        /// Sound role
        /// </summary>
        private const string _ROLE_SOUND = "Sound making";

        /// <summary>
        /// Gauges role
        /// </summary>
        private const string _ROLE_GAUGES = "Gauges making";

        /// <summary>
        /// Testing role
        /// </summary>
        private const string _ROLE_TESTING = "Testing";

        /// <summary>
        /// Tool developer role
        /// </summary>
        private const string _ROLE_TOOLS = "Tools development";

        /// <summary>
        /// Custom role
        /// </summary>
        public const string ROLE_CUSTOM1 = "Custom 1";

        /// <summary>
        /// Custome role
        /// </summary>
        public const string ROLE_CUSTOM2 = "Custom 2";
        
        /// <summary>
        /// Unknown name
        /// </summary>
        private const string _NAME_UNKNOWN = "Unknown";

        /// <summary>
        /// Djey name
        /// </summary>
        private const string _NAME_DJEY = "Djey";

        /// <summary>
        /// EDEN GAMES name
        /// </summary>
        private const string _NAME_EDEN = "EDEN GAMES";
        #endregion

        #region Structures and types
        /// <summary>
        /// Represents an install group
        /// </summary>
        public struct InstallGroup
        {
            public string name;
            public string parentName;
            public Collection<string> excludedGroupNames;
            public bool isDefaultChecked;
            public bool exists;
        }
        #endregion

        #region Properties
        /// <summary>
        /// List of instructions to be applied
        /// </summary>
        public Collection<PatchInstruction> PatchInstructions
        {
            get { return _PatchInstructions; }
        }
        private readonly Collection<PatchInstruction> _PatchInstructions = new Collection<PatchInstruction>();

        /// <summary>
        /// Patch or project name
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set {_Name = value; }
        }
        private string _Name = _DEFAULT_NAME;

        /// <summary>
        /// Patch version
        /// </summary>
        public string Version
        {
            get { return _Version; }
            set { _Version = value; }
        }
        private string _Version = "";

        /// <summary>
        /// Patch or project author
        /// </summary>
        public string Author
        {
            get { return _Author; }
            set { _Author = value; }
        }
        private string _Author = "";

        /// <summary>
        /// Patch date
        /// </summary>
        public string Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        private string _Date = "";

        /// <summary>
        /// Free comment
        /// </summary>
        public string Free
        {
            get { return _Free; }
            set { _Free = value; }
        }
        private string _Free = "";

        /// <summary>
        /// Reference of car slot to install (for authorization checks)
        /// </summary>
        public string SlotRef
        {
            get { return _SlotRef;  }
            set
            {
                // Some values are not allowed
                if (Tools.KEY_REMAPPING_SLOT.Equals(value))
                    throw new Exception("Specified slot can't be used.");

                 _SlotRef = value;
            }
        }
        private string _SlotRef = Tools.KEY_MISC_SLOT;

        /// <summary>
        /// Roles in this project
        /// </summary>
        public Dictionary<string, string> Roles
        {
            get { return _Roles; }
        }
        private readonly Dictionary<string, string> _Roles = new Dictionary<string, string>();

        /// <summary>
        /// Install groups in this patch
        /// </summary>
        public List<InstallGroup> Groups
        {
            get { return _Groups; }
        }
        private readonly List<InstallGroup> _Groups = new List<InstallGroup>();

        /// <summary>
        /// Custom name for required group
        /// </summary>
        public string CustomRequiredName
        {
            get {return _CustomRequiredName;}
            set { _CustomRequiredName = value;}
        }
        private string _CustomRequiredName = REQUIRED_GROUP_NAME;
        
        /// <summary>
        /// Installer executable file name
        /// </summary>
        public string InstallerFileName
        {
            get { return _InstallerFileName; }
            set { _InstallerFileName = value; }
        }
        private string _InstallerFileName = INSTALLER_FILE_NAME;

        /// <summary>
        /// Information URL
        /// </summary>
        public string InfoURL { get; set; }
        #endregion

        #region TDUFile implementation
        /// <summary>
        /// Reads patch from a PCH (XML format) file
        /// </summary>
        protected override sealed void _ReadData()
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(FileName);

                // Properties
                XmlElement docElement = doc.DocumentElement;

                if (docElement != null)
                {
                    XmlNode propNode = docElement.SelectSingleNode(_PROPERTIES_NODE);

                    _Name = propNode.Attributes[_NAME_ATTRIBUTE].Value;
                    _Version = propNode.Attributes[_VERSION_ATTRIBUTE].Value;
                    _Author = propNode.Attributes[_AUTHOR_ATTRIBUTE].Value;
                    _Date = propNode.Attributes[_DATE_ATTRIBUTE].Value;
                    _Free = Xml2.GetAttributeWithDefaultValue(propNode, _FREE_ATTRIBUTE, "");
                    _InstallerFileName = Xml2.GetAttributeWithDefaultValue(propNode, _INSTALLER_FILE_NAME_ATTRIBUTE,
                                                                           INSTALLER_FILE_NAME);

                    // EVO_131: roles
                    _RetrieveRoles(propNode);

                    // EVO_134: groups
                    _RetrieveGroups(propNode);

                    // New attributes
                    _SlotRef = Xml2.GetAttributeWithDefaultValue(propNode, _SLOT_REF_ATTRIBUTE, "");
                    InfoURL = Xml2.GetAttributeWithDefaultValue(propNode, _INFO_URL_ATTRIBUTE, "");

                    // Instructions
                    XmlNode instrNode = docElement.SelectSingleNode(_INSTRUCTIONS_NODE);
                    XmlNodeList allInstructionNodes = instrNode.SelectNodes(_SINGLE_INSTRUCTION_NODE);
                    int order = 1;

                    if (allInstructionNodes != null)
                    {
                        foreach (XmlNode anotherInstructionNode in allInstructionNodes)
                        {
                            try
                            {
                                PatchInstruction pi = _ProcessInstruction(anotherInstructionNode, order);

                                if (pi == null)
                                    throw new Exception();
                                _PatchInstructions.Add(pi);

                                // Groups update
                                if (!_Groups.Contains(pi.Group))
                                    _Groups.Add(pi.Group);

                                order++;
                            }
                            catch (Exception ex)
                            {
                                // Current instruction won't be added
                                Exception2.PrintStackTrace(new Exception("Invalid instruction.", ex));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Silent exception
                Exception2.PrintStackTrace(new Exception(_ERROR_LOADING_PATCH,ex));
            }

            // EVO_65: Properties
            Property.ComputeValueDelegate instructionCountDelegate = () => PatchInstructions.Count.ToString();
            Property.ComputeValueDelegate nameDelegate = () => Name;
            Property.ComputeValueDelegate authorDelegate = () => Author;
            Property.ComputeValueDelegate dateDelegate = () => Date;
            Property.ComputeValueDelegate versionDelegate = () => Version;
            Property.ComputeValueDelegate slotDelegate = () => SlotRef;
            Property.ComputeValueDelegate groupCountDelegate = () => Groups.Count.ToString();
            Property.ComputeValueDelegate installerDelegate = () => InstallerFileName;
            Property.ComputeValueDelegate urlDelegate = () => InfoURL;

            Properties.Add(new Property("Patch name", "Patch", nameDelegate));
            Properties.Add(new Property("Author", "Patch", authorDelegate));
            Properties.Add(new Property("Date", "Patch", dateDelegate));
            Properties.Add(new Property("Version", "Patch", versionDelegate));
            Properties.Add(new Property("Group count", "Patch", groupCountDelegate));
            Properties.Add(new Property("Instruction count", "Patch", instructionCountDelegate));
            Properties.Add(new Property("Slot reference", "Patch", slotDelegate));
            Properties.Add(new Property("Installer file name", "Patch", installerDelegate)); 
            Properties.Add(new Property("Information URL", "Patch", urlDelegate));
        }

        /// <summary>
        /// Saves patch to a PCH (XML format) file
        /// </summary>
        public override void Save()
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                // Header
                doc.LoadXml(Xml2.XML_1_0_HEADER + _ROOT_NODE_START + _ROOT_NODE_END);

                // Properties
                XmlElement propElement = doc.CreateElement(_PROPERTIES_NODE);

                propElement.SetAttribute(_NAME_ATTRIBUTE, _Name);
                propElement.SetAttribute(_VERSION_ATTRIBUTE, _Version);
                propElement.SetAttribute(_AUTHOR_ATTRIBUTE, _Author);
                propElement.SetAttribute(_DATE_ATTRIBUTE, _Date);
                propElement.SetAttribute(_SLOT_REF_ATTRIBUTE, _SlotRef);
                propElement.SetAttribute(_FREE_ATTRIBUTE, _Free);
                propElement.SetAttribute(_INSTALLER_FILE_NAME_ATTRIBUTE, _InstallerFileName);
                propElement.SetAttribute(_INFO_URL_ATTRIBUTE, InfoURL);

                // EVO_131: roles
                _WriteRoles(doc, propElement);

                // EVO_134: groups
                _WriteGroups(doc, propElement);

                XmlElement docElement = doc.DocumentElement;

                if (docElement != null)
                {
                    docElement.AppendChild(propElement);

                    // Instructions
                    XmlElement instructionsElement = doc.CreateElement(_INSTRUCTIONS_NODE);

                    foreach (PatchInstruction instr in _PatchInstructions)
                        _SaveInstruction(instr, doc, instructionsElement);

                    docElement.AppendChild(instructionsElement);                    
                }

                // End
                doc.Save(_FileName);
            }
            catch (Exception ex)
            {
                Exception2.PrintStackTrace(ex);
                throw new Exception(_ERROR_SAVING_PATCH,ex);
            }
        }
        #endregion

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="fileName">Patch file name</param>
        internal PCH(string fileName)
        {
            _FileName = fileName;

            if (File.Exists(fileName))
                _ReadData();
            else
                _SetStandardRoles();
        }

        internal PCH(){}
        #region Private methods
        /// <summary>
        /// Method to create an instruction from a XML node
        /// </summary>
        /// <param name="node">node to process</param>
        /// <param name="order">instruction order</param>
        /// <returns>Correponding instruction</returns>
        private PatchInstruction _ProcessInstruction(XmlNode node, int order)
        {
            PatchInstruction processedPI = null;

            if (node != null && order > 0)
            {
                // Main attributes
                string instructionType = node.Attributes[_TYPE_ATTRIBUTE].Value;
                string isFailOnError = Xml2.GetAttributeWithDefaultValue(node, _FAIL_ON_ERROR_ATTRIBUTE, true.ToString());
                string isEnabled = Xml2.GetAttributeWithDefaultValue(node, _ENABLED_ATTRIBUTE, true.ToString());
                string comment = Xml2.GetAttributeWithDefaultValue(node, _COMMENT_ATTRIBUTE, "");
                string groupName = Xml2.GetAttributeWithDefaultValue(node, _GROUP_ATTRIBUTE, REQUIRED_GROUP_NAME);

                processedPI = PatchInstruction.MakeInstruction(instructionType);

                if (processedPI != null)
                {
                    processedPI.FailOnError = bool.Parse(isFailOnError);
                    processedPI.Enabled = bool.Parse(isEnabled);
                    processedPI.Order = order;
                    processedPI.Comment = comment;

                    if (string.IsNullOrEmpty(groupName))
                        groupName = REQUIRED_GROUP_NAME;

                    processedPI.Group = GetGroupFromName(groupName);

                    // Instruction parameters
                    XmlNodeList allParameterNodes = node.SelectNodes(_PARAMETER_NODE);

                    if (allParameterNodes != null)
                    {
                        foreach (XmlNode anotherParameterNode in allParameterNodes)
                        {
                            PatchInstructionParameter pip = _ProcessParameter(anotherParameterNode);

                            if (pip == null)
                                throw new Exception("Invalid parameter: " + anotherParameterNode.Value);

                            processedPI.Parameters.Add(pip.Name, pip);
                        }
                    }
                }
            }

            return processedPI;
        }

        /// <summary>
        /// Returns group according to provided name. If group does not exist, it is created and returned
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public InstallGroup GetGroupFromName(string groupName) 
        {
            InstallGroup returnedGroup = new InstallGroup { exists = true };

            if (!string.IsNullOrEmpty(groupName))
            {
                bool isFound = false;

                foreach (InstallGroup anotherGroup in _Groups)
                {
                    if (groupName.Equals(anotherGroup.name))
                    {
                        returnedGroup = anotherGroup;
                        isFound = true;
                        break;
                    }
                }

                if (!isFound)
                {
                    returnedGroup = new InstallGroup {name = groupName, exists = true};
                    _Groups.Add(returnedGroup);
                }
            }

            return returnedGroup;
        }

        /// <summary>
        /// Method to create a parameter from a XML node
        /// </summary>
        /// <param name="anotherParameterNode">node to process</param>
        /// <returns>Corresponding parameter</returns>
        private static PatchInstructionParameter _ProcessParameter(XmlNode anotherParameterNode)
        {
            PatchInstructionParameter param = null;
            
            if (anotherParameterNode != null)
            {
                string parameterName = anotherParameterNode.Attributes[_NAME_ATTRIBUTE].Value;
                string parameterValue = anotherParameterNode.Attributes[_VALUE_ATTRIBUTE].Value;

                param = PatchInstructionParameter.MakeParameter(parameterName);

                if (param == null)
                    throw new Exception("Invalid parameter: " + anotherParameterNode.Value);

                param.Value = parameterValue;
            }

            return param;
        }

        /// <summary>
        /// Handles XML writing of specified instruction
        /// </summary>
        /// <param name="instr">Instruction to write</param>
        /// <param name="doc">Main XML document</param>
        /// <param name="instructionsElement">Instruction collection node. If null, written element becomes document element</param>
        private static void _SaveInstruction(PatchInstruction instr, XmlDocument doc, XmlNode instructionsElement)
        {
            if (instr != null && doc != null)
            {
                XmlElement currentInstructionElement = doc.CreateElement(_SINGLE_INSTRUCTION_NODE);

                if (instructionsElement == null)
                    doc.AppendChild(currentInstructionElement);
                else
                    instructionsElement.AppendChild(currentInstructionElement);

                // Instruction attributes
                currentInstructionElement.SetAttribute(_TYPE_ATTRIBUTE, instr.Name);
                currentInstructionElement.SetAttribute(_FAIL_ON_ERROR_ATTRIBUTE, instr.FailOnError.ToString());
                currentInstructionElement.SetAttribute(_ENABLED_ATTRIBUTE, instr.Enabled.ToString());
                currentInstructionElement.SetAttribute(_COMMENT_ATTRIBUTE, instr.Comment);

                string groupName = instr.Group.name;

                if (string.IsNullOrEmpty(groupName))
                    groupName = REQUIRED_GROUP_NAME;

                currentInstructionElement.SetAttribute(_GROUP_ATTRIBUTE, groupName);

                // Parameters
                foreach (KeyValuePair<string, PatchInstructionParameter> pair in instr.Parameters)
                    _SaveParameter(pair.Value, doc, currentInstructionElement);
            }
        }

        /// <summary>
        /// Handles XML writing of specified parameter
        /// </summary>
        /// <param name="param">Parameter to write</param>
        /// <param name="doc">Main XML document</param>
        /// <param name="instruction">Instruction node</param>
        private static void _SaveParameter(PatchInstructionParameter param, XmlDocument doc, XmlNode instruction)
        {
            if (param == null || doc == null || instruction == null)
                return;

            XmlElement currentParameter = doc.CreateElement(_SINGLE_PARAMETER_NODE);

            instruction.AppendChild(currentParameter);

            // Parameter attributes
            currentParameter.SetAttribute(_NAME_ATTRIBUTE, param.Name);
            currentParameter.SetAttribute(_VALUE_ATTRIBUTE, param.Value);
        }

        /// <summary>
        /// Reads role list from specified XML node to update role list
        /// </summary>
        /// <param name="propNode"></param>
        private void _RetrieveRoles(XmlNode propNode)
        {
            _Roles.Clear();

            try
            {
                XmlNode rolesNode = propNode.SelectSingleNode(_ROLES_NODE);
                XmlNodeList allRoles = rolesNode.SelectNodes(_ROLE_NODE);

                if (allRoles != null)
                {
                    foreach (XmlNode anotherRole in allRoles)
                    {
                        string role = anotherRole.Attributes[_WHAT_ATTRIBUTE].Value;
                        string name = Xml2.GetAttributeWithDefaultValue(anotherRole, _NAME_ATTRIBUTE, _NAME_UNKNOWN);

                        _Roles.Add(role, name);
                    }
                }
            }
            catch (Exception ex)
            {
                Exception2.PrintStackTrace(new Exception("Roles not defined in patch file! Using defaults.", ex));
            }

            // If no role is set, standard roles are set
            if (_Roles.Count == 0)
                _SetStandardRoles();
        }

        /// <summary>
        /// Reads group dependency and exclusion from specified XML node to update list
        /// </summary>
        /// <param name="propNode"></param>
        private void _RetrieveGroups(XmlNode propNode)
        {
            _Groups.Clear();

            try
            {
                XmlNode groupsNode = propNode.SelectSingleNode(_GROUPS_NODE);

                if (groupsNode != null)
                {
                    // Custom required group name
                    _CustomRequiredName = Xml2.GetAttributeWithDefaultValue(groupsNode, _CUSTOM_REQUIRED_ATTRIBUTE,
                                                        REQUIRED_GROUP_NAME);

                    // Dependencies and exclusions
                    XmlNodeList allDependancies = groupsNode.SelectNodes(_DEPENDENCY_NODE);

                    if (allDependancies != null)
                    {
                        foreach (XmlNode anotherDep in allDependancies)
                        {
                            string groupName = anotherDep.Attributes[_GROUP_ATTRIBUTE].Value;
                            string requiredName = Xml2.GetAttributeWithDefaultValue(anotherDep, _REQUIRED_ATTRIBUTE, null);
                            bool isDefaultChecked = bool.Parse(Xml2.GetAttributeWithDefaultValue(anotherDep, _CHECKED_ATTRIBUTE,"false"));
                            Collection<string> excludedGroups = _RetrieveExcludedGroups(anotherDep);

                            _Groups.Add(new InstallGroup { name = groupName, parentName = requiredName, isDefaultChecked = isDefaultChecked, excludedGroupNames = excludedGroups, exists = true});
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exception2.PrintStackTrace(new Exception("Roles not defined in patch file! Using defaults.", ex));
            }

            // If no role is set, standard roles are set
            if (_Roles.Count == 0)
                _SetStandardRoles();
        }

        /// <summary>
        /// Reads group exclusion list from specified XML node to update list
        /// </summary>
        /// <param name="groupNode"></param>
        private static Collection<string> _RetrieveExcludedGroups(XmlNode groupNode)
        {
            Collection<string> returnedNames = new Collection<string>();
            XmlNode exclusionsNode = groupNode.SelectSingleNode(_EXCLUSIONS_NODE);

            if (exclusionsNode != null)
            {
                try
                {
                    XmlNodeList allExclusions = exclusionsNode.SelectNodes(_EXCLUSION_NODE);

                    if (allExclusions != null)
                    {
                        foreach (XmlNode anotherExc in allExclusions)
                        {
                            string groupName = anotherExc.Attributes[_GROUP_ATTRIBUTE].Value;

                            if (!returnedNames.Contains(groupName))
                                returnedNames.Add(groupName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Exception2.PrintStackTrace(new Exception("Dependencies not defined in patch file! Using defaults.", ex));
                }
            }

            return returnedNames;
        }

        /// <summary>
        /// Defines standard roles
        /// </summary>
        private void _SetStandardRoles()
        {
            _Roles.Add(_ROLE_MODELER, _NAME_EDEN);
            _Roles.Add(_ROLE_CONVERTER, _NAME_EDEN);
            _Roles.Add(_ROLE_SOUND, _NAME_EDEN);
            _Roles.Add(_ROLE_GAUGES, _NAME_EDEN);
            _Roles.Add(_ROLE_TESTING, _NAME_UNKNOWN);
            _Roles.Add(_ROLE_TOOLS, _NAME_DJEY);
            _Roles.Add(ROLE_CUSTOM1, _NAME_UNKNOWN);
            _Roles.Add(ROLE_CUSTOM2, _NAME_UNKNOWN);
        }

        /// <summary>
        /// Updates specified XML node with current role information
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="element"></param>
        private void _WriteRoles(XmlDocument doc, XmlNode element)
        {
            if (doc != null && element != null)
            {
                XmlElement rolesElement = doc.CreateElement(_ROLES_NODE);

                foreach(KeyValuePair<string, string> roleInformation in _Roles)
                {
                    XmlElement roleElement = doc.CreateElement(_ROLE_NODE);
                    XmlAttribute roleAttribute = doc.CreateAttribute(_WHAT_ATTRIBUTE);
                    XmlAttribute nameAttribute = doc.CreateAttribute(_NAME_ATTRIBUTE);

                    roleAttribute.Value = roleInformation.Key;
                    nameAttribute.Value = roleInformation.Value;
                    roleElement.Attributes.Append(roleAttribute);
                    roleElement.Attributes.Append(nameAttribute);

                    rolesElement.AppendChild(roleElement);
                }

                element.AppendChild(rolesElement);
            }
        }

        /// <summary>
        /// Updates specified XML node with group dependancies and exclusions
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="element"></param>
        private void _WriteGroups(XmlDocument doc, XmlNode element)
        {
            if (doc != null && element != null)
            {
                XmlElement groupsElement = doc.CreateElement(_GROUPS_NODE);

                // Custom required group name
                XmlAttribute customRequiredAttribute = doc.CreateAttribute(_CUSTOM_REQUIRED_ATTRIBUTE);

                customRequiredAttribute.Value = _CustomRequiredName;
                groupsElement.Attributes.Append(customRequiredAttribute);
                
                // Dependencies
                foreach (InstallGroup dependancyInfo in _Groups)
                {
                    if (!REQUIRED_GROUP_NAME.Equals(dependancyInfo.name) /*&& !string.IsNullOrEmpty(dependancyInfo.parentName)*/)
                    {
                        XmlElement depElement = doc.CreateElement(_DEPENDENCY_NODE);
                        XmlAttribute groupAttribute = doc.CreateAttribute(_GROUP_ATTRIBUTE);
                        XmlAttribute requiredAttribute = doc.CreateAttribute(_REQUIRED_ATTRIBUTE);
                        XmlAttribute checkedAttribute = doc.CreateAttribute(_CHECKED_ATTRIBUTE);

                        groupAttribute.Value = dependancyInfo.name;
                        requiredAttribute.Value = dependancyInfo.parentName;
                        checkedAttribute.Value = dependancyInfo.isDefaultChecked.ToString();
                        depElement.Attributes.Append(groupAttribute);
                        depElement.Attributes.Append(requiredAttribute);
                        depElement.Attributes.Append(checkedAttribute);

                        // Exclusions
                        _WriteExclusions(doc, depElement, dependancyInfo.excludedGroupNames);

                        groupsElement.AppendChild(depElement);
                    }
                }

                element.AppendChild(groupsElement);
            }
        }

        /// <summary>
        /// Updates specified XML node with group exclusions
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="depElement"></param>
        /// <param name="excludedGroupNames"></param>
        private static void _WriteExclusions(XmlDocument doc, XmlElement depElement, IEnumerable<string> excludedGroupNames)
        {
            if (doc != null && depElement != null && excludedGroupNames != null)
            {
                XmlElement exclusionsElement = doc.CreateElement(_EXCLUSIONS_NODE);

                foreach (string groupName in excludedGroupNames)
                {
                    XmlElement excElement = doc.CreateElement(_EXCLUSION_NODE);
                    XmlAttribute groupAttribute = doc.CreateAttribute(_GROUP_ATTRIBUTE);

                    groupAttribute.Value = groupName;
                    excElement.Attributes.Append(groupAttribute);

                    exclusionsElement.AppendChild(excElement);
                }

                depElement.AppendChild(exclusionsElement);
            }
        }

        /// <summary>
        /// Returns group in specified list
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="groups"></param>
        /// <returns></returns>
        private static InstallGroup _GetGroup(string groupName, IEnumerable<InstallGroup> groups)
        {
            InstallGroup returnedGroup = new InstallGroup();

            if (groups != null)
            {
                foreach (InstallGroup anotherGroup in groups)
                {
                    if (anotherGroup.name.Equals(groupName))
                        returnedGroup = anotherGroup;
                }
            }

            return returnedGroup;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Returns instruction with the specified order
        /// </summary>
        /// <param name="order">Order of the instruction to get</param>
        /// <returns>The instruction, or null if not found</returns>
        public PatchInstruction GetInstruction(int order)
        {
            foreach (PatchInstruction anotherInstruction in _PatchInstructions)
            {
                if (anotherInstruction.Order == order)
                    return anotherInstruction;
            }

            return null;
       }

        /// <summary>
        /// Adds or replace an instruction. If instruction order already exists, corresponding instruction is replaced; else specified instruction is added to the list.
        /// </summary>
        /// <param name="instruction">Instruction to set</param>
        public void SetInstruction(PatchInstruction instruction)
        {
            if (instruction == null || instruction.Order < 1)
                return;

            // Unsupported parameters are removed
            instruction.RemoveUnsupportedParameters();

            if (instruction.Order > PatchInstructions.Count)
            {
                // Add mode
                PatchInstructions.Add(instruction);
            }
            else
            {
                // Replace mode
                for (int i = 0 ; i < PatchInstructions.Count ; i++)
                {
                    PatchInstruction currentInstruction = PatchInstructions[i];

                    if (currentInstruction.Order == instruction.Order)
                    {
                        PatchInstructions[i] = instruction;
                        break;
                    }
                }
            }     
        }

        /// <summary>
        /// Deletes an instruction at specified order
        /// </summary>
        /// <param name="order">Instruction order</param>
        public void DeleteInstructionAt(int order)
        {
            if (order > PatchInstructions.Count)
                return;

            // Retrieving instruction & deletion
            PatchInstruction instructionToDelete = GetInstruction(order);

            if (instructionToDelete != null)
            {
                PatchInstructions.Remove(instructionToDelete);
                
                // Updating order of following instructions
                foreach (PatchInstruction anotherInstruction in _PatchInstructions)
                {
                    if (anotherInstruction.Order > order)
                        anotherInstruction.Order--;
                }
            }
        }

        /// <summary>
        /// Swaps 2 instructions according to their order
        /// </summary>
        /// <param name="order">Order of first instruction</param>
        /// <param name="anotherOrder">Order of second instruction</param>
        public void SwitchInstructions(int order, int anotherOrder)
        {
            if (order == anotherOrder
                || order < 1 
                || anotherOrder < 1 
                || order > PatchInstructions.Count
                || anotherOrder > PatchInstructions.Count)
                return;

            PatchInstruction instr1 = GetInstruction(order);
            PatchInstruction instr2 = GetInstruction(anotherOrder);

            if (instr1 != null && instr2 != null)
            {
                instr1.Order = anotherOrder;
                instr2.Order = order;

                _PatchInstructions[order - 1] = instr2;
                _PatchInstructions[anotherOrder - 1] = instr1;
            }
        }
        
        /// <summary>
        /// Duplicates current properties to specified patch (instruction list is not considered as property)
        /// </summary>
        /// <param name="patch"></param>
        public void DuplicateProperties(PCH patch)
        {
            if (patch != null)
            {
                patch._Author = _Author;
                patch._Date = _Date;
                patch._Name = _Name;
                patch._SlotRef = _SlotRef;
                patch._Version = _Version;
                patch._InstallerFileName = _InstallerFileName;
            }
        }

        /// <summary>
        /// Imports instruction(s) according to provided bribe
        /// </summary>
        /// <param name="bribe">Instructions XML bribe</param>
        public void ImportInstruction(string bribe)
        {
            if (!string.IsNullOrEmpty(bribe))
            {
                XmlDocument doc = new XmlDocument();

                doc.LoadXml(bribe);

                try
                {
                    XmlNode instructionsNode = doc.DocumentElement;

                    if (instructionsNode != null)
                    {
                        XmlNodeList allInstructionNodes = instructionsNode.SelectNodes(_SINGLE_INSTRUCTION_NODE);
                        int order = _PatchInstructions.Count + 1;

                        if (allInstructionNodes != null)
                        {
                            foreach (XmlNode anotherInstructionNode in allInstructionNodes)
                            {
                                PatchInstruction pi = _ProcessInstruction(anotherInstructionNode, order);

                                if (pi == null)
                                    Log.Warning("Invalid instruction - can't be imported.");
                                else
                                {
                                    _PatchInstructions.Add(pi);
                                    order++;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Current instruction won't be added
                    Exception2.PrintStackTrace(ex);
                }
            }
        }

        /// <summary>
        /// Import specified instruction to current patch
        /// </summary>
        /// <param name="anotherInstruction"></param>
        public void ImportInstruction(PatchInstruction anotherInstruction)
        {
            if (anotherInstruction != null)
            {
                anotherInstruction.Order = PatchInstructions.Count + 1;

                PatchInstructions.Add(anotherInstruction);
            }
        }

        /// <summary>
        /// Exports instruction bribe to specified file
        /// </summary>
        /// <param name="instructionsToExport">List of instructions to export</param>
        /// <param name="writer">XML writer to export to - must be initialized</param>
        public void ExportInstruction(List<PatchInstruction> instructionsToExport, XmlWriter writer)
        {
            if (instructionsToExport != null && writer != null)
            {
                XmlDocument doc = new XmlDocument();

                try
                {
                    XmlElement instructionsElement = doc.CreateElement(_INSTRUCTIONS_NODE);

                    foreach(PatchInstruction anotherInstruction in instructionsToExport)
                        _SaveInstruction(anotherInstruction, doc, instructionsElement);

                    doc.AppendChild(instructionsElement);
                }
                catch (Exception ex)
                {
                    // Current instruction won't be exported
                    Exception2.PrintStackTrace(ex);
                }

                doc.Save(writer);
            }
        }

        /// <summary>
        /// Sets role name at specified index
        /// </summary>
        /// <param name="roleIndex"></param>
        /// <param name="roleName"></param>
        public void SetRoleName(int roleIndex, string roleName)
        {
            if (roleName != null && roleIndex >= 0 && roleIndex < _Roles.Count)
            {
                KeyValuePair<string,string> roleInformation = new KeyValuePair<string, string>(null, null);
                int currentIndex = 0;

                foreach (KeyValuePair<string,string> keyValuePair in _Roles)
                {
                    if (currentIndex == roleIndex)
                    {
                        roleInformation = keyValuePair;
                        break;
                    }

                    currentIndex++;
                }

                if (roleInformation.Key != null && !_Roles.ContainsKey(roleName))
                {
                    // Removing current instance
                    _Roles.Remove(roleInformation.Key);

                    // Updating information
                    _Roles.Add(roleName, roleInformation.Value);
                }
                else 
                    throw new Exception(roleName + " role name can't be used.");
            }
        }

        /// <summary>
        /// Sets author name at specified index
        /// </summary>
        /// <param name="roleIndex"></param>
        /// <param name="authorName"></param>
        public void SetRoleAuthorName(int roleIndex, string authorName)
        {
            if (authorName != null && roleIndex >= 0 && roleIndex < _Roles.Count)
            {
                KeyValuePair<string, string> roleInformation = new KeyValuePair<string, string>(null, null);
                int currentIndex = 0;

                foreach (KeyValuePair<string, string> keyValuePair in _Roles)
                {
                    if (currentIndex == roleIndex)
                    {
                        roleInformation = keyValuePair;
                        break;
                    }

                    currentIndex++;
                }

                if (roleInformation.Key != null)
                {
                    // Updating information
                    _Roles[roleInformation.Key] = authorName;
                }
            }
        }

        /// <summary>
        /// Updates specified group
        /// </summary>
        /// <param name="newGroup"></param>
        public void UpdateGroup(InstallGroup newGroup)
        {
            // Searches group with the same name...
            int index = 0;
            bool isFound = false;

            foreach(InstallGroup anotherGroup in _Groups)
            {
                if (newGroup.name != null 
                    && newGroup.name.Equals(anotherGroup.name))
                {
                    isFound = true;
                    break;
                }

                index++;
            }

            if (isFound)
                _Groups[index] = newGroup;
        }

        /// <summary>
        /// Computes et return dependancy level of specified group in list
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="groups"></param>
        /// <returns>-1 if specified group does not exist</returns>
        public static int GetDependancyLevel(string groupName, List<InstallGroup> groups)
        {
            // Retrieving group in list
            int returnedLevel = -1;
            InstallGroup startGroup = _GetGroup(groupName, groups);

            if (startGroup.name != null)
            {
                returnedLevel++;

                while (startGroup.parentName != null)
                {
                    returnedLevel++;

                    startGroup = _GetGroup(startGroup.parentName, groups);
                }
            }

            return returnedLevel;
        }

        /// <summary>
        /// Removes group with specified name
        /// </summary>
        /// <param name="groupToDelete"></param>
        public void RemoveGroup(string groupToDelete)
        {
            // Required group can't be removed
            if (!REQUIRED_GROUP_NAME.Equals(groupToDelete))
            {
                InstallGroup currentGroup = _GetGroup(groupToDelete, _Groups);

                if (currentGroup.exists)
                {
                    _Groups.Remove(currentGroup);

                    // Set instructions to required group
                    InstallGroup requiredGroup = _GetGroup(REQUIRED_GROUP_NAME, _Groups);

                    for (int i = 0 ; i < _PatchInstructions.Count ; i++)
                    {
                        PatchInstruction currentInstruction = _PatchInstructions[i];

                        if (currentInstruction.Group.Equals(currentGroup))
                            currentInstruction.Group = requiredGroup;
                    }
                }
            }
        }
        #endregion
    }
}
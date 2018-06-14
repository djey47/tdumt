using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace TDUModdingLibrary.fileformats.database.helper
{
    /// <summary>
    /// Helper class ton facilitate color management
    /// </summary>
    public static class ColorsHelper
    {
        #region Constants
        /// <summary>
        /// Reference file name
        /// </summary>
        public const string FILE_COLORS_REF_XML = @"\Colors.xml";

        /// <summary>
        /// XML node for each color
        /// </summary>
        private const string _COLOR_NODE = "color";

        /// <summary>
        /// XML attribute for color code
        /// </summary>
        private const string _CODE_ATTRIBUTE = "code";

        /// <summary>
        /// XML attribute for color label
        /// </summary>
        private const string _LABEL_ATTRIBUTE = "label";

        /// <summary>
        /// XML attribute for color type
        /// </summary>
        private const string _TYPE_ATTRIBUTE = "type";

        /// <summary>
        /// XML attribute value: exterior
        /// </summary>
        private const string _EXTERIOR_VALUE = "exterior";

        /// <summary>
        /// XML attribute value: exterior
        /// </summary>
        private const string _INTERIOR_VALUE = "interior";
        
        /// <summary>
        /// XML attribute value: exterior
        /// </summary>
        private const string _CALIPERS_VALUE = "calipers";

        /// <summary>
        /// XML attribute value: exterior
        /// </summary>
        private const string _MATERIAL_VALUE = "material";

        /// <summary>
        /// Unknown color name
        /// </summary>
        private const string _UNKNOWN_COLOR = "Unknown color";
        #endregion

        #region Members
        /// <summary>
        /// Exterior color index: (color code, color label)
        /// </summary>
        public static Dictionary<string, string> ExteriorReference
        {
            get { return _ExteriorReference; }
        }
        private static readonly Dictionary<string, string> _ExteriorReference = new Dictionary<string, string>();

        /// <summary>
        /// Interior color index: (color code, color label)
        /// </summary>
        public static Dictionary<string, string> InteriorReference
        {
            get { return _InteriorReference; }
        }
        private static readonly Dictionary<string, string> _InteriorReference = new Dictionary<string, string>();
        
        /// <summary>
        /// Calipers color index: (color code, color label)
        /// </summary>
        public static Dictionary<string, string> CalipersReference
        {
            get { return _CalipersReference; }
        }
        private static readonly Dictionary<string, string> _CalipersReference = new Dictionary<string, string>();

        /// <summary>
        /// Materials index: (material code, material label)
        /// </summary>
        public static Dictionary<string, string> MaterialsReference
        {
            get { return _MaterialsReference; }
        }
        private static readonly Dictionary<string, string> _MaterialsReference = new Dictionary<string, string>();


        /// <summary>
        /// Color index: (color code, color id)
        /// </summary>
        public static Dictionary<string, string> IdByCodeReference
        {
            get { return _IdByCodeReference; }
        }
        private static readonly Dictionary<string, string> _IdByCodeReference = new Dictionary<string, string>();
        #endregion

        #region Members
        /// <summary>
        /// Indicates if reference contents are ready
        /// </summary>
        private static bool _IsReady = false;

        /// <summary>
        /// Indicates if id reference contents are ready
        /// </summary>
        private static bool _IsIdReady = false;
        #endregion

        #region Public methods
        /// <summary>
        /// Initializes reference according to specified file
        /// </summary>
        /// <param name="referenceFilePath">Path of XML reference file (just path, not filename)</param>
        public static void InitReference(string referenceFilePath)
        {
            if (string.IsNullOrEmpty(referenceFilePath) || !Directory.Exists(referenceFilePath))
                return;

            // Is init needed ?
            if (_IsReady)
                return;

            // Parsing ref XML file and writing to reference
            XmlDocument doc = new XmlDocument();

            doc.Load(referenceFilePath + FILE_COLORS_REF_XML);

            XmlNode slotsNode = doc.DocumentElement;
            XmlNodeList allColors = slotsNode.SelectNodes(_COLOR_NODE);

            foreach (XmlNode anotherNode in allColors)
            {
                string code = anotherNode.Attributes[_CODE_ATTRIBUTE].Value;
                string label = anotherNode.Attributes[_LABEL_ATTRIBUTE].Value;
                string type = anotherNode.Attributes[_TYPE_ATTRIBUTE].Value;

                // According to type...
                Dictionary<string, string> currentIndex;

                switch(type)
                {
                    case _EXTERIOR_VALUE:
                        currentIndex = _ExteriorReference;
                        break;
                    case _INTERIOR_VALUE:
                        currentIndex = _InteriorReference;
                        break;
                    case _CALIPERS_VALUE:
                        currentIndex = _CalipersReference;
                        break;
                    case _MATERIAL_VALUE:
                        currentIndex = _MaterialsReference;
                        break;
                    default:
                        currentIndex = null;
                        break;
                }

                if (currentIndex != null && !currentIndex.ContainsKey(code))
                    currentIndex.Add(code, label);
            }

            // OK !
            _IsReady = true;
        }

        /// <summary>
        /// Initializes identifier reference according to specified color resource
        /// </summary>
        /// <param name="colorsResource"></param>
        /// <param name="interiorColorsResource"></param>
        public static void InitIdReference(DBResource colorsResource, DBResource interiorColorsResource)
        {
            // Is init needed ?
            if (!_IsIdReady && colorsResource != null && interiorColorsResource != null)
            {
                // Initializes reverse color reference : parsing resource if provided
                foreach (DBResource.Entry anotherEntry in colorsResource.EntryList)
                {
                    if (anotherEntry.isValid && !anotherEntry.isComment)
                    {
                        if (!_IdByCodeReference.ContainsKey(anotherEntry.value))
                            _IdByCodeReference.Add(anotherEntry.value, anotherEntry.id.Id);
                    }
                }

                // Interior
                foreach (DBResource.Entry anotherEntry in interiorColorsResource.EntryList)
                {
                    if (anotherEntry.isValid && !anotherEntry.isComment)
                    {
                        if (!_IdByCodeReference.ContainsKey(anotherEntry.value))
                            _IdByCodeReference.Add(anotherEntry.value, anotherEntry.id.Id);
                    }
                }

                // OK !
                _IsIdReady = true;
            }
        }

        /// <summary>
        /// Returns color name from specified color code
        /// </summary>
        /// <param name="colorCode"></param>
        /// <returns></returns>
        public static string GetColorName(string colorCode)
        {
            string returnedName = _UNKNOWN_COLOR;

            if (!string.IsNullOrEmpty(colorCode))
            {
                if (_ExteriorReference.ContainsKey(colorCode))
                    returnedName = _ExteriorReference[colorCode];
                else if (_InteriorReference.ContainsKey(colorCode))
                    returnedName = _InteriorReference[colorCode];
                else if (_CalipersReference.ContainsKey(colorCode))
                    returnedName = _CalipersReference[colorCode];
                else if (_MaterialsReference.ContainsKey(colorCode))
                    returnedName = _MaterialsReference[colorCode];
            }

            return returnedName;
        }
        #endregion

        #region Private methods
        #endregion
    }
}
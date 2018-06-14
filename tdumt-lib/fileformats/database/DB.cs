using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DjeFramework1.Common.Support.Meta;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.support.constants;
using TDUModdingLibrary.support.encryption;

namespace TDUModdingLibrary.fileformats.database
{
    /// <summary>
    /// Represents an encrypted database file (DB)
    /// </summary>
    public class DB:TduFile
    {
        #region Constants
        /// <summary>
        /// Name of main database file (without extension)
        /// </summary>
        public const string DATABASE_MAIN_NAME = "DB";

        /// <summary>
        /// Pattern for file name (to redefine)
        /// </summary>
        public new static readonly string FILENAME_PATTERN = string.Format(String2.REGEX_PATTERN_EXTENSION, LibraryConstants.EXTENSION_DB_FILE);

        /// <summary>
        /// Comments in file. Don't remove space after last slash !
        /// </summary>
        internal const string _CHAR_COMMENTS = "//";

        /// <summary>
        /// Début de données textuelles
        /// </summary>
        internal const char _CHAR_START_TEXT = '{';

        /// <summary>
        /// Fin de données textuelles
        /// </summary>
        internal const char _CHAR_END_TEXT = '}';

        /// <summary>
        /// Value separator
        /// </summary>
        private const char _CHAR_VALUE_SEPARATOR = ';';

        /// <summary>
        /// BNK database file name pattern
        /// </summary>
        private const string _FORMAT_BNK_NAME = "DB_{0}." + LibraryConstants.EXTENSION_BNK_FILE;

        /// <summary>
        /// Database file name pattern
        /// </summary>
        private const string _FORMAT_DB_NAME = "TDU_{0}.{1}";

        /// <summary>
        /// Format for item count comment (must be ok as game is using it!)
        /// </summary>
        private const string _FORMAT_COMMENT_COUNT = "// items: {0}";
        #endregion

        #region Enums
        /// <summary>
        /// Différentes catégories de la DB
        /// </summary>
        public enum Topic
        {
            None,
            Achievements,
            AfterMarketPacks,
            Bots,
            Brands,
            CarColors,
            CarPacks,
            CarPhysicsData,
            CarRims,
            CarShops,
            Clothes,
            Hair,
            Houses,
            Interior,
            Menus,
            PNJ,
            Rims,
            SubTitles,
            Tutorials
        }
        
        /// <summary>
        /// Différentes cultures pour les fichiers de DB
        /// </summary>
        public enum Culture
        {
            FR = 0, GE = 1, US = 2, KO = 3, CH = 4, JA = 5, IT = 6, SP = 7, Global = -1
        }

        /// <summary>
        /// All types of values
        /// </summary>
        public enum ValueType
        {
            // b
            BitField,
            // f ??
            F,
            // x - Unique id
            PrimaryKey,
            // i - int
            Integer,
            // p - %
            Percentage,
            // r > reference to other table
            Reference,
            // l > reference to other resource
            ReferenceL,
            // TDUMT specific
            Undefined,
            // u > value in current resource
            ValueInResource,
            // h ??
            ValueInResourceH
        }
        #endregion

        #region Structures
        /// <summary>
        /// Represents an entry in this topic
        /// </summary>
        public struct Entry
        {
            /// <summary>
            /// Index of this entry
            /// </summary>
            public int index;

            /// <summary>
            /// All cells in entry
            /// </summary>
            public List<Cell> cells;

            /// <summary>
            /// PK Value
            /// </summary>
            public string primaryKey;
        }

        /// <summary>
        /// Represent a cell for current entry
        /// </summary>
        public struct Cell
        {
            public int index;
            public int entryIndex;
            public string name;
            public ValueType valueType;
            public string optionalRef;
            public string value;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Current database topic
        /// </summary>
        public Topic CurrentTopic
        {
            get { return _CurrentTopic; }
        }
        protected Topic _CurrentTopic = Topic.None;

        /// <summary>
        /// All entries in this file
        /// </summary>
        public List<Entry> Entries
        {
            get { return _Entries; }
        }
        private readonly List<Entry> _Entries;

        /// <summary>
        /// Reference of this database file
        /// </summary>
        public string FileReference
        {
            get { return _FileReference; }
        }
        private string _FileReference;

        /// <summary>
        /// Accelerator to entries by primary key. 
        /// A primary key is either a REF value (in most tables), or a couple of values for reference tables (e.g CarRims).
        /// </summary>
        internal Dictionary<string, Entry> EntriesByPrimaryKey
        {
            get { return _EntriesByPrimaryKey; }
        }
        private readonly Dictionary<string, Entry> _EntriesByPrimaryKey = new Dictionary<string, Entry>();

        /// <summary>
        /// Accelerator to column index by columnname
        /// </summary>
        internal Dictionary<string, int> ColumnIndexByName
        {
            get { return _ColumnIndexByName; }
        }
        private readonly Dictionary<string, int> _ColumnIndexByName = new Dictionary<string, int>();
        
        /// <summary>
        /// Returns current entry count
        /// </summary>
        public int EntryCount
        {
            get { return _Entries.Count; }
        }

        /// <summary>
        /// Returns current column count
        /// </summary>
        public int ColumnCount
        {
            get { return _Structure.Count; }
        }

        /// <summary>
        /// Returns table structure
        /// </summary>
        public List<Cell> Structure
        {
            get { return _Structure; }
        }

        /// <summary>
        /// Reference of all topic per identifier
        /// </summary>
        public static Dictionary<string, Topic> TopicPerTopicId
        {
            get { return _TopicPerTopicId;  }
        }
        private static readonly Dictionary<string, Topic> _TopicPerTopicId;
        #endregion

        #region Members
        /// <summary>
        /// Utility association to enable database cross-seeking
        /// </summary>
        private static readonly Dictionary<string, string> _DBFilesByReference = new Dictionary<string, string>();

        /// <summary>
        /// Topic structure (=column list)
        /// </summary>
        private List<Cell> _Structure;

        /// <summary>
        /// Comments at beginning of file
        /// </summary>
        private string _HeaderComments = "";
        #endregion

        /// <summary>
        /// Type constructor
        /// </summary>
        static DB()
        {
            _TopicPerTopicId = new Dictionary<string, Topic>
                                   {
                                       {"2442784645", Topic.Achievements},
                                       {"2064375635", Topic.AfterMarketPacks},
                                       {"1277864264", Topic.Bots },
                                       {"1209165514", Topic.Brands },
                                       {"954601678", Topic.CarShops},
                                       {"1975083164", Topic.CarPhysicsData},
                                       {"1282586878", Topic.Clothes},
                                       {"1137960528", Topic.Interior},
                                       {"82704264", Topic.PNJ},
                                       {"1270424264", Topic.Rims}
                                   };
        }

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="fileName">File name to load</param>
        internal DB(string fileName)
        {
            // Common TduFile members
            _FileName = fileName;

            _Entries = new List<Entry>();
            _EntriesByPrimaryKey = new Dictionary<string, Entry>();

            _ReadData();
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        internal DB() { }

        #region TduFile implementation
        /// <summary>
        /// Lit les données du fichier. A implémenter si nécessaire
        /// </summary>
        protected override sealed void _ReadData()
        {
            // Decrypting needed
            string tempFolder = File2.SetTemporaryFolder(null, LibraryConstants.FOLDER_TEMP, true);
            FileInfo fi = new FileInfo(_FileName);
            string tempFile = tempFolder + @"\" + fi.Name + ".OK";
            XTEAEncryption.Decrypt(_FileName, tempFile);

            // Parsing clear file
            using (TextReader reader = new StreamReader(new FileStream(tempFile, FileMode.Open, FileAccess.Read)))
            {
                // Clearing all data
                _Entries.Clear();
                _EntriesByPrimaryKey.Clear();

                // First lines = header comments
                StringBuilder sbHeader = new StringBuilder();
                string currentLine = reader.ReadLine();

                while (currentLine != null && currentLine.StartsWith(_CHAR_COMMENTS))
                {
                    sbHeader.AppendLine(currentLine);
                    currentLine = reader.ReadLine();
                }

                _HeaderComments = sbHeader.ToString();

                // Column list
                _Structure = new List<Cell>();
                int count = 0;
                int unicityCounter = 1;

                while (currentLine != null && currentLine[0] == _CHAR_START_TEXT)
                {
                    // Parsing column description
                    int nameEndPosition = currentLine.IndexOf(_CHAR_END_TEXT);
                    string name = currentLine.Substring(1, nameEndPosition - 1);

                    if (count++ == 0)
                    {
                        // First column is just file reference
                        _CurrentTopic = _GetTopicByName(name);
                        _FileReference = currentLine.Substring(nameEndPosition + 2);
                    }
                    else
                    {
                        Cell currentCell = new Cell();

                        // Regular column description
                        string type = currentLine.Substring(nameEndPosition + 2, 1);

                        currentCell.name = name;
                        currentCell.valueType = _GetValueTypeBySymbol(type);
                        currentCell.index = count - 2;

                        if (currentCell.valueType == ValueType.Reference || currentCell.valueType == ValueType.ReferenceL)
                        {
                            string reference = currentLine.Substring(nameEndPosition + 4);

                            currentCell.optionalRef = reference;
                        }

                        _Structure.Add(currentCell);

                        // Index update
                        // BUGFIX: on TDU_PNJ, 6 columns have the same name
                        // Column name is suffixed by a counter
                        if (_ColumnIndexByName.ContainsKey(currentCell.name))
                        {
                            currentCell.name = currentCell.name + "_" + unicityCounter;
                            unicityCounter++;
                        }

                        _ColumnIndexByName.Add(currentCell.name, currentCell.index);
                    }

                    currentLine = reader.ReadLine();
                }

                // Comment for line count
                string countComment = currentLine;
                currentLine = reader.ReadLine();

                // Values
                int entryCount = 0;
                int maxEntryCount = _GetMaxEntryCount(countComment);
                string separator = new string(_CHAR_VALUE_SEPARATOR,1);

                // BUGFIX: does not take entries when count is higher than expected
                while (currentLine != null && currentLine.Contains(separator))
                {
                    if (entryCount > maxEntryCount)
                        break;

                    // Getting all values by splitting between ;
                    string[] allValues = currentLine.Split(_CHAR_VALUE_SEPARATOR);

                    // BUG_65: ensure current line is valid. If last separator is missing, entry is also considered as OK.
                    if (allValues.Length < _Structure.Count || allValues.Length > _Structure.Count + 1)
                    {
                        Log.Info("Current DB entry is invalid (" + _FileName + "):\r\n" + currentLine);

                        break;
                    }

                    Entry currentEntry = new Entry();
                    string primaryKey = null;

                    currentEntry.index = entryCount;
                    currentEntry.cells = new List<Cell>();

                    for (int i = 0; i < allValues.Length - 1; i++)
                    {
                        Cell currentCell = _Structure[i];

                        currentCell.value = allValues[i];
                        currentCell.index = i;
                        currentCell.entryIndex = entryCount;
                        currentEntry.cells.Add(currentCell);

                        if (currentCell.valueType == ValueType.PrimaryKey)
                            primaryKey = currentCell.value;
                    }

                    // If no REF column found, primary key is automatically built with contents of first 2 columns
                    if (primaryKey == null && currentEntry.cells.Count >= 2)
                        primaryKey = string.Format(DatabaseHelper.FORMAT_COMPLEX_PK,
                                                   currentEntry.cells[0].value,
                                                   currentEntry.cells[1].value);

                    currentEntry.primaryKey = primaryKey;

                    _Entries.Add(currentEntry);

                    // Debug only - very slow !
                    //Log.Info("primary key = " + primaryKey);

                    // TODO potential problem with Elise duplicated rims 1297992029
                    // Duplicate risk with TDU_Achievements
                    if (primaryKey != null && !_EntriesByPrimaryKey.ContainsKey(primaryKey))
                        _EntriesByPrimaryKey.Add(primaryKey, currentEntry);

                    currentLine = reader.ReadLine();
                    entryCount++;
                }
            }

            // Adding current reference to global association
            if (_FileReference != null && !_DBFilesByReference.ContainsKey(_FileReference))
                _DBFilesByReference.Add(_FileReference, _CurrentTopic.ToString());

            // EVO_65: Properties
            Property.ComputeValueDelegate encryptedDelegate = (() => "Yes");

            Properties.Add(new Property("Encrypted ?", "Database", encryptedDelegate));
        }

        /// <summary>
        /// Saves the current file to disk. To implement if necessary.
        /// </summary>
        public override void Save()
        {
            // Preparing destination
            string tempFolder = File2.SetTemporaryFolder(null, LibraryConstants.FOLDER_TEMP, true);
            FileInfo fi = new FileInfo(_FileName);
            string tempFile = tempFolder + @"\" + fi.Name + ".OK";

            // Saving data
            using (TextWriter writer = new StreamWriter(new FileStream(tempFile, FileMode.Create, FileAccess.Write)))
            {
                // Header comments
                writer.Write(_HeaderComments);

                // Column list
                StringBuilder sbFileRef = new StringBuilder();

                sbFileRef.Append(_CHAR_START_TEXT);
                sbFileRef.Append("TDU_");
                sbFileRef.Append(_CurrentTopic.ToString());
                sbFileRef.Append(_CHAR_END_TEXT);
                sbFileRef.Append(' ');
                sbFileRef.Append(_FileReference);
                writer.WriteLine(sbFileRef.ToString());

                foreach (Cell anotherColumn in _Structure)
                {
                    StringBuilder sbColumn = new StringBuilder();

                    sbColumn.Append(_CHAR_START_TEXT);
                    sbColumn.Append(anotherColumn.name);
                    sbColumn.Append(_CHAR_END_TEXT);
                    sbColumn.Append(' ');
                    sbColumn.Append(_GetSymbolByValueType(anotherColumn.valueType));

                    if (anotherColumn.valueType == ValueType.Reference || anotherColumn.valueType == ValueType.ReferenceL)
                    {
                        sbColumn.Append(' ');
                        sbColumn.Append(anotherColumn.optionalRef);
                    }
                    writer.WriteLine(sbColumn.ToString());
                }

                // Count comment
                string countComment = string.Format(_FORMAT_COMMENT_COUNT, _Entries.Count);

                writer.WriteLine(countComment);

                // Entries
                foreach (Entry anotherEntry in _Entries)
                {
                    StringBuilder sbEntry = new StringBuilder();

                    foreach (Cell anotherCell in anotherEntry.cells)
                    {
                        sbEntry.Append(anotherCell.value);
                        sbEntry.Append(_CHAR_VALUE_SEPARATOR);
                    }

                    writer.WriteLine(sbEntry.ToString());
                }
            }

            // Encrypting needed
            XTEAEncryption.Encrypt(tempFile, _FileName);
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Return max item count according to comment found in file
        /// </summary>
        /// <param name="countComment"></param>
        /// <returns></returns>
        private static int _GetMaxEntryCount(string countComment)
        {
            int returnCount = 0;

            if (!string.IsNullOrEmpty(countComment))
            {
                string countPart = countComment.Substring(9);

                returnCount = int.Parse(countPart);
            }

            return returnCount;
        }

        /// <summary>
        /// Gets right value type according to read letter
        /// </summary>
        /// <param name="symbol">Letter symbol</param>
        /// <returns></returns>
        private static ValueType _GetValueTypeBySymbol(string symbol)
        {
            switch(symbol)
            {
                case "b":
                    return ValueType.BitField;
                case "f":
                    return ValueType.F;
                case "h":
                    return ValueType.ValueInResourceH;
                case "i":
                    return ValueType.Integer;
                case "l":
                    return ValueType.ReferenceL;
                case "p":
                    return ValueType.Percentage;
                case "r":
                    return ValueType.Reference;
                case "u":
                    return ValueType.ValueInResource;
                case "x":
                    return ValueType.PrimaryKey;
                default:
                    return ValueType.Undefined;
            }
        }

        /// <summary>
        /// Gets right symbol according to specified value type
        /// </summary>
        /// <param name="valueType">Value type to get a symbol</param>
        /// <returns>Null if specified value type is not supported</returns>
        private static string _GetSymbolByValueType(ValueType valueType)
        {
            switch(valueType)
            {
                case ValueType.BitField:
                    return "b";
                case ValueType.F:
                    return "f";
                case ValueType.ValueInResourceH:
                    return "h";
                case ValueType.Integer:
                    return "i";
                case ValueType.Percentage:
                    return "p";
                case ValueType.Reference:
                    return "r";
                case ValueType.ReferenceL:
                    return "l";
                case ValueType.ValueInResource:
                    return "u";
                case ValueType.PrimaryKey:
                    return "x";
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets right database topic according to specified name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static Topic _GetTopicByName(string name)
        {
            Topic resultTopic = Topic.None;

            if (string.IsNullOrEmpty(name))
                return resultTopic;

            // Browsing all topic names
            string[] allTopics = Enum.GetNames(typeof(Topic));

            foreach (string anotherTopic in allTopics)
            {
                if (name.Contains(anotherTopic))
                {
                    resultTopic = (Topic)Enum.Parse(typeof(Topic), anotherTopic);
                    break;
                }
            }

            return resultTopic;
        }
        
        /// <summary>
        /// Updates entry-by-pk cache. If pkValue does not exist, it is added.
        /// If isDeletion set to true, entry of specified pkValue is removed.
        /// </summary>
        /// <param name="pkValue"></param>
        /// <param name="newEntry"></param>
        /// <param name="isDeletion"></param>
        internal void _UpdateEntryCache(string pkValue, Entry newEntry, bool isDeletion)
        {
            bool isPkExist = _EntriesByPrimaryKey.ContainsKey(pkValue);

            if (isDeletion && isPkExist)
                // DEL mode
                _EntriesByPrimaryKey.Remove(pkValue);
            else if (isPkExist)
                // UPD mode
                _EntriesByPrimaryKey[pkValue] = newEntry;
            else 
                // ADD
                _EntriesByPrimaryKey.Add(pkValue, newEntry);           
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Returns BNK file name for specified culture
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string GetBNKFileName(Culture culture)
        {
            string returnedName = (culture == Culture.Global
                                       ? string.Concat("DB.", LibraryConstants.EXTENSION_BNK_FILE)
                                       : string.Format(_FORMAT_BNK_NAME, culture));

            return returnedName;
        }
    
        /// <summary>
        /// Returns packed database file name for specified culture and topic
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="topic"></param>
        /// <returns></returns>
        public static string GetFileName(Culture culture, Topic topic)
        {
            string extension;

            if (culture == Culture.Global)
                extension = LibraryConstants.EXTENSION_DB_FILE.ToLower();
            else
                extension = culture.ToString().ToLower();

            return string.Format(_FORMAT_DB_NAME, topic, extension);
        }   
        #endregion

        #region Internal methods
        /// <summary>
        /// Creates an empty cell according to specified column index
        /// </summary>
        /// <param name="index">Column index</param>
        /// <returns></returns>
        internal Cell _CreateEmptyCell(int index)
        {
            Cell newCell = new Cell();

            if (index >= 0 && index < _Structure.Count)
                newCell = _Structure[index];

            return newCell;
        }
        #endregion
    }
}
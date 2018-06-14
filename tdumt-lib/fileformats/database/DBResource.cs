using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using DjeFramework1.Common.Support.Meta;
using DjeFramework1.Common.Types;
using DjeFramework1.Util.BasicStructures;
using TDUModdingLibrary.fileformats.database.util;

namespace TDUModdingLibrary.fileformats.database
{
    /// <summary>
    /// Represents an unencrypted database file (DB resource)
    /// </summary>
    public class DBResource:TduFile
    {
        #region Enumérations et structures
        /// <summary>
        /// Différents types d'entrées
        /// </summary>
        public enum EntryType
        {
            Comment,
            Regular,
            Invalid
        }

        /// <summary>
        /// Représente une entrée dans le fichier
        /// </summary>
        public struct Entry
        {
            /// <summary>
            /// Numéro de ligne dans le fichier
            /// </summary>
            public int index;

            /// <summary>
            /// Identifiant
            /// </summary>
            public ResourceIdentifier id;

            /// <summary>
            /// Valeur
            /// </summary>
            public string value;

            /// <summary>
            /// L'entrée est-elle valide ?
            /// </summary>
            public bool isValid;

            /// <summary>
            /// Est-ce un commentaire ?
            /// </summary>
            public bool isComment;
        }
        #endregion

        #region Properties
        /// <summary>
        /// List of available tongues and cultures
        /// </summary>
        public static string[] LanguageList
        {
            get { return _LanguageList; }
        }
        private static readonly string[] _LanguageList = new[]
        {
            DB.Culture.CH + " - Chinese",
            DB.Culture.FR + " - French",
            DB.Culture.GE + " - German",
            DB.Culture.IT + " - Italian",
            DB.Culture.JA + " - Japanese",
            DB.Culture.KO + " - Korean",
            DB.Culture.SP + " - Spanish",
            DB.Culture.US + " - English"
        };

        /// <summary>
        /// Culture actuelle du fichier de DB
        /// </summary>
        protected DB.Culture _CurrentCulture = DB.Culture.Global;
        public DB.Culture CurrentCulture
        {
            get { return _CurrentCulture; }
        }

        /// <summary>
        /// Current database topic
        /// </summary>
        public DB.Topic CurrentTopic
        {
            get { return _CurrentTopic;  }
        }
        protected DB.Topic _CurrentTopic = DB.Topic.None;

        /// <summary>
        /// Liste d'entrées trouvées
        /// </summary>
        private readonly Collection<Entry> _Entries = new Collection<Entry>();
        public Collection<Entry> EntryList
        {
            get { return _Entries; }
        }
        #endregion

        #region Attributs
        /// <summary>
        /// Accelerator: index of entries by identifier (comments and invalid ones are not referenced)
        /// </summary>
        private readonly Dictionary<ResourceIdentifier, Entry> entriesById = new Dictionary<ResourceIdentifier, Entry>();
        #endregion

        #region Constants
        /// <summary>
        /// Pattern for file name (to redefine)
        /// </summary>
        public new static readonly string FILENAME_PATTERN = string.Format(String2.REGEX_PATTERN_EXTENSION, "(FR|CH|GE|IT|JA|KO|SP|US)");

        /// <summary>
        /// Extension de fichier DB pour la France
        /// </summary>
        public const string EXTENSION_FR = "FR";

        /// <summary>
        /// Extension de fichier DB pour la Chine
        /// </summary>
        public const string EXTENSION_CH = "CH";

        /// <summary>
        /// Extension de fichier DB pour l'Allemagne
        /// </summary>
        public const string EXTENSION_GE = "GE";

        /// <summary>
        /// Extension de fichier DB pour l'Italie
        /// </summary>
        public const string EXTENSION_IT = "IT";

        /// <summary>
        /// Extension de fichier DB pour le Japon
        /// </summary>
        public const string EXTENSION_JA = "JA";

        /// <summary>
        /// Extension de fichier DB pour l'Allemagne
        /// </summary>
        public const string EXTENSION_KO = "KO";

        /// <summary>
        /// Extension de fichier DB pour l'Allemagne
        /// </summary>
        public const string EXTENSION_SP = "SP";

        /// <summary>
        /// Extension de fichier DB pour l'Allemagne
        /// </summary>
        public const string EXTENSION_US = "US";

        /// <summary>
        /// Regular entry pattern
        /// </summary>
        private const string _FORMAT_REGULAR_ENTRY = "{0}{1}{2} {3}";

        /// <summary>
        /// Comment entry pattern
        /// </summary>
        private const string _FORMAT_COMMENT_ENTRY = "{0} {1}";

        /// <summary>
        /// Regex pattern for comment entry
        /// //\s*(.*)
        /// </summary>
        private static readonly string _PATTERN_COMMENT_ENTRY = string.Concat(DB._CHAR_COMMENTS, @"\s*(?<comment>.*)");

        /// <summary>
        /// Regex pattern for regular entry
        /// ^{.*(\n?.*)*)} (\d+)}$
        /// </summary>
        private static readonly string _PATTERN_REGULAR_ENTRY =
            string.Concat("^", DB._CHAR_START_TEXT, @"(?<value>.*(\n?.*)*)", DB._CHAR_END_TEXT, @" (?<id>\d+)$");

        /// <summary>
        /// Suffix for engine type resource identifier
        /// </summary>
        public const string SUFFIX_PHYSICS_ENGINE_TYPE = "709";
        
        /// <summary>
        /// Suffix for engine type resource identifier
        /// </summary>
        public const string SUFFIX_PHYSICS_TIRES = "338424";

        /// <summary>
        /// Suffix for brake characteristics resource identifier
        /// </summary>
        public const string SUFFIX_PHYSICS_BRAKES_CHAR = "5936";
        #endregion

        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="dbFileName">Nom du fichier de bases de données à charger</param>
        internal DBResource(string dbFileName)
        {
            _FileName = dbFileName;
            _CurrentCulture = (DB.Culture)Enum.Parse(typeof(DB.Culture), File2.GetExtensionFromFilename(_FileName).ToUpper());

            // Lecture
            _ReadData();
        }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal DBResource() {}

        #region TDUFile implementation
        /// <summary>
        /// Initialise les infos avec le contenu du fichier (redéfinition)
        /// </summary>
        protected override void _ReadData()
        {
            using (TextReader reader = new StreamReader(new FileStream(_FileName, FileMode.Open, FileAccess.Read)))
            {
                // Clearing all data
                _Entries.Clear();
                entriesById.Clear();

                // Browsing all lines
                string contents = reader.ReadToEnd();

                string[] endOfEntrySeparator = new string[] {"\r\n"};
                int entryCount = 0;
                foreach(string currentLine in contents.Split(endOfEntrySeparator, StringSplitOptions.RemoveEmptyEntries))
                {
                    Entry entry = _GenerateEntryFromLine(currentLine);

                    if (entry.isValid)
                    {
                        // Searching topic in first comment entry
                        if (_CurrentTopic == DB.Topic.None && entry.isComment)
                            _CurrentTopic = _SearchForCurrentTopic(entry);

                        entry.index = ++entryCount;
                        _Entries.Add(entry);
                        
                        // Accelerator update
                        if (entry.id != null && !entriesById.ContainsKey(entry.id))
                            entriesById.Add(entry.id, entry);
                    }
                }
            }

            // EVO_65: Properties
            Property.ComputeValueDelegate encryptedDelegate = (() => "No");
            Property.ComputeValueDelegate entryCountDelegate = (() => _Entries.Count.ToString());

            Properties.Add(new Property("Encrypted ?", "DB Resources", encryptedDelegate));
            Properties.Add(new Property("Entry count", "DB Resources", entryCountDelegate));
        }

        public override void Save()
        {
            using (TextWriter writer = new StreamWriter(new FileStream(_FileName, FileMode.Create, FileAccess.Write), Encoding.Unicode))
            {
                // Ecriture par ligne
                foreach (Entry anotherEntry in _Entries)
                {
                    string lineToWrite;

                    // Selon le type d'entrée...
                    if (!anotherEntry.isValid)
                        lineToWrite = anotherEntry.value;
                    else if (anotherEntry.isComment)
                        lineToWrite = string.Format(_FORMAT_COMMENT_ENTRY, DB._CHAR_COMMENTS, anotherEntry.value);
                    else
                        lineToWrite = string.Format(_FORMAT_REGULAR_ENTRY, DB._CHAR_START_TEXT, anotherEntry.value, DB._CHAR_END_TEXT, anotherEntry.id.Id);

                    writer.WriteLine(lineToWrite);
                }
            }
        }
        #endregion

        #region Méthodes publiques
        /// <summary>
        /// Renvoie l'entrée à la ligne indiquée
        /// </summary>
        /// <param name="lineNumber">Ligne concernée</param>
        /// <returns></returns>
        public Entry GetEntryFromLine(int lineNumber)
        {
            foreach (Entry anotherEntry in _Entries)
            {
                if (anotherEntry.index == lineNumber)
                    return anotherEntry;
            }

            return new Entry();
        }

        /// <summary>
        /// Returns entry with specified id
        /// </summary>
        /// <param name="id">Id to search</param>
        /// <returns>Correpsonding entry or a blank entry if it does not exist</returns>
        public Entry GetEntryFromId(string id)
        {
            Entry returnedEntry =  new Entry();
            ResourceIdentifier identifierToSearch = new ResourceIdentifier(id, CurrentTopic);

            if (entriesById.ContainsKey(identifierToSearch))
                returnedEntry = entriesById[identifierToSearch];

            return returnedEntry;
        }

        /// <summary>
        /// Returns all entries with specified value
        /// </summary>
        /// <param name="val">Value to search</param>
        /// <param name="comparisonMode"></param>
        /// <returns></returns>
        public Collection<Entry> GetEntriesFromValue(string val, StringComparison comparisonMode)
        {
            Collection<Entry> returnedEntries = new Collection<Entry>();

            if (val != null)
            {
                foreach (Entry anotherEntry in _Entries)
                {
                    if (anotherEntry.isValid
                        && anotherEntry.value != null
                        && string.Compare(val, anotherEntry.value, comparisonMode) == 0)
                            returnedEntries.Add(anotherEntry);
                }
            }

            return returnedEntries;
        }

        /// <summary>
        /// Met à jour l'entrée correspondante
        /// </summary>
        /// <param name="editedEntry">Entrée modifiée</param>
        /// <returns>entry after update. Contents may be modified for compatibility reasons</returns>
        public Entry UpdateEntry(Entry editedEntry)
        {
            Entry newEntry = new Entry();

            // Parcours des entrées
            for (int i = 0 ; i < _Entries.Count ; i++)
            {
                newEntry = _Entries[i];

                if (newEntry.index == editedEntry.index)
                {
                    ResourceIdentifier oldId = newEntry.id;

                    // Process value to replace forbidden characters
                    Couple<char> couple1 = new Couple<char>(DB._CHAR_START_TEXT, '(');
                    Couple<char> couple2 = new Couple<char>(DB._CHAR_END_TEXT, ')');

                    newEntry.id = editedEntry.id.Clone() as ResourceIdentifier;

                    if (newEntry.id != null)
                    {
                        newEntry.value = String2.ReplaceChars(editedEntry.value, couple1, couple2);
                        _Entries[i] = newEntry;

                        // Accelerator update !
                        entriesById.Remove(oldId);
                        entriesById.Add(newEntry.id, newEntry);
                    }

                    break;
                }
            }

            return newEntry;
        }

        /// <summary>
        /// Creates a new entry. It must be valid and its identifier must not exist.
        /// Its index must be specified.
        /// </summary>
        /// <param name="entryToAdd">Entry to insert</param>
        public void InsertEntry(Entry entryToAdd)
        {
            // Controls
            if (!entryToAdd.isValid || entryToAdd.id == null)
                return;

            if (GetEntryFromId(entryToAdd.id.Id).isValid)
                throw new Exception("Unable to add this entry. An entry with id '" + entryToAdd.id.Id + "' already exists.");

            // Inserting new entry
            _Entries.Insert(entryToAdd.index-1, entryToAdd);

            // Accelerator update
            entriesById.Add(entryToAdd.id, entryToAdd);

            // Adding 1 to each following index + accelerator update
            for (int i = entryToAdd.index; i < _Entries.Count; i++)
            {
                Entry currentEntry = _Entries[i];

                currentEntry.index += 1;
                _Entries[i] = currentEntry;

                if (currentEntry.id != null)
                {
                    entriesById.Remove(currentEntry.id);
                    entriesById.Add(currentEntry.id, currentEntry);
                }
            }
        }

        /// <summary>
        /// Deletes specified entry
        /// </summary>
        /// <param name="entryToDelete">Entry to delete</param>
        public void DeleteEntry(Entry entryToDelete)
        {
            // Controls
            if (!entryToDelete.isValid)
                return;

            _Entries.Remove(entryToDelete);

            // Accelerator update
            entriesById.Remove(entryToDelete.id);

            // Removing 1 to each following index + accelerator update
            for (int i = entryToDelete.index - 1; i < _Entries.Count; i++)
            {
                Entry currentEntry = _Entries[i];

                currentEntry.index -= 1;
                _Entries[i] = currentEntry;

                if (currentEntry.id != null)
                {
                    entriesById.Remove(currentEntry.id);
                    entriesById.Add(currentEntry.id, currentEntry);
                }
            }
        }
        #endregion

        #region Méthodes privées
        /// <summary>
        /// Retrieves the db file topic by watching given comment entry
        /// </summary>
        /// <param name="entry">Comment entry to analyze</param>
        /// <returns>Current topic for this DB dile, Topic.None if not found</returns>
        private static DB.Topic _SearchForCurrentTopic(Entry entry)
        {
            DB.Topic resultTopic = DB.Topic.None;

            if (entry.isComment && entry.value != null)
                resultTopic = DB._GetTopicByName(entry.value);
           
            return resultTopic;
        }

        /// <summary>
        /// Génère l'entrée correspondant à la ligne
        /// </summary>
        /// <param name="currentLine">Données de la ligne à analyser</param>
        /// <returns>L'entrée correspondante</returns>
        private Entry _GenerateEntryFromLine(string currentLine)
        {
            Entry entry = new Entry();

            // Comment test
            Regex re = new Regex(_PATTERN_COMMENT_ENTRY);
            MatchCollection matches = re.Matches(currentLine);

            if (matches.Count == 1)
            {
                // Comment
                entry.value = matches[0].Groups["comment"].Value;
                entry.id = null;
                entry.isComment = true;
                entry.isValid = true;
            }
            else
            {
                // Regular - other
                re = new Regex(_PATTERN_REGULAR_ENTRY);
                matches = re.Matches(currentLine);

                if (matches.Count == 1)
                {
                    try
                    {
                        entry.value = matches[0].Groups["value"].Value;
                        entry.id = new ResourceIdentifier(matches[0].Groups["id"].Value, _CurrentTopic);
                        entry.isValid = true;
                    }
                    catch (Exception)
                    {
                        entry.isValid = false;
                    }
                }
            }

            return entry;
        }
        #endregion
    }
}
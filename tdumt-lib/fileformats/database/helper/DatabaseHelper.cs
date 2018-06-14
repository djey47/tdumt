using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DjeFramework1.Common.Calculations;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Util.BasicStructures;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.fileformats.database.util;
using TDUModdingLibrary.support;
using TDUModdingLibrary.support.constants;
using TDUModdingLibrary.support.editing;

namespace TDUModdingLibrary.fileformats.database.helper
{
    /// <summary>
    /// Utility class to allow whole database access, including encrypted and resource files
    /// </summary>
    public class DatabaseHelper
    {
        #region Constants
        /// <summary>
        /// Symbol used to separate fields in string entry values
        /// </summary>
        private const char _SYMBOL_FIELD_SEPARATOR = '\t';

        /// <summary>
        /// Format for complex primary key (2 values)
        /// </summary>
        internal readonly static string FORMAT_COMPLEX_PK = "{0}" + Tools.SYMBOL_VALUE_SEPARATOR3 + "{1}";
        #endregion

        /// <summary>
        /// Loads the whole specified topic in current database for read-only mode
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="culture"></param>
        /// <returns>Array of database loaded TduFile. Index 0 is the main (encrypted) file, index 1 is the resource one</returns>
        public static TduFile[] LoadTopicForReadOnly(DB.Topic topic, DB.Culture culture)
        {
            TduFile[] returnedFiles = new TduFile[2];

            // Loading BNKs
            string databaseFolder = LibraryConstants.GetSpecialFolder(LibraryConstants.TduSpecialFolder.Database);
            string mainBnkFile = string.Concat(databaseFolder, DB.GetBNKFileName(DB.Culture.Global));
            string resourceBnkFile = string.Concat(databaseFolder, DB.GetBNKFileName(culture));
            BNK mainBnk = TduFile.GetFile(mainBnkFile) as BNK;
            BNK resourceBnk = TduFile.GetFile(resourceBnkFile) as BNK;

            // Getting read-only files
            if (mainBnk != null && resourceBnk != null)
            {
                string dbPackedFileName = DB.GetFileName(DB.Culture.Global, topic);
                string fileName =
                    EditHelper.Instance.PrepareFile(mainBnk, mainBnk.GetPackedFilesPaths(dbPackedFileName)[0]);
                string dbrPackedFileName = DB.GetFileName(culture, topic);
                string resourceFileName =
                    EditHelper.Instance.PrepareFile(resourceBnk, resourceBnk.GetPackedFilesPaths(dbrPackedFileName)[0]);

                // Loading these files
                DB main = TduFile.GetFile(fileName) as DB;
                DBResource resource = TduFile.GetFile(resourceFileName) as DBResource;

                if (main == null || !main.Exists)
                    throw new Exception(topic + " main database failure.");
                if (resource == null || !resource.Exists)
                    throw new Exception(string.Concat(topic, "-", culture, " resource database failure."));

                // Filling array
                returnedFiles[0] = main;
                returnedFiles[1] = resource;
            }

            return returnedFiles;
        }

        /// <summary>
        /// Loads the whole specified topic for edit mode and returns corresponding TduFiles and EditTasks
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="culture"></param>
        /// <param name="bnkFile"></param>
        /// <param name="rBnkFile"></param>
        /// <param name="returnedTasks"></param>
        /// <returns></returns>
        public static TduFile[] LoadTopicForEdit(DB.Topic topic, DB.Culture culture, BNK bnkFile, BNK rBnkFile, out EditHelper.Task[] returnedTasks)
        {
            TduFile[] returnedFiles = new TduFile[2];

            returnedTasks = new EditHelper.Task[2];

            if (bnkFile != null && bnkFile.Exists)
            {
                // Getting files
                string dbFilePath = bnkFile.GetPackedFilesPaths(DB.GetFileName(DB.Culture.Global, topic))[0];

                returnedTasks[0] = EditHelper.Instance.AddTask(bnkFile, dbFilePath, true);

                if (culture != DB.Culture.Global
                    && rBnkFile != null && rBnkFile.Exists)
                {
                    string dbrFilePath = rBnkFile.GetPackedFilesPaths(DB.GetFileName(culture, topic))[0];

                    returnedTasks[1] = EditHelper.Instance.AddTask(rBnkFile, dbrFilePath, true);
                }

                // Loading these files
                DB main = TduFile.GetFile(returnedTasks[0].extractedFile) as DB;

                if (main == null || !main.Exists)
                    throw new Exception(topic + " main database loading failure.");
                returnedFiles[0] = main;

                // Resource (optional)
                if (returnedTasks[1].isValid)
                {
                    DBResource resource = TduFile.GetFile(returnedTasks[1].extractedFile) as DBResource;

                    if (resource == null || !resource.Exists)
                        throw new Exception(string.Concat(topic, "-", culture, " resource database loading failure."));
                    returnedFiles[1] = resource;
                }
            }

            return returnedFiles;
        }
        
        /// <summary>
        /// Loads the whole database in specified location and returns edit tasks
        /// </summary>
        /// <param name="databasePath"></param>
        /// <param name="culture"></param>
        /// <param name="isReadOnly"></param>
        /// <param name="returnedTasks"></param>
        /// <returns></returns>
        public static Dictionary<DB.Topic, TduFile[]> LoadDatabase(string databasePath, DB.Culture culture, bool isReadOnly, out Dictionary<DB.Topic, EditHelper.Task[]> returnedTasks)
        {
            Dictionary<DB.Topic, TduFile[]> returnedData = new Dictionary<DB.Topic, TduFile[]>();
            
            returnedTasks = new Dictionary<DB.Topic, EditHelper.Task[]>();

            if (!string.IsNullOrEmpty(databasePath))
            {
                if (Directory.Exists(databasePath))
                {
                    // Opening main BNK
                    string dbBnkFile = databasePath + @"\" + DB.GetBNKFileName(DB.Culture.Global);
                    BNK dbBnk = TduFile.GetFile(dbBnkFile) as BNK;

                    if (dbBnk == null)
                        throw new Exception("Unable to load BNK: " + dbBnkFile);

                    // Opening resource BNK
                    string resBnkFile = databasePath + @"\" + DB.GetBNKFileName(culture);
                    BNK resBnk = TduFile.GetFile(resBnkFile) as BNK;

                    if (resBnk == null)
                        throw new Exception("Unable to load BNK: " + resBnk);

                    // Loading topics and resources
                    foreach(DB.Topic anotherTopic in Enum.GetValues(typeof(DB.Topic)))
                    {
                        if (anotherTopic != DB.Topic.None)
                        {
                            EditHelper.Task[] loadedTasks;
                            TduFile[] loadedFiles = LoadTopicForEdit(anotherTopic, culture, dbBnk, resBnk,
                                                                     out loadedTasks);

                            returnedData.Add(anotherTopic, loadedFiles);

                            if (!isReadOnly)
                                returnedTasks.Add(anotherTopic, loadedTasks);
                        }
                    }
                }
                else
                    throw new Exception("Specified database path doesn't exist: " + databasePath);
            }

            return returnedData;
        }

        #region Public query methods
        /// <summary>
        /// Indicates if specified table contains a particular primary key.
        /// SQL-base: SELECT COUNT(*) FROM [topic] WHERE (primary key] = [pkValue];
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="pkValue"></param>
        /// <returns></returns>
        public static bool ContainsPrimaryKey(DB topic, string pkValue)
        {
            List<DB.Entry> entries =
                SelectAllCellsFromTopicWherePrimaryKey(topic, pkValue);

            return (entries.Count > 0);
        }

        /// <summary>
        /// Basic request from primary key (quick)
        /// SQL equivalent: SELECT [colName] FROM [topic] WHERE (primary key) IN ([pkValues]);
        /// </summary>
        /// <param name="colName">Column name</param>
        /// <param name="topic">Database topic</param>
        /// <param name="pkValues">Array of primary key values to search</param>
        public static List<DB.Cell> SelectCellsFromTopicWherePrimaryKey(string colName, DB topic, params string[] pkValues)
        {
            List<DB.Cell> result = new List<DB.Cell>();

            if (colName != null && pkValues != null)
            {
                // Searching for specified primary key values
                foreach (string anotherPk in pkValues)
                {
                    if (topic.EntriesByPrimaryKey.ContainsKey(anotherPk))
                    {
                        DB.Entry currentEntry = topic.EntriesByPrimaryKey[anotherPk];

                        // Searching for value in current entry
                        DB.Cell resultCell = GetCellFromEntry(topic, currentEntry, colName);

                        result.Add(resultCell);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Basic request from specified conditions (slower)
        /// SQL equivalent: SELECT [colName] FROM [topic] WHERE [conds[0].firstValue] = '[conds[0].secondValue]' AND [conds[1].firstValue = '[conds[1].secondValue]' ...;
        /// </summary>
        /// <param name="colName">Column name</param>
        /// <param name="topic">Database topic</param>
        /// <param name="conds">Array of (column name,value) conditions</param>
        /// <returns></returns>
        public static List<DB.Cell> SelectCellsFromTopicWhereCellValues(string colName, DB topic, params Couple<string>[] conds)
        {
            List<DB.Cell> result = new List<DB.Cell>();

            if (string.IsNullOrEmpty(colName) || topic == null || conds == null || conds.Length == 0)
                return result;

            // Searching for specified value
            foreach (DB.Entry anotherEntry in topic.Entries)
            {
                bool isValid = false;
                DB.Cell validCell = new DB.Cell();

                // Verifying conditions
                foreach (Couple<string> cond in conds)
                {
                    DB.Cell searchedCell = GetCellFromEntry(topic, anotherEntry, cond.FirstValue);

                    if (searchedCell.value.Equals(cond.SecondValue))
                    {
                        isValid = true;
                        validCell = GetCellFromEntry(topic, anotherEntry, colName);
                    }
                    else
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                    result.Add(validCell);              
            }

            return result;
        }   

        /// <summary>
        /// Basic request to get all cells from specified column
        /// SQL equivalent: SELECT [colNames[0],colNames[1],...] FROM [topic];
        /// </summary>
        /// <param name="colNames">Column names</param>
        /// <param name="topic">Database topic</param>
        /// <returns></returns>
        public static List<DB.Cell[]> SelectCellsFromTopic(DB topic, params string[] colNames)
        {
            List<DB.Cell[]> result = new List<DB.Cell[]>();

            if (colNames == null || colNames.Length == 0 || topic == null)
                return result;

            // Searching for specified columns
            foreach (DB.Entry anotherEntry in topic.Entries)
            {
                DB.Cell[] entryCells = new DB.Cell[colNames.Length];
                int i = 0;

                foreach (string colName in colNames)
                    entryCells[i++] = GetCellFromEntry(topic, anotherEntry, colName);

                result.Add(entryCells);
            }

            return result;
        }

        /// <summary>
        /// Technical request (fast)
        /// No SQL equivalent known.
        /// </summary>
        /// <param name="colName"></param>
        /// <param name="topic"></param>
        /// <param name="entryIds"></param>
        /// <returns></returns>
        public static List<DB.Cell> SelectCellsFromTopicWithEntryId(string colName, DB topic, params int[] entryIds)
        {
            List<DB.Cell> returnedCells = new List<DB.Cell>();

            if (colName != null && topic != null && entryIds != null)
            {
                foreach (int anotherId in entryIds)
                {
                    if ( anotherId >= 0 && anotherId < topic.EntryCount)
                    {
                        // Getting entry
                        DB.Entry currentEntry = topic.Entries[anotherId];
                        // Getting cell...
                        DB.Cell currentCell = GetCellFromEntry(topic, currentEntry, colName);

                        returnedCells.Add(currentCell);
                    }
                }                
            }

            return returnedCells;
        }

        /// <summary>
        /// Technical request (fast) to get full entries
        /// No SQL equivalent known.
        /// </summary>
        /// <param name="topic">Database topic</param>
        /// <param name="entryIds"></param>
        public static List<DB.Entry> SelectAllCellsFromTopicWhereId(DB topic, params int[] entryIds)
        {
            List<DB.Entry> result = new List<DB.Entry>();

            if (topic != null && entryIds != null)
            {
                // Searching for specified id values
                foreach (int anotherId in entryIds)
                {
                    DB.Entry resultEntry = new DB.Entry();

                    if (anotherId < topic.Entries.Count)
                        resultEntry = topic.Entries[anotherId];

                    result.Add(resultEntry);
                }
            }

            return result;
        }

        /// <summary>
        /// Basic request to get all cells from all columns
        /// SQL equivalent: SELECT * FROM [topic];
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        public static List<DB.Entry> SelectAllCellsFromTopic(DB topic)
        {
            return topic.Entries;
        }

        /// <summary>
        /// Basic request from primary key (quick)
        /// SQL equivalent: SELECT * FROM [topic] WHERE (primary key) IN ([pkValues]);
        /// </summary>
        /// <param name="topic">Database topic</param>
        /// <param name="pkValues">Array of primary key values to search</param>
        public static List<DB.Entry> SelectAllCellsFromTopicWherePrimaryKey(DB topic, params string[] pkValues)
        {
            List<DB.Entry> result = new List<DB.Entry>();

            if (pkValues == null)
                return result;

            // Searching for specified primary key values
            foreach (string anotherPk in pkValues)
            {
                if (topic.EntriesByPrimaryKey.ContainsKey(anotherPk))
                {
                    DB.Entry currentEntry = topic.EntriesByPrimaryKey[anotherPk];

                    if (!result.Contains(currentEntry))
                        result.Add(currentEntry);
                }
            }

            return result;
        }

        /// <summary>
        /// Basic request from specified cell value (slower)
        /// SQL equivalent: SELECT * FROM [topic] WHERE [cellName] = '[cellValue]';
        /// </summary>
        /// <param name="topic">Database topic</param>
        /// <param name="cellName">Name of column to test</param>
        /// <param name="cellValue">Value to search in cellName</param>
        public static List<DB.Entry> SelectAllCellsFromTopicWhereCellValue(DB topic, string cellName, string cellValue)
        {
            List<DB.Entry> result = new List<DB.Entry>();

            if (string.IsNullOrEmpty(cellName) || string.IsNullOrEmpty(cellValue))
                return result;

            // Searching for specified value
            foreach (DB.Entry anotherEntry in topic.Entries)
            {
                DB.Cell searchedCell = GetCellFromEntry(topic, anotherEntry, cellName);

                if (cellValue.Equals(searchedCell.value, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (!result.Contains(anotherEntry))
                        result.Add(anotherEntry);
                }
            }

            return result;
        }

        /// <summary>
        /// Request to get maximum column value from specified topic
        /// </summary>
        /// <param name="cellName"></param>
        /// <param name="topic"></param>
        /// <returns></returns>
        public static string SelectMaxColumnFromTopic(string cellName, DB topic)
        {
            string returnedValue = null;

            if (!string.IsNullOrEmpty(cellName) && topic != null)
            {
                // Gathering all column values....
                List<int> allValues = new List<int>();
                List<DB.Cell[]> allCells = SelectCellsFromTopic(topic, cellName);

                foreach (DB.Cell[] cell in allCells)
                {
                    int currentValue = int.Parse(cell[0].value);

                    allValues.Add(currentValue);
                }

                // Sorts array
                if (allValues.Count > 0)
                {
                    allValues.Sort();
                    returnedValue = allValues[allValues.Count - 1].ToString();
                }
            }

            return returnedValue;
        }

        /// <summary>
        /// Basic update (quick)
        /// SQL equivalent: UPDATE [topic] SET [colName]=[newValue] WHERE (primary key) = '[pkValue]';
        /// </summary>
        /// <param name="topic">Database topic</param>
        /// <param name="colName">Column name</param>
        /// <param name="pkValue">Value identifying line to search</param>
        /// <param name="newValue">New value to write into database</param>
        public static void UpdateCellFromTopicWherePrimaryKey(DB topic, string colName, string pkValue, string newValue)
        {
            if (colName == null || pkValue == null)
                return;

            // Getting cell...
            DB.Cell cell = SelectCellsFromTopicWherePrimaryKey(colName, topic, pkValue)[0];

            // Update
            _UpdateCell(topic, cell, newValue);
        }

        /// <summary>
        /// Basic update (slow)
        /// SQL equivalent: UPDATE [topic] SET [colName]=[newValue] WHERE [conds[0].firstValue] = '[conds[0].secondValue]' AND [conds[1].firstValue = '[conds[1].secondValue]' ...;
        /// </summary>
        /// <param name="topic">Database topic</param>
        /// <param name="colName">Column name</param>
        /// <param name="newValue">New value to write into database</param>
        /// <param name="conds">Array of (column name,value) conditions</param>
        public static void UpdateCellFromTopicWhereCellValues(DB topic, string colName, string newValue, params Couple<string>[] conds)
        {
            if (colName == null || newValue == null || conds == null || conds.Length == 0)
                return;

            // Getting cell...
            DB.Cell cell = SelectCellsFromTopicWhereCellValues(colName, topic, conds)[0];
        
            // Update
            _UpdateCell(topic, cell, newValue);
        }
        
        /// <summary>
        /// Basic update (technical-fast)
        /// No SQL equivalent.
        /// </summary>
        /// <param name="topic">Database topic</param>
        /// <param name="colName">Column name</param>
        /// <param name="newValue">New value to write into database</param>
        /// <param name="entryId">Technical id of entry to modify</param>
        public static void UpdateCellFromTopicWithEntryId(DB topic, string colName, string newValue, int entryId)
        {
            if (colName == null || newValue == null || entryId < 0 || entryId >= topic.EntryCount)
                return;

            // Getting entry
            DB.Entry currentEntry = topic.Entries[entryId];

            // Getting cell...
            DB.Cell cell = GetCellFromEntry(topic, currentEntry, colName);

            // Update           
            _UpdateCell(topic, cell, newValue);
        }

        /// <summary>
        /// Full update.
        /// No SQL equivalent known. Replaces specified entry with provided value. Cell values must be separated by tab 
        /// </summary>
        /// <param name="topic">Database topic</param>
        /// <param name="pkValue">Value identifying line to update; REF column, typically</param>
        /// <param name="value">New value</param>
        public static void UpdateAllCellsFromTopicWherePrimaryKey(DB topic, string pkValue, string value)
        {
            if (pkValue == null || value == null)
                return;

            DB.Entry currentEntry = topic.EntriesByPrimaryKey[pkValue];
            DB.Entry newEntry = _UpdateFullEntryWithValue(currentEntry, value);

            if (currentEntry.cells != null && newEntry.cells != null)
            {
                topic.Entries[currentEntry.index] = newEntry;
                topic._UpdateEntryCache(pkValue, newEntry, false);                
            }
        }

        /// <summary>
        /// Replaces specified value from any column with provided default value
        /// No SQL equivalent known.
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="value">value to remove</param>
        /// <param name="defaultValue">default value to set</param>
        public static void RemoveValueFromAnyColumn(DB topic, string value, string defaultValue)
        {
            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(defaultValue))
            {
                // Parsing all entries
                for (int i = 0; i < topic.Entries.Count; i++)
                {
                    DB.Entry currentEntry = topic.Entries[i];
                    bool isModified = false;

                    // Parsing all columns
                    for (int j = 0; j < currentEntry.cells.Count; j++)
                    {
                        DB.Cell currentCell = currentEntry.cells[j];

                        if (value.Equals(currentCell.value))
                        {
                            currentCell.value = defaultValue;
                            currentEntry.cells[j] = currentCell;
                            isModified = true;
                        }
                    }

                    if (isModified)
                        topic.Entries[i] = currentEntry;
                }
            }
        }

        /// <summary>
        /// Full insert. PK value is not checked.
        /// SQL equivalent: INSERT INTO [topic] (column list) VALUES ([value]);
        /// </summary>
        /// <param name="topic">Database topic</param>
        /// <param name="value">Values to add (=list of values separated by tab)</param>
        public static void InsertAllCellsIntoTopic(DB topic, string value)
        {
            if (topic != null && !string.IsNullOrEmpty(value))
            {
                // Parsing provided value
                string[] allValues = value.Split(_SYMBOL_FIELD_SEPARATOR);

                if (allValues.Length == topic.ColumnCount)
                {
                    // Creating entry
                    int entryIndex = topic.EntryCount;
                    DB.Entry newEntry = new DB.Entry();

                    newEntry.index = entryIndex;
                    newEntry.cells = new List<DB.Cell>();

                    // Creating all cells
                    int cellIndex = 0;

                    foreach (string anotherValue in allValues)
                    {
                        DB.Cell currentCell = topic._CreateEmptyCell(cellIndex);

                        currentCell.entryIndex = entryIndex;
                        currentCell.value = anotherValue;
                        newEntry.cells.Add(currentCell);

                        // BUG_78: updating primary key in entry
                        if (currentCell.valueType == DB.ValueType.PrimaryKey)
                            newEntry.primaryKey = currentCell.value;

                        cellIndex++;
                    }

                    // Adding entry
                    topic.Entries.Add(newEntry);

                    // Updating index
                    DB.Cell pkCell = GetCellFromEntry(topic, newEntry, DatabaseConstants.REF_DB_COLUMN);

                    if (pkCell.value != null)
                    {
                        newEntry.primaryKey = pkCell.value;
                        topic._UpdateEntryCache(pkCell.value, newEntry, false);
                    }
                }
                else
                    Log.Error("Value to insert is not compatible with target table.", null);
            }
        }

        /// <summary>
        /// Full insert. PK value is automatically set and returned.
        /// SQL equivalent: INSERT INTO [topic] (column list) VALUES ([value]);
        /// </summary>
        /// <param name="topic">Database topic</param>
        /// <param name="pkColumnIndex">0-based index of column containing PK value; -1 indicates no pk in this line.</param>
        /// <param name="values">Values to add (=list of values separated by tab)</param>
        /// <returns></returns>
        public static string InsertAllCellsIntoTopic(DB topic, int pkColumnIndex, string values)
        {
            string returnedPk = null;

            if (topic != null && !string.IsNullOrEmpty(values))
            {
                // Parsing provided value
                string[] allValues = values.Split(_SYMBOL_FIELD_SEPARATOR);

                if (allValues.Length == topic.ColumnCount)
                {
                    if (pkColumnIndex != -1)
                    {
                        if (pkColumnIndex < -1 || pkColumnIndex >= topic.ColumnCount)
                            pkColumnIndex = 0;

                        // Generating new primary key
                        int lastPk = int.Parse(SelectMaxColumnFromTopic(DatabaseConstants.REF_DB_COLUMN, topic));

                        lastPk++;
                        allValues[pkColumnIndex] = lastPk.ToString();
                        values = BuildFullLineValue(topic, allValues);
                        returnedPk = lastPk.ToString();
                    }

                    InsertAllCellsIntoTopic(topic, values);
                }
                else 
                    Log.Error("Value to insert is not compatible with target table.", null);             
            }

            return returnedPk;
        }

        /// <summary>
        /// Full insert (technical, fast). Entry must not exists already !
        /// No SQL equivalent.
        /// </summary>
        /// <param name="topic">Database topic</param>
        /// <param name="entry">Entry to add</param>
        public static void InsertAllCellsIntoTopic(DB topic, DB.Entry entry)
        {
            if (topic != null && entry.cells.Count == topic.Structure.Count)
            {
                topic.Entries.Add(entry);

                _UpdateAllCellEntryIds(entry);
                _UpdatePKCache(topic, entry);
            }
            else
                Log.Error("Value to insert is not compatible with target table.", null);
        }

        /// <summary>
        /// Deletion.
        /// SQL equivalent: DELETE FROM [topic] WHERE [first column]=[id];
        /// </summary>
        /// <param name="topic">Database topic</param>
        /// <param name="id">Id of lines to delete (value of first column)</param>
        public static void DeleteFromTopicWhereIdentifier(DB topic, string id)
        {
            if (string.IsNullOrEmpty(id))
                return;

            // Browsing all entries
            int deletedCount = 0;
            int entryIndex = 0;

            while (entryIndex < topic.Entries.Count)
            {
                DB.Entry anotherEntry = topic.Entries[entryIndex];
                DB.Cell firstCell = anotherEntry.cells[0];

                if (id.Equals(firstCell.value))
                {
                    topic.Entries.Remove(anotherEntry);

                    if (anotherEntry.primaryKey != null)
                        topic.EntriesByPrimaryKey.Remove(anotherEntry.primaryKey);

                    deletedCount++;
                }
                else
                {
                    if (deletedCount > 0)
                    {
                        anotherEntry.index -= deletedCount;

                        // BUG_95: update entry id in all cells
                        anotherEntry = _UpdateAllCellEntryIds(anotherEntry);
                        //   
                        _UpdatePKCache(topic, anotherEntry);
                        topic.Entries[anotherEntry.index] = anotherEntry;
                    }

                    entryIndex++;
                }
            }
        }

        /// <summary>
        /// Deletion (technical-fast)
        /// No SQL equivalent.
        /// </summary>
        /// <param name="topic">Database topic</param>
        /// <param name="entryId">Id of entries to delete (value of first column)</param>
        public static void DeleteFromTopicWithEntryId(DB topic, int entryId)
        {
            if (entryId < 0 || entryId >= topic.EntryCount)
                return;

            // Removing entry
            DB.Entry currentEntry = topic.Entries[entryId];

            topic.Entries.Remove(currentEntry);

            if (currentEntry.primaryKey != null)
                topic.EntriesByPrimaryKey.Remove(currentEntry.primaryKey);

            // Updating following entry ids
            for (int index = entryId; index < topic.Entries.Count; index++ )
            {
                DB.Entry anotherEntry = topic.Entries[index];

                anotherEntry.index--;

                // BUG_95: update entry id in all cells
                anotherEntry = _UpdateAllCellEntryIds(anotherEntry);
                //   
                _UpdatePKCache(topic, anotherEntry);
                topic.Entries[anotherEntry.index] = anotherEntry;
            }
        }
        #endregion

        #region Data handling methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string BuildFullLineValue(DB topic, params string[] values)
        {
            string returnedValue = "";
            StringBuilder sb = new StringBuilder();

            if (values.Length == topic.ColumnCount)
            {
                for (int i = 0; i < topic.ColumnCount; i++)
                {
                    sb.Append(values[i]);

                    if (i != (topic.ColumnCount - 1))
                        sb.Append(_SYMBOL_FIELD_SEPARATOR);
                }

                returnedValue = sb.ToString();
            }

            return returnedValue;
        }

        /// <summary>
        /// Returns final resource value from specified cell.
        /// Does not support cross reference still.
        /// </summary>
        /// <param name="cell">Cell to get value</param>
        /// <param name="topicResource">Resource file to get values (when valueType = ValueInResource)</param>
        /// <returns></returns>
        public static string GetResourceValueFromCell(DB.Cell cell, DBResource topicResource)
        {
            string result;

            // According to value type...
            switch (cell.valueType)
            {
                case DB.ValueType.ReferenceL:
                case DB.ValueType.ValueInResource:
                case DB.ValueType.ValueInResourceH:
                    // Corresponding value must be found into resource file
                    DBResource.Entry currentEntry = topicResource.GetEntryFromId(cell.value);

                    result = currentEntry.value;
                    break;
                default:
                    result = cell.value;
                    break;
            }

            // Failsafe operation
            if (result == null)
            {
                result = DatabaseConstants.DEFAULT_RESOURCE_VALUE;
                Log.Info("WARNING ! Resource with code " + cell.value + " was not found in " + topicResource.CurrentTopic + "-" + topicResource.CurrentCulture + ". Please fix it!");
            }

            return result;
        }

        /// <summary>
        /// Retrieves a cell with given name in specified entry
        /// </summary>
        /// <param name="topic">Table providing data</param>
        /// <param name="entry">Entry to parse</param>
        /// <param name="colName">Column name of searched cell</param>
        /// <returns></returns>
        public static DB.Cell GetCellFromEntry(DB topic, DB.Entry entry, string colName)
        {
            DB.Cell result = new DB.Cell();

            // Using index
            if (colName != null && topic.ColumnIndexByName.ContainsKey(colName))
            {
                int columnIndex = topic.ColumnIndexByName[colName];

                result = entry.cells[columnIndex];
            }

            return result;
        }

        /// <summary>
        /// Returns BitField value in a bool array fashion. 
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static bool[] ParseBitField(DB.Cell cell)
        {
            bool[] result = new bool[0];

            if (cell.valueType == DB.ValueType.BitField)
            {
                uint valueToParse;
                
                if (uint.TryParse(cell.value, out valueToParse))
                    result = BinaryTools.IntegerToBits(valueToParse);
            }

            return result;
        }
        
        /// <summary>
        /// Retrieves all entries having specified values for provided column indexes
        /// </summary>
        /// <param name="topic">Table providing data</param>
        /// <param name="columnIndexes">Array of all column indexes to search</param>
        /// <param name="columnValues">Array of all column values (columnIndexes array index-wise)</param>
        /// <returns>List of corresponding entries</returns>
        public static List<DB.Entry> GetAllEntriesWithSpecifiedColumnValues(DB topic, int[] columnIndexes, string[] columnValues)
        {
            List<DB.Entry> returnedList = new List<DB.Entry>();

            if (topic != null
                && columnValues != null
                && columnIndexes != null
                && columnIndexes.Length == topic.ColumnCount
                && columnIndexes.Length == columnValues.Length)
            {
                // Searching over all entries
                foreach (DB.Entry anotherEntry in topic.Entries)
                {
                    bool searchResult = false;

                    foreach (int anotherIndex in columnIndexes)
                    {
                        string searchedValue = columnValues[anotherIndex];
                        DB.Cell currentCell = anotherEntry.cells[anotherIndex];

                        if (currentCell.value != null && currentCell.value.Equals(searchedValue))
                            searchResult = true;
                        else
                        {
                            searchResult = false;
                            break;
                        }
                    }

                    if (searchResult)
                    {
                        // Entry found, it must be added to result list
                        returnedList.Add(anotherEntry);
                    }
                }
            }

            return returnedList;
        }

        /// <summary>
        /// Exchanges specified entries
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="entryId1"></param>
        /// <param name="entryId2"></param>
        public static void SwapEntriesWithEntryId(DB topic, int entryId1, int entryId2)
        {
            if (topic != null 
                && entryId1 >= 0 
                && entryId2 >= 0 
                && entryId1 < topic.EntryCount
                && entryId2 < topic.EntryCount
                && entryId1 != entryId2)
            {
                DB.Entry entry1 = topic.Entries[entryId1];
                DB.Entry entry2 = topic.Entries[entryId2];

                // Entryies update
                entry1.index = entryId2;
                entry2.index = entryId1;

                // Cells update
                _UpdateAllCellEntryIds(entry1);
                _UpdateAllCellEntryIds(entry2);

                // PK update
                _UpdatePKCache(topic, entry1);
                _UpdatePKCache(topic, entry2);
                
                // Entry cache update
                topic.Entries[entryId1] = entry2;
                topic.Entries[entryId2] = entry1;
                
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Updates the whole specified entry with provided value 
        /// </summary>
        /// <param name="entryToUpdate">Entry to update</param>
        /// <param name="newValue">All values, separated by default separator (TAB)</param>
        /// <returns></returns>
        private static DB.Entry _UpdateFullEntryWithValue(DB.Entry entryToUpdate, string newValue)
        {
            DB.Entry returnedEntry = new DB.Entry();

            // Parsing provided value
            string[] allValues = newValue.Split(_SYMBOL_FIELD_SEPARATOR);

            if (allValues.Length == entryToUpdate.cells.Count)
            {
                // Updating all cells
                int cellIndex = 0;

                foreach (string anotherValue in allValues)
                {
                    DB.Cell currentCell = entryToUpdate.cells[cellIndex];

                    currentCell.value = anotherValue;
                    entryToUpdate.cells[cellIndex] = currentCell;
                    cellIndex++;
                }

                returnedEntry = entryToUpdate;
            }
            else
                Log.Error("Value to update is not compatible with target table.", null);

            return returnedEntry;
        }

        /// <summary>
        /// Utility method to directly update specified cell with newValue
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="cell"></param>
        /// <param name="newValue"></param>
        private static void _UpdateCell(DB topic, DB.Cell cell, string newValue)
        {
            if (topic != null && newValue != null)
            {
                if (cell.valueType == DB.ValueType.Undefined)
                    throw new Exception("Unable to access to specified location.");
                cell.value = newValue;

                // Getting entry
                DB.Entry modifiedEntry = topic.Entries[cell.entryIndex];

                // Setting cell in entry
                modifiedEntry.cells[cell.index] = cell;

                // Setting entry in topic
                topic.Entries[modifiedEntry.index] = modifiedEntry;
            }
        }

        /// <summary>
        /// Ensures entry ids in child cells are correctly set
        /// </summary>
        /// <param name="anotherEntry"></param>
        /// <returns></returns>
        private static DB.Entry _UpdateAllCellEntryIds(DB.Entry anotherEntry)
        {
            List<DB.Cell> allCells = anotherEntry.cells;
            int updatedCount = 0;

            for (int cellIndex = 0; cellIndex < allCells.Count; cellIndex++)
            {
                DB.Cell currentCell = allCells[cellIndex];

                if (currentCell.entryIndex != anotherEntry.index)
                {
                    currentCell.entryIndex = anotherEntry.index;
                    allCells[cellIndex] = currentCell;
                    updatedCount++;
                }
            }

            if (updatedCount > 0)
                anotherEntry.cells = allCells;

            return anotherEntry;
        }

        /// <summary>
        /// Ensures PK cache is correctly updated
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="anotherEntry"></param>
        private static void _UpdatePKCache(DB topic, DB.Entry anotherEntry)
        {
            if (topic != null && anotherEntry.primaryKey != null)
                topic.EntriesByPrimaryKey[anotherEntry.primaryKey] = anotherEntry;
        }
        #endregion
    }
}
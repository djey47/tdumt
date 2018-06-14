using System;
using System.Collections.Generic;
using DjeFramework1.Common.Support.Traces;
using TDUModdingLibrary.fileformats.database.util;

namespace TDUModdingLibrary.fileformats.database.helper
{
    /// <summary>
    /// Utility class to fix database corruption errors
    /// </summary>
    public static class DatabaseFixer
    {
        #region Constants
        /// <summary>
        /// Default resource value
        /// </summary>
        private const string _DEFAULT_RESOURCE_VALUE = "<Fixed by TDUMT>";
        #endregion
        #region Enums
        /// <summary>
        /// Types of corruption
        /// </summary>
        public enum CorruptionKind
        {
            None = 0,
            MissingResource = 1,
            MissingReference = 2,
            UnknownReferencedTopic,
            StructureMissingColumns
        }
        #endregion

        #region Inner types
        /// <summary>
        /// Corruption entry
        /// </summary>
        public struct Corruption
        {
            public CorruptionKind kind;
            public string comment;
            public DB.Topic topic;
            public DB.Topic referencedTopic;
            public DB.Culture culture;
            public int entryId;
            public DB.Cell corruptedCell;
            public string corruptedValue;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Defines data to process
        /// </summary>
        public static Dictionary<DB.Topic, TduFile[]> Data
        {
            set { _Data = value; }
        }

        /// <summary>
        /// Defines data to help fixing
        /// </summary>
        public static Dictionary<DB.Topic, TduFile[]> BackupData
        {
            set { _BackupData = value; }
        }
        #endregion

        #region Private fields
        /// <summary>
        /// Loaded data
        /// </summary>
        private static Dictionary<DB.Topic, TduFile[]> _Data;

        /// <summary>
        /// Loaded data as backup
        /// </summary>
        private static Dictionary<DB.Topic, TduFile[]> _BackupData;
        #endregion

        #region Public methods
        /// <summary>
        /// Loads a particular topic and its resource to fix it. To fix an already loaded database, just set Data property and do not use this method.
        /// </summary>
        /// <param name="dbTopic"></param>
        /// <param name="dbResource"></param>
        public static void LoadTopic(DB dbTopic, DBResource dbResource)
        {
            if (dbTopic != null && dbResource != null)
            {
                // Data init if needed
                if (_Data == null)
                    _Data = new Dictionary<DB.Topic, TduFile[]>();

                DB.Topic currentTopic = dbTopic.CurrentTopic;

                _Data.Remove(currentTopic);
                _Data.Add(currentTopic, new TduFile[] { dbTopic, dbResource});
            } 
        }
        
        /// <summary>
        /// Removes all data
        /// </summary>
        public static void ClearData()
        {
            _Data.Clear();
        }

        /// <summary>
        /// Fixes specified corruption
        /// </summary>
        /// <param name="corruptionEntry"></param>
        public static void Fix(Corruption corruptionEntry)
        {
            // Has topic been loaded yet ?
            if (_Data.ContainsKey(corruptionEntry.topic))
            {
                // Data access
                TduFile[] files = _Data[corruptionEntry.topic];
                DB db = files[0] as DB;
                DBResource res = files[1] as DBResource;

                TduFile[] refFiles = _Data[corruptionEntry.referencedTopic];
                DB refDb = refFiles[0] as DB;
                DBResource refRes = refFiles[1] as DBResource;

                TduFile[] backupFiles = _BackupData[corruptionEntry.topic];
                DB backupDb = backupFiles[0] as DB;
                DBResource backupRes = backupFiles[1] as DBResource;
                
                TduFile[] backupRefFiles = _BackupData[corruptionEntry.referencedTopic];
                DB backupRefDb = backupRefFiles[0] as DB;
                DBResource backupRefRes = backupRefFiles[1] as DBResource;

                // Integrity checks
                if (db == null || res == null || refDb == null || refRes == null)
                    throw new Exception("Database loading error.");
                if  (backupDb == null || backupRes == null || backupRefDb == null || backupRefRes == null)
                    throw new Exception("Database backup loading error.");
                    
                string colName = corruptionEntry.corruptedCell.name;

                // According to corruption kind
                switch(corruptionEntry.kind)
                {
                    case CorruptionKind.UnknownReferencedTopic:
                        throw new Exception("UnknownReferencedTopic issue can't be solved yet.");
                    case CorruptionKind.StructureMissingColumns:
                        throw new Exception("StructureMissingColumns issue can't be solved yet.");
                    case CorruptionKind.MissingReference:
                        // If referenced entry does not exist in backup, restore referenced id from backup
                        // else restore entry
                        string referencedEntryPk = corruptionEntry.corruptedValue;
                        List<DB.Entry> entries = DatabaseHelper.SelectAllCellsFromTopicWherePrimaryKey(backupRefDb, new[] {referencedEntryPk});

                        if (entries.Count == 0)
                        {
                            DB.Entry currentEntry =
                                DatabaseHelper.SelectAllCellsFromTopicWhereId(db, corruptionEntry.entryId)[0];
                            string entryPk = currentEntry.primaryKey;
                            List<DB.Cell> backupCells =
                                DatabaseHelper.SelectCellsFromTopicWherePrimaryKey(colName, backupDb, new[]{entryPk});

                            if (backupCells.Count == 0)
                            {
                                // Worst case : referenced item not found in backup > must restore original reference
                                DB.Cell cleanCell = DatabaseHelper.SelectCellsFromTopicWithEntryId(colName, backupDb, new[]{corruptionEntry.entryId})[0];

                                DatabaseHelper.UpdateCellFromTopicWithEntryId(db, colName, cleanCell.value,
                                                                              corruptionEntry.entryId);
                                Log.Info("Fixed issue : " + corruptionEntry.kind + " by setting new reference: " + cleanCell.value + " in " + colName + ", line " + corruptionEntry.entryId);
                            }
                            else
                            {
                                DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(db, colName, entryPk,
                                                                                  backupCells[0].value);
                                Log.Info("Fixed issue : " + corruptionEntry.kind + " by setting new reference: " + backupCells[0].value + " in " + colName + ", line " + corruptionEntry.entryId);
                            }

                            db.Save();           
                        }
                        else
                        {
                            DatabaseHelper.InsertAllCellsIntoTopic(refDb, entries[0]);
                            refDb.Save();

                            Log.Info("Fixed issue : " + corruptionEntry.kind + " by creating new line.");
                        }
                        break;
                    case CorruptionKind.MissingResource:
                        // If resource exists in backup, restore resource id
                        // else restore referenced resource id from backup
                        DBResource backupResource;
                        DBResource resource;

                        if (corruptionEntry.topic == corruptionEntry.referencedTopic)
                        {
                            // Resource is in the same topic
                            backupResource = backupRes;
                            resource = res;
                        }
                        else
                        {
                            // Resource is in another topic
                            backupResource = backupRefRes;
                            resource = refRes;
                        }

                        DBResource.Entry backupResEntry = backupResource.GetEntryFromId(corruptionEntry.corruptedValue);

                        if (backupResEntry.isValid)
                        {
                            resource.InsertEntry(backupResEntry);
                            resource.Save();

                            Log.Info("Fixed issue : " + corruptionEntry.kind + " by creating new resource: " + backupResEntry.value + " in " + resource.CurrentTopic + ", id=" + backupResEntry.id);
                        }
                        else
                        {
                            DB.Cell backupCell =
                                DatabaseHelper.SelectCellsFromTopicWithEntryId(colName, backupDb, corruptionEntry.entryId)[0];

                            // Trying to get resource from backup
                            backupResEntry = backupResource.GetEntryFromId(backupCell.value);

                            if (backupResEntry.isValid)
                            {
                                DatabaseHelper.UpdateCellFromTopicWithEntryId(db, colName, backupCell.value,
                                                                            corruptionEntry.entryId);
                                db.Save();

                                Log.Info("Fixed issue : " + corruptionEntry.kind + " by setting new reference to resource: " + backupCell.value + " in " + colName + ", line " + corruptionEntry.entryId);
                            }
                            else
                            {
                                // Applying default resource value
                                backupResEntry.id = new ResourceIdentifier(backupCell.value, backupResource.CurrentTopic);
                                backupResEntry.isValid = true;
                                backupResEntry.value = _DEFAULT_RESOURCE_VALUE;
                                backupResEntry.index = resource.EntryList.Count;

                                resource.InsertEntry(backupResEntry);
                                resource.Save();

                                Log.Info("Fixed issue : " + corruptionEntry.kind + " by creating new resource: " +
                                         backupResEntry.value + " in " + resource.CurrentTopic + ", id=" +
                                         backupResEntry.id);
                            }
                        }

                        break;
                }
            }
            else
                throw new Exception("Please load corresponding data and resource first!");
        }
        #endregion
    }
}

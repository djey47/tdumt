using System;
using System.Collections.Generic;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Util.BasicStructures;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.support.constants;
using TDUModdingLibrary.support.editing;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.util;

namespace TDUModdingLibrary.support.patcher.instructions
{
    /// <summary>
    /// Instruction to update lines in regular database files (with REF)
    /// </summary>
    /// EVO_114
    class UpdateDatabaseI : PatchInstruction
    {
        /// <summary>
        /// Instruction name
        /// </summary>
        public override string Name
        {
            get { return InstructionName.updateDatabase.ToString(); }
        }

        /// <summary>
        /// Instruction description
        /// </summary>
        public override string Description
        {
            get { return "Adds or modifies lines in provided database file."; }
        }

        /// <summary>
        /// List of supported parameters
        /// </summary>
        internal override Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo>
            SupportedParameterInformation
        {
            get
            {
                ParameterInfo fileNameParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.resourceFileName, true);
                ParameterInfo valuesParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.resourceValues, true);

                return _DefineParameters(fileNameParameter, valuesParameter);
            }
        }

        /// <summary>
        /// What the instruction should do
        /// </summary>
        protected override void _Process()
        {
            // Checking parameters
            string fileName = _GetParameter(PatchInstructionParameter.ParameterName.resourceFileName);
            string values = _GetParameter(PatchInstructionParameter.ParameterName.resourceValues);

            // Modifying corresponding topic
            DB.Topic currentTopic = (DB.Topic) Enum.Parse(typeof(DB.Topic), fileName);
            EditHelper.Task dbTask = new EditHelper.Task();
            string bnkFilePath = "";

            try 
            {
                string dbFileName = null;

                // 1. Creating edit task
                try
                {
                    bnkFilePath = string.Concat(LibraryConstants.GetSpecialFolder(LibraryConstants.TduSpecialFolder.Database), DB.GetBNKFileName(DB.Culture.Global));
                    
                    BNK databaseBNK = TduFile.GetFile(bnkFilePath) as BNK;

                    if (databaseBNK != null)
                    {
                        dbFileName = DB.GetFileName(DB.Culture.Global, currentTopic);
                        dbTask = EditHelper.Instance.AddTask(databaseBNK, databaseBNK.GetPackedFilesPaths(dbFileName)[0], true);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to create edit task on BNK file: " + bnkFilePath, ex);
                }

                // 2. Getting TduFile
                DB db;

                try
                {
                    db = TduFile.GetFile(dbTask.extractedFile) as DB;

                    if (db == null || !db.Exists)
                        throw new Exception("Extracted db file not found!");
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to gain access to DB contents: " + dbFileName, ex);
                }

                // 3. Setting values in DB file
                // One identifier (primary key: REF) or 2 identifiers (2 first columns)
                List<Couple<string>> couples = Tools.ParseCouples(values);
                string[] idArray = new string[couples.Count];
                int counter = 0;

                // Parsing couples
                foreach (Couple<string> couple in couples)
                {
                    string id = couple.FirstValue;
                    string value = couple.SecondValue;

                    // Does id exist ?
                    if (db.EntriesByPrimaryKey.ContainsKey(id))
                    {
                        // Already exists > modify it
                        DatabaseHelper.UpdateAllCellsFromTopicWherePrimaryKey(db, id, value);

                        Log.Info("Entry updating completed: " + id + " - " + value);
                    }
                    else
                    {
                        // Does not exist > create it
                        DatabaseHelper.InsertAllCellsIntoTopic(db, value);

                        Log.Info("Entry adding completed: " + value);
                    }

                    idArray[counter++] = id;
                }

                // 4. Saving
                try
                {
                    db.Save();
                    EditHelper.Instance.ApplyChanges(dbTask);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to save BNK file: " + bnkFilePath, ex);
                }
            }
            finally
            {
                // Cleaning up
                EditHelper.Instance.RemoveTask(dbTask);
            }
        }
    }
}
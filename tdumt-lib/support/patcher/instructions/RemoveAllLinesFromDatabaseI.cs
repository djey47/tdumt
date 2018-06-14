using System;
using System.Collections.Generic;
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
    class RemoveAllLinesFromDatabaseI:PatchInstruction
    {
        /// <summary>
        /// Instruction name
        /// </summary>
        public override string Name
        {
            get { return InstructionName.removeAllLinesFromDatabase.ToString(); }
        }

        /// <summary>
        /// Instruction description
        /// </summary>
        public override string Description
        {
            get {
                return "Removes all lines with specified barcode."; }
        }

        /// <summary>
        /// List of supported parameters
        /// </summary>
        // EVO_81: property added
        internal override Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo>
            SupportedParameterInformation
        {
            get
            {
                ParameterInfo fileNameParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.resourceFileName, true);
                ParameterInfo idParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.databaseId, true);

                return _DefineParameters(fileNameParameter, idParameter);
            }
        }

        /// <summary>
        /// What the instruction should do
        /// </summary>
        protected override void _Process()
        {
            // Checking parameters
            string fileName = _GetParameter(PatchInstructionParameter.ParameterName.resourceFileName);
            string id = _GetParameter(PatchInstructionParameter.ParameterName.databaseId);

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

                // 3. Removing values in DB file
                DatabaseHelper.DeleteFromTopicWhereIdentifier(db, id);

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
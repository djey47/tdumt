using System;
using System.Collections.Generic;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Util.BasicStructures;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.database.helper;
using TDUModdingLibrary.fileformats.database.util;
using TDUModdingLibrary.support.constants;
using TDUModdingLibrary.support.editing;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.util;

namespace TDUModdingLibrary.support.patcher.instructions
{
    class SetVehicleOnSpotsI:PatchInstruction
    {
        /// <summary>
        /// Instruction name
        /// </summary>
        public override string Name
        {
            get { return InstructionName.setVehicleOnSpots.ToString(); }
        }

        /// <summary>
        /// Instruction description
        /// </summary>
        public override string Description
        {
            get { return "Dispatches specified car to many availability locations."; }
        }

        /// <summary>
        /// List of supported parameters
        /// </summary>
        internal override Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo>
            SupportedParameterInformation
        {
            get
            {
                ParameterInfo vehicleSlotParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.vehicleDatabaseId, true);
                ParameterInfo valuesParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.resourceValues, true);

                return _DefineParameters(vehicleSlotParameter, valuesParameter);
            }
        }

        /// <summary>
        /// What the instruction should do
        /// </summary>
        protected override void _Process()
        {
            // Checking parameter
            string vehicleRef = _GetParameter(PatchInstructionParameter.ParameterName.vehicleDatabaseId);
            string spotAndSlots = _GetParameter(PatchInstructionParameter.ParameterName.resourceValues);

            // Modifying corresponding topic
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
                        dbFileName = DB.GetFileName(DB.Culture.Global, DB.Topic.CarShops);
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
                // One identifier (primary key: REF)
                List<Couple<string>> couples = Tools.ParseCouples(spotAndSlots);

                // Parsing couples
                foreach (Couple<string> couple in couples)
                {
                    string spotRef = couple.FirstValue;
                    string[] slots = couple.SecondValue.Split(new string[]{Tools.SYMBOL_VALUE_SEPARATOR3},StringSplitOptions.RemoveEmptyEntries);

                    // Does id exist ?
                    if (db.EntriesByPrimaryKey.ContainsKey(spotRef))
                    {
                        // Already exists > modify it
                        foreach (string anotherSlot in slots)
                        {
                            string slotColumnName =
                                string.Format(DatabaseConstants.SLOT_CARSHOPS_PATTERN_DB_COLUMN, anotherSlot);

                            DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(db, slotColumnName, spotRef, vehicleRef);
                        }

                        Log.Info("Entry updating completed for spot: " + spotRef + " - " + couple.SecondValue);
                    }
                    else
                        Log.Error("Specified location does not exist! " + spotRef,null);
                }

                Log.Info("Entry updating completed for vehicle: " + vehicleRef);

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
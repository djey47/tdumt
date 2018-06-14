using System;
using System.Collections.Generic;
using DjeFramework1.Common.Support.Traces;
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
    class RemoveVehicleFromSpotsI:PatchInstruction
    {
        /// <summary>
        /// Instruction name
        /// </summary>
        public override string Name
        {
            get { return InstructionName.removeVehicleFromSpots.ToString(); }
        }

        /// <summary>
        /// Instruction description
        /// </summary>
        public override string Description
        {
            get { return "Removes specified vehicle reference from all spots (dealers/V-RENT/...)"; }
        }

        /// <summary>
        /// List of supported parameters
        /// </summary>
        internal override Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo>
            SupportedParameterInformation
        {
            get
            {
                ParameterInfo vehicleRefParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.vehicleDatabaseId, true);

                return _DefineParameters(vehicleRefParameter);
            }
        }

        /// <summary>
        /// What the instruction should do
        /// </summary>
        protected override void _Process()
        {
            // Checking parameter
            string vehicleRef = _GetParameter(PatchInstructionParameter.ParameterName.vehicleDatabaseId);

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
                DatabaseHelper.RemoveValueFromAnyColumn(db, vehicleRef, DatabaseConstants.COMPACT1_PHYSICS_DB_RESID);

                Log.Info("Entry updating completed: " + vehicleRef);

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
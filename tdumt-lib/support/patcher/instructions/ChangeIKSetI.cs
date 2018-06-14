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
    class ChangeIKSetI:PatchInstruction
    {
        /// <summary>
        /// Instruction name
        /// </summary>
        public override string Name
        {
            get {
                return "";/* InstructionName.changeIKSet.ToString();*/ }
        }

        /// <summary>
        /// Instruction description
        /// </summary>
        public override string Description
        {
            get { return "Changes IK (positions) set of specified vehicle.\n- slotFullName: name of vehicle slot to be modified.\n- cameraIKIdentifier: id of IK set to use."; }
        }

        /// <summary>
        /// List of supported parameters
        /// </summary>
        internal override Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo>
            SupportedParameterInformation
        {
            get
            {
                ParameterInfo vehicleNameParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.slotFullName, true);
                ParameterInfo ikSlotParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.cameraIKIdentifier, true);

                return _DefineParameters(vehicleNameParameter, ikSlotParameter);
            }
        }

        /// <summary>
        /// What the instruction should do
        /// </summary>
        protected override void _Process()
        {
            // Parameters
            string vehicleName = _GetParameter(PatchInstructionParameter.ParameterName.slotFullName);
            string ikId = _GetParameter(PatchInstructionParameter.ParameterName.cameraIKIdentifier);

            // Loading reference
            VehicleSlotsHelper.InitReference(PatchHelper.CurrentPath);

            // Checking validity
            if (!VehicleSlotsHelper.SlotReference.ContainsKey(vehicleName))
                throw new Exception("Specified vehicle name is not supported: " + vehicleName );

            if (!VehicleSlotsHelper.CamReference.ContainsKey(ikId))
                throw new Exception("Specified IK identifier is not supported: " + ikId);

            // Edit task
            EditHelper.Task task = new EditHelper.Task();

            try
            {
                try
                {
                    string bnkFileName = string.Concat(Tools.TduPath, LibraryConstants.FOLDER_DB, DB.GetBNKFileName(DB.Culture.Global));
                    BNK dbBnkFile = TduFile.GetFile(bnkFileName) as BNK;
                    
                    if (dbBnkFile != null)
                    {
                        string dbFilePath =
                            dbBnkFile.GetPackedFilesPaths(DB.GetFileName(DB.Culture.Global, DB.Topic.CarPhysicsData))[0];

                        task =
                            EditHelper.Instance.AddTask(dbBnkFile, dbFilePath, true);
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to get TDU database contents in DB.BNK.", ex);
                }

                // Opens packed file
                DB physicsDB = TduFile.GetFile(task.extractedFile) as DB;

                if (physicsDB == null)
                    throw new Exception("Unable to get CarPhysicsData information.");

                // Changes IK
                try
                {
                    string vehicleRef = VehicleSlotsHelper.SlotReference[vehicleName];

                    VehicleSlotsHelper.ChangeIKById(vehicleRef, ikId, physicsDB);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to use new IK set: " + ikId + " for " + vehicleName, ex);
                }

                // Saving
                try
                {
                    physicsDB.Save();
                    EditHelper.Instance.ApplyChanges(task);
                    EditHelper.Instance.RemoveTask(task);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to save and replace file in BNK.", ex);
                }
            }
            finally
            {
                EditHelper.Instance.RemoveTask(task);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Util.BasicStructures;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats.database.util;
using TDUModdingLibrary.support.constants;
using TDUModdingLibrary.support.editing;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.util;

namespace TDUModdingLibrary.support.patcher.instructions
{
    /// <summary>
    /// Instruction allowing to update resources into language files 
    /// </summary>
    /// EVO_114
    class UpdateResourceI : PatchInstruction
    {
        /// <summary>
        /// Instruction name
        /// </summary>
        public override string Name
        {
            get { return InstructionName.updateResource.ToString(); }
        }

        /// <summary>
        /// Instruction description
        /// </summary>
        public override string Description
        {
            get { return "Allows to update game resources."; }
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

            // For each language file
            DB.Topic currentTopic = (DB.Topic) Enum.Parse(typeof(DB.Topic), fileName);

            for (int i = 0 ; i < 8 ; i++)
            {
                DB.Culture currentCulture = (DB.Culture) Enum.Parse(typeof (DB.Culture), i.ToString());
                EditHelper.Task resourceTask = new EditHelper.Task();
                string bnkFilePath = "";
                string resourceFileName = null;

                // 1. Creating edit task
                try
                {
                    bnkFilePath = string.Concat(LibraryConstants.GetSpecialFolder(LibraryConstants.TduSpecialFolder.Database), DB.GetBNKFileName(currentCulture));

                    BNK databaseBNK = TduFile.GetFile(bnkFilePath) as BNK;

                    if (databaseBNK != null)
                    {
                        resourceFileName = DB.GetFileName(currentCulture, currentTopic);
                        resourceTask =
                            EditHelper.Instance.AddTask(databaseBNK,
                                                        databaseBNK.GetPackedFilesPaths(resourceFileName)[0], true);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to create edit task on BNK file: " + bnkFilePath, ex);
                }

                // 2. Getting TduFile
                DBResource resource;

                try
                {
                    resource = TduFile.GetFile(resourceTask.extractedFile) as DBResource;

                    if (resource == null || !resource.Exists)
                        throw new Exception("Extracted resource db file not found!");
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to gain access to DB contents: " + resourceFileName, ex);
                }

                // 3. Setting values in DB file
                List<Couple<string>> couples = Tools.ParseCouples(values);

                // Parsing couples
                foreach (Couple<string> couple in couples)
                {
                    string id = couple.FirstValue;
                    string value = couple.SecondValue;

                    // Does id exist ?
                    DBResource.Entry currentEntry = resource.GetEntryFromId(id);

                    if (currentEntry.isValid)
                    {
                        // Already exists > modify it
                        currentEntry.value = value;
                        resource.UpdateEntry(currentEntry);

                        Log.Info("Entry succesfully updated : " + id + " - " + value);
                    }
                    else
                    {
                        // Does not exist > create it
                        currentEntry.isValid = true;
                        currentEntry.id = new ResourceIdentifier(id, currentTopic);
                        currentEntry.value = value;
                        currentEntry.index = resource.EntryList.Count + 1;
                        resource.InsertEntry(currentEntry);

                        Log.Info("Entry succesfully added : " + id + " - " + value);
                    }
                }

                // 4. Saving
                try
                {
                    resource.Save();
                    EditHelper.Instance.ApplyChanges(resourceTask);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to save BNK file: " + bnkFilePath, ex);
                }

                // 5. Cleaning up
                EditHelper.Instance.RemoveTask(resourceTask);
            }
        }
    }
}
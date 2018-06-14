using System;
using System.Collections.Generic;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.util;
using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.support.patcher.instructions
{
    /// <summary>
    /// Instruction to change a rim name
    /// </summary>
    class ChangeRimNameI : PatchInstruction
    {
        /// <summary>
        /// Instruction name
        /// </summary>
        public override string Name
        {
            get {
                return ""; /*InstructionName.changeRimName.ToString();*/ }
        }

        /// <summary>
        /// Instruction description
        /// </summary>
        public override string Description
        {
            get { return "Allows to change specified rim name in TDU database.\n- databaseId: id of rim to modify\n- rimName: new rim name."; }
        }

        /// <summary>
        /// List of supported parameters
        /// </summary>
        internal override Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo> SupportedParameterInformation
        {
            get
            {
                ParameterInfo dbIdParameter = new ParameterInfo(PatchInstructionParameter.ParameterName.databaseId, true);
                ParameterInfo rimNameParameter = new ParameterInfo(PatchInstructionParameter.ParameterName.rimName, false);

                return _DefineParameters(dbIdParameter, rimNameParameter);
            }
        }

        /// <summary>
        /// What the instruction should do
        /// </summary>
        protected override void _Process()
        {
            // Checking parameters
            string databaseIdentifier = _GetParameter(PatchInstructionParameter.ParameterName.databaseId);
            string rimName = _GetParameter(PatchInstructionParameter.ParameterName.rimName);
            string tempFolder = "";

            try
            {
                // 1. Extracting brands
                BNK databaseBNK;
                string rimsDBFileName;
                string rimsDBFilePath;

                try
                {
                    string bnkFilePath = string.Concat(LibraryConstants.GetSpecialFolder(LibraryConstants.TduSpecialFolder.Database), DB.GetBNKFileName(PatchHelper.CurrentCulture));

                    rimsDBFileName = DB.GetFileName(PatchHelper.CurrentCulture, DB.Topic.Rims);
                    databaseBNK = TduFile.GetFile(bnkFilePath) as BNK;

                    if (databaseBNK == null)
                        throw new Exception("Database BNK file is invalid.");

                    // Files are extracted to a temporary location
                    rimsDBFilePath = databaseBNK.GetPackedFilesPaths(rimsDBFileName)[0];

                    tempFolder = File2.SetTemporaryFolder(null, LibraryConstants.FOLDER_TEMP, true);
                    databaseBNK.ExtractPackedFile(rimsDBFilePath, tempFolder, true);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to extract DB file.", ex);
                }

                // 2. Getting TduFile
                DBResource rimsDBResource;

                try
                {
                    rimsDBResource = TduFile.GetFile(tempFolder + @"\" + rimsDBFileName) as DBResource;

                    if (rimsDBResource == null || !rimsDBResource.Exists)
                        throw new Exception("Extracted rim db file not found!");
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to gain access to DB contents.", ex);
                }

                // 3. Setting value in DB file
                DBResource.Entry rimsEntry = rimsDBResource.GetEntryFromId(databaseIdentifier);
                
                try
                {
                    if (rimsEntry.isValid)
                    {
                        rimsEntry.value = rimName;
                        rimsDBResource.UpdateEntry(rimsEntry);
                    }
                    else
                        throw new Exception("Unable to retrieve a DB entry for identifier: " + databaseIdentifier);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to locate DB entry to update.", ex);
                }

                // 4. Saving
                try
                {
                    rimsDBResource.Save();
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to save DB files.", ex);
                }

                // 5. File replacing
                try
                {
                    // Rims
                    databaseBNK.ReplacePackedFile(rimsDBFilePath, tempFolder + @"\" + rimsDBFileName);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to replace database files in BNK.", ex);
                }
            }
            finally
            {
                // Cleaning up
                try
                {
                    File2.RemoveTemporaryFolder(null, tempFolder);
                }
                catch (Exception ex)
                {
                    // Silent
                    Exception2.PrintStackTrace(ex);
                }
            }
        }
    }
}
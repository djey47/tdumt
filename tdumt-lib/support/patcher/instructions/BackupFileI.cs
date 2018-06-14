using System.Collections.Generic;
using System.IO;
using TDUModdingLibrary.support.constants;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.util;

namespace TDUModdingLibrary.support.patcher.instructions
{
    /// <summary>
    /// Instruction to create a file backup
    /// </summary>
    class BackupFileI:PatchInstruction
    {
        public override string Name
        {
            get {
                return "";/* InstructionName.backupFile.ToString();*/ }
        }

        public override string Description
        {
            get { return "Creates a backup of the specified file.\n- fileName: file to make a backup\n- destination: (optional) folder to place backup. If not specified, same folder as fileName is used."; }
        }

        /// <summary>
        /// List of supported parameters
        /// </summary>
        internal override Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo> SupportedParameterInformation
        {
            get
            {
                ParameterInfo fileNameParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.fileName, true);
                ParameterInfo destinationParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.destination, false);

                return _DefineParameters(fileNameParameter, destinationParameter);
            }
        }

        protected override void _Process()
        {
            // Parameters
            string fileName = _GetParameter(PatchInstructionParameter.ParameterName.fileName);
            string destination = _GetParameter(PatchInstructionParameter.ParameterName.destination);
            FileInfo fileToBackup = new FileInfo(fileName);

            if (destination == null)
                destination = fileToBackup.DirectoryName + @"\";
            else
                destination += @"\";

            string backupFileName = destination + fileToBackup.Name + "."  + LibraryConstants.EXTENSION_BACKUP;

            Tools.BackupFile(fileName, backupFileName);
        }
    }
}
using System.Collections.Generic;
using System.IO;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.util;

namespace TDUModdingLibrary.support.patcher.instructions
{
    /// <summary>
    /// Instruction to copy a specified file
    /// </summary>
    class CopyFileI:PatchInstruction
    {
        public override string Name
        {
            get {
                return "";  /*return InstructionName.copyFile.ToString();*/ }
        }

        public override string Description
        {
            get { return "Copies specified file to a custom destination. Warning: existing files with the same name will be lost!\n- fileName: file to copy (full path)\n- destination: path where the file must be copied\n- createFolder: (optional) to enforce destination folder's creation if it does not exist."; }
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
                    new ParameterInfo(PatchInstructionParameter.ParameterName.destination, true);
                ParameterInfo createFolderParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.createFolder, false);

                return _DefineParameters(fileNameParameter, destinationParameter, createFolderParameter);
            } 
        }

        protected override void _Process()
        {
            // Parameters
            string fileName = _GetParameter(PatchInstructionParameter.ParameterName.fileName);
            string destination = _GetParameter(PatchInstructionParameter.ParameterName.destination);
            string createFolder = _GetParameter(PatchInstructionParameter.ParameterName.createFolder);
            bool canCreateFolder = false;

            if (!string.IsNullOrEmpty(createFolder))
                canCreateFolder = bool.Parse(createFolder);

            // Create target folder ?
            if (canCreateFolder && !Directory.Exists(destination))
                Directory.CreateDirectory(destination);

            // File copy
            FileInfo sourceFile = new FileInfo(fileName);
            string copyFileName = destination + @"\" + sourceFile.Name;

            File.Copy(fileName, copyFileName, true);
        }
    }
}
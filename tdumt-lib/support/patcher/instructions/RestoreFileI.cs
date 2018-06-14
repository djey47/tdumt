using System.Collections.Generic;
using System.IO;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.util;

namespace TDUModdingLibrary.support.patcher.instructions
{
    /// <summary>
    /// Instruction to restore a file from backup
    /// </summary>
    class RestoreFileI : PatchInstruction
    {
        public override string Name
        {
            get {
                return "";/* InstructionName.restoreFile.ToString();*/ }
        }

        public override string Description
        {
            get { return "Restores a file from specified backup.\n- fileName: backup to restore\n- destination: (optional) file to replace with backup."; }
        }

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

            if (destination == null)
                destination = @".\" + File2.GetNameFromFilename(fileName);
            else
            {
                if (Directory.Exists(destination))
                {
                    // It's a directory. File name must be appended
                    destination += @".\" + File2.GetNameFromFilename(fileName);
                }
            }

            Tools.RestoreFile(fileName, destination); 
        }
    }
}
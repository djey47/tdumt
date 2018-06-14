using System.Collections.Generic;
using System.IO;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.support.patcher.parameters;
using TDUModdingLibrary.support.patcher.parameters.util;

namespace TDUModdingLibrary.support.patcher.instructions
{
    /// <summary>
    /// Advanced instruction to directly write bytes into binary or text files
    /// </summary>
    class PatchBytesI:PatchInstruction
    {
        public override string Name
        {
            get {
                return "";/* InstructionName.patchBytes.ToString();*/ }
        }

        public override string Description
        {
            get { return "Directly writes bytes at a specific location in file (advanced feature).\n- fileName: name of the file to patch\n- value: (optional) value to write\n- valueAddress: location to write in file\n- valueLength: (optional) number of bytes this value will take."; }
        }

        internal override Dictionary<PatchInstructionParameter.ParameterName, ParameterInfo> SupportedParameterInformation
        {
            get
            {
                ParameterInfo fileNameParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.fileName, true);
                ParameterInfo valueParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.value, false);
                ParameterInfo valueAddressParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.valueAddress, true);
                ParameterInfo valueLengthParameter =
                    new ParameterInfo(PatchInstructionParameter.ParameterName.valueLength, false);

                return _DefineParameters(fileNameParameter, valueParameter, valueAddressParameter, valueLengthParameter);
            }
        }

        protected override void _Process()
        {
            // Parameters
            string fileName = _GetParameter(PatchInstructionParameter.ParameterName.fileName);
            string address = _GetParameter(PatchInstructionParameter.ParameterName.valueAddress);
            string value = _GetParameter(PatchInstructionParameter.ParameterName.value);
            string length = _GetParameter(PatchInstructionParameter.ParameterName.valueLength);
            int intAddress = int.Parse(address);
            long longValue;
            long longLength;

            if (string.IsNullOrEmpty(value))
                longValue = 0;
            else
                longValue = long.Parse(value);
            if (string.IsNullOrEmpty(length))
                longLength = 1;
            else
                longLength = long.Parse(length);

            BinaryWriter writer = null;

            try
            {
                // Removes the read-only attribute
                File2.RemoveAttribute(fileName, FileAttributes.ReadOnly);

                // File opening
                writer = new BinaryWriter(new FileStream(fileName, FileMode.Open, FileAccess.Write));

                // Data writing
                writer.Seek(intAddress, SeekOrigin.Begin);

                switch(longLength)
                {
                    case 1:
                        writer.Write((byte) longValue);
                        break;
                    case 2:
                        writer.Write((short) longValue);
                        break;
                    case 4:
                        writer.Write((int) longValue);
                        break;
                    case 8:
                    default:
                        writer.Write(longValue);
                        break;
                }
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
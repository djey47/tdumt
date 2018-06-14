using System.IO;
using DjeFramework1.Common.Types;

namespace TDUModdingLibrary.fileformats.world
{
    public class IGE : DFE
    {
        #region Constants
        /// <summary>
        /// Pattern for file name (to redefine)
        /// </summary>
        public new static readonly string FILENAME_PATTERN = string.Format(String2.REGEX_PATTERN_EXTENSION, "IGE");
        #endregion

        #region Properties
        /// <summary>
        /// Checksum to protect this file when using in dinners
        /// </summary>
        public ulong Checksum { get; set; }
        #endregion

        #region Overrides of TduFile
        /// <summary>
        /// Reads file data.
        /// </summary>
        protected override sealed void _ReadData()
        {
            using (BinaryReader reader = new BinaryReader(new FileStream(_FileName, FileMode.Open, FileAccess.Read)))
            {
                // Header
                _HeaderData = _ReadHeader(reader);

                // Parameters
                // Parameter count @ 0x18
                int paramCount = _HeaderData[_HEADER_PARAM_COUNT_OFFSET];

                _Parameters.Clear();

                for (int i = 0; i < paramCount; i++)
                {
                    // Code : 4 chars
                    string code = new string(reader.ReadChars(4));
                    // Unknwown : 2 bytes
                    byte[] unknown = reader.ReadBytes(2);
                    // Zero : 1 byte
                    reader.ReadByte();
                    // Value length
                    byte valueLength = reader.ReadByte();
                    // Zeros : 4 bytes
                    reader.ReadBytes(4);
                    // Value
                    byte[] value = reader.ReadBytes(valueLength);

                    ParameterInfo newParam = new ParameterInfo { code = code, unknown = unknown, value = value };

                    _Parameters.Add(code, newParam);
                }

                // Waypoints
                _ReadTraceData(reader);

                // EVO_65: Properties              
                _SetProperties();
            }
        }
        #endregion
        
        /// <summary>
        /// Default constructor
        /// </summary>
        internal IGE() {}

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="dfeFileName">Name of file providing camera data</param>
        internal IGE(string dfeFileName)
        {
            _FileName = dfeFileName;

            _ReadData();
        }

        #region Protected methods
        /// <summary>
        /// Reads file header and returns parameter count
        /// </summary>
        protected new byte[] _ReadHeader(BinaryReader currentReader)
        {
            byte[] returnedData = new byte[0];

            if (currentReader != null)
            {
                // 8 first bytes are skipped (=checksum)
                Checksum = currentReader.ReadUInt64();

                // Header itself (= DFE one)
                returnedData = currentReader.ReadBytes(_HEADER_SIZE);
            }
                
            return returnedData;
        }
        #endregion
    }
}

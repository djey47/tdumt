using System;
using System.IO;
using DjeFramework1.Common.Support.Traces;

namespace TDUModdingLibrary.support.encryption
{
    /// <summary>
    /// C++ to C# conversion by Djey.
    /// Thanks to Kegetys for amazing C++ version.
    /// </summary>
    public class XTEAEncryption
    {
        /// <summary>
        /// Number of rounds each pair will be encrypted
        /// </summary>
        private const int _NUMROUNDS = 32;

        /// <summary>
        /// Delta value
        /// </summary>
        private const uint _DELTA = 0x9E3779B9;

        /// <summary>
        /// XTEA key for TDU files (not valid for savegames?)
        /// </summary>
        private static readonly uint[] _KEY = {
	        0x4FE23C4A,
	        0x80BAC211,
	        0x6917BD3A,
	        0xF0528EBD,
        };

        #region Private methods
        /// <summary>
        /// XTEA decipher by David Wheeler and Roger Needham
        /// </summary>
        /// <param name="result"></param>
        /// <param name="values"></param>
        /// <param name="key"></param>
        private static void _Decipher(uint[] result, uint[] values, uint[] key)
        {
            uint v0 = values[0], v1 = values[1];
            uint sum = unchecked(_DELTA * _NUMROUNDS);

            for(int i=0; i<_NUMROUNDS; i++)
            {
                v1 -= (((v0 << 4) ^ (v0 >> 5)) + v0) ^ (sum + key[(sum>>11) & 3]);
                sum -= _DELTA;
                v0 -= (((v1 << 4) ^ (v1 >> 5)) + v1) ^ (sum + key[sum & 3]);
            }

	        result[0]=v0; result[1]=v1;
        }

        /// <summary>
        /// XTEA cipher by David Wheeler and Roger Needham
        /// </summary>
        /// <param name="result"></param>
        /// <param name="values"></param>
        /// <param name="key"></param>
        private static void _Encipher(uint[] result, uint[] values, uint[] key)
        {
            uint v0=values[0], v1=values[1];
            uint sum=0;

            for(int i=0; i<_NUMROUNDS; i++)
            {
                v0 += ((v1 << 4 ^ v1 >> 5) + v1) ^ (sum + key[sum & 3]);
                sum += _DELTA;
                v1 += ((v0 << 4 ^ v0 >> 5) + v0) ^ (sum + key[sum>>11 & 3]);
            }

            result[0]=v0; result[1]=v1;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Decrypts iname file to oname file
        /// </summary>
        /// <param name="iname"></param>
        /// <param name="oname"></param>
        /// <returns></returns>
        public static void Decrypt(string iname, string oname)
        {
            if (string.IsNullOrEmpty(iname) || string.IsNullOrEmpty(oname))
                return;

            FileStream inputFile = null;
            FileStream outputFile = null;

            try
            {
                inputFile = new FileStream(iname, FileMode.Open, FileAccess.Read);
                outputFile = new FileStream(oname, FileMode.Create, FileAccess.Write);

                BinaryReader reader = new BinaryReader(inputFile);
                BinaryWriter writer = new BinaryWriter(outputFile);

                while (reader.BaseStream.Position < reader.BaseStream.Length - 8)
                {
                    uint[] res = new uint[2];
                    uint[] ddata = { reader.ReadUInt32(), reader.ReadUInt32() };
                    uint[] ddataToDecipher = { reader.ReadUInt32(), reader.ReadUInt32() };
                    // Go back to next 8 bytes to read
                    reader.BaseStream.Seek(-8, SeekOrigin.Current);

                    _Decipher(res, ddataToDecipher, _KEY);
                    res[0] ^= ddata[0]; // XOR XTEA decipher result A with file data+0
		            res[1] ^= ddata[1]; // XOR XTEA decipher result B with file data+4

                    writer.Write(res[0]);
                    writer.Write(res[1]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error when decrypting file: " + iname, ex);
            }
            finally
            {
                if (inputFile != null)
                    inputFile.Close();
                if (outputFile != null)
                    outputFile.Close();
            }
        }

        /// <summary>
        /// Encrypts iname file to oname file
        /// </summary>
        /// <param name="iname"></param>
        /// <param name="oname"></param>
        public static void Encrypt(string iname, string oname)
        {
            if (string.IsNullOrEmpty(iname) || string.IsNullOrEmpty(oname))
                return;

            FileStream inputFile = null;
            FileStream outputFile = null;

            try
            {
                long fileSize = new FileInfo(iname).Length;

                inputFile = new FileStream(iname, FileMode.Open, FileAccess.Read);
                outputFile = new FileStream(oname, FileMode.Create, FileAccess.Write);

                BinaryReader reader = new BinaryReader(inputFile);
                BinaryWriter writer = new BinaryWriter(outputFile);

                // BUG_62: ensure array size is always a multiple of 2 (processing by 2 groups of 4 bytes)
                uint size = (uint) (2 + fileSize/4);

                if (size % 2 != 0)
                    size += (size%2);

                uint[] ddata = new uint[size];

                // Preparing data buffer
                ddata[0] = 0x27AD2123;
                ddata[1] = 0x128675FA;

                try
                {
                    for (int arrayIndex = 0; arrayIndex < ddata.Length - 2; arrayIndex++)
                    {
                        int remainingCount = (int) (reader.BaseStream.Length - reader.BaseStream.Position);

                        if (remainingCount > 0  && remainingCount < 4)
                        {
                            // Reading remaining bytes
                            for (int j = 0; j < remainingCount; j++)
                                ddata[arrayIndex + 2] = (uint) (reader.ReadByte() * Math.Pow(256,j) + ddata[arrayIndex + 2]); 
                        }
                        else
                            ddata[arrayIndex + 2] = reader.ReadUInt32();
                    }
                }
                catch (EndOfStreamException)
                {
                    Log.Info("End of source file. Last read value= " + ddata[ddata.Length - 1]);
                }

                reader.Close();
                
                // Encrypt data & write to output
                writer.Write(ddata[0]);
                writer.Write(ddata[1]);

                for (int i = 0; i < fileSize/4; i+=2)
                {
                    uint[] res = new uint[2];
                    uint[] ddataToEncipher = { ddata[i + 2], ddata[i + 3] };

                    res[0] = ddata[i] ^ ddata[i+2];
                    res[1] = ddata[i+1] ^ ddata[i+3];
                    _Encipher(ddataToEncipher, res, _KEY);
                    ddata[i + 2] = ddataToEncipher[0];
                    ddata[i + 3] = ddataToEncipher[1];

                    writer.Write(ddata[i + 2]);
                    writer.Write(ddata[i + 3]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error when encrypting file: " + iname, ex);
            }
            finally
            {
                if (inputFile != null)
                    inputFile.Close();
                if (outputFile != null)
                    outputFile.Close();
            }
        }
        #endregion
    }
}
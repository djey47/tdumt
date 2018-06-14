using System;
using System.IO;
using System.Runtime.InteropServices;
using DjeFramework1.Common.Support.Meta;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.fileformats.interfaces;

namespace TDUModdingLibrary.fileformats.graphics
{
    /// <summary>
    /// Infos pour le format DDS
    /// </summary>
    public class DDS : TduFile, ITextureFile
    {
        #region Constantes
        /// <summary>
        /// Pattern for file name (to redefine)
        /// </summary>
        public new static readonly string FILENAME_PATTERN = string.Format(String2.REGEX_PATTERN_EXTENSION, "DDS");

        /// <summary>
        /// Taille de l'entête
        /// </summary>
        public const long HEADER_SIZE = 0x80L;

        /// <summary>
        /// FOURCC : format DXT1
        /// </summary>
        public const uint FORMAT_FOURCC_DXT1 = 0x31545844;

        /// <summary>
        /// FOURCC : format DXT3
        /// </summary>
        public const uint FORMAT_FOURCC_DXT3 = 0x33545844;

        /// <summary>
        /// FOURCC : format DXT5
        /// </summary>
        public const uint FORMAT_FOURCC_DXT5 = 0x35545844;

        /// <summary>
        /// FOURCC : unknown format - used for ARGB8 data
        /// </summary>
        public const uint FORMAT_FOURCC_UNKNOWN = 0;
        #endregion

        #region Structures
        /// <summary>
        /// En-tête de fichier DDS
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TextureHeader
        {
            public uint dwMagic;
            public DDSURFACEDESC2 ddsd;
        }

        /// <summary>
        /// Surface DDS
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DDSURFACEDESC2
        {
            public uint dwSize;
            public uint dwFlags;
            public uint dwHeight;
            public uint dwWidth;
            public uint dwPitchOrLinearSize;
            public uint dwDepth;
            public uint dwMipMapCount;
            public byte[] dwReserved1;
            public DDPIXELFORMAT ddpfPixelFormat;
            public DDCAPS2 ddsCaps;
            public uint dwReserved2;
        }

        /// <summary>
        /// Format de pixels
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DDPIXELFORMAT
        {
            public uint dwSize;
            public uint dwFlags;
            public uint dwFourCC;
            public uint dwRGBBitCount;
            public uint dwRBitMask;
            public uint dwGBitMask;
            public uint dwBBitMask;
            public uint dwRGBAlphaBitMask;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DDCAPS2
        {
            public uint dwCaps1;
            public uint dwCaps2;
            public byte[] Reserved;
        }

        #endregion

        #region Properties
        /// <summary>
        /// En-tête de la texture sous forme de structure
        /// </summary>
        public ValueType Header
        {
            get
            {
                return _Header;
            }
            set
            {
                _Header = (TextureHeader) value;
            }
        }
        private TextureHeader _Header;

        /// <summary>
        /// Données de l'en-tête sous forme de tableau d'octets (lecture seule)
        /// </summary>
        public byte[] HeaderData
        {
            get
            {
                return _ConvertHeaderToBytes();
            }
        }

        /// <summary>
        /// Données de l'image sous forme de tableau d'octets
        /// </summary>
        public byte[] ImageData
        {
            get
            {
                return _ImageData;
            }
            set
            {
                _ImageData = value;
            }
        }
        private byte[] _ImageData;
        #endregion

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal DDS() { }

        /// <summary>
        /// Constructeur paramétré. Charge le contenu du fichier.
        /// </summary>
        /// <param name="ddsFileName">Nom du fichier</param>
        internal DDS(string ddsFileName)
        {
            _FileName = ddsFileName;
            _ReadData();
        }

        #region TDUFile implementation
        /// <summary>
        /// Lecture du contenu du fichier
        /// </summary>
        protected override void _ReadData()
        {
            FileStream input = null; 
            BinaryReader reader = null; 

            try
            {
                input = new FileStream(_FileName, FileMode.Open, FileAccess.Read);
                reader = new BinaryReader(input);

                // En-tête
                DDSURFACEDESC2 resultSurface;
                DDPIXELFORMAT resultPixelFormat;
                DDCAPS2 resultCaps;

                _Header.dwMagic = reader.ReadUInt32();

                resultSurface.dwSize = reader.ReadUInt32();
                resultSurface.dwFlags = reader.ReadUInt32();
                resultSurface.dwHeight = reader.ReadUInt32();
                resultSurface.dwWidth = reader.ReadUInt32();
                resultSurface.dwPitchOrLinearSize = reader.ReadUInt32();
                resultSurface.dwDepth = reader.ReadUInt32();
                resultSurface.dwMipMapCount = reader.ReadUInt32();
                resultSurface.dwReserved1 = reader.ReadBytes(44);

                resultPixelFormat.dwSize = reader.ReadUInt32();
                resultPixelFormat.dwFlags = reader.ReadUInt32();
                resultPixelFormat.dwFourCC = reader.ReadUInt32();
                resultPixelFormat.dwRGBBitCount = reader.ReadUInt32();
                resultPixelFormat.dwRBitMask = reader.ReadUInt32();
                resultPixelFormat.dwGBitMask = reader.ReadUInt32();
                resultPixelFormat.dwBBitMask = reader.ReadUInt32();
                resultPixelFormat.dwRGBAlphaBitMask = reader.ReadUInt32();

                resultCaps.dwCaps1 = reader.ReadUInt32();
                resultCaps.dwCaps2 = reader.ReadUInt32();
                resultCaps.Reserved = reader.ReadBytes(8);

                resultSurface.dwReserved2 = reader.ReadUInt32();

                resultSurface.ddsCaps = resultCaps;
                resultSurface.ddpfPixelFormat = resultPixelFormat;
                _Header.ddsd = resultSurface;

                // Données d'image
                FileInfo fi = new FileInfo(_FileName);

                _ImageData = reader.ReadBytes((int) (fi.Length - HEADER_SIZE));

                // EVO_65: property support
                Property.ComputeValueDelegate dimensionsDelegate = delegate { return resultSurface.dwWidth + "x" + resultSurface.dwHeight; };
                Property.ComputeValueDelegate mipmapDelegate = delegate { return resultSurface.dwMipMapCount.ToString(); };
                Property.ComputeValueDelegate formatDelegate = delegate { return GetFormatName(resultPixelFormat.dwFourCC); };
                Property.ComputeValueDelegate bitCountDelegate = delegate { return resultPixelFormat.dwRGBBitCount.ToString(); };

                _Properties.Add(new Property("Dimensions", "Texture", dimensionsDelegate));
                _Properties.Add(new Property("Mipmap count", "Texture", mipmapDelegate));
                _Properties.Add(new Property("Format", "DDS", formatDelegate));
                _Properties.Add(new Property("RGB bit count", "DDS", bitCountDelegate));
            }
            catch (Exception ex)
            {
                Exception2.PrintStackTrace(ex);
                throw;
            }
            finally
            {
                // Fermeture
                if (reader != null)
                    reader.Close();
                if (input != null)
                    input.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Save()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Méthodes privées
        /// <summary>
        /// Convertit l'en-tête DDS en tableau d'octets
        /// </summary>
        /// <returns></returns>
        protected byte[] _ConvertHeaderToBytes()
        {
            byte[] result = new byte[HEADER_SIZE];
            
            MemoryStream memStream = new MemoryStream(result);
            BinaryWriter writer = new BinaryWriter(memStream);

            try
            {
                writer.Write(_Header.dwMagic);
                writer.Write(_Header.ddsd.dwSize);
                writer.Write(_Header.ddsd.dwFlags);
                writer.Write(_Header.ddsd.dwHeight);
                writer.Write(_Header.ddsd.dwWidth);
                writer.Write(_Header.ddsd.dwPitchOrLinearSize);
                writer.Write(_Header.ddsd.dwDepth);
                writer.Write(_Header.ddsd.dwMipMapCount);
                writer.Write(_Header.ddsd.dwReserved1);
                writer.Write(_Header.ddsd.ddpfPixelFormat.dwSize);
                writer.Write(_Header.ddsd.ddpfPixelFormat.dwFlags);
                writer.Write(_Header.ddsd.ddpfPixelFormat.dwFourCC);
                writer.Write(_Header.ddsd.ddpfPixelFormat.dwRGBBitCount);
                writer.Write(_Header.ddsd.ddpfPixelFormat.dwRBitMask);
                writer.Write(_Header.ddsd.ddpfPixelFormat.dwGBitMask);
                writer.Write(_Header.ddsd.ddpfPixelFormat.dwBBitMask);
                writer.Write(_Header.ddsd.ddpfPixelFormat.dwRGBAlphaBitMask);
                writer.Write(_Header.ddsd.ddsCaps.dwCaps1);
                writer.Write(_Header.ddsd.ddsCaps.dwCaps2);
                writer.Write(_Header.ddsd.ddsCaps.Reserved);
                writer.Write(_Header.ddsd.dwReserved2);
            }
            catch (Exception ex)
            {
                Exception2.PrintStackTrace(ex);
                throw;
            }
            finally
            {
                memStream.Close();
                writer.Close();
            }

            return result;
        }
        #endregion

        #region ITextureFile Members
        /// <summary>
        /// Returns texture format name
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string GetFormatName(uint format)
        {
            switch (format)
            {
                case FORMAT_FOURCC_DXT1:
                    return "DXT1";
                case FORMAT_FOURCC_DXT3:
                    return "DXT3";
                case FORMAT_FOURCC_DXT5:
                    return "DXT5";
                case FORMAT_FOURCC_UNKNOWN:
                    return "ARGB8";
                default:
                    return "??";
            }
        }
        #endregion
    }
}
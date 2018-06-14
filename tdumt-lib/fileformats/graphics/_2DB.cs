using System;
using System.IO;
using DjeFramework1.Common.Support.Meta;
using DjeFramework1.Common.Types;
using DjeFramework1.Common.Types.Collections;
using TDUModdingLibrary.fileformats.interfaces;
using TDUModdingLibrary.support;

namespace TDUModdingLibrary.fileformats.graphics
{
    /// <summary>
    /// Représente un fichier de texture 2DB. Moteur 3D EDEN GAMES. Les données d'image sont au format DDS, seul l'en-tête change pour prendre en compte les spécificité du moteur.
    /// </summary>
    public class _2DB : TduFile, ITextureFile
    {
        #region Constantes
        /// <summary>
        /// Pattern for file name (to redefine)
        /// </summary>
        public new static readonly string FILENAME_PATTERN = string.Format(String2.REGEX_PATTERN_EXTENSION, "2DB");

        /// <summary>
        /// Pattern for file name (to redefine)
        /// </summary>
        public static readonly string FILENAME_OLD_PATTERN = string.Format(String2.REGEX_PATTERN_EXTENSION, "OLDB");

        /// <summary>
        /// Taille de l'entête
        /// </summary>
        internal const long HEADER_SIZE = 0x50L;

        /// <summary>
        ///  Permet de finaliser le fichier : REAL.....
        /// </summary>
        internal static readonly byte[] FINALIZATION_STRING = new byte[] { 
                                                                             0x52, 0x45, 0x41, 0x4C,
                                                                             0x0, 0x0, 0x0, 0x0,
                                                                             0x10, 0x0, 0x0, 0x0,
                                                                             0x10, 0x0, 0x0, 0x0
                                                                         };

        /// <summary>
        /// Identifiant 1 pour l'en tête
        /// </summary>
        internal const string ID1_STRING = ".2DB";

        /// <summary>
        /// Identifiant 2 pour l'en tête
        /// </summary>
        internal const string ID2_STRING = "BMAP";

        /// <summary>
        /// Identifiant du format basé sur DXT5
        /// </summary>
        internal const byte FORMAT_ID_B5 = 0x88;

        /// <summary>
        /// Identifiant du format basé sur DXT1
        /// </summary>
        internal const byte FORMAT_ID_B1 = 0x84;

        /// <summary>
        /// Identifiant du format basé sur DXT1 (variante)
        /// </summary>
        internal const byte FORMAT_ID_B1BIS = 0xc4;

        /// <summary>
        /// Identifier of ARGB8-based format
        /// </summary>
        internal const byte FORMAT_ID_BARGB8 = 0x90;

        /// <summary>
        /// Type for DXT1 format
        /// </summary>
        internal const uint TYPE_DXT1 = 51;

        /// <summary>
        /// Type for DXT5 format
        /// </summary>
        internal const uint TYPE_DXT5 = 55;

        /// <summary>
        /// Type for ARGB8 format
        /// </summary>
        internal const uint TYPE_ARGB8 = 29;
        #endregion

        #region Structures
        /// <summary>
        /// En-tête du fichier 2DB (80 octets). Données dans l'ordre de lecture.
        /// </summary>
        public struct TextureHeader
        {
            /// <summary>
            /// (?) Valeur à 2 
            /// </summary>
            public uint dwTwo;
            /// <summary>
            /// (?) Valeur à 0
            /// </summary>
            public uint dwZero1;
            /// <summary>
            /// Taille du fichier
            /// </summary>
            public uint dwSize;
            /// <summary>
            /// TAG ".2DB"
            /// </summary>
            public byte[] bID1;
            /// <summary>
            /// TAG "BMAP"
            /// </summary>
            public byte[] bID2;
            /// <summary>
            /// (?) Valeur à 0
            /// </summary>
            public uint dwZero2;
            /// <summary>
            /// Taille du fichier - 32 (va comprendre !)
            /// </summary>
            public uint dwSize2;
            /// <summary>
            /// Taille du fichier - 32
            /// </summary>
            public uint dwSize2Bis;
            /// <summary>
            /// Nom de la texture sur 8 caractères
            /// </summary>
            public byte[] strName;
            /// <summary>
            /// Dimension : largeur
            /// </summary>
            public short width;
            /// <summary>
            /// Dimension : hauteur
            /// </summary>
            public short height;
            /// <summary>
            /// (?) Valeur à 1
            /// </summary>
            public short one; 
            /// <summary>
            /// Nombre de mipmaps
            /// </summary>
            public byte bMipMapCount;
            /// <summary>
            /// Nombre de mipmaps, identique au précédent
            /// </summary>
            public byte bMipMapCountBis;
            /// <summary>
            /// Format texture : DXT5, DXT1, DXT1 bis ... (cf. FORMAT_XX)
            /// </summary>
            public byte bFormat;
            /// <summary>
            /// (?)
            /// </summary>
            public byte bUnk2;
            /// <summary>
            /// (?)
            /// </summary>
            public short unk3;
            /// <summary>
            /// (?)
            /// </summary>
            public uint dwUnk4;
            /// <summary>
            /// (?)
            /// </summary>
            public uint dwUnk5;
            /// <summary>
            /// Type image : "3", "7"... 0x1D for "RAW/BMP";
            /// </summary>
            public uint dwType;
            /// <summary>
            /// 0x2000, 0x800 or 0x400... looks like pitch/linear size
            /// </summary>
            public uint dwFlags;
            /// <summary>
            /// (?)
            /// </summary>
            public uint dwUnk6;
            /// <summary>
            /// Checksum ?
            /// </summary>
            public uint dwUnk7;
            /// <summary>
            /// (?) Valeur à 0
            /// </summary>
            public uint dwZero3;
        };
        #endregion

        #region Properties
        /// <summary>
        /// En-tête de la texture
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
        /// Constructeur par défaut.
        /// </summary>
        internal _2DB() { }

        /// <summary>
        /// Constructeur paramétré. Charge le contenu du fichier.
        /// </summary>
        /// <param name="_2DBFileName">nom du fichier</param>
        internal _2DB(string _2DBFileName)
        {
            _FileName = _2DBFileName;
            _ReadData();
        }

        #region TDUFile implementation
        /// <summary>
        /// Lit le contenu du fichier
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
                _Header.dwTwo = reader.ReadUInt32();
                _Header.dwZero1 = reader.ReadUInt32();
                _Header.dwSize = reader.ReadUInt32();
                _Header.bID1 = reader.ReadBytes(4);
                _Header.bID2 = reader.ReadBytes(4);
                _Header.dwZero2 = reader.ReadUInt32();
                _Header.dwSize2 = reader.ReadUInt32();
                _Header.dwSize2Bis = reader.ReadUInt32();
                _Header.strName = reader.ReadBytes(8);
                _Header.width = reader.ReadInt16();
                _Header.height = reader.ReadInt16();
                _Header.one = reader.ReadInt16();
                _Header.bMipMapCount = reader.ReadByte();
                _Header.bMipMapCountBis = reader.ReadByte();
                _Header.bFormat = reader.ReadByte();
                _Header.bUnk2 = reader.ReadByte();
                _Header.unk3 = reader.ReadInt16();
                _Header.dwUnk4 = reader.ReadUInt32();
                _Header.dwUnk5 = reader.ReadUInt32();
                _Header.dwType = reader.ReadUInt32();
                _Header.dwFlags = reader.ReadUInt32();
                _Header.dwUnk6 = reader.ReadUInt32();
                _Header.dwUnk7 = reader.ReadUInt32();
                _Header.dwZero3 = reader.ReadUInt32();

                // Données d'image
                FileInfo fi = new FileInfo(_FileName);

                _ImageData = reader.ReadBytes((int)(fi.Length - HEADER_SIZE));

                // EVO_65: property support
                Property.ComputeValueDelegate nameDelegate = delegate
                                                                 {
                                                                     string textureName =
                                                                         Array2.BytesToString(_Header.strName);

                                                                     return Tools.OutlineExtendedCharacters(textureName);
                                                                 };
                Property.ComputeValueDelegate dimensionsDelegate = delegate { return _Header.width + "x" + _Header.height; };
                Property.ComputeValueDelegate mipmapDelegate = delegate { return _Header.bMipMapCount.ToString(); };
                Property.ComputeValueDelegate formatDelegate = delegate { return GetFormatName(_Header.bFormat); };
                Property.ComputeValueDelegate typeDelegate = delegate { return _Header.dwType.ToString(); };
                Property.ComputeValueDelegate flagsDelegate = delegate { return _Header.dwFlags.ToString(); };
                Property.ComputeValueDelegate magicDelegate = delegate { return _Header.dwUnk6 + " - " + _Header.dwUnk7; };

                _Properties.Add(new Property("Texture name", "2DB", nameDelegate));
                _Properties.Add(new Property("Dimensions", "Texture", dimensionsDelegate));
                _Properties.Add(new Property("Mipmap count", "Texture", mipmapDelegate));
                _Properties.Add(new Property("Format", "2DB", formatDelegate));
                _Properties.Add(new Property("Type", "2DB", typeDelegate));
                _Properties.Add(new Property("Flags", "2DB", flagsDelegate));
                _Properties.Add(new Property("Magic", "2DB", magicDelegate));
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
        /// Convertit l'en-tête 2DB en tableau d'octets
        /// </summary>
        /// <returns></returns>
        protected byte[] _ConvertHeaderToBytes()
        {
            byte[] result = new byte[HEADER_SIZE];
            Stream memStream = new MemoryStream(result);
            BinaryWriter writer = new BinaryWriter(memStream);

            try
            {
                // Reconstitution de la structure dans le tableau d'octets
                writer.Write(_Header.dwTwo);
                writer.Write(_Header.dwZero1);
                writer.Write(_Header.dwSize);
                writer.Write(_Header.bID1);
                writer.Write(_Header.bID2);
                writer.Write(_Header.dwZero2);
                writer.Write(_Header.dwSize2);
                writer.Write(_Header.dwSize2Bis);
                writer.Write(_Header.strName);
                writer.Write(_Header.width);
                writer.Write(_Header.height);
                writer.Write(_Header.one);
                writer.Write(_Header.bMipMapCount);
                writer.Write(_Header.bMipMapCountBis);
                writer.Write(_Header.bFormat);
                writer.Write(_Header.bUnk2);
                writer.Write(_Header.unk3);
                writer.Write(_Header.dwUnk4);
                writer.Write(_Header.dwUnk5);
                writer.Write(_Header.dwType);
                writer.Write(_Header.dwFlags);
                writer.Write(_Header.dwUnk6);
                writer.Write(_Header.dwUnk7);
                writer.Write(_Header.dwZero3);
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
                case FORMAT_ID_B5:
                    return "DXT5-like";
                case FORMAT_ID_B1:
                case FORMAT_ID_B1BIS:
                    return "DXT1-like";
                case FORMAT_ID_BARGB8:
                    return "ARGB8-like";
                default:
                    return "??";
            }
        }
        #endregion
    }
}
using System;
using System.IO;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.graphics;
using TDUModdingLibrary.support;

namespace TDUModdingLibrary.converters
{
    /// <summary>
    /// Classe utilitaire réalisant les conversions de fichiers graphiques
    /// </summary>
    public static class GraphicsConverters
    {
        #region Constants
        /// <summary>
        /// Indicates original mipmap count must be kept
        /// </summary>
        public const int KEEP_ORIGINAL_MIPMAP_COUNT = -1;

        /// <summary>
        /// Indicates original texture name must be kept
        /// </summary>
        public const string KEEP_ORIGINAL_TEXTURE_NAME = null;
        #endregion

        #region Public methods
        /// <summary>
        /// Convertit un fichier DDS en fichier 2DB. Utilise l'en-tête du fichier 2DB d'origine.
        /// </summary>
        /// <param name="original2DBFile">ancien fichier 2DB</param>
        /// <param name="sourceDDSFile">fichier DDS à convertir</param>
        /// <param name="target2DBFile">nouveau fichier 2DB à créer</param>
        /// <param name="newTextureName">if not null, allows to override texture name</param>
        /// <param name="mipmapCount">if not -1, forces mipmap count</param>
        public static void DDSTo2DB(string original2DBFile, string sourceDDSFile, string target2DBFile, string newTextureName, int mipmapCount)
        {
            // EVO_37 : 1. Récupérer les dimensions de la texture
            DDS ddsFile = (DDS) TduFile.GetFile(sourceDDSFile);
            DDS.TextureHeader ddsHeader = (DDS.TextureHeader)ddsFile.Header;
            uint ddsWidth = ddsHeader.ddsd.dwWidth;
            uint ddsHeight = ddsHeader.ddsd.dwHeight;
            uint ddsMipmapCount = ddsHeader.ddsd.dwMipMapCount;

            // 2. Prendre l'en-tête du fichier 2DB original
            _2DB old2DBFile = (_2DB) TduFile.GetFile(original2DBFile);
            _2DB.TextureHeader old2DBHeader = (_2DB.TextureHeader)old2DBFile.Header;

            // 3.  Mettre à jour les dimensions et nombre de mipmap
            old2DBHeader.height = (short) ddsHeight;
            old2DBHeader.width = (short) ddsWidth;
            // BUG_58 : mipmap count in 2DB takes main texture into account (so always >= 1)

            // Mipmap count enforcement
            if (mipmapCount == KEEP_ORIGINAL_MIPMAP_COUNT)
                old2DBHeader.bMipMapCount = old2DBHeader.bMipMapCountBis = (byte)(ddsMipmapCount + 1);
            else
                old2DBHeader.bMipMapCount = old2DBHeader.bMipMapCountBis = (byte) mipmapCount;

            // 4. Calcul de bourrage / découpage éventuels
            long fileSize = _2DB.HEADER_SIZE + (ddsFile.Size - DDS.HEADER_SIZE) + _2DB.FINALIZATION_STRING.LongLength;

            // 5. Création du nouveau fichier et assemblage
            using (BinaryWriter writer = new BinaryWriter(new FileStream(target2DBFile, FileMode.Create, FileAccess.Write)))
            {
                // Mettre à jour la taille du fichier dans l'entete
                old2DBHeader.dwSize = (uint) fileSize;
                old2DBHeader.dwSize2 = old2DBHeader.dwSize2Bis = (uint) (fileSize - 32);

                // Override texture name ?
                if (newTextureName != null)
                    old2DBHeader.strName = String2.ToByteArray(Tools.NormalizeName(newTextureName));

                // Ecriture de l'en-tête
                old2DBFile.Header = old2DBHeader;
                writer.Write(old2DBFile.HeaderData);

                // Data writing
                byte[] imageData = ddsFile.ImageData;

                writer.Write(imageData);

                // Finalization: REAL  (??)
                writer.Write(_2DB.FINALIZATION_STRING);
            }
        }

        /// <summary>
        /// Convertit un fichier DDS en fichier 2DB (EVO_37). Génère un nouvel en-tête.
        /// </summary>
        /// <param name="sourceDDSFile">fichier DDS à convertir</param>
        /// <param name="target2DBFile">nouveau fichier 2DB à créer</param>
        /// <param name="newTextureName">if not null, allows to override texture name</param>
        /// <param name="mipmapCount">if not -1, forces mipmap count</param>
        public static void DDSTo2DB(string sourceDDSFile, string target2DBFile, string newTextureName, int mipmapCount)
        {
            if (sourceDDSFile != null && target2DBFile != null)
            {
                DDS ddsFile = (DDS) TduFile.GetFile(sourceDDSFile);
                DDS.TextureHeader ddsHeader = (DDS.TextureHeader) ddsFile.Header;

                // Construction d'un fichier 2DB flambant neuf
                FileInfo fi = new FileInfo(target2DBFile);
                _2DB new2DBFile = (_2DB) TduFile.GetFile(target2DBFile);
                _2DB.TextureHeader newHeader = new _2DB.TextureHeader();
                string picName;

                // Override texture name ?
                if (newTextureName != null)
                    picName = Tools.NormalizeName(newTextureName);
                else
                    picName = Tools.NormalizeName(fi.Name.ToUpper());

                // Valeurs connues
                newHeader.bID1 = String2.ToByteArray(_2DB.ID1_STRING);
                newHeader.bID2 = String2.ToByteArray(_2DB.ID2_STRING);
                newHeader.dwTwo = 2;
                newHeader.dwZero1 = newHeader.dwZero2 = newHeader.dwZero3 = 0;
                newHeader.one = 1;
                newHeader.height = (short) ddsHeader.ddsd.dwHeight;
                newHeader.width = (short) ddsHeader.ddsd.dwWidth;
                newHeader.strName = String2.ToByteArray(picName);

                // BUG_58 : mipmap count in 2DB takes main texture into account (so always >= 1)
                uint ddsMipmapCount = ddsHeader.ddsd.dwMipMapCount;

                // Mipmap count enforcement
                if (mipmapCount == KEEP_ORIGINAL_MIPMAP_COUNT)
                    newHeader.bMipMapCount = newHeader.bMipMapCountBis = (byte)(ddsMipmapCount + 1);
                else
                    newHeader.bMipMapCount = newHeader.bMipMapCountBis = (byte)mipmapCount;

                // Format & type (linked ??)
                byte format;
                uint type;

                switch (ddsHeader.ddsd.ddpfPixelFormat.dwFourCC)
                {
                    case DDS.FORMAT_FOURCC_DXT1:
                        format = _2DB.FORMAT_ID_B1;
                        type = _2DB.TYPE_DXT1;
                        break;
                    case DDS.FORMAT_FOURCC_DXT5:
                        format = _2DB.FORMAT_ID_B5;
                        type = _2DB.TYPE_DXT5;
                        break;
                    case DDS.FORMAT_FOURCC_UNKNOWN:
                        format = _2DB.FORMAT_ID_BARGB8;
                        type = _2DB.TYPE_ARGB8;
                        break;
                    default:
                        // Log warning
                        Log.Warning("Unsupported texture format: '" + ddsHeader.ddsd.ddpfPixelFormat.dwFourCC +
                                          "'. Treated as DXT5.");
                        // DXT5
                        format = _2DB.FORMAT_ID_B5;
                        type = _2DB.TYPE_DXT5;
                        break;
                }
                newHeader.bFormat = format;
                newHeader.dwType = type;

                // TODO A découvrir... et initialiser plus tard
                newHeader.dwFlags = 512;
                newHeader.dwUnk6 = 0;
                newHeader.dwUnk7 = 0;
                newHeader.bUnk2 = 0;
                newHeader.unk3 = 0;
                newHeader.dwUnk4 = 0;

                // Derniers calculs : taille du fichier
                uint _2dbSize = (uint) (_2DB.HEADER_SIZE + ddsFile.ImageData.Length + _2DB.FINALIZATION_STRING.Length);

                newHeader.dwSize = _2dbSize;
                newHeader.dwSize2 = newHeader.dwSize2Bis = _2dbSize - 32;

                // Ecriture des sections
                try
                {
                    using (BinaryWriter writer = new BinaryWriter(new FileStream(target2DBFile, FileMode.Create, FileAccess.Write)))
                    {
                        // 1. En-tête
                        new2DBFile.Header = newHeader;
                        writer.Write(new2DBFile.HeaderData);

                        // 2. Données d'image
                        writer.Write(ddsFile.ImageData);

                        // Finalisation
                        writer.Write(_2DB.FINALIZATION_STRING);
                    }
                }
                catch (IOException ioe)
                {
                    Exception2.PrintStackTrace(ioe);
                    throw;
                }
            }
        }

        /// <summary>
        /// Convertit un fichier 2DB en fichier DDS.
        /// </summary>
        /// <param name="source2DBFile">fichier 2DB à convertir</param>
        /// <param name="targetDDSFile">nouveau fichier DDS à créer</param>
        public static void _2DBToDDS(string source2DBFile, string targetDDSFile)
        {
            // 1.8.0: new method
            _2DB original2DBFile = TduFile.GetFile(source2DBFile) as _2DB;

            if (original2DBFile == null)
                throw new Exception("Invalid source file: " + source2DBFile);

            if (!original2DBFile.Exists)
                throw new FileNotFoundException("", source2DBFile);

            // Reading into header
            _2DB.TextureHeader origHeader = (_2DB.TextureHeader) original2DBFile.Header;
            ushort imageWidth = (ushort) origHeader.width;
            ushort imageHeight = (ushort) origHeader.height;
            byte mipmapCount = origHeader.bMipMapCount;
            byte imageFormat = origHeader.bFormat;

            // Reading image data into buffer
            byte[] imageDataBuffer = original2DBFile.ImageData;

            // Conversion en-tête 2DB vers DDS
            DDS.TextureHeader sHeader = new DDS.TextureHeader
                                            {
                                                dwMagic = 0x20534444,
                                                ddsd =
                                                    {
                                                        dwSize = 0x7c,
                                                        dwFlags = 0xfeb0,
                                                        dwHeight = imageHeight,
                                                        dwWidth = imageWidth
                                                    }
                                            };

            switch (imageFormat)
            {
                case _2DB.FORMAT_ID_B5:
                    sHeader.ddsd.dwFlags = 0xa1007;
                    sHeader.ddsd.dwPitchOrLinearSize = imageWidth * (uint)imageHeight;
                    sHeader.ddsd.ddpfPixelFormat.dwFourCC = DDS.FORMAT_FOURCC_DXT5;
                    sHeader.ddsd.ddpfPixelFormat.dwFlags = 4;
                    sHeader.ddsd.ddpfPixelFormat.dwRBitMask = 0;
                    sHeader.ddsd.ddpfPixelFormat.dwGBitMask = 0;
                    sHeader.ddsd.ddpfPixelFormat.dwBBitMask = 0;
                    sHeader.ddsd.ddpfPixelFormat.dwRGBAlphaBitMask = 0;
                    sHeader.ddsd.ddpfPixelFormat.dwRGBBitCount = 0;
                    break;

                case _2DB.FORMAT_ID_B1:
                    sHeader.ddsd.dwFlags = 0xa1007;
                    sHeader.ddsd.dwPitchOrLinearSize = (uint) Math.Round(((double) (imageWidth * imageHeight)) / 2);
                    sHeader.ddsd.ddpfPixelFormat.dwFourCC = DDS.FORMAT_FOURCC_DXT1;
                    sHeader.ddsd.ddpfPixelFormat.dwFlags = 4;
                    sHeader.ddsd.ddpfPixelFormat.dwRBitMask = 0;
                    sHeader.ddsd.ddpfPixelFormat.dwGBitMask = 0;
                    sHeader.ddsd.ddpfPixelFormat.dwBBitMask = 0;
                    sHeader.ddsd.ddpfPixelFormat.dwRGBAlphaBitMask = 0;
                    sHeader.ddsd.ddpfPixelFormat.dwRGBBitCount = 0;
                    break;

                case _2DB.FORMAT_ID_B1BIS:
                    sHeader.ddsd.dwFlags = 0xa1007;
                    sHeader.ddsd.dwPitchOrLinearSize = (uint) Math.Round(((double) (imageWidth * imageHeight)) / 2);
                    sHeader.ddsd.ddpfPixelFormat.dwFourCC = DDS.FORMAT_FOURCC_DXT1;
                    sHeader.ddsd.ddpfPixelFormat.dwFlags = 4;
                    sHeader.ddsd.ddpfPixelFormat.dwRBitMask = 0;
                    sHeader.ddsd.ddpfPixelFormat.dwGBitMask = 0;
                    sHeader.ddsd.ddpfPixelFormat.dwBBitMask = 0;
                    sHeader.ddsd.ddpfPixelFormat.dwRGBAlphaBitMask = 0;
                    sHeader.ddsd.ddpfPixelFormat.dwRGBBitCount = 0;
                    break;

                case _2DB.FORMAT_ID_BARGB8:
                    sHeader.ddsd.dwFlags = 0x81007;
                    sHeader.ddsd.dwPitchOrLinearSize = (uint) ((imageWidth * imageHeight) * 4L);
                    sHeader.ddsd.ddpfPixelFormat.dwFourCC = DDS.FORMAT_FOURCC_UNKNOWN;
                    sHeader.ddsd.ddpfPixelFormat.dwFlags = 0x41;
                    sHeader.ddsd.ddpfPixelFormat.dwRBitMask = 0xff0000;
                    sHeader.ddsd.ddpfPixelFormat.dwGBitMask = 0xff00;
                    sHeader.ddsd.ddpfPixelFormat.dwBBitMask = 0xff;
                    sHeader.ddsd.ddpfPixelFormat.dwRGBAlphaBitMask = 0xff000000;
                    sHeader.ddsd.ddpfPixelFormat.dwRGBBitCount = 0x20;
                    break;
            }
            sHeader.ddsd.dwDepth = 0;
            // BUG_58: DDS mipmap count does not take the main texture into account
            if (mipmapCount < 1)
                mipmapCount = 1;

            sHeader.ddsd.dwMipMapCount = (uint) (mipmapCount - 1);
            sHeader.ddsd.dwReserved1 = new byte[] { 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
             };
            sHeader.ddsd.ddpfPixelFormat.dwSize = 0x20;
            
            if (mipmapCount == 0L)
                sHeader.ddsd.ddsCaps.dwCaps1 = 0x1000;
            else
                sHeader.ddsd.ddsCaps.dwCaps1 = 0x401008;
            
            // ??
            //sHeader.ddsd.ddsCaps.dwCaps1 = 0;

            sHeader.ddsd.dwReserved2 = 0;
            sHeader.ddsd.ddsCaps.Reserved = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

            // Création du fichier DDS
            using (BinaryWriter writer = new BinaryWriter(new FileStream(targetDDSFile, FileMode.Create, FileAccess.Write)))
            {
                // Ecriture nouvel en-tête
                writer.Write(sHeader.dwMagic);
                writer.Write(sHeader.ddsd.dwSize);
                writer.Write(sHeader.ddsd.dwFlags);
                writer.Write(sHeader.ddsd.dwHeight);
                writer.Write(sHeader.ddsd.dwWidth);
                writer.Write(sHeader.ddsd.dwPitchOrLinearSize);
                writer.Write(sHeader.ddsd.dwDepth);
                writer.Write(sHeader.ddsd.dwMipMapCount);
                writer.Write(sHeader.ddsd.dwReserved1);
                writer.Write(sHeader.ddsd.ddpfPixelFormat.dwSize);
                writer.Write(sHeader.ddsd.ddpfPixelFormat.dwFlags);
                writer.Write(sHeader.ddsd.ddpfPixelFormat.dwFourCC);
                writer.Write(sHeader.ddsd.ddpfPixelFormat.dwRGBBitCount);
                writer.Write(sHeader.ddsd.ddpfPixelFormat.dwRBitMask);
                writer.Write(sHeader.ddsd.ddpfPixelFormat.dwGBitMask);
                writer.Write(sHeader.ddsd.ddpfPixelFormat.dwBBitMask);
                writer.Write(sHeader.ddsd.ddpfPixelFormat.dwRGBAlphaBitMask);
                writer.Write(sHeader.ddsd.ddsCaps.dwCaps1);
                writer.Write(sHeader.ddsd.ddsCaps.dwCaps2);
                writer.Write(sHeader.ddsd.ddsCaps.Reserved);
                writer.Write(sHeader.ddsd.dwReserved2);

                // Recopie de la section de données
                writer.Write(imageDataBuffer);
            }
        }
        #endregion

        #region Méthodes privées
        #endregion
    }
}
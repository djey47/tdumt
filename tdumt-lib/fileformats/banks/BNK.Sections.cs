using System;
using System.IO;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.support;

namespace TDUModdingLibrary.fileformats.banks
{
    /// <summary>
    /// BNK format - Sections handling
    /// </summary>
    partial class BNK
    {
        #region Types
        /// <summary>
        /// Types de sections dans le BNK
        /// </summary>
        internal enum SectionType
        {
            None = 0,
            Header = 1,
            FileSize = 2,
            TypeMapping = 3,
            FileName = 4,
            FileOrder = 5,
            Unknown = 6,
            FileData = 7
        };

        /// <summary>
        /// Describes a section in BNK file;
        /// a BNK file contains 5 to 6 sections
        /// </summary>
        private class Section
        {
            #region Properties
            /// <summary>
            /// Returns true if current section has no data
            /// </summary>
            internal bool IsEmpty
            {
                get { return (usableSize == 0); }
            }

            /// <summary>
            /// Calculates and returns current checksum
            /// </summary>
            internal int Checksum
            {
                get
                {
                    int returnedChecksum = 0;

                    if (data != null && sectionType != SectionType.FileData)
                        returnedChecksum = (int)Tools.GetChecksum(data, true);

                    return returnedChecksum;
                }
            }
            #endregion

            #region Members
            // Infos déterminées
            /// <summary>
            /// Le type de section
            /// </summary>
            internal readonly SectionType sectionType;

            /// <summary>
            /// L'adresse de départ
            /// </summary>
            internal uint address;

            /// <summary>
            /// La taille du bourrage
            /// </summary>
            internal uint paddingSize;

            // Infos lues
            /// <summary>
            /// La taille des données utiles
            /// </summary>
            internal uint usableSize;

            /// <summary>
            /// Data in this section (not including header)
            /// </summary>
            internal byte[] data;

            /// <summary>
            /// Is this section present in current BNK ?
            /// </summary>
            internal bool isPresent = true;
            #endregion

            /// <summary>
            /// Parametrized constructor
            /// </summary>
            /// <param name="type"></param>
            internal Section(SectionType type)
            {
                sectionType = type;
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Initializes section list
        /// </summary>
        private void _CreateSections()
        {
            _SectionList.Clear();

            Section headerSection = new Section(SectionType.Header);
            Section fileSizeSection = new Section(SectionType.FileSize);
            Section typeMappingSection = new Section(SectionType.TypeMapping);
            Section fileNameSection = new Section(SectionType.FileName);
            Section fileOrderSection = new Section(SectionType.FileOrder);
            Section fileDataSection = new Section(SectionType.FileData);

            _SectionList.Add(headerSection);
            _SectionList.Add(fileSizeSection);
            _SectionList.Add(typeMappingSection);
            _SectionList.Add(fileNameSection);
            _SectionList.Add(fileOrderSection);
            _SectionList.Add(fileDataSection);
        }

        /// <summary>
        /// Retourne la section de type spécifié
        /// </summary>
        private Section _GetSection(SectionType type)
        {
            Section returnedSection = new Section(SectionType.None);

            foreach (Section anotherSection in _SectionList)
            {
                if (anotherSection.sectionType == type)
                {
                    returnedSection = anotherSection;
                    break;
                }
            }

            return returnedSection;
        }

        /// <summary>
        /// Renvoie la taille du bourrage selon la taille de données spécifiée 
        /// </summary>
        /// <param name="sampleLength"></param>
        /// <param name="blockSize"></param>
        /// <returns></returns>
        private static uint _GetPaddingLength(uint sampleLength, uint blockSize)
        {
            uint returnedLength = sampleLength % 16;

            if (returnedLength == 4)
                return 12;

            return returnedLength;

            //uint returnedLength = 0;
            //uint diff = sampleLength % blockSize;

            //if ((sampleLength + diff) % blockSize == 0)
            //    returnedLength += diff;
            //else
            //    returnedLength += (blockSize - diff);

            //return returnedLength;
        }

        private static uint _GetSectionPaddingLength(uint sampleLength, uint blockSize)
        {
            return sampleLength % 16;
        }

        /// <summary>
        /// Reads and completes section contents
        /// </summary>
        /// <param name="sectionType"></param>
        /// <param name="bnkReader"></param>
        private void _ReadSection(SectionType sectionType, BinaryReader bnkReader)
        {
            if (bnkReader == null) return;

            Section section = _GetSection(sectionType);

            bnkReader.BaseStream.Seek(section.address, SeekOrigin.Begin);

            // FileData section has no length info
            if (SectionType.FileData == section.sectionType)
                section.usableSize = (Size - section.address);
            else
            {
                section.usableSize = bnkReader.ReadUInt32();

                // Checksum: 4 bytes - ignored
                bnkReader.BaseStream.Seek(0x4, SeekOrigin.Current);
            }

            if (section.IsEmpty) return;

            // Données : taille variable
            section.data = bnkReader.ReadBytes((int)section.usableSize);

            // Deep reading
            _ReadSectionData(section);
        }

        /// <summary>
        /// Reads contents of specified section
        /// </summary>
        /// <param name="section">Section to read</param>
        private void _ReadSectionData(Section section)
        {
            if (section == null || section.data == null) return;

            using (BinaryReader dataReader = new BinaryReader(new MemoryStream(section.data)))
            {
                // Lecture différente selon le type de section
                switch (section.sectionType)
                {
                    case SectionType.Header:
                        #region HEADER
                        // Données inutiles...
                        dataReader.BaseStream.Seek(0xC, SeekOrigin.Current);
                        // Special flags (required on some files)
                        _SpecialFlag1 = dataReader.ReadUInt16();
                        _SpecialFlag2 = dataReader.ReadUInt16();
                        // Taille du fichier bnk, en octets
                        _FileSize = dataReader.ReadUInt32();
                        // Taille du contenu compacté (modulo 0x10)
                        dataReader.BaseStream.Seek(0x4, SeekOrigin.Current);
                        // Block sizes (???)
                        _MainBlockSize = dataReader.ReadUInt32();
                        _SecondaryBlockSize = dataReader.ReadUInt32();
                        // File count
                        //_PackedFileCount = dataReader.ReadUInt32();
                        dataReader.ReadUInt32();
                        // Année de réalisation
                        _Year = dataReader.ReadUInt32();
                        // Section addresses
                        _GetSection(SectionType.FileSize).address = dataReader.ReadUInt32();
                        _GetSection(SectionType.TypeMapping).address = dataReader.ReadUInt32();
                        _GetSection(SectionType.FileName).address = dataReader.ReadUInt32();
                        _GetSection(SectionType.FileOrder).address = dataReader.ReadUInt32();
                        // TODO 1x4 bytes to decrypt
                        _Unknown = dataReader.ReadUInt32();
                        _GetSection(SectionType.FileData).address = dataReader.ReadUInt32();
                        break;
                        #endregion
                    case SectionType.FileSize:
                        #region INFOS SUR LE CONTENU
                        int fileCounter = 0;

                        while (dataReader.BaseStream.Position < section.data.Length)
                        {
                            fileCounter++;

                            PackedFile aFile = new PackedFile
                            {
                                startAddress = dataReader.ReadUInt32(),
                                fileSize = dataReader.ReadUInt32()
                            };

                            // Nom de fichier (temporaire). Info non lue.
                            // BUG_39 : handling of 0 byte files
                            if (aFile.fileSize != 0
                                ||
                                (aFile.fileSize == 0 &&
                                 (section.data.Length - dataReader.BaseStream.Position) > _FILE_INFO_ENTRY_LENGTH))
                                aFile.fileName = _UNKNOWN_FILE_NAME + fileCounter;
                            else
                            {
                                // It's pure padding info
                                // https://github.com/djey47/tdumt/issues/1: size should be 0
                                aFile.fileName = _PADDING_FILE_NAME + fileCounter;
                                aFile.fileSize = 0;
                            }
                            // TODO 2x4 octets à décrypter....
                            aFile.unknown1 = dataReader.ReadUInt32();
                            aFile.unknown2 = dataReader.ReadUInt32();
                            //
                            aFile.exists = true;
                            aFile.parentBnk = this;

                            // Warning: use private member here to prevent recursive Loading !
                            __FileList.Add(aFile);
                        }
                        break;
                    #endregion
                    case SectionType.FileName:
                        #region LISTE DE FICHIERS
                        // New loader, recursive
                        if (section.data.Length > 1)
                            _FileInfoHierarchyRoot = _ReadFileInfoNode(dataReader, 0, null, section.data.Length);
                        break;
                        #endregion
                    case SectionType.FileOrder:
                        #region ORDONNANCEMENT DES FICHIERS
                        // On récupère l'ordre et on met à jour le nom de fichier
                        try
                        {
                            // Last file is just padding and must no be taken into account for order
                            for (int i = 0; i < __FileList.Count - 1; i++)
                            {
                                // Lecture du numéro de fichier concerné
                                // Si il y a plus de 255 fichiers, on doit lire des valeurs de 2 octets
                                int anOrder = __FileList.Count > 255 ? dataReader.ReadInt16() : dataReader.ReadByte();

                                // Récupération du nom de fichier
                                PackedFile aFile = __FileList[anOrder];

                                // Renaming
                                aFile.fileName = _NameList[i];
                                aFile.filePath = _PathList[i];

                                // On ajoute le fichier à la liste, au bon emplacement
                                __FileList[anOrder] = aFile;

                                // On met à jour l'index par nom de fichier
                                try
                                {
                                    __FileByPathList.Add(aFile.filePath, aFile);
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception("Error when adding a file path to index: " + aFile.filePath, ex);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Exception2.PrintStackTrace(ex);
                            throw;
                        }
                        break;
                        #endregion
                }
            }
        }

        /// <summary>
        /// Generates specified section contents into a byte array
        /// </summary>
        /// <param name="type"></param>
        private byte[] _WriteSection(SectionType type)
        {
            Section sectionToWrite = _GetSection(type);
            byte[] returnedData = new byte[0];

            // Modification différente selon le type de section
            switch (type)
            {
                case SectionType.Header:
                    #region HEADER
                    sectionToWrite.data = _WriteHeaderSection();
                    break;
                    #endregion

                case SectionType.FileSize:
                    #region INFOS SUR LE CONTENU
                    sectionToWrite.data = _WriteContentInfoSection();
                    break;
                    #endregion

                case SectionType.TypeMapping:
                    #region UNKNOWN SECTION
                    // Original contents are kept
                    break;
                    #endregion

                case SectionType.FileName:
                    #region LISTE DE NOMS DE FICHIERS
                    // New! all data is now rewritten for this section
                    sectionToWrite.data = _WriteFileListSection();
                    break;
                    #endregion

                case SectionType.FileOrder:
                    #region ORDRE DES FICHIERS
                    // Original contents are kept
                    sectionToWrite.data = _WriteFileOrderSection();
                    break;
                    #endregion

                case SectionType.FileData:
                    #region PACKED FILES
                    // Contents already updated
                    returnedData = sectionToWrite.data;
                    break;
                    #endregion

                default:
                    throw new Exception("Writing " + type + " section is not supported here.");
            }

            if (SectionType.FileData == sectionToWrite.sectionType) return returnedData;

            // New length update
            sectionToWrite.usableSize = (uint) sectionToWrite.data.Length;

            // Calcul du bourrage
            sectionToWrite.paddingSize =
                _GetSectionPaddingLength(_PREDATA_LENGTH + (uint) sectionToWrite.data.Length, _MainBlockSize);

            // New size and checksum
            byte[] preData = new byte[_PREDATA_LENGTH];

            using (BinaryWriter preDataWriter = new BinaryWriter(new MemoryStream(preData)))
            {
                preDataWriter.Write((ushort) sectionToWrite.usableSize);
                preDataWriter.BaseStream.Seek(0x2, SeekOrigin.Current);
                preDataWriter.Write(sectionToWrite.Checksum);
            }

            // Padding calculation
            byte[] padding = new byte[0];

            if (type != SectionType.FileData)
            {
                string paddingString = _PADDING_SEQUENCE.Substring(0, (int) sectionToWrite.paddingSize);

                padding = String2.ToByteArray(paddingString);
            }

            // Writing
            int dataLength = 0;

            if (sectionToWrite.data != null)
                dataLength = sectionToWrite.data.Length;

            returnedData = new byte[preData.Length + dataLength + padding.Length];
            Array.Copy(preData, returnedData, preData.Length);

            if (dataLength != 0)
                Array.Copy(sectionToWrite.data, 0, returnedData, preData.Length, dataLength);

            Array.Copy(padding, 0, returnedData, preData.Length + dataLength, padding.Length);

            return returnedData;
        }

        /// <summary>
        /// Writes header section into a byte array
        /// </summary>
        private byte[] _WriteHeaderSection()
        {
            // Max length = 64 bytes
            int dataLength = 64;
            byte[] tempData = new byte[dataLength];

            using (BinaryWriter dataWriter = new BinaryWriter(new MemoryStream(tempData)))
            {
                // TAG
                dataWriter.Write(String2.ToByteArray(_BANK_TAG));
                
                // 0 data
                dataWriter.Seek(0x8, SeekOrigin.Current);
                // Special flags
                dataWriter.Write(_SpecialFlag1);
                dataWriter.Write(_SpecialFlag2);
                
                // BNK file size
                dataWriter.Write(_GetTotalSize());
                
                // Packed content size (% 0x10)
                dataWriter.Write(PackedContentSize);
                
                // TODO 2x4 octets ....
                dataWriter.Write(_MainBlockSize);
                dataWriter.Write(_SecondaryBlockSize);

                // Packed files count
                dataWriter.Write(PackedFilesCount);
                
                // Year
                dataWriter.Write((uint)DateTime.Today.Year);
                
                // Section addresses
                uint fileSizeSectionAddress = _GetSection(SectionType.FileSize).address;
                uint typeMappingSectionAddress = _GetSection(SectionType.TypeMapping).address;
                uint fileNameSectionAddress = _GetSection(SectionType.FileName).address;
                uint fileOrderSectionAddress = _GetSection(SectionType.FileOrder).address;
                uint fileDataSectionAddress = _GetSection(SectionType.FileData).address;

                dataWriter.Write(fileSizeSectionAddress);
                dataWriter.Write(typeMappingSectionAddress);
                dataWriter.Write(fileNameSectionAddress);
                dataWriter.Write(fileOrderSectionAddress);
                dataWriter.Write(_Unknown);
                dataWriter.Write(fileDataSectionAddress);
                
                // Data size
                dataLength = (int)dataWriter.BaseStream.Position;
            }

            byte[] returnedData = new byte[dataLength];

            Array.Copy(tempData, returnedData, dataLength);

            return returnedData;
        }

        /// <summary>
        /// Writes content info section into a byte array
        /// </summary>
        private byte[] _WriteContentInfoSection()
        {
            // TODO dynamical limit ?
            int dataLength = 16384;
            byte[] tempData = new byte[dataLength];

            using (BinaryWriter dataWriter = new BinaryWriter(new MemoryStream(tempData)))
            {
                // Pour chaque fichier de la liste ...
                for (int i = 0; i < _FileList.Count; i++)
                {
                    PackedFile aFile = _FileList[i];

                    // Adresse de début du fichier
                    dataWriter.Write(aFile.startAddress);
                    // Taille du fichier
                    dataWriter.Write(aFile.fileSize);
                    // TODO 2x4 octets....
                    dataWriter.Write(aFile.unknown1);
                    dataWriter.Write(aFile.unknown2);
                }

                dataLength = (int)dataWriter.BaseStream.Position;
            }

            byte[] returnedData = new byte[dataLength];

            Array.Copy(tempData, returnedData, dataLength);

            return returnedData;
        }

        /// <summary>
        /// Writes file list section into a byte array
        /// </summary>
        private byte[] _WriteFileListSection()
        {
            // TODO dynamical limit ?
            int dataLength = 16384;
            byte[] tempData = new byte[dataLength];

            using (BinaryWriter newDataWriter = new BinaryWriter(new MemoryStream(tempData)))
            {
                _WriteFileNode(newDataWriter, _FileInfoHierarchyRoot);
                newDataWriter.Write((byte)0x0);
                dataLength = (int)newDataWriter.BaseStream.Position;
            }

            byte[] returnedData = new byte[dataLength];
            Array.Copy(tempData, returnedData, dataLength);

            return returnedData;
        }

        /// <summary>
        /// Writes file order section into a byte array
        /// </summary>
        /// <returns></returns>
        private byte[] _WriteFileOrderSection()
        {
            // TODO dynamical limit ?
            int dataLength = 16384;
            byte[] tempData = new byte[dataLength];

            using (BinaryWriter newDataWriter = new BinaryWriter(new MemoryStream(tempData)))
            {
                foreach (string anotherPath in _PathList)
                {
                    // Searching file path in path list and returning real index
                    short index = -1;

                    for (short i = 0; i < __FileList.Count - 1 && index == -1; i++)
                    {
                        PackedFile currentFile = __FileList[i];

                        if(anotherPath.Equals(currentFile.filePath))
                            index = i;
                    }

                    // Writing file number
                    // if more than 255 files, 16bit values must be written
                    if (__FileList.Count > 255)
                        newDataWriter.Write(index);
                    else
                        newDataWriter.Write((byte) index);
                }

                dataLength = (int)newDataWriter.BaseStream.Position;
            }

            byte[] returnedData = new byte[dataLength];
            Array.Copy(tempData, returnedData, dataLength);

            return returnedData;
        }

        /// <summary>
        /// Updates section addresses for specified data arrays
        /// </summary>
        /// <param name="fileSizeData"></param>
        /// <param name="typeMappingData"></param>
        /// <param name="fileNameData"></param>
        /// <param name="fileOrderData"></param>
        private void _ComputeSectionAddresses(byte[] fileSizeData, byte[] typeMappingData, byte[] fileNameData, byte[] fileOrderData)
        {
            // Header section : since this section has not been written yet, padding size must be calculated here
            Section headerSection = _GetSection(SectionType.Header);

            headerSection.paddingSize = _GetSectionPaddingLength(_PREDATA_LENGTH + (uint)headerSection.data.Length, _MainBlockSize);

            // File size section
            Section fileSizeSection = _GetSection(SectionType.FileSize);

            fileSizeSection.address = (uint)(_PREDATA_LENGTH + headerSection.data.Length + headerSection.paddingSize);

            // Type mapping section
            Section typeMappingSection = _GetSection(SectionType.TypeMapping);

            if (typeMappingSection.isPresent)
                typeMappingSection.address = (uint)(fileSizeSection.address + fileSizeData.Length);

            // File name section
            Section fileNameSection = _GetSection(SectionType.FileName);
            
            if (typeMappingSection.isPresent)
                fileNameSection.address = (uint) (typeMappingSection.address + typeMappingData.Length);
            else
                fileNameSection.address = (uint) (fileSizeSection.address + fileSizeData.Length);

            // File order section
            Section fileOrderSection = _GetSection(SectionType.FileOrder);

            fileOrderSection.address = (uint) (fileNameSection.address + fileNameData.Length);

            // File data section
            Section fileDataSection = _GetSection(SectionType.FileData);

            fileDataSection.address = (uint) (fileOrderSection.address + fileOrderData.Length);

            // Offset for packed files addresses: section sizes may have changed since latest packed content update
            if (_FileList.Count > 0)
            {
                PackedFile firstPackedFile = _FileList[0];
                int delta = (int) (fileDataSection.address - firstPackedFile.startAddress);

                if (delta != 0)
                {
                    foreach (PackedFile packedFile in _FileList)
                    {
                        if (delta > 0)
                            packedFile.startAddress += (uint)Math.Abs(delta);
                        else
                            packedFile.startAddress -= (uint)Math.Abs(delta);
                    }
                }
            }
        }

        /// <summary>
        /// Calculates total size of current BNK file, according file data section information
        /// </summary>
        /// <returns></returns>
        private uint _GetTotalSize()
        {
            Section fileDataSection = _GetSection(SectionType.FileData);

            return (fileDataSection.address + fileDataSection.usableSize);
        }
        #endregion
    }
}
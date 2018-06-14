using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using DjeFramework1.Common.Support.Meta;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.support;
using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.fileformats.banks
{
    /// <summary>
    /// Represents and handles BNK format (TDU Archives)
    /// </summary>
    public partial class BNK : TduFile
    {
        #region Constants
        /// <summary>
        /// Complete padding sequence (lol)
        /// </summary>
        private const string _PADDING_SEQUENCE = "STNICC2000 RULEZPADDING DATAS...-ORIC AND ATARI--COOL  MACHINES-";

        /// <summary>
        /// Tag for BANK
        /// </summary>
        private const string _BANK_TAG = "KNAB";

        /// <summary>
        /// Longueur de la zone taille + CRC de chaque section
        /// </summary>
        private const uint _PREDATA_LENGTH = 0x8;

        /// <summary>
        /// Exception message format when specified packed file does not exist into current BNK
        /// </summary>
        private const string _FORMAT_ERROR_PACKED_FILE_NOT_EXISTS = "Specified packed file does not exist:\r\n{0}";

        /// <summary>
        /// Exception message format when attempting to rename a file whereas a file already exists with same name
        /// </summary>
        private const string _FORMAT_ERROR_PACKED_FILE_NAME_EXISTS = "A packed file already exists with same name:\r\n{0}";

        /// <summary>
        /// Error message when attempting to rename extensions
        /// </summary>
        private const string _ERROR_RENAMING_EXTENSIONS = "Sorry, file extensions can't be changed for now.";
        #endregion

        #region Properties
        /// <summary>
        /// Taille du contenu compacté, en octets
        /// </summary>
        public uint PackedContentSize {
            get
            {
                uint returnedSize = 0;

                // Browsing all packed files
                foreach (PackedFile anotherFile in _FileList)
                {
                    returnedSize += anotherFile.fileSize;

                    // BUG: if one packed file, do not take padding file size into account
                    /*if (_FileList.Count == 2)
                        break;*/
                }

                return returnedSize;
            }
        }

        /// <summary>
        /// Année de création du fichier
        /// </summary>
        public uint Year { get { return _Year; } }
        private uint _Year;

        /// <summary>
        /// Nombre de fichiers empaquetés. Do not use when loading/saving, prefer getting __FileList.Count
        /// </summary>
        public int PackedFilesCount
        {
            // Last file does not count (padding)
             get { return _FileList.Count - 1; }
        }
        #endregion

        #region Members
        /// <summary>
        /// Liste de sections
        /// </summary>
        private readonly Collection<Section> _SectionList = new Collection<Section>();

        /// <summary>
        /// Liste des noms de fichiers empaquetés. Utilisée uniquement durant la lecture.
        /// </summary>
        private readonly Collection<string> _NameList = new Collection<string>();

        /// <summary>
        /// Packed paths list. Used only when reading.
        /// </summary>
        private readonly Collection<string> _PathList = new Collection<string>();

        /// <summary>
        /// Main block size for reading (sections 1 to 5)
        /// </summary>
        private uint _MainBlockSize;

        /// <summary>
        /// Secondary block size for reading (section 6)
        /// </summary>
        private uint _SecondaryBlockSize;

        /// <summary>
        /// ???
        /// </summary>
        private uint _Unknown;

        /// <summary>
        /// Required on some files
        /// </summary>
        private ushort _SpecialFlag1;

        /// <summary>
        /// Required on some files
        /// </summary>
        private ushort _SpecialFlag2;
        #endregion

        /// <summary>
        /// Constructeur paramétré
        /// </summary>
        /// <param name="bnkFileName">Chemin du fichier BNK</param>
        internal BNK(string bnkFileName)
        {
            _FileName = bnkFileName;
            _ReadData();
        }

        internal BNK()
        {}

        #region TDUFile implementation
        /// <summary>
        /// Initialise les infos avec le contenu du fichier (redéfinition). To be called one in constructor.
        /// If you need reloading file, use _ReloadIfNecessary() method instead.
        /// </summary>
        protected override void _ReadData()
        {
            // Clearing all data lists
            _NameList.Clear();
            _PathList.Clear();
            __FileList.Clear();
            __FileByPathList.Clear();
            __FileInfoByPathList.Clear();

            // Initializing sections
            _CreateSections();

            // Reading file
            using (BinaryReader bnkReader 
                = new BinaryReader(new FileStream(_FileName, FileMode.Open, FileAccess.Read)))
            {
                // Section 1: header
                _ReadSection(SectionType.Header, bnkReader);

                // Section 2: fileSize
                _ReadSection(SectionType.FileSize, bnkReader);

                // Section 3: typeMapping (optional)
                Section typeMappingSection = _GetSection(SectionType.TypeMapping);

                if (typeMappingSection.address > 0)
                    _ReadSection(SectionType.TypeMapping, bnkReader);
                else
                    typeMappingSection.isPresent = false;

                // Section 4: fileName
                _ReadSection(SectionType.FileName, bnkReader);

                // Section 5: fileOrder
                _ReadSection(SectionType.FileOrder, bnkReader);

                // Section 6: fileData
                _ReadSection(SectionType.FileData, bnkReader);
            }

            // EVO_65: Properties
            Property.ComputeValueDelegate fileCountDelegate = () => PackedFilesCount.ToString();
            Property.ComputeValueDelegate yearDelegate = () => Year.ToString();

            Properties.Add(new Property("Packed files count", "BNK", fileCountDelegate));
            Properties.Add(new Property("Year", "BNK", yearDelegate));
        }

        /// <summary>
        /// Writes current BNK file to disk.
        /// This method is not to be called directly in BNK context.
        /// </summary>
        public override void Save()
        {
            try
            {
                // Removing read-only flag
                File2.RemoveAttribute(_FileName, FileAttributes.ReadOnly);

                // Getting all data
                byte[] fileSizeData = _WriteSection(SectionType.FileSize);
                byte[] typeMappingData = new byte[0];
                byte[] fileNameData = _WriteSection(SectionType.FileName);
                byte[] fileOrderData = _WriteSection(SectionType.FileOrder);
                byte[] fileData = _WriteSection(SectionType.FileData);

                if (_GetSection(SectionType.TypeMapping).isPresent)
                    typeMappingData = _WriteSection(SectionType.TypeMapping);

                // Computing new section addresses
                _ComputeSectionAddresses(fileSizeData, typeMappingData, fileNameData, fileOrderData);
                // Warning ! Files address may have changed. fileSize section data may need to be updated (written again)
                fileSizeData = _WriteSection(SectionType.FileSize);

                // Header is written, finally
                byte[] headerData = _WriteSection(SectionType.Header);

                // Writing into file
                using (BinaryWriter newBnkWriter =
                        new BinaryWriter(new FileStream(_FileName, FileMode.Create, FileAccess.Write)))
                {
                    newBnkWriter.Write(headerData);
                    newBnkWriter.Write(fileSizeData);
                    newBnkWriter.Write(typeMappingData);
                    newBnkWriter.Write(fileNameData);
                    newBnkWriter.Write(fileOrderData);
                    newBnkWriter.Write(fileData);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error when saving BNK file: " + _FileName, ex);
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Extracts specified file from BNK.
        /// </summary>
        /// <param name="packedFilePath">Packed file path</param>
        /// <param name="path">File or folder name where packed file must be extracted</param>
        /// <param name="toFolder">true if path is a folder name, else if it's a single file name</param>
        public void ExtractPackedFile(string packedFilePath, string path, bool toFolder)
        {
            if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(_FileName))
            {
                try
                {
                    // Getting packed file
                    PackedFile aFile = _GetPackedFile(packedFilePath);

                    if (aFile.exists)
                        _ExtractPackedFile(aFile, path, toFolder);
                    else
                        throw new Exception(string.Format(_FORMAT_ERROR_PACKED_FILE_NOT_EXISTS, packedFilePath));
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to extract packed file: " + packedFilePath, ex);
                }
            }
        }

        /// <summary>
        /// Updates a packed file by specified file. BNK is saved automatically.
        /// </summary>
        /// <param name="packedFilePath">Path of packed file to update</param>
        /// <param name="newFileName">Path of file with new contents for updating</param>
        public void ReplacePackedFile(string packedFilePath, string newFileName)
        {
            if (!string.IsNullOrEmpty(packedFilePath) && !string.IsNullOrEmpty(newFileName))
            {
                try
                {
                    // Getting packed file...
                    PackedFile packedFile = _GetPackedFile(packedFilePath);

                    if (packedFile.exists)
                        _ReplacePackedFile(packedFile, newFileName);
                    else
                        throw new Exception(string.Format(_FORMAT_ERROR_PACKED_FILE_NOT_EXISTS, packedFilePath));
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to replace packed file: " + packedFilePath, ex);
                }
            }
        }      
        
        /// <summary>
        /// Changes name of specified packed file
        /// </summary>
        /// <param name="packedFilePath"></param>
        /// <param name="newPackedFileName"></param>
        public void RenamePackedFile(string packedFilePath, string newPackedFileName)
        {
            try
            {
                if (!string.IsNullOrEmpty(packedFilePath) && !string.IsNullOrEmpty(newPackedFileName))
                {
                    // Extension check
                    string packedFileName = GetPackedFileName(packedFilePath);
                    string oldExtension = File2.GetExtensionFromFilename(packedFileName);
                    string newExtension = File2.GetExtensionFromFilename(newPackedFileName);

                    if (oldExtension != null
                        && newExtension != null
                        && !oldExtension.Equals(newExtension, StringComparison.InvariantCultureIgnoreCase))
                        throw new Exception(_ERROR_RENAMING_EXTENSIONS);

                    // Name check
                    if (GetPackedFilesPaths(newPackedFileName).Count > 0)
                        throw new Exception(string.Format(_FORMAT_ERROR_PACKED_FILE_NAME_EXISTS, newPackedFileName));

                    // Getting packed file...
                    PackedFile packedFile = _GetPackedFile(packedFilePath);

                    if (packedFile.exists)
                        _RenamePackedFile(packedFile, newPackedFileName);
                    else
                        throw new Exception(string.Format(_FORMAT_ERROR_PACKED_FILE_NOT_EXISTS, packedFilePath));
                }
                else
                    throw new Exception("Forbidden operation.");
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to rename packed file: " + packedFilePath + "\r\n" + ex.Message, ex);
            }
        }

        /// <summary>
        /// Deletes specified packed file
        /// </summary>
        /// <param name="packedFilePath"></param>
        public void DeletePackedFile(string packedFilePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(packedFilePath))
                {
                    // Getting packed file...
                    PackedFile packedFile = _GetPackedFile(packedFilePath);

                    if (packedFile.exists)
                        _DeletePackedFile(packedFile);
                    else
                        throw new Exception(string.Format(_FORMAT_ERROR_PACKED_FILE_NOT_EXISTS, packedFilePath));
                }
                else
                    throw new Exception("Forbidden operation.");
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to delete packed file: " + packedFilePath + "\r\n" + ex.Message, ex);
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Extrait le fichier spécifié du BNK.
        /// If destination file already exists, it is renamed to *_old.
        /// </summary>
        /// <param name="aFile">Fichier empaqueté</param>
        /// <param name="path">Nom du fichier à créer ou nom du dossier où il doit être extrait (le nom sera inchangé)</param>
        /// <param name="toFolder">true si le path désigne un nom de dossier, false si c'est un nom de fichier</param>
        private static void _ExtractPackedFile(PackedFile aFile, string path, bool toFolder)
        {
            if (!string.IsNullOrEmpty(path) && aFile.exists)
            {
                // Gestion des deux modes d'extraction
                string newFileName = path;

                if (toFolder)
                    newFileName = Path.Combine(path, aFile.fileName);

                // Same name management
                Tools.RenameIfNecessary(newFileName, LibraryConstants.SUFFIX_OLD_FILE);

                // On lance la création
                File2.WriteBytesToFile(newFileName, aFile.Data);
            }
        }

        /// <summary>
        /// Replaces specified packed file with a file from disk.
        /// New Replacing: BNK is automatically saved.
        /// </summary>
        /// <param name="replacedFile"></param>
        /// <param name="newFilename"></param>
        private void _ReplacePackedFile(PackedFile replacedFile, string newFilename)
        {
            if (replacedFile != null && !string.IsNullOrEmpty(newFilename))
            {
                // New file data
                List<byte[]> newFileData = new List<byte[]>();
                int currentFileIndex = _GetPackedFileIndex(replacedFile);
                int newFileSize = (int)new FileInfo(newFilename).Length;
                byte[] newData;

                using (BinaryReader newReader = new BinaryReader(new FileStream(newFilename, FileMode.Open, FileAccess.Read)))
                    newData = newReader.ReadBytes(newFileSize);

                // Previous files
                for (int i = 0 ; i < currentFileIndex ; i++)
                {
                    PackedFile currentFile = _FileList[i];
                    
                    newFileData.Add(currentFile.Data);
                }

                // New file
                newFileData.Add(newData);

                // Next files
                for (int i = currentFileIndex + 1; i < _FileList.Count; i++)
                {
                    PackedFile currentFile = _FileList[i];

                    newFileData.Add(currentFile.Data);
                }

                // Setting data in right section.Packed files are also updated
                _GetSection(SectionType.FileData).data = _MakeFileData(newFileData);

                // Final saving
                Save();
            }
        }

        /// <summary>
        /// Handles packed file renaming. Extensions can't be changed for now.
        /// BNK is automatically saved.
        /// </summary>
        /// <param name="renamedPackedFile"></param>
        /// <param name="newFileName"></param>
        private void _RenamePackedFile(PackedFile renamedPackedFile, string newFileName)
        {
            if (renamedPackedFile != null && !string.IsNullOrEmpty(newFileName))
            {
                // Changing file info node should be sufficient...
                FileInfoNode infoNode = _FileInfoByPathList[renamedPackedFile.filePath];
                
                infoNode.name = File2.GetNameFromFilename(newFileName);
                renamedPackedFile.fileName = newFileName;

                // Final saving
                Save();
            }
        }

        /// <summary>
        /// Deletes specified packed file from this BNK
        /// </summary>
        /// <param name="packedFileToDelete"></param>
        private void _DeletePackedFile(PackedFile packedFileToDelete)
        {
            if (packedFileToDelete != null)
            {
                // Getting parent node...
                FileInfoNode currentNode = _FileInfoByPathList[packedFileToDelete.filePath];
                FileInfoNode parentNode = currentNode.parentNode;

                // Removing child
                parentNode.childNodes.Remove(currentNode);
                // TODO delete parent node if children count = 0 ? (recursive)
                
                // New file data: empty
                List<byte[]> newFileData = new List<byte[]>();
                int deletedFileIndex = _GetPackedFileIndex(packedFileToDelete);

                for (int i = 0; i < _FileList.Count; i++)
                {
                    // Ignores file to delete
                    if (i != deletedFileIndex)
                    {
                        PackedFile currentFile = _FileList[i];

                        newFileData.Add(currentFile.Data);
                    }
                }

                // Deletion
                _FileInfoByPathList.Remove(packedFileToDelete.filePath);
                _PathList.Remove(packedFileToDelete.filePath);
                _FileList.Remove(packedFileToDelete);

                // Setting data in right section.Packed files are also updated
                _GetSection(SectionType.FileData).data = _MakeFileData(newFileData);

                // Final saving
                Save();
            }
        }
        #endregion
    }
}
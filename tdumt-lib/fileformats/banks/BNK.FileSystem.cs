using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Common.Types;

namespace TDUModdingLibrary.fileformats.banks
{
    /// <summary>
    /// BNK format - File System handling
    /// </summary>
    partial class BNK
    {
        #region Constants
        /// <summary>
        /// Pattern for file name (to redefine)
        /// </summary>
        public new static readonly string FILENAME_PATTERN = string.Format(String2.REGEX_PATTERN_EXTENSION, "BNK");

        /// <summary>
        /// Length of each entry for file info
        /// </summary>
        private const uint _FILE_INFO_ENTRY_LENGTH = 0x8;
        
        /// <summary>
        /// Flag used to mark a folder position
        /// </summary>
        private const uint _LIST_FOLDER_NODE_FLAG = 0x100;

        /// <summary>
        /// Smallest value for a usable character
        /// </summary>
        private const uint _LIST_MIN_USABLE_CHAR = 0x1F;

        /// <summary>
        /// Name for last file in BNK (=final padding);
        /// </summary>
        private const string _PADDING_FILE_NAME = "Padding.";


        /// <summary>
        /// Name for unknown files in BNK (whose name has not been resolved yet)
        /// </summary>
        private const string _UNKNOWN_FILE_NAME = "File.";
        #endregion

        #region Types
        /// <summary>
        /// Describes an information node in file list
        /// </summary>
        public class FileInfoNode
        {
            #region Members
            /// <summary>
            /// Internal node id
            /// </summary>
            internal int id = -1;

            /// <summary>
            /// Parent node reference
            /// </summary>
            internal FileInfoNode parentNode;

            /// <summary>
            /// Name
            /// </summary>
            public string name;

            /// <summary>
            /// Type (file/folder)
            /// </summary>
            public FileInfoNodeType type;

            /// <summary>
            /// Child node list
            /// </summary>
            public readonly List<FileInfoNode> childNodes = new List<FileInfoNode>();
            #endregion

            #region Properties
            /// <summary>
            /// Name of parent node
            /// </summary>
            internal string ParentName
            {
                get { return (parentNode == null ? "" : parentNode.name); }
            }

            /// <summary>
            /// Packed file path (given by node)
            /// </summary>
            public string Path
            {
                get
                {
                    // Rebuilding whole path, recursively
                    StringBuilder sb;

                    if (parentNode == null)
                        sb = new StringBuilder();
                    else
                        sb = new StringBuilder(parentNode.Path);

                    if (!IsRoot)
                        sb.Append(@"\");
                    sb.Append(name);

                    return sb.ToString();
                }
            }

            /// <summary>
            /// Packed file name
            /// </summary>
            public string FileName
            {
                get
                {
                    string fileName = null;

                    if (type == FileInfoNodeType.File)
                        // Extension is ParentName
                        fileName = string.Concat(name, ParentName);

                    return fileName;
                }
            }

            /// <summary>
            /// Root node ?
            /// </summary>
            internal bool IsRoot
            {
                get { return (id == 0 && parentNode == null); }
            }
            #endregion
        }

        /// <summary>
        /// Node type in hierarchy
        /// </summary>
        public enum FileInfoNodeType
        {
            Folder, File, ExtensionGroup
        }

        /// <summary>
        /// Décrit un fichier empaqueté
        /// </summary>
        internal class PackedFile : ICloneable
        {
            #region Properties
            /// <summary>
            /// Returns address of current packed file in data array
            /// </summary>
            private uint LocalAddress
            {
                get
                {
                    uint returnedAddress = 0;

                    if (parentBnk != null)
                    {
                        Section fileDataSection = parentBnk._GetSection(SectionType.FileData);

                        if (startAddress < fileDataSection.address)
                            throw new Exception("Corrupted BNK file: file data section and file address do not match");

                        returnedAddress = startAddress - fileDataSection.address;
                    }

                    return returnedAddress;
                }
            }

            /// <summary>
            /// Returns current file data
            /// </summary>
            internal byte[] Data
            {
                get
                {
                    byte[] returnedData = null;

                    if (parentBnk != null)
                    {
                        Section filesSection = parentBnk._GetSection(SectionType.FileData);

                        if (filesSection.data != null)
                        {
                            returnedData = new byte[fileSize];
                            Array.Copy(filesSection.data, LocalAddress, returnedData, 0, fileSize);
                        }
                    }

                    return returnedData;
                }
            }
            #endregion

            #region Members
            internal string fileName;
            internal string filePath;
            internal uint startAddress;
            internal uint fileSize;
            internal uint unknown1;
            internal uint unknown2;
            internal bool exists;
            internal BNK parentBnk;
            #endregion

            #region ICloneable Members
            ///<summary>
            /// Creates a new object that is a copy of the current instance.
            ///</summary>
            ///<returns>
            /// A new object that is a copy of this instance.
            ///</returns>
            ///<filterpriority>2</filterpriority>
            public object Clone()
            {
                PackedFile returnedClone = new PackedFile
                                               {
                                                   fileName = fileName,
                                                   filePath = filePath,
                                                   fileSize = fileSize,
                                                   startAddress = startAddress,
                                                   exists = exists,
                                                   parentBnk = parentBnk,
                                                   unknown1 = unknown1,
                                                   unknown2 = unknown2
                                               };

                return returnedClone;
            }
            #endregion
        }
        #endregion
    
        #region Private properties
        /// <summary>
        /// List of packed files, in read order (cache)
        /// </summary>
        internal Collection<PackedFile> _FileList
        {
            get
            {
                ReloadIfNecessary();

                return __FileList;
            }
        }

        /// <summary>
        /// Index of packed files by file paths (cache)
        /// </summary>
        private Dictionary<string, PackedFile> _FileByPathList
        {
            get
            {
                ReloadIfNecessary();

                return __FileByPathList;
            }
        }

        /// <summary>
        /// Index of nodes by file names (cache)
        /// </summary>
        private Dictionary<string, FileInfoNode> _FileInfoByPathList
        {
            get
            {
                ReloadIfNecessary();

                return __FileInfoByPathList;
            }
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Root node for file information list
        /// </summary>
        public FileInfoNode FileInfoHierarchyRoot { get { return _FileInfoHierarchyRoot; } }
        private FileInfoNode _FileInfoHierarchyRoot;
        #endregion

        #region Members to be accessed when loading only
        /// <summary>
        /// Index of nodes by file path
        /// </summary>
        private readonly Dictionary<string, FileInfoNode> __FileInfoByPathList = new Dictionary<string, FileInfoNode>();

        /// <summary>
        /// Liste de fichiers empaquetés par chemin complet
        /// </summary>
        private readonly Dictionary<string, PackedFile> __FileByPathList = new Dictionary<string, PackedFile>();
        
        /// <summary>
        /// Liste de fichiers empaquetés, dans l'ordre de lecture. When not loading use property to access it !
        /// </summary>
        internal readonly Collection<PackedFile> __FileList = new Collection<PackedFile>();
        #endregion

        #region Private methods
        /// <summary>
        /// Reads and returns current node in file list
        /// </summary>
        /// <param name="memReader"></param>
        /// <param name="nodeId"></param>
        /// <param name="parent"></param>
        /// <param name="bufferSize"></param>
        /// <returns></returns>
        private FileInfoNode _ReadFileInfoNode(BinaryReader memReader, int nodeId, FileInfoNode parent, long bufferSize)
        {
            FileInfoNode returnedNode = new FileInfoNode();

            if (memReader != null && memReader.BaseStream.Position < bufferSize)
            {
                // Type
                byte currentChar = memReader.ReadByte();

                if (_LIST_FOLDER_NODE_FLAG - currentChar <= 64)
                    returnedNode.type = FileInfoNodeType.Folder;
                else
                    returnedNode.type = FileInfoNodeType.File;

                // Id
                returnedNode.id = nodeId;

                // Parent node
                returnedNode.parentNode = parent;

                if (returnedNode.type == FileInfoNodeType.Folder)
                {
                    // Children count
                    // Warning ! max 255 files per folder.
                    byte childrenCount = memReader.ReadByte();
                    byte testNext = memReader.ReadByte();
                    uint childrenCountFinal = childrenCount;

                    // BUGFIX https://github.com/djey47/tdumt/issues/1
                    // Some files get an additional byte here, we ignore it
                    if (testNext > _LIST_MIN_USABLE_CHAR)
                        // Standard case
                        memReader.BaseStream.Seek(-1, SeekOrigin.Current);
                    else
                        Log.Warning($"Weird byte spotted in file tree {testNext} at id={returnedNode.id} - children count: {childrenCountFinal}");

                    // Name
                    uint nameLength = _LIST_FOLDER_NODE_FLAG - currentChar;
                    byte[] bytes = memReader.ReadBytes((int)nameLength);

                    returnedNode.name = Encoding.ASCII.GetString(bytes);

                    // Children list
                    for (uint u = 0; u < childrenCountFinal; u++)
                    {
                        FileInfoNode childNode = _ReadFileInfoNode(memReader, ++nodeId, returnedNode, bufferSize);

                        returnedNode.childNodes.Add(childNode);
                    }
                }
                else
                {
                    // File
                    // Parent is now an ExtensionGroup
                    parent.type = FileInfoNodeType.ExtensionGroup;

                    byte[] bytes = memReader.ReadBytes(currentChar);

                    returnedNode.name = Encoding.ASCII.GetString(bytes);

                    // Adding file name to global lists
                    _NameList.Add(returnedNode.FileName);
                    _PathList.Add(returnedNode.Path);

                    // Index update
                    string keyFileName = returnedNode.Path;

                    if (__FileInfoByPathList.ContainsKey(keyFileName))
                        throw new Exception("Corrupted BNK file, two files have the same path: " + keyFileName);
                    
                    __FileInfoByPathList.Add(keyFileName, returnedNode);
                }
            }

            return returnedNode;
        }

        /// <summary>
        /// Writes specified file list node
        /// </summary>
        /// <param name="bnkWriter"></param>
        /// <param name="nodeToWrite"></param>
        /// <returns></returns>
        private static void _WriteFileNode(BinaryWriter bnkWriter, FileInfoNode nodeToWrite)
        {
            if (bnkWriter != null
                && nodeToWrite != null
                && bnkWriter.BaseStream.Position < bnkWriter.BaseStream.Length)
            {
                // Current node
                switch (nodeToWrite.type)
                {
                    case FileInfoNodeType.File:
                        bnkWriter.Write((byte)nodeToWrite.name.Length);
                        break;

                    case FileInfoNodeType.Folder:
                    case FileInfoNodeType.ExtensionGroup:
                        byte nodeFlag = (byte)(_LIST_FOLDER_NODE_FLAG - nodeToWrite.name.Length);
                        int packedFilesCount = nodeToWrite.childNodes.Count;

                        bnkWriter.Write(nodeFlag);
                        bnkWriter.Write((byte) packedFilesCount);

                        // BUGFIX https://github.com/djey47/tdumt/issues/1
                        // FIXME Find better solution... might break other files!
                        if (packedFilesCount >= 176)
                        {
                            bnkWriter.Write((byte) 1);
                        }

                        break;
                }

                byte[] currentName = String2.ToByteArray(nodeToWrite.name);

                bnkWriter.Write(currentName);

                // Writing child nodes
                if (nodeToWrite.childNodes != null)
                {
                    foreach (FileInfoNode anotherChildNode in nodeToWrite.childNodes)
                        _WriteFileNode(bnkWriter, anotherChildNode);
                }
            }
        }

        /// <summary>
        /// Returns information about packed file from its path.
        /// </summary>
        /// <param name="packedFilePath">Le nom du fichier a récupérer.</param>
        /// <returns>Les infos sur le fichier</returns>
        private PackedFile _GetPackedFile(string packedFilePath)
        {
            PackedFile returnedFile = new PackedFile();

            if (_FileByPathList.ContainsKey(packedFilePath))
                returnedFile = _FileByPathList[packedFilePath];

            return returnedFile;
        }

        /// <summary>
        /// Retourne l'index du fichier empaqueté spécifié
        /// </summary>
        /// <param name="packedFile">fichier empaqueté</param>
        /// <returns>-1 if not found</returns>
        private int _GetPackedFileIndex(PackedFile packedFile)
        {
            int returnedIndex = -1;

            for (int i = 0; i < _FileList.Count && returnedIndex == -1; i++)
            {
                if (_FileList[i].filePath.Equals(packedFile.filePath))
                    returnedIndex = i;
            }

            return returnedIndex;
        }

        /// <summary>
        /// Returns array of bytes for file data section
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        private byte[] _MakeFileData(IEnumerable<byte[]> files)
        {
            byte[] returnedData = new byte[0];

            if (files != null)
            {
                // Computing theorical size: current size of each file + up to 16 padding bytes per file
                int maxSize = 0;

                foreach (byte[] anotherFileData in files)
                    maxSize += (anotherFileData.Length + 16);

                byte[] tempData = new byte[maxSize];
                long realDataSize;

                // Writing all files
                Section fileDataSection = _GetSection(SectionType.FileData);

                using (BinaryWriter writer = new BinaryWriter(new MemoryStream(tempData)))
                {
                    int index = 0;

                    foreach (byte[] anotherFileData in files)
                    {
                        // Updating packed file information
                        PackedFile correspondingFile = _FileList[index++];

                        correspondingFile.fileSize = (uint)anotherFileData.Length;
                        correspondingFile.startAddress = (uint)writer.BaseStream.Position + fileDataSection.address;

                        // Data
                        writer.Write(anotherFileData);

                        // BUGFIX https://github.com/djey47/tdumt/issues/1
                        // Padding... no padding for the last real file
                        if (index < _FileList.Count - 1)
                        {
                            uint paddingLength = _GetPaddingLength((uint) anotherFileData.Length, _SecondaryBlockSize);
                            byte[] padding = new byte[paddingLength];
                            byte[] paddingString = String2.ToByteArray(_PADDING_SEQUENCE);

                            Array.Copy(paddingString, padding, paddingLength);
                            writer.Write(padding);
                        }

                        // TODO Padding block ignored at writing => to be tested
                    }

                    realDataSize = writer.BaseStream.Position;
                }

                // Putting real data into result
                returnedData = new byte[realDataSize];
                Array.Copy(tempData, 0, returnedData, 0, realDataSize);

                // Updating usableSize
                fileDataSection.usableSize = (uint)realDataSize;
            }

            return returnedData;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Returns size (in bytes) of packed file with specified path
        /// </summary>
        /// <param name="packedFilePath"></param>
        /// <returns></returns>
        public long GetPackedFileSize(string packedFilePath)
        {
            long returnedSize = 0;

            if (!string.IsNullOrEmpty(packedFilePath))
            {
                // Getting packed file
                PackedFile aFile = _GetPackedFile(packedFilePath);

                if (aFile.exists)
                    returnedSize = aFile.fileSize;
                else
                    throw new Exception(string.Format(_FORMAT_ERROR_PACKED_FILE_NOT_EXISTS, packedFilePath));
            }

            return returnedSize;
        }

        /// <summary>
        /// Returns type description for specified packed file path
        /// </summary>
        /// <param name="packedFilePath"></param>
        /// <returns></returns>
        public string GetPackedFileTypeDescription(string packedFilePath)
        {
            string returnedDesc = "";

            if (!string.IsNullOrEmpty(packedFilePath))
            {
                string packedFileName = GetPackedFileName(packedFilePath);

                returnedDesc = GetTypeDescription(packedFileName);
            }

            return returnedDesc;
        }

        /// <summary>
        /// Returns list of packed file names having specified extension
        /// </summary>
        /// <param name="extension">extension of searched files, NOT including point. If set to null or empty, all files are returned.</param>
        /// <returns></returns>
        public Collection<string> GetPackedFilesPathsByExtension(string extension)
        {
            Collection<string> resultList = new Collection<string>();

            // Parcours de la liste de fichiers
            foreach (PackedFile anotherFile in _FileList)
            {
                if (anotherFile.fileName != null
                    && !anotherFile.fileName.StartsWith(_PADDING_FILE_NAME)
                    && !anotherFile.fileName.StartsWith(_UNKNOWN_FILE_NAME))
                {
                    string anotherExtension = File2.GetExtensionFromFilename(anotherFile.fileName);

                    if (string.IsNullOrEmpty(extension)
                        || extension.Equals(anotherExtension, StringComparison.InvariantCultureIgnoreCase))
                        resultList.Add(anotherFile.filePath);
                }
            }
            return resultList;
        }       
        
        /// <summary>
        /// Returns list of all packed files paths, or having specified file name
        /// </summary>
        /// <param name="packedFileName">can be null, to return all available paths</param>
        /// <returns></returns>
        public Collection<string> GetPackedFilesPaths(string packedFileName)
        {
            Collection<string> resultList = new Collection<string>();

            // Parcours de la liste de fichiers
            foreach (PackedFile anotherFile in _FileList)
            {
                if (anotherFile.filePath != null)
                {
                    if (packedFileName == null 
                        || (packedFileName.Equals(anotherFile.fileName, StringComparison.InvariantCultureIgnoreCase)))
                    resultList.Add(anotherFile.filePath);
                }
            }

            return resultList;
        }

        /// <summary>
        /// Returns name of packed file from its path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string GetPackedFileName(string filePath)
        {
            string returnedFileName = null;

            if (!string.IsNullOrEmpty(filePath))
            {
                PackedFile file = _FileByPathList[filePath];

                if (file != null)
                    returnedFileName = file.fileName;
            }

            return returnedFileName;
        }
        #endregion

        #region Overrides of TduFile



        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using DjeFramework1.Common.Support.Meta;
using DjeFramework1.Common.Types;
using DjeFramework1.Common.Types.Collections;
using DjeFramework1.Util.BasicStructures;
using TDUModdingLibrary.support;

namespace TDUModdingLibrary.fileformats.binaries
{
    /// <summary>
    /// Represents camera data stored in cameras.bin file
    /// </summary>
    public class Cameras : TduFile
    {
        #region Constants
        /// <summary>
        /// Pattern for file name (to redefine)
        /// </summary>
        public new const string FILENAME_PATTERN = @"cameras\.bin$";

        /// <summary>
        /// File name for camera data
        /// </summary>
        public const string FILE_CAMERAS_BIN = @"\cameras.bin";

        /// <summary>
        /// Size of header
        /// </summary>
        private const int HeaderSize = 0x20;
        #endregion

        #region Enums
        /// <summary>
        /// List of all vehicle types
        /// </summary>
        public enum VehicleType
        {
            Car, Bike, Unknown
        };

        /// <summary>
        /// List of all camera views
        /// </summary>
        public enum ViewType
        {
            Follow_Near = 20,
            Follow_Far = 21,
            Bumper = 22,
            Cockpit = 23,
            Hood = 24,
            Follow_Large = 25,
            Follow_Near_Back = 40,
            Follow_Far_Back = 41,
            Bumper_Back = 42,
            Cockpit_Back = 43,
            Hood_Back = 44,
            Follow_Large_Back = 45,
            Unknown = 0
        };

        /// <summary>
        /// Camera source and target positions
        /// </summary>
        public enum Position
        {
            Left = 62, Middle = 0, Right = -66, Unknown = -1
        }
        #endregion

        #region Structures
        /// <summary>
        /// Represents data for a camera entry
        /// </summary>
        public struct CamEntry
        {
            public List<View> views;
            public ushort id;
            public bool isValid;
        }

        /// <summary>
        /// Represents data for a camera view
        /// </summary>
        public struct View
        {
            internal byte[] unknown1;
            internal byte[] unknown2;
            internal uint tag;
            public ushort cameraId;
            public ushort parentCameraId;
            public ViewType type;
            public ViewType parentType;
            internal string name;
            public bool isValid;
            public Position source;
            public Position target;
        }
        #endregion

        #region Properties
        /// <summary>
        /// List of all cam entries
        /// </summary>
        public List<CamEntry> Entries
        {
            get { return _entries; }
        }
        private readonly List<CamEntry> _entries = new List<CamEntry>();

        /// <summary>
        /// Index by camera id : view count. Do not modify this or the game will crash !
        /// </summary>
        public Dictionary<ushort, ushort> Index
        {
            get { return _index; }
        }
        private readonly Dictionary<ushort, ushort> _index = new Dictionary<ushort, ushort>();

        private int _ZeroZoneSize
        {
            get { return 12 + (_index.Count - 3) * 16; }
        }
        #endregion

        #region Members
        /// <summary>
        /// Cameras data header
        /// </summary>
        private byte[] _header;

        /// <summary>
        /// (TODO) What's that ?
        /// </summary>
        private uint _unknown1;
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        internal Cameras() {}

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="camFileName">Name of file providing camera data</param>
        internal Cameras(string camFileName)
        {
            _FileName = camFileName;

            _ReadData();
        }

        #region TduFile implementation
        protected sealed override void _ReadData()
        {
            using (BinaryReader reader = new BinaryReader(new FileStream(_FileName, FileMode.Open, FileAccess.Read)))
            {
                // Header (TODO)
                _header = reader.ReadBytes(HeaderSize);

                // Index size (2 bytes)
                ushort indexSize = reader.ReadUInt16();

                // Zero1 (2 bytes)
                reader.ReadBytes(2);

                // Unknown1 ... (4 bytes) ... always=40
                _unknown1 = reader.ReadUInt32();

                // Cam index
                _index.Clear();

                for (int i = 0; i < indexSize; i++ )
                {
                    // Camera id (2 bytes)
                    ushort camId = reader.ReadUInt16();

                    // Zero1 (2 bytes)
                    reader.ReadBytes(2);

                    // View count (2 bytes, ignored)
                    ushort viewCount = reader.ReadUInt16();

                    // Zeroes (6 bytes)
                    reader.ReadBytes(6);

                    _index.Add(camId, viewCount);
                }

                // Zero zone
                reader.ReadBytes(_ZeroZoneSize);

                // Beginning of interesting zone
                CamEntry currentEntry = new CamEntry();
                List<ushort> processedIds = new List<ushort>();

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    View newView = new View();

                    // Zero1 (4 bytes)
                    reader.ReadBytes(4);

                    // Unknown1 (180 bytes)
                    newView.unknown1 = reader.ReadBytes(0xB4);

                    // Tag (?)
                    newView.tag = reader.ReadUInt32();

                    // Camera id
                    ushort currentId = reader.ReadUInt16();

                    if (!processedIds.Contains(currentId))
                    {
                        // New camera entry: saving latest entry first
                        if (processedIds.Count > 0)
                        {
                            currentEntry.isValid = true;
                            _entries.Add(currentEntry);
                        }

                        // Creating new entry
                        currentEntry = new CamEntry {id = currentId, views = new List<View>()};
                        processedIds.Add(currentId);
                    }

                    newView.cameraId = currentId;

                    // Zero2 (2 bytes)
                    reader.ReadBytes(2);

                    // View type
                    ushort currentViewType = reader.ReadUInt16();

                    newView.type = (ViewType) Enum.Parse(typeof (ViewType), currentViewType.ToString());

                    // Zero3 (2 bytes)
                    reader.ReadBytes(2);

                    // View name
                    byte[] nameBytes = reader.ReadBytes(16);

                    newView.name = _GetViewName(nameBytes);

                    // Custom properties hack
                    Couple<string> customProps = _GetViewProperties(nameBytes);

                    if (customProps != null)
                    {
                        newView.parentCameraId = ushort.Parse(customProps.FirstValue);
                        newView.parentType = (ViewType) Enum.Parse(typeof (ViewType), customProps.SecondValue, false);
                    }

                    // Unknown2 (420 bytes)
                    newView.unknown2 = reader.ReadBytes(0x1A4);
                    // Source and target positions
                    newView.source = (Position) ((sbyte)newView.unknown2[195]);
                    newView.target = (Position) ((sbyte)newView.unknown2[211]);

                    // Adding view to existing set
                    newView.isValid = true;
                    currentEntry.views.Add(newView);
                }

                // Finalization
                currentEntry.isValid = true;
                _entries.Add(currentEntry);

                // EVO_65: Properties
                Property.ComputeValueDelegate camCountDelegate = () => _entries.Count.ToString();

                Properties.Add(new Property("Camera sets count", "CAM-BIN", camCountDelegate));
            }
        }
        
        /// <summary>
        /// Writes camera file back
        /// </summary>
        public override void Save()
        {
            // Removing read-only attribute
            if (File.Exists(_FileName))
            {
                File2.RemoveAttribute(_FileName, FileAttributes.ReadOnly);
            }

            using (BinaryWriter writer = new BinaryWriter(new FileStream(_FileName, FileMode.Create, FileAccess.Write)))
            {
                // Header 
                _WriteHeader(writer);

                // Camera index
                _WriteIndex(writer);

                // Browsing camera sets
                foreach (CamEntry anotherEntry in _entries)
                {
                    if (anotherEntry.isValid)
                    {
                        // Views
                        foreach (View anotherView in anotherEntry.views)
                        {
                            // Zero1 (4 bytes)
                            writer.Write(0);

                            // Unknown1 (180 bytes)
                            writer.Write(anotherView.unknown1);

                            // Tag (?)
                            writer.Write(anotherView.tag);

                            // Camera id
                            writer.Write(anotherView.cameraId);

                            // Zero2 (2 bytes)
                            writer.Write((short) 0);

                            // View type
                            writer.Write((short) anotherView.type);

                            // Zero3 (2 bytes)
                            writer.Write((short) 0);

                            // View name + custom properties
                            writer.Write(_SetViewName(anotherView));

                            // Unknown2 (420 bytes)
                                // Positions : 195>source, 211>target
                            anotherView.unknown2[195] = (byte)anotherView.source;
                            anotherView.unknown2[211] = (byte)anotherView.target;
                            writer.Write(anotherView.unknown2);
                        }
                    }
                }
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Returns camera entry associated to specified id
        /// </summary>
        /// <param name="camId"></param>
        /// <returns></returns>
        public CamEntry GetEntryByCameraId(string camId)
        {
            CamEntry returnedEntry = new CamEntry();
            
            if (!string.IsNullOrEmpty(camId))
            {
                ushort id = ushort.Parse(camId);

                foreach (CamEntry anotherCamEntry in _entries)
                {
                    if (anotherCamEntry.isValid && anotherCamEntry.id == id)
                    {
                        returnedEntry = anotherCamEntry;
                        break;
                    }
                }
            }
            
            return returnedEntry;
        }

        /// <summary>
        /// Returns view from specified entry according to its type
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="searchedType"></param>
        /// <returns></returns>
        public static View GetViewByType(CamEntry entry, ViewType searchedType)
        {
            View returnedView = new View();

            if (entry.isValid)
            {
                foreach (View anotherView in entry.views)
                {
                    if (anotherView.type == searchedType)
                    {
                        returnedView = anotherView;
                        break;
                    }
                }
            }

            return returnedView;
        }
        
        /// <summary>
        /// Allow to update a camera entry
        /// </summary>
        /// <param name="modifiedEntry"></param>
        public void UpdateEntry(CamEntry modifiedEntry)
        {
            if (modifiedEntry.isValid)
            {
                // Browsing entries
                bool isFinished = false;

                for (int i = 0 ; !isFinished && i < Entries.Count ; i++)
                {
                    CamEntry anotherEntry = Entries[i];

                    if (anotherEntry.id == modifiedEntry.id)
                    {
                        Entries[i] = modifiedEntry;
                        isFinished = true;
                    }
                }
            }
        }

        /// <summary>
        /// Removes view for specified entry
        /// </summary>
        /// <param name="currentEntry"></param>
        /// <param name="currentViewType"></param>
        public void RemoveView(CamEntry currentEntry, ViewType currentViewType)
        {
            if (currentEntry.isValid && currentViewType != ViewType.Unknown)
            {
                View viewToRemove = GetViewByType(currentEntry, currentViewType);

                if (viewToRemove.isValid)
                {
                    currentEntry.views.Remove(viewToRemove);
                    UpdateEntry(currentEntry);
                }
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Utility method returning clean view name from a byte array
        /// </summary>
        /// <param name="currentViewName"></param>
        /// <returns></returns>
        private static string _GetViewName(byte[] currentViewName)
        {
            string returnedName = "";

            if (currentViewName != null)
            {
                string dirtyName = Array2.BytesToString(currentViewName);

                // Removing every character after the ASCII 0
                int zeroPos = dirtyName.IndexOf('\0');

                returnedName = dirtyName.Substring(0, zeroPos);
            }

            return returnedName;
        }

        /// <summary>
        /// Utility methods returning custom view properties from byte array containing view name
        /// </summary>
        /// <param name="currentViewName"></param>
        /// <returns></returns>
        private static Couple<string> _GetViewProperties(byte[] currentViewName)
        {
            Couple<string> returnedProps = null;

            if (currentViewName != null)
            {
                string dirtyName = Array2.BytesToString(currentViewName);

                // Getting every character after the ASCII 0
                int zeroPos = dirtyName.IndexOf('\0');
                string props = dirtyName.Substring(zeroPos + 1);

                if (props.Contains(new string(Tools.SYMBOL_VALUE_SEPARATOR, 1)))
                {
                    // Properties are set
                    string[] splittedProps = props.Split(Tools.SYMBOL_VALUE_SEPARATOR);

                    if (splittedProps.Length == 2)
                    {
                        // Cleaning props
                        string parentId = splittedProps[0];
                        string parentView = (splittedProps[1].Length == 2) ?
                            splittedProps[1] : 
                            splittedProps[1].Substring(0, splittedProps[1].IndexOf((char)0xCD));

                        returnedProps = new Couple<string>(parentId, parentView);
                    }
                }
            }

            return returnedProps;   
        }

        /// <summary>
        /// Utility method returning internal view name from a clean one + custom properties (if necessary)
        /// </summary>
        /// <param name="currentView"></param>
        /// <returns></returns>
        private static byte[] _SetViewName(View currentView)
        {
            byte[] returnedBytes = new byte[16];

            if (currentView.isValid)
            {
                string currentViewName = currentView.name;
                int index = 0;

                foreach (char anotherCharacter in currentViewName)
                {
                    returnedBytes[index] = (byte) anotherCharacter;
                    index++;
                }

                // Finalization of string
                returnedBytes[index] = (byte)'\0';
                index++;

                // Properties for custom camera, if there's enough place to store them...
                if (currentView.parentCameraId != 0 && index <= 8)
                {
                    // Parent id
                    string parentCameraId = currentView.parentCameraId.ToString();
                    byte[] idArray = String2.ToByteArray(parentCameraId);

                    Array.Copy(idArray, 0, returnedBytes, index, idArray.Length);
                    index += idArray.Length;

                    // Separator
                    returnedBytes[index] = (byte) Tools.SYMBOL_VALUE_SEPARATOR;
                    index++;

                    // Parent type
                    string parentType = ((int) currentView.parentType).ToString();
                    byte[] typeArray = String2.ToByteArray(parentType);

                    Array.Copy(typeArray, 0, returnedBytes, index, typeArray.Length);
                    index += typeArray.Length;
                }

                // Padding
                for (int i = index; i < 16; i++)
                    returnedBytes[i] = 0xCD;
            }

            return returnedBytes;
        }

        /// <summary>
        /// Handles writing of header
        /// </summary>
        /// <param name="writer"></param>
        private void _WriteHeader(BinaryWriter writer)
        {
            if (writer != null)
            {
                // Head data (32 bytes)
                writer.Write(_header);

                // Index size (2 bytes)
                writer.Write((ushort) _index.Count);

                // zero1 (2 bytes)
                writer.Write((ushort)0x0);

                // unknown1 (4 bytes)
                writer.Write(_unknown1);
            }
        }

        /// <summary>
        /// Handles writing of camera index
        /// </summary>
        /// <param name="writer"></param>
        private void _WriteIndex(BinaryWriter writer)
        {
            if (writer != null)
            {
                foreach(ushort camId in _index.Keys)
                {
                    // camId (2 bytes)
                    writer.Write(camId);

                    // zero1 (2 bytes)
                    writer.Write((ushort)0x0);

                    // viewCount (2 bytes)
                    writer.Write(_index[camId]);
                    
                    // zeroes (6 bytes)
                    writer.Write(new byte[6]);
                }

                // Remaining space: blocks of 12 bytes
                byte[] finalZeroes = new byte[_ZeroZoneSize];

                writer.Write(finalZeroes);
            }
        }
        #endregion
    }
}

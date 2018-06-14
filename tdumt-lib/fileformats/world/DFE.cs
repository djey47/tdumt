using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DjeFramework1.Common.Calculations;
using DjeFramework1.Common.Support.Meta;
using DjeFramework1.Common.Types;
using DjeFramework1.Common.Types.Collections;

namespace TDUModdingLibrary.fileformats.world
{
    public class DFE : TduFile
    {
        #region Enums
        /// <summary>
        /// Challenge modes
        /// </summary>
        public enum GameMode
        {
            Time = 0,
            Speed = 1,
            WeirdRide = 2,
            Race = 3,
            Unhandled = 4
        }

        /// <summary>
        /// Challenge types
        /// </summary>
        public enum GameType
        {
            Offline = 49344,
            Online = 49345,
            Club = 49346,
            Unhandled = 0
        }

        /// <summary>
        /// Driving aids
        /// </summary>
        public enum DrivingAid
        {
            Off = 0,
            Hypersport = 1,
            Sport = 2,
            Assisted = 3,
            Unhandled = 4
        }

        #endregion

        #region Structures
        /// <summary>
        /// 
        /// </summary>
        protected struct ParameterInfo
        {
            internal string code;
            internal byte[] value;
            internal byte[] unknown;
        }
        #endregion

        #region Constants
        /// <summary>
        /// Pattern for file name (to redefine)
        /// </summary>
        public new const string FILENAME_PATTERN = FILE_DATA_DFE + String2.REGEX_PATTERN_STARTS_ENDS_WITH;

        /// <summary>
        /// File name (starting) for tracks
        /// </summary>
        public const string FILE_DATA_DFE = "data_dfe_";

        /// <summary>
        /// Size of header
        /// </summary>
        protected const int _HEADER_SIZE = 0x10;

        /// <summary>
        /// Position of parameter count in header
        /// </summary>
        protected const int _HEADER_PARAM_COUNT_OFFSET = 0xF;

        /// <summary>
        /// Length of description parameter
        /// </summary>
        private const int _DESCRIPTION_PARAM_VALUE_LENGTH = 64;

        /// <summary>
        /// Size of waypoint data
        /// </summary>
        protected const int _WAYPOINT_SIZE = 0x60;

        /// <summary>
        /// Size of car placement data
        /// </summary>
        protected const int _CAR_PLACEMENT_SIZE = 0x5C;

        /// <summary>
        /// Size of trace information data
        /// </summary>
        protected const int _TRACE_INFO_SIZE = 0xC;

        /// <summary>
        /// Parameter: track name (mnme)
        /// </summary>
        private const string _NAME_TRACK_PARAM = "mnme";

        /// <summary>
        /// Parameter: db resource for track name (dbna)
        /// </summary>
        private const string _DB_NAME_TRACK_PARAM = "dbna";

        /// <summary>
        /// Parameter: challenge mode (gmth)
        /// </summary>
        private const string _GAME_MODE_TRACK_PARAM = "gmth";

        /// <summary>
        /// Parameter: challenge type (gmpm)
        /// </summary>
        private const string _GAME_TYPE_TRACK_PARAM = "gmpm";

        /// <summary>
        /// Parameter: description (DesC)
        /// </summary>
        private const string _DESCRIPTION_PARAM = "DesC";

        /// <summary>
        /// Parameter: gps (ChGP)
        /// </summary>
        private const string _USE_GPS_PARAM = "ChGP";

        /// <summary>
        /// Parameter: use radars in speed mode? (ChST)
        /// </summary>
        private const string _USE_RADARS_PARAM = "ChST";

        /// <summary>
        /// Parameter: force cockpit view (Cock)
        /// </summary>
        private const string _FORCE_COCKPIT_PARAM = "Cock";

        /// <summary>
        /// Parameter: ebnable driving gauge (offR)
        /// </summary>
        private const string _DRIVING_GAUGE_ENABLE_PARAM = "offR";

        /// <summary>
        /// Parameter: ebnable driving gauge (offR)
        /// </summary>
        private const string _DRIVING_GAUGE_PTS_PARAM = "PeCr";

        /// <summary>
        /// Parameter: enable route markers (forc)
        /// </summary>
        private const string _MARKED_ROUTE_PARAM = "forc";

        /// <summary>
        /// Parameter: traffic is present (ChTR)
        /// </summary>
        private const string _TRAFFIC_PARAM = "ChTR";

        /// <summary>
        /// Parameter: traffic level (trat)
        /// </summary>
        private const string _TRAFFIC_LEVEL_PARAM = "trat";

        /// <summary>
        /// Parameter: loop race (   #)
        /// </summary>
        private const string _LOOP_RACE_PARAM = "\0\0\0#";

        /// <summary>
        /// Parameter: lap count (nblp)
        /// </summary>
        private const string _LAP_COUNT_PARAM = "nblp";

        /// <summary>
        /// Parameter: speed to reach in time challenges - no radar (SP2R)
        /// </summary>
        private const string _SPEED_TO_REACH_PARAM = "SP2R";

        /// <summary>
        /// Parameter: use driving gauge in solo (offR)
        /// </summary>
        private const string _GAUGE_PARAM = "offR";

        /// <summary>
        /// Parameter: with cops (ChCO)
        /// </summary>
        private const string _COPS_PARAM = "ChCO";

        /// <summary>
        /// Parameter: cops level (cops)
        /// </summary>
        private const string _COPS_DENSITY_PARAM = "cops";

        /// <summary>
        /// Parameter: driving aids (DrAi)
        /// </summary>
        private const string _DRIVING_AID_PARAM = "DrAi";

        /// <summary>
        /// Parameter: number of bots (NbIA)
        /// </summary>
        private const string _BOTS_NB_PARAM = "NbIA";
        #endregion

        #region Properties
        /// <summary>
        /// Track name (default)
        /// </summary>
        public string TrackName
        {
            get { return _ExtractName(_GetParameter(_NAME_TRACK_PARAM));}
        }

        /// <summary>
        /// Database resource id to check for track name
        /// </summary>
        public string TrackNameDbResource
        {
            get { return _Extract4Bytes(_GetParameter(_DB_NAME_TRACK_PARAM));}
        }

        /// <summary>
        /// Game mode (time, speed, ...)
        /// </summary>
        public GameMode ChallengeMode
        {
            get
            {
                string val = _Extract4Bytes(_GetParameter(_GAME_MODE_TRACK_PARAM));
                
                return (GameMode) Enum.Parse(typeof(GameMode), val);
            }
            set
            {
                byte[] val = _BuildChallengeMode(value);

                _SetParameter(_GAME_MODE_TRACK_PARAM, val);
            }
        }

        /// <summary>
        /// Game type (solo, multiplayer, club)
        /// </summary>
        public GameType ChallengeType
        {
            get
            {
                string val = _Extract4Bytes(_GetParameter(_GAME_TYPE_TRACK_PARAM));

                return (GameType)Enum.Parse(typeof(GameType), val);
            }
            set
            {
                byte[] val = _BuildChallengeType(value);

                _SetParameter(_GAME_TYPE_TRACK_PARAM, val);
            }
        }

        /// <summary>
        /// Driving aids (assisted > off)
        /// </summary>
        public DrivingAid DrivingAids
        {
            get
            {
                string val = _Extract4Bytes(_GetParameter(_DRIVING_AID_PARAM));

                return (DrivingAid)Enum.Parse(typeof(DrivingAid), val);
            }
            set
            {
                byte[] val = _BuildDrivingAid(value);

                _SetParameter(_DRIVING_AID_PARAM, val);
            }
        }

        /// <summary>
        /// GPS to assist player?
        /// </summary>
        public bool UseGPS
        {
            get { return _ExtractBool(_GetParameter(_USE_GPS_PARAM));}
            set
            {
                byte[] val = _BuildBool(value);

                _SetParameter(_USE_GPS_PARAM, val);
            }
        }

        /// <summary>
        /// Radars in speed challenge ?
        /// </summary>
        public bool WithRadars
        {
            get { return _ExtractBool(_GetParameter(_USE_RADARS_PARAM)); }
            set
            {
                byte[] val = _BuildBool(value);

                _SetParameter(_USE_RADARS_PARAM, val);
            }
        }

        /// <summary>
        /// Cops in solo ?
        /// </summary>
        public bool WithCops
        {
            get { return _ExtractBool(_GetParameter(_COPS_PARAM)); }
            set
            {
                byte[] val = _BuildBool(value);

                _SetParameter(_COPS_PARAM, val);
            }
        }

        /// <summary>
        /// Driving view is cockpit only
        /// </summary>
        public bool ForceCockpitView
        {
            get { return _ExtractBool(_GetParameter(_FORCE_COCKPIT_PARAM)); }
            set
            {
                byte[] val = _BuildBool(value);

                _SetParameter(_FORCE_COCKPIT_PARAM, val);
            }
        }

        /// <summary>
        /// Driving gauge is enabled. No effect in multiplayer, lobby setting is always used
        /// </summary>
        public bool DrivingGaugeEnabled
        {
            get { return _ExtractBool(_GetParameter(_DRIVING_GAUGE_ENABLE_PARAM)); }
            set
            {
                byte[] val = _BuildBool(value);

                _SetParameter(_DRIVING_GAUGE_ENABLE_PARAM, val);
            }
        }

        /// <summary>
        /// Amount of gauge points
        /// </summary>
        public uint DrivingGaugePoints
        {
            get
            {
                string val = _Extract4Bytes(_GetParameter(_DRIVING_GAUGE_PTS_PARAM));

                return uint.Parse(val);
            }
            set
            {
                byte[] val = _Build4Bytes(value);

                _SetParameter(_DRIVING_GAUGE_PTS_PARAM, val);
            }
        }

        /// <summary>
        /// There are blocks along the road
        /// </summary>
        public bool MarkedRoute
        {
            get { return _ExtractBool(_GetParameter(_MARKED_ROUTE_PARAM)); }
            set
            {
                byte[] val = _BuildBool(value);

                _SetParameter(_MARKED_ROUTE_PARAM, val);
            }
        }

        /// <summary>
        /// Traffic is there
        /// </summary>
        public bool WithTraffic
        {
            get { return _ExtractBool(_GetParameter(_TRAFFIC_PARAM)); }
            set
            {
                byte[] val = _BuildBool(value);

                _SetParameter(_TRAFFIC_PARAM, val);
            }
        }

        /// <summary>
        /// Driving gauge (solo only)
        /// </summary>
        public bool WithDrivingGauge
        {
            get { return _ExtractBool(_GetParameter(_GAUGE_PARAM)); }
            set
            {
                byte[] val = _BuildBool(value);

                _SetParameter(_GAUGE_PARAM, val);
            }
        }

        /// <summary>
        /// Amount of traffic
        /// </summary>
        public uint TrafficLevel
        {
            get
            {
                string val = _Extract4Bytes(_GetParameter(_TRAFFIC_LEVEL_PARAM));

                return uint.Parse(val); 
            }
            set
            {
                byte[] val = _Build4Bytes(value);

                _SetParameter(_TRAFFIC_LEVEL_PARAM, val);
            }
        }

        /// <summary>
        /// Amount of traffic
        /// </summary>
        public uint CopsLevel
        {
            get
            {
                string val = _Extract4Bytes(_GetParameter(_COPS_DENSITY_PARAM));

                return uint.Parse(val);
            }
            set
            {
                byte[] val = _Build4Bytes(value);

                _SetParameter(_COPS_DENSITY_PARAM, val);
            }
        }

        /// <summary>
        /// Traffic is there
        /// </summary>
        public bool WithLaps
        {
            get { return _ExtractBool(_GetParameter(_LOOP_RACE_PARAM)); }
            set
            {
                byte[] val = _BuildBool(value);

                _SetParameter(_LOOP_RACE_PARAM, val);
            }
        }

        /// <summary>
        /// Amount of traffic
        /// </summary>
        public uint LapCount
        {
            get
            {
                string val = _Extract4Bytes(_GetParameter(_LAP_COUNT_PARAM));

                return uint.Parse(val);
            }
            set
            {
                byte[] val = _Build4Bytes(value);

                _SetParameter(_LAP_COUNT_PARAM, val);
            }
        }

        /// <summary>
        /// Number of bots (solo only)
        /// </summary>
        public uint BotCount
        {
            get
            {
                string val = _Extract4Bytes(_GetParameter(_BOTS_NB_PARAM));

                return uint.Parse(val);
            }
            set
            {
                byte[] val = _Build4Bytes(value);

                _SetParameter(_BOTS_NB_PARAM, val);
            }
        }

        /// <summary>
        /// DFE track identifier
        /// </summary>
        public string TrackId
        {
            get { return (new FileInfo(_FileName).Name); }
        }

        /// <summary>
        /// Track description
        /// </summary>
        public string Description
        {
            get { return _ExtractName(_GetParameter(_DESCRIPTION_PARAM));}
            set
            {
                byte[] val = _BuildName(value, _DESCRIPTION_PARAM_VALUE_LENGTH);

                _SetParameter(_DESCRIPTION_PARAM, val);
            }
        }

        /// <summary>
        /// Speed to reach in time challenges
        /// </summary>
        public uint SpeedToReach
        {
            get
            {
                string val = _Extract4Bytes(_GetParameter(_SPEED_TO_REACH_PARAM));

                return (uint.Parse(val) / 100);
            }
            set
            {
                byte[] val = _Build4Bytes(value * 100);

                _SetParameter(_SPEED_TO_REACH_PARAM, val);
            }
        }

        /// <summary>
        /// Number of checkpoints / radars. Start line does not count as checkpoint
        /// </summary>
        public ushort CheckpointCount { get; set;}
        #endregion

        #region Private fields
        /// <summary>
        /// List of all parameters values by code
        /// </summary>
        protected readonly Dictionary<string, ParameterInfo> _Parameters = new Dictionary<string, ParameterInfo>();

        /// <summary>
        /// Data about start line spot
        /// </summary>
        protected byte[] _StartingSpotData;

        /// <summary>
        /// Data about car placement
        /// </summary>
        protected byte[] _CarPlacementData;

        /// <summary>
        /// Information about tracé
        /// </summary>
        protected byte[] _TraceInformationData;

        /// <summary>
        /// Data about waypoints
        /// </summary>
        protected byte[] _WayPointData;

        /// <summary>
        /// Header
        /// </summary>
        protected byte[] _HeaderData;
        #endregion

        #region Overrides of TduFile
        /// <summary>
        /// Reads file data.
        /// </summary>
        protected override void _ReadData()
        {
            using (BinaryReader reader = new BinaryReader(new FileStream(_FileName, FileMode.Open, FileAccess.Read)))
            {
                // Header
                _HeaderData = _ReadHeader(reader);

                // Parameters
                // Parameter count @ 0xF
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

                    ParameterInfo newParam = new ParameterInfo {code = code, unknown = unknown, value = value};

                    _Parameters.Add(code, newParam);
                }

                // Tracé
                _ReadTraceData(reader);

                // EVO_65: Properties              
                _SetProperties();
            }
        }

        /// <summary>
        /// Saves the current file to disk.
        /// </summary>
        public override void Save()
        {
            //
            using (BinaryWriter writer = new BinaryWriter(new FileStream(_FileName, FileMode.Create, FileAccess.Write)))
            {
                // Header
                _WriteHeader(writer);
       
                // Parameters
                foreach (KeyValuePair<string, ParameterInfo> anotherParameter in _Parameters)
                {
                    // Code : 4 chars
                    byte[] code = _BuildName(anotherParameter.Key, 4);

                    writer.Write(code);
                    // Unknwown : 2 bytes
                    writer.Write(anotherParameter.Value.unknown);
                    // Value length : 2 bytes
                    short length = BinaryTools.ToBigEndian((short) anotherParameter.Value.value.Length);

                    writer.Write(length);
                    // Zeros : 4 bytes
                    writer.Write(0);
                    // Value
                    writer.Write(anotherParameter.Value.value);
                }

                // Info about tracé
                _WriteTraceInformation(writer);

                // Starting spot
                writer.Write(_StartingSpotData);

                // Car placement
                writer.Write(_CarPlacementData);

                // Waypoints
                writer.Write(_WayPointData);
            }
        }
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        internal DFE() {}

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="dfeFileName">Name of file providing camera data</param>
        internal DFE(string dfeFileName)
        {
            _FileName = dfeFileName;

            _ReadData();
        }

        #region Public methods
        /// <summary>
        /// Copies waypoints from additional track to end of current challenge
        /// </summary>
        /// <param name="additionalTrack"></param>
        public void MergeCheckpoints(DFE additionalTrack)
        {
            if (additionalTrack != null)
            {
                // Creating new waypoint data
                byte[] newWaypointData = new byte[_WayPointData.Length + additionalTrack._WayPointData.Length];

                using (BinaryWriter writer = new BinaryWriter(new MemoryStream(newWaypointData)))
                {
                    writer.Write(_WayPointData);
                    writer.Write(additionalTrack._WayPointData);
                }

                _WayPointData = newWaypointData;

                // Updating waypoint count
                CheckpointCount += additionalTrack.CheckpointCount;
            }
        }
        #endregion

        #region Protected methods
        /// <summary>
        /// Reads file header and returns parameter count
        /// </summary>
        protected byte[] _ReadHeader(BinaryReader currentReader)
        {
            byte[] returnedData = new byte[0];

            if (currentReader != null)
                returnedData = currentReader.ReadBytes(_HEADER_SIZE);

            return returnedData;
        }

        /// <summary>
        /// Reads and store information about tracé
        /// </summary>
        /// <param name="reader"></param>
        protected void _ReadTraceData(BinaryReader reader)
        {
            if (reader != null)
            {
                // Must be read in that order
                _TraceInformationData = _ReadTraceInformation(reader);
                _StartingSpotData = _ReadStartingSpot(reader);
                _CarPlacementData = _ReadCarPlacement(reader);
                _WayPointData = _ReadWayPoints(reader);
            }
        }

        /// <summary>
        /// Defines file properties
        /// </summary>
        protected void _SetProperties()
        {
            Property.ComputeValueDelegate wpCountDelegate = () => CheckpointCount.ToString();
            Property.ComputeValueDelegate modeDelegate = () => ChallengeMode.ToString();
            Property.ComputeValueDelegate typeDelegate = () => ChallengeType.ToString();
            Property.ComputeValueDelegate nameDelegate = () => TrackName;

            Properties.Add(new Property("Track name", "DFE/IGE", nameDelegate));
            Properties.Add(new Property("Challenge mode", "DFE/IGE", modeDelegate));
            Properties.Add(new Property("Challenge type", "DFE/IGE", typeDelegate));
            Properties.Add(new Property("Waypoint count", "DFE/IGE", wpCountDelegate));
        }

        /// <summary>
        /// Updates header information
        /// </summary>
        /// <param name="currentWriter"></param>
        protected void _WriteHeader(BinaryWriter currentWriter)
        {
            if (currentWriter != null)
            {
                // Param count @ 15 bytes from start
                _HeaderData[_HEADER_PARAM_COUNT_OFFSET] = (byte)_Parameters.Count;

                currentWriter.Write(_HeaderData);
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Reads and set general info about tracé
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private byte[] _ReadTraceInformation(BinaryReader reader)
        {
            byte[] readData = reader.ReadBytes(_TRACE_INFO_SIZE);

            using (BinaryReader memReader = new BinaryReader(new MemoryStream(readData)))
            {
                // Waypoint count @ 0xA : start line + car placement + intermediary way points
                memReader.BaseStream.Seek(0xA, SeekOrigin.Begin);
                CheckpointCount = (ushort) (BinaryTools.ToBigEndian(memReader.ReadInt16()) - 2);
            }

            return readData;
        }

        /// <summary>
        /// Updates and writes general info about tracé
        /// </summary>
        /// <param name="writer"></param>
        private void _WriteTraceInformation(BinaryWriter writer)
        {
            // Updating waypoint data
            using (BinaryWriter memWriter = new BinaryWriter(new MemoryStream(_TraceInformationData)))
            {
                ushort count = (ushort) (BinaryTools.ToLittleEndian((short) (CheckpointCount+2)));

                // Waypoint count @ 0xA : waypoints + car placement + start line
                memWriter.BaseStream.Seek(0xA, SeekOrigin.Begin);
                memWriter.Write(count);
            }

            writer.Write(_TraceInformationData);
        }

        /// <summary>
        /// Reads information about car placement
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static byte[] _ReadCarPlacement(BinaryReader reader)
        {
            byte[] readData = reader.ReadBytes(_CAR_PLACEMENT_SIZE);

            return readData;
        }

        /// <summary>
        /// Reads information about start line spoty
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static byte[] _ReadStartingSpot(BinaryReader reader)
        {
            byte[] readData = reader.ReadBytes(_WAYPOINT_SIZE);

            return readData;
        }

        /// <summary>
        /// Gets waypoint information then return read data
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static byte[] _ReadWayPoints(BinaryReader reader)
        {
            // Reads till end
            byte[] readData = reader.ReadBytes((int)(reader.BaseStream.Length - reader.BaseStream.Position));

            return readData;
        }

        /// <summary>
        /// Sets parameter value in index
        /// </summary>
        /// <param name="paramCode"></param>
        /// <param name="paramValue"></param>
        private void _SetParameter(string paramCode, byte[] paramValue)
        {
            if (!string.IsNullOrEmpty(paramCode) && paramValue != null)
            {
                ParameterInfo currentInfo = new ParameterInfo();

                if (_Parameters.ContainsKey(paramCode))
                    currentInfo = _Parameters[paramCode];
                else
                {
                    // New parameter
                    _Parameters.Add(paramCode, currentInfo);
                    currentInfo.code = paramCode;
                    currentInfo.unknown = new byte[2];
                }

                // New value
                currentInfo.value = paramValue;

                _Parameters[paramCode] = currentInfo;
            }
        }

        /// <summary>
        /// Gets parameter value from index
        /// </summary>
        /// <param name="paramCode"></param>
        private ParameterInfo _GetParameter(string paramCode)
        {
            ParameterInfo returnedValue = new ParameterInfo();

            if (!string.IsNullOrEmpty(paramCode) && _Parameters.ContainsKey(paramCode))
                returnedValue = _Parameters[paramCode];
                
            return returnedValue;
        }

        /// <summary>
        /// Builds boolean value to be written (4 bytes)
        /// </summary>
        /// <param name="useGps"></param>
        /// <returns></returns>
        private static byte[] _BuildBool(bool useGps)
        {
            byte[] returnedValue = new byte[4];

            returnedValue[3] = (useGps ? (byte)1 : (byte)0);

            return returnedValue;
        }

        /// <summary>
        /// Builds string name value to be written
        /// </summary>
        /// <param name="description"></param>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        private static byte[] _BuildName(IEnumerable<char> description, int maxSize)
        {
            byte[] returnedValue = new byte[maxSize];
            int i = 0;

            foreach (char c in description)
                returnedValue[i++] = (byte) c;

            return returnedValue;
        }

        /// <summary>
        /// Builds challenge type value to be written
        /// </summary>
        /// <param name="challengeType"></param>
        /// <returns></returns>
        private static byte[] _BuildChallengeType(GameType challengeType)
        {
            byte[] returnedValue = new byte[4];
            uint val = BinaryTools.ToBigEndian((uint)challengeType);

            Array2.Write4Bytes(returnedValue, 0, val);
            return returnedValue;
        }

        /// <summary>
        /// Builds challenge mode value to be written
        /// </summary>
        /// <param name="challengeMode"></param>
        /// <returns></returns>
        private static byte[] _BuildChallengeMode(GameMode challengeMode)
        {
            byte[] returnedValue = new byte[4];
            uint val = BinaryTools.ToBigEndian((uint)challengeMode);

            Array2.Write4Bytes(returnedValue, 0, val);
            return returnedValue;
        }

        /// <summary>
        /// Builds driving aids value to be written
        /// </summary>
        /// <param name="drivingAid"></param>
        /// <returns></returns>
        private static byte[] _BuildDrivingAid(DrivingAid drivingAid)
        {
            byte[] returnedValue = new byte[4];
            uint val = BinaryTools.ToBigEndian((uint)drivingAid);

            Array2.Write4Bytes(returnedValue, 0, val);
            return returnedValue;
        }

        /// <summary>
        /// Builds 4-byte value to be written
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private static byte[] _Build4Bytes(uint val)
        {
            byte[] returnedValue = new byte[4];

            val = BinaryTools.ToBigEndian(val);

            Array2.Write4Bytes(returnedValue, 0, val);
            return returnedValue;
        }

        /// <summary>
        /// Returns a boolean from specified 4-byte value
        /// </summary>
        /// <param name="paramInfo"></param>
        /// <returns></returns>
        private static bool _ExtractBool(ParameterInfo paramInfo)
        {
            bool returnedResult = false;
            byte[] value = paramInfo.value;

            if (value.Length == 4)
                returnedResult = (value[3] == 1);

            return returnedResult;
        }

        /// <summary>
        /// Returns 4-byte value
        /// </summary>
        /// <param name="paramInfo"></param>
        /// <returns></returns>
        private static string _Extract4Bytes(ParameterInfo paramInfo)
        {
            string returnedValue = "";
            byte[] value = paramInfo.value;

            if (value != null)
            {
                if (value.Length == 8)
                {
                    // Resource case > Only last 4 bytes are kept
                    value = new[]
                               {
                                   value[4], value[5], value[6], value[7]
                               };                    
                }

                using(BinaryReader reader = new BinaryReader(new MemoryStream(value)))
                {
                    uint readValue = BinaryTools.ToBigEndian(reader.ReadUInt32());

                    returnedValue = readValue.ToString(); 
                } 
            }

            return returnedValue;
        }

        /// <summary>
        /// Returns clean name from specified parameter information (removes eventual garbage)
        /// </summary>
        /// <param name="paramInfo"></param>
        /// <returns></returns>
        private static string _ExtractName(ParameterInfo paramInfo)
        {
            string returnedName = "";
            byte[] value = paramInfo.value;

            if (value != null)
            {
                StringBuilder sb = new StringBuilder();

                foreach (char anotherChar in value)
                {
                    if (anotherChar == '\0')
                        break;

                    sb.Append(anotherChar);
                }

                returnedName = sb.ToString();
            }

            return returnedName;
        }
        #endregion
    }
}
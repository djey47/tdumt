using System;
using System.IO;
using System.Xml;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.support.constants;
using TDUModdingLibrary.support.encryption;

namespace TDUModdingLibrary.fileformats.xml
{
    /// <summary>
    /// Represents an AI configuration file (XML+encryption based)
    /// </summary>
    class AIConfig:TduFile
    {
        #region Constants
        /// <summary>
        /// Pattern for file name (to redefine)
        /// </summary>
        public new const string FILENAME_PATTERN = @"AIConfig\.xml$";

        /// <summary>
        /// File name for AI configuration
        /// </summary>
        public const string FILE_AICONFIG_XML = @"\AIConfig.xml";

        /// <summary>
        /// frequency XML attribute
        /// </summary>
        private const string _ATTRIBUTE_FREQUENCY = "frequency";

        /// <summary>
        /// nb_traffic_per_lane_kilometer XML attribute
        /// </summary>
        private const string _ATTRIBUTE_NB_TRAFFIC_PER_KM = "nb_traffic_per_lane_kilometer";
        #endregion

        #region Members
        /// <summary>
        /// Embedded XML document
        /// </summary>
        private XmlDocument _Doc;

        /// <summary>
        /// Path and name of unencrypted file
        /// </summary>
        private string _WorkingFileName;

        /// <summary>
        /// Non-xml data in file, to be re-written 'as is'
        /// </summary>
        private byte[] _DirtyData;

        /// <summary>
        /// All traffic zone nodes
        /// </summary>
        private XmlNodeList _ZoneNodes;
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        internal AIConfig() {}

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="aiFileName">Name of file providing ai data</param>
        internal AIConfig(string aiFileName)
        {
            _FileName = aiFileName;

            _ReadData();
        }

        #region Overrides of TduFile
        /// <summary>
        /// Reads file data. This method should not be called from subclasses !
        /// </summary>
        protected override void _ReadData()
        {
            try
            {
                // File decryption
                string tempFolder = File2.SetTemporaryFolder(null, LibraryConstants.FOLDER_TEMP, true);

                _WorkingFileName = string.Concat(tempFolder, FILE_AICONFIG_XML);
                XTEAEncryption.Decrypt(_FileName, _WorkingFileName);

                // Extracting xml and dirty data
                byte[] xmlData;

                using (BinaryReader reader = new BinaryReader(new FileStream(_WorkingFileName, FileMode.Open, FileAccess.Read)))
                {
                    // Finding 0x00 0x00 sequence
                    byte readByte = 0xAA;

                    while (readByte != 0x00)
                        readByte = reader.ReadByte();

                    long xmlPartLength = reader.BaseStream.Position;

                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    xmlData = reader.ReadBytes((int)(xmlPartLength - 1));
                    _DirtyData = reader.ReadBytes((int)(reader.BaseStream.Length - xmlData.Length));
                }

                // Loading xml...
                _Doc = new XmlDocument();

                File2.WriteBytesToFile(_WorkingFileName, xmlData);
                _Doc.Load(_WorkingFileName);

                // Locating zones
                const string xRequest = "/AICONFIG/VEHICLE_REPARTITION/ZONE";

                _ZoneNodes = _Doc.SelectNodes(xRequest);

                if (_ZoneNodes == null || _ZoneNodes.Count == 0)
                    throw new Exception("Invalid zoning data in AIConfig.xml: see " + xRequest);

            }
            catch (Exception ex)
            {
                throw new Exception("Unable to read file: " + _FileName, ex);
            }
        }

        /// <summary>
        /// Saves the current file to disk.
        /// </summary>
        public override void Save()
        {
            try
            {
                byte[] xmlData;

                // Saving XML then re-building config file
                _Doc.Save(_WorkingFileName);

                using (
                    BinaryReader reader =
                        new BinaryReader(new FileStream(_WorkingFileName, FileMode.Open, FileAccess.Read)))
                {
                    xmlData = reader.ReadBytes((int) reader.BaseStream.Length);
                }
                using (
                    BinaryWriter writer =
                        new BinaryWriter(new FileStream(_WorkingFileName, FileMode.Create, FileAccess.Write)))
                {
                    writer.Write(xmlData);
                    writer.Write(_DirtyData);
                }

                // File encryption
                File2.RemoveAttribute(_FileName, FileAttributes.ReadOnly);
                XTEAEncryption.Encrypt(_WorkingFileName, _FileName);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to save file: " + _FileName, ex);
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Defines distribution index for a traffic vehicle in specified zone
        /// </summary>
        /// <param name="vehicleId"></param>
        /// <param name="zoneId"></param>
        /// <param name="index"></param>
        public void SetTrafficVehicleDistribution(string vehicleId, int zoneId, string index)
        {
            string xRequest = "VEHICLE[@database_id='" + vehicleId + "']";
            XmlNode zoneNode = _ZoneNodes[zoneId - 1];
            XmlNode foundNode = zoneNode.SelectSingleNode(xRequest);

            if (foundNode == null)
                throw new Exception("Unable to find zone data in AIConfig.xml: see " + xRequest);

            // Setting distribution value
            foundNode.Attributes[_ATTRIBUTE_FREQUENCY].Value = index;
        }

        /// <summary>
        /// Defines traffic vehicle count per km in specified zone
        /// </summary>
        /// <param name="zoneId"></param>
        /// <param name="count"></param>
        public void SetTrafficVehicleCountPerKilometer(int zoneId, string count)
        {
            XmlNode zoneNode = _ZoneNodes[zoneId - 1];

            // Setting count value
            zoneNode.Attributes[_ATTRIBUTE_NB_TRAFFIC_PER_KM].Value = count;
        }
        #endregion
    }
}
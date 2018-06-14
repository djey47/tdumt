using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.fileformats.binaries;
using TDUModdingLibrary.fileformats.database.util;
using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.fileformats.database.helper
{
    /// <summary>
    /// Static class helping with vehicle slots management (cameras, ik, ...)
    /// </summary>
    public static class VehicleSlotsHelper
    {
        #region Constants
        /// <summary>
        /// Reference file name
        /// </summary>
        public const string FILE_SLOTS_REF_XML = @"\VehicleSlots.xml";

        /// <summary>
        /// XML node for each vehicle
        /// </summary>
        private const string _VEHICLE_NODE = "vehicle";

        /// <summary>
        /// XML attribute for vehicle name
        /// </summary>
        private const string _NAME_ATTRIBUTE = "name";

        /// <summary>
        /// XML attribute for vehicle reference
        /// </summary>
        private const string _REF_ATTRIBUTE = "ref";

        /// <summary>
        /// XML attribute for vehicle default camera
        /// </summary>
        private const string _CAMERA_ATTRIBUTE = "camera";

        /// <summary>
        /// XML attribute for vehicle custom camera
        /// </summary>
        private const string _NEW_CAMERA_ATTRIBUTE = "newCamera";

        /// <summary>
        /// XML attribute for vehicle default IK
        /// </summary>
        private const string _IK_ATTRIBUTE = "ik";

        /// <summary>
        /// XML attribute for vehicle availability as addon
        /// </summary>
        private const string _ADDON_ATTRIBUTE = "addon";

        /// <summary>
        /// XML attribute for bike nature
        /// </summary>
        private const string _BIKE_ATTRIBUTE = "bike";

        /// <summary>
        /// XML attribute for modding authorization
        /// </summary>
        private const string _MODDABLE_ATTRIBUTE = "moddable";

        /// <summary>
        /// XML attribute for vehicle top speed
        /// </summary>
        private const string _TOP_SPEED_ATTRIBUTE = "topSpeed";
        #endregion

        #region Inner types
        /// <summary>
        /// Represents vehicle information
        /// </summary>
        public struct VehicleInfo
        {
            public string defaultCamera;
            public string newCamera;
            public string defaultIK;
            public ushort topSpeed;
            public bool isAddon;
            public bool isBike;
            public bool isModdable;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Index: (camera, vehicle name)
        /// </summary>
        public static Dictionary<string, string> CamReference
        {
            get { return _CamReference; }
        }
        private static readonly Dictionary<string, string> _CamReference = new Dictionary<string, string>();

        /// <summary>
        /// Index: (new camera, vehicle name)
        /// </summary>
        public static Dictionary<string, string> NewCamReference
        {
            get { return _NewCamReference; }
        }
        private static readonly Dictionary<string, string> _NewCamReference = new Dictionary<string, string>();

        /// <summary>
        /// Index: (vehicle name,default camera)
        /// </summary>
        public static Dictionary<string, string> CamReferenceReverse
        {
            get { return _CamReferenceReverse; }
        }
        private static readonly Dictionary<string, string> _CamReferenceReverse = new Dictionary<string, string>();

        /// <summary>
        /// Index: (vehicle name,default IK)
        /// </summary>
        public static Dictionary<string, string> IKReferenceReverse
        {
            get { return _IKReferenceReverse; }
        }
        private static readonly Dictionary<string, string> _IKReferenceReverse = new Dictionary<string, string>();
        
        /// <summary>
        /// Index: (vehicle name, ref)
        /// </summary>
        public static Dictionary<string, string> SlotReference
        {
            get { return _SlotReference; }
        }
        private static readonly Dictionary<string, string> _SlotReference = new Dictionary<string, string>();

        /// <summary>
        /// Index: (ref, vehicle name)
        /// </summary>
        public static Dictionary<string, string> SlotReferenceReverse
        {
            get { return _SlotReferenceReverse; }
        }
        private static readonly Dictionary<string, string> _SlotReferenceReverse = new Dictionary<string, string>();
        
        /// <summary>
        /// Index: (IK, vehicle name)
        /// </summary>
        public static Dictionary<string, string> IKReference
        {
            get { return _IKReference; }
        }
        private static readonly Dictionary<string, string> _IKReference = new Dictionary<string, string>();
        
        /// <summary>
        /// Index: (ref, vehicle information)
        /// </summary>
        public static Dictionary<string, VehicleInfo> VehicleInformation
        {
            get { return _VehicleInformation; }
        }
        private static readonly Dictionary<string, VehicleInfo> _VehicleInformation = new Dictionary<string, VehicleInfo>();

        /// <summary>
        /// Default camera information
        /// </summary>
        public static Cameras DefaultCameras
        {
            get { return _DefaultCameras; }
        }
        private static Cameras _DefaultCameras;
        #endregion

        #region Members
        /// <summary>
        /// Indicates if reference contents are ready
        /// </summary>
        private static bool _IsReady;
        #endregion

        #region Public methods
        /// <summary>
        /// Initializes vehicle reference according to specified XML file.
        /// </summary>
        /// <param name="referenceFilePath">Path of XML reference file (just path, not filename)</param>
        public static void InitReference(string referenceFilePath)
        {
            // Is init needed ?
            if (!_IsReady && !string.IsNullOrEmpty(referenceFilePath) && Directory.Exists(referenceFilePath))
            {
                // Parsing ref XML file and writing to reference
                XmlDocument doc = new XmlDocument();

                doc.Load(referenceFilePath + FILE_SLOTS_REF_XML);

                XmlNode slotsNode = doc.DocumentElement;

                if (slotsNode != null)
                {
                    XmlNodeList allVehicles = slotsNode.SelectNodes(_VEHICLE_NODE);

                    if (allVehicles != null)
                    {
                        foreach (XmlNode anotherNode in allVehicles)
                        {
                            string name = anotherNode.Attributes[_NAME_ATTRIBUTE].Value;
                            string vehicleRef = anotherNode.Attributes[_REF_ATTRIBUTE].Value;
                            string defaultCam = anotherNode.Attributes[_CAMERA_ATTRIBUTE].Value;
                            string defaultIK = anotherNode.Attributes[_IK_ATTRIBUTE].Value;

                            // New camera ?
                            string newCam = Xml2.GetAttributeWithDefaultValue(anotherNode, _NEW_CAMERA_ATTRIBUTE, null);

                            // Top speed (km/h)
                            string topSpeed = Xml2.GetAttributeWithDefaultValue(anotherNode, _TOP_SPEED_ATTRIBUTE, "0");

                            // Addon car ?
                            bool isAddon =
                                bool.Parse(Xml2.GetAttributeWithDefaultValue(anotherNode, _ADDON_ATTRIBUTE, "false"));

                            // Is it a bike ?
                            bool isBike =
                                bool.Parse(Xml2.GetAttributeWithDefaultValue(anotherNode, _BIKE_ATTRIBUTE, "false"));

                            // Moddable vehicle ?
                            bool isModdable =
                                bool.Parse(Xml2.GetAttributeWithDefaultValue(anotherNode, _MODDABLE_ATTRIBUTE, "true"));

                            _SlotReference.Add(name, vehicleRef);
                            _SlotReferenceReverse.Add(vehicleRef, name);
                            _CamReferenceReverse.Add(name, defaultCam);
                            _IKReferenceReverse.Add(name, defaultIK);

                            if (!_CamReference.ContainsKey(defaultCam))
                                _CamReference.Add(defaultCam, name);
                            if (newCam != null && !_NewCamReference.ContainsKey(newCam))
                                _NewCamReference.Add(newCam, name);
                            if (!_IKReference.ContainsKey(defaultIK))
                                _IKReference.Add(defaultIK, name);

                            // Vehicle information
                            VehicleInfo info = new VehicleInfo
                                                   {
                                                       isAddon = isAddon,
                                                       isBike = isBike,
                                                       defaultCamera = defaultCam,
                                                       newCamera = newCam,
                                                       defaultIK = defaultIK,
                                                       isModdable = isModdable,
                                                       topSpeed = ushort.Parse(topSpeed)
                                                   };

                            // Updating reference
                            _VehicleInformation.Add(vehicleRef, info);
                        }
                    }
                }

                // Loads default camera
                string defaultCamerasFileName = referenceFilePath + LibraryConstants.FOLDER_DEFAULT +
                                                Cameras.FILE_CAMERAS_BIN;
                _DefaultCameras = (Cameras) TduFile.GetFile(defaultCamerasFileName);

                if (_DefaultCameras == null || !_DefaultCameras.Exists)
                    throw new Exception("Default camera file invalid or not found: " + defaultCamerasFileName);

                // OK !
                _IsReady = true;
            }
        }

        /// <summary>
        /// Changes camera of specified vehicle by using one of specified vehicle
        /// </summary>
        /// <param name="vehicleRef">Reference of vehicle to change camera</param>
        /// <param name="vehicleName">Name of selected vehicle in bottom list</param>
        /// <param name="physicsDB">Instance of DB file to modify</param>
        public static void ChangeCameraByVehicleName(string vehicleRef, string vehicleName, DB physicsDB)
        {
            if (!string.IsNullOrEmpty(vehicleRef) && !string.IsNullOrEmpty(vehicleName) && physicsDB != null)
            {
                // Getting new camera id
                string newCamera = CamReferenceReverse[vehicleName];

                DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(physicsDB, DatabaseConstants.CAMERA_PHYSICS_DB_COLUMN,
                                                                  vehicleRef, newCamera);
            }
        }

        /// <summary>
        /// Changes camera of specified vehicle
        /// </summary>
        /// <param name="vehicleRef">Reference of vehicle to change camera</param>
        /// <param name="camId">Camera identifier</param>
        /// <param name="physicsDB">Instance of DB file to modify</param>
        public static void ChangeCameraById(string vehicleRef, string camId, DB physicsDB)
        {
            if (!string.IsNullOrEmpty(vehicleRef) && !string.IsNullOrEmpty(camId) && physicsDB != null)
                DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(physicsDB, DatabaseConstants.CAMERA_PHYSICS_DB_COLUMN, vehicleRef, camId);
        }

        /// <summary>
        /// Changes IK of specified vehicle by using one of specified vehicle
        /// </summary>
        /// <param name="vehicleRef">Reference of vehicle to change camera</param>
        /// <param name="vehicleName">Name of selected vehicle in bottom list</param>
        /// <param name="physicsDB">Instance of DB file to modify</param>
        public static void ChangeIKByVehicleName(string vehicleRef, string vehicleName, DB physicsDB)
        {
            if (!string.IsNullOrEmpty(vehicleRef) && !string.IsNullOrEmpty(vehicleName) && physicsDB != null)
            {
                // Getting new camera id
                string newIK = IKReferenceReverse[vehicleName];

                DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(physicsDB, DatabaseConstants.SAME_IK_PHYSICS_DB_COLUMN,
                                                                  vehicleRef, newIK);
            }
        }

        /// <summary>
        /// Changes IK of specified vehicle
        /// </summary>
        /// <param name="vehicleRef">Reference of vehicle to change camera</param>
        /// <param name="ikId">Camera identifier</param>
        /// <param name="physicsDB">Instance of DB file to modify</param>
        public static void ChangeIKById(string vehicleRef, string ikId, DB physicsDB)
        {
            if (!string.IsNullOrEmpty(vehicleRef) && !string.IsNullOrEmpty(ikId) && physicsDB != null)
                DatabaseHelper.UpdateCellFromTopicWherePrimaryKey(physicsDB, DatabaseConstants.SAME_IK_PHYSICS_DB_COLUMN, vehicleRef, ikId);
        }

        /// <summary>
        /// Replaces specified camera view with data taken into another one. If view does not exist, it is created then added to entry.
        /// </summary>
        /// <param name="cameraData"></param>
        /// <param name="currentEntry"></param>
        /// <param name="currentViewType"></param>
        /// <param name="baseEntry"></param>
        /// <param name="baseViewType"></param>
        public static void CustomizeCameraView(Cameras cameraData, Cameras.CamEntry currentEntry, Cameras.ViewType currentViewType, Cameras.CamEntry baseEntry, Cameras.ViewType baseViewType)
        {
            if (cameraData != null)
            {
                Cameras.View baseView = Cameras.GetViewByType(baseEntry, baseViewType);
                Cameras.View viewToReplace = Cameras.GetViewByType(currentEntry, currentViewType);

                if (baseView.isValid)
                {
                    Cameras.View newView = baseView;

                    newView.cameraId = currentEntry.id;
                    // Set to empty to get enough place
                    newView.name = "";
                    newView.parentCameraId = baseView.cameraId;
                    newView.parentType = baseView.type;
                    newView.type = currentViewType;

                    if (viewToReplace.isValid)
                    {
                        // Replace mode : searching for view to replace
                        List<Cameras.View> currentViews = currentEntry.views;

                        if (currentViews.Contains(viewToReplace))
                            currentViews.Remove(viewToReplace);

                        currentViews.Add(newView);
                        currentEntry.views = currentViews;
                    }
                    else
                    {
                        // Add mode >>> disabled if view count bigger than original one :(
                        ushort originalViewCount = cameraData.Index[newView.cameraId];

                        if (currentEntry.views.Count + 1 > originalViewCount)
                            throw new Exception("You can't add views for now... hope that'll be solved in the future.");
                        
                        currentEntry.views.Add(newView);
                    }

                    // Updating entry
                    cameraData.UpdateEntry(currentEntry);
                }
            }
        }

        /// <summary>
        /// Changes camera source and/or target position(s)
        /// </summary>
        /// <param name="cameraData"></param>
        /// <param name="entry"></param>
        /// <param name="viewType"></param>
        /// <param name="sourcePosition"></param>
        /// <param name="targetPosition"></param>
        public static void CustomizeCameraPosition(Cameras cameraData, Cameras.CamEntry entry, Cameras.ViewType viewType, Cameras.Position sourcePosition, Cameras.Position targetPosition)
        {
            if (cameraData != null)
            {
                Cameras.View baseView = Cameras.GetViewByType(entry, viewType);

                if (baseView.isValid)
                {
                    List<Cameras.View> currentViews = entry.views;

                    currentViews.Remove(baseView);
                    
                    baseView.source = sourcePosition;
                    baseView.target = targetPosition;

                    currentViews.Add(baseView);
                    entry.views = currentViews;

                    // Updating entry
                    cameraData.UpdateEntry(entry);
                }
            }
        }

        #endregion
    }
}
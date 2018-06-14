using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TDUModdingLibrary.fileformats;
using CameraView=Djey.TduModdingTools.CLI.CustomizeObject.View;
using NativeCameras=TDUModdingLibrary.fileformats.binaries.Cameras;
using NativeCamerasEntry=TDUModdingLibrary.fileformats.binaries.Cameras.CamEntry;
using NativeCamerasView=TDUModdingLibrary.fileformats.binaries.Cameras.View;
using TDUModdingLibrary.fileformats.database.helper;
using System.Reflection;

namespace Djey.TduModdingTools.CLI.Modules.Cameras
{
	/// <summary>
	/// Module handling TDU Banks (bnk) management assemblies.
	/// </summary>
	public class CamerasModule
	{
		// TODO Convert to enum
		public const string CommandCameraCustomize = "CAM-C";
		public const string CommandCameraReset = "CAM-R";
		public const string CommandCameraList = "CAM-L";

	    private readonly ModuleDispatcher _moduleDispatcher;

		public CamerasModule (ModuleDispatcher moduleDispatcher)
		{
			_moduleDispatcher = moduleDispatcher;
		}

	    /// <summary>
	    /// Provides information about changes in camera entry.
	    /// </summary>
	    /// <param name="args"></param>
	    public void List (string[] args)
	    {
			var camFileName = GetCamFileNameParam (args);
			var cameraId = GetCamIdParam (args);

			var camFile = _LoadCameras (camFileName);
			var camEntry = _GetEntry (camFile, cameraId);

			var outputObject = new CustomizeObject ();

			var views = new List<CameraView> ();
			foreach (var view in camEntry.views) {
			  var item = new CustomizeObject.View
			  {
					RootCameraId = cameraId,
			    	CameraId = view.parentCameraId,
			    	ViewId = (int) view.parentType,
			    	ViewKind = view.type
			  };

				views.Add(item);
			}
			outputObject.Views = views;

			_moduleDispatcher.ModuleOutput = JsonConvert.SerializeObject (outputObject);
	    }

		/// <summary>
		/// Performs changes in camera entry.
		/// </summary>
		/// <param name="args">Arguments.</param>
		public void Customize (string[] args)
		{
			var camFileName = GetCamFileNameParam (args);
			var cameraId = GetCamIdParam (args);
			var camInputFileName = GetCamInputFileParam (args);

			var camFile = _LoadCameras (camFileName);
			var defaultCamFile = _LoadDefaultCameras ();

			var customizeInput = _LoadCustomizeInputFile (camInputFileName);

			foreach (var view in customizeInput.Views) {

				_Customize (camFile, defaultCamFile, cameraId, view.ViewKind, view.CameraId, (NativeCameras.ViewType) view.ViewId);  

			}

			camFile.Save ();

			_moduleDispatcher.NoModuleOutput();
		}

		/// <summary>
		/// Reverts changes in camera entry.
		/// </summary>
		/// <param name="args">Arguments.</param>
		public void Reset (string[] args)
		{
			var camFileName = GetCamFileNameParam (args);
			var cameraId = GetCamIdParam (args);

			var camFile = _LoadCameras (camFileName);
			var defaultCamFile = _LoadDefaultCameras ();

			var entryToReset = _GetEntry (camFile, cameraId);
			var viewKinds = new List<NativeCameras.ViewType> ();
			foreach (var view in entryToReset.views) {

				viewKinds.Add(view.type);

			}

			var defaultEntry = _GetEntry (defaultCamFile, cameraId);
			foreach (var viewKind in viewKinds) {

				VehicleSlotsHelper.CustomizeCameraView(camFile, entryToReset, viewKind, defaultEntry, viewKind);

			}

			camFile.Save ();

			_moduleDispatcher.NoModuleOutput();
		}

		public static string GetCamFileNameParam (IList<string> args)
		{
			return args [0];
		}

		public static int GetCamIdParam (IList<string> args)
		{
			return int.Parse(args [1]);
		}

		public static string GetCamInputFileParam (IList<string> args)
		{
			return args [2];
		}

		private static CustomizeObject _LoadCustomizeInputFile (string inputFileName)
		{
			var fileContents = File.ReadAllText(inputFileName);
			return JsonConvert.DeserializeObject<CustomizeObject>(fileContents);
		}

		private static string _WriteDefaultCameraResourceToFile()
		{
			using(var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Djey.TduModdingTools.CLI.Resources.cameras.bin"))
			{
			  if (resource == null) return null;

			  using (var reader = new BinaryReader(resource))
			  {
			    var fileName = Path.Combine(Path.GetTempPath (), "cameras.bin");
			    var contentsAsBytes = reader.ReadBytes((int)resource.Length);
			    File.WriteAllBytes (fileName, contentsAsBytes);
			    return fileName;
			  }
			}
		}

		private static NativeCameras _LoadCameras(string camFileName) {
			var camFile = TduFile.GetFile (camFileName) as TDUModdingLibrary.fileformats.binaries.Cameras;
			if (camFile == null) {
				throw new FileLoadException ("Unable to load cameras file.", camFileName);
			}
			return camFile;
		}

		private static NativeCameras _LoadDefaultCameras() {
			var defaultCamFileName = _WriteDefaultCameraResourceToFile ();
			return _LoadCameras (defaultCamFileName);
		}

		private static void _Customize(NativeCameras currentData, NativeCameras defaultData, int entryId, NativeCameras.ViewType currentView, int takenEntryId, NativeCameras.ViewType takenView)
		{
			var entryToChange = _GetEntry (currentData, entryId);
			var takenEntry = _GetEntry (defaultData, takenEntryId);

			VehicleSlotsHelper.CustomizeCameraView(currentData, entryToChange, currentView, takenEntry, takenView);
		}

		private static NativeCamerasEntry _GetEntry(NativeCameras camFile, int entryId) {
			var camEntry = camFile.GetEntryByCameraId (entryId.ToString());
			if (!camEntry.isValid) {
				throw new ArgumentException ("Specified camera identifier does not exist: " + entryId, "entryId");
			}
			return camEntry;
		}
	}
}

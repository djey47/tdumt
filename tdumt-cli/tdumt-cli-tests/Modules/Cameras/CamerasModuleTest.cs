using NUnit.Framework;
using System;
using Djey.TduModdingTools.CLI.Modules.Banks;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace Djey.TduModdingTools.CLI.Modules.Cameras
{
	[TestFixture ()]
	public class CamerasModuleTest
	{
		private static readonly Assembly _CURRENT_ASSEMBLY = Assembly.GetExecutingAssembly();

		[Test ()]
		public void List_WhenCameraIdExists_ShouldReturnDetails () 
		{
			// GIVEN
			var moduleDispatcher = new ModuleDispatcher();
			var module = new CamerasModule (moduleDispatcher);
			var cameraFileName = _WriteCameraResourceToFile ();

			// WHEN
			module.List (new string[] {cameraFileName, "326"});

			// THEN
			var expectedOutput = _ReadTextFromResourceFile ("Djey.TduModdingTools.CLI.Resources.ListCameraOutput-Default.json");
			Assert.AreEqual(expectedOutput, moduleDispatcher.ModuleOutput);
		}

		[Test ()]
		public void List_WhenCameraIdExists_AndNewCameraIdentifier_ShouldReturnDetails () 
		{
			// GIVEN
			var moduleDispatcher = new ModuleDispatcher();
			var module = new CamerasModule (moduleDispatcher);
			var cameraFileName = _WriteCameraResourceToFile ();

			// WHEN
			module.List (new string[] {cameraFileName, "315"});

			// THEN
			var expectedOutput = _ReadTextFromResourceFile ("Djey.TduModdingTools.CLI.Resources.ListCameraOutput-Default.json");
			Assert.AreEqual(expectedOutput, moduleDispatcher.ModuleOutput);
		}

		[Test ()]
		public void List_WhenCameraIdDoesNotExist_ShouldThrowException () 
		{
			// GIVEN
			var moduleDispatcher = new ModuleDispatcher();
			var module = new CamerasModule (moduleDispatcher);
			var cameraFileName = _WriteCameraResourceToFile ();

			// WHEN-THEN
			Exception actualException = Assert.Catch (typeof(ArgumentException), delegate { CamListDelegate(module, cameraFileName); });
			Assert.AreEqual ("Specified camera identifier does not exist: 0\nParameter name: entryId", actualException.Message);
		}

		[Test ()]
		public void Customize_WhenCameraIdDoesNotExist_ShouldThrowException () 
		{
			// GIVEN
			var moduleDispatcher = new ModuleDispatcher();
			var module = new CamerasModule (moduleDispatcher);
			var cameraFileName = _WriteCameraResourceToFile ();
			var customizeInputFileName = _WriteCameraJSONInputResourceToFile ();

			// WHEN-THEN
			Exception actualException = Assert.Catch (typeof(ArgumentException), delegate { CamCustomizeDelegate(module, cameraFileName, customizeInputFileName); });
			Assert.AreEqual ("Specified camera identifier does not exist: 0\nParameter name: entryId", actualException.Message);
		}

		[Test ()]
		public void Reset_WhenCameraIdDoesNotExist_ShouldThrowException () 
		{
			// GIVEN
			var moduleDispatcher = new ModuleDispatcher();
			var module = new CamerasModule (moduleDispatcher);
			var cameraFileName = _WriteCameraResourceToFile ();

			// WHEN-THEN
			Exception actualException = Assert.Catch (typeof(ArgumentException), delegate { CamResetDelegate(module, cameraFileName); });
			Assert.AreEqual ("Specified camera identifier does not exist: 0\nParameter name: entryId", actualException.Message);
		}

		[Test ()]
		public void GetCamFileNameParam_WhenArgs_ShouldReturnFirstOne ()
		{
			// GIVEN
			List<string> args = new List <string>();
			args.Add("cameras.bin");
			args.Add ("1");

			// WHEN
			string actualParam = CamerasModule.GetCamFileNameParam (args);

			// THEN
			Assert.AreEqual ("cameras.bin", actualParam);
		}

		[Test ()]
		public void GetCamIdParam_WhenArgs_ShouldReturnSecondOne ()
		{
			// GIVEN
			List<string> args = new List <string>();
			args.Add("cameras.bin");
			args.Add ("1");

			// WHEN
			int actualParam = CamerasModule.GetCamIdParam (args);

			// THEN
			Assert.AreEqual (1, actualParam);
		}

		[Test ()]
		public void GetCamInputFileParam_WhenArgs_ShouldReturnThirdOne ()
		{
			// GIVEN
			List<string> args = new List <string>();
			args.Add("cameras.bin");
			args.Add ("1");
			args.Add ("CamInput.json");

			// WHEN
			string actualParam = CamerasModule.GetCamInputFileParam (args);

			// THEN
			Assert.AreEqual ("CamInput.json", actualParam);
		}

		private static void CamListDelegate(CamerasModule module, string cameraFileName)
		{
			module.List (new string[] {cameraFileName, "000"});
		}

		private static void CamCustomizeDelegate(CamerasModule module, string cameraFileName, string customizeInputFileName)
		{
			module.Customize (new string[] {cameraFileName, "000", customizeInputFileName});
		}

		private static void CamResetDelegate(CamerasModule module, string cameraFileName)
		{
			module.Reset (new string[] {cameraFileName, "000"});
		}

		private static string _WriteCameraResourceToFile()
		{
			return _WriteResourceToFile ("Djey.TduModdingTools.CLI.Resources.binaries.cameras.bin", "cameras.bin");
		}

		private static string _WriteCameraJSONInputResourceToFile()
		{
			return _WriteResourceToFile ("Djey.TduModdingTools.CLI.Resources.CustomizeCameraInput.json", "CustomizeCameraInput.json");
		}

		private static string _WriteResourceToFile(string resourcePath, string targetFileName )
		{
			using(var resource = _CURRENT_ASSEMBLY.GetManifestResourceStream(resourcePath))
			{
				if (resource == null) return null;

				using (var reader = new BinaryReader(resource))
				{
					var fileName = Path.Combine(Path.GetTempPath (), targetFileName);
					var contentsAsBytes = reader.ReadBytes((int)resource.Length);
					File.WriteAllBytes (fileName, contentsAsBytes);
					return fileName;
				}
			}
		}

		private static string _ReadTextFromResourceFile(string resourceName) {
			using (Stream stream = _CURRENT_ASSEMBLY.GetManifestResourceStream(resourceName))
			using (StreamReader reader = new StreamReader(stream))
			{
				return reader.ReadToEnd ();
			}
		}
	}
}

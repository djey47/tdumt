using System;
using NUnit.Framework;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using ViewType = TDUModdingLibrary.fileformats.binaries.Cameras.ViewType;
using System.Collections.Generic;

namespace Djey.TduModdingTools.CLI.Modules.Cameras.Dto
{
	[TestFixture]
	public class MappingTest
	{
		private static readonly Assembly _CURRENT_ASSEMBLY = Assembly.GetExecutingAssembly();

		[Test]
		public void Map_CustomizeCameraInput ()
		{
			// GIVEN
			var resourceName = "Djey.TduModdingTools.CLI.Resources.CustomizeCameraInput.json";
			var fileContents = _ReadFlatJSONFromResourceFile (resourceName);

			// WHEN
			CustomizeObject actualObject = JsonConvert.DeserializeObject<CustomizeObject>(fileContents);

			// THEN
			Assert.IsNotNull (actualObject);
			Assert.AreEqual (4, actualObject.Views.Count);
			Assert.AreEqual (ViewType.Hood, actualObject.Views [0].ViewKind);
			Assert.AreEqual (2014, actualObject.Views [0].CameraId);
			Assert.AreEqual (44, actualObject.Views [0].ViewId);
			Assert.AreEqual (ViewType.Hood_Back, actualObject.Views [1].ViewKind);
			Assert.AreEqual (2013, actualObject.Views [1].CameraId);
			Assert.AreEqual (24, actualObject.Views [1].ViewId);
			Assert.AreEqual (ViewType.Cockpit, actualObject.Views [2].ViewKind);
			Assert.AreEqual (2012, actualObject.Views [2].CameraId);
			Assert.AreEqual (45, actualObject.Views [2].ViewId);
			Assert.AreEqual (ViewType.Cockpit_Back, actualObject.Views [3].ViewKind);
			Assert.AreEqual (2011, actualObject.Views [3].CameraId);
			Assert.AreEqual (25, actualObject.Views [3].ViewId);
		}

		[Test]
		public void Map_CustomizeCameraOutput ()
		{
			// GIVEN
			var customizeCameraOutput = new CustomizeObject();
			customizeCameraOutput.Views = new List<CustomizeObject.View> ();
			CustomizeObject.View view1 = new CustomizeObject.View();
			view1.CameraId = 2014;
			view1.ViewId = 44;
			view1.ViewKind = ViewType.Hood;
			customizeCameraOutput.Views.Add (view1);
			CustomizeObject.View view2 = new CustomizeObject.View();
			view2.CameraId = 2013;
			view2.ViewId = 24;
			view2.ViewKind = ViewType.Hood_Back;
			customizeCameraOutput.Views.Add (view2);
			CustomizeObject.View view3 = new CustomizeObject.View();
			view3.CameraId = 2012;
			view3.ViewId = 45;
			view3.ViewKind = ViewType.Cockpit;
			customizeCameraOutput.Views.Add (view3);
			CustomizeObject.View view4 = new CustomizeObject.View();
			view4.CameraId = 0;
			view4.ViewId = 0;
			view4.ViewKind = ViewType.Cockpit_Back;
			customizeCameraOutput.Views.Add (view4);

			var resourceName = "Djey.TduModdingTools.CLI.Resources.CustomizeCameraOutput.json";
			var fileContents = _ReadFlatJSONFromResourceFile (resourceName);

			// WHEN
			string actual = JsonConvert.SerializeObject (customizeCameraOutput);

			// THEN
			Assert.AreEqual (fileContents, actual);
		}

		private static string _ReadFlatJSONFromResourceFile(string resourceName) {
			using (Stream stream = _CURRENT_ASSEMBLY.GetManifestResourceStream(resourceName))
			using (StreamReader reader = new StreamReader(stream))
			{
				return reader.ReadToEnd()
					.Replace ("\n", "")
					.Replace ("\t", "")
					.Replace (" ", ""); 
			}
		}
	}
}


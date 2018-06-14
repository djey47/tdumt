using System;
using NUnit.Framework;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

namespace Djey.TduModdingTools.CLI.Modules.Banks.Dto
{
	[TestFixture]
	public class MappingTest
	{
		private static readonly Assembly _CURRENT_ASSEMBLY = Assembly.GetExecutingAssembly();

		[Test]
		public void Map_BatchRepackInput ()
		{
			// GIVEN
			var resourceName = "Djey.TduModdingTools.CLI.Resources.BatchRepackInput.json";
			var fileContents = _ReadTextFromResourceFile (resourceName);

			// WHEN
			BatchInputObject actualObject = JsonConvert.DeserializeObject<BatchInputObject>(fileContents);

			// THEN
			Assert.IsNotNull (actualObject);
			Assert.AreEqual (2, actualObject.Items.Count);
			Assert.AreEqual ("d:\\file1", actualObject.Items [0].ExternalFile);
			Assert.AreEqual ("\\PATH1", actualObject.Items [0].InternalPath);
			Assert.AreEqual ("d:\\file2", actualObject.Items [1].ExternalFile);
			Assert.AreEqual ("\\PATH2", actualObject.Items [1].InternalPath);
		}

		[Test]
		public void Map_BatchUnpackInput_WithoutTargetFiles ()
		{
			// GIVEN
			var resourceName = "Djey.TduModdingTools.CLI.Resources.BatchUnpackInput-NoTarget.json";
			var fileContents = _ReadTextFromResourceFile (resourceName);

			// WHEN
			BatchInputObject actualObject = JsonConvert.DeserializeObject<BatchInputObject>(fileContents);

			// THEN
			Assert.IsNotNull (actualObject);
			Assert.AreEqual (2, actualObject.Items.Count);
			Assert.IsNull (actualObject.Items [0].ExternalFile);
			Assert.AreEqual ("\\PATH1", actualObject.Items [0].InternalPath);
			Assert.IsNull (actualObject.Items [1].ExternalFile);
			Assert.AreEqual ("\\PATH2", actualObject.Items [1].InternalPath);
		}

		private static string _ReadTextFromResourceFile(string resourceName) {
			using (Stream stream = _CURRENT_ASSEMBLY.GetManifestResourceStream(resourceName))
			using (StreamReader reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}
	}
}

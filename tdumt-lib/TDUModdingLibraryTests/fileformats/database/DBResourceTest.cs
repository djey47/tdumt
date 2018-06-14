using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats;
using System.IO;
using TDUModdingLibraryTests.Properties;

namespace TDUModdingLibraryTests.fileformats.database
{
    [TestClass]
    public class DBResourceTest
    {
        private const string RESOURCE_FILE = @"\TDU_Achievements_testing.fr";

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            File.Delete(Path.GetTempPath() + RESOURCE_FILE);
        }

        [TestMethod]
        public void ReadResourceFile_ShouldGetAllEntries()
        {
            //Given
            string fileName = Path.GetTempPath() + RESOURCE_FILE;
            File.WriteAllBytes(fileName, Resources.TDU_Achievements);

            //When
            DBResource dbResource = TduFile.GetFile(fileName) as DBResource;

            //Then
            Assert.AreEqual(DB.Culture.FR,  dbResource.CurrentCulture);
            Assert.AreEqual(DB.Topic.Achievements, dbResource.CurrentTopic);
            Assert.AreEqual(253, dbResource.EntryList.Count);
        }
    }
}

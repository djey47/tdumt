using TDUModdingLibrary.fileformats.database;
using TDUModdingLibrary.fileformats;
using System.IO;
using NUnit.Framework;
using tdumtlibtests.Common;

namespace tdumtlibtests.fileformats.database
{
    [TestFixture]
    public class DBResourceTest
    {
        private const string ResourceFile = "TDU_Achievements_testing.fr";

        private static string _tempPath;

        [OneTimeSetUp]
        public static void SetUp()
        {
            _tempPath = FileTesting.CreateTempDirectory();
        }

        [OneTimeTearDown]
        public static void MyClassCleanup()
        {
            File.Delete(Path.Combine(_tempPath, ResourceFile));
        }

        [Test]
        public void ReadResourceFile_ShouldGetAllEntries()
        {
            //Given
            string fileName = FileTesting.CreateFileFromResource("tdumtlibtests.Resources.TDU_Achievements.fr", Path.Combine(_tempPath, ResourceFile));

            //When
            DBResource dbResource = TduFile.GetFile(fileName) as DBResource;

            //Then
            Assert.NotNull(dbResource);
            Assert.AreEqual(DB.Culture.FR,  dbResource.CurrentCulture);
            Assert.AreEqual(DB.Topic.Achievements, dbResource.CurrentTopic);
            Assert.AreEqual(253, dbResource.EntryList.Count);
        }
    }
}

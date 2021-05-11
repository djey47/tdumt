using TDUModdingLibrary.fileformats;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using TDUModdingLibrary.fileformats.database;
using tdumtlibtests.Common;

namespace tdumtlibtests.fileformats.database
{
    [TestFixture]
    public class DbTest
    {
        private const string ContentsFile = "TDU_Achievements_testing.db";

        private static string _tempPath;

        [OneTimeSetUp]
        public static void SetUp()
        {
            _tempPath = FileTesting.CreateTempDirectory();
        }

        [OneTimeTearDown]
        public static void MyClassCleanup()
        {
            File.Delete(Path.Combine(_tempPath, ContentsFile));
        }

        [Test]
        public void Save_ShouldMakeFileWithSizeMultipleOf8()
        {
            //Given
            string fileName = FileTesting.CreateFileFromResource(
                "tdumtlibtests.Resources.TDU_Achievements.db", Path.Combine(_tempPath, ContentsFile));
            DB db = TduFile.GetFile(fileName) as DB;

            //When
            Assert.NotNull(db);
            db.Save();

            //Then
            FileInfo fileInfo = new FileInfo(fileName);
            Assert.IsTrue(fileInfo.Length % 8 == 0);
        }
    }
}

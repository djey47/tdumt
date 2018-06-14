using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDUModdingLibrary.fileformats;
using System.IO;
using TDUModdingLibraryTests.Properties;
using TDUModdingLibrary.fileformats.database;

namespace TDUModdingLibraryTests.fileformats.database
{
    [TestClass]
    public class DbTest
    {
        private const string CONTENTS_FILE = @"\TDU_Achievements_testing.db";

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            File.Delete(Path.GetTempPath() + CONTENTS_FILE);
        }

        [TestMethod]
        public void Save_ShouldMakeFileWithSizeMultipleOf8()
        {
            //Given
            string fileName = Path.GetTempPath() + CONTENTS_FILE;
            File.WriteAllBytes(fileName, Resources.TDU_Achievements_contents);
            DB db = TduFile.GetFile(fileName) as DB;

            //When
            db.Save();

            //Then
            FileInfo fileInfo = new FileInfo(fileName);
            Assert.IsTrue(fileInfo.Length % 8 == 0);
        }
    }
}

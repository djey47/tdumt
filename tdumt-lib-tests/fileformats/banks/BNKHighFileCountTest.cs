using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.fileformats;
using System.IO;
using NUnit.Framework;
using tdumtlibtests.Common;

namespace tdumtlibtests.fileformats.banks
{
    [TestFixture]
    public class BNKHighFileCountTest
    {
        private const string ResourceFile = "hud.bnk";

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
        public void BugScenario()
        {
            // GIVEN
            string bankFile = FileTesting.CreateFileFromResource("tdumtlibtests.Resources.banks.pc." + ResourceFile,
                Path.Combine(_tempPath, ResourceFile));
            BNK bnk = TduFile.GetFile(bankFile) as BNK;
            Assert.NotNull(bnk);
            string packedFilePath = @"D:\Eden-Prog\Games\TestDrive\Resources\4Build\PC\EURO\FrontEnd\hud\.2db\fla_0026";

            // WHEN: extracting
            bnk.ExtractPackedFile(packedFilePath, _tempPath, true);
            string expectedFilePath = Path.Combine(_tempPath, "fla_0026.2db");
            Assert.IsTrue(File.Exists(expectedFilePath));

            // WHEN: repacking same file
            bnk.ReplacePackedFile(packedFilePath, expectedFilePath);

            // THEN
        }
    }
}

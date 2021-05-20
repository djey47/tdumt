using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.fileformats;
using System.IO;
using NUnit.Framework;
using tdumtlibtests.Common;

namespace tdumtlibtests.fileformats.banks
{
    [TestFixture]
    public class BNKTest
    {
        private const string _RESOURCE_FILE_PREFIX = "tdumtlibtests.Resources.banks.pc.";

        private const string _PACKED_FILE_PATH = @"D:\Eden-Prog\Games\TestDrive\Resources\4Build\PC\EURO\Vehicules\Cars\Mercedes\CLK_55\.2DM\CLK_55";

        private static string _TempPath;

        [OneTimeSetUp]
        public static void SetUp()
        {
            _TempPath = FileTesting.CreateTempDirectory();
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            Directory.Delete(_TempPath, true);
        }

        [Test]
        public void ExtractPackedFile_WhenFolderTarget_ShouldCreateFileAtRightLocation()
        {
            // GIVEN
            string resourceFile = "CLK_55.bnk";
            string bankFile = FileTesting.CreateFileFromResource($"{_RESOURCE_FILE_PREFIX}{resourceFile}",
                Path.Combine(_TempPath, resourceFile));
            BNK bnk = TduFile.GetFile(bankFile) as BNK;
            Assert.NotNull(bnk);

            // WHEN
            bnk.ExtractPackedFile(_PACKED_FILE_PATH, _TempPath, true);

            // THEN
            string expectedFilePath = Path.Combine(_TempPath, "CLK_55.2DM");
            Assert.IsTrue(File.Exists(expectedFilePath));
        }

        [Test]
        public void ExtractPackedFile_WhenFileTarget_ShouldCreateFileAtRightLocation()
        {
            // GIVEN
            string resourceFile = "CLK_55.bnk";
            string bankFile = FileTesting.CreateFileFromResource($"{_RESOURCE_FILE_PREFIX}{resourceFile}",
                Path.Combine(_TempPath, resourceFile) );
            BNK bnk = TduFile.GetFile(bankFile) as BNK;
            Assert.NotNull(bnk);

            // WHEN
            string expectedFilePath = Path.Combine(_TempPath, "extracted.CLK_55.2DM");
            bnk.ExtractPackedFile(_PACKED_FILE_PATH, expectedFilePath, false);

            // THEN
            Assert.IsTrue(File.Exists(expectedFilePath));
        }
    }
}

using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.fileformats;
using System.IO;
using NUnit.Framework;
using TDUModdingLibraryTests.Common;

namespace TDUModdingLibraryTests.fileformats.banks
{
    [TestFixture]
    public class BNKTest
    {
        private const string ResourceFile = "CLK_55.bnk";

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
        public void ExtractPackedFile_WhenFolderTarget_ShouldCreateFileAtRightLocation()
        {
            // GIVEN
            string bankFile = FileTesting.CreateFileFromResource("TDUModdingLibraryTests.Resources.banks.pc." + ResourceFile,
                Path.Combine(_tempPath, ResourceFile));
            BNK bnk = TduFile.GetFile(bankFile) as BNK;
            Assert.NotNull(bnk);
            string packedFilePath = @"D:\Eden-Prog\Games\TestDrive\Resources\4Build\PC\EURO\Vehicules\Cars\Mercedes\CLK_55\.2DM\CLK_55";

            // WHEN
            bnk.ExtractPackedFile(packedFilePath, _tempPath, true);

            // THEN
            string expectedFilePath = Path.Combine(_tempPath, "CLK_55.2DM");
            Assert.IsTrue(File.Exists(expectedFilePath));
        }

        [Test]
        public void ExtractPackedFile_WhenFileTarget_ShouldCreateFileAtRightLocation()
        {
            // GIVEN
            string bankFile = FileTesting.CreateFileFromResource("TDUModdingLibraryTests.Resources.banks.pc." + ResourceFile,
                Path.Combine(_tempPath, ResourceFile) );
            BNK bnk = TduFile.GetFile(bankFile) as BNK;
            Assert.NotNull(bnk);
            string packedFilePath = @"D:\Eden-Prog\Games\TestDrive\Resources\4Build\PC\EURO\Vehicules\Cars\Mercedes\CLK_55\.2DM\CLK_55";

            // WHEN
            string expectedFilePath = Path.Combine(_tempPath, "extracted.CLK_55.2DM");
            bnk.ExtractPackedFile(packedFilePath, expectedFilePath, false);

            // THEN
            Assert.IsTrue(File.Exists(expectedFilePath));
        }
    }
}

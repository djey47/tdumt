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
        private string ResourceFile { get; set; }

        private static string _tempPath;

        [OneTimeSetUp]
        public static void SetUp()
        {
            _tempPath = FileTesting.CreateTempDirectory();
        }

        [OneTimeTearDown]
        public static void MyClassCleanup()
        {
            Directory.Delete(_tempPath, true);
        }

        [Test]
        public void BugScenario_WithAnnoyingByte()
        {
            // GIVEN
            ResourceFile = "hud.bnk";
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

        [Test]
        public void BugScenario_WithInvalidPaddingFileSize()
        {
            // GIVEN
            ResourceFile = "icd_12a2.bnk";
            string bankFile = FileTesting.CreateFileFromResource("tdumtlibtests.Resources.banks.pc." + ResourceFile,
                Path.Combine(_tempPath, ResourceFile));

            // WHEN; loading
            BNK bnk = TduFile.GetFile(bankFile) as BNK;

            // THEN: BNK loaded, padding info size set to 0
            Assert.NotNull(bnk);
            Assert.Zero(bnk.__FileList[135].fileSize);

            // WHEN: extracting
            string packedFilePath = @"D:\Eden-Prog\Games\TestDrive\Resources\4Build\PC\Euro\sound\cinematiks\final\car dealer deluxe\.wav\cd_luxe_buy_car_l_adpcm";
            bnk.ExtractPackedFile(packedFilePath, _tempPath, true);

            // THEN: file extracted with right name and size
            string expectedFilePath = Path.Combine(_tempPath, "cd_luxe_buy_car_l_adpcm.wav");
            FileInfo extractedFileInfo = new FileInfo(expectedFilePath);
            Assert.IsTrue(extractedFileInfo.Exists);
            Assert.AreEqual(100331, extractedFileInfo.Length);

            // WHEN: repacking same file
            bnk.ReplacePackedFile(packedFilePath, expectedFilePath);

            // THEN: target BNK size is correct
            FileInfo updatedBankInfo = new FileInfo(bankFile);
            Assert.AreEqual(4635704, updatedBankInfo.Length);
        }
    }
}

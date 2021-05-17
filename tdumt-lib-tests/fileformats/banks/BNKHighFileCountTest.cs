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
            string bankFile1 = FileTesting.CreateFileFromResource("tdumtlibtests.Resources.banks.pc." + ResourceFile,
                Path.Combine(_tempPath, ResourceFile));
            ResourceFile = "icd_12e2.bnk";
            string bankFile2 = FileTesting.CreateFileFromResource("tdumtlibtests.Resources.banks.pc." + ResourceFile,
                Path.Combine(_tempPath, ResourceFile));

            // WHEN; loading
            BNK bnk1 = TduFile.GetFile(bankFile1) as BNK;
            BNK bnk2 = TduFile.GetFile(bankFile2) as BNK;

            // THEN: BNK loaded, padding info size set to 0
            Assert.NotNull(bnk1);
            Assert.Zero(bnk1.__FileList[135].fileSize);
            Assert.NotNull(bnk2);
            Assert.Zero(bnk2.__FileList[146].fileSize);

            // WHEN: extracting
            string packedFilePath1 = @"D:\Eden-Prog\Games\TestDrive\Resources\4Build\PC\Euro\sound\cinematiks\final\car dealer deluxe\.wav\cd_luxe_buy_car_l_adpcm";
            bnk1.ExtractPackedFile(packedFilePath1, _tempPath, true);
            string packedFilePath2 = @"D:\Eden-Prog\Games\TestDrive\Resources\4Build\PC\Euro\sound\cinematiks\final\car dealer deluxe\.wav\cd_luxe_buy_car_r_adpcm";
            bnk2.ExtractPackedFile(packedFilePath2, _tempPath, true);

            // THEN: file extracted with right name and size
            string expectedFilePath1 = Path.Combine(_tempPath, "cd_luxe_buy_car_l_adpcm.wav");
            FileInfo extractedFileInfo1 = new FileInfo(expectedFilePath1);
            Assert.IsTrue(extractedFileInfo1.Exists);
            Assert.AreEqual(100331, extractedFileInfo1.Length);
            string expectedFilePath2 = Path.Combine(_tempPath, "cd_luxe_buy_car_r_adpcm.wav");
            FileInfo extractedFileInfo2 = new FileInfo(expectedFilePath2);
            Assert.IsTrue(extractedFileInfo2.Exists);
            Assert.AreEqual(100331, extractedFileInfo2.Length);

            // WHEN: repacking same file
            bnk1.ReplacePackedFile(packedFilePath1, expectedFilePath1);
            bnk2.ReplacePackedFile(packedFilePath2, expectedFilePath2);

            // THEN: target BNK size is correct
            FileInfo updatedBankInfo1 = new FileInfo(bankFile1);
            Assert.AreEqual(4635704, updatedBankInfo1.Length);
            FileInfo updatedBankInfo2 = new FileInfo(bankFile2);
            Assert.AreEqual(4635704, updatedBankInfo2.Length);
        }
    }
}

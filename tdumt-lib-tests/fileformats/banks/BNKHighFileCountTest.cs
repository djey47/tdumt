using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.fileformats;
using System.IO;
using NUnit.Framework;
using tdumtlibtests.Common;
using System;

namespace tdumtlibtests.fileformats.banks
{
    [TestFixture]
    public class BNKHighFileCountTest
    {
        private const string _RESOURCE_FILE_PREFIX = "tdumtlibtests.Resources.banks.pc.";

        private static string _TempPath;

        [OneTimeSetUp]
        public static void SetUp()
        {
            _TempPath = FileTesting.CreateTempDirectory();
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            // Comment below to debug with produced files
            Directory.Delete(_TempPath, true);
        }

        [Test]
        public void Scenario_BugWithAnnoyingByte()
        {
            // GIVEN
            string resourceFile = "hud.bnk";
            string bankFile = FileTesting.CreateFileFromResource($"{_RESOURCE_FILE_PREFIX}{resourceFile}",
                Path.Combine(_TempPath, resourceFile));
            BNK bnk = TduFile.GetFile(bankFile) as BNK;
            Assert.NotNull(bnk);

            string packedFilePrefix = @"D:\Eden-Prog\Games\TestDrive\Resources\4Build\PC\EURO\FrontEnd\hud\.2db\";
            string packedFileName = "fla_0026";
            string packedFilePath = $"{packedFilePrefix}{packedFileName}";

            // WHEN: extracting
            bnk.ExtractPackedFile(packedFilePath, _TempPath, true);
            string expectedFilePath = Path.Combine(_TempPath, $"{packedFileName}.2db");
            Assert.IsTrue(File.Exists(expectedFilePath));

            // WHEN: repacking same file
            bnk.ReplacePackedFile(packedFilePath, expectedFilePath);

            // THEN: target BNK size is correct
            FileInfo updatedBankInfo = new FileInfo(bankFile);
            Assert.AreEqual(2213920, updatedBankInfo.Length);

            // THEN: reloading + meta info is correct
            _AssertBNKMeta(bnk, TduFile.GetFile(bankFile) as BNK);
        }

        [Test]
        public void Scenario_BugWithInvalidPaddingFileSize()
        {
            // GIVEN
            string resourceFile1 = "icd_12a2.bnk";
            string bankFile1 = FileTesting.CreateFileFromResource($"{_RESOURCE_FILE_PREFIX}{resourceFile1}",
                Path.Combine(_TempPath, resourceFile1));
            string resourceFile2 = "icd_12e2.bnk";
            string bankFile2 = FileTesting.CreateFileFromResource($"{_RESOURCE_FILE_PREFIX}{resourceFile2}",
                Path.Combine(_TempPath, resourceFile2));

            // WHEN; loading
            BNK bnk1 = TduFile.GetFile(bankFile1) as BNK;
            BNK bnk2 = TduFile.GetFile(bankFile2) as BNK;

            // THEN: BNK loaded, padding info size set to 0
            Assert.NotNull(bnk1);
            Assert.Zero(bnk1.__FileList[135].fileSize);
            Assert.NotNull(bnk2);
            Assert.Zero(bnk2.__FileList[146].fileSize);

            // WHEN: extracting
            string packedFilePrefix = @"D:\Eden-Prog\Games\TestDrive\Resources\4Build\PC\Euro\sound\cinematiks\final\car dealer deluxe\.wav\";
            string packedFileName1 = "cd_luxe_buy_car_l_adpcm";
            string packedFilePath1 = $"{packedFilePrefix}{packedFileName1}";
            bnk1.ExtractPackedFile(packedFilePath1, _TempPath, true);
            string packedFileName2 = "cd_luxe_buy_car_r_adpcm";
            string packedFilePath2 = $"{packedFilePrefix}{packedFileName2}";
            bnk2.ExtractPackedFile(packedFilePath2, _TempPath, true);

            // THEN: file extracted with right name and size
            string expectedFilePath1 = Path.Combine(_TempPath, $"{packedFileName1}.wav");
            FileInfo extractedFileInfo1 = new FileInfo(expectedFilePath1);
            Assert.IsTrue(extractedFileInfo1.Exists);
            Assert.AreEqual(100331, extractedFileInfo1.Length);
            string expectedFilePath2 = Path.Combine(_TempPath, $"{packedFileName2}.wav");
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
            Assert.AreEqual(4258032, updatedBankInfo2.Length);

            // THEN: reloading + meta info is correct
            _AssertBNKMeta(bnk1, TduFile.GetFile(bankFile1) as BNK);
            _AssertBNKMeta(bnk2, TduFile.GetFile(bankFile2) as BNK);
        }

        private static void _AssertBNKMeta(BNK expected, BNK actual)
        {
            Assert.AreEqual(DateTime.Now.Year, actual.Year);
            Assert.AreEqual(expected.PackedContentSize, actual.PackedContentSize);
            Assert.AreEqual(expected.PackedFilesCount, actual.PackedFilesCount);
            Assert.AreEqual(expected.GetPackedFilesPaths(null), actual.GetPackedFilesPaths(null));
            Assert.AreEqual(expected.GetPackedFilesPathsByExtension(null), actual.GetPackedFilesPathsByExtension(null));
        }
    }
}

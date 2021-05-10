using System.IO;
using NUnit.Framework;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.banks;
using tdumtlibtests.Common;

namespace tdumtlibtests.fileformats.banks
{
    [TestFixture]
    [Ignore("Unsupported feature")]
    public class BnkFocusOnXenonTest
    {
        private const string ResourceFile = "246_GT.bnk";

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

        // FIXME handle Xenon BNKs
        [Test]
        public void LoadBnkFileForXenonPlatform_ShouldNotCrash()
        {
            // GIVEN
            string bankFile = FileTesting.CreateFileFromResource("TDUModdingLibraryTests.Resources.banks.xenon." + ResourceFile,
                Path.Combine(_tempPath, ResourceFile));

            // WHEN
            BNK bnk = TduFile.GetFile(bankFile) as BNK;

            // THEN
            Assert.NotNull(bnk);
        }
    }
}

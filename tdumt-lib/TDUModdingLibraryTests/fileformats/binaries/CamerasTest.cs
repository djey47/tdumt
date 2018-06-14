using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TDUModdingLibrary.fileformats.binaries;
using TDUModdingLibraryTests.Common;

namespace TDUModdingLibraryTests.fileformats.binaries
{
    [TestFixture]
    public class CamerasTest
    {
        private const string GenuineCamerasResourceFile = "Cameras-1.66A-MP.bin";
        private const string ExtendedCamerasResourceFile = "Cameras-2.00A.bin";

        private static string _tempPath;

        [OneTimeSetUp]
        public static void SetUp()
        {
            _tempPath = FileTesting.CreateTempDirectory();
        }

        [OneTimeTearDown]
        public static void MyClassCleanup()
        {
            File.Delete(Path.Combine(_tempPath, GenuineCamerasResourceFile));
            File.Delete(Path.Combine(_tempPath, ExtendedCamerasResourceFile));
        }

        [Test]
        public void ReadData_ShouldReadGenuineCameras()
        {
            // GIVEN
            var camFile =
                FileTesting.CreateFileFromResource("TDUModdingLibraryTests.Resources.cameras." +
                                                   GenuineCamerasResourceFile, Path.Combine(_tempPath, GenuineCamerasResourceFile));

            // WHEN
            var cameras = new Cameras(camFile);

            // THEN
            Assert.NotNull(cameras);
            Assert.AreEqual(150, cameras.Index.Count);
            Assert.AreEqual(148, cameras.Entries.Count);
        }

        [Test]
        public void ReadData_ShouldReadExtendedCameras()
        {
            // GIVEN
            var camFile =
                FileTesting.CreateFileFromResource("TDUModdingLibraryTests.Resources.cameras." +
                                                   ExtendedCamerasResourceFile, Path.Combine(_tempPath, ExtendedCamerasResourceFile));

            // WHEN
            var cameras = new Cameras(camFile);

            // THEN
            Assert.NotNull(cameras);
            Assert.AreEqual(174, cameras.Index.Count);
            Assert.AreEqual(172, cameras.Entries.Count);
        }

        [Test]
        public void SaveData_ShouldSaveExtendedCameras()
        {
            // GIVEN
            var camFile =
                FileTesting.CreateFileFromResource("TDUModdingLibraryTests.Resources.cameras." +
                                                   ExtendedCamerasResourceFile, Path.Combine(_tempPath, ExtendedCamerasResourceFile));
            var initialCameras = new Cameras(camFile);

            // WHEN
            string savedFileName = Path.Combine(_tempPath, "saved-" + ExtendedCamerasResourceFile);
            initialCameras.SaveAs(savedFileName);

            // THEN
            var savedCameras = new Cameras(savedFileName);
            Assert.AreEqual(initialCameras.Index.Count, savedCameras.Index.Count);
            Assert.AreEqual(initialCameras.Entries.Count, savedCameras.Entries.Count);
        }

        [Test]
        public void UpdateEntry_WhenNoChangeMade_ShouldNotAffectIt()
        {
            // GIVEN
            var camFile =
                FileTesting.CreateFileFromResource("TDUModdingLibraryTests.Resources.cameras." +
                                                   ExtendedCamerasResourceFile, Path.Combine(_tempPath, ExtendedCamerasResourceFile));
            var cameras = new Cameras(camFile);
            Cameras.CamEntry modifiedEntry = cameras.GetEntryByCameraId("101");

            // WHEN
            cameras.UpdateEntry(modifiedEntry);

            // THEN
            Assert.AreEqual(modifiedEntry, cameras.GetEntryByCameraId("101"));
        }

        [Test]
        public void UpdateEntry_WhenReplacedView_ShouldAffectIt()
        {
            // GIVEN
            var camFile =
                FileTesting.CreateFileFromResource("TDUModdingLibraryTests.Resources.cameras." +
                                                   ExtendedCamerasResourceFile, Path.Combine(_tempPath, ExtendedCamerasResourceFile));
            var cameras = new Cameras(camFile);
            var baseEntry = cameras.GetEntryByCameraId("101");
            var modifiedEntry = baseEntry;
            var modifiedView = modifiedEntry.views[0];
            modifiedView.type = Cameras.ViewType.Follow_Large_Back;

            modifiedEntry.views = new List<Cameras.View>();
            modifiedEntry.views.AddRange(baseEntry.views);
            modifiedEntry.views[0] = modifiedView;

            // WHEN
            cameras.UpdateEntry(modifiedEntry);

            // THEN
            Assert.AreNotEqual(modifiedEntry, baseEntry);
        }
    }
}

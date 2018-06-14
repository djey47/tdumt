using System.IO;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.support;

namespace TDUModdingLibrary.installer.steps
{
    /// <summary>
    /// Class providing static methods to prepare install process 
    /// </summary>
    static class PreparatoryTask
    {
        #region Members
        /// <summary>
        /// Current slot storage folder
        /// </summary>
        private static string _SlotFolder = null;

        /// <summary>
        /// Current mod folder
        /// </summary>
        private static string _ModFolder = null;
        #endregion

        #region Internal methods
        /// <summary>
        /// What the task must do
        /// </summary>
        internal static void Run()
        {
            // 1. Mods parent folder
            DirectoryInfo di = new DirectoryInfo(InstallHelper.BasePath);

            if (!di.Exists)
                di.Create();

            // 2. Current slot folder
            string slotRef = InstallHelper.CurrentPatch.SlotRef;

            if (string.IsNullOrEmpty(slotRef))
                slotRef = Tools.KEY_MISC_SLOT;

            _SlotFolder = string.Concat(InstallHelper.BasePath, slotRef, @"\");
            di = new DirectoryInfo(_SlotFolder);

            if (!di.Exists)
                di.Create();

            // Creates folders for backup
            _CreatePartFolders(_SlotFolder);

            // 3. Current mod folder
            _ModFolder = InstallHelper.CurrentPatch.Name;

            if (string.IsNullOrEmpty(_ModFolder))
            {
                Log.Info("Warning: current mod has no name. Please provide one in the future... now using NONAME");
                _ModFolder = InstallHelper.DEFAULT_MOD_NAME;
            }
            else
                _ModFolder = Directory2.GetCleanName(_ModFolder);

            di = new DirectoryInfo(_SlotFolder + _ModFolder);

            if (!di.Exists)
                di.Create();

            // 4. Parts folders
            _CreatePartFolders(di.FullName);

            // 5. Remapping
            Directory.CreateDirectory(InstallHelper.RemapPath);
            InstallHelper.RemappedFiles.Clear();

            // Folder information
            InstallHelper.SlotPath = _SlotFolder + @"\";
            InstallHelper.ModPath = di.FullName + @"\";
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Creates all needed parts folders under specified parent
        /// </summary>
        /// <param name="parentFolder"></param>
        private static void _CreatePartFolders(string parentFolder)
        {
            if (Directory.Exists(parentFolder))
            {
                // Jauges-high
                string folderToCreate = parentFolder + InstallHelper.FOLDER_HI_GAUGE_PART;
                DirectoryInfo di = new DirectoryInfo(folderToCreate);

                if (!di.Exists)
                    di.Create();

                // Jauges-Low
                folderToCreate = parentFolder + InstallHelper.FOLDER_LO_GAUGE_PART;
                di = new DirectoryInfo(folderToCreate);

                if (!di.Exists)
                    di.Create();
            }
            else
                throw new DirectoryNotFoundException("Specified parent folder does not exist: " + parentFolder);
        }
        #endregion
    }
}
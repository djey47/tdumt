using System;
using System.Collections.Generic;
using DjeFramework1.Common.Support.Traces;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.support;
using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.installer.steps
{
    /// <summary>
    /// Class providing static methods to finalize mod install 
    /// </summary>
    internal static class FinalizationTask
    {
        #region Internal methods
        /// <summary>
        /// What the task must do
        /// </summary>
        /// <param name="isInstall">true to install, false to uninstall</param>
        internal static void Run(bool isInstall)
        {
            //1. Magic Map
            try
            {
                string mapFileName = string.Concat(Tools.TduPath, LibraryConstants.FOLDER_BNK, MAP.FILE_MAP);
                MAP mapFile = TduFile.GetFile(mapFileName) as MAP;

                if (mapFile != null)
                    mapFile.PatchIt(false);
            }
            catch (Exception ex)
            {
                throw new Exception("Error when trying to convert to Magic Map", ex);
            }

            //2. Radial
            try
            {
                Tools.DeleteRadial();
            }
            catch (Exception ex)
            {
                // Silent error
                Log.Error("Error when trying to delete radial", ex);
            }

            //3. Registry fix
            try
            {
                Tools.FixRegistry(Tools.TduPath);
            }
            catch (Exception ex)
            {
                // Silent error
                Log.Error("Error when trying to fix TDU registry information.", ex);
            }

            //4. Patch copy
            // Currently disabled.
            /*try
            {
                FileInfo fi = new FileInfo(InstallHelper.CurrentPatch.FileName);

                File.Copy(InstallHelper.CurrentPatch.FileName, InstallHelper.ModPath + fi.Name);
            }
            catch (Exception ex)
            {
                // Silent error
                Log.Error("Error when trying to copy patch to mod folder: " + InstallHelper.CurrentPatch.FileName + " > " + InstallHelper.ModPath, ex);
            }*/

            // 5. Putting or removing install flag in slot folder
            string flagFile = string.Concat(InstallHelper.SlotPath, InstallHelper.FILE_INSTALL_FLAG);

            try
            {
                InstallHelper._UpdateFlagFile(flagFile, isInstall, null);
            }
            catch (Exception ex)
            {
                // Silent error
                Log.Error("Error when trying to update install flag: " + flagFile, ex);
            }

            // 6. Putting or removing remapping flag(s) in REMAPPING slot folder
            foreach (KeyValuePair<string, string> anotherRemappedFile in InstallHelper.RemappedFiles)
            {                
                flagFile = string.Concat(InstallHelper.RemapPath, string.Format(InstallHelper.FORMAT_FILE_REMAP_FLAG, anotherRemappedFile.Key));

                try
                {
                    InstallHelper._UpdateFlagFile(flagFile, isInstall, anotherRemappedFile.Value );
                }
                catch (Exception ex)
                {
                    // Silent error
                    Log.Error("Error when trying to update remapping flag: " + flagFile, ex);
                }
            }
        }
        #endregion
    }
}
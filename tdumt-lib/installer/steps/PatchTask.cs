using System;
using TDUModdingLibrary.support.patcher;
using System.Windows.Forms;

namespace TDUModdingLibrary.installer.steps
{
    /// <summary>
    /// Class providing static methods to execute patch 
    /// </summary>
    class PatchTask
    {
        #region Constants
        /// <summary>
        /// Error message when a critical error occurs during execution
        /// </summary>
        private const string _ERROR_RUN_FAILED = "Patch was not applied as a critical error occured.\r\nPlease contact author of this mod then Modding Tools support, eventually.\r\n({0})";

        /// <summary>
        /// Error message when an error occurs during execution
        /// </summary>
        private const string _MESSAGE_PATCH_ERROR = "Patch was not applied as an error occured.\r\nPlease refer to the log files for more information.";

        /// <summary>
        /// Error message when warning(s) occur(s) during execution
        /// </summary>
        private const string _MESSAGE_PATCH_WARNINGS = "Patch was applied, but at least one warning was given.\r\nPlease refer to the log file for more information.";

        /// <summary>
        /// Warning message when patch did not run at all
        /// </summary>
        private const string _WARNING_NO_INSTRUCTION =
            "There are no instructions to run.\r\nProvided patch does not seem to be compliant.";
        #endregion

        #region Internal methods
        /// <summary>
        /// What the task must do
        /// </summary>
        internal static void Run()
        {
            try
            {
                // Running
                PatchHelper.RunResult result =
                    PatchHelper.RunPatch(InstallHelper.CurrentPatch,
                        InstallHelper.Log,
                        Application.StartupPath,
                        InstallHelper.Culture,
                        InstallHelper.InstallGroups);

                // Result analysis
                switch (result)
                {
                    case PatchHelper.RunResult.NotRun:
                        throw new Exception(_WARNING_NO_INSTRUCTION);
                    case PatchHelper.RunResult.RunWithErrors:
                        throw new Exception(_MESSAGE_PATCH_ERROR);
                    case PatchHelper.RunResult.RunWithWarnings:
                        // Not critical
                        PatchHelper.AddMessage(_MESSAGE_PATCH_WARNINGS);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                // Critical error
                string errorMessage = string.Format(_ERROR_RUN_FAILED, ex.Message);
                throw new Exception(errorMessage, ex);
            }
        }
        #endregion
    }
}
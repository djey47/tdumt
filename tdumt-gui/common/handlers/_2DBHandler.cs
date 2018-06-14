using System;
using System.IO;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.converters;
using TDUModdingLibrary.fileformats.graphics;
using TDUModdingLibrary.support.constants;

namespace TDUModdingTools.common.handlers
{
    /// <summary>
    /// Handles processing of .2DB image files in TDUMT
    /// </summary>
    class _2DBHandler:FileHandler
    {
        #region Properties
        /// <summary>
        /// Editeur par défaut
        /// </summary>
        public static new String DefaultEditor
        {
            get { return "your DDS editor"; }
        }
        #endregion

        #region FileHandler implementation
        /// <summary>
        /// Implémentation de l'édition
        /// </summary>
        public override void Edit()
        {
            if (!string.IsNullOrEmpty(FileName))
            {
                // L'édition d'un fichier 2DB passe par une conversion en DDS
                string ddsFilename = File2.ChangeExtensionOfFilename(FileName, LibraryConstants.EXTENSION_DDS_FILE);

                // BUG_49: if DDS file already exists, then open it directly
                if (!File.Exists(ddsFilename))
                    GraphicsConverters._2DBToDDS(FileName, ddsFilename);

                // Lancement de l'éditeur par défaut
                __SystemRun(ddsFilename);
            }
        }

        /// <summary>
        /// Application des modifs. 
        /// </summary>
        /// <param name="paramList">Paramètre booléen [0] maintainSize : true pour maintenir la taille, false sinon</param>
        public override void Apply(params object[] paramList)
        {
            if (!string.IsNullOrEmpty(FileName))
            {
                // Si le fichier d'origine est un 2DB, le DDS doit être reconverti
                string ddsFileName = File2.ChangeExtensionOfFilename(FileName, LibraryConstants.EXTENSION_DDS_FILE);

                try
                {
                    // Utilisation d'une copie pour l'en-tête
                    string oldFileName = File2.ChangeExtensionOfFilename(FileName, LibraryConstants.EXTENSION_2DB_OLD_FILE);

                    File.Copy(FileName, oldFileName, true);

                    GraphicsConverters.DDSTo2DB(oldFileName, ddsFileName, FileName, GraphicsConverters.KEEP_ORIGINAL_TEXTURE_NAME, GraphicsConverters.KEEP_ORIGINAL_MIPMAP_COUNT);
                }
                catch (Exception ex)
                {
                    string message = string.Format(_ERROR_APPLY, ddsFileName, FileName);

                    throw new Exception(message, ex);
                }
            }
        }
        #endregion
    }
}
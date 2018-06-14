using System;

namespace TDUModdingLibrary.fileformats.interfaces
{
    /// <summary>
    /// Interface des classes spécialisées dans le stockage des textures.
    /// </summary>
    interface ITextureFile
    {
        #region Properties
        /// <summary>
        /// En-tête du fichier
        /// </summary>
        ValueType Header { get; set; }

        /// <summary>
        /// Données de l'en-tête
        /// </summary>
        byte[] HeaderData { get; }

        /// <summary>
        /// Données de l'image
        /// </summary>
        byte[] ImageData { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Returns texture format name
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        string GetFormatName(uint format);
        #endregion
    }
}
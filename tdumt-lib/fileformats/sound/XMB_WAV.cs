using System;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.fileformats.sound
{
    /// <summary>
    /// Représente un fichier XMB_WAV, qui est en fait un WAV précédé d'un en-tête XMB
    /// </summary>
    public class XMB_WAV:TduFile
    {
        #region Constantes
        /// <summary>
        /// Pattern for file name (to redefine)
        /// </summary>
        public new static readonly string FILENAME_PATTERN = string.Format(String2.REGEX_PATTERN_EXTENSION, LibraryConstants.EXTENSION_WAV_FILE); 
        #endregion
        
        #region Properties
        /// <summary>
        /// Attached sound parameters
        /// </summary>
        public XMB SoundParameters { get; set; }
        #endregion

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal XMB_WAV() { }

        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="fileName"></param>
        internal XMB_WAV(string fileName)
        {
            // On doit vérifier s'il s'agit bien d'un mixte XMB_WAV. Le cas contraire, on lève une exception
            throw new FormatException();
        }

        #region TDUFile implementation
        /// <summary>
        /// 
        /// </summary>
        protected override void _ReadData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the current file to disk. To implement if necessary.
        /// </summary>
        public override void Save()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
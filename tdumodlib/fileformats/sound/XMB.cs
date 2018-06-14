using System;
using DjeFramework1.Common.Types;

namespace TDUModdingLibrary.fileformats.sound
{
    /// <summary>
    /// Représente un fichier XMB (traitement sonore)
    /// Le contenu de ce fichier n'est pas encore décodé...
    /// </summary>
    public class XMB:TduFile
    {
        #region Constantes
        /// <summary>
        /// Pattern for file name (to redefine)
        /// </summary>
        public new static readonly string FILENAME_PATTERN = string.Format(String2.REGEX_PATTERN_EXTENSION, "XMB");
        #endregion
        
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        internal XMB() { }

        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="fileName"></param>
        internal XMB(string fileName)
        {}

        #region TDUFile implementation
        protected override void _ReadData()
        {
            throw new NotImplementedException();
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
        #endregion        
    }
}
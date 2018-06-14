using System;
using DjeFramework1.Common.Types;

namespace TDUModdingLibrary.fileformats.physics
{
    class BTRQ:TduFile
    {
        #region Constants
        /// <summary>
        /// Pattern for file name (to redefine)
        /// </summary>
        public new static readonly string FILENAME_PATTERN = string.Format(String2.REGEX_PATTERN_EXTENSION, "BTRQ");
        #endregion

        /// <summary>
        /// Lit les données du fichier. A implémenter si nécessaire
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
    }
}
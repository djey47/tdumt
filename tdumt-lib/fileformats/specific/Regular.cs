using System;

namespace TDUModdingLibrary.fileformats.specific
{
    /// <summary>
    /// Represents a file which is not supported internally
    /// </summary>
    public class Regular:TduFile
    {
        /// <summary>
        /// Lit les données du fichier.
        /// </summary>
        protected override void _ReadData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the current file to disk.
        /// </summary>
        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}

using System;

namespace TDUModdingTools.common.handlers
{
    class TrackHandler:FileHandler
    {
        #region Overrides of FileHandler

        /// <summary>
        /// Réalise l'édition du fichier. A redéfinir dans chaque sous-classe si nécessaire.
        /// </summary>
        public override void Edit()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Met à jour les modifications réalisées sur ce fichier.
        /// Utile pour les types de fichiers nécessitant une re-conversion (ex: DDS)
        /// A redéfinir dans chaque sous-classe
        /// </summary>
        /// <param name="paramList">Paramètres éventuels</param>
        public override void Apply(params object[] paramList)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

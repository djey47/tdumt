using System;
using TDUModdingTools.common.handlers;

namespace TDUModdingTools.common.handlers.database
{
    /// <summary>
    /// Handler for encrypted database files
    /// </summary>
    class DBHandler:FileHandler
    {
        /// <summary>
        /// Réalise l'édition du fichier. A redéfinir dans chaque sous-classe si nécessaire.
        /// </summary>
        public override void Edit()
        {
            // Test
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
    }
}
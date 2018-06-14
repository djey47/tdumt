using TDUModdingTools.common.handlers;

namespace TDUModdingTools.common.handlers.specific
{
    /// <summary>
    /// Handler for files that are not managed by Modding Tools
    /// </summary>
    class RegularHandler:FileHandler
    {
        /// <summary>
        /// Réalise l'édition du fichier.
        /// </summary>
        public override void Edit()
        {
            // Editing unsupported internally, using Windows default editor
            __SystemRun(FileName);
        }

        /// <summary>
        /// Met à jour les modifications réalisées sur ce fichier.
        /// Utile pour les types de fichiers nécessitant une re-conversion (ex: DDS)
        /// </summary>
        /// <param name="paramList">Paramètres éventuels</param>
        public override void Apply(params object[] paramList)
        {
            // Nohting to do
        }
    }
}
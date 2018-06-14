namespace TDUModdingTools.common.handlers
{
    /// <summary>
    /// Handles processing of DDS image files
    /// </summary>
    class DDSHandler:FileHandler
    {
        #region Properties
        /// <summary>
        /// Editeur par défaut
        /// </summary>
        public static new string DefaultEditor
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
            // Lancement de l'éditeur par défaut
            __SystemRun(FileName);
        }

        /// <summary>
        /// Application des modifs
        /// </summary>
        public override void Apply(params object[] paramList)
        {
            // Non nécessaire, le DDS n'est pas géré nativement
        }
        #endregion
    }
}
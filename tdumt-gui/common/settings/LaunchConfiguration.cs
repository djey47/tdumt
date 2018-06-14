using System.Collections.ObjectModel;

namespace TDUModdingTools.common.settings
{
    /// <summary>
    /// Classe représentant une configuration de lancement pour TDU
    /// </summary>
    class LaunchConfiguration
    {
        #region Properties
        /// <summary>
        /// Le radial.cdb doit-il être effacé ?
        /// </summary>
        public bool CleanRadial { get; set; }

        /// <summary>
        /// TDU doit-il être lancé en mode fenêtré ?
        /// </summary>
        public bool WindowedMode { get; set; }

        /// <summary>
        /// Le compteur de fps doit-il être activé ?
        /// </summary>
        public bool FpsDisplayed { get; set; }

        /// <summary>
        /// Will coordinates appear in top-left corner 
        /// </summary>
        public bool PosDisplayed { get; set; }

        /// <summary>
        /// Ligne de commande à exécuter avant le lancement de TDU
        /// </summary>
        public string PreviousCommand { get; set; }

        /// <summary>
        /// Ligne de commande à exécuter juste après le lancement de TDU
        /// </summary>
        public string NextCommand { get; set; }

        /// <summary>
        /// Indique si la configuration est celle par défaut
        /// </summary>
        public bool Default { get; set; }

        /// <summary>
        /// Nom de la configuration
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name;

        public LaunchConfiguration()
        {
            Default = false;
            NextCommand = null;
            PreviousCommand = null;
        }
        #endregion

        #region Méthodes privées
        /// <summary>
        /// Renvoie la configuration par défaut parmi la liste de configurations
        /// </summary>
        /// <param name="configList">Liste de configurations de lancement</param>
        /// <returns>La configuration par défaut, ou null si aucune correspondance</returns>
        internal static LaunchConfiguration GetDefaultConfiguration(Collection<LaunchConfiguration> configList)
        {
            LaunchConfiguration config = null;

            if (configList != null)
            {
                // Parcours de la liste
                foreach (LaunchConfiguration anotherConfig in configList)
                {
                    if (anotherConfig.Default)
                    {
                        config = anotherConfig;
                        break;
                    }
                }
            }

            return config;
        }

        /// <summary>
        /// Renvoie la configuration de nom spécifié parmi la liste de configurations
        /// </summary>
        /// <param name="configList">Liste de configurations de lancement</param>
        /// <param name="configName">Nom de la configuration recherchée</param>
        /// <returns>La configuration recherchée, ou null si aucune correspondance</returns>
        internal static LaunchConfiguration GetConfigurationByName(Collection<LaunchConfiguration> configList, string configName)
        {
            LaunchConfiguration config = null;

            if (configList != null && configName != null)
            {
                // Parcours de la liste
                foreach (LaunchConfiguration anotherConfig in configList)
                {
                    if (configName.Equals(anotherConfig._Name))
                    {
                        config = anotherConfig;
                        break;
                    }
                }
            }

            return config;
        }
        #endregion
    }
}
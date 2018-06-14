using System;
using System.Collections.ObjectModel;
using System.Text;
using DjeFramework1.Common.Types;

namespace TDUModdingTools.common.settings
{
    static class LaunchConfigurationConverter
    {
        #region Constantes
        /// <summary>
        /// Séparateur de valeurs
        /// </summary>
        private const char _VALUE_SEPARATOR = '|';

        /// <summary>
        /// Séparateur d'items
        /// </summary>
        private const char _ITEM_SEPARATOR = '¤';
        #endregion

        /// <summary>
        /// Convertit de chaîne en Config de lancement
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Collection<LaunchConfiguration> ConvertFromString(string value)
        {
            Collection<LaunchConfiguration> returnedConfigList = null;

            // Paramètres sous forme de chaîne
            if (value != null)
            {
                string[] itemList = value.Split(_ITEM_SEPARATOR);
                LaunchConfiguration result;

                returnedConfigList = new Collection<LaunchConfiguration>();

                foreach (string item in itemList)
                {
                    string[] paramList = item.Split(_VALUE_SEPARATOR);

                    result = new LaunchConfiguration();

                    try
                    {
                        // 0. Nom
                        result.Name = paramList[0];

                        // 1. Default
                        result.Default = bool.Parse(paramList[1]);

                        // 2. Mode fenetré
                        result.WindowedMode = bool.Parse(paramList[2]);

                        // 3. FPS
                        result.FpsDisplayed = bool.Parse(paramList[3]);

                        // 4. Nettoyage radial
                        result.CleanRadial = bool.Parse(paramList[4]);

                        // 5. Commande avant lancement
                        result.PreviousCommand = paramList[5];

                        // 6. Commande après lancement
                        result.NextCommand = paramList[6];

                        // 7. Coordinates
                        if (paramList.Length > 7)
                            result.PosDisplayed = bool.Parse(paramList[7]); 

                        returnedConfigList.Add(result);
                    }
                    catch (Exception ex)
                    {
                        Exception2.PrintStackTrace(ex);
                    }
                }
            }

            return returnedConfigList;
        }

        /// <summary>
        /// Convertit de liste de config de lancement en chaîne
        /// </summary>
        /// <param name="configList"></param>
        /// <returns></returns>
        public static string ConvertToString(Collection<LaunchConfiguration> configList) 
        {
            string returnedValue = null;

            if (configList != null)
            {
                StringBuilder sbResult = new StringBuilder();
                int itemCount = 0;

                foreach (LaunchConfiguration config in configList)
                {
                    itemCount++;

                    try
                    {
                        // 0. Nom
                        sbResult.Append(config.Name);
                        sbResult.Append(_VALUE_SEPARATOR);

                        // 1. Default
                        sbResult.Append(config.Default.ToString());
                        sbResult.Append(_VALUE_SEPARATOR);

                        // 2. Mode fenetré
                        sbResult.Append(config.WindowedMode.ToString());
                        sbResult.Append(_VALUE_SEPARATOR);

                        // 3. FPS
                        sbResult.Append(config.FpsDisplayed.ToString());
                        sbResult.Append(_VALUE_SEPARATOR);

                        // 4. Nettoyage radial
                        sbResult.Append(config.CleanRadial.ToString());
                        sbResult.Append(_VALUE_SEPARATOR);

                        // 5. Commande avant lancement
                        if (config.PreviousCommand == null)
                            sbResult.Append("");
                        else
                            sbResult.Append(config.PreviousCommand);

                        sbResult.Append(_VALUE_SEPARATOR);

                        // 6. Commande après lancement
                        if (config.NextCommand == null)
                            sbResult.Append("");
                        else
                            sbResult.Append(config.NextCommand);

                        sbResult.Append(_VALUE_SEPARATOR);

                        // 7. Coordinates
                        sbResult.Append(config.PosDisplayed.ToString());

                        // Item suivant
                        if (itemCount != configList.Count)
                            sbResult.Append(_ITEM_SEPARATOR);
                    }
                    catch (Exception ex)
                    {
                        Exception2.PrintStackTrace(ex);
                    }                    
                }

                returnedValue = sbResult.ToString();
            }

            return returnedValue;
        }
    }
}
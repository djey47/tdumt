using System.Collections.ObjectModel;
using TDUModdingLibrary.fileformats.database;

namespace TDUModdingLibrary.support.patcher.parameters.providers
{
    class ResourceFileNameVP:IValuesProvider
    {
        #region IValuesProvider Members
        /// <summary>
        /// Provides common values for this type of instruction
        /// </summary>
        public Collection<string> Values
        {
            get
            {
                Collection<string> values = new Collection<string>();

                values.Add(DB.Topic.Achievements.ToString());
                values.Add(DB.Topic.AfterMarketPacks.ToString());
                values.Add(DB.Topic.Bots.ToString());
                values.Add(DB.Topic.Brands.ToString());
                values.Add(DB.Topic.CarColors.ToString());
                values.Add(DB.Topic.CarPacks.ToString());
                values.Add(DB.Topic.CarPhysicsData.ToString());
                values.Add(DB.Topic.CarRims.ToString());
                values.Add(DB.Topic.CarShops.ToString());
                values.Add(DB.Topic.Clothes.ToString());
                values.Add(DB.Topic.Hair.ToString());
                values.Add(DB.Topic.Houses.ToString());
                values.Add(DB.Topic.Interior.ToString());
                values.Add(DB.Topic.Menus.ToString());
                values.Add(DB.Topic.PNJ.ToString());
                values.Add(DB.Topic.Rims.ToString());
                values.Add(DB.Topic.SubTitles.ToString());
                values.Add(DB.Topic.Tutorials.ToString());

                return values;
            }
        }

        /// <summary>
        /// Returns real value from chosen label (to handle id-label associations)
        /// </summary>
        /// <param name="chosenLabel"></param>
        /// <returns></returns>
        public string GetValueFromLabel(string chosenLabel)
        {
            return chosenLabel;
        }
        #endregion
    }
}
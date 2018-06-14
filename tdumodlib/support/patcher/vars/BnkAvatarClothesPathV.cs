using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.support.patcher.vars
{
    /// <summary>
    /// Variable pointing Euro\Bnk\Avatar\CLOTHES folder
    /// </summary>
    class BnkAvatarClothesPathV : PatchVariable
    {
        /// <summary>
        /// Variable name
        /// </summary>
        public override string Name
        {
            get { return VariableName.bnkAvatarClothesPath.ToString(); }
        }

        /// <summary>
        /// Variable description
        /// </summary>
        public override string Description
        {
            get { return @"Folder where common clothes model BNK files are stored (...\Test Drive Unlimited\Euro\Bnk\Avatar\CLOTHES)"; }
        }

        /// <summary>
        /// Which value the variable should refer to
        /// </summary>
        internal override string GetValue()
        {
            return LibraryConstants.GetSpecialFolder(LibraryConstants.TduSpecialFolder.AvatarClothes);
        }
    }
}
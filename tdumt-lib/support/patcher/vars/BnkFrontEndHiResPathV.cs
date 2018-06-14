using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.support.patcher.vars
{
    /// <summary>
    /// Variable pointing Euro\Bnk\FrontEnd\HiRes folder
    /// </summary>
    class BnkFrontEndHiResPathV:PatchVariable
    {
        /// <summary>
        /// Variable name
        /// </summary>
        public override string Name
        {
            get { return VariableName.bnkFrontEndHiResPath.ToString(); }
        }

        /// <summary>
        /// Variable description
        /// </summary>
        public override string Description
        {
            get { return @"Folder where menus hi-res texture BNK files are stored (...\Test Drive Unlimited\Euro\Bnk\FrontEnd\HiRes)"; }
        }

        /// <summary>
        /// Which value the variable should refer to
        /// </summary>
        internal override string GetValue()
        {
            return LibraryConstants.GetSpecialFolder(LibraryConstants.TduSpecialFolder.FrontEndHiRes);
        }        
    }
}
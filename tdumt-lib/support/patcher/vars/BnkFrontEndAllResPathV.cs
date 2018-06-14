using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.support.patcher.vars
{
    /// <summary>
    /// Variable pointing Euro\Bnk\FrontEnd\AllRes folder
    /// </summary>
    class BnkFrontEndAllResPathV:PatchVariable
    {
        /// <summary>
        /// Variable name
        /// </summary>
        public override string Name
        {
            get { return VariableName.bnkFrontEndAllResPath.ToString(); }
        }

        /// <summary>
        /// Variable description
        /// </summary>
        public override string Description
        {
            get { return @"Folder where common all-res texture BNK files are stored (...\Test Drive Unlimited\Euro\Bnk\FrontEnd\AllRes)"; }
        }

        /// <summary>
        /// Which value the variable should refer to
        /// </summary>
        internal override string GetValue()
        {
            return LibraryConstants.GetSpecialFolder(LibraryConstants.TduSpecialFolder.FrontEndAllRes);
        }
    }
}
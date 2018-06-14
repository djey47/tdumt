using System.IO;

namespace TDUModdingLibrary.support.patcher.vars
{
    /// <summary>
    /// Variable referring to system's temporary folder
    /// </summary>
    class TempPathV:PatchVariable
    {
        public override string Name
        {
            get { return VariableName.tempPath.ToString(); }
        }

        public override string Description
        {
            get { return "Windows temporary folder"; }
        }

        internal override string GetValue()
        {
            //Il s'agit bien du dossier temp utilisateur
            return Path.GetTempPath();
        }
    }
}

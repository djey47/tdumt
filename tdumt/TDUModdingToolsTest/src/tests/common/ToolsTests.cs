using DjeFramework1.Common.Support.Traces;
using TDUModdingLibrary.support;

namespace TDUModdingToolsTest.tests.common
{
    class ToolsTests
    {
        public static string NormalizeNameTest(string objectName)
        {
            string result = Tools.NormalizeName(objectName);

            Log.Info(objectName + " -> " + result);
            Log.Info("");

            return result;
        }

        public static void OutlineExtendedCharactersTest(string objectName)
        {
            string result = Tools.OutlineExtendedCharacters(objectName);

            Log.Info(objectName + " -> " + result);
            Log.Info("");
        }
    }
}
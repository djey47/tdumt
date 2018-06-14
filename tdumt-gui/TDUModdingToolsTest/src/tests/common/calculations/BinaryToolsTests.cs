using DjeFramework1.Common.Calculations;

namespace TDUModdingToolsTest.tests.common.calculations
{
    class BinaryToolsTests
    {
        internal static void TestIntegerToBits()
        {
            uint value = 153;

            bool[] bits = BinaryTools.IntegerToBits(value);
        }
    }
}
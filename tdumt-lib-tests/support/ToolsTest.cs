using TDUModdingLibrary.support;
using DjeFramework1.Util.BasicStructures;
using System.Collections.Generic;
using NUnit.Framework;

namespace tdumtlibtests.support
{
    [TestFixture]
    public class ToolsTest
    {
        [Test]
        public void ParseCouples()
        {
            //Given
            string values = "123|Test||456|Test2||";
            Couple<string> expectedCouple1 = new Couple<string>("123","Test");
            Couple<string> expectedCouple2 = new Couple<string>("456", "Test2");

            //When
            List<Couple<string>> actualCouples = Tools.ParseCouples(values);

            //Then
            Assert.AreEqual(2, actualCouples.Count);
            Assert.AreEqual(expectedCouple1, actualCouples[0]);
            Assert.AreEqual(expectedCouple2, actualCouples[1]);
        }
    }
}

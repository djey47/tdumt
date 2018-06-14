using System;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Common.Support.Traces.Appenders;
using TDUModdingToolsTest.tests.common;
using TDUModdingToolsTest.tests.common.calculations;
using TDUModdingToolsTest.tests.gfx;
using TDUModdingToolsTest.tests.support.traces;

namespace TDUModdingToolsTest
{
    class Program
    {
        static void Main()
        {
            // Logging configuration
            Log.GlobalAppenders.Add(new ConsoleAppender());
            Log.GlobalAppenders.Add(new FileAppender(Environment.CurrentDirectory + @"\..\..\log\tests.log"));

            Log.Info("-START-");

            // TEXTURES
            Log.Info("Testing texture processing...");

            // Test de chargement de fichier 2DB
            TexturesTests.Read2DBFileTest(Environment.CurrentDirectory + @"\..\..\resources\gfx\cock_lr.2db");

            // Test de la conversion d'en-tête 2DB vers un tableau de bytes
            TexturesTests.Get2DBHeaderAsBytesTest(Environment.CurrentDirectory + @"\..\..\resources\gfx\cock_lr.2db");

            // Test de la conversion d'en-tête DDS vers un tableau de bytes
            TexturesTests.GetDDSHeaderAsBytesTest(Environment.CurrentDirectory + @"\..\..\resources\gfx\cock_lr.dds");


            // COMMON TOOLS
            // Test de normalisation de nom d'objet
            Log.Info("Testing name normalization...");

            string res1 = ToolsTests.NormalizeNameTest("TS_GASCAP.2db");
            string res2 = ToolsTests.NormalizeNameTest("TS_GASCAP_A.2db");
            string res3 = ToolsTests.NormalizeNameTest("STIRRUP_FR");
            string res4 = ToolsTests.NormalizeNameTest("WS_DASHBOARD_COUPE.2db");
            string res5 = ToolsTests.NormalizeNameTest("LAMBORGHINI_TEXT_TS.2db");

            // Test d'affichage correct d'un nom d'objet
            ToolsTests.OutlineExtendedCharactersTest(res1);
            ToolsTests.OutlineExtendedCharactersTest(res2);
            ToolsTests.OutlineExtendedCharactersTest(res3);
            ToolsTests.OutlineExtendedCharactersTest(res4);
            ToolsTests.OutlineExtendedCharactersTest(res5);


            // SUPPORT
            // - Logging: File appenders
            Log.Info("Testing logging and file appender...");

            AppendersTests.TestFileAppender(@"C:\Temp\fileAppender.log");

            // COMMON FRAMEWORK
            Log.Info("Testing common framework > Integer To Bits...");
            BinaryToolsTests.TestIntegerToBits();


            Log.Info("-END-");
        }
    }
}

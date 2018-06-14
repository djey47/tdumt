using System;
using DjeFramework1.Common.Support.Traces;
using DjeFramework1.Common.Support.Traces.Appenders;

namespace TDUModdingToolsTest.tests.support.traces
{
    class AppendersTests
    {
        internal static void TestFileAppender(string p)
        {
            Log testLog = new Log("For testing purposes");
            FileAppender fileAppender = new FileAppender(p);

            testLog.Appenders.Add(fileAppender);
            testLog.WriteEvent("First test");
            testLog.WriteEvent("Second test");
            testLog.WriteEvent(DateTime.MinValue, "Third test");
        }
    }
}

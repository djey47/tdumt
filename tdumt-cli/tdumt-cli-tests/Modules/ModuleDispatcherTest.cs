using NUnit.Framework;
using System;
using Djey.TduModdingTools.CLI.Modules;

namespace Djey.TduModdingTools.CLI.Modules
{
	[TestFixture]
	public class ModuleDispatcherTest
	{
		readonly ModuleDispatcher dispatcher = new ModuleDispatcher();

		[Test]
		public void Dispatch_whenUnknownCommand_shouldThrowException ()
		{
			// GIVEN-WHEN-THEN
			Assert.Throws (typeof(ArgumentOutOfRangeException), new TestDelegate ( () => dispatcher.Dispatch ("FOO", new string[0] )));
		}

		[Test]
		public void Dispatch_whenKnownCommand_shouldNotThrowException ()
		{
			// GIVEN-WHEN
			dispatcher.Dispatch ("FAKE", new string[0]);

			// THEN
			Assert.AreEqual ("{}", dispatcher.ModuleOutput);
		}	
	}
}
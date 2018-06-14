using NUnit.Framework;

namespace Djey.TduModdingTools.CLI
{
	[TestFixture]
	public class ProgramTest
	{
		const int StatusSuccess = 0;
		const int StatusFailure = 1;

		[Test]
		public void Main_withNullArgs_shouldReturnFailureStatus ()
		{
			// GIVEN-WHEN
			int actualStatus = Program.Main (null);

			// THEN
			Assert.AreEqual (StatusFailure, actualStatus);
		}

		[Test]
		public void Main_withoutArgs_shouldReturnFailureStatus ()
		{
			// GIVEN-WHEN
			int actualStatus = Program.Main (new string[0]);

			// THEN
			Assert.AreEqual (StatusFailure, actualStatus);
		}

		[Test]
		public void Main_withArg_andInvalidCommand_shouldReturnFailureStatus ()
		{
			// GIVEN-WHEN
			int actualStatus = Program.Main (new []{"FOO"});

			// THEN
			Assert.AreEqual (StatusFailure, actualStatus);
		}

		[Test]
		public void Main_withArg_andValidCommand_shouldReturnSuccessStatus ()
		{
			// GIVEN-WHEN
			int actualStatus = Program.Main (new []{"FAKE"});

			// THEN
			Assert.AreEqual (StatusSuccess, actualStatus);
		}
	}
}
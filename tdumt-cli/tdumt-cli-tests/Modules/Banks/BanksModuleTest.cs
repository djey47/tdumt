using NUnit.Framework;
using System;
using Djey.TduModdingTools.CLI.Modules.Banks;
using System.Collections.Generic;

namespace Djey.TduModdingTools.CLI.Modules.Banks
{
	[TestFixture ()]
	public class BanksModuleTest
	{
		[Test ()]
		public void GetBankFileNameParam_WhenArgs_ShouldReturnFirstOne ()
		{
			// GIVEN
			List<string> args = new List <string>();
			args.Add("TEST.BNK");
			args.Add ("1");

			// WHEN
			string actualParam = BanksModule.GetBankFileNameParam (args);

			// THEN
			Assert.AreEqual ("TEST.BNK", actualParam);
		}

		[Test ()]
		public void GetPackedFileParam_WhenArgs_ShouldReturnSecondOne ()
		{
			// GIVEN
			List<string> args = new List <string>();
			args.Add("TEST.BNK");
			args.Add ("PackedFile");
			args.Add ("1");

			// WHEN
			string actualParam = BanksModule.GetPackedFileParam (args);

			// THEN
			Assert.AreEqual ("PackedFile", actualParam);
		}

		[Test ()]
		public void GetBatchInputFileParam_WhenArgs_ShouldReturnSecondOne ()
		{
			// GIVEN
			List<string> args = new List <string>();
			args.Add("TEST.BNK");
			args.Add ("BatchInput.json");
			args.Add ("1");

			// WHEN
			string actualParam = BanksModule.GetBatchInputFileParam (args);

			// THEN
			Assert.AreEqual ("BatchInput.json", actualParam);
		}		

		[Test ()]
		public void GetSourceFileNameOrTargetDirectoryParam_WhenArgs_ShouldReturnThirdOne ()
		{
			// GIVEN
			List<string> args = new List <string>();
			args.Add("TEST.BNK");
			args.Add ("PackedFile");
			args.Add ("C:\\file1");

			// WHEN
			string actualParam = BanksModule.GetSourceFileNameOrTargetDirectoryParam (args);

			// THEN
			Assert.AreEqual ("C:\\file1", actualParam);
		}

		[Test ()]
		public void GetSourceFileNameOrTargetDirectoryParam_WhenNoThirdArg_ShouldReturnNull ()
		{
			// GIVEN
			List<string> args = new List <string>();
			args.Add("TEST.BNK");
			args.Add ("PackedFile");

			// WHEN
			string actualParam = BanksModule.GetSourceFileNameOrTargetDirectoryParam (args);

			// THEN
			Assert.IsNull (actualParam);
		}
	}
}

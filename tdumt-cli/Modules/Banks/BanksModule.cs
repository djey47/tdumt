using System;
using System.Collections.Generic;
using System.IO;
using Djey.TduModdingTools.CLI.Modules.Banks.Dto;
using Newtonsoft.Json;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.banks;

namespace Djey.TduModdingTools.CLI.Modules.Banks
{
	/// <summary>
	/// Module handling TDU Banks (bnk) management assemblies.
	/// </summary>
	public class BanksModule
	{
		// TODO Convert to enum
		public const string CommandBankInfo = "BANK-I";
		public const string CommandBankUnpack = "BANK-U";
		public const string CommandBankBatchUnpack = "BANK-UX";
		public const string CommandBankRepack = "BANK-R";
		public const string CommandBankBatchRepack = "BANK-RX";

	    readonly ModuleDispatcher _moduleDispatcher;

		public BanksModule (ModuleDispatcher moduleDispatcher)
		{
			_moduleDispatcher = moduleDispatcher;
		}

	    /// <summary>
	    /// Provides information about Bank file and contents.
	    /// </summary>
	    /// <param name="args"></param>
	    public void Info (string[] args)
	    {
			var bankFileName = GetBankFileNameParam (args);

			var bankFile = _LoadBankFile (bankFileName);

            var outputObject = GetBankInfoOutput(bankFile);

			_moduleDispatcher.ModuleOutput = JsonConvert.SerializeObject (outputObject);
	    }

		/// <summary>
		/// Extracts a packed file onto a real file name.
		/// </summary>
		/// <param name="args">Arguments.</param>
		public void Unpack (string[] args)
		{
			var bankFileName = GetBankFileNameParam (args);
			var packedFilePath = GetPackedFileParam (args);
			var targetDirectory = GetSourceFileNameOrTargetDirectoryParam (args);

			var bankFile = _LoadBankFile (bankFileName);
			bankFile.ExtractPackedFile (packedFilePath, targetDirectory, true); 

			_moduleDispatcher.NoModuleOutput();
		}

		/// <summary>
		/// Extracts all packed files onto real file names.
		/// </summary>
		/// <param name="args">Arguments.</param>
		public void BatchUnpack (string[] args)
		{
			var bankFileName = GetBankFileNameParam (args);
			var batchInputFileName = GetBatchInputFileParam (args);
			var targetDirectory = GetSourceFileNameOrTargetDirectoryParam (args);

			var batchItems = _LoadBatchInputFile (batchInputFileName).Items;

			var bankFile = _LoadBankFile (bankFileName);
			foreach (Djey.TduModdingTools.CLI.BatchInputObject.BatchItem batchItem in batchItems)
			{
				bool unpackToDirectory = (targetDirectory != null);

				if (unpackToDirectory) {
					bankFile.ExtractPackedFile (batchItem.InternalPath, targetDirectory, true); 
				} else {
					bankFile.ExtractPackedFile (batchItem.InternalPath, batchItem.ExternalFile, false); 
				}
			}

			_moduleDispatcher.NoModuleOutput();
		}

		/// <summary>
		/// Replaces a packed file with provided real one.
		/// </summary>
		/// <param name="args">Arguments.</param>
		public void Repack (string[] args)
		{
			var bankFileName = GetBankFileNameParam (args);
			var packedFilePath = GetPackedFileParam (args);
			var sourceFileName = GetSourceFileNameOrTargetDirectoryParam (args);

			var bankFile = _LoadBankFile (bankFileName);
			bankFile.ReplacePackedFile (packedFilePath, sourceFileName);

			// Bank file is automatically saved

			_moduleDispatcher.NoModuleOutput();
		}

		/// <summary>
		/// Replaces all packed files with provided ones.
		/// </summary>
		/// <param name="args">Arguments.</param>
		public void BatchRepack (string[] args)
		{
			var bankFileName = GetBankFileNameParam (args);
			var batchInputFileName = GetBatchInputFileParam (args);

			var batchItems = _LoadBatchInputFile (batchInputFileName).Items;

			var bankFile = _LoadBankFile (bankFileName);
			foreach (Djey.TduModdingTools.CLI.BatchInputObject.BatchItem batchItem in batchItems)
			{
				bankFile.ReplacePackedFile (batchItem.InternalPath, batchItem.ExternalFile);
			}

			_moduleDispatcher.NoModuleOutput();
		}

	    static BankInfoOutputObject GetBankInfoOutput(BNK bankFile)
	    {
	        var outputObject = new BankInfoOutputObject((int) bankFile.Size, (int) bankFile.Year);

	        var packedFilesInformations = new List<PackedFileInfoOutputObject>();
	        foreach (var packedFilePath in bankFile.GetPackedFilesPaths(null))
	        {
				var packedFileShortName = bankFile.GetPackedFileName (packedFilePath);
	            var packedFileSize = bankFile.GetPackedFileSize(packedFilePath);
				var packedFileTypeDescription = bankFile.GetPackedFileTypeDescription (packedFilePath);

				var packedFilesInfo = new PackedFileInfoOutputObject(packedFileShortName, packedFilePath, packedFileSize, packedFileTypeDescription);
	            packedFilesInformations.Add(packedFilesInfo);
	        }

	        outputObject.PackedFiles.AddRange(packedFilesInformations);

	        return outputObject;
	    }

		public static string GetBankFileNameParam (IList<string> args)
		{
			return args [0];
		}

		public static string GetPackedFileParam (IList<string> args)
		{
			return args [1];				
		}

		public static string GetSourceFileNameOrTargetDirectoryParam (IList<string> args)
		{
			if (args.Count != 3) {
				return null;
			}
			return args [2];
		}

		public static string GetBatchInputFileParam (IList<string> args)
		{
			return args [1];				
		}

		private static BNK _LoadBankFile (string bankFileName)
		{
			var bankFile = TduFile.GetFile (bankFileName) as BNK;
			if (bankFile == null) {
				throw new FileLoadException("Unable to load bank file.", bankFileName);
			}
			return bankFile;
		}

		private static BatchInputObject _LoadBatchInputFile (string batchInputFileName)
		{
			string fileContents = File.ReadAllText(batchInputFileName);
			return JsonConvert.DeserializeObject<BatchInputObject>(fileContents);
		}
	}
}

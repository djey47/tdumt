using System;
using Djey.TduModdingTools.CLI.Modules.Banks;
using Djey.TduModdingTools.CLI.Modules.Cameras;

namespace Djey.TduModdingTools.CLI.Modules
{
	/// <summary>
	/// Module dispatcher.
	/// </summary>
	public class ModuleDispatcher
	{
		// For testing use.
		const string CommandFake = "FAKE";

		public ModuleDispatcher() 
		{
			NoModuleOutput();
		}

		/// <summary>
		/// Dispatch the specified command and args to right module.
		/// </summary>
		/// <param name="command">Command.</param>
		/// <param name="args">Arguments.</param>
		public void Dispatch(string command, params string[] args) 
		{
			var banksModule = new BanksModule (this);
			var camerasModule = new CamerasModule (this);

			switch (command) {
			case BanksModule.CommandBankInfo: 
				banksModule.Info (args);
				break;
			case BanksModule.CommandBankUnpack: 
				banksModule.Unpack (args);
				break;
			case BanksModule.CommandBankRepack: 
				banksModule.Repack (args);
				break;
			case BanksModule.CommandBankBatchUnpack: 
				banksModule.BatchUnpack (args);
				break;
			case BanksModule.CommandBankBatchRepack: 
				banksModule.BatchRepack (args);
				break;
			case CamerasModule.CommandCameraList:
				camerasModule.List (args);
				break;
			case CamerasModule.CommandCameraReset:
				camerasModule.Reset (args);
				break;
			case CamerasModule.CommandCameraCustomize:
				camerasModule.Customize (args);
				break;
			case CommandFake:
				break;
			default:
				throw new ArgumentOutOfRangeException ("command", command, "Specified command name is invalid.");
			}					
		}

		public void NoModuleOutput ()
		{
			ModuleOutput = "{}";
		}

		/// <summary>
		/// Gets or sets the module output.
		/// </summary>
		/// <value>The module output.</value>
		public string ModuleOutput { get;set; }
	}
}
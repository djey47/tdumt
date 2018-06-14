using System;
using Djey.TduModdingTools.CLI.Modules;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("tdumt-cli-tests")]
namespace Djey.TduModdingTools.CLI
{
    /// <summary>
    /// TDUMT-CLI startup class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Unique entry point for command line application.
        /// </summary>
        /// <param name="args"></param>
        internal static int Main(string[] args)
        {
			try {
				ValidateCommandLineArguments (args);
			} catch (ArgumentException ae) {
				Console.Error.WriteLine (ae);
				return 1;
			}
				
			var dispatcher = new ModuleDispatcher ();
			try {
				dispatcher.Dispatch (args [0], ArgsToCommandArgs(args));
			} catch (Exception e) {
				Console.Error.WriteLine (e);
				return 1;
			}

			Console.WriteLine(dispatcher.ModuleOutput);
			return 0;
        }

		static void ValidateCommandLineArguments(params string[] args)
		{
			if (args == null || args.Length == 0) {
				throw new ArgumentException ("Command argument not found", "args");
			}
		}

		static string[] ArgsToCommandArgs(Array args)
		{
			var commandArgs = new string[args.Length - 1];
			Array.Copy (args, 1, commandArgs, 0, commandArgs.Length);
			return commandArgs;
		}
    }
}
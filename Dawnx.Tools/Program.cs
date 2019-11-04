using NStandard;
using Dawnx.Data;
using Dawnx.Net.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Dawnx.Tools
{
    public class Program
    {
        public static readonly string CLI_VERSION = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static readonly string DOWNLOAD_DIRECTORY = $"{Path.GetTempPath()}DawnxCliCaches";
        public static TargetProjectInfo TargetProjectInfo { get; set; }
        public const string SUPPORT_URL = "http://dawnx.net/CliService";

        private static Dictionary<string, ICommand> Commands = new Dictionary<string, ICommand>();
        private static List<CommandAttribute> CommandAttributes = new List<CommandAttribute>();

        static void Main(string[] args)
        {
            TargetProjectInfo = ProjectUtility.GetTargetProjectInfo();

            var cargs = new ConsoleArgs(args, "-");

            CheckDownloadDirectory();
            CacheCommands();

            Http.RegisterSystemLogin(true);
            Http.RegisterProxy(true);

            PrintWelcome();

            if (!cargs.Contents.Any())
            {
                PrintUsage();
                return;
            }

            Console.CursorVisible = false;
            try
            {
                PrintTargetProjectInfo();

                if (Commands.ContainsKey(cargs[0]))
                {
                    Commands[cargs[0].ToLower()].Run(cargs);
                }
                else Console.WriteLine($"Unkown command: {cargs[0]}");
            }
            finally
            {
                Console.CursorVisible = true;
                Console.WriteLine("Completed.");
            }
        }

        private static void CacheCommands()
        {
            var types = Assembly.GetExecutingAssembly().GetTypesWhichMarkedAs<CommandAttribute>();
            foreach (var type in types)
            {
                var command = Activator.CreateInstance(type) as ICommand;
                var attr = type.GetCustomAttribute<CommandAttribute>();
                CommandAttributes.Add(attr);

                if (!attr.Name.IsNullOrWhiteSpace())
                    Commands[attr.Name.Trim().ToLower()] = command;
                if (!attr.ShortName.IsNullOrWhiteSpace())
                    Commands[attr.ShortName.Trim().ToLower()] = command;
            }
        }

        public static void PrintTargetProjectInfo()
        {
            Console.WriteLine($@"
* {nameof(TargetProjectInfo.ProjectName)}:        {TargetProjectInfo.ProjectName}
* {nameof(TargetProjectInfo.AssemblyName)}:       {TargetProjectInfo.AssemblyName}
* {nameof(TargetProjectInfo.RootNamespace)}:      {TargetProjectInfo.RootNamespace}
* {nameof(TargetProjectInfo.TargetFramework)}:    {TargetProjectInfo.TargetFramework}");
            Console.WriteLine();
        }

        public static void PrintWelcome()
        {
            Console.WriteLine($@"
{"ヽ(*^▽^)ノ".Center(60)}

Dawnx .NET Command-line Tools {CLI_VERSION}

<Hint>: All files will be downloaded to {DOWNLOAD_DIRECTORY}");
        }

        public static void PrintUsage()
        {
            Console.WriteLine($@"
Usage: dotnet nx [command]

Commands:
  {"Name".PadRight(20)}{"\t"}{"ShortName".PadRight(10)}{"\t"}{"Description"}");

            foreach (var attr in CommandAttributes)
                Console.WriteLine($"  {attr.Name.PadRight(20)}\t{attr.ShortName.PadRight(10)}\t{attr.Description}");
            Console.WriteLine();
        }

        public static void CheckDownloadDirectory()
        {
            if (!Directory.Exists(DOWNLOAD_DIRECTORY))
                Directory.CreateDirectory(DOWNLOAD_DIRECTORY);
        }

    }
}

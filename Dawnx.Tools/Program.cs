using Dawnx.Data;
using Dawnx.Net.Web;
using Dawnx.Security.AesSecurity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Dawnx.Tools
{
    public class Program
    {
        public static readonly string CLI_VERSION = Assembly.GetEntryAssembly().GetName().Version.ToString();
        public static readonly string DOWNLOAD_DIRECTORY = $"{Path.GetTempPath()}DawnxCliCaches";
        public static TargetProjectInfo TargetProjectInfo { get; set; }
        public const string SUPPORT_URL = "http://dawnx.net/CliService";

        private static Dictionary<string, ICommand> Commands = new Dictionary<string, ICommand>();

        static void Main(string[] args)
        {
            var cargs = new ConsoleArgs(args, "-");

            TargetProjectInfo = ProjectUtility.GetTargetProjectInfo();
            CheckDownloadDirectory();
            CacheCommands();

            Http.RegisterSystemLogin(true);
            Http.RegisterProxy(true);

            if (!cargs.Contents.Any())
            {
                // Help Content
                return;
            }

            Console.CursorVisible = false;
            try
            {
                Console.WriteLine(
                    $"Welcome to use Dawnx Cli Tools {CLI_VERSION}{Environment.NewLine}" +
                    $"======================================================================{Environment.NewLine}" +
                    $"Hint: All files will be downloaded to {DOWNLOAD_DIRECTORY}{Environment.NewLine}");

                PrintTargetProjectInfo();

                if (Commands.ContainsKey(cargs[0]))
                {
                    Commands[cargs[0]].Run(cargs);
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

                if (!attr.Name.IsNullOrWhiteSpace())
                    Commands[attr.Name.Trim()] = command;
                if (!attr.ShortName.IsNullOrWhiteSpace())
                    Commands[attr.ShortName.Trim()] = command;
            }
        }

        private static void PrintTargetProjectInfo()
        {
            Console.WriteLine(
                $"{nameof(TargetProjectInfo.ProjectName)}:        {TargetProjectInfo.ProjectName}{Environment.NewLine}" +
                $"{nameof(TargetProjectInfo.AssemblyName)}:       {TargetProjectInfo.AssemblyName}{Environment.NewLine}" +
                $"{nameof(TargetProjectInfo.RootNamespace)}:      {TargetProjectInfo.RootNamespace}{Environment.NewLine}" +
                $"{nameof(TargetProjectInfo.TargetFramework)}:    {TargetProjectInfo.TargetFramework}{Environment.NewLine}");
        }

        private static void CheckDownloadDirectory()
        {
            if (!Directory.Exists(DOWNLOAD_DIRECTORY))
                Directory.CreateDirectory(DOWNLOAD_DIRECTORY);
        }

    }
}

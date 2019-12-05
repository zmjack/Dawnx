using Dawnx.Net.Web;
using DotNetCli;
using NEcho;
using NStandard;
using System;
using System.IO;
using System.Reflection;

namespace Dawnx.Tools
{
    public class Program
    {
        public static readonly string CLI_VERSION = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static readonly string DOWNLOAD_DIRECTORY = $"{Path.GetTempPath()}DawnxCliCaches";
        public const string SUPPORT_URL = "http://dawnx.net/CliService";

        public static CommandContainer CommandContainer;
        public static ProjectInfo ProjectInfo => CommandContainer.ProjectInfo;

        static void Main(string[] args)
        {
            Http.RegisterSystemLogin(true);
            Http.RegisterProxy(true);

            CommandContainer = new CommandContainer(ProjectInfo.GetCurrent(), "nx");
            CommandContainer.CacheCommands(Assembly.GetExecutingAssembly());

            PrintWelcome();
            CheckDownloadDirectory();

            var conArgs = new ConArgs(args, "-");
            CommandContainer.PrintProjectInfo();
            CommandContainer.Run(conArgs);
        }

        public static void PrintWelcome()
        {
            Console.WriteLine($@"
{"ヽ(*^▽^)ノ".Center(60)}

Dawnx .NET Command-line Tools {CLI_VERSION}

<Hint>: All files will be downloaded to {DOWNLOAD_DIRECTORY}");
        }

        public static void CheckDownloadDirectory()
        {
            if (!Directory.Exists(DOWNLOAD_DIRECTORY))
                Directory.CreateDirectory(DOWNLOAD_DIRECTORY);
        }

    }
}

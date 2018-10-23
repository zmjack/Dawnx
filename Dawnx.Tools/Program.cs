using Dawnx.Analysises;
using Dawnx.Net.Http;
using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Dawnx.Tools
{
    public class Program
    {
        public static readonly string CLI_VERSION = Assembly.GetEntryAssembly().GetName().Version.ToString();
        public static readonly string DOWNLOAD_DIRECTORY = $"{Path.GetTempPath()}DawnxCliCaches";

        //public static readonly string SUPPORT_URL = "http://localhost:57028/CliService";
        public const string SUPPORT_URL = "http://dawnx.net/CliService";

        static void Main(string[] args)
        {
            if (!Directory.Exists(DOWNLOAD_DIRECTORY))
                Directory.CreateDirectory(DOWNLOAD_DIRECTORY);

            Web.RegisterSystemLogin(true);

            Console.CursorVisible = false;
            try
            {
                Console.WriteLine($"{Environment.NewLine}" +
                    $"Welcome Dawnx Command-line Tools {CLI_VERSION}{Environment.NewLine}" +
                    $"======================================================================{Environment.NewLine}" +
                    $"Hint: All files will be downloaded to {DOWNLOAD_DIRECTORY}{Environment.NewLine}");

#if !DEBUG
                ProjectUtility.Sync();
                ProjectUtility.PrintInfo();
#endif

#if DEBUG
                InitArgs(new[] { "", "install", "general.ts" });
                //InitArgs(new[] { "", "install", "general.ts" });
#else
                InitArgs(args);
#endif
            }
            finally
            {
                Console.CursorVisible = true;
            }
        }

        private static void InitArgs(string[] args)
        {
            var cargs = new ConsoleArgs(args, "-");

            Match match;
            switch (cargs.SimpleLine.ToLower())
            {
                case string line
                when (match = new Regex(@" *install +([^ ]+)").Match(line)).Success:
                    Commands.Install(match.Groups[1].Value);
                    break;

                case string line
                when (match = new Regex(@" *gcs +([^ ]+)").Match(line)).Success:
                    Commands.Gcs(match.Groups[1].Value);
                    break;

                default:
                    Console.WriteLine("Unkown command.");
                    break;
            }
        }

        //public static void AddView(
        //    string view, string model, string controller, string area,
        //    string template, string website, string user)
        //{
        //    if (!area.IsNullOrWhiteSpace())
        //    {
        //        var filePath = $"{_Directory}\\Areas\\{area}\\Views\\{controller}\\{view}.cshtml";
        //        if (File.Exists(filePath))
        //        {
        //            using (var file = new FileStream(filePath, FileMode.Create))
        //            using (var fileStream = new StreamWriter(file))
        //            {

        //            }
        //        }
        //    }

        //    File.Exists($"{_Directory}\\Views\\{controller}");
        //    Console.WriteLine();
        //}

    }
}

using Dawnx.Analysises;
using Dawnx.Net.Web;
using System;
using System.IO;
using System.Reflection;

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

            Http.RegisterSystemLogin(true);
            Http.RegisterProxy(true);

            Console.CursorVisible = false;
            try
            {
                Con.Print(
                    $"Welcome Dawnx Command-line Tools {CLI_VERSION}{Environment.NewLine}" +
                    $"======================================================================{Environment.NewLine}" +
                    $"Hint: All files will be downloaded to {DOWNLOAD_DIRECTORY}{Environment.NewLine}")
                    .Line();

#if !DEBUG
                ProjectUtility.PrintInfo();
#endif

#if DEBUG
                Run(new[] { "tsgen" });
#else
                Run(args);
#endif
            }
            finally
            {
                Console.CursorVisible = true;
            }
        }

        private static void Run(string[] args)
        {
            var cargs = new ConsoleArgs(args, "-");

            switch (cargs.Contents[0])
            {
                case "install":
                    Commands.Install(cargs.Contents[1]);
                    break;

                case "gcs":
                    Commands.Gcs(cargs.Contents[1]);
                    break;

                case "tsgen":
                    if (cargs.Contents.Length < 2)
                        Commands.TsGen("TsGens");
                    else Commands.TsGen(cargs.Contents[1]);
                    break;

                default:
                    Con.Print("Unkown command.").Line();
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
        //    Con.Line();
        //}

    }
}

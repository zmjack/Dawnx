using DotNetCli;
using Ink;
using NStandard;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dawnx.Tools
{
    [Command("CCHeader", "cch", Description = "Convert declarations of C++ header file to DotNet P/Invoke declaration.")]
    public class ConvertCppHeaderCommand : ICommand
    {
        public void PrintUsage()
        {
            Console.WriteLine($@"
Usage: dotnet nx (cch|ccheader) [CppHeaderFile] [Options]

CppHeaderFile:
  {"<value>".PadRight(20)}{"\t"}The path of C++ header file.
Options:
  {"-l|--lang".PadRight(20)}{"\t"}Language to generate into; vb or cs(default)
  {"-o|--out".PadRight(20)}{"\t"}Output file name (default PI_<Header>.cs)
");
        }

        public void Run(string[] args)
        {
            var conArgs = new ConArgs(args, "-");
            if (conArgs.Properties.For(x => x.ContainsKey("-h") || x.ContainsKey("--help")) || conArgs.Contents.Length < 2)
            {
                PrintUsage();
                return;
            }

            var headerFile = conArgs[1];
            var headerName = Path.GetFileNameWithoutExtension(headerFile);
            var language = conArgs["-l"].Concat(conArgs["--lang"]).FirstOrDefault() ?? "cs";
            var outFile = conArgs["-o"].Concat(conArgs["--out"]).FirstOrDefault() ?? $"PI_{headerName}.{language}";

            var _headerFile = $"_{headerFile}";
            var content = File.ReadAllText(headerFile)
                .RegexReplace(new Regex(@"#include (?:(""|<)).+(?:(""|>))"), "")
                .RegexReplace(new Regex(@"using namespace .+;"), "");
            File.WriteAllText(_headerFile, content);

            var sigimp = Process.Start(new ProcessStartInfo
            {
                Arguments = $"{_headerFile} /lang:{language} /out:{outFile}",
                FileName = $"{Program.ProjectInfo.CliPackagePath}/lib/netstandard2.0/sigimp.exe",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
            });
            sigimp.OutputDataReceived += Sigimp_OutputDataReceived;
            sigimp.BeginOutputReadLine();
            sigimp.WaitForExit();

            var finalContent = File.ReadAllText(outFile)
                .Replace(@"[System.Runtime.InteropServices.DllImportAttribute(""<Unknown>""", $@"[DllImport(""{headerName}.dll"")")
                .RegexReplace(new Regex(@"System\.Runtime\.InteropServices\.(\w+)Attribute"), "$1")
                .Replace("System.Runtime.InteropServices.", "")
                .Replace("public static extern  ", "    public static extern ");
            File.WriteAllText(outFile, finalContent);
            File.Delete(_headerFile);
        }

        private void Sigimp_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
    }

}

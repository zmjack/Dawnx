using DotNetCli;
using Ink;
using NStandard;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace Dawnx.Tools.Test
{
    public class UnitTest1
    {
        [Fact]
        public void TestEntry()
        {
            Program.CommandContainer = new CommandContainer("nx", new ProjectInfo
            {
                ProjectRoot = $"{Directory.GetCurrentDirectory()}/../../../../../~Experiment/DawnxDemo",
                ProjectName = "DawnxDemo.csproj",
                AssemblyName = "DawnxDemo",
                RootNamespace = "DawnxDemo",
                TargetFramework = "netcoreapp2.2",
                CliPackagePath = $"{Directory.GetCurrentDirectory()}/../../../../../Dawnx.Tools",
            });

            ConvertCppHeaderTest();
            //AesTest();
            //CompressTest();
        }

        private void ConvertCppHeaderTest()
        {
            var args = new[] { "cch", "CppDll.h" };

            using (var memory = new MemoryStream())
            using (var writer = new StreamWriter(memory))
            {
                Console.SetOut(writer);
                new ConvertCppHeaderCommand().Run(args);

                var output = GetText(writer);
                var expected = @"
public partial class NativeMethods {
    
    /// Return Type: LPWSTR->WCHAR*
    [DllImport(""CppDll.dll""), EntryPoint=""CreateString"", CallingConvention=CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.LPWStr)]
    public static extern string CreateString() ;

}
";
                Assert.Equal(expected, File.ReadAllText("PI_CppDll.cs"));
            }
        }

        private void AesTest()
        {
            var args = new[] { "aes", "hex" };

            using (var memory = new MemoryStream())
            using (var writer = new StreamWriter(memory))
            {
                Console.SetOut(writer);
                new AesCommand().Run(args);

                var output = GetText(writer);
                Assert.True(output.IsMatch(new Regex(@"New HexString:\t[0-9a-f]{64}")));
            }
        }

        private void CompressTest()
        {
            var args = new[] { "compress" };

            using (var memory = new MemoryStream())
            using (var writer = new StreamWriter(memory))
            {
                Console.SetOut(writer);
                new CompressCommand().Run(args);

                var output = GetText(writer);
                Assert.Equal(401, new FileInfo(Directory.GetCurrentDirectory() + "/compress.zip").Length);
            }
        }

        private string GetText(StreamWriter writer)
        {
            var stream = writer.BaseStream as MemoryStream;
            string ret;

            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            ret = stream.ToArray().String();
            stream.Seek(0, SeekOrigin.End);

            return ret;
        }

    }
}

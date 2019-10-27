using Dawnx.Data;
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
            Program.TargetProjectInfo = new TargetProjectInfo
            {
                ProjectRoot = $"{Directory.GetCurrentDirectory()}/../../../../../~Experiment/DawnxDemo",
                ProjectName = "DawnxDemo.csproj",
                AssemblyName = "DawnxDemo",
                RootNamespace = "DawnxDemo",
                TargetFramework = "netcoreapp2.2",
            };

            //AesTest();
            //CompressTest();
            TypeScriptGeneratorTest();
        }

        private void AesTest()
        {
            var args = new[] { "aes", "hex" };
            var cargs = new ConsoleArgs(args, "-");

            using (var memory = new MemoryStream())
            using (var writer = new StreamWriter(memory))
            {
                Console.SetOut(writer);
                new AesCommand().Run(cargs);

                var output = GetText(writer);
                Assert.True(output.IsMatch(new Regex(@"New HexString:\t[0-9a-f]{64}")));
            }
        }

        private void CompressTest()
        {
            var args = new[] { "compress" };
            var cargs = new ConsoleArgs(args, "-");

            using (var memory = new MemoryStream())
            using (var writer = new StreamWriter(memory))
            {
                Console.SetOut(writer);
                new CompressCommand().Run(cargs);

                var output = GetText(writer);
                Assert.Equal(401, new FileInfo(Directory.GetCurrentDirectory() + "/compress.zip").Length);
            }
        }

        private void TypeScriptGeneratorTest()
        {
            var args = new[] { "tsg", "-o", "Vuets/Typings/@project", "-i", "jsend" };
            var cargs = new ConsoleArgs(args, "-");

            using (var memory = new MemoryStream())
            using (var writer = new StreamWriter(memory))
            {
                Console.SetOut(writer);
                new TypeScriptGeneratorCommand().Run(cargs);

                var output = GetText(writer);
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

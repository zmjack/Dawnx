using Dawnx.Net.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TypeLite;

namespace Dawnx.Tools
{
    public static partial class Commands
    {
        public static void Gts(string outDir)
        {
            var path = $@"bin\Debug\{ProjectUtility.TargetFramework}\{ProjectUtility.ProjectName}.dll";
            var assembly = Assembly.LoadFile(path);
            var name = Assembly.GetExecutingAssembly().GetName().Name;

            var tsFluent = TypeScript.Definitions().For(assembly);

            var files = new[]
            {
                new
                {
                    FileName = $"{Path.GetPathRoot(outDir)}/{name}.d.ts",
                    Content = tsFluent.Generate(TsGeneratorOutput.Enums | TsGeneratorOutput.Properties | TsGeneratorOutput.Fields),
                },
                new
                {
                    FileName = $"{Path.GetPathRoot(outDir)}/{name}.d.ts",
                    Content = tsFluent.Generate(TsGeneratorOutput.Constants),
                },
            };
            foreach (var file in files)
            {
                File.WriteAllText(file.FileName, file.Content);
                Con.PrintLine($"  File Saved: {file.FileName}");
            }
        }

    }
}

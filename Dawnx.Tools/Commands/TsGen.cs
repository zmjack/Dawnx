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
        public static void TsGen(string outDir)
        {
#if DEBUG
            var path = Path.GetFullPath(
                $"../../../../~Experiment/DawnxDevelopingWeb/bin/Debug/" +
                $"{ProjectUtility.TargetFramework}/{ProjectUtility.ProjectName}.dll");
#else
            var path = Path.GetFullPath($"bin/Debug/{ProjectUtility.TargetFramework}/{ProjectUtility.ProjectName}.dll");
#endif
            var assembly = Assembly.LoadFile(path);
            var tsFluent = TypeScript.Definitions();
            foreach (var type in assembly.GetTypesWhichMarks<TsClassAttribute>())
                tsFluent.For(type);

            var files = new[]
            {
                new
                {
                    FileName = $"{Path.GetPathRoot(outDir)}/{ProjectUtility.ProjectName}.d.ts",
                    Content = tsFluent.Generate(TsGeneratorOutput.Enums | TsGeneratorOutput.Properties | TsGeneratorOutput.Fields),
                },
                new
                {
                    FileName = $"{Path.GetPathRoot(outDir)}/{ProjectUtility.ProjectName}.d.ts",
                    Content = tsFluent.Generate(TsGeneratorOutput.Constants),
                },
            };
            foreach (var file in files)
            {
                File.WriteAllText(file.FileName, file.Content);
                Con.Print($"  File Saved: {file.FileName}").Line();
            }
        }

    }
}

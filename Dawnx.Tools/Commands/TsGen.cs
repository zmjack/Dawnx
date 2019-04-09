using Dawnx.Annotation;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using TypeLitePlus;

namespace Dawnx.Tools
{
    public static partial class Commands
    {
        private static string[] SearchDirs = new[]
        {
#if DEBUG
            Path.GetFullPath($"../../../../~Experiment/DawnxDevelopingWeb/bin/Debug/{ProjectUtility.TargetFramework}"),
#else
            Path.GetFullPath($"bin/Debug/{ProjectUtility.TargetFramework}"),
#endif
            $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}/dotnet/sdk/NuGetFallbackFolder",
            $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.nuget/packages",
        };

        public static void TsGen(string outFolder)
        {
            if (!Directory.Exists(outFolder))
                Directory.CreateDirectory(outFolder);

            var dllPath = Path.GetFullPath($"{SearchDirs[0]}/{ProjectUtility.AssemblyName}.dll");

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            var assembly = Assembly.LoadFrom(dllPath);
            var tsFluent = TypeScript.Definitions().WithConvertor<Guid>(x => "string");
            foreach (var type in assembly.GetTypesWhichMarkedAs<TsGenAttribute>())
                tsFluent.For(type);

            var files = new[]
            {
                new
                {
                    FileName = $"{Path.GetFullPath($"{outFolder}/JSend.d.ts")}",
                    Content = TypeScript.Definitions().For(typeof(JSend))
                        .WithMemberFormatter(x => $"{x.Name}?")
                        .Generate(TsGeneratorOutput.Enums | TsGeneratorOutput.Properties | TsGeneratorOutput.Fields),
                },
                new
                {
                    FileName = $"{Path.GetFullPath($"{outFolder}/{ProjectUtility.AssemblyName}.d.ts")}",
                    Content = tsFluent.Generate(TsGeneratorOutput.Enums | TsGeneratorOutput.Properties | TsGeneratorOutput.Fields),
                },
                new
                {
                    FileName = $"{Path.GetFullPath($"{outFolder}/{ProjectUtility.AssemblyName}.const.ts")}",
                    Content = tsFluent.Generate(TsGeneratorOutput.Constants),
                },
            };

            foreach (var file in files)
            {
                File.WriteAllText(file.FileName, file.Content);
                Con.Print($"  File Saved: {file.FileName}").Line();
            }

            var typingContent = files
                .Select(x => $"/// <reference path=\"{Path.GetFileName(x.FileName)}\" />{Environment.NewLine}")
                .Join("");
            File.WriteAllText($"{Path.GetFullPath($"{outFolder}/typing.d.ts")}", typingContent);
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var regex = new Regex("([^,]+), Version=([^,]+), Culture=[^,]+, PublicKeyToken=.+");
            var match = regex.Match(args.Name);

            var assemblyName = match.Groups[1].Value;
            var version = match.Groups[2].Value.For(ver =>
            {
                if (ver.EndsWith(".0"))
                    return ver.Substring(0, ver.Length - 2);
                else return ver;
            });

            foreach (var vi in SearchDirs.AsVI())
            {
                var dir = vi.Value;
                string file;

                if (vi.Index == 0)
                    file = $"{assemblyName}.dll";
                else file = $"{dir}/{assemblyName}/{version}/lib/netstandard2.0/{assemblyName}.dll";

                if (File.Exists(file))
                    return Assembly.LoadFrom(file);
            }

            return null;
        }
    }
}

using Dawnx.Data;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using TypeSharp;

namespace Dawnx.Tools
{
    [Command("TypeScriptGenerator", "tsg")]
    public class TypeScriptGenerator : ICommand
    {
        private static string[] SearchDirs = new[]
        {
#if DEBUG
            Path.GetFullPath($"../../../../~Experiment/DawnxDemo/bin/Debug/{ProjectUtility.TargetFramework}"),
#else
            Path.GetFullPath($"bin/Debug/{ProjectUtility.TargetFramework}"),
#endif
            $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}/dotnet/sdk/NuGetFallbackFolder",
            $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.nuget/packages",
        };

        public void Help()
        {
            throw new NotImplementedException();
        }

        public void Run(ConsoleArgs cargs)
        {
            var outFolder = cargs["--out"] ?? cargs["-o"] ?? "Typings";
            var includes = cargs["--include"]?.Split(",") ?? cargs["-i"]?.Split(",") ?? new string[0];

            GenerateTypeScript(outFolder, includes);
        }


        private static void GenerateTypeScript(string outFolder, string[] includes)
        {
            includes = includes?.Select(x => x.ToLower()).ToArray() ?? new string[0];

            if (!Directory.Exists(outFolder))
                Directory.CreateDirectory(outFolder);

            var dllPath = Path.GetFullPath($"{SearchDirs[0]}/{ProjectUtility.AssemblyName}.dll");

            var assembly = Assembly.LoadFrom(dllPath);
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            #region Assembly Types
            {
                var builder = new TypeScriptModelBuilder();
                var modelTypes = assembly.GetTypesWhichMarkedAs<TypeScriptModelAttribute>();
                builder.WriteTo($"{Path.GetFullPath($"{outFolder}/{ProjectUtility.AssemblyName}.d.ts")}");
            }
            #endregion

            #region JSend Types
            if (includes.Contains("jsend"))
            {
                var builder = new TypeScriptModelBuilder();
                builder.CacheType<JSend>();
                builder.WriteTo($"{Path.GetFullPath($"{outFolder}/JSend.d.ts")}");
            };
            #endregion
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
                    file = $"{dir}/{assemblyName}.dll";
                else file = $"{dir}/{assemblyName}/{version}/lib/netstandard2.0/{assemblyName}.dll";

                if (File.Exists(file))
                    return Assembly.LoadFile(file);
            }

            return null;
        }

    }

}

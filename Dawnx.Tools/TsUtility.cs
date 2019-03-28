using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using TypeLitePlus;

namespace Dawnx.Tools
{
    public static class TsUtility
    {
        public static void GenerateTsFile(string path) => GenerateTsFile(Assembly.LoadFile(path));
        public static void GenerateTsFile(Assembly assembly)
        {
            var tsFluent = TypeScript.Definitions().For(assembly);
            var dts = tsFluent.Generate(TsGeneratorOutput.Enums | TsGeneratorOutput.Properties | TsGeneratorOutput.Fields);
            var cdts = tsFluent.Generate(TsGeneratorOutput.Constants);

            var name = Assembly.GetExecutingAssembly().GetName().Name;
            File.WriteAllText($"{name}.d.ts", dts);
            File.WriteAllText($"{name}.const.d.ts", cdts);

        }

    }
}

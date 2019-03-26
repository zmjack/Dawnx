using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using TypeLite;
using TypeLite.Net4;

namespace Dawnx.Tools
{
    public static class TsUtility
    {
        public static void GenerateTsFile(Assembly assembly)
        {
            var tsFluent = TypeScript.Definitions().ForReferencedAssembly(assembly.FullName);
            var name = Assembly.GetExecutingAssembly().GetName().Name;
            File.WriteAllText($"{name}.d.ts", tsFluent.Generate(
                TsGeneratorOutput.Enums | TsGeneratorOutput.Properties | TsGeneratorOutput.Fields));
            File.WriteAllText($"{name}.const.d.ts", tsFluent.Generate(TsGeneratorOutput.Constants));
        }

    }
}

using System;
using System.IO;
using System.Xml;

namespace Dawnx.Tools
{
    public static class ProjectUtility
    {
        public static string ProjectName { get; private set; }
        public static string RootNamespace { get; private set; }
        public static string TargetFramework { get; private set; }

        static ProjectUtility()
        {
            var dir = Directory.GetCurrentDirectory();
            var projectFile = Directory.GetFiles(dir, "*.csproj").For(_ =>
            {
                if (_.Length == 1) return _[0];
                else throw new FileLoadException("More than one .csproj files are exist in the current directory.");
            });

            var xml = new XmlDocument().Self(_ => _.Load(projectFile));
            ProjectName = Path.GetFileName(projectFile);
            RootNamespace = GetRootNamespace(xml);
            TargetFramework = GetTargetFramework(xml);
        }

        public static void PrintInfo()
        {
            Con.PrintLine($"{nameof(ProjectName)}:\t{ProjectName}");
            Con.PrintLine($"{nameof(RootNamespace)}:\t{RootNamespace}");
            Con.PrintLine($"{nameof(TargetFramework)}:\t{TargetFramework}");
        }

        private static string GetRootNamespace(XmlDocument doc)
            => doc.SelectNodes("/Project/PropertyGroup/RootNamespace").InnerText() ?? Path.GetFileNameWithoutExtension(ProjectName);
        private static string GetTargetFramework(XmlDocument doc)
            => doc.SelectNodes("/Project/PropertyGroup/TargetFramework").InnerText() ?? "Unknown";

        private static string InnerText(this XmlNodeList @this)
        {
            return @this.For(_ =>
            {
                if (_.Count > 0) return _[0].InnerText;
                else return null;
            });
        }

    }
}

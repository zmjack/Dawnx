using System;
using System.IO;
using System.Xml;

namespace Dawnx.Tools
{
    public static class ProjectUtility
    {
        public static string ProjectName { get; private set; }
        public static string AssemblyName { get; private set; }
        public static string RootNamespace { get; private set; }
        public static string TargetFramework { get; private set; }

        static ProjectUtility()
        {
#if DEBUG
            ProjectName = "DawnxDemo.csproj";
            AssemblyName = "DawnxDemo";
            RootNamespace = "DawnxDemo";
            TargetFramework = "netcoreapp2.2";
#else
            var dir = Directory.GetCurrentDirectory();
            var projectFile = Directory.GetFiles(dir, "*.csproj").For(_ =>
            {
                if (_.Length == 1) return _[0];
                else throw new FileLoadException("More than one .csproj files are exist in the current directory.");
            });

            var xml = new XmlDocument().Self(_ => _.Load(projectFile));
            ProjectName = Path.GetFileName(projectFile);
            AssemblyName = GetAssemblyName(xml);
            RootNamespace = GetRootNamespace(xml);
            TargetFramework = GetTargetFramework(xml);
#endif
        }

        public static void PrintInfo()
        {
            Con.Print(
                $"{nameof(ProjectName)}:\t{ProjectName}{Environment.NewLine}" +
                $"{nameof(AssemblyName)}:\t{AssemblyName}{Environment.NewLine}" +
                $"{nameof(RootNamespace)}:\t{RootNamespace}{Environment.NewLine}" +
                $"{nameof(TargetFramework)}:\t{TargetFramework}{Environment.NewLine}").Line(2);
        }

        private static string GetAssemblyName(XmlDocument doc)
        {
            return doc.SelectNodes("/Project/PropertyGroup/AssemblyName").InnerText()
                ?? Path.GetFileNameWithoutExtension(ProjectName);
        }

        private static string GetRootNamespace(XmlDocument doc)
        {
            return doc.SelectNodes("/Project/PropertyGroup/RootNamespace").InnerText()
                ?? Path.GetFileNameWithoutExtension(ProjectName);
        }

        private static string GetTargetFramework(XmlDocument doc)
        {
            return doc.SelectNodes("/Project/PropertyGroup/TargetFramework").InnerText() ?? "Unknown";
        }

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

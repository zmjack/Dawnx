using System;
using System.IO;
using System.Xml;

namespace Dawnx.Tools
{
    public static class ProjectUtility
    {
        public static TargetProjectInfo GetTargetProjectInfo()
        {
            var dir = Directory.GetCurrentDirectory();
            var projectFile = Directory.GetFiles(dir, "*.csproj").For(files =>
            {
                if (files.Length == 0)
                    throw new FileLoadException("The .csproj file is not found in the current directory.");
                else if (files.Length > 1)
                    throw new FileLoadException("More than one .csproj files are exist in the current directory.");
                else return files[0];
            });
            var projectName = Path.GetFileName(projectFile);

            var xml = new XmlDocument().Then(x => x.Load(projectFile));
            return new TargetProjectInfo
            {
                ProjectRoot = dir,
                ProjectName = projectName,
                AssemblyName = xml.SelectNodes("/Project/PropertyGroup/AssemblyName").InnerText() ?? Path.GetFileNameWithoutExtension(projectName),
                RootNamespace = xml.SelectNodes("/Project/PropertyGroup/RootNamespace").InnerText() ?? Path.GetFileNameWithoutExtension(projectName),
                TargetFramework = xml.SelectNodes("/Project/PropertyGroup/TargetFramework").InnerText() ?? "Unknown",
            };
        }

        private static string InnerText(this XmlNodeList @this)
        {
            return @this.For(nodeList =>
            {
                if (nodeList.Count > 0) return nodeList[0].InnerText;
                else return null;
            });
        }

    }
}

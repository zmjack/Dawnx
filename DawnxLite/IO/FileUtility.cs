using System.Collections.Generic;
using System.IO;

namespace Dawnx.IO
{
    public static class FileUtility
    {
        public static string[] GetAllFiles(string path)
        {
            var list = new List<string>();
            AddFilesToList(list, path);
            return list.ToArray();
        }

        private static void AddFilesToList(List<string> list, string path)
        {
            var root = new DirectoryInfo(path);
            root.GetDirectories().Each(dir => AddFilesToList(list, dir.FullName));
            root.GetFiles().Each(file => list.Add(file.FullName));
        }

    }
}

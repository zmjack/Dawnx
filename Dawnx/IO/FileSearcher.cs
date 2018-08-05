using System.Collections.Generic;
using System.IO;

namespace Dawnx.IO
{
    public static class FilesSearcher
    {
        public static string[] GetAllFiles(string path)
        {
            return new List<string>().For(_ =>
            {
                AddFilesToList(_, path);
                return _.ToArray();
            });
        }

        private static void AddFilesToList(List<string> list, string path)
        {
            var root = new DirectoryInfo(path);
            root.GetDirectories().Each(dir => AddFilesToList(list, dir.FullName));
            root.GetFiles().Each(file => list.Add(file.FullName));
        }

    }
}

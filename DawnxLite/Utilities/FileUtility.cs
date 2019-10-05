using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Dawnx.Utilities
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

        public static bool IsFilePath(string path)
        {
            var ext = Path.GetExtension(path);
            if (ext != string.Empty)
            {
                var dir = Path.GetDirectoryName(path);
                if (DirectoryUtility.IsDirectoryPath(dir))
                    return !Path.GetFileNameWithoutExtension(path).IsMatch(new Regex(@"[\\/:*?\""<>|]"));
                else return false;

            }
            else return false;
        }

        public static string ComputeMD5(string path)
        {
            using (var file = new FileStream(path, FileMode.Open))
            {
                return MD5.Create().ComputeHash(file).HexString();
            }
        }

    }
}

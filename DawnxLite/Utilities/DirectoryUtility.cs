using NStandard;
using System.IO;
using System.Text.RegularExpressions;

namespace Dawnx.Utilities
{
    public static class DirectoryUtility
    {
        public static string GetDirFullPath(string dirPath)
        {
            if (dirPath.EndsWith("\\"))
                return dirPath.Slice(0, -1);
            else return dirPath;
        }

        public static void Copy(string dirPath, string destDirPath, bool overwrite = false)
        {
            dirPath = GetDirFullPath(dirPath);
            destDirPath = GetDirFullPath(destDirPath);

            if (Directory.Exists(dirPath))
            {
                if (!Directory.Exists(destDirPath))
                    Directory.CreateDirectory(destDirPath);

                foreach (var filePath in Directory.GetFiles(dirPath))
                {
                    var fileName = Path.GetFileName(filePath);
                    File.Copy($"{dirPath}/{fileName}", $"{destDirPath}/{fileName}", overwrite);
                }
                foreach (var dir in Directory.GetDirectories(dirPath))
                {
                    var dirName = Path.GetFileName(dir);
                    Copy(dir, $"{destDirPath}/{dirName}", overwrite);
                }
            }
            else throw new DirectoryNotFoundException($"The specified path({dirPath}) is not found.");
        }

        public static bool IsDirectoryPath(string path)
        {
            var ext = Path.GetExtension(path);
            if (ext == string.Empty)
                return !path.IsMatch(new Regex(@"[*?\""<>|]"));
            else return false;
        }

    }
}

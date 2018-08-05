using System;
using System.IO;
using System.Security.Cryptography;

namespace Dawnx.Tools
{
    public static class FileUtility
    {
        public static bool CheckMD5(string path, string md5)
        {
            using (var file = new FileStream(path, FileMode.Open))
            {
                Console.SetCursorPosition(72, Console.CursorTop);

                var savedMD5 = MD5.Create().ComputeHash(file).GetHexString();
                return (savedMD5 == md5);
            }
        }

    }
}
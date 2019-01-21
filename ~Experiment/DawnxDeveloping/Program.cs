#if !USE
using Dawnx;
using System.Linq;
using HtmlAgilityPack;
using System.Collections.Generic;
using Dawnx.Net.Web;
using System.IO;

namespace DawnxDevloping
{
    class Program
    {
        static void Main(string[] args)
        {
            var ftp = new FtpAccess("ftp://127.0.0.1");
            string ss;

            ftp.MakeDirectory("地地地");

            using (var file = new FileStream(@"C:\Users\19558\Desktop\[TOC].md", FileMode.Open))
                ftp.UploadFile(file, "1.md");

            using (var file = new FileStream(@"D:\Temp\1.md", FileMode.Create))
                ftp.DownloadFile(file, "1.md");

            var s = ftp.ListDirectoryDetails("");

            var tree1 = ftp.ListTree("", true).Description;
            var tree2 = ftp.ListTree("地", true).Description;
        }

    }
}
#endif

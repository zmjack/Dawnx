using Dawnx.Algorithms.Tree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dawnx.Net.Web
{
    public class FtpListItem
    {
        public enum ItemType { File, Directory }

        public FtpListItem(Uri uri)
        {
            Url = uri;
        }

        public Uri Url { get; private set; }
        public ItemType Type { get; set; }
        public string Name { get; set; }
        public DateTime LastWriteTime { get; set; }
        public long Size { get; set; }

        public string FileNameWithoutExtension
            => Type == ItemType.File ? Path.GetFileNameWithoutExtension(Name) : null;
        public string Extension
            => Type == ItemType.File ? Path.GetExtension(Name) : null;

    }
}

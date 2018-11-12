using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dawnx.AspNetCore
{
    public partial class ZipStream
    {
        public class StaticDataSource : IStaticDataSource
        {
            private Stream StoredStream;

            public StaticDataSource(Stream stream)
            {
                StoredStream = stream;
            }

            public Stream GetSource() => StoredStream;
        }
    }
}

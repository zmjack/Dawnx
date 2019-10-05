using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace Dawnx.Compress
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

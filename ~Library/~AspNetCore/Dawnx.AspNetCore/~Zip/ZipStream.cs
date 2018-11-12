using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

namespace Dawnx.AspNetCore
{
    public class ZipStream : IDisposable
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

        private readonly ZipFile StoredZipFile;

        public ZipStream(Stream outStream)
        {
            StoredZipFile = ZipFile.Create(outStream);
            StoredZipFile.BeginUpdate();
        }

        public void AddFile(string fileName, string entryName)
        {
            StoredZipFile.Add(fileName, entryName);
        }

        public void AddFile(Stream inStream, string entryName)
        {
            StoredZipFile.Add(new StaticDataSource(inStream), entryName);
        }

        public void Dispose()
        {
            StoredZipFile.CommitUpdate();
        }
    }
}

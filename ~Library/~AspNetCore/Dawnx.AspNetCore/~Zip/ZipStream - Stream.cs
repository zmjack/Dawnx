using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

namespace Dawnx.AspNetCore
{
    public partial class ZipStream : Stream
    {
        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => MappedStream.Length;

        public override long Position
        {
            get => MappedStream.Position;
            set => MappedStream.Position = value;
        }

        public override void Flush()
        {
            StoredZipFile.CommitUpdate();
            StoredZipFile.BeginUpdate();
        }

        public override int Read(byte[] buffer, int offset, int count)
            => MappedStream.Read(buffer, offset, count);

        public override long Seek(long offset, SeekOrigin origin)
            => MappedStream.Seek(offset, origin);

        public override void SetLength(long value)
            => throw new NotSupportedException();

        public override void Write(byte[] buffer, int offset, int count)
            => throw new NotSupportedException();
    }
}

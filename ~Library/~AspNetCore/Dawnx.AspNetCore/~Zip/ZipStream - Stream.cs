using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

namespace Dawnx.AspNetCore
{
    public partial class ZipStream : Stream
    {
        public override bool CanRead => false;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => MappedStream.Length;

        public override long Position
        {
            get => MappedStream.Position;
            set => throw new NotSupportedException();
        }

        public override void Flush() => MappedStream.Flush();
        public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
    }
}

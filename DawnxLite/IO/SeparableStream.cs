using System;
using System.IO;

namespace Dawnx.IO
{
    public abstract class SeparableStream : Stream
    {
        protected MemoryStream Stream = new MemoryStream();

        public override bool CanRead => true;
        public override bool CanSeek => true;
        public override bool CanWrite => true;

        public override long Length => Stream.Length;

        public override long Position
        {
            get => Stream.Position;
            set => Stream.Position = value;
        }

        public override void Flush() => throw new NotSupportedException();
        public override int Read(byte[] buffer, int offset, int count) => Stream.Read(buffer, offset, count);
        public override long Seek(long offset, SeekOrigin origin) => Stream.Seek(offset, origin);
        public override void SetLength(long value) => throw new NotSupportedException();
        public override void Write(byte[] buffer, int offset, int count) => Stream.Write(buffer, offset, count);

        public abstract byte[] Take();
        public abstract int BufferSize { get; }

        public byte[] Separate(int count)
        {
            if (count > Stream.Length)
                throw new ArgumentOutOfRangeException($"The '{nameof(count)}' can not be greater than the length of data.");

            Stream.Seek(0, SeekOrigin.Begin);
            var ret = new byte[count].Then(x => Stream.Read(x, 0, count));
            Stream = new MemoryStream().Then(x => Stream.CopyTo(x, BufferSize));

            return ret;
        }

        public void Remove(int count)
        {
            Stream.Seek(count, SeekOrigin.Begin);
            Stream = new MemoryStream().Then(x => Stream.CopyTo(x, BufferSize));
        }

    }
}

using System.IO;

namespace Dawnx
{
    public static class DawnStream
    {
        public delegate void ReadProcessingHandler(byte[] buffer, int length);
        public delegate void WriteProcessingHandler(int wrote);

        public static void ReadProcessing(this Stream @this, int bufferSize, ReadProcessingHandler processing)
        {
            var buffer = new byte[bufferSize];
            int readLength;
            while ((readLength = @this.Read(buffer, 0, bufferSize)) > 0)
                processing(buffer, readLength);
        }

        public static void WriteProcessing(this Stream @this, byte[] buffer, int pieceSize, WriteProcessingHandler processing)
        {
            int total = buffer.Length;
            int wrote = 0;

            while (wrote < total)
            {
                var write = (total - wrote).For(_ => _ < pieceSize ? _ : pieceSize);
                @this.Write(buffer, wrote, write);
                wrote += write;
                processing(wrote);
            }
        }

    }

}

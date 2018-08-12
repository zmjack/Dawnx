using System;
using System.IO;

namespace Dawnx
{
    public static class DawnStream
    {
        public delegate void ReadProcessingHandler(Stream readTarget, byte[] buffer, int readLength);
        public delegate void WriteProcessingHandler(Stream writeTarget, byte[] buffer, int wrote);

        public static void ReadProcess(this Stream @this, int bufferSize, ReadProcessingHandler processing)
        {
            var buffer = new byte[bufferSize];
            int readLength;
            while ((readLength = @this.Read(buffer, 0, bufferSize)) > 0)
                processing(@this, buffer, readLength);
        }

        public static void WriteProcess(this Stream @this, Stream writeTarget, int bufferSize, WriteProcessingHandler processing)
        {
            if (@this.Length >= int.MaxValue)
                throw new NotSupportedException();

            int total = (int)@this.Length;
            int wrote = 0;

            ReadProcess(@this, 1 * 1024 * 1024, (readTarget, buffer, readLength) =>
            {
                writeTarget.Write(buffer, 0, readLength);
                wrote += readLength;
                processing(writeTarget, buffer, wrote);
            });
        }

    }

}

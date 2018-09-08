using System;
using System.IO;

namespace Dawnx
{
    public static class DawnStream
    {
        public delegate void ReadProcessingHandler(Stream readTarget, byte[] buffer, int readLength);
        public delegate void WriteProcessingHandler(Stream writeTarget, byte[] buffer, int totalWrittenLength);
        
        public static void ReadProcess(this Stream @this, int bufferSize, ReadProcessingHandler processing)
        {
            var buffer = new byte[bufferSize];
            int readLength;
            while ((readLength = @this.Read(buffer, 0, bufferSize)) > 0)
                processing(@this, buffer, readLength);
        }

        public static void WriteProcess(this Stream @this, Stream writeTarget, int bufferSize)
            => WriteProcess(@this, writeTarget, bufferSize, (_writeTarget, _buffer, _totalWrittenLength) => { });

        public static void WriteProcess(this Stream @this, Stream writeTarget, int bufferSize, WriteProcessingHandler processing)
        {
            int totalWritten = 0;
            ReadProcess(@this, bufferSize, (readTarget, buffer, readLength) =>
            {
                writeTarget.Write(buffer, 0, readLength);
                totalWritten += readLength;
                processing(writeTarget, buffer, totalWritten);
            });
        }

    }

}

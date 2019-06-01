using Dawnx.AspNetCore;
using Dawnx.Lock;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Microsoft.AspNetCore.Builder
{
    public class ErrorLoggingFileMiddleware : ErrorLoggingMiddleware
    {
        private FileStream _loggingFileStream;
        private StreamWriter _loggingWriter;
        private static TypeLock<ErrorLoggingFileMiddleware> HandleLock = TypeLock<ErrorLoggingFileMiddleware>.Get();
        private string CheckHourlyFile;

        public ErrorLoggingFileMiddleware(RequestDelegate next) : base(next) { }

        protected override void Handle(HttpContext context, Exception exception)
        {
            lock (HandleLock.InternString)
            {
                var now = DateTime.Now;
                var pathBase = "logs";
                var dailyDirectory = now.ToString("yyyyMMdd");
                var hourlyFile = $"{dailyDirectory}/{now.ToString("yyyyMMdd HH")}.txt";

                if (CheckHourlyFile != hourlyFile)
                {
                    var dir = $"{pathBase}/{dailyDirectory}";
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    CheckHourlyFile = hourlyFile;

                    _loggingFileStream?.Dispose();
                    _loggingWriter?.Dispose();

                    _loggingFileStream = new FileStream($"{pathBase}/{hourlyFile}", FileMode.Append, FileAccess.Write);
                    _loggingWriter = new StreamWriter(_loggingFileStream);
                }

                _loggingWriter.WriteLineAsync($"{DateTime.Now}\tUrl: {context.Request.Url()}\tFrom: {context.Connection.RemoteIpAddress}");
                _loggingWriter.WriteLine(exception.Message);
                _loggingWriter.WriteLine(exception.StackTrace);
                _loggingWriter.WriteLine();

                _loggingWriter.Flush();
                _loggingFileStream.Flush();
            }
        }
    }

    public static class ErrorLoggingFileMiddlewareExtension
    {
        public static IApplicationBuilder UseErrorLoggingFile<TErrorLoggingFileMiddleware>(this IApplicationBuilder builder, string loggingFilePath)
            where TErrorLoggingFileMiddleware : ErrorLoggingFileMiddleware
        {
            return builder.UseMiddleware<TErrorLoggingFileMiddleware>(loggingFilePath);
        }
    }

}

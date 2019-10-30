using Dawnx.Security.AesSecurity;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace DawnxDemo
{
    public class Program
    {
        public static AesProvider RemoteAuthSecurity = new AesProvider()
            .WithKey(AesKey.Base64String, "WWtDTmZkdnV0V1J4Ym1qTk1udE5ESmVob1JaZEZ0ZE0=");

        public static void Main(string[] args)
        {
            using (var file = new FileStream("D:/tmp/1.txt", FileMode.Create))
            using (var writer = new StreamWriter(file))
            {
                writer.WriteLine(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
                writer.WriteLine(Environment.GetEnvironmentVariable("ASPNETCORE_APPL_PATH"));
            }

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

    }
}

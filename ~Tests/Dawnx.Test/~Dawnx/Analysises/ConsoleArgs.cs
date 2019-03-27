using System.Collections.Generic;
using Xunit;

namespace Dawnx.Analysises.Test
{
    public class ConsoleArgsTest
    {
        [Fact]
        public void Test1()
        {
            var conArgs = new ConsoleArgs(new[]
            {
                "-h", "123", "-u" ,"root", "C:\\Program Files", "-h", "127.0.0.1"
            }, new[] { "-" });

            Assert.Equal(conArgs.Properties, new Dictionary<string, string>
            {
                ["-h"] = "127.0.0.1",
                ["-u"] = "root",
            });

            Assert.Equal(conArgs.Contents, new[] { "C:\\Program Files" });

            Assert.Null(conArgs["-t"]);
        }

        [Fact]
        public void Test2()
        {
            var conArgs = new ConsoleArgs(@"-h 123 -u root ""C:\Program Files"" -h 127.0.0.1", new[] { "-" });

            Assert.Equal(conArgs.Properties, new Dictionary<string, string>
            {
                ["-h"] = "127.0.0.1",
                ["-u"] = "root",
            });

            Assert.Equal(conArgs.Contents, new[] { "C:\\Program Files" });

            Assert.Null(conArgs["-t"]);
        }

    }
}

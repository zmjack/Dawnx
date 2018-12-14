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
                "-h", "123", "-u" ,"root", "c:\\temp", "-h", "127.0.0.1"
            }, new[] { "-" });

            Assert.Equal(conArgs, new Dictionary<string, string>
            {
                ["-h"] = "127.0.0.1",
                ["-u"] = "root",
                [""] = "c:\\temp",
            });

            Assert.Null(conArgs["-t"]);
        }
    }
}

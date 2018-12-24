using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Dawnx.Test
{
    public class DawnObjectTests
    {
        [Fact]
        public void ForTest()
        {
            var items = new[] { "a12", "_34", "$56" };
            var result = items.Select(x => x.For(new Func<string, string>[]
            {
                _ => _.Project(@"[a-zA-Z]+(\d+)"),
                _ => _.Project(@"_(\d+)"),
            }) ?? "Unknown");

            Assert.Equal(new[] { "12", "34", "Unknown" }, result);
        }
    }
}

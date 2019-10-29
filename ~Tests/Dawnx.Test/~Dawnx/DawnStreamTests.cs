using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Dawnx.Test
{
    public class DawnStreamTests
    {
        [Fact]
        public void Test1()
        {
            using (var memory1 = new MemoryStream("123".Bytes()))
            using (var memory2 = new MemoryStream())
            {
                memory1.WriteTo(memory2, 4096);
                Assert.Equal("123", memory2.ToArray().String());
            }
        }

    }
}

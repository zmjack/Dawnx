using Dawnx.Ranges;
using System;
using Xunit;

namespace Dawnx.Test
{
    public class DawnArrayTests
    {
        [Fact]
        public void Test2()
        {
            var arr = new string[2, 3]
            {
                { "0,0", "0,1", "0,2" },
                { "1,0", "1,1", "1,2" },
            };
            arr.Each((v, i1, i2) =>
            {
                Assert.Equal($"{i1},{i2}", v);
                Assert.Equal($"{i1},{i2}", arr[i1, i2]);
            });

        }
    }
}

using Dawnx.Ranges;
using System;
using Xunit;

namespace Dawnx.Test
{
    public class DawnArrayTests
    {
        [Fact]
        public void Test1()
        {
            var random = new Random();
            var arr = new int[1000000];
            IntegerRange.Create(arr.Length).Each(i => arr[i] = i);
            arr.Shuffle();
        }

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

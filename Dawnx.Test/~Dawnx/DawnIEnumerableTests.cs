using Dawnx.Ranges;
using System.Linq;
using Xunit;

namespace Dawnx.Test
{
    public class DawnIEnumerableTests
    {
        [Fact]
        public void Test1()
        {
            var arr = new[] { 1, 2, 3, 4, 5, 6, 7 };
            var rets = arr.Distribute(3).ToArray();
            Assert.Equal(string.Join(",", new[] { 1, 2, 3 }), string.Join(",", rets[0]));
            Assert.Equal(string.Join(",", new[] { 4, 5, 6 }), string.Join(",", rets[1]));
            Assert.Equal(string.Join(",", new[] { 7 }), string.Join(",", rets[2]));
        }

        [Fact]
        public void Test2()
        {
            var arr = new int[][]
            {
                new[] { 1, 11, 12 },
                new[] { 1, 21, 22 },
                new[] { 2, 31, 32 },
            };
            var groups = arr.Distribute(_ => _[0]).ToArray();

            Assert.Equal(new int[][]
            {
                new[] { 1, 11, 12 },
                new[] { 1, 21, 22 },
            }, groups[0]);

            Assert.Equal(new int[][]
            {
                new[] { 2, 31, 32 },
            }, groups[1]);
        }

        [Fact]
        public void PageTest()
        {
            var items = IntegerRange.Create(10);
            Assert.Equal(4, items.SelectPage(2, 3).PageCount);
            Assert.Equal(2, items.SelectPage(2, 5).PageCount);
            Assert.True(items.SelectPage(1, 3).IsFristPage);
            Assert.True(items.SelectPage(4, 3).IsLastPage);
            Assert.Equal(IntegerRange.Create(5), items.SelectPage(1, 5).ToArray());
            Assert.Equal(IntegerRange.Create(5, 10), items.SelectPage(2, 5).ToArray());
        }

    }
}

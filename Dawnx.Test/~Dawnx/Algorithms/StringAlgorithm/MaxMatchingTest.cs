using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dawnx.Algorithms.StringAlgorithm.Test
{
    public class MaxMatchingTest
    {
        [Fact]
        public void Test1()
        {
            var maxMatching = new MaxMatching(new[] { "同", "一个", "世界", "梦想" });
            var words = maxMatching.GetWords("同一个世界，同一个梦想").Distinct();

            Assert.Equal(new[] { "同", "一个", "世界", "，", "梦想" }, words);
        }

    }
}

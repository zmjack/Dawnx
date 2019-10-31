using System.Linq;
using Xunit;

namespace Dawnx.Algorithms.ApplyingAlgorithm.Test
{
    public class DistributorTests
    {
        private class Bucket
        {
            public int Capacity { get; set; }
            public int Gain { get; set; }
        }

        [Fact]
        public void Test1()
        {
            var distributor = new Distributor(DistributionMethod.FormerPreferred);
            var rest = distributor.Distribute(20, new[] { 4, 2, 8, 10 }, out var result);
            Assert.Equal(new[] { 4, 2, 8, 6 }, result);
            Assert.Equal(0, rest);
        }

        [Fact]
        public void Test2()
        {
            var buckets = new[]
            {
                new Bucket{ Capacity = 4 },
                new Bucket{ Capacity = 2 },
                new Bucket{ Capacity = 8 },
                new Bucket{ Capacity = 10 },
            };

            var distributor = new Distributor(DistributionMethod.FormerPreferred);
            var rest = distributor.Distribute(20, buckets, x => x.Capacity, (x, gain) => x.Gain = gain);
            Assert.Equal(new[] { 4, 2, 8, 6 }, buckets.Select(x => x.Gain).ToArray());
            Assert.Equal(0, rest);
        }

    }
}

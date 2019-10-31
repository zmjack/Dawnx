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
            var producer = new DefaultProducer(16);
            var consumers = new[] { 4, 2, 8, 10 }.Select(x => new DefaultConsumer(x)).ToArray();
            var result = distributor.Distribute(new[] { producer }, x => x.Amount, consumers, x => x.Capacity, out var rests);
            Assert.Equal(new[] { 4, 2, 8, 2 }, result.Select(x => x.Gain).ToArray());
            Assert.Equal(0, rests[0].Rest);
        }

        [Fact]
        public void MutilProducerTest()
        {
            var producers = new[]
            {
                new DefaultProducer(10),
                new DefaultProducer(6),
            };

            var customers = new[]
            {
                new Bucket{ Capacity = 4 },
                new Bucket{ Capacity = 2 },
                new Bucket{ Capacity = 8 },
                new Bucket{ Capacity = 10 },
            };

            var distributor = new Distributor(DistributionMethod.FormerPreferred);
            var result = distributor.Distribute(producers, x => x.Amount, customers, x => x.Capacity, out var rests);
            Assert.Equal(new[] { 4, 2, 4, 4, 2 }, result.Select(x => x.Gain).ToArray());
            Assert.Equal(0, rests[0].Rest);
        }

    }
}

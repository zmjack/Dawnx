using NStandard;
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
        public void SimpleTest()
        {
            var distributor = new Distributor(DistributionMethod.FormerPreferred);
            var contract = new DistributionContract<Ref<int>, Ref<int>>
            {
                Producers = new Ref<int>[] { 8, 8 },
                ProducerAmount = x => x.Value,
                Consumers = new Ref<int>[] { 4, 2, 8, 10 },
                ConsumerAmount = x => x.Value,
            };
            var results = distributor.Distribute(contract, out var rests);

            var ps = contract.Producers.Select(x => x.RefValue).ToArray();
            var cs = contract.Consumers.Select(x => x.RefValue).ToArray();
            Assert.Equal(new[] { 4, 2, 2, 6, 2 }, results.Select(x => x.Gain).ToArray());
            foreach (var line in Linear.Create(new[] { ps[0], ps[0], ps[0], ps[1], ps[1] }, results.Select(x => x.Producer.RefValue).ToArray()))
                Assert.Same(line.Item1, line.Item2);
            foreach (var line in Linear.Create(new[] { cs[0], cs[1], cs[2], cs[2], cs[3] }, results.Select(x => x.Consumer.RefValue).ToArray()))
                Assert.Same(line.Item1, line.Item2);
            Assert.Equal(0, rests[0].Rest);

            Assert.Same(results[0].Producer.RefValue, ps[0]);
            Assert.NotSame(results[0].Producer.RefValue, ps[1]);
        }

        [Fact]
        public void DefaultDistributionPartTest()
        {
            var distributor = new Distributor(DistributionMethod.FormerPreferred);
            var contract = new DistributionContract<DefaultDistributionPart<string>, Bucket>
            {
                Producers = new[]
                {
                    DefaultDistributionPart.Create("A", 10),
                    DefaultDistributionPart.Create("B", 6),
                },
                ProducerAmount = x => x.Amount,
                Consumers = new[]
                {
                    new Bucket{ Capacity = 4 },
                    new Bucket{ Capacity = 2 },
                    new Bucket{ Capacity = 8 },
                    new Bucket{ Capacity = 10 },
                },
                ConsumerAmount = x => x.Capacity,
            };
            var result = distributor.Distribute(contract, out var rests);

            Assert.Equal(new[] { 4, 2, 4, 4, 2 }, result.Select(x => x.Gain).ToArray());
            Assert.Equal(0, rests[0].Rest);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.Algorithms.ApplyingAlgorithm
{
    public class DistributionContract<TProducer, TConsumer>
        where TProducer : class
        where TConsumer : class
    {
        public TProducer[] Producers;
        public Func<TProducer, int> ProducerAmount;
        public TConsumer[] Consumers;
        public Func<TConsumer, int> ConsumerAmount;
    }
}

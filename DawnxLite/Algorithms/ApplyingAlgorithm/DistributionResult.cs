namespace Dawnx.Algorithms.ApplyingAlgorithm
{
    public static class DistributionResult
    {
        public static DistributionResult<TProducer, TConsumer> Create<TProducer, TConsumer>(TProducer producer, TConsumer consumer, int gain)
            where TProducer : class
            where TConsumer : class
        {
            return new DistributionResult<TProducer, TConsumer>(producer, consumer, gain);
        }
    }

    public class DistributionResult<TProducer, TConsumer>
        where TProducer : class
        where TConsumer : class
    {
        public TProducer Producer { get; set; }
        public TConsumer Consumer { get; set; }
        public int Gain { get; set; }

        public DistributionResult(TProducer producer, TConsumer consumer, int gain)
        {
            Producer = producer;
            Consumer = consumer;
            Gain = gain;
        }

    }
}

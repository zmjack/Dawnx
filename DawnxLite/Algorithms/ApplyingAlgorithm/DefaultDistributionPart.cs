namespace Dawnx.Algorithms.ApplyingAlgorithm
{
    public static class DefaultDistributionPart
    {
        public static DefaultDistributionPart<TName> Create<TName>(TName name, int amount) => new DefaultDistributionPart<TName>(name, amount);
    }

    public class DefaultDistributionPart<TName>
    {
        public TName Name { get; set; }
        public int Amount { get; set; }

        public DefaultDistributionPart(TName name, int amount)
        {
            Name = name;
            Amount = amount;
        }
    }

}

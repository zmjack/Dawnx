namespace Dawnx.Algorithms.ApplyingAlgorithm
{
    public class DefaultProducer
    {
        public string Name { get; set; }
        public int Amount { get; set; }

        public DefaultProducer(int amount) => Amount = amount;
        public DefaultProducer(string name, int amount) : this(amount) => Name = name;
    }
}

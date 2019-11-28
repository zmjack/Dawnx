namespace Dawnx.Algorithms.ApplyingAlgorithm
{
    public class DefaultConsumer
    {
        public string Name { get; set; }
        public int Capacity { get; set; }

        public DefaultConsumer(int capacity) => Capacity = capacity;
        public DefaultConsumer(string name, int capacity) : this(capacity) => Name = name;
    }
}

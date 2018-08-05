namespace Dawnx
{
    public class VI<TValue>
    {
        public VI(TValue value, int index)
        {
            Value = value;
            Index = index;
        }

        public TValue Value { get; private set; }
        public int Index { get; private set; }
    }
}

namespace Dawnx
{
    public class ValueTextPair<TValue>
    {
        public ValueTextPair(TValue value, string text)
        {
            Value = value;
            Text = text;
        }

        public TValue Value { get; private set; }
        public string Text { get; private set; }
    }

}

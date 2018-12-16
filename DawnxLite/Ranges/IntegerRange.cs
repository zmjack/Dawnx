using System.Collections;
using System.Collections.Generic;

namespace Dawnx.Ranges
{
    public class IntegerRange : IRange<int>
    {
        public int Start { get; private set; }
        public int End { get; private set; }
        public int Step { get; private set; }

        public IntegerRange(int end) : this(0, end, 1) { }
        public IntegerRange(int start, int end) : this(start, end, 1) { }
        public IntegerRange(int start, int end, int step)
        {
            Start = start;
            End = end;
            Step = step;
        }

        public int GetValue(int index) => Start + index * Step;

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i * Step <= (End - Start); i++)
                yield return GetValue(i);
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool IsInRange(int value)
        {
            if (Start <= value && value <= End)
                return (value - Start) % Step == 0;
            else return false;
        }
    }
}

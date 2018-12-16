using Dawnx.Sequences;
using System.Collections;
using System.Collections.Generic;

namespace Dawnx.Ranges
{
    public class LetterRange : IRange<string>
    {
        public int Start { get; private set; }
        public int End { get; private set; }
        public int Step { get; private set; }

        public LetterRange(string end) : this("A", end, 1) { }
        public LetterRange(string start, string end) : this(start, end, 1) { }
        public LetterRange(string start, string end, int step) : this(LetterSequence.GetNumber(start), LetterSequence.GetNumber(end), 1) { }
        public LetterRange(int start, int end, int step)
        {
            Start = start;
            End = end;
            Step = step;
        }

        public string GetValue(int index) => LetterSequence.GetLetter(index);

        public IEnumerator<string> GetEnumerator()
        {
            for (int i = 0; i <= End; i++)
                yield return GetValue(i);
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool IsInRange(string value)
        {
            var number = LetterSequence.GetNumber(value);
            if (Start <= number && number <= End)
                return (number - Start) % Step == 0;
            else return false;
        }
    }
}

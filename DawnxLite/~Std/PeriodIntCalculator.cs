using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx
{
    //TODO: Add some typical scenarios
    public class PeriodIntCalculator
    {
        public int Start { get; private set; }
        public int End { get; private set; }
        public uint Span { get; private set; }
        public uint Period => NeedCalculate ? Span + 1 : 0;
        public bool NeedCalculate { get; private set; }

        public PeriodIntCalculator(int start, int end)
        {
            Start = start;
            End = end;
            unchecked
            {
                Span = (uint)(end - start);
            }
            NeedCalculate = Span != uint.MaxValue;
        }

        public PeriodInt From(int value)
        {
            return new PeriodInt(this, value);
        }
    }

}

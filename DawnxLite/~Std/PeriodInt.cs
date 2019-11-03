using System;

namespace Dawnx
{
    public struct PeriodInt
    {
        public PeriodIntCalculator Calculator { get; private set; }
        public int Value { get; set; }

        public PeriodInt(PeriodIntCalculator calculator, int value)
        {
            Calculator = calculator;
            Value = 0;
            Value = GetValidValue(value);
        }

        public int GetValidValue(int value)
        {
            if (Calculator.NeedCalculate)
            {
                unchecked
                {
                    return (int)(Calculator.Start + ((uint)(value - Calculator.Start)).Mod(Calculator.Period));
                }
            }
            else return value;
        }

        public static PeriodInt operator +(PeriodInt self, int value)
        {
            value = self.GetValidValue(self.Value + value);
            return new PeriodInt(self.Calculator, value);
        }

        public static PeriodInt operator -(PeriodInt self, int value)
        {
            value = self.GetValidValue(self.Value - value);
            return new PeriodInt(self.Calculator, value);
        }

        public static implicit operator int(PeriodInt self) => self.Value;

    }
}

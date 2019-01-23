using System;
using System.Linq;
using System.Text;

namespace Dawnx.Chinese
{
    public class CurrencyUtility
    {
        private static readonly string[] LowerValues = new string[] { "〇", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
        private static readonly string[] UpperValues = new string[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
        private static readonly string[] Levels = new string[] { "亿", "万", "" };
        private static readonly string[] LowerSingleUnits = new string[] { "千", "百", "十", "" };
        private static readonly string[] UpperSingleUnits = new string[] { "仟", "佰", "拾", "" };

        public static string GetString(double money, CurrencyOption option) => GetString((int)money, (int)(money * 100 % 100), option);
        public static string GetString(int integer, int fractional100, CurrencyOption option)
        {
            if (!(0 <= fractional100 && fractional100 <= 99))
                throw new ArgumentException($"The argument `{nameof(fractional100)}` must be between 0 and 99.");

            string GetPartString(char[] singles, string level)
            {
                if (!singles.Any()) return string.Empty;

                var sb = new StringBuilder();
                string zeroValue;
                switch (option.Target)
                {
                    case CurrencyOption.ETarget.Lower: zeroValue = LowerValues[0]; break;
                    case CurrencyOption.ETarget.Upper: zeroValue = UpperValues[0]; break;
                    default: throw new NotSupportedException();
                }

                var zero = false;
                foreach (var vi in singles.AsVI())
                {
                    if (vi.Value != '0')
                    {
                        string value, singleUnit;
                        switch (option.Target)
                        {
                            case CurrencyOption.ETarget.Lower:
                                value = LowerValues[vi.Value - '0'];
                                singleUnit = LowerSingleUnits[LowerSingleUnits.Length - singles.Length + vi.Index];
                                break;
                            case CurrencyOption.ETarget.Upper:
                                value = UpperValues[vi.Value - '0'];
                                singleUnit = UpperSingleUnits[UpperSingleUnits.Length - singles.Length + vi.Index];
                                break;
                            default: throw new NotSupportedException();
                        }

                        if (zero)
                            sb.Append($"{zeroValue}{value}{singleUnit}");
                        else sb.Append($"{value}{singleUnit}");

                        zero = false;
                    }
                    else zero = true;
                }
                sb.Append(level);

                return sb.ToString();
            }

            var levelParts = integer.ToString().Distribute(4, true).ToArray();
            var ret = levelParts.Select((v, i) => GetPartString(v, Levels[Levels.Length - levelParts.Length + i])).Join("");

            if (option.IsSimplified && (ret.StartsWith("一十") || ret.StartsWith("壹拾")))
                ret = ret.Substring(1);

            if (fractional100 == 0)
                return $"{ret}元整";
            else if (fractional100 % 10 == 0)
            {
                switch (option.Target)
                {
                    case CurrencyOption.ETarget.Lower: return $"{ret}元{LowerValues[fractional100 / 10]}角整";
                    case CurrencyOption.ETarget.Upper: return $"{ret}元{UpperValues[fractional100 / 10]}角整";
                    default: throw new NotSupportedException();
                }
            }
            else
            {
                switch (option.Target)
                {
                    case CurrencyOption.ETarget.Lower: return $"{ret}元{LowerValues[fractional100 / 10]}角{LowerValues[fractional100 % 10]}分";
                    case CurrencyOption.ETarget.Upper: return $"{ret}元{UpperValues[fractional100 / 10]}角{UpperValues[fractional100 % 10]}分";
                    default: throw new NotSupportedException();
                }
            }
        }

        public static double GetDouble(string str)
        {
            throw new NotImplementedException();
        }

    }
}

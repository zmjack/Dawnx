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
        private static readonly string[] Unit = new string[] { "元", "角", "分" };

        public static string GetString(double money, CurrencyOption option) => GetString((int)money, (int)money % 1, option);
        public static string GetString(int integer, int fractional, CurrencyOption option)
        {
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
            var str = levelParts.Select((v, i) => GetPartString(v, Levels[Levels.Length - levelParts.Length + i])).Join("");

            if (option.IsSimplified && (str.StartsWith("一十") || str.StartsWith("壹拾")))
                return str.Substring(1);
            else return str;
        }

        //public static double GetDouble(string str) => _Instance._GetDouble(str);

    }
}

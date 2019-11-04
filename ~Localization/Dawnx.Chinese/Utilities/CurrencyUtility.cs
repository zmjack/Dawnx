using NStandard;
using System;
using System.Linq;
using System.Text;

namespace Dawnx.Chinese
{
    public class CurrencyUtility
    {
        private static readonly string[] Levels = new string[] { "亿", "万", "" };
        private static readonly string[][] NumberValues = new string[][]
        {
            new[] { "〇", "一", "二", "三", "四", "五", "六", "七", "八", "九" },
            new[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" },
        };
        private static readonly string[][] SingleNumberUnits = new string[][]
        {
            new[] { "千", "百", "十", "" },
            new[] { "仟", "佰", "拾", "" },
        };
        private static readonly string[][] UnitValues = new string[][]
        {
            new[] { "元", "角", "分" },
            new[] { "圆", "角", "分" },
        };

        public static string GetString(double money, CurrencyOption option) => GetString((int)money, (int)(money * 100 % 100), option);
        public static string GetString(int integer, int fractional100, CurrencyOption option)
        {
            if (!(0 <= fractional100 && fractional100 <= 99))
                throw new ArgumentException($"The argument `{nameof(fractional100)}` must be between 0 and 99.");

            int index;
            switch (option.Target)
            {
                case CurrencyOption.ETarget.Lower: index = 0; break;
                case CurrencyOption.ETarget.Upper: index = 1; break;
                default: throw new NotSupportedException();
            }

            string GetPartString(char[] singles, string level)
            {
                if (!singles.Any()) return string.Empty;

                var sb = new StringBuilder();
                var zero = false;
                foreach (var kv in singles.AsKvPairs())
                {
                    if (kv.Value != '0')
                    {
                        var value = NumberValues[index][kv.Value - '0'];
                        var singleNumberUnit = SingleNumberUnits[index][SingleNumberUnits[0].Length - singles.Length + kv.Key];

                        if (zero)
                            sb.Append($"{NumberValues[index][0]}{value}{singleNumberUnit}");
                        else sb.Append($"{value}{singleNumberUnit}");

                        zero = false;
                    }
                    else zero = true;
                }
                sb.Append(level);

                return sb.ToString();
            }

            var levelParts = integer.ToString()
                .For(parts => parts.AsKvPairs()
                    .GroupBy(x => (x.Key + (4 - parts.Length % 4)) / 4)
                    .Select(g => g.Select(x => x.Value).ToArray()))
                .ToArray();
            var integerRet = levelParts.Select((v, i) => GetPartString(v, Levels[Levels.Length - levelParts.Length + i])).Join("");

            if (option.IsSimplified && (integerRet.StartsWith("一十") || integerRet.StartsWith("壹拾")))
                integerRet = integerRet.Substring(1);

            if (fractional100 == 0)
                return $"{integerRet}{UnitValues[index][0]}整";
            else if (fractional100 % 10 == 0)
                return $"{integerRet}{UnitValues[index][0]}{NumberValues[index][fractional100 / 10]}{UnitValues[index][1]}整";
            else return $"{integerRet}{UnitValues[index][0]}{NumberValues[index][fractional100 / 10]}{UnitValues[index][1]}{NumberValues[index][fractional100 % 10]}{UnitValues[index][2]}";
        }

        public static double GetDouble(string str)
        {
            throw new NotImplementedException();
        }

    }
}

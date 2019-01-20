using System;
using System.Linq;
using System.Text;

namespace Dawnx.Chinese
{
    public class CurrencyUtility : Localization.CurrencyUtility
    {
        private static CurrencyUtility _Instance = new CurrencyUtility();

        private static readonly char[] Values = new char[] { '零', '壹', '贰', '叁', '肆', '伍', '陆', '柒', '捌', '玖' };
        private static readonly char[] Units = new char[] { '元', '拾', '佰', '仟', '万', '拾', '佰', '仟', '亿' };

        public static string GetString(double money) => _Instance._GetString(money);
        protected override string _GetString(double money)
        {
            throw new NotImplementedException();
        }

        public static double GetDouble(string str) => _Instance._GetDouble(str);
        protected override double _GetDouble(string str)
        {
            throw new NotImplementedException();
        }

    }
}

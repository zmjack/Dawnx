using System;
using System.Globalization;
using System.Linq;

namespace Dawnx.Chinese
{
    public class IdentityCard
    {
        public DateTime Birthday { get; private set; }
        public string SerialNumbers { get; private set; }
        public bool IsValid { get; private set; }

        public IdentityCard(string serialNumbers)
        {
            if (serialNumbers.Length != 18)
                throw new ArgumentException("The length of serial numbers must be 18.");
            if (!serialNumbers.Where((v, i) => i < 17).All(x => '0' <= x && x <= '9'))
                throw new ArgumentException("Each number of `serial` must be between 0 and 9.");

            SerialNumbers = serialNumbers;
            IsValid = CheckValid(serialNumbers);
            Birthday = GetBirthday(serialNumbers);
        }

        public static bool CheckValid(string serialNumbers)
        {
            var arr = serialNumbers.ToArray().Select(x => x - '0').ToArray();
            var weights = new[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            var lasts = new[] { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };

            var product = weights.Select((v, i) => arr[i] * v).Sum();
            var last = lasts[product % 11];

            return serialNumbers.Last() == last;
        }

        public static DateTime GetBirthday(string serialNumbers)
        {
            DateTime.TryParseExact(serialNumbers.Substring(6, 8), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var ret);
            return ret;
        }

    }
}

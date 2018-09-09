using System.Linq;
using System.Text;

namespace Dawnx.Sequences
{
    public static class LetterSequence
    {
        public const int RADIX = 26;

        public static int GetNumber(string latter)
        {
            int ret = 0;
            for (int i = 0; i < latter.Length; i++)
                ret = ret * RADIX + latter.ElementAt(i) - 'A' + 1;
            return ret - 1;
        }

        public static string GetLetter(int number)
        {
            var title = new StringBuilder();
            number += 1;

            do
            {
                number -= 1;
                title.Append((char)(number % RADIX + 'A'));
                number /= RADIX;
            } while (number > 0);

            return new string(title.ToString().Reverse().ToArray());
        }
    }
}

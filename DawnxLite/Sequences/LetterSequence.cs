using System.Linq;
using System.Text;

namespace Dawnx.Sequences
{
    public static class LetterSequence
    {
        public const int RADIX = 26;

        /// <summary>
        /// Gets the number of letter. It's start at 0. (For example, "A" is 0).
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
        public static int GetNumber(string letter)
        {
            int ret = 0;
            for (int i = 0; i < letter.Length; i++)
                ret = ret * RADIX + letter.ElementAt(i) - 'A' + 1;
            return ret - 1;
        }

        /// <summary>
        /// Gets the letter of number. It's start at "A". (For example, 0 is "A").
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
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

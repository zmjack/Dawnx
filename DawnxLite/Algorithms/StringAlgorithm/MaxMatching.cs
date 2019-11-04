using NStandard;
using System.Collections.Generic;
using System.Linq;

namespace Dawnx.Algorithms.StringAlgorithm
{
    public class MaxMatching
    {
        public string[] Dictionary { get; private set; }

        public MaxMatching(string[] dictionary)
        {
            Dictionary = dictionary;
        }

        private bool IsMatchPrefix(string str)
        {
            return Dictionary.Where(x => x.Length >= str.Length)
                .Any(x => x.Substring(0, str.Length) == str);
        }

        private IEnumerable<string> _GetWords(string sentence)
        {
            string word = "";
            var chars = sentence.GetEnumerator();

            for (int i = 0; i < sentence.Length; i++)
            {
                var ch = chars.TakeElement();
                var candicate = word + ch;

                if (IsMatchPrefix(candicate))
                    word = candicate;
                else
                {
                    yield return word;
                    word = ch.ToString();
                }
            }
            yield return word;
        }

        public string[] GetWords(string sentence) => _GetWords(sentence).ToArray();

    }
}

using System.Collections.Generic;

namespace Dawnx.Algorithms.StringAlgorithm
{
    public class Kmp
    {
        public string Pattern { get; private set; }
        public int[] PartialMoves { get; private set; }

        public Kmp(string pattern)
        {
            Pattern = pattern;
            PartialMoves = new int[pattern.Length];
            AnalysisPattern();
        }

        private void AnalysisPattern()
        {
            PartialMoves[0] = 1;
            for (int i = 1; i < Pattern.Length; i++)
            {
                var prefixs = Prefixs(Pattern.Slice(0, i + 1));
                var suffixs = Suffixs(Pattern.Slice(0, i + 1));

                for (int j = 0; j < prefixs.Length; j++)
                {
                    if (prefixs[j] == suffixs[j])
                    {
                        PartialMoves[i] = j + 1;
                        break;
                    }
                }

                if (PartialMoves[i] == 0)
                    PartialMoves[i] = 1;
            }
        }

        private string[] Prefixs(string input)
        {
            var ret = new Stack<string>();
            for (int i = 1; i < input.Length; i++)
                ret.Push(input.Slice(0, i));
            return ret.ToArray();
        }

        private string[] Suffixs(string input)
        {
            var ret = new Stack<string>();
            for (int i = 1; i < input.Length; i++)
                ret.Push(input.Slice(-i, input.Length));
            return ret.ToArray();
        }

        public unsafe int Count(string source, bool repeatMatchChars = false)
        {
            int findCount = 0;

            fixed (char* pSource = source)
            {
                char* p = pSource;
                char* pEnd = pSource + source.Length;
                while (p < pEnd)
                {
                    var find = true;
                    for (int i = 0; i < Pattern.Length; i++)
                    {
                        if (*(p + i) != Pattern[i])
                        {
                            p += PartialMoves[i];
                            find = false;
                            break;
                        }
                    }
                    if (find)
                    {
                        findCount++;
                        if (repeatMatchChars)
                            p += 1;
                        else p += Pattern.Length;
                    }
                }
            }

            return findCount;
        }

    }
}

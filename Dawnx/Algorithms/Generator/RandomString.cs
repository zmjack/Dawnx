using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Dawnx.Algorithms.Generator
{
    public static class RandomString
    {
        public static string ReadOne(string format)
            => ReadOne(format, TimeSpan.FromSeconds(5));
        public static string ReadOne(string format, TimeSpan timeout)
            => ReadMany(format, 1, timeout)[0];

        public static string[] ReadMany(string format, int count)
            => ReadMany(format, count, TimeSpan.FromSeconds(5));
        public static string[] ReadMany(string format, int count, TimeSpan timeout)
        {
            var watch = new Stopwatch();
            var segments = CodeSegments(format);
            var maxCount = MaxCount(segments);
            var random = new System.Random();
            var code = new byte[segments.Length];
            var sets = new HashSet<string>();

            if (count > maxCount)
                throw new OverflowException();

            watch.Start();
            for (int recordAmount = 0; recordAmount < count;)
            {
                random.NextBytes(code);
                for (int i = 0; i < code.Length; i++)
                    code[i] = (byte)(code[i] % (byte)segments[i].Length);
                var generatedCode = new string(segments.Select((segment, i) => segment[code[i]]).ToArray());

                if (watch.Elapsed < timeout)
                {
                    if (sets.Add(generatedCode))
                        recordAmount++;
                }
                else throw new TimeoutException();
            }

            return sets.ToArray();
        }

        private unsafe static bool CheckFormat(string format)
        {
            int bracketCount = 0;

            fixed (char* pformat = format)
            {
                char* p = pformat;
                while (*p != 0)
                {

                    if (*p == '$')
                    {
                        switch (*(p + 1))
                        {
                            case 'c':
                            case 'w':
                            case 'l':
                            case 'u':
                            case 'd':
                            case '[':
                            case ']':
                            case '$':
                                p += 2;
                                break;
                            default: return false;
                        }
                    }
                    else
                    {
                        if (*p == '[') bracketCount++;
                        else if (*p == ']') bracketCount--;

                        p++;
                    }


                    if (bracketCount < 0) return false;
                }
            }

            return true;
        }

        private unsafe static string[] CodeSegments(string format)
        {
            if (!CheckFormat(format))
                throw new FormatException("Can not analysis the specified format.");

            var segments = new List<string>();
            fixed (char* pformat = format)
            {
                var segment = new StringBuilder();
                var inBracket = false;

                char* p = pformat;
                while (*p != 0)
                {
                    if (*p == '$')
                    {
                        switch (*(p + 1))
                        {
                            case 'c': segment.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"); break;
                            case 'w': segment.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"); break;
                            case 'l': segment.Append("abcdefghijklmnopqrstuvwxyz"); break;
                            case 'u': segment.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ"); break;
                            case 'd': segment.Append("0123456789"); break;
                            case '[': segment.Append("["); break;
                            case ']': segment.Append("]"); break;
                            case '$': segment.Append("$"); break;
                        }
                        p += 2;
                    }
                    else if (*p == '[')
                    {
                        inBracket = true;
                        p++;
                    }
                    else if (*p == ']')
                    {
                        inBracket = false;
                        p++;
                    }
                    else segment.Append(*p++);

                    if (!inBracket)
                    {
                        segments.Add(new string(segment.ToString().ToCharArray().Distinct().ToArray()));
                        segment.Clear();
                    }
                }
            }
            return segments.ToArray();
        }

        private static double MaxCount(string[] segments)
        {
            double maxCount = 1;
            foreach (var segment in segments)
                maxCount *= segment.Length;
            return maxCount;
        }

    }

}

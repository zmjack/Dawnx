using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Dawnx.Algorithms.String;
using Dawnx.Ranges;

namespace Dawnx
{
    public static partial class DawnString
    {
        /// <summary>
        /// Indicates whether the specified string is null or an System.String.Empty string.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string @this) => string.IsNullOrEmpty(@this);

        /// <summary>
        /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string @this) => string.IsNullOrWhiteSpace(@this);

        /// <summary>
        /// Indicates whether the specified string is an System.String.Empty string.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string @this) => @this.Length == 0;

        /// <summary>
        /// Indicates whether a specified string is empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsWhiteSpace(this string @this)
        {
            foreach (var c in @this)
                if (!char.IsWhiteSpace(c)) return false;
            return true;
        }

        /// <summary>
        /// Returns centered in a string of length width. Padding is done using the specified fillchar (default is an ASCII space).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string Center(this string @this, int width, char fillChar = ' ')
        {
            var len = @this.Length;
            if (width <= len) return @this;

            var total = width - @this.Length;
            var right = total / 2;
            var left = right;
            if (total.IsOdd()) left += 1;

            var sb = new StringBuilder(width);
            Range.Create(left).Each(i => sb.Append(fillChar));
            sb.Append(@this);
            Range.Create(right).Each(i => sb.Append(fillChar));

            return sb.ToString();
        }

        /// <summary>
        /// Indicates whether the string matches the specified regular expression.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static bool IsMatch(this string @this, string regex)
            => new Regex(regex).Match(@this).Success;

        /// <summary>
        /// Indicates whether the string matches the specified regular expression.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="regex"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool IsMatch(this string @this, Regex regex) => regex.Match(@this).Success;

        /// <summary>
        /// Retrieves a substring from this instance. The substring starts at a specified
        ///     character position and continues to the end of the string.
        ///     (If the parameter is negative, the search will start on the right.)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static string Slice(this string @this, int start) => Slice(@this, start, @this.Length);

        /// <summary>
        /// Retrieves a substring from this instance. The substring starts at a specified
        ///     character position and ends with a specified character position.
        ///     (If the parameters is negative, the search will start on the right.)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public static string Slice(this string @this, int start, int stop)
        {
            start = GetCharPosition(ref @this, start);
            stop = GetCharPosition(ref @this, stop);

            var length = stop - start;
            if (length > 0)
                return @this.Substring(start, length);
            else if (length == 0) return "";
            else throw new IndexOutOfRangeException($"'start:{start}' can not greater than 'end:{stop}'.");
        }
        private static int GetCharPosition(ref string str, int pos) => pos < 0 ? str.Length + pos : pos;

        /// <summary>
        /// Returns the char at a specified index in the string.
        ///     (If the parameter is negative, the search will start on the right.)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static char CharAt(this string @this, int pos) => @this[GetCharPosition(ref @this, pos)];

        /// <summary>
        /// Returns a string which is equivalent to adding it to itself n times.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public static string Times(this string @this, int times)
        {
            var sb = new StringBuilder(@this.Length * times);
            for (int i = 0; i < times; i++)
                sb.Append(@this);
            return sb.ToString();
        }

        /// <summary>
        /// Returns the number of occurrences of substring sub in the string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="searchString"></param>
        /// <param name="overlapping"></param>
        /// <returns></returns>
        public static int Count(this string @this, string searchString, bool overlapping = false)
            => new Kmp(searchString).Count(@this, overlapping);

        /// <summary>
        /// Returns a new string which is normalized by the newline string of current environment.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string NormalizeNewLine(this string @this)
        {
            //TODO: Is \r allowed ?
            switch (Environment.NewLine)
            {
                case "\r\n":
                    return new Regex("(?<!\r)\n", RegexOptions.Singleline).Replace(@this, "\r\n");
                case "\n":
                    return new Regex("\r\n", RegexOptions.Singleline).Replace(@this, "\n");

                default: return @this;
            }
        }

        /// <summary>
        /// Divides a string into multi-lines. If the string is null, return string[0]. 
        /// (Maybe you shall use NormalizeNewLine before to convert the NewLine 
        ///     which is defined in other system into the current system's.)
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetLines(this string @this)
        {
            if (@this != null)
            {
                var newLineLength = Environment.NewLine.Length;
                var startIndex = 0;
                var findIndex = -1;

                while ((findIndex = @this.IndexOf(Environment.NewLine, startIndex)) >= 0)
                {
                    yield return @this.Slice(startIndex, findIndex);
                    startIndex = findIndex + newLineLength;
                }

                if (startIndex != @this.Length)
                    yield return @this.Slice(startIndex, @this.Length);
            }
        }

        /// <summary>
        /// Removes all leading and trailing white-space characters from the current string,
        ///     and replaces multiple spaces with a single.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string Unique(this string @this)
            => new Regex(@"[\s]{2,}").Replace(@this.NormalizeNewLine().Replace(Environment.NewLine, " ").Trim(), " ");

        /// <summary>
        /// Projects the specified string to a new string by using regular expressions.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="regex"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string Project(this string @this, Regex regex, string target = null)
        {
            //TODO: to fix $ symbol analysis. (eg. $$1)
            var match = regex.Match(@this);
            if (match.Success)
            {
                if (target is null)
                    return new IntegerRange(1, match.Groups.Count).Select(i => match.Groups[i].Value).Join("");
                else return regex.Replace(@this, target);
            }
            else return null;
        }

    }
}

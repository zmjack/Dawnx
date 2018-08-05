using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.Utilities
{
    public static class StringUtility
    {
        /// <summary>
        /// Get the common starts of the specified strings.
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static string CommonStarts(params string[] strings)
        {
            var minLength = strings.Min(x => x.Length);
            var sb = new StringBuilder(minLength);

            for (int i = 0; i < minLength; i++)
            {
                var take = strings[0][i];
                if (strings.All(x => x[i] == take))
                    sb.Append(take);
                else break;
            }

            return sb.ToString();
        }

    }
}

using System;

namespace Dawnx.Utilities
{
    public static class Base64Utility
    {
        /// <summary>
        /// Converts the specified string, which encodes binary data as url safe base-64 digits, to
        ///     an equivalent 8-bit unsigned integer array.
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string ConvertBase64ToUrlBase64(string base64)
        {
            return base64.Replace("/", "_").Replace("+", "-").TrimEnd('=');
        }

        /// <summary>
        /// Converts an array of 8-bit unsigned integers to its equivalent string representation
        ///     that is encoded with url safe base-64 digits.
        /// </summary>
        /// <param name="urlBase64"></param>
        /// <returns></returns>
        public static string ConvertUrlBase64ToBase64(string urlBase64)
        {
            var padding = "=".Repeat((urlBase64.Length % 4).For(_ =>
            {
                switch (_)
                {
                    case 0: return 0;
                    case 2: return 2;
                    case 3: return 1;
                    default: throw new FormatException("The input is not a valid Base-64 string");
                }
            }));

            return urlBase64.Replace("_", "/").Replace("-", "+").For(_ => $"{_}{padding}");
        }

    }
}

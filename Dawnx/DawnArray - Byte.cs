using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx
{
    public static partial class DawnArray
    {
        /// <summary>
        /// Converts an array of 8-bit unsigned integers to its equivalent string representation
        ///     that is encoded with base-64 digits.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string GetBase64String(this byte[] @this) => Convert.ToBase64String(@this);

        /// <summary>
        /// Converts an array of 8-bit unsigned integers to its equivalent string representation
        ///     that is encoded with hex digits.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string GetHexString(this byte[] @this, string separator = "")
        {
            var ret = new List<string>();
            @this.Each(@byte => ret.Add(@byte.ToString("x2")));
            return string.Join(separator, ret.ToArray());
        }

        /// <summary>
        /// Decodes all the bytes in the specified byte (Unicode, UTF-16) array into a string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetString(this byte[] @this)
            => GetString(@this, Encoding.Unicode);

        /// <summary>
        /// Decodes all the bytes in the specified byte array into a string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetString(this byte[] @this, string encoding)
            => Encoding.GetEncoding(encoding).GetString(@this);

        /// <summary>
        /// Decodes all the bytes in the specified byte array into a string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetString(this byte[] @this, Encoding encoding)
            => encoding.GetString(@this);

    }
}

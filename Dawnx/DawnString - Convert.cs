using System;
using System.Text;

namespace Dawnx
{
    public static partial class DawnString
    {
        /// <summary>
        /// Encodes all the characters in the specified string into a sequence of bytes (Unicode, UTF-16), then returns it.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this string @this)
            => GetBytes(@this, Encoding.Unicode);

        /// <summary>
        /// Encodes all the characters in the specified string into a sequence of bytes, then returns it.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this string @this, Encoding encoding)
            => encoding.GetBytes(@this);

        /// <summary>
        /// Encodes all the characters in the specified string into a sequence of bytes, then returns it.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this string @this, string encoding)
            => Encoding.GetEncoding(encoding).GetBytes(@this);

        /// <summary>
        /// Converts the specified string, which encodes binary data as base-64 digits, to
        ///     an equivalent 8-bit unsigned integer array.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] GetBytesFromBase64(this string @this)
            => Convert.FromBase64String(@this);

        /// <summary>
        /// Converts the specified string, which encodes binary data as hex digits, to
        ///     an equivalent 8-bit unsigned integer array.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static byte[] GetBytesFromHexString(this string @this, string separator = "")
        {
            if (@this.IsNullOrEmpty())
                return new byte[0];

            var hexString = @this;
            if (!separator.IsEmpty())
                hexString = hexString.Replace(separator, "");

            var length = @this.Length;
            if (length.IsOdd())
                throw new FormatException("The specified string's length must be even.");

            var ret = new byte[length / 2];
            for (int i = 0; i < ret.Length; i++)
                ret[i] = Convert.ToByte(@this.Substring(i * 2, 2), 16);

            return ret;
        }

    }
}

using System;
using System.Net;
using System.Text;

namespace Dawnx
{
    public static partial class DawnString
    {
        /// <summary>
        /// Encodes all the characters in the specified string into a sequence of bytes(UTF-8), then returns it.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] Bytes(this string @this) => Bytes(@this, Encoding.UTF8);

        /// <summary>
        /// Encodes all the characters in the specified string into a sequence of bytes, then returns it.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] Bytes(this string @this, Encoding encoding) => encoding.GetBytes(@this);

        /// <summary>
        /// Encodes all the characters in the specified string into a sequence of bytes, then returns it.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] Bytes(this string @this, string encoding) => Encoding.GetEncoding(encoding).GetBytes(@this);

        /// <summary>
        /// Converts the specified string, which encodes binary data as base-64 digits, to
        ///     an equivalent 8-bit unsigned integer array.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] BytesFromBase64(this string @this) => Convert.FromBase64String(@this);

        /// <summary>
        /// Converts the specified string, which encodes binary data as hex digits, to
        ///     an equivalent 8-bit unsigned integer array.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static byte[] BytesFromHex(this string @this, string separator = "")
        {
            if (@this.IsNullOrEmpty()) return new byte[0];

            var hexString = @this;
            if (!separator.IsNullOrEmpty())
                hexString = hexString.Replace(separator, "");

            var length = hexString.Length;
            if (length.IsOdd())
                throw new FormatException("The specified string's length must be even.");

            var ret = new byte[length / 2];
            for (int i = 0; i < ret.Length; i++)
                ret[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);

            return ret;
        }

        /// <summary>
        /// Converts the specified string into a Base64-encoded string(UTF-8).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string Base64Encode(this string @this)
            => @this.Bytes(Encoding.UTF8).Base64String();

        /// <summary>
        /// Converts the specified string into a Base64-encoded string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Base64Encode(this string @this, Encoding encoding)
            => @this.Bytes(encoding).Base64String();

        /// <summary>
        /// Converts the specified string into a Hex-encoded string(UTF-8).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string HexEncode(this string @this)
            => @this.Bytes(Encoding.UTF8).HexString();

        /// <summary>
        /// Converts the specified string into a Hex-encoded string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string HexEncode(this string @this, Encoding encoding)
            => @this.Bytes(encoding).HexString();

        /// <summary>
        /// Converts the specified string, which encodes binary data as base-64 digits, to a new string(UTF-8).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string Base64Decode(this string @this)
            => @this.BytesFromBase64().String(Encoding.UTF8);

        /// <summary>
        /// Converts the specified string, which encodes binary data as base-64 digits, to a new string.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string Base64Decode(this string @this, Encoding encoding)
            => @this.BytesFromBase64().String(encoding);

        /// <summary>
        /// Converts the specified string, which encodes binary data as hex digits, to a new string(UTF-8).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string HexDecode(this string @this, string separator = "")
            => @this.BytesFromHex(separator).String(Encoding.UTF8);

        /// <summary>
        /// Converts the specified string, which encodes binary data as hex digits, to a new string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string HexDecode(this string @this, Encoding encoding, string separator = "")
            => @this.BytesFromHex(separator).String(encoding);

        /// <summary>
        /// Converts the specified string into a URL-encoded string.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string UrlEncode(this string @this) => WebUtility.UrlEncode(@this);

        /// <summary>
        /// Converts the specified string that has been encoded for transmission in a URL into a decoded string.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string UrlDecode(this string @this) => WebUtility.UrlDecode(@this);

        /// <summary>
        /// Converts the specified string to an HTML-encoded string.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string HtmlEncode(this string @this) => WebUtility.HtmlEncode(@this);

        /// <summary>
        /// Converts the specified string that has been HTML-encoded for HTTP transmission into a decoded string.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string HtmlDecode(this string @this) => WebUtility.HtmlDecode(@this);

    }
}

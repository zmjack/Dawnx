//using Dawnx.Utilities;
//using System;
//using System.Net;
//using System.Text;

//namespace Dawnx
//{
//    public static partial class DawnString
//    {

//        /// <summary>
//        /// Converts the specified string into a Base64-encoded string(UTF-8).
//        /// </summary>
//        /// <param name="this"></param>
//        /// <returns></returns>
//        public static string Base64Encode(this string @this)
//            => @this.Bytes(Encoding.UTF8).Base64();

//        /// <summary>
//        /// Converts the specified string into a url safe Base64-encoded string.
//        /// </summary>
//        /// <param name="this"></param>
//        /// <param name="encoding"></param>
//        /// <returns></returns>
//        public static string UrlSafeBase64Encode(this string @this, Encoding encoding)
//            => @this.Bytes(encoding).UrlSafeBase64();

//        /// <summary>
//        /// Converts the specified string into a url safe Base64-encoded string(UTF-8).
//        /// </summary>
//        /// <param name="this"></param>
//        /// <returns></returns>
//        public static string UrlSafeBase64Encode(this string @this)
//            => @this.Bytes(Encoding.UTF8).UrlSafeBase64();

//        /// <summary>
//        /// Converts the specified string into a Base64-encoded string.
//        /// </summary>
//        /// <param name="this"></param>
//        /// <param name="encoding"></param>
//        /// <returns></returns>
//        public static string Base64Encode(this string @this, Encoding encoding)
//            => @this.Bytes(encoding).Base64();

//        /// <summary>
//        /// Converts the specified string into a Hex-encoded string(UTF-8).
//        /// </summary>
//        /// <param name="this"></param>
//        /// <returns></returns>
//        public static string HexEncode(this string @this)
//            => @this.Bytes(Encoding.UTF8).HexString();

//        /// <summary>
//        /// Converts the specified string into a Hex-encoded string.
//        /// </summary>
//        /// <param name="this"></param>
//        /// <param name="encoding"></param>
//        /// <returns></returns>
//        public static string HexEncode(this string @this, Encoding encoding)
//            => @this.Bytes(encoding).HexString();

//        /// <summary>
//        /// Converts the specified string, which encodes binary data as base-64 digits, to a new string(UTF-8).
//        /// </summary>
//        /// <param name="this"></param>
//        /// <returns></returns>
//        public static string Base64Decode(this string @this)
//            => @this.Base64Bytes().String(Encoding.UTF8);

//        /// <summary>
//        /// Converts the specified string, which encodes binary data as base-64 digits, to a new string.
//        /// </summary>
//        /// <param name="this"></param>
//        /// <param name="encoding"></param>
//        /// <returns></returns>
//        public static string Base64Decode(this string @this, Encoding encoding)
//            => @this.Base64Bytes().String(encoding);

//        /// <summary>
//        /// Converts the specified string, which encodes binary data as base-64 digits, to a new string(UTF-8).
//        /// </summary>
//        /// <param name="this"></param>
//        /// <returns></returns>
//        public static string UrlSafeBase64Decode(this string @this)
//            => @this.UrlSafeBase64Bytes().String(Encoding.UTF8);

//        /// <summary>
//        /// Converts the specified string, which encodes binary data as base-64 digits, to a new string.
//        /// </summary>
//        /// <param name="this"></param>
//        /// <param name="encoding"></param>
//        /// <returns></returns>
//        public static string UrlSafeBase64Decode(this string @this, Encoding encoding)
//            => @this.UrlSafeBase64Bytes().String(encoding);

//        /// <summary>
//        /// Converts the specified string, which encodes binary data as hex digits, to a new string(UTF-8).
//        /// </summary>
//        /// <param name="this"></param>
//        /// <param name="separator"></param>
//        /// <returns></returns>
//        public static string HexDecode(this string @this, string separator = "")
//            => @this.HexStringBytes(separator).String(Encoding.UTF8);

//        /// <summary>
//        /// Converts the specified string, which encodes binary data as hex digits, to a new string.
//        /// </summary>
//        /// <param name="this"></param>
//        /// <param name="separator"></param>
//        /// <returns></returns>
//        public static string HexDecode(this string @this, Encoding encoding, string separator = "")
//            => @this.HexStringBytes(separator).String(encoding);

//        /// <summary>
//        /// Converts the specified string into a URL-encoded string.
//        /// </summary>
//        /// <param name="this"></param>
//        /// <returns></returns>
//        public static string UrlEncode(this string @this) => WebUtility.UrlEncode(@this);

//        /// <summary>
//        /// Converts the specified string that has been encoded for transmission in a URL into a decoded string.
//        /// </summary>
//        /// <param name="this"></param>
//        /// <returns></returns>
//        public static string UrlDecode(this string @this) => WebUtility.UrlDecode(@this);

//        /// <summary>
//        /// Converts the specified string to an HTML-encoded string.
//        /// </summary>
//        /// <param name="this"></param>
//        /// <returns></returns>
//        public static string HtmlEncode(this string @this) => WebUtility.HtmlEncode(@this);

//        /// <summary>
//        /// Converts the specified string that has been HTML-encoded for HTTP transmission into a decoded string.
//        /// </summary>
//        /// <param name="this"></param>
//        /// <returns></returns>
//        public static string HtmlDecode(this string @this) => WebUtility.HtmlDecode(@this);

//    }
//}

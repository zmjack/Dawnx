using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Text;

namespace Dawnx.AspNetCore
{
    public static class DawnHttpRequest
    {
        /// <summary>
        /// Returns $"{@this.PathBase}{@this.Path}".
        ///     eg. /controller/action
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string UrlPath(this HttpRequest @this) => $"{@this.PathBase}{@this.Path}";

        /// <summary>
        /// Returns $"{@this.PathBase}{@this.Path}{@this.QueryString}".
        ///     eg. /controller/action?id=1
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string UrlPathQuery(this HttpRequest @this) => $"{@this.PathBase}{@this.Path}{@this.QueryString}";

        /// <summary>
        /// Returns $"{@this.Scheme}://{@this.Host}".
        ///     eg. http://dawnx.net
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string UrlSchemeHost(this HttpRequest @this) => $"{@this.Scheme}://{@this.Host}";

        /// <summary>
        /// Returns $"{@this.Scheme}://{@this.Host}{@this.PathBase}{@this.Path}{@this.QueryString}".
        ///     eg. http://dawnx.net/controller/action?id=1
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string Url(this HttpRequest @this)
            => $"{@this.Scheme}://{@this.Host}{@this.PathBase}{@this.Path}{@this.QueryString}";

        /// <summary>
        /// Returns $"{@this.Scheme}://{@this.Host}{@this.PathBase}{@this.Path}".
        ///     eg. http://dawnx.net/controller/action
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string UrlSchemeHostPath(this HttpRequest @this)
            => $"{@this.Scheme}://{@this.Host}{@this.PathBase}{@this.Path}";

        /// <summary>
        /// Determines whether the access device is a mobile.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsMobile(this HttpRequest @this)
        {
            var userAgent = @this.Headers["User-Agent"].ToString();
            var mobileKeywords = new[] { "Android", "iPhone", "iPod", "iPad", "Windows Phone", "Mobile" };
            return mobileKeywords.Any(x => userAgent.Contains(x));
        }

        /// <summary>
        /// Determines whether the access device is a tablet.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsTablet(this HttpRequest @this)
        {
            var userAgent = @this.Headers["User-Agent"].ToString();
            var mobileKeywords = new[] { "Android", "iPhone", "iPod", "iPad", "Windows Phone", "Mobile" };
            return mobileKeywords.Any(x => userAgent.Contains(x));
        }

        /// <summary>
        /// Gets the request body string(UTF-8).
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string BodyString(this HttpRequest @this) => BodyString(@this, Encoding.UTF8);

        /// <summary>
        /// Gets the request body string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string BodyString(this HttpRequest @this, Encoding encoding)
        {
            var memory = new MemoryStream();
            @this.Body.WriteProcess(memory, 256 * 1024);
            return memory.ToArray().String(encoding);
        }

        /// <summary>
        /// Deserializes the body(UTF-8), which is json, to a .NET object.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static object BodyJsonDecode(this HttpRequest @this) => BodyJsonDecode(@this, Encoding.UTF8);

        /// <summary>
        /// Deserializes the body, which is json, to a .NET object.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static object BodyJsonDecode(this HttpRequest @this, Encoding encoding)
            => BodyString(@this, encoding).GetFromJson();

        /// <summary>
        /// Deserializes the body(UTF-8), which is json, to a .NET object.
        /// </summary>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static TRet BodyJsonDecode<TRet>(this HttpRequest @this) => BodyJsonDecode<TRet>(@this);

        /// <summary>
        /// Deserializes the body, which is json, to a .NET object.
        /// </summary>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static TRet BodyJsonDecode<TRet>(this HttpRequest @this, Encoding encoding)
            => BodyString(@this, encoding).GetFromJson<TRet>();

    }
}

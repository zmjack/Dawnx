using Microsoft.AspNetCore.Http;
using System.Linq;

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
        public static string FullPath(this HttpRequest @this) => $"{@this.PathBase}{@this.Path}{@this.QueryString}";

        /// <summary>
        /// Returns $"{@this.Scheme}://{@this.Host}".
        ///     eg. http://dawnx.net
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string SchemeHost(this HttpRequest @this) => $"{@this.Scheme}://{@this.Host}";

        /// <summary>
        /// Returns $"{@this.Scheme}://{@this.Host}{@this.PathBase}{@this.Path}{@this.QueryString}".
        ///     eg. http://dawnx.net/controller/action?id=1
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string Url(this HttpRequest @this)
            => $"{@this.Scheme}://{@this.Host}{@this.PathBase}{@this.Path}{@this.QueryString}";

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

    }
}

using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Dawnx.AspNetCore
{
    public static class DawnHttpRequest
    {
        public static string FullPath(this HttpRequest @this) => $"{@this.PathBase}{@this.Path}{@this.QueryString}";
        public static string SchemeHost(this HttpRequest @this) => $"{@this.Scheme}://{@this.Host}";
        public static string Url(this HttpRequest @this) => $"{SchemeHost(@this)}{FullPath(@this)}";

        public static bool IsMobile(this HttpRequest @this)
        {
            var userAgent = @this.Headers["User-Agent"].ToString();
            var mobileKeywords = new[] { "Android", "iPhone", "iPod", "iPad", "Windows Phone", "Mobile" };
            return mobileKeywords.Any(x => userAgent.Contains(x));
        }
    }
}

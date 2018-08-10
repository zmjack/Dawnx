using System.Net;

namespace Dawnx.Net.Http
{
    public class WebRequestStateContainer
    {
        public string Encoding = DefaultEncoding;
        public string UserAgent = DefaultUserAgent;
        public bool UseProxy = ProxyEnabledByDefault;
        public string ProxyAddress = DefaultProxy.Address;
        public string ProxyUsername = DefaultProxy.Username;
        public string ProxyPassword = DefaultProxy.Password;
        public bool SystemLogin = SystemLoginByDefault;
        public CookieContainer Cookies = new CookieContainer();

        public static bool ProxyEnabledByDefault { get; set; } = false;
        public static ProxyInfo DefaultProxy { get; set; } = new ProxyInfo();
        public static string DefaultUserAgent { get; set; } = "";
        public static string DefaultEncoding { get; set; } = "utf-8";
        public static bool SystemLoginByDefault { get; set; } = false;

        public const string URL_ENCODED = "application/x-www-form-urlencoded";
        public const string FORM_DATA = "multipart/form-data";
        public const string GET = "get";
        public const string POST = "post";
        public const string Put = "put";
        public const string DELETE = "delete";

    }
}

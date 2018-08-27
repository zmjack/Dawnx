using Dawnx.Enums;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Dawnx.Net.Http
{
    public class Web
    {
        public static void RegisterProxy(bool enabledByDefault, string address, string username, string password)
        {
            WebRequestStateContainer.ProxyEnabledByDefault = enabledByDefault;
            WebRequestStateContainer.DefaultProxy = new ProxyInfo
            {
                Address = address,
                Username = username,
                Password = password,
            };
        }
        public static void RegisterSystemLogin(bool value) => WebRequestStateContainer.SystemLoginByDefault = value;
        public static void RegisterUserAgent(string userAgent) => WebRequestStateContainer.DefaultUserAgent = userAgent;
        public static void RegisterEncoding(string encoding) => WebRequestStateContainer.DefaultEncoding = encoding;

        public static string Get(string url, Dictionary<string, object> updata = null,
            WebRequestStateContainer config = null)
            => new WebAccess(config).Get(url, updata);
        public static string Post(string url, Dictionary<string, object> updata = null,
            WebRequestStateContainer config = null)
            => new WebAccess(config).Post(url, updata);
        public static string Up(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null,
            WebRequestStateContainer config = null)
            => new WebAccess(config).Up(url, updata, upfiles);

        public static TRet Get<TRet>(string url, Dictionary<string, object> updata = null,
            WebRequestStateContainer config = null)
            => new WebAccess(config).Get<TRet>(url, updata);
        public static TRet Post<TRet>(string url, Dictionary<string, object> updata = null,
            WebRequestStateContainer config = null)
            => new WebAccess(config).Post<TRet>(url, updata);
        public static TRet Up<TRet>(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null,
            WebRequestStateContainer config = null)
            => new WebAccess(config).Up<TRet>(url, updata, upfiles);

        public static void GetDownload(Stream receiver, string url, Dictionary<string, object> updata = null,
            int bufferSize = WebAccess.RECOMMENDED_BUFFER_SIZE, WebRequestStateContainer config = null)
            => new WebAccess(config).GetDownload(receiver, url, updata, bufferSize);
        public static void PostDownload(Stream receiver, string url, Dictionary<string, object> updata = null,
            int bufferSize = WebAccess.RECOMMENDED_BUFFER_SIZE, WebRequestStateContainer config = null)
            => new WebAccess(config).PostDownload(receiver, url, updata, bufferSize);
        public static void UpDownload(Stream receiver, string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null,
            int bufferSize = WebAccess.RECOMMENDED_BUFFER_SIZE, WebRequestStateContainer config = null)
            => new WebAccess(config).UpDownload(receiver, url, updata, upfiles, bufferSize);

        public static void Download(Stream receiver,
            string method, string url,
            string enctype = MediaType.APPLICATION_X_WWW_FORM_URLENCODED,
            Dictionary<string, object> updata = null,
            Dictionary<string, object> upfiles = null,
            int bufferSize = 4096,
            WebRequestStateContainer config = null)
        {
            new WebAccess(config).Download(receiver, method, enctype, url, updata, upfiles, bufferSize);
        }

        public static string ReadString(
            string method, string url,
            string enctype = MediaType.APPLICATION_X_WWW_FORM_URLENCODED,
            Dictionary<string, object> updata = null,
            Dictionary<string, object> upfiles = null,
            WebRequestStateContainer config = null)
        {
            return new WebAccess(config).ReadString(method, enctype, url, updata, upfiles);
        }

        public static HttpWebResponse GetResponse(
            string method, string url,
            string enctype = MediaType.APPLICATION_X_WWW_FORM_URLENCODED,
            Dictionary<string, object> updata = null,
            Dictionary<string, object> upfiles = null,
            WebRequestStateContainer config = null)
        {
            return new WebAccess(config).GetResponse(method, enctype, url, updata, upfiles);
        }

    }
}

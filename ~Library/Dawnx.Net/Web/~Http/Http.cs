using Dawnx.Definition;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Dawnx.Net.Web
{
    public partial class Http
    {
        public static void RegisterProxy(bool enabledByDefault)
        {
            HttpStateContainer.ProxyEnabledByDefault = enabledByDefault;
        }
        public static void RegisterProxy(bool enabledByDefault, string address, string username, string password)
        {
            HttpStateContainer.ProxyEnabledByDefault = enabledByDefault;
            HttpStateContainer.DefaultProxy = new ProxyInfo
            {
                Address = address,
                Username = username,
                Password = password,
            };
        }
        public static void RegisterSystemLogin(bool value) => HttpStateContainer.SystemLoginByDefault = value;
        public static void RegisterUserAgent(string userAgent) => HttpStateContainer.DefaultUserAgent = userAgent;
        public static void RegisterEncoding(string encoding) => HttpStateContainer.DefaultEncoding = encoding;

        public static void Download(Stream receiver,
            string method, string url,
            string enctype = MimeType.APPLICATION_X_WWW_FORM_URLENCODED,
            Dictionary<string, object> updata = null,
            Dictionary<string, object> upfiles = null,
            int bufferSize = 4096,
            HttpStateContainer config = null)
        {
            new HttpAccess(config).Download(receiver, method, enctype, url, updata, upfiles, bufferSize);
        }

        public static string ReadString(
            string method, string url,
            string enctype = MimeType.APPLICATION_X_WWW_FORM_URLENCODED,
            Dictionary<string, object> updata = null,
            Dictionary<string, object> upfiles = null,
            HttpStateContainer config = null)
        {
            return new HttpAccess(config).ReadString(method, enctype, url, updata, upfiles);
        }

        public static HttpWebResponse GetResponse(
            string method, string url,
            string enctype = MimeType.APPLICATION_X_WWW_FORM_URLENCODED,
            Dictionary<string, object> updata = null,
            Dictionary<string, object> upfiles = null,
            HttpStateContainer config = null)
        {
            return new HttpAccess(config).GetResponse(method, enctype, url, updata, upfiles);
        }

    }
}

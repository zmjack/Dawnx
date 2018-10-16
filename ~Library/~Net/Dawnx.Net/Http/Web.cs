using Dawnx.Enums;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Dawnx.Net.Http
{
    public partial class Web
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

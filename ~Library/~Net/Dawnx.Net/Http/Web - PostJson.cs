using Dawnx.Enums;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Dawnx.Net.Http
{
    public partial class Web
    {
        public static string PostJson(string url, Dictionary<string, object> updata = null, WebRequestStateContainer config = null)
            => new WebAccess(config).PostJson(url, updata);
        public static string PostJson(string url, object updata, WebRequestStateContainer config = null)
            => new WebAccess(config).PostJson(url, updata);

        public static void PostJsonDownload(Stream receiver, string url, Dictionary<string, object> updata = null,
            int bufferSize = WebAccess.RECOMMENDED_BUFFER_SIZE, WebRequestStateContainer config = null)
            => new WebAccess(config).PostJsonDownload(receiver, url, updata, bufferSize);
        public static void PostJsonDownload(Stream receiver, string url, object updata,
            int bufferSize = WebAccess.RECOMMENDED_BUFFER_SIZE, WebRequestStateContainer config = null)
            => new WebAccess(config).PostJsonDownload(receiver, url, updata, bufferSize);

        public static TRet PostJsonFor<TRet>(string url, Dictionary<string, object> updata = null,
            WebRequestStateContainer config = null)
            => new WebAccess(config).PostJsonFor<TRet>(url, updata);
        public static TRet PostJsonFor<TRet>(string url, object updata,
            WebRequestStateContainer config = null)
            => new WebAccess(config).PostJsonFor<TRet>(url, updata);

        public static Dictionary<string, object> PostJsonFor(string url, Dictionary<string, object> updata = null,
            WebRequestStateContainer config = null)
            => new WebAccess(config).PostJsonFor(url, updata);
        public static Dictionary<string, object> PostJsonFor(string url, object updata,
            WebRequestStateContainer config = null)
            => new WebAccess(config).PostJsonFor(url, updata);
        
    }
}

using Dawnx.Enums;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Dawnx.Net.Http
{
    public partial class Web
    {
        public static string Post(string url, Dictionary<string, object> updata = null, WebRequestStateContainer config = null)
            => new WebAccess(config).Post(url, updata);
        public static string Post(string url, object updata, WebRequestStateContainer config = null)
            => new WebAccess(config).Post(url, updata);

        public static void PostDownload(Stream receiver, string url, Dictionary<string, object> updata = null,
            int bufferSize = WebAccess.RECOMMENDED_BUFFER_SIZE, WebRequestStateContainer config = null)
            => new WebAccess(config).PostDownload(receiver, url, updata, bufferSize);
        public static void PostDownload(Stream receiver, string url, object updata,
            int bufferSize = WebAccess.RECOMMENDED_BUFFER_SIZE, WebRequestStateContainer config = null)
            => new WebAccess(config).PostDownload(receiver, url, updata, bufferSize);

        public static TRet PostFor<TRet>(string url, Dictionary<string, object> updata = null,
            WebRequestStateContainer config = null)
            => new WebAccess(config).PostFor<TRet>(url, updata);
        public static TRet PostFor<TRet>(string url, object updata,
            WebRequestStateContainer config = null)
            => new WebAccess(config).PostFor<TRet>(url, updata);

        public static JToken PostFor(string url, Dictionary<string, object> updata = null,
            WebRequestStateContainer config = null)
            => new WebAccess(config).PostFor(url, updata);
        public static JToken PostFor(string url, object updata,
            WebRequestStateContainer config = null)
            => new WebAccess(config).PostFor(url, updata);

    }
}

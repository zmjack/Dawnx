using Dawnx.Enums;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Dawnx.Net.Http
{
    public partial class Web
    {
        public static string Get(string url, Dictionary<string, object> updata = null, WebRequestStateContainer config = null)
            => new WebAccess(config).Get(url, updata);
        public static string Get(string url, object updata, WebRequestStateContainer config = null)
            => new WebAccess(config).Get(url, updata);
        public static void GetDownload(Stream receiver, string url, Dictionary<string, object> updata = null,
            int bufferSize = WebAccess.RECOMMENDED_BUFFER_SIZE, WebRequestStateContainer config = null)
            => new WebAccess(config).GetDownload(receiver, url, updata, bufferSize);
        public static void GetDownload(Stream receiver, string url, object updata,
            int bufferSize = WebAccess.RECOMMENDED_BUFFER_SIZE, WebRequestStateContainer config = null)
            => new WebAccess(config).GetDownload(receiver, url, updata, bufferSize);

        public static TRet GetFor<TRet>(string url, Dictionary<string, object> updata = null,
            WebRequestStateContainer config = null)
            => new WebAccess(config).GetFor<TRet>(url, updata);
        public static TRet GetFor<TRet>(string url, object updata,
            WebRequestStateContainer config = null)
            => new WebAccess(config).GetFor<TRet>(url, updata);

        public static JToken GetFor(string url, Dictionary<string, object> updata = null,
            WebRequestStateContainer config = null)
            => new WebAccess(config).GetFor(url, updata);
        public static JToken GetFor(string url, object updata,
            WebRequestStateContainer config = null)
            => new WebAccess(config).GetFor(url, updata);

    }
}

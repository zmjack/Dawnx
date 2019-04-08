using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Dawnx.Net.Web
{
    public static partial class Http
    {
        public static string PutJson(string url, Dictionary<string, object> updata = null, HttpStateContainer config = null)
            => new HttpAccess(config).PutJson(url, updata);
        public static string PutJson(string url, object updata, HttpStateContainer config = null)
            => new HttpAccess(config).PutJson(url, updata);

        public static string PutJsonDownload(Stream receiver, string url, Dictionary<string, object> updata = null,
            int bufferSize = HttpAccess.RECOMMENDED_BUFFER_SIZE, HttpStateContainer config = null)
            => new HttpAccess(config).PutJsonDownload(receiver, url, updata, bufferSize);
        public static string PutJsonDownload(Stream receiver, string url, object updata,
            int bufferSize = HttpAccess.RECOMMENDED_BUFFER_SIZE, HttpStateContainer config = null)
            => new HttpAccess(config).PutJsonDownload(receiver, url, updata, bufferSize);

        public static TRet PutJsonFor<TRet>(string url, Dictionary<string, object> updata = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).PutJsonFor<TRet>(url, updata);
        public static TRet PutJsonFor<TRet>(string url, object updata,
            HttpStateContainer config = null)
            => new HttpAccess(config).PutJsonFor<TRet>(url, updata);

        public static JToken PutJsonFor(string url, Dictionary<string, object> updata = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).PutJsonFor(url, updata);
        public static JToken PutJsonFor(string url, object updata,
            HttpStateContainer config = null)
            => new HttpAccess(config).PutJsonFor(url, updata);

        public static HttpWebResponse PutJsonResponse(string url, Dictionary<string, object> updata = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).PutJsonResponse(url, updata);

    }
}

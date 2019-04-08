using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Dawnx.Net.Web
{
    public static partial class Http
    {
        public static string Put(string url, Dictionary<string, object> updata = null, HttpStateContainer config = null)
            => new HttpAccess(config).Put(url, updata);
        public static string Put(string url, object updata, HttpStateContainer config = null)
            => new HttpAccess(config).Put(url, updata);

        public static string PutDownload(Stream receiver, string url, Dictionary<string, object> updata = null,
            int bufferSize = HttpAccess.RECOMMENDED_BUFFER_SIZE, HttpStateContainer config = null)
            => new HttpAccess(config).PutDownload(receiver, url, updata, bufferSize);
        public static string PutDownload(Stream receiver, string url, object updata,
            int bufferSize = HttpAccess.RECOMMENDED_BUFFER_SIZE, HttpStateContainer config = null)
            => new HttpAccess(config).PutDownload(receiver, url, updata, bufferSize);

        public static TRet PutFor<TRet>(string url, Dictionary<string, object> updata = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).PutFor<TRet>(url, updata);
        public static TRet PutFor<TRet>(string url, object updata,
            HttpStateContainer config = null)
            => new HttpAccess(config).PutFor<TRet>(url, updata);

        public static JToken PutFor(string url, Dictionary<string, object> updata = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).PutFor(url, updata);
        public static JToken PutFor(string url, object updata,
            HttpStateContainer config = null)
            => new HttpAccess(config).PutFor(url, updata);

        public static HttpWebResponse PutResponse(string url, Dictionary<string, object> updata = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).PutResponse(url, updata);

    }
}

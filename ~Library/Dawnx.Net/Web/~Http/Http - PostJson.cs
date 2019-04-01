using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace Dawnx.Net.Web
{
    public static partial class Http
    {
        public static string PostJson(string url, Dictionary<string, object> updata = null, HttpStateContainer config = null)
            => new HttpAccess(config).PostJson(url, updata);
        public static string PostJson(string url, object updata, HttpStateContainer config = null)
            => new HttpAccess(config).PostJson(url, updata);

        public static string PostJsonDownload(Stream receiver, string url, Dictionary<string, object> updata = null,
            int bufferSize = HttpAccess.RECOMMENDED_BUFFER_SIZE, HttpStateContainer config = null)
            => new HttpAccess(config).PostJsonDownload(receiver, url, updata, bufferSize);
        public static string PostJsonDownload(Stream receiver, string url, object updata,
            int bufferSize = HttpAccess.RECOMMENDED_BUFFER_SIZE, HttpStateContainer config = null)
            => new HttpAccess(config).PostJsonDownload(receiver, url, updata, bufferSize);

        public static TRet PostJsonFor<TRet>(string url, Dictionary<string, object> updata = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).PostJsonFor<TRet>(url, updata);
        public static TRet PostJsonFor<TRet>(string url, object updata,
            HttpStateContainer config = null)
            => new HttpAccess(config).PostJsonFor<TRet>(url, updata);

        public static JToken PostJsonFor(string url, Dictionary<string, object> updata = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).PostJsonFor(url, updata);
        public static JToken PostJsonFor(string url, object updata,
            HttpStateContainer config = null)
            => new HttpAccess(config).PostJsonFor(url, updata);

        public static Stream GetStreamUsingPostJson(string url, Dictionary<string, object> updata = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).GetStreamUsingPostJson(url, updata);

    }
}

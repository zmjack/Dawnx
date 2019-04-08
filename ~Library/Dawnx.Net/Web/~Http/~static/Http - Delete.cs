using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Dawnx.Net.Web
{
    public static partial class Http
    {
        public static string Delete(string url, Dictionary<string, object> updata = null, HttpStateContainer config = null)
            => new HttpAccess(config).Delete(url, updata);
        public static string Delete(string url, object updata, HttpStateContainer config = null)
            => new HttpAccess(config).Delete(url, updata);

        public static string DeleteDownload(Stream receiver, string url, Dictionary<string, object> updata = null,
            int bufferSize = HttpAccess.RECOMMENDED_BUFFER_SIZE, HttpStateContainer config = null)
            => new HttpAccess(config).DeleteDownload(receiver, url, updata, bufferSize);
        public static string DeleteDownload(Stream receiver, string url, object updata,
            int bufferSize = HttpAccess.RECOMMENDED_BUFFER_SIZE, HttpStateContainer config = null)
            => new HttpAccess(config).DeleteDownload(receiver, url, updata, bufferSize);

        public static TRet DeleteFor<TRet>(string url, Dictionary<string, object> updata = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).DeleteFor<TRet>(url, updata);
        public static TRet DeleteFor<TRet>(string url, object updata,
            HttpStateContainer config = null)
            => new HttpAccess(config).DeleteFor<TRet>(url, updata);

        public static JToken DeleteFor(string url, Dictionary<string, object> updata = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).DeleteFor(url, updata);
        public static JToken DeleteFor(string url, object updata,
            HttpStateContainer config = null)
            => new HttpAccess(config).DeleteFor(url, updata);

        public static HttpWebResponse DeleteResponse(string url, Dictionary<string, object> updata = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).DeleteResponse(url, updata);

    }
}

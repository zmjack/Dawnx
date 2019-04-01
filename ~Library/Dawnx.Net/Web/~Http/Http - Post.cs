using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace Dawnx.Net.Web
{
    public static partial class Http
    {
        public static string Post(string url, Dictionary<string, object> updata = null, HttpStateContainer config = null)
            => new HttpAccess(config).Post(url, updata);
        public static string Post(string url, object updata, HttpStateContainer config = null)
            => new HttpAccess(config).Post(url, updata);

        public static string PostDownload(Stream receiver, string url, Dictionary<string, object> updata = null,
            int bufferSize = HttpAccess.RECOMMENDED_BUFFER_SIZE, HttpStateContainer config = null)
            => new HttpAccess(config).PostDownload(receiver, url, updata, bufferSize);
        public static string PostDownload(Stream receiver, string url, object updata,
            int bufferSize = HttpAccess.RECOMMENDED_BUFFER_SIZE, HttpStateContainer config = null)
            => new HttpAccess(config).PostDownload(receiver, url, updata, bufferSize);

        public static TRet PostFor<TRet>(string url, Dictionary<string, object> updata = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).PostFor<TRet>(url, updata);
        public static TRet PostFor<TRet>(string url, object updata,
            HttpStateContainer config = null)
            => new HttpAccess(config).PostFor<TRet>(url, updata);

        public static JToken PostFor(string url, Dictionary<string, object> updata = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).PostFor(url, updata);
        public static JToken PostFor(string url, object updata,
            HttpStateContainer config = null)
            => new HttpAccess(config).PostFor(url, updata);

        public static Stream GetStreamUsingPost(string url, Dictionary<string, object> updata = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).GetStreamUsingPost(url, updata);

    }
}

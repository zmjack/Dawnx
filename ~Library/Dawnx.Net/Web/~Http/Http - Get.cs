using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace Dawnx.Net.Web
{
    public static partial class Http
    {
        public static string Get(string url, Dictionary<string, object> updata = null, HttpStateContainer config = null)
            => new HttpAccess(config).Get(url, updata);
        public static string Get(string url, object updata, HttpStateContainer config = null)
            => new HttpAccess(config).Get(url, updata);

        public static string GetDownload(Stream receiver, string url, Dictionary<string, object> updata = null,
            int bufferSize = HttpAccess.RECOMMENDED_BUFFER_SIZE, HttpStateContainer config = null)
            => new HttpAccess(config).GetDownload(receiver, url, updata, bufferSize);
        public static string GetDownload(Stream receiver, string url, object updata,
            int bufferSize = HttpAccess.RECOMMENDED_BUFFER_SIZE, HttpStateContainer config = null)
            => new HttpAccess(config).GetDownload(receiver, url, updata, bufferSize);

        public static TRet GetFor<TRet>(string url, Dictionary<string, object> updata = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).GetFor<TRet>(url, updata);
        public static TRet GetFor<TRet>(string url, object updata,
            HttpStateContainer config = null)
            => new HttpAccess(config).GetFor<TRet>(url, updata);

        public static JToken GetFor(string url, Dictionary<string, object> updata = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).GetFor(url, updata);
        public static JToken GetFor(string url, object updata,
            HttpStateContainer config = null)
            => new HttpAccess(config).GetFor(url, updata);

        public static Stream GetStreamUsingGet(string url, Dictionary<string, object> updata = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).GetStreamUsingGet(url, updata);

    }
}

using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Dawnx.Net.Web
{
    public static partial class Http
    {
        public static string Up(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, HttpStateContainer config = null)
            => new HttpAccess(config).Up(url, updata, upfiles);
        public static string Up(string url, object updata, Dictionary<string, object> upfiles = null, HttpStateContainer config = null)
            => new HttpAccess(config).Up(url, updata, upfiles);

        public static string UpDownload(Stream receiver, string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, int bufferSize = HttpAccess.RECOMMENDED_BUFFER_SIZE, HttpStateContainer config = null)
            => new HttpAccess(config).UpDownload(receiver, url, updata, upfiles, bufferSize);
        public static string UpDownload(Stream receiver, string url, object updata, Dictionary<string, object> upfiles = null, int bufferSize = HttpAccess.RECOMMENDED_BUFFER_SIZE, HttpStateContainer config = null)
            => new HttpAccess(config).UpDownload(receiver, url, updata, upfiles, bufferSize);

        public static TRet UpFor<TRet>(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, HttpStateContainer config = null)
            => new HttpAccess(config).UpFor<TRet>(url, updata, upfiles);
        public static TRet UpFor<TRet>(string url, object updata, Dictionary<string, object> upfiles = null, HttpStateContainer config = null)
            => new HttpAccess(config).UpFor<TRet>(url, updata, upfiles);

        public static JToken UpFor(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, HttpStateContainer config = null)
            => new HttpAccess(config).UpFor(url, updata, upfiles);
        public static JToken UpFor(string url, object updata, Dictionary<string, object> upfiles = null, HttpStateContainer config = null)
            => new HttpAccess(config).UpFor(url, updata, upfiles);

        public static HttpWebResponse UpResponse(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).UpResponse(url, updata, upfiles);

    }
}

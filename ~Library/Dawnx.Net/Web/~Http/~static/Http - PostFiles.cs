using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Dawnx.Net.Web
{
    public static partial class Http
    {
        public static string PostFiles(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, HttpStateContainer config = null)
            => new HttpAccess(config).PostFiles(url, updata, upfiles);
        public static string PostFiles(string url, object updata, Dictionary<string, object> upfiles = null, HttpStateContainer config = null)
            => new HttpAccess(config).PostFiles(url, updata, upfiles);

        public static string PostFilesDownload(Stream receiver, string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, int bufferSize = HttpAccess.RECOMMENDED_BUFFER_SIZE, HttpStateContainer config = null)
            => new HttpAccess(config).PostFilesDownload(receiver, url, updata, upfiles, bufferSize);
        public static string PostFilesDownload(Stream receiver, string url, object updata, Dictionary<string, object> upfiles = null, int bufferSize = HttpAccess.RECOMMENDED_BUFFER_SIZE, HttpStateContainer config = null)
            => new HttpAccess(config).PostFilesDownload(receiver, url, updata, upfiles, bufferSize);

        public static TRet PostFilesFor<TRet>(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, HttpStateContainer config = null)
            => new HttpAccess(config).PostFilesFor<TRet>(url, updata, upfiles);
        public static TRet PostFilesFor<TRet>(string url, object updata, Dictionary<string, object> upfiles = null, HttpStateContainer config = null)
            => new HttpAccess(config).PostFilesFor<TRet>(url, updata, upfiles);

        public static JToken PostFilesFor(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, HttpStateContainer config = null)
            => new HttpAccess(config).PostFilesFor(url, updata, upfiles);
        public static JToken PostFilesFor(string url, object updata, Dictionary<string, object> upfiles = null, HttpStateContainer config = null)
            => new HttpAccess(config).PostFilesFor(url, updata, upfiles);

        public static HttpWebResponse PostFilesResponse(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).PostFilesResponse(url, updata, upfiles);

    }
}

using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Dawnx.Net.Web
{
    public static partial class Http
    {
        public static string PutFiles(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, HttpStateContainer config = null)
            => new HttpAccess(config).PutFiles(url, updata, upfiles);
        public static string PutFiles(string url, object updata, Dictionary<string, object> upfiles = null, HttpStateContainer config = null)
            => new HttpAccess(config).PutFiles(url, updata, upfiles);

        public static string PutFilesDownload(Stream receiver, string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, int bufferSize = HttpAccess.RECOMMENDED_BUFFER_SIZE, HttpStateContainer config = null)
            => new HttpAccess(config).PutFilesDownload(receiver, url, updata, upfiles, bufferSize);
        public static string PutFilesDownload(Stream receiver, string url, object updata, Dictionary<string, object> upfiles = null, int bufferSize = HttpAccess.RECOMMENDED_BUFFER_SIZE, HttpStateContainer config = null)
            => new HttpAccess(config).PutFilesDownload(receiver, url, updata, upfiles, bufferSize);

        public static TRet PutFilesFor<TRet>(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, HttpStateContainer config = null)
            => new HttpAccess(config).PutFilesFor<TRet>(url, updata, upfiles);
        public static TRet PutFilesFor<TRet>(string url, object updata, Dictionary<string, object> upfiles = null, HttpStateContainer config = null)
            => new HttpAccess(config).PutFilesFor<TRet>(url, updata, upfiles);

        public static JToken PutFilesFor(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, HttpStateContainer config = null)
            => new HttpAccess(config).PutFilesFor(url, updata, upfiles);
        public static JToken PutFilesFor(string url, object updata, Dictionary<string, object> upfiles = null, HttpStateContainer config = null)
            => new HttpAccess(config).PutFilesFor(url, updata, upfiles);

        public static HttpWebResponse PutFilesResponse(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null,
            HttpStateContainer config = null)
            => new HttpAccess(config).PutFilesResponse(url, updata, upfiles);

    }
}

using Dawnx.Utilities;
using Def;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Dawnx.Net.Web
{
    public partial class HttpAccess
    {
        public string PutJson(string url, Dictionary<string, object> updata = null)
        {
            return ReadString(
                HttpVerb.PUT, MimeMap.APPLICATION_JSON,
                url, updata, null);
        }
        public string PutJson(string url, object updata) => PutJson(url, ObjectUtility.CovertToDictionary(updata));

        public string PutJsonDownload(Stream receiver, string url, Dictionary<string, object> updata = null, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => Download(receiver, HttpVerb.PUT, MimeMap.APPLICATION_JSON, url, updata, null, bufferSize);
        public string PutJsonDownload(Stream receiver, string url, object updata, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => PutJsonDownload(receiver, url, ObjectUtility.CovertToDictionary(updata), bufferSize);

        public TRet PutJsonFor<TRet>(string url, Dictionary<string, object> updata = null) => PackAsJson<TRet>(PutJson(url, updata));
        public TRet PutJsonFor<TRet>(string url, object updata) => PutJsonFor<TRet>(url, ObjectUtility.CovertToDictionary(updata));

        public JToken PutJsonFor(string url, Dictionary<string, object> updata = null) => PackAsJson<JToken>(PutJson(url, updata));
        public JToken PutJsonFor(string url, object updata) => PutJsonFor(url, ObjectUtility.CovertToDictionary(updata));

        public HttpWebResponse PutJsonResponse(string url, Dictionary<string, object> updata = null)
            => GetPureResponse(HttpVerb.PUT, MimeMap.APPLICATION_JSON, url, updata, null);

    }
}

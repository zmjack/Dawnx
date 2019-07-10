using Dawnx.Definition;
using Dawnx.Utilities;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Dawnx.Net.Web
{
    public partial class HttpAccess
    {
        public string PostJson(string url, Dictionary<string, object> updata = null)
        {
            return ReadString(
                HttpVerb.POST, MimeType.APPLICATION_JSON,
                url, updata, null);
        }
        public string PostJson(string url, object updata) => PostJson(url, ObjectUtility.CovertToDictionary(updata));

        public string PostJsonDownload(Stream receiver, string url, Dictionary<string, object> updata = null, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => Download(receiver, HttpVerb.POST, MimeType.APPLICATION_JSON, url, updata, null, bufferSize);
        public string PostJsonDownload(Stream receiver, string url, object updata, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => PostJsonDownload(receiver, url, ObjectUtility.CovertToDictionary(updata), bufferSize);

        public TRet PostJsonFor<TRet>(string url, Dictionary<string, object> updata = null) => PackAsJson<TRet>(PostJson(url, updata));
        public TRet PostJsonFor<TRet>(string url, object updata) => PostJsonFor<TRet>(url, ObjectUtility.CovertToDictionary(updata));

        public JToken PostJsonFor(string url, Dictionary<string, object> updata = null) => PackAsJson<JToken>(PostJson(url, updata));
        public JToken PostJsonFor(string url, object updata) => PostJsonFor(url, ObjectUtility.CovertToDictionary(updata));

        public HttpWebResponse PostJsonResponse(string url, Dictionary<string, object> updata = null)
            => GetPureResponse(HttpVerb.POST, MimeType.APPLICATION_JSON, url, updata, null);

    }
}

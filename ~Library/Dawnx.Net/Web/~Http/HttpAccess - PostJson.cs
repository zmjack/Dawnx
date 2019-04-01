using Dawnx.Definition;
using Dawnx.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

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

        public TRet PostJsonFor<TRet>(string url, Dictionary<string, object> updata = null)
            => JsonConvert.DeserializeObject<TRet>(PostJson(url, updata));
        public TRet PostJsonFor<TRet>(string url, object updata)
            => PostJsonFor<TRet>(url, ObjectUtility.CovertToDictionary(updata));

        public JToken PostJsonFor(string url, Dictionary<string, object> updata = null)
            => JsonConvert.DeserializeObject<JToken>(PostJson(url, updata));
        public JToken PostJsonFor(string url, object updata)
            => PostJsonFor(url, ObjectUtility.CovertToDictionary(updata));

        public Stream GetStreamUsingPostJson(string url, Dictionary<string, object> updata = null)
        {
            var resp = GetPureResponse(HttpVerb.POST, MimeType.APPLICATION_JSON, url, updata, null);
            return resp.GetResponseStream();
        }

    }
}

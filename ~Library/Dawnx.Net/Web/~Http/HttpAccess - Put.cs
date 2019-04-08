using Dawnx.Definition;
using Dawnx.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Dawnx.Net.Web
{
    public partial class HttpAccess
    {
        public string Put(string url, Dictionary<string, object> updata = null)
        {
            return ReadString(
                HttpVerb.PUT, MimeType.APPLICATION_X_WWW_FORM_URLENCODED,
                url, updata, null);
        }
        public string Put(string url, object updata) => Put(url, ObjectUtility.CovertToDictionary(updata));

        public string PutDownload(Stream receiver, string url, Dictionary<string, object> updata = null, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => Download(receiver, HttpVerb.PUT, MimeType.APPLICATION_X_WWW_FORM_URLENCODED, url, updata, null, bufferSize);
        public string PutDownload(Stream receiver, string url, object updata, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => PutDownload(receiver, url, ObjectUtility.CovertToDictionary(updata), bufferSize);

        public TRet PutFor<TRet>(string url, Dictionary<string, object> updata = null)
            => JsonConvert.DeserializeObject<TRet>(Put(url, updata));
        public TRet PutFor<TRet>(string url, object updata)
            => PutFor<TRet>(url, ObjectUtility.CovertToDictionary(updata));

        public JToken PutFor(string url, Dictionary<string, object> updata = null)
            => JsonConvert.DeserializeObject<JToken>(Put(url, updata));
        public JToken PutFor(string url, object updata)
            => PutFor(url, ObjectUtility.CovertToDictionary(updata));

        public HttpWebResponse PutResponse(string url, Dictionary<string, object> updata = null)
            => GetPureResponse(HttpVerb.PUT, MimeType.APPLICATION_X_WWW_FORM_URLENCODED, url, updata, null);

    }
}

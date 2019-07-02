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
        public string Delete(string url, Dictionary<string, object> updata = null)
        {
            return ReadString(
                HttpVerb.DELETE, MimeType.APPLICATION_X_WWW_FORM_URLENCODED,
                url, updata, null);
        }
        public string Delete(string url, object updata) => Delete(url, ObjectUtility.CovertToDictionary(updata));

        public string DeleteDownload(Stream receiver, string url, Dictionary<string, object> updata = null, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => Download(receiver, HttpVerb.DELETE, MimeType.APPLICATION_X_WWW_FORM_URLENCODED, url, updata, null, bufferSize);
        public string DeleteDownload(Stream receiver, string url, object updata, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => DeleteDownload(receiver, url, ObjectUtility.CovertToDictionary(updata), bufferSize);

        public TRet DeleteFor<TRet>(string url, Dictionary<string, object> updata = null) => PackAsJson<TRet>(Delete(url, updata));
        public TRet DeleteFor<TRet>(string url, object updata) => DeleteFor<TRet>(url, ObjectUtility.CovertToDictionary(updata));

        public JToken DeleteFor(string url, Dictionary<string, object> updata = null) => PackAsJson<JToken>(Delete(url, updata));
        public JToken DeleteFor(string url, object updata) => DeleteFor(url, ObjectUtility.CovertToDictionary(updata));

        public HttpWebResponse DeleteResponse(string url, Dictionary<string, object> updata = null)
            => GetPureResponse(HttpVerb.DELETE, MimeType.APPLICATION_X_WWW_FORM_URLENCODED, url, updata, null);

    }
}

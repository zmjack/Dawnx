using Dawnx.Enums;
using Dawnx.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace Dawnx.Net.Http
{
    public partial class WebAccess
    {
        public string Post(string url, Dictionary<string, object> updata = null)
        {
            return ReadString(
                HttpVerb.POST, MediaType.APPLICATION_X_WWW_FORM_URLENCODED,
                url, updata, null);
        }
        public string Post(string url, object updata) => Post(url, ObjectUtility.CovertToDictionary(updata));

        public void PostDownload(Stream receiver, string url, Dictionary<string, object> updata = null, int bufferSize = RECOMMENDED_BUFFER_SIZE)
        {
            Download(receiver, HttpVerb.POST, MediaType.APPLICATION_X_WWW_FORM_URLENCODED, url, updata, null, bufferSize);
        }
        public void PostDownload(Stream receiver, string url, object updata, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => PostDownload(receiver, url, ObjectUtility.CovertToDictionary(updata), bufferSize);

        public TRet PostFor<TRet>(string url, Dictionary<string, object> updata = null)
            => JsonConvert.DeserializeObject<TRet>(Post(url, updata));
        public TRet PostFor<TRet>(string url, object updata)
            => PostFor<TRet>(url, ObjectUtility.CovertToDictionary(updata));

        public JToken PostFor(string url, Dictionary<string, object> updata = null)
            => JsonConvert.DeserializeObject<JToken>(Post(url, updata));
        public JToken PostFor(string url, object updata)
            => PostFor(url, ObjectUtility.CovertToDictionary(updata));

    }
}

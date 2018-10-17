using Dawnx.Enums;
using Dawnx.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Dawnx.Net.Http
{
    public partial class WebAccess
    {
        public string PostJson(string url, Dictionary<string, object> updata = null)
        {
            return ReadString(
                HttpVerb.POST, MediaType.APPLICATION_JSON,
                url, updata, null);
        }
        public string PostJson(string url, object updata) => PostJson(url, ObjectUtility.CovertToDictionary(updata));

        public void PostJsonDownload(Stream receiver, string url, Dictionary<string, object> updata = null, int bufferSize = RECOMMENDED_BUFFER_SIZE)
        {
            Download(receiver, HttpVerb.POST, MediaType.APPLICATION_JSON, url, updata, null, bufferSize);
        }
        public void PostJsonDownload(Stream receiver, string url, object updata, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => PostJsonDownload(receiver, url, ObjectUtility.CovertToDictionary(updata), bufferSize);

        public TRet PostJsonFor<TRet>(string url, Dictionary<string, object> updata = null)
            => JsonConvert.DeserializeObject<TRet>(PostJson(url, updata));
        public TRet PostJsonFor<TRet>(string url, object updata)
            => PostJsonFor<TRet>(url, ObjectUtility.CovertToDictionary(updata));

        public JToken PostJsonFor(string url, Dictionary<string, object> updata = null)
            => JsonConvert.DeserializeObject<JToken>(PostJson(url, updata));
        public JToken PostJsonFor(string url, object updata)
            => PostJsonFor(url, ObjectUtility.CovertToDictionary(updata));

    }
}

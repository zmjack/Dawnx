using Dawnx.Enums;
using Dawnx.Utilities;
using Newtonsoft.Json;
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
        public string Get(string url, Dictionary<string, object> updata = null)
        {
            return ReadString(
                HttpVerb.GET, MediaType.APPLICATION_X_WWW_FORM_URLENCODED,
                url, updata, null);
        }
        public string Get(string url, object updata) => Get(url, ObjectUtility.CovertToDictionary(updata));

        public void GetDownload(Stream receiver, string url, Dictionary<string, object> updata = null, int bufferSize = RECOMMENDED_BUFFER_SIZE)
        {
            Download(receiver,
                HttpVerb.GET, MediaType.APPLICATION_X_WWW_FORM_URLENCODED,
                url, updata, null, bufferSize);
        }
        public void GetDownload(Stream receiver, string url, object updata, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => GetDownload(receiver, url, ObjectUtility.CovertToDictionary(updata), bufferSize);

        public TRet GetFor<TRet>(string url, Dictionary<string, object> updata = null)
            => JsonConvert.DeserializeObject<TRet>(Get(url, updata));
        public TRet GetFor<TRet>(string url, object updata)
            => GetFor<TRet>(url, ObjectUtility.CovertToDictionary(updata));

        public Dictionary<string, object> GetFor(string url, Dictionary<string, object> updata = null)
            => JsonConvert.DeserializeObject<Dictionary<string, object>>(Get(url, updata));
        public Dictionary<string, object> GetFor(string url, object updata)
            => GetFor(url, ObjectUtility.CovertToDictionary(updata));

    }
}

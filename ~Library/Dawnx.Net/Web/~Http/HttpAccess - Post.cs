﻿using Dawnx.Utilities;
using Def;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Dawnx.Net.Web
{
    public partial class HttpAccess
    {
        public string Post(string url, Dictionary<string, object> updata = null)
        {
            return ReadString(
                HttpVerb.POST, MimeMap.APPLICATION_X_WWW_FORM_URLENCODED,
                url, updata, null);
        }
        public string Post(string url, object updata) => Post(url, ObjectUtility.CovertToDictionary(updata));

        public string PostDownload(Stream receiver, string url, Dictionary<string, object> updata = null, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => Download(receiver, HttpVerb.POST, MimeMap.APPLICATION_X_WWW_FORM_URLENCODED, url, updata, null, bufferSize);
        public string PostDownload(Stream receiver, string url, object updata, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => PostDownload(receiver, url, ObjectUtility.CovertToDictionary(updata), bufferSize);

        public TRet PostFor<TRet>(string url, Dictionary<string, object> updata = null) => PackAsJson<TRet>(Post(url, updata));
        public TRet PostFor<TRet>(string url, object updata) => PostFor<TRet>(url, ObjectUtility.CovertToDictionary(updata));

        public JToken PostFor(string url, Dictionary<string, object> updata = null) => PackAsJson<JToken>(Post(url, updata));
        public JToken PostFor(string url, object updata) => PostFor(url, ObjectUtility.CovertToDictionary(updata));

        public HttpWebResponse PostResponse(string url, Dictionary<string, object> updata = null)
            => GetPureResponse(HttpVerb.POST, MimeMap.APPLICATION_X_WWW_FORM_URLENCODED, url, updata, null);

    }
}

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
        public string Up(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null)
        {
            return ReadString(
                HttpVerb.POST, MimeType.MULTIPART_FORM_DATA,
                url, updata, upfiles);
        }
        public string Up(string url, object updata, Dictionary<string, object> upfiles = null)
            => Up(url, ObjectUtility.CovertToDictionary(updata), upfiles);

        public string UpDownload(Stream receiver, string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => Download(receiver, HttpVerb.POST, MimeType.MULTIPART_FORM_DATA, url, updata, upfiles, bufferSize);
        public string UpDownload(Stream receiver, string url, object updata, Dictionary<string, object> upfiles = null, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => UpDownload(receiver, url, ObjectUtility.CovertToDictionary(updata), upfiles, bufferSize);

        public TRet UpFor<TRet>(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null)
            => JsonConvert.DeserializeObject<TRet>(Up(url, updata, upfiles));
        public TRet UpFor<TRet>(string url, object updata, Dictionary<string, object> upfiles = null)
            => UpFor<TRet>(url, ObjectUtility.CovertToDictionary(updata), upfiles);

        public JToken UpFor(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null)
            => JsonConvert.DeserializeObject<JToken>(Up(url, updata, upfiles));
        public JToken UpFor(string url, object updata, Dictionary<string, object> upfiles = null)
            => UpFor(url, ObjectUtility.CovertToDictionary(updata), upfiles);

        public HttpWebResponse UpResponse(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null)
            => GetPureResponse(HttpVerb.POST, MimeType.MULTIPART_FORM_DATA, url, updata, upfiles);

    }
}

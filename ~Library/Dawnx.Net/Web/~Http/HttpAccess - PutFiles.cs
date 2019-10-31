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
        public string PutFiles(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null)
        {
            return ReadString(
                HttpVerb.PUT, MimeMap.MULTIPART_FORM_DATA,
                url, updata, upfiles);
        }
        public string PutFiles(string url, object updata, Dictionary<string, object> upfiles = null)
            => PutFiles(url, ObjectUtility.CovertToDictionary(updata), upfiles);

        public string PutFilesDownload(Stream receiver, string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => Download(receiver, HttpVerb.PUT, MimeMap.MULTIPART_FORM_DATA, url, updata, upfiles, bufferSize);
        public string PutFilesDownload(Stream receiver, string url, object updata, Dictionary<string, object> upfiles = null, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => PutFilesDownload(receiver, url, ObjectUtility.CovertToDictionary(updata), upfiles, bufferSize);

        public TRet PutFilesFor<TRet>(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null) => PackAsJson<TRet>(PutFiles(url, updata, upfiles));
        public TRet PutFilesFor<TRet>(string url, object updata, Dictionary<string, object> upfiles = null) => PutFilesFor<TRet>(url, ObjectUtility.CovertToDictionary(updata), upfiles);

        public JToken PutFilesFor(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null) => PackAsJson<JToken>(PutFiles(url, updata, upfiles));
        public JToken PutFilesFor(string url, object updata, Dictionary<string, object> upfiles = null) => PutFilesFor(url, ObjectUtility.CovertToDictionary(updata), upfiles);

        public HttpWebResponse PutFilesResponse(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null)
            => GetPureResponse(HttpVerb.PUT, MimeMap.MULTIPART_FORM_DATA, url, updata, upfiles);

    }
}

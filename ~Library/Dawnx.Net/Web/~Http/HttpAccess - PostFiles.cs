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
        public string PostFiles(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null)
        {
            return ReadString(
                HttpVerb.POST, MimeType.MULTIPART_FORM_DATA,
                url, updata, upfiles);
        }
        public string PostFiles(string url, object updata, Dictionary<string, object> upfiles = null)
            => PostFiles(url, ObjectUtility.CovertToDictionary(updata), upfiles);

        public string PostFilesDownload(Stream receiver, string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => Download(receiver, HttpVerb.POST, MimeType.MULTIPART_FORM_DATA, url, updata, upfiles, bufferSize);
        public string PostFilesDownload(Stream receiver, string url, object updata, Dictionary<string, object> upfiles = null, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => PostFilesDownload(receiver, url, ObjectUtility.CovertToDictionary(updata), upfiles, bufferSize);

        public TRet PostFilesFor<TRet>(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null) => PackAsJson<TRet>(PostFiles(url, updata, upfiles));
        public TRet PostFilesFor<TRet>(string url, object updata, Dictionary<string, object> upfiles = null) => PostFilesFor<TRet>(url, ObjectUtility.CovertToDictionary(updata), upfiles);

        public JToken PostFilesFor(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null) => PackAsJson<JToken>(PostFiles(url, updata, upfiles));
        public JToken PostFilesFor(string url, object updata, Dictionary<string, object> upfiles = null) => PostFilesFor(url, ObjectUtility.CovertToDictionary(updata), upfiles);

        public HttpWebResponse PostFilesResponse(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null)
            => GetPureResponse(HttpVerb.POST, MimeType.MULTIPART_FORM_DATA, url, updata, upfiles);

    }
}

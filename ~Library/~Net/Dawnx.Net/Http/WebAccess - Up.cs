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
        public string Up(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null)
        {
            return ReadString(
                HttpVerb.POST, MediaType.MULTIPART_FORM_DATA,
                url, updata, upfiles);
        }
        public string Up(string url, object updata, Dictionary<string, object> upfiles = null)
            => Up(url, ObjectUtility.CovertToDictionary(updata), upfiles);

        public void UpDownload(Stream receiver, string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, int bufferSize = RECOMMENDED_BUFFER_SIZE)
        {
            Download(receiver,
                HttpVerb.POST, MediaType.MULTIPART_FORM_DATA,
                url, updata, upfiles, bufferSize);
        }
        public void UpDownload(Stream receiver, string url, object updata, Dictionary<string, object> upfiles = null, int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => UpDownload(receiver, url, ObjectUtility.CovertToDictionary(updata), upfiles, bufferSize);

        public TRet UpFor<TRet>(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null)
            => JsonConvert.DeserializeObject<TRet>(Up(url, updata, upfiles));
        public TRet UpFor<TRet>(string url, object updata, Dictionary<string, object> upfiles = null)
            => UpFor<TRet>(url, ObjectUtility.CovertToDictionary(updata), upfiles);

        public Dictionary<string, object> UpFor(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null)
            => JsonConvert.DeserializeObject<Dictionary<string, object>>(Up(url, updata, upfiles));
        public Dictionary<string, object> UpFor(string url, object updata, Dictionary<string, object> upfiles = null)
            => UpFor(url, ObjectUtility.CovertToDictionary(updata), upfiles);

    }
}

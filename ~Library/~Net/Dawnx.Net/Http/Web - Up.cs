using Dawnx.Enums;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Dawnx.Net.Http
{
    public partial class Web
    {
        public static string Up(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, WebRequestStateContainer config = null)
            => new WebAccess(config).Up(url, updata, upfiles);
        public static string Up(string url, object updata, Dictionary<string, object> upfiles = null, WebRequestStateContainer config = null)
            => new WebAccess(config).Up(url, updata, upfiles);

        public static void UpDownload(Stream receiver, string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, int bufferSize = WebAccess.RECOMMENDED_BUFFER_SIZE, WebRequestStateContainer config = null)
            => new WebAccess(config).UpDownload(receiver, url, updata, upfiles, bufferSize);
        public static void UpDownload(Stream receiver, string url, object updata, Dictionary<string, object> upfiles = null, int bufferSize = WebAccess.RECOMMENDED_BUFFER_SIZE, WebRequestStateContainer config = null)
            => new WebAccess(config).UpDownload(receiver, url, updata, upfiles, bufferSize);

        public static TRet UpFor<TRet>(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, WebRequestStateContainer config = null)
            => new WebAccess(config).UpFor<TRet>(url, updata, upfiles);
        public static TRet UpFor<TRet>(string url, object updata, Dictionary<string, object> upfiles = null, WebRequestStateContainer config = null)
            => new WebAccess(config).UpFor<TRet>(url, updata, upfiles);

        public static Dictionary<string, object> UpFor(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null, WebRequestStateContainer config = null)
            => new WebAccess(config).UpFor(url, updata, upfiles);
        public static Dictionary<string, object> UpFor(string url, object updata, Dictionary<string, object> upfiles = null, WebRequestStateContainer config = null)
            => new WebAccess(config).UpFor(url, updata, upfiles);

    }
}

using System.Collections.Generic;
using System.Net;

namespace Dawnx.Net.Http
{
    public interface IResponseProcessor
    {
        HttpWebResponse Process(
            WebAccess web, HttpWebResponse response,
            string method, string url, string enctype,
            Dictionary<string, object> updata,
            Dictionary<string, object> upfiles);
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace Dawnx.Net.Http
{
    public abstract class LoginProcessor : IResponseProcessor
    {
        public abstract HttpWebResponse LoginProcess(WebAccess web, HttpWebResponse response);

        HttpWebResponse IResponseProcessor.Process(
            WebAccess web, HttpWebResponse response,
            string method, string enctype, string url,
            Dictionary<string, object> updata,
            Dictionary<string, object> upfiles)
        {
            return LoginProcess(web, response);
        }

    }
}

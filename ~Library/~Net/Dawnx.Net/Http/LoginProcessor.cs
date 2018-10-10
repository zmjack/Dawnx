using System.Collections.Generic;
using System.Net;

namespace Dawnx.Net.Http
{
    public abstract class LoginProcessor : IResponseProcessor
    {
        /// <summary>
        /// If this method cannot determine response, it should be return null.
        /// </summary>
        /// <param name="web"></param>
        /// <param name="response"></param>
        /// <returns></returns>
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

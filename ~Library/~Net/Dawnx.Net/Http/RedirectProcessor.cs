using Dawnx.Enums;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace Dawnx.Net.Http
{
    public class RedirectProcessor : IResponseProcessor
    {
        public HttpWebResponse Process(
            WebAccess web, HttpWebResponse response,
            string method, string enctype, string url,
            Dictionary<string, object> updata,
            Dictionary<string, object> upfiles)
        {
            if (response.StatusCode.In(
                HttpStatusCode.MovedPermanently,        // 301
                HttpStatusCode.Redirect,                // 302
                HttpStatusCode.TemporaryRedirect))      // 307                
            {
                string location = response.Headers["Location"];
                if (!new Regex("^https?://").Match(location).Success)
                    location = response.ResponseUri.For(_ => $"{_.Scheme}://{_.Authority}{location}");

                if (!location.IsNullOrWhiteSpace() && web.RedirectTimes < web.AllowRedirectTimes)
                {
                    web.RedirectTimes++;
                    return web.GetResponse(HttpVerb.GET, MediaType.APPLICATION_X_WWW_FORM_URLENCODED, location, null, null);
                }
                else throw new WebException("Too many automatic redirections were attempted.");
            }

            return null;
        }
    }
}

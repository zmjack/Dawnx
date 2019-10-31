using Def;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace Dawnx.Net.Web.Processors
{
    public class RedirectProcessor : IProcessor
    {
        public HttpWebResponse Process(
            HttpAccess web, HttpWebResponse response,
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
                    OnRedirect?.Invoke(location);
                    web.RedirectTimes++;
                    return web.GetLastResponse(HttpVerb.GET, MimeMap.APPLICATION_X_WWW_FORM_URLENCODED, location, null, null);
                }
                else throw new WebException("Too many automatic redirections were attempted.");
            }

            return null;
        }

        public delegate void OnRedirectHandler(string location);
        public event OnRedirectHandler OnRedirect;

    }
}

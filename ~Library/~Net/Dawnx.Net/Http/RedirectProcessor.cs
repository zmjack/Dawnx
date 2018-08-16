﻿using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace Dawnx.Net.Http
{
    public class RedirectProcessor : IResponseProcessor
    {
        public HttpWebResponse Process(
            WebAccess web, HttpWebResponse response,
            string method, string url, string enctype,
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
                    return new WebAccess(web.StateContainer)
                    {
                        RedirectTimes = web.RedirectTimes + 1
                    }.GetResponse(method, location, enctype, null, null);
                }
                else throw new WebException("Too many automatic redirections were attempted.");
            }

            return null;
        }
    }
}
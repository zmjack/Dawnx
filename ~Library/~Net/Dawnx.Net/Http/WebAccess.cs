using Dawnx.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Dawnx.Net.Http
{
    public class WebAccess
    {
        static WebAccess()
        {
            //TODO: If WebClient can not run in netcore 2.1+ normally, delete it.
            AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);
        }

        public const int RECOMMENDED_BUFFER_SIZE = 256 * 1024;      // 256 KB

        public delegate void ProgressHandler(WebAccess sender, string url, long done, long length);
        public event ProgressHandler DownloadProgress;
        public event ProgressHandler UploadProgress;

        public WebRequestStateContainer StateContainer { get; private set; }
        public HashSet<IResponseProcessor> ResponseProcessors { get; private set; }
            = new HashSet<IResponseProcessor>().Self(_ => _.Add(new RedirectProcessor()));

        public WebAccess() : this(new WebRequestStateContainer()) { }
        public WebAccess(WebRequestStateContainer config)
        {
            if (config != null)
                StateContainer = config;
            else StateContainer = new WebRequestStateContainer();
        }

        public void AttachProcessor(IResponseProcessor processor) => ResponseProcessors.Add(processor);
        public void ClearProcessor() => ResponseProcessors.Clear();

        public int AllowRedirectTimes { get; set; } = 10;
        public int RedirectTimes { get; set; } = 0;

        public string Get(string url, Dictionary<string, object> updata = null)
            => ReadString(HttpVerb.GET, MediaType.APPLICATION_X_WWW_FORM_URLENCODED, url, updata, null);
        public string Post(string url, Dictionary<string, object> updata = null)
            => ReadString(HttpVerb.POST, MediaType.APPLICATION_X_WWW_FORM_URLENCODED, url, updata, null);
        public string PostJson(string url, Dictionary<string, object> updata = null)
            => ReadString(HttpVerb.POST, MediaType.APPLICATION_JSON, url, updata, null);
        public string Up(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null)
            => ReadString(HttpVerb.POST, MediaType.MULTIPART_FORM_DATA, url, updata, upfiles);

        public TRet Get<TRet>(string url, Dictionary<string, object> updata = null)
            => JsonConvert.DeserializeObject<TRet>(ReadString(HttpVerb.GET, MediaType.APPLICATION_X_WWW_FORM_URLENCODED, url, updata, null));
        public TRet Post<TRet>(string url, Dictionary<string, object> updata = null)
            => JsonConvert.DeserializeObject<TRet>(ReadString(HttpVerb.POST, MediaType.APPLICATION_X_WWW_FORM_URLENCODED, url, updata, null));
        public TRet PostJson<TRet>(string url, Dictionary<string, object> updata = null)
            => JsonConvert.DeserializeObject<TRet>(ReadString(HttpVerb.POST, MediaType.APPLICATION_JSON, url, updata, null));
        public TRet Up<TRet>(string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null)
            => JsonConvert.DeserializeObject<TRet>(ReadString(HttpVerb.POST, MediaType.MULTIPART_FORM_DATA, url, updata, upfiles));

        public void GetDownload(Stream receiver, string url, Dictionary<string, object> updata = null,
            int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => Download(receiver, HttpVerb.GET, MediaType.APPLICATION_X_WWW_FORM_URLENCODED, url, updata, null, bufferSize);
        public void PostDownload(Stream receiver, string url, Dictionary<string, object> updata = null,
            int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => Download(receiver, HttpVerb.POST, MediaType.APPLICATION_X_WWW_FORM_URLENCODED, url, updata, null, bufferSize);
        public void PostJsonDownload(Stream receiver, string url, Dictionary<string, object> updata = null,
            int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => Download(receiver, HttpVerb.POST, MediaType.APPLICATION_JSON, url, updata, null, bufferSize);
        public void UpDownload(Stream receiver, string url, Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null,
            int bufferSize = RECOMMENDED_BUFFER_SIZE)
            => Download(receiver, HttpVerb.POST, MediaType.MULTIPART_FORM_DATA, url, updata, upfiles, bufferSize);

        public void Download(
            Stream receiver,
            string method, string enctype, string url,
            Dictionary<string, object> updata,
            Dictionary<string, object> upfiles,
            int bufferSize)
        {
            using (var response = GetResponse(method, enctype, url, updata, upfiles))
            {
                long received = 0;
                using (var stream = response.GetResponseStream())
                {
                    stream.ReadProcess(bufferSize, (readTarget, buffer, readLength) =>
                    {
                        receiver.Write(buffer, 0, readLength);
                        received += readLength;
                        DownloadProgress?.Invoke(this, url, received, response.ContentLength);
                    });
                }
            }
        }

        public string ReadString(
            string method, string enctype, string url,
            Dictionary<string, object> updata,
            Dictionary<string, object> upfiles)
        {
            using (var response = GetResponse(method, enctype, url, updata, upfiles))
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public HttpWebResponse GetResponse(
            string method, string enctype, string url,
            Dictionary<string, object> updata,
            Dictionary<string, object> upfiles)
        {
            var response = GetPureResponse(method, enctype, url, updata, upfiles);

            foreach (Cookie respCookie in response.Cookies)
                StateContainer.Cookies.Add(respCookie);

            foreach (var processor in ResponseProcessors)
            {
                var processedResponse = processor.Process(this, response, method, enctype, url, updata, upfiles);

                if (processedResponse != null)
                    return processedResponse;
                else continue;
            }

            return response;
        }

        public HttpWebResponse GetPureResponse(
            string method, string enctype, string url,
            Dictionary<string, object> updata,
            Dictionary<string, object> upfiles)
        {
            method = method.ToLower();
            enctype = enctype.ToLower();

            if (updata == null)
                updata = new Dictionary<string, object>();
            if (upfiles == null)
                upfiles = new Dictionary<string, object>();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var encoding = Encoding.GetEncoding(StateContainer.Encoding);
            Stream bodyStream = null;

            switch (enctype)
            {
                default:
                case MediaType.APPLICATION_X_WWW_FORM_URLENCODED:
                    var query = new List<string>();
                    foreach (var data in updata)
                    {
                        var values = NormalizeStringValues(data.Value);
                        foreach (var value in values)
                            query.Add($"{data.Key}={HttpUtility.UrlEncode(value)}");
                    }
                    var queryString = query.Join("&");

                    if (method == HttpVerb.GET)
                    {
                        if (!url.Contains("?"))
                            url = $"{url}?{queryString}";
                        else url = $"{url}&{queryString}";
                    }
                    else if (method == HttpVerb.POST)
                    {
                        bodyStream = new MemoryStream(queryString.GetBytes(encoding));
                    }
                    break;

                case MediaType.MULTIPART_FORM_DATA:
                    var formData = new HttpFormData(encoding);
                    foreach (var data in updata)
                    {
                        var values = NormalizeStringValues(data.Value);
                        foreach (var value in values)
                            formData.AddData(data.Key, value.GetBytes(encoding));
                    }
                    foreach (var file in upfiles)
                    {
                        var values = NormalizeStringValues(file.Value);
                        foreach (var value in values)
                            formData.AddFile(file.Key, Path.GetFileName(value), new FileStream(value, FileMode.Open, FileAccess.Read));
                    }

                    bodyStream = formData.GetStream();
                    enctype = formData.ContentType;
                    break;

                case MediaType.APPLICATION_JSON:
                    bodyStream = new MemoryStream(JsonConvert.SerializeObject(updata).GetBytes(encoding));
                    break;
            }

            var request = ((HttpWebRequest)WebRequest.Create(new Uri(url))).Self(_ =>
            {
                StateContainer.Headers.IfNotNull(headers =>
                {
                    foreach (var header in StateContainer.Headers)
                        _.Headers.Add(header.Key, header.Value);
                });
                
                _.UserAgent = StateContainer.UserAgent;
                _.Method = method;
                _.Timeout = -1;
                _.UseDefaultCredentials = StateContainer.SystemLogin;
                if (StateContainer.UseProxy)
                {
                    if (!string.IsNullOrEmpty(StateContainer.ProxyAddress))
                    {
                        _.Proxy = new WebProxy
                        {
                            Address = new Uri(StateContainer.ProxyAddress),
                            Credentials = new NetworkCredential
                            {
                                UserName = StateContainer.ProxyUsername,
                                Password = StateContainer.ProxyPassword,
                            }
                        };
                    }
                }
                else _.Proxy = null;

                _.CookieContainer = StateContainer.Cookies;
            });

            if (method == HttpVerb.POST)
            {
                if (!request.Headers.AllKeys.Any(x => x == "Content-Type"))
                    request.ContentType = enctype;

                request.ContentLength = bodyStream.Length;
                using (var stream = request.GetRequestStream())
                {
                    bodyStream.WriteProcess(stream, RECOMMENDED_BUFFER_SIZE, (writeTarget, buffer, totalWrittenLength) =>
                    {
                        UploadProgress?.Invoke(this, url, totalWrittenLength, bodyStream.Length);
                    });
                }
            }

            HttpWebResponse response = null;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
                if (response == null) throw;
            }
            return response;
        }

        private static IEnumerable<string> NormalizeStringValues(object dvalue)
        {
            if (dvalue is Array)
            {
                return (dvalue as Array).OfType<object>()
                    .Select(value => value.ToString()).ToArray();
            }
            else return new[] { dvalue?.ToString() ?? "" };
        }

    }
}

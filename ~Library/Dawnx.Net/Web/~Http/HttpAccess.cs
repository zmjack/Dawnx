using Dawnx.Definition;
using Dawnx.Net.Web.Processors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Dawnx.Net.Web
{
    public partial class HttpAccess
    {
        static HttpAccess()
        {
            // Ensure WebClient runs correctly in the netcore 2.1+.
            AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);
        }

        public const int RECOMMENDED_BUFFER_SIZE = 256 * 1024;      // 256 KB

        public delegate void ProgressHandler(HttpAccess sender, string url, long done, long length);
        public event ProgressHandler DownloadProgress;
        public event ProgressHandler UploadProgress;

        public HttpStateContainer StateContainer { get; private set; }
        public LinkedList<IProcessor> ResponseProcessors { get; private set; }
            = new LinkedList<IProcessor>().Self(_ => _.AddLast(new RedirectProcessor()));

        public HttpAccess() : this(new HttpStateContainer()) { }
        public HttpAccess(HttpStateContainer config)
        {
            if (config != null)
                StateContainer = config;
            else StateContainer = new HttpStateContainer();
        }

        private LinkedListNode<IProcessor> FindProcessorNode(string processorFullName)
        {
            for (var node = ResponseProcessors.First; node != null; node = node.Next)
                if (node.Value.GetType().FullName == processorFullName) return node;
            return null;
        }

        public void AddProcessor(IProcessor processor)
        {
            var findNode = FindProcessorNode(processor.GetType().FullName);
            if (findNode is null)
                ResponseProcessors.AddLast(processor);
            else throw new ArgumentException("Each type of processor can only be added once.");
        }
        public void AddProcessorBefore<TFindProcessor>(IProcessor processor)
            where TFindProcessor : IProcessor
        {
            var findNode = FindProcessorNode(processor.GetType().FullName);
            if (findNode is null)
            {
                var targetNode = FindProcessorNode(typeof(TFindProcessor).FullName);
                if (targetNode != null)
                    ResponseProcessors.AddBefore(targetNode, new LinkedListNode<IProcessor>(processor));
                else ResponseProcessors.AddLast(processor);
            }
            else throw new ArgumentException("Each type of processor can only be added once.");
        }
        public void ClearProcessors() => ResponseProcessors.Clear();

        public int AllowRedirectTimes { get; set; } = 10;
        public int RedirectTimes { get; set; } = 0;

        public virtual bool Ready(
            string method, string enctype, string url,
            Dictionary<string, object> updata,
            Dictionary<string, object> upfiles) => true;

        public string Download(
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
                return response.Headers["Content-Disposition"];
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
                return reader.ReadToEnd();
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

        private HttpWebResponse GetPureResponse(
            string method, string enctype, string url,
            Dictionary<string, object> updata,
            Dictionary<string, object> upfiles)
        {
            if (!Ready(method, enctype, url, updata, upfiles))
                throw new WebException("Instance is not ready to request.");

            method = method.ToUpper();
            enctype = enctype.ToLower();

            if (updata is null)
                updata = new Dictionary<string, object>();
            if (upfiles is null)
                upfiles = new Dictionary<string, object>();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var encoding = Encoding.GetEncoding(StateContainer.Encoding);
            Stream bodyStream = null;

            switch (enctype)
            {
                default:
                case MimeType.APPLICATION_X_WWW_FORM_URLENCODED:
                    var query = new List<string>();
                    foreach (var data in updata)
                    {
                        var values = NormalizeStringValues(data.Value);
                        foreach (var value in values)
                            query.Add($"{data.Key}={WebUtility.UrlEncode(value)}");
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
                        bodyStream = new MemoryStream(queryString.Bytes(encoding));
                    }
                    break;

                case MimeType.MULTIPART_FORM_DATA:
                    var formData = new HttpFormData(encoding);
                    foreach (var data in updata)
                    {
                        var values = NormalizeStringValues(data.Value);
                        foreach (var value in values)
                            formData.AddData(data.Key, value.Bytes(encoding));
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

                case MimeType.APPLICATION_JSON:
                    bodyStream = new MemoryStream(JsonConvert.SerializeObject(updata).Bytes(encoding));
                    break;
            }

            var request = ((HttpWebRequest)WebRequest.Create(new Uri(url))).Self(_ =>
            {
                StateContainer.Headers.Self(headers =>
                {
                    if (!(headers is null))
                    {
                        foreach (var header in StateContainer.Headers)
                            _.Headers.Add(header.Key, header.Value);
                    }
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

                _.CookieContainer = StateContainer.Cookies;
            });

            if (method == HttpVerb.POST)
            {
                if (!request.Headers.AllKeys.Any(x => x == "Content-Type"))
                    request.ContentType = enctype;

                request.ContentLength = bodyStream.Length;
                using (var stream = request.GetRequestStream())
                {
                    bodyStream.WriteTo(stream, RECOMMENDED_BUFFER_SIZE, (writeTarget, buffer, totalWrittenLength) =>
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
                if (response is null) throw;
            }
            return response;
        }

        private static IEnumerable<string> NormalizeStringValues(object dvalue)
        {
            //TODO: This method limit the urlencode ability of urlencode, because it support only 1 level search.
            if (dvalue is Array)
            {
                return (dvalue as Array).OfType<object>()
                    .Select(value => value.ToString()).ToArray();
            }
            else return new[] { dvalue?.ToString() ?? "" };
        }

        /// <summary>
        /// Sets HttpRequestHeaders for next request.
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public HttpAccess WithHeaders(Dictionary<HttpRequestHeader, string> headers)
        {
            StateContainer.Headers.Clear();
            foreach (var header in headers)
                StateContainer.Headers.Add(header.Key.ToString(), header.Value);
            return this;
        }

        /// <summary>
        /// Sets HttpRequestHeaders for next request.
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public HttpAccess WithHeaders(Dictionary<string, string> headers)
        {
            StateContainer.Headers = headers;
            return this;
        }

        /// <summary>
        /// Sets HttpRequestHeaders to null for next request.
        /// </summary>
        /// <returns></returns>
        public HttpAccess WithNoHeaders()
        {
            StateContainer.Headers = null;
            return this;
        }

    }
}

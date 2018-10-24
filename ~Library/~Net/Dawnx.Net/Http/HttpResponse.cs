using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Dawnx;
using Dawnx.Enums;
using Dawnx.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dawnx.Net.Http
{
    //TODO: to complete this class
    public class HttpResponse
    {
        private byte[] _Origin;
        private string _Method;
        private string _Enctype;
        private string _Url;
        private Dictionary<string, object> _Updata;
        private Dictionary<string, object> _Upfiles;
        private Dictionary<string, string> _Headers;
        private int _BufferSize = WebAccess.RECOMMENDED_BUFFER_SIZE;
        private bool _IsCompleted;

        private const string ERROR_REQUEST_IS_COMPLETED = "Request is completed";

        public HttpResponse(string method, string enctype, string url)
        {
            _Method = method;
            _Enctype = enctype;
            _Url = url;
        }

        public WebAccess Access { get; set; }
        public byte[] Origin
        {
            get
            {
                if (!_IsCompleted)
                {
                    using (var memory = new MemoryStream())
                    {
                        Access.Download(memory, _Method, _Enctype, _Url, _Updata, _Upfiles, _BufferSize);
                        _Origin = memory.ToArray();
                        _IsCompleted = true;
                    }
                }

                return _Origin;
            }
        }
        public string String => Origin.String(Encoding.UTF8);
        public JToken Token => JsonConvert.DeserializeObject<JToken>(String);
        public TRet Object<TRet>() => JsonConvert.DeserializeObject<TRet>(String);

        public HttpResponse WithGet(object updata) => WithGet(ObjectUtility.CovertToDictionary(updata));
        public HttpResponse WithGet(Dictionary<string, object> updata = null)
        {
            if (_IsCompleted)
                throw new InvalidOperationException(ERROR_REQUEST_IS_COMPLETED);

            _Method = HttpVerb.GET;
            _Enctype = MediaType.APPLICATION_X_WWW_FORM_URLENCODED;
            _Updata = updata;
            _Upfiles = null;
            return this;
        }

        public HttpResponse WithPost(object updata) => WithPost(ObjectUtility.CovertToDictionary(updata));
        public HttpResponse WithPost(Dictionary<string, object> updata = null)
        {
            if (_IsCompleted)
                throw new InvalidOperationException(ERROR_REQUEST_IS_COMPLETED);

            _Method = HttpVerb.POST;
            _Enctype = MediaType.APPLICATION_X_WWW_FORM_URLENCODED;
            _Updata = updata;
            _Upfiles = null;
            return this;
        }

        public HttpResponse WithPostJson(object updata) => WithPostJson(ObjectUtility.CovertToDictionary(updata));
        public HttpResponse WithPostJson(Dictionary<string, object> updata = null)
        {
            if (_IsCompleted)
                throw new InvalidOperationException(ERROR_REQUEST_IS_COMPLETED);

            _Method = HttpVerb.POST;
            _Enctype = MediaType.APPLICATION_JSON;
            _Updata = updata;
            _Upfiles = null;
            return this;
        }

        public HttpResponse WithUp(object updata, object upfiles)
            => WithUp(ObjectUtility.CovertToDictionary(updata), ObjectUtility.CovertToDictionary(upfiles));
        public HttpResponse WithUp(Dictionary<string, object> updata = null, Dictionary<string, object> upfiles = null)
        {
            if (_IsCompleted)
                throw new InvalidOperationException(ERROR_REQUEST_IS_COMPLETED);

            _Method = HttpVerb.POST;
            _Enctype = MediaType.MULTIPART_FORM_DATA;
            _Updata = updata;
            _Upfiles = upfiles;
            return this;
        }

        public HttpResponse WithBufferSize(int size)
        {
            if (_IsCompleted)
                throw new InvalidOperationException(ERROR_REQUEST_IS_COMPLETED);

            _BufferSize = size;
            return this;
        }

        public HttpResponse WithHeaders(Dictionary<HttpRequestHeader, string> headers)
        {
            if (_IsCompleted)
                throw new InvalidOperationException(ERROR_REQUEST_IS_COMPLETED);

            _Headers = new Dictionary<string, string>();
            foreach (var header in headers)
                _Headers.Add(header.Key.ToString(), header.Value);

            return this;
        }
        public HttpResponse WithHeaders(Dictionary<string, string> headers)
        {
            if (_IsCompleted)
                throw new InvalidOperationException(ERROR_REQUEST_IS_COMPLETED);

            _Headers = headers;
            return this;
        }

    }
}

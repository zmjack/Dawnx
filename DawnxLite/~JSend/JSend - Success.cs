
using System;

namespace Dawnx
{
#pragma warning disable IDE1006

    public partial class JSend
    {
        /// <summary>
        /// All went well, and (usually) some data was returned.
        /// </summary>
        public sealed class Success : IJSend
        {
            public string status => JSendConst.SUCCESS_STATUS;

            /// <summary>
            /// Required Key:
            ///     Acts as the wrapper for any data returned by the API call.
            ///     If the call returns no data, data should be set to null.
            /// </summary>
            public dynamic data { get; set; }

            string IJSend.code { get; set; }
            string IJSend.message { get; set; }
        }

        /// <summary>
        /// All went well, and (usually) some data was returned.
        /// </summary>
        public sealed class Success<TData> : IJSend<TData>
        {
            public string status => JSendConst.SUCCESS_STATUS;

            /// <summary>
            /// Required Key:
            ///     Acts as the wrapper for any data returned by the API call.
            ///     If the call returns no data, data should be set to null.
            /// </summary>
            public TData data { get; set; }

            string IJSend<TData>.code { get; set; }
            string IJSend<TData>.message { get; set; }
        }
    }
#pragma warning restore
}


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
            public string status => SUCCESS_STATUS;

            /// <summary>
            /// Required Key:
            ///     Acts as the wrapper for any data returned by the API call.
            ///     If the call returns no data, data should be set to null.
            /// </summary>
            public object data { get; set; }

            int IJSend.code { get; set; }
            dynamic IJSend.message { get; set; }
        }
    }
#pragma warning restore
}

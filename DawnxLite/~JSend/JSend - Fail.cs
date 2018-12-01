
using System;

namespace Dawnx
{
#pragma warning disable IDE1006

    public partial class JSend
    {
        /// <summary>
        /// There was a problem with the data submitted, or some pre-condition of the API call wasn't satisfied.
        /// </summary>
        public class Fail : IJSend
        {
            public string status => JSendConst.FAIL_STATUS;

            /// <summary>
            /// Required Key:
            ///     Provides the wrapper for the details of why the request failed.
            ///     If the reasons for failure correspond to POST values,
            ///     the response object's keys SHOULD correspond to those POST values.
            /// </summary>
            public dynamic data { get; set; }

            string IJSend.code { get; set; }
            string IJSend.message { get; set; }
        }

        /// <summary>
        /// There was a problem with the data submitted, or some pre-condition of the API call wasn't satisfied.
        /// </summary>
        public class Fail<TData> : IJSend<TData>
        {
            public string status => JSendConst.FAIL_STATUS;

            /// <summary>
            /// Required Key:
            ///     Provides the wrapper for the details of why the request failed.
            ///     If the reasons for failure correspond to POST values,
            ///     the response object's keys SHOULD correspond to those POST values.
            /// </summary>
            public TData data { get; set; }

            string IJSend<TData>.code { get; set; }
            string IJSend<TData>.message { get; set; }
        }
    }
#pragma warning restore
}

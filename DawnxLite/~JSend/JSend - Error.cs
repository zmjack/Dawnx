
using System;

namespace Dawnx
{
#pragma warning disable IDE1006

    public partial class JSend
    {
        /// <summary>
        /// There was a problem with the data submitted, or some pre-condition of the API call wasn't satisfied.
        /// </summary>
        public class Error : IJSend
        {
            public string status => JSendConst.ERROR_STATUS;

            /// <summary>
            /// Required Key:
            ///     A meaningful, end-user-readable (or at the least log-worthy) message, explaining what went wrong.
            /// </summary>
            public string message { get; set; }

            /// <summary>
            /// Optional Key:
            ///     A numeric code corresponding to the error, if applicable.
            /// </summary>
            public string code { get; set; }

            /// <summary>
            /// Optional Key:
            ///     A generic container for any other information about the error,
            ///         i.e.the conditions that caused the error, stack traces, etc.
            /// </summary>
            public dynamic data { get; set; }
        }

        /// <summary>
        /// There was a problem with the data submitted, or some pre-condition of the API call wasn't satisfied.
        /// </summary>
        public class Error<TData> : IJSend<TData>
        {
            public string status => JSendConst.ERROR_STATUS;

            /// <summary>
            /// Required Key:
            ///     A meaningful, end-user-readable (or at the least log-worthy) message, explaining what went wrong.
            /// </summary>
            public string message { get; set; }

            /// <summary>
            /// Optional Key:
            ///     A numeric code corresponding to the error, if applicable.
            /// </summary>
            public string code { get; set; }

            /// <summary>
            /// Optional Key:
            ///     A generic container for any other information about the error,
            ///         i.e.the conditions that caused the error, stack traces, etc.
            /// </summary>
            public TData data { get; set; }
        }
    }
#pragma warning restore
}

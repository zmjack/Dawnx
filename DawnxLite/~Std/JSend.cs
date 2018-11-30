
namespace Dawnx
{
#pragma warning disable IDE1006

    // Refer: http://labs.omniti.com/labs/jsend
    public static class JSend
    {
        /// <summary>
        /// All went well, and (usually) some data was returned.
        /// </summary>
        public class Success
        {
            public string status => "success";

            /// <summary>
            /// Required Key:
            ///     Acts as the wrapper for any data returned by the API call.
            ///     If the call returns no data, data should be set to null.
            /// </summary>
            public object data;
        }

        /// <summary>
        /// There was a problem with the data submitted, or some pre-condition of the API call wasn't satisfied.
        /// </summary>
        public class Fail
        {
            public string status => "fail";

            /// <summary>
            /// Required Key:
            ///     Provides the wrapper for the details of why the request failed.
            ///     If the reasons for failure correspond to POST values,
            ///     the response object's keys SHOULD correspond to those POST values.
            /// </summary>
            public object data;
        }

        /// <summary>
        /// There was a problem with the data submitted, or some pre-condition of the API call wasn't satisfied.
        /// </summary>
        public class Error
        {
            public string status => "error";

            /// <summary>
            /// Required Key:
            ///     A meaningful, end-user-readable (or at the least log-worthy) message, explaining what went wrong.
            /// </summary>
            public object message;

            /// <summary>
            /// A numeric code corresponding to the error, if applicable.
            /// </summary>
            public int code;

            /// <summary>
            /// A generic container for any other information about the error,
            ///     i.e.the conditions that caused the error, stack traces, etc.
            /// </summary>
            public object data;
        }

    }
}
#pragma warning restore

namespace Dawnx
{
#pragma warning disable IDE1006

    public partial class JSend
    {
        public static class Error
        {
            public static Error<string> Create() => new Error<string>();
            public static Error<string> Create(string message) => new Error<string>(message);
            public static Error<TData> Create<TData>(string message, string code, TData data) => new Error<TData>(message, code, data);
        }

        /// <summary>
        /// There was a problem with the data submitted, or some pre-condition of the API call wasn't satisfied.
        /// </summary>
        public class Error<TData> : IJSend
        {
            public Error() { }
            public Error(string message)
            {
                this.message = message;
            }
            public Error(string message, string code, TData data)
            {
                this.message = message;
                this.code = code;
                this.data = data;
            }

            public string status => JSendConst.ERROR_STATUS;
            object IJSend.data { get; set; }

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
            public TData data
            {
                get => (TData)(this as IJSend).data;
                set => (this as IJSend).data = value;
            }

            public static implicit operator JSend<TData>(Error<TData> @this) => JSend<TData>.Parse(@this);
        }

    }
#pragma warning restore
}

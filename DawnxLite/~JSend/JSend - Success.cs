namespace Dawnx
{
#pragma warning disable IDE1006

    public partial class JSend
    {
        public static class Success
        {
            public static Success<string> Create() => new Success<string>();
            public static Success<TData> Create<TData>(TData data) => new Success<TData>(data);
        }

        /// <summary>
        /// All went well, and (usually) some data was returned.
        /// </summary>
        public class Success<TData> : IJSend
        {
            public Success() { }
            public Success(TData data)
            {
                this.data = data;
            }

            public string status => JSendConst.SUCCESS_STATUS;
            dynamic IJSend.data { get; set; }
            string IJSend.code { get; set; }
            string IJSend.message { get; set; }

            /// <summary>
            /// Required Key:
            ///     Acts as the wrapper for any data returned by the API call.
            ///     If the call returns no data, data should be set to null.
            /// </summary>
            public TData data
            {
                get => (TData)(this as IJSend).data;
                set => (this as IJSend).data = value;
            }

            public static implicit operator JSend<TData>(Success<TData> @this) => JSend<TData>.Parse(@this);
        }

    }
#pragma warning restore
}

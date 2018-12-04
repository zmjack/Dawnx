namespace Dawnx
{
#pragma warning disable IDE1006

    public partial class JSend
    {
        public static class Fail
        {
            public static Fail<string> Create() => new Fail<string>();
            public static Fail<TData> Create<TData>(TData data) => new Fail<TData>(data);
        }

        /// <summary>
        /// There was a problem with the data submitted, or some pre-condition of the API call wasn't satisfied.
        /// </summary>
        public class Fail<TData> : IJSend
        {
            public Fail() { }
            public Fail(TData data)
            {
                this.data = data;
            }

            public string status => JSendConst.FAIL_STATUS;
            dynamic IJSend.data { get; set; }
            string IJSend.code { get; set; }
            string IJSend.message { get; set; }

            /// <summary>
            /// Required Key:
            ///     Provides the wrapper for the details of why the request failed.
            ///     If the reasons for failure correspond to POST values,
            ///     the response object's keys SHOULD correspond to those POST values.
            /// </summary>
            public TData data
            {
                get => (TData)(this as IJSend).data;
                set => (this as IJSend).data = value;
            }

            public static implicit operator JSend<TData>(Fail<TData> @this) => JSend<TData>.Parse(@this);
        }

    }
#pragma warning restore
}

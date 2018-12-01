
namespace Dawnx
{
#pragma warning disable IDE1006

    // Refer: http://labs.omniti.com/labs/jsend
    public partial class JSend : IJSend
    {
        public string status { get; set; }
        public object data { get; set; }

        public string code { get; set; }
        public string message { get; set; }

        public static JSend Parse(IJSend jSend)
        {
            return new JSend
            {
                code = jSend.code,
                data = jSend.data,
                message = jSend.message,
                status = jSend.status,
            };
        }
    }

    public partial class JSend<TData> : JSend
    {
        public new TData data
        {
            get => (TData)base.data;
            set => base.data = value;
        }

        public static new JSend<TData> Parse(IJSend jSend)
        {
            return new JSend<TData>
            {
                code = jSend.code,
                data = (TData)jSend.data,
                message = jSend.message,
                status = jSend.status,
            };
        }
    }
#pragma warning restore
}
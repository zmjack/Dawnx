
namespace Dawnx
{
#pragma warning disable IDE1006

    // Refer: http://labs.omniti.com/labs/jsend
    public partial class JSend : IJSend
    {
        public string status { get; set; }
        public dynamic data { get; set; }

        public string code { get; set; }
        public string message { get; set; }

        public bool IsSuccess() => status == JSendConst.SUCCESS_STATUS;
        public bool IsFail() => status == JSendConst.FAIL_STATUS;
        public bool IsError() => status == JSendConst.ERROR_STATUS;
    }

    public partial class JSend<TData> : IJSend<TData>
    {
        public string status { get; set; }
        public TData data { get; set; }

        public string code { get; set; }
        public string message { get; set; }

        public bool IsSuccess() => status == JSendConst.SUCCESS_STATUS;
        public bool IsFail() => status == JSendConst.FAIL_STATUS;
        public bool IsError() => status == JSendConst.ERROR_STATUS;
    }
#pragma warning restore
}
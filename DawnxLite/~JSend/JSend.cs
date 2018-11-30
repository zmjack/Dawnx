
namespace Dawnx
{
#pragma warning disable IDE1006

    // Refer: http://labs.omniti.com/labs/jsend
    public partial class JSend : IJSend
    {
        public const string SUCCESS_STATUS = "success";
        public const string FAIL_STATUS = "fail";
        public const string ERROR_STATUS = "error";

        public string status { get; set; }
        public dynamic data { get; set; }

        public int code { get; set; }
        public dynamic message { get; set; }

        public bool IsSuccess() => status == SUCCESS_STATUS;
        public bool IsFail() => status == SUCCESS_STATUS;
        public bool IsError() => status == SUCCESS_STATUS;
    }
#pragma warning restore
}

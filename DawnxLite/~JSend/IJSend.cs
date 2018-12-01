
namespace Dawnx
{
#pragma warning disable IDE1006    
    public interface IJSend
    {
        string status { get; }
        object data { get; set; }

        string code { get; set; }
        string message { get; set; }
    }

    public static class IJSendExtension
    {
        public static bool IsSuccess(this IJSend @this) => @this.status == JSendConst.SUCCESS_STATUS;
        public static bool IsFail(this IJSend @this) => @this.status == JSendConst.FAIL_STATUS;
        public static bool IsError(this IJSend @this) => @this.status == JSendConst.ERROR_STATUS;
    }
#pragma warning restore
}

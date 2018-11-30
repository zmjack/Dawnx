
namespace Dawnx
{
#pragma warning disable IDE1006

    // Refer: http://labs.omniti.com/labs/jsend
    public interface IJSend
    {
        string status { get; }
        dynamic data { get; set; }

        int code { get; set; }
        dynamic message { get; set; }
    }
#pragma warning restore
}

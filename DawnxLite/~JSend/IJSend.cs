
namespace Dawnx
{
#pragma warning disable IDE1006    
    public interface IJSend
    {
        string status { get; }
        dynamic data { get; set; }

        string code { get; set; }
        string message { get; set; }
    }

    public interface IJSend<TData>
    {
        string status { get; }
        TData data { get; set; }

        string code { get; set; }
        string message { get; set; }
    }
#pragma warning restore
}

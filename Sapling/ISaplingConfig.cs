using Dawnx;

namespace Sapling
{
    public interface ISaplingConfig { }

    public static class IJSendWrapperExtension
    {
        public static JSend ToJSend(this ISaplingConfig @this)
        {
            return JSend.Success.Create(@this);
        }
    }

}

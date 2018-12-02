using Dawnx;
using System;
using System.Collections.Generic;
using System.Text;

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

using Dawnx;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sapling
{
    public static class SpUploadBox
    {
        public static JSend JSendConfig(string statUrl, string uploadUrl)
        {
            return JSend.Success.Create(new { statUrl, uploadUrl });
        }
    }
}

using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using System.Text;

namespace Dawnx.AspNetCore
{
    public partial class ZipStream
    {
        static ZipStream()
        {
            ZipConstants.DefaultCodePage = 0;
        }

    }
}

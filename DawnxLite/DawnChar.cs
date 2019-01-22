using Dawnx.Data;
using System;

namespace Dawnx
{
    public static class DawnChar
    {
        public static int ConsoleWidth(this char @this) => @this < 0x100 ? 1 : 2;

    }
}

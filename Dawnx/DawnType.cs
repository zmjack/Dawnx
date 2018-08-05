using System;
using System.Linq;
using System.Reflection;

namespace Dawnx
{
    public static class DawnType
    {
        public static MethodInfo GetMethodViaFormatName(this Type @this, string formatName)
        {
            return @this.GetMethods().First(x => x.ToString() == formatName);
        }
    }
}

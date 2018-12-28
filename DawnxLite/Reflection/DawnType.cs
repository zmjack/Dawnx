using System;
using System.Linq;
using System.Reflection;

namespace Dawnx.Reflection
{
    public static class Reflection_DawnType
    {
        public static MethodInfo GetMethodViaQualifiedName(this Type @this, string formatName)
        {
            return @this.GetMethods().First(x => x.ToString() == formatName);
        }
    }
}

using System;
using System.Linq;
using System.Reflection;

namespace Dawnx
{
    public static class DawnICustomAttributeProvider
    {
        public static bool IsMarkedAs<T>(this ICustomAttributeProvider @this, bool inherit = true)
            => @this.GetCustomAttributes(typeof(T), inherit).Any();

        public static bool IsMarkedAs(this ICustomAttributeProvider @this, Type attribute, bool inherit = true)
            => @this.GetCustomAttributes(attribute, inherit).Any();

    }
}

using System.ComponentModel;
using System.Linq;

namespace System.Reflection
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XICustomAttributeProvider
    {
        public static bool IsMarkedAs<T>(this ICustomAttributeProvider @this, bool inherit = true)
            => @this.GetCustomAttributes(typeof(T), inherit).Any();

        public static bool IsMarkedAs(this ICustomAttributeProvider @this, Type attribute, bool inherit = true)
            => @this.GetCustomAttributes(attribute, inherit).Any();

    }
}

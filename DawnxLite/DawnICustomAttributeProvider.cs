using System.Linq;
using System.Reflection;

namespace Dawnx
{
    public static class DawnICustomAttributeProvider
    {
        public static bool HasCustomAttribute<T>(this ICustomAttributeProvider @this, bool inherit = true)
            => @this.GetCustomAttributes(typeof(T), inherit).Any();

    }
}

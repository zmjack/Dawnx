using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Dawnx.Utilities
{
    public static class DataAnnotationUtility
    {
        public static string GetDisplayName(MemberInfo memberInfo, bool inherit = true)
        {
            var attr_DispalyName = memberInfo.GetCustomAttribute<DisplayNameAttribute>(inherit);
            if (attr_DispalyName != null)
                return attr_DispalyName.DisplayName;

            var attr_Dispaly = memberInfo.GetCustomAttribute<DisplayAttribute>(inherit);
            if (attr_Dispaly != null) return attr_Dispaly.Name;

            return memberInfo.Name;
        }
    }
}

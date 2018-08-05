using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Dawnx
{
    public static class DawnEnum
    {
        public static string DisplayName(this Enum @this)
        {
            var field = @this.GetType().GetFields().First(x => x.Name == @this.ToString());

            var displayNameAttr = field.GetCustomAttribute<DisplayNameAttribute>();
            if (displayNameAttr != null) return displayNameAttr.DisplayName;

            var displayAttr = field.GetCustomAttribute<DisplayAttribute>();
            if (displayAttr != null) return displayAttr.Name;

            return @this.ToString();
        }

    }
}

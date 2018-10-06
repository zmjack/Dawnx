using System;
using System.Linq;

namespace Dawnx
{
    public static class DawnEnum
    {
        public static string DisplayName(this Enum @this)
        {
            var field = @this.GetType().GetFields().First(x => x.Name == @this.ToString());
            return DataAnnotationUtility.GetDisplayNameFromAttribute(field);
        }

    }
}

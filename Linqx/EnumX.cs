using Linqx;
using System;
using System.Linq;

namespace Linqx
{
    public static class EnumX
    {
        public static string DisplayName(this Enum @this)
        {
            var field = @this.GetType().GetFields().First(x => x.Name == @this.ToString());
            return DataAnnotationUtility.GetDisplayName(field);
        }

    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Dawnx.AspNetCore
{
    public class ValueTextPair
    {
        public object Value { get; set; }
        public string Text { get; set; }
    }

    public static class DawnKeyValuePairEnumerable
    {
        public static SelectList ToSelectList(this IEnumerable<ValueTextPair> @this)
            => new SelectList(@this, nameof(ValueTextPair.Value), nameof(ValueTextPair.Text));

        public static SelectList ToSelectList(this IEnumerable<ValueTextPair> @this, object selectedValue)
            => new SelectList(@this, nameof(ValueTextPair.Value), nameof(ValueTextPair.Text), selectedValue);
    }

}

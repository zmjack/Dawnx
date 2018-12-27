using System.Collections;
using System.Collections.Generic;

namespace Dawnx
{
    public static class DawnEnumerator
    {
        public static TElement TakeElement<TElement>(this IEnumerator<TElement> @this)
        {
            if (@this.MoveNext())
                return @this.Current;
            else return default(TElement);
        }
        
        public static object TakeElement(this IEnumerator @this)
        {
            if (@this.MoveNext())
                return @this.Current;
            else return null;
        }

    }
}

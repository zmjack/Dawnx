using System.ComponentModel;

namespace System.Collections.Generic
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XEnumerator
    {
        public static TElement TakeElement<TElement>(this IEnumerator<TElement> @this)
        {
            if (@this.MoveNext())
                return @this.Current;
            else return default;
        }

    }
}

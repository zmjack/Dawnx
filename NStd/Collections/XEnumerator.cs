using System.ComponentModel;

namespace System.Collections
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XEnumerator
    {
        public static object TakeElement(this IEnumerator @this)
        {
            if (@this.MoveNext())
                return @this.Current;
            else return null;
        }

    }
}

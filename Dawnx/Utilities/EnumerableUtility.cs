using System.Collections.Generic;

namespace Dawnx.Utilities
{
    public static class EnumerableUtility
    {
        public static IEnumerable<T> Combine<T>(params IEnumerable<T>[] enumerables)
        {
            foreach (var enumerable in enumerables)
                foreach (var item in enumerable)
                    yield return item;
        }
    }
}

using System.Collections.Generic;

namespace Dawnx.Ranges
{
    public interface IRange<T> : IEnumerable<T>
    {
        T GetValue(int index);
    }
}

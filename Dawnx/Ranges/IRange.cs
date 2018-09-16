using Dawnx.Sequences;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx.Ranges
{
    public interface IRange<T> : IEnumerable<T>
    {
        T GetValue(int index);
    }
}

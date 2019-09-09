using System;
using System.Collections.Generic;
using System.Text;

namespace NLinq
{
    [Flags]
    public enum NLinqAnnotation
    {
        Index = 1,
        Provider = 2,
        CompositeKey = 4,
        All = Index | Provider | CompositeKey,
    }
}

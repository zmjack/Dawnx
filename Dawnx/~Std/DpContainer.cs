using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Dawnx
{
    public abstract partial class DpContainer<TIn, TOut>
    {
        public abstract TOut StateTransfer(TIn x);
    }
}

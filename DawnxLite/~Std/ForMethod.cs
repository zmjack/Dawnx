using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx
{
    public class ForMethod<TModel, TRet>
    {
        public Func<TRet, bool> Condition { get; set; } = _ => !((object)_ is null);
        public Func<TModel, TRet>[] Methods { get; set; }
        public Func<TModel, TRet> DefaultReturn { get; set; } = _ => default(TRet);
    }

}

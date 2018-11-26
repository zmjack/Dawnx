using System;
using System.Collections.Generic;
using System.Text;

namespace Dawnx
{
    public class ForMethod<TModel, TRet>
    {
        public static Func<TRet, bool> DefaultConditionMethod = _ => !((object)_ is null);

        public Func<TRet, bool> Condition { get; set; } = DefaultConditionMethod;
        public Func<TModel, TRet>[] Methods { get; set; }
        public TRet DefaultReturn { get; set; } = default(TRet);
    }

}

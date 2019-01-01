using Dawnx.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIEnumerable
    {
        public static IEnumerable<TEntity> WhereBefore<TEntity>(this IEnumerable<TEntity> @this,
            Expression<Func<TEntity, DateTime>> memberExp,
            Expression<Func<TEntity, DateTime>> beforeExp,
            bool includePoint = false)
            => @this.WhereStrategy(new WhereBeforeStrategy<TEntity>(memberExp, beforeExp, includePoint));

        public static IEnumerable<TEntity> WhereBefore<TEntity>(this IEnumerable<TEntity> @this,
            Expression<Func<TEntity, DateTime>> memberExp,
            DateTime before,
            bool includePoint = false)
            => @this.WhereStrategy(new WhereBeforeStrategy<TEntity>(memberExp, before, includePoint));
        
    }
}

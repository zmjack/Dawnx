using Dawnx.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIEnumerable
    {
        public static IEnumerable<TEntity> WhereAfter<TEntity>(this IEnumerable<TEntity> @this,
            Expression<Func<TEntity, DateTime>> memberExp,
            Expression<Func<TEntity, DateTime>> afterExp,
            bool includePoint = false)
            => @this.WhereStrategy(new WhereAfterStrategy<TEntity>(memberExp, afterExp, includePoint));

        public static IEnumerable<TEntity> WhereAfter<TEntity>(this IEnumerable<TEntity> @this,
            Expression<Func<TEntity, DateTime>> memberExp,
            DateTime after,
            bool includePoint = false)
            => @this.WhereStrategy(new WhereAfterStrategy<TEntity>(memberExp, after, includePoint));
        
    }
}

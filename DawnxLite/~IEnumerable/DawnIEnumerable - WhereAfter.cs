using Dawnx.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIEnumerable
    {
        public static IEnumerable<TEntity> WhereAfter<TEntity>(this IEnumerable<TEntity> @this,
            Expression<Func<TEntity, DateTime>> memberExp,
            Expression<Func<TEntity, DateTime>> afterExp,
            bool includePoint = true)
            => @this.WhereStrategy(new WhereAfterStrategy<TEntity>(memberExp, afterExp, includePoint));

        public static IEnumerable<TEntity> WhereAfter<TEntity>(this IEnumerable<TEntity> @this,
            Expression<Func<TEntity, DateTime>> memberExp,
            DateTime after,
            bool includePoint = true)
            => @this.WhereStrategy(new WhereAfterStrategy<TEntity>(memberExp, after, includePoint));

        public static IEnumerable<TEntity> WhereAfter<TEntity>(this IEnumerable<TEntity> @this,
            Expression<Func<TEntity, DateTime?>> memberExp,
            Expression<Func<TEntity, DateTime>> afterExp,
            bool liftNullToTrue, bool includePoint = true)
            => @this.WhereStrategy(new WhereAfterStrategy<TEntity>(memberExp, afterExp, liftNullToTrue, includePoint));

        public static IEnumerable<TEntity> WhereAfter<TEntity>(this IEnumerable<TEntity> @this,
            Expression<Func<TEntity, DateTime?>> memberExp,
            DateTime after,
            bool liftNullToTrue, bool includePoint = true)
            => @this.WhereStrategy(new WhereAfterStrategy<TEntity>(memberExp, after, liftNullToTrue, includePoint));

        public static IEnumerable<TEntity> WhereAfter<TEntity>(this IEnumerable<TEntity> @this,
            Expression<Func<TEntity, object>> yearExp,
            Expression<Func<TEntity, object>> monthExp,
            Expression<Func<TEntity, object>> dayExp,
            DateTime after,
            bool includePoint = true)
            => @this.WhereStrategy(new WhereAfterStrategy<TEntity>(yearExp, monthExp, dayExp, after, includePoint));

    }
}

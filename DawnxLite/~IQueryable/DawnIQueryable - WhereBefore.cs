using Dawnx.Linq;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIQueryable
    {
        public static IQueryable<TEntity> WhereBefore<TEntity>(this IQueryable<TEntity> @this,
            Expression<Func<TEntity, DateTime>> memberExp,
            Expression<Func<TEntity, DateTime>> beforeExp,
            bool includePoint = true)
            => @this.WhereStrategy(new WhereBeforeStrategy<TEntity>(memberExp, beforeExp, includePoint));

        public static IQueryable<TEntity> WhereBefore<TEntity>(this IQueryable<TEntity> @this,
            Expression<Func<TEntity, DateTime>> memberExp,
            DateTime before,
            bool includePoint = true)
            => @this.WhereStrategy(new WhereBeforeStrategy<TEntity>(memberExp, before, includePoint));

        public static IQueryable<TEntity> WhereBefore<TEntity>(this IQueryable<TEntity> @this,
            Expression<Func<TEntity, object>> yearExp,
            Expression<Func<TEntity, object>> monthExp,
            Expression<Func<TEntity, object>> dayExp,
            DateTime before,
            bool includePoint = true)
            => @this.WhereStrategy(new WhereBeforeStrategy<TEntity>(yearExp, monthExp, dayExp, before, includePoint));

    }
}

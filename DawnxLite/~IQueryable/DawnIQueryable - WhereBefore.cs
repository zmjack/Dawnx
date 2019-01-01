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
            bool includePoint = false)
            => @this.WhereStrategy(new WhereBeforeStrategy<TEntity>(memberExp, beforeExp, includePoint));

        public static IQueryable<TEntity> WhereBefore<TEntity>(this IQueryable<TEntity> @this,
            Expression<Func<TEntity, DateTime>> memberExp,
            DateTime before,
            bool includePoint = false)
            => @this.WhereStrategy(new WhereBeforeStrategy<TEntity>(memberExp, before, includePoint));
        
    }
}

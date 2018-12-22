using Dawnx.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIEnumerable
    {
        /// <summary>
        /// Use a WhereStragtegy to generate a where expression.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="this"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static IEnumerable<TEntity> WhereStrategy<TEntity>(this IEnumerable<TEntity> @this,
            IWhereStrategy<TEntity> strategy)
            => @this.Where(strategy.StrategyExpression.Compile());

        public static IEnumerable<TEntity> WhereSearch<TEntity>(this IEnumerable<TEntity> @this,
            string searchString,
            Expression<Func<TEntity, object>> searchMembers)
            => @this.WhereStrategy(new WhereSearchStrategy<TEntity>(searchString, searchMembers));

        public static IEnumerable<TEntity> WhereMatch<TEntity>(this IEnumerable<TEntity> @this,
            string searchString,
            Expression<Func<TEntity, object>> searchMembers)
            => @this.WhereStrategy(new WhereMatchStrategy<TEntity>(searchString, searchMembers));

        public static IEnumerable<TEntity> WhereBetween<TEntity>(this IEnumerable<TEntity> @this,
            Expression<Func<TEntity, DateTime>> memberExp,
            Expression<Func<TEntity, DateTime>> startExp,
            Expression<Func<TEntity, DateTime>> endExp)
            => @this.WhereStrategy(new WhereBetweenStrategy<TEntity>(memberExp, startExp, endExp));

        public static IEnumerable<TEntity> WhereBetween<TEntity>(this IEnumerable<TEntity> @this,
            Expression<Func<TEntity, DateTime>> memberExp,
            DateTime start,
            Expression<Func<TEntity, DateTime>> endExp)
            => @this.WhereStrategy(new WhereBetweenStrategy<TEntity>(memberExp, start, endExp));

        public static IEnumerable<TEntity> WhereBetween<TEntity>(this IEnumerable<TEntity> @this,
            Expression<Func<TEntity, DateTime>> memberExp,
            Expression<Func<TEntity, DateTime>> startExp,
            DateTime end)
            => @this.WhereStrategy(new WhereBetweenStrategy<TEntity>(memberExp, startExp, end));

        public static IEnumerable<TEntity> WhereBetween<TEntity>(this IEnumerable<TEntity> @this,
            Expression<Func<TEntity, DateTime>> memberExp,
            DateTime start,
            DateTime end)
            => @this.WhereStrategy(new WhereBetweenStrategy<TEntity>(memberExp, start, end));

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

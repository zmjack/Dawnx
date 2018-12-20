using Dawnx.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIQueryable
    {
        /// <summary>
        /// Use an OrderByStrategy to generate an orberby expression.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="this"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static IOrderedQueryable<TEntity> OrderByCaseStrategy<TEntity>(this IQueryable<TEntity> @this, IOrderStrategy<TEntity> strategy)
            => @this.OrderBy(strategy.StrategyExpression);

        public static IOrderedQueryable<TEntity> OrderByCase<TEntity>(this IQueryable<TEntity> @this,
            Expression<Func<TEntity, string>> memberExp,
            string[] orderValues)
            => @this.OrderByCaseStrategy(new OrderByCaseStrategy<TEntity>(memberExp, orderValues));

        /// <summary>
        /// Use an OrderByStrategy to generate an orberby expression.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="this"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static IOrderedQueryable<TEntity> OrderByCaseDescendingStrategy<TEntity>(this IQueryable<TEntity> @this, IOrderStrategy<TEntity> strategy)
            => @this.OrderByDescending(strategy.StrategyExpression);

        public static IOrderedQueryable<TEntity> OrderByCaseDescending<TEntity>(this IQueryable<TEntity> @this,
            Expression<Func<TEntity, string>> memberExp,
            string[] orderValues)
            => @this.OrderByCaseDescendingStrategy(new OrderByCaseStrategy<TEntity>(memberExp, orderValues));

        /// <summary>
        /// Use a WhereStragtegy to generate a where expression.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="this"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> WhereStrategy<TEntity>(this IQueryable<TEntity> @this,
            IWhereStrategy<TEntity> strategy)
            => @this.Where(strategy.StrategyExpression);

        public static IQueryable<TEntity> WhereSearch<TEntity>(this IQueryable<TEntity> @this,
            string searchString,
            Expression<Func<TEntity, object>> searchMembers)
            => @this.WhereStrategy(new WhereSearchStrategy<TEntity>(searchString, searchMembers));

        public static IQueryable<TEntity> WhereMatch<TEntity>(this IQueryable<TEntity> @this,
            string searchString,
            Expression<Func<TEntity, object>> searchMembers)
            => @this.WhereStrategy(new WhereMatchStrategy<TEntity>(searchString, searchMembers));

        public static IQueryable<TEntity> WhereBetween<TEntity>(this IQueryable<TEntity> @this,
            Expression<Func<TEntity, DateTime>> memberExp,
            Expression<Func<TEntity, DateTime>> startExp,
            Expression<Func<TEntity, DateTime>> endExp)
            => @this.WhereStrategy(new WhereBetweenStrategy<TEntity>(memberExp, startExp, endExp));

        public static IQueryable<TEntity> WhereBetween<TEntity>(this IQueryable<TEntity> @this,
            Expression<Func<TEntity, DateTime>> memberExp,
            DateTime start,
            Expression<Func<TEntity, DateTime>> endExp)
            => @this.WhereStrategy(new WhereBetweenStrategy<TEntity>(memberExp, start, endExp));

        public static IQueryable<TEntity> WhereBetween<TEntity>(this IQueryable<TEntity> @this,
            Expression<Func<TEntity, DateTime>> memberExp,
            Expression<Func<TEntity, DateTime>> startExp,
            DateTime end)
            => @this.WhereStrategy(new WhereBetweenStrategy<TEntity>(memberExp, startExp, end));

        public static IQueryable<TEntity> WhereBetween<TEntity>(this IQueryable<TEntity> @this,
            Expression<Func<TEntity, DateTime>> memberExp,
            DateTime start,
            DateTime end)
            => @this.WhereStrategy(new WhereBetweenStrategy<TEntity>(memberExp, start, end));

        public static IQueryable<TEntity> WhereAfter<TEntity>(this IQueryable<TEntity> @this,
            Expression<Func<TEntity, DateTime>> memberExp,
            Expression<Func<TEntity, DateTime>> afterExp,
            bool includePoint = false)
            => @this.WhereStrategy(new WhereAfterStrategy<TEntity>(memberExp, afterExp, includePoint));

        public static IQueryable<TEntity> WhereAfter<TEntity>(this IQueryable<TEntity> @this,
            Expression<Func<TEntity, DateTime>> memberExp,
            DateTime after,
            bool includePoint = false)
            => @this.WhereStrategy(new WhereAfterStrategy<TEntity>(memberExp, after, includePoint));

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

        /// <summary>
        /// Projects page elements of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="pageNumber">'pageNumber' starts at 1</param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PagedQueryable<TSource> SelectPage<TSource>(this IQueryable<TSource> @this, int pageNumber, int pageSize)
            => new PagedQueryable<TSource>(@this, pageNumber, pageSize);

        /// <summary>
        /// Calculates the max page number through the specified page size.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static int PageCount<TSource>(this IQueryable<TSource> @this, int pageSize)
            => (int)Math.Ceiling((double)@this.Count() / pageSize);

        public static IQueryable<TSource> WhereNot<TSource>(this IQueryable<TSource> @this, Expression<Func<TSource, bool>> predicate)
            => @this.Where(Expression.Lambda<Func<TSource, bool>>(Expression.Not(predicate.Body), predicate.Parameters));

    }

}

using System;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIQueryable
    {
        /// <summary>
        /// Use a WhereStragtegy to generate a where expression.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="this"></param>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> WhereStrategy<TEntity>(this IQueryable<TEntity> @this, IWhereStrategy<TEntity> strategy)
            => @this.Where(strategy.StrategyExpression);

        public static IQueryable<TEntity> WhereMatch<TEntity>(this IQueryable<TEntity> @this, string searchString, Expression<Func<TEntity, object>> searchMembers)
            => @this.WhereStrategy(new MatchStrategy<TEntity>(searchString, searchMembers));

        public static IQueryable<TEntity> WhereSearch<TEntity>(this IQueryable<TEntity> @this, string searchString, Expression<Func<TEntity, object>> searchMembers)
            => @this.WhereStrategy(new SearchStrategy<TEntity>(searchString, searchMembers));

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
    }

}

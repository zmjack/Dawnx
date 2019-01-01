using Dawnx.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIEnumerable
    {
        public static IEnumerable<TEntity> WhereSearch<TEntity>(this IEnumerable<TEntity> @this,
            string searchString,
            Expression<Func<TEntity, object>> searchMembers)
            => @this.WhereStrategy(new WhereSearchStrategy<TEntity>(searchString, searchMembers));

        public static IEnumerable<TEntity> WhereMatch<TEntity>(this IEnumerable<TEntity> @this,
            string searchString,
            Expression<Func<TEntity, object>> searchMembers)
            => @this.WhereStrategy(new WhereMatchStrategy<TEntity>(searchString, searchMembers));

    }
}

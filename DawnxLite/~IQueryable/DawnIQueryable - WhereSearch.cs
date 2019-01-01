﻿using Dawnx.Linq;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIQueryable
    {
        public static IQueryable<TEntity> WhereSearch<TEntity>(this IQueryable<TEntity> @this,
            string searchString,
            Expression<Func<TEntity, object>> searchMembers)
            => @this.WhereStrategy(new WhereSearchStrategy<TEntity>(searchString, searchMembers));

        public static IQueryable<TEntity> WhereMatch<TEntity>(this IQueryable<TEntity> @this,
            string searchString,
            Expression<Func<TEntity, object>> searchMembers)
            => @this.WhereStrategy(new WhereMatchStrategy<TEntity>(searchString, searchMembers));

    }
}

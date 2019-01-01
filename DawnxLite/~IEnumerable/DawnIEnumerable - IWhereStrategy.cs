﻿using Dawnx.Linq;
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

    }
}

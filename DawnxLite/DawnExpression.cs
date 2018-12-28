﻿using Dawnx.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Dawnx
{
    public static class DawnExpression
    {
        public static TExpression RebindParameter<TExpression>(this TExpression @this, ParameterExpression origin, ParameterExpression target)
            where TExpression : Expression
            => new ExpressionRebindVisitor(origin, target).Visit(@this) as TExpression;

        public static TLambdaExpression LambdaJoin<TLambdaExpression>(this IEnumerable<TLambdaExpression> @this, Func<Expression, Expression, BinaryExpression> binary)
            where TLambdaExpression : LambdaExpression
        {
            var parameter = @this.First().Parameters[0];

            return Expression.Lambda(@this.Aggregate(null as Expression, (acc, exp) =>
           {
               if (acc is null)
                   return exp.Body;
               else
               {
                   var rebindExp = RebindParameter(exp, exp.Parameters[0], parameter);
                   return binary(acc, rebindExp.Body);
               }
           }), parameter) as TLambdaExpression;
        }
    }

}
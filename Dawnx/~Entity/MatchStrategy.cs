using Dawnx.Reflection;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public class MatchStrategy<TEntity> : WhereStrategy<TEntity>
    {
        public MatchStrategy(string searchString, Expression<Func<TEntity, object>> searchMembers)
            : base((leftExp, rightExp) => leftExp.For(_ =>
            {
                if (_.Type == typeof(string))
                    return Expression.Call(leftExp, typeof(string).GetMethod(nameof(string.Equals), new[] { typeof(string) }), rightExp);
                else if (_.Type.GetInterface(typeof(IEnumerable).FullName) != null)
                {
                    var parameter = Expression.Parameter(typeof(string));
                    var anyMethod = typeof(Enumerable)
                        .GetMethodViaFormatName("Boolean Any[TSource](System.Collections.Generic.IEnumerable`1[TSource], System.Func`2[TSource,System.Boolean])")
                        .MakeGenericMethod(typeof(string));
                    var lambda = Expression.Lambda<Func<string, bool>>(
                        Expression.Call(parameter, typeof(string).GetMethod(nameof(string.Equals), new[] { typeof(string) }), rightExp), parameter);

                    return Expression.Call(anyMethod, leftExp, lambda);
                }
                else throw new NotSupportedException();
            }), searchString ?? "", searchMembers)
        {
        }

    }
}

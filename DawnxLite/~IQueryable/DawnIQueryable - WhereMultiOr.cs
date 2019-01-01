using Dawnx;
using Dawnx.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIQueryable
    {
        public static IQueryable<TSource> WhereMultiOr<TSource>(this IQueryable<TSource> @this,
            Func<IQueryable<TSource>, IEnumerable<object>> getGroupSelectArray_AnonSource)
        {
            var groupSelectArray_AnonSource = getGroupSelectArray_AnonSource(@this).ToArray();
            var parameter = Expression.Parameter(typeof(TSource));

            var whereExp = groupSelectArray_AnonSource.Select(group =>
            {
                var dict = ObjectUtility.GetPropertyPureDictionary(group);
                return dict.Keys.Select(key =>
                {
                    var value = dict[key];

                    var nullable_left = BasicTypeUtility.IsNullableType(Expression.Property(parameter, key).Type);
                    var nullable_right = BasicTypeUtility.IsNullableType(value);

                    if (nullable_left || nullable_right)
                    {
                        return Expression.Lambda<Func<TSource, bool>>(
                            Expression.Equal(
                                Expression.Property(parameter, key),
                                Expression.Convert(Expression.Constant(value), ObjectUtility.GetNullableType(value))),
                            parameter);
                    }
                    else
                    {
                        return Expression.Lambda<Func<TSource, bool>>(
                            Expression.Equal(
                                Expression.Property(parameter, key),
                                Expression.Constant(value)),
                            parameter);
                    }
                }).LambdaJoin(Expression.AndAlso);
            }).LambdaJoin(Expression.OrElse);

            return @this.Where(whereExp);
        }

    }
}
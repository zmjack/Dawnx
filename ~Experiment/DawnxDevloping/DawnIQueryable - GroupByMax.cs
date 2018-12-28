using Dawnx;
using Dawnx.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DawnxDevloping
{
    public static partial class DawnIQueryable
    {
        public class GroupInfo<TKey, TSelector>
        {
            public TKey __key__ { get; set; }
            public TSelector __selector__ { get; set; }
        }

        private static IQueryable<TSource> _WhereGroup<TSource, TKey, TSelector>(this IQueryable<TSource> source,
            Expression<Func<TSource, TKey>> keySelector,
            GroupInfo<TKey, TSelector>[] groupInfos, string selectorName)
        {
            var parameter = Expression.Parameter(typeof(TSource));

            throw new NotSupportedException();
            //var body = groupInfos.Select(g =>
            //{
            //    IEnumerable<KeyValuePair<string, object>> GetLinePropValuePairs(object instance)
            //    {
            //        var props = instance.GetType().GetProperties();
            //        foreach (var prop in props)
            //        {
            //            if (prop.PropertyType.BaseType == typeof(ValueType))
            //            {
            //                if (prop.Name == nameof(GroupInfo<TKey, TSelector>.__selector__))
            //                    yield return KeyValuePair.Create(selectorName, prop.GetValue(instance));
            //                else yield return KeyValuePair.Create(prop.Name, prop.GetValue(instance));
            //            }
            //            else
            //            {
            //                foreach (var pair in GetLinePropValuePairs(prop.PropertyType))
            //                    yield return pair;
            //            }
            //        }
            //    }
            //    return new Dictionary<string, object>(GetLinePropValuePairs(g));
            //}).Select(dict =>
            //{
            //    return dict.Keys.Select(key =>
            //    {
            //        return Expression.Equal(
            //            Expression.Property(parameter, key),
            //            Expression.Constant(dict[key]));
            //    }).JoinBinary(Expression.AndAlso);
            //}).JoinBinary(Expression.OrElse);

            //return source.Where(Expression.Lambda<Func<TSource, bool>>(body, parameter));
        }

        public static IQueryable<TSource> WhereGroupMax<TSource, TKey, TSelector>(this IQueryable<TSource> source,
            Expression<Func<TSource, TKey>> keySelector,
            Expression<Func<TSource, TSelector>> selector)
        {
            var selectorName = (selector.Body as MemberExpression).Member.Name;
            var groups = source.GroupBy(keySelector);

            var compiledKeySelector = keySelector.Compile();
            var compiledSelector = selector.Compile();

            throw new NotSupportedException();

            switch (typeof(TSelector).FullName)
            {
                //case BasicTypeUtility.@double:
                //    {
                //        dynamic typed_select = (selector as Expression<Func<TSource, int>>).Compile();
                //        return _WhereGroup(source, keySelector, groups.Select(x =>
                //        {
                //            return new GroupInfo<TKey, double>
                //            {
                //                __key__ = x.Key,
                //                __selector__ = x.Max<TSource, double>(typed_select),
                //            };
                //        }).ToArray(), selectorName);
                //    }

                    //case BasicTypeUtility.@long:
                    //    return _WhereGroup(source, groups.Select(x =>
                    //    {
                    //        return new GroupInfo<TKey, long>
                    //        {
                    //            __key__ = x.Key,
                    //            __selector__ = x.Max(compiledSelector as Func<TSource, long>),
                    //        };
                    //    }).ToArray(), selectorName);

                    //case BasicTypeUtility.@float:
                    //    return _WhereGroup(source, groups.Select(x =>
                    //    {
                    //        return new GroupInfo<TKey, float>
                    //        {
                    //            __key__ = x.Key,
                    //            __selector__ = (x as IGrouping<TKey, TSource>).Max(compiledSelector as Func<TSource, float>),
                    //        };
                    //    }).ToArray(), selectorName);

                    //case BasicTypeUtility.@double:
                    //    return _WhereGroup(source, groups.Select(x =>
                    //    {
                    //        return new GroupInfo<TKey, double>
                    //        {
                    //            __key__ = x.Key,
                    //            __selector__ = (x as IGrouping<TKey, TSource>).Max(compiledSelector as Func<TSource, double>),
                    //        };
                    //    }).ToArray(), selectorName);

                    //case BasicTypeUtility.@decimal:
                    //    return _WhereGroup(source, groups.Select(x =>
                    //    {
                    //        return new GroupInfo<TKey, decimal>
                    //        {
                    //            __key__ = x.Key,
                    //            __selector__ = (x as IGrouping<TKey, TSource>).Max(compiledSelector as Func<TSource, decimal>),
                    //        };
                    //    }).ToArray(), selectorName);

                    //default:
                    //    return _WhereGroup(source, groups.Select(x =>
                    //    {
                    //        return new GroupInfo<TKey, TSelector>
                    //        {
                    //            __key__ = x.Key,
                    //            __selector__ = (x as IGrouping<TKey, TSource>).Max(compiledSelector as Func<TSource, TSelector>),
                    //        };
                    //    }).ToArray(), selectorName);
            }
        }

    }
}

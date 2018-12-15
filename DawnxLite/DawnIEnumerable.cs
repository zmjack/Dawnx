using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dawnx
{
    public static partial class DawnIEnumerable
    {
        /// <summary>
        /// Do action for each item.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Each<TSource>(this IEnumerable<TSource> @this, Action<TSource> task)
        {
            foreach (var item in @this)
                task(item);
            return @this;
        }

        /// <summary>
        /// Do action for each item.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static IEnumerable Each<TSource>(IEnumerable @this, Action<TSource> task)
        {
            foreach (TSource item in @this)
                task(item);
            return @this;
        }

        /// <summary>
        /// Do action for each item.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Each<TSource>(this IEnumerable<TSource> @this, Action<TSource, int> task)
        {
            int i = 0;
            foreach (var item in @this)
                task(item, i++);
            return @this;
        }

        /// <summary>
        /// Do action for each item.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static IEnumerable Each<TSource>(this IEnumerable @this, Action<TSource, int> task)
        {
            int i = 0;
            foreach (TSource item in @this)
                task(item, i++);
            return @this;
        }

        public static IEnumerable<TSource> WhereMin<TSource, TResult>(this IEnumerable<TSource> @this, Func<TSource, TResult> selector)
        {
            var min = @this.Min(selector);
            return @this.Where(x => selector(x).Equals(min));
        }

        public static IEnumerable<TSource> WhereMax<TSource, TResult>(this IEnumerable<TSource> @this, Func<TSource, TResult> selector)
        {
            var min = @this.Max(selector);
            return @this.Where(x => selector(x).Equals(min));
        }

        /// <summary>
        /// Returns a collection of tuples containing values and indexes.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<VI<TSource>> AsVI<TSource>(this IEnumerable<TSource> @this)
        {
            int i = 0;
            foreach (var item in @this)
                yield return new VI<TSource>(item, i++);
        }

        /// <summary>
        /// Returns a collection of tuples containing values and indexes.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<VI<TSource>> AsVI<TSource>(this IEnumerable @this)
        {
            int i = 0;
            foreach (TSource item in @this)
                yield return new VI<TSource>(item, i++);
        }

        /// <summary>
        /// Projects page elements of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="pageNumber">'pageNumber' starts at 1</param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PagedEnumerable<TSource> SelectPage<TSource>(this IEnumerable<TSource> @this, int pageNumber, int pageSize)
            => new PagedEnumerable<TSource>(@this, pageNumber, pageSize);

        /// <summary>
        /// Calculates the max page number through the specified page size.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static int PageCount<TSource>(this IEnumerable<TSource> @this, int pageSize)
        {
            int count = 0;
            switch (@this)
            {
                case TSource[] array: count = array.Length; break;
                case ICollection<TSource> collection: count = collection.Count; break;
                case IQueryable<TSource> querable: count = querable.Count(); break;
                default: count = @this.Count(); break;
            }
            return (int)Math.Ceiling((double)count / pageSize);
        }

        /// <summary>
        /// Concatenates the members of a collection, using the specified separator between each member.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Join<TSource>(this IEnumerable<TSource> @this, string separator) => string.Join(separator, @this);

        /// <summary>
        /// Projects each element of the specified array into a new distributed array collection by 'Classify' method.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TConditionRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="Classify"></param>
        /// <returns></returns>
        public static IEnumerable<TSource[]> Distribute<TSource, TConditionRet>(
            this IEnumerable<TSource> @this,
            Func<TSource, TConditionRet> Classify)
        {
            if (@this.Any())
            {
                var value = Classify(@this.First());
                var group = new List<TSource>();

                foreach (var item in @this)
                {
                    var itemValue = Classify(item);

                    if (!itemValue.Equals(value))
                    {
                        value = itemValue;
                        yield return group.ToArray();
                        group.Clear();
                    }

                    group.Add(item);
                }
                yield return group.ToArray();
            }
            else yield return new TSource[0];
        }

        /// <summary>
        /// Projects each element of the specified array into a new distributed array collection by 'Classify' method.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TConditionRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="Classify"></param>
        /// <returns></returns>
        public static IEnumerable<TSource[]> Distribute<TSource, TConditionRet>(
            this IEnumerable<TSource> @this,
            Func<TSource, int, TConditionRet> Classify)
        {
            if (@this.Any())
            {
                int index = 0;
                var value = Classify(@this.First(), 0);
                var group = new List<TSource>();

                foreach (var item in @this)
                {
                    var itemValue = Classify(item, index++);

                    if (!itemValue.Equals(value))
                    {
                        value = itemValue;
                        yield return group.ToArray();
                        group.Clear();
                    }

                    group.Add(item);
                }
                yield return group.ToArray();
            }
            else yield return new TSource[0];
        }

        /// <summary>
        /// Projects each element of the specified array into a new distributed array collection by its index.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<TSource[]> Distribute<TSource>(
            this IEnumerable<TSource> @this, int count)
        {
            if (@this.Any())
            {
                var index = 0;
                var group = new List<TSource>();

                foreach (var item in @this)
                {
                    if (index > 0 && (index % count) == 0)
                    {
                        yield return group.ToArray();
                        group.Clear();
                    }

                    group.Add(item);
                    index++;
                }
                yield return group.ToArray();
            }
            else yield return new TSource[0];
        }

        /// <summary>
        /// Returns distinct elements from a sequence by using a specified properties to compare values.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="compareMethod"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctByValue<TSource>(this IEnumerable<TSource> source, Expression<Func<TSource, object>> expression)
                => Enumerable.Distinct(source, new ExactEqualityComparer<TSource>(expression));

        /// <summary>
        /// Produces the set difference of two sequences by using the specified properties to compare values.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="compareMethod"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Expression<Func<TSource, object>> expression)
            => Enumerable.Except(first, second, new ExactEqualityComparer<TSource>(expression));

        /// <summary>
        /// Produces the set union of two sequences by using a specified properties.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="compareMethod"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Expression<Func<TSource, object>> expression)
            => Enumerable.Union(first, second, new ExactEqualityComparer<TSource>(expression));

        /// <summary>
        /// Produces the set intersection of two sequences by using the specified properties to compare values.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="compareMethod"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Expression<Func<TSource, object>> expression)
            => Enumerable.Intersect(first, second, new ExactEqualityComparer<TSource>(expression));

    }
}

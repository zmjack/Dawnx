using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Dawnx
{
    public static partial class DawnObject
    {
        /// <summary>
        /// Determines whether the specified object is null.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNull<TSelf>(this TSelf @this)
            where TSelf : class
            => @this is null;

        /// <summary>
        /// Determines whether the specified object is not null.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNotNull<TSelf>(this TSelf @this)
            where TSelf : class
            => !(@this is null);

        /// <summary>
        /// Do a task for itself.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static TSelf Self<TSelf>(this TSelf @this, Action<TSelf> task)
        {
            task(@this);
            return @this;
        }

        /// <summary>
        /// Casts the element to the specified type through the specified convert method.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="convert"></param>
        /// <returns></returns>
        public static TRet For<TSelf, TRet>(this TSelf @this, Func<TSelf, TRet> convert)
            => convert(@this);

        /// <summary>
        /// Casts the element to the specified type through the specified ForMethod.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="convert"></param>
        /// <returns></returns>
        public static TRet For<TSelf, TRet>(this TSelf @this, ForMethod<TSelf, TRet> forMethod)
        {
            foreach (var project in forMethod.Methods)
            {
                var result = project(@this);
                if (forMethod.Condition(result))
                    return result;
            }

            return forMethod.DefaultReturn;
        }

        /// <summary>
        /// Determines whether the specified element in a sequence by using the default equality comparer.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool In<TSource>(this TSource @this, params TSource[] sequence)
            => sequence.Contains(@this);

        /// <summary>
        /// Determines whether the specified element in a sequence by using the default equality comparer.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool In<TSource>(this TSource @this, IEnumerable<TSource> sequence)
            => sequence.Contains(@this);

        /// <summary>
        /// Converts the specified object to <see cref="ExpandoObject"/>.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static ExpandoObject ToExpandoObject(this object @this)
        {
            var obj = new ExpandoObject();
            var objDict = obj as IDictionary<string, object>;

            var props = @this.GetType().GetProperties();
            foreach (var prop in props)
            {
                var value = prop.GetValue(@this);
                if (value.GetType().Name.StartsWith("<>f__AnonymousType"))
                    objDict[prop.Name] = ToExpandoObject(value);
                else objDict[prop.Name] = value;
            }
            return obj;
        }

    }
}

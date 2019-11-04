using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XObject
    {
        /// <summary>
        /// Do a task for itself.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static TSelf Then<TSelf>(this TSelf @this, Action<TSelf> task)
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
        public static TRet For<TSelf, TRet>(this TSelf @this, TRet convert) => convert;

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
        /// Casts the element to the specified type through the specified filter method.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static TRet For<TSelf, TRet>(this TSelf @this, Func<TSelf, TRet>[] filters)
            where TRet : class
        {
            foreach (var project in filters)
            {
                var result = project(@this);
                if (!(result is null))
                    return result;
            }
            return null;
        }

        /// <summary>
        /// Determines whether the specified element in a sequence by using the default equality comparer.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static bool In<TSource>(this TSource @this, params TSource[] sequence)
            => sequence.Contains(@this);

        /// <summary>
        /// Determines whether the specified element in a sequence by using the default equality comparer.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static bool In<TSource>(this TSource @this, IEnumerable<TSource> sequence)
            => sequence.Contains(@this);

        /// <summary>
        /// Determines whether the specified object is null.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNull<TSelf>(this TSelf @this)
            where TSelf : class
            => @this is null;

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
                if (value.GetType().IsAnonymousType())
                    objDict[prop.Name] = ToExpandoObject(value);
                else objDict[prop.Name] = value;
            }
            return obj;
        }

        #region Flow
        public static TRet Flow<T, TRet>(this T @this, IFlow<T, TRet> flow) => flow.Execute(@this);

        public static TRet Flow<T, TRet>(this T @this,
            Func<T, TRet> ret)
        {
            return new Flow<T, TRet>(ret).Execute(@this);
        }

        public static TRet Flow<T, TStep1, TRet>(this T @this,
            Func<T, TStep1> step1,
            Func<TStep1, TRet> ret)
        {
            return new Flow<T, TStep1, TRet>(step1, ret).Execute(@this);
        }

        public static TRet Flow<T, TStep1, TStep2, TRet>(this T @this,
            Func<T, TStep1> step1,
            Func<TStep1, TStep2> step2,
            Func<TStep2, TRet> ret)
        {
            return new Flow<T, TStep1, TStep2, TRet>(step1, step2, ret).Execute(@this);
        }

        public static TRet Flow<T, TStep1, TStep2, TStep3, TRet>(this T @this,
            Func<T, TStep1> step1,
            Func<TStep1, TStep2> step2,
            Func<TStep2, TStep3> step3,
            Func<TStep3, TRet> ret)
        {
            return new Flow<T, TStep1, TStep2, TStep3, TRet>(step1, step2, step3, ret).Execute(@this);
        }

        public static TRet Flow<T, TStep1, TStep2, TStep3, TStep4, TRet>(this T @this,
            Func<T, TStep1> step1,
            Func<TStep1, TStep2> step2,
            Func<TStep2, TStep3> step3,
            Func<TStep3, TStep4> step4,
            Func<TStep4, TRet> ret)
        {
            return new Flow<T, TStep1, TStep2, TStep3, TStep4, TRet>(step1, step2, step3, step4, ret).Execute(@this);
        }

        public static TRet Flow<T, TStep1, TStep2, TStep3, TStep4, TStep5, TRet>(this T @this,
            Func<T, TStep1> step1,
            Func<TStep1, TStep2> step2,
            Func<TStep2, TStep3> step3,
            Func<TStep3, TStep4> step4,
            Func<TStep4, TStep5> step5,
            Func<TStep5, TRet> ret)
        {
            return new Flow<T, TStep1, TStep2, TStep3, TStep4, TStep5, TRet>(step1, step2, step3, step4, step5, ret).Execute(@this);
        }

        public static TRet Flow<T, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TRet>(this T @this,
            Func<T, TStep1> step1,
            Func<TStep1, TStep2> step2,
            Func<TStep2, TStep3> step3,
            Func<TStep3, TStep4> step4,
            Func<TStep4, TStep5> step5,
            Func<TStep5, TStep6> step6,
            Func<TStep6, TRet> ret)
        {
            return new Flow<T, TStep1, TStep2, TStep3, TStep4, TStep5, TStep6, TRet>(step1, step2, step3, step4, step5, step6, ret).Execute(@this);
        }
        #endregion

        // Method
        public static object Invoke(this object @this, string methodName, params object[] parameters)
            => @this.GetType().GetTypeInfo().GetDeclaredMethod(methodName).Invoke(@this, parameters);
        public static object Invoke<TThis>(this object @this, string methodName, params object[] parameters)
            => typeof(TThis).GetTypeInfo().GetDeclaredMethod(methodName).Invoke(@this, parameters);

        // Property
        public static object GetPropertyValue(this object @this, string propertyName)
            => @this.GetType().GetTypeInfo().GetDeclaredProperty(propertyName).GetValue(@this);
        public static object GetPropertyValue<TThis>(this object @this, string propertyName)
            => typeof(TThis).GetTypeInfo().GetDeclaredProperty(propertyName).GetValue(@this);

        public static void SetPropertyValue(this object @this, string propertyName, object value)
            => @this.GetType().GetTypeInfo().GetDeclaredProperty(propertyName).SetValue(@this, value);
        public static void SetPropertyValue<TThis>(this object @this, string propertyName, object value)
            => typeof(TThis).GetTypeInfo().GetDeclaredProperty(propertyName).SetValue(@this, value);

        // Field
        public static object GetFieldValue(this object @this, string filedName)
            => @this.GetType().GetTypeInfo().GetDeclaredField(filedName).GetValue(@this);
        public static object GetFieldValue<TThis>(this object @this, string filedName)
            => typeof(TThis).GetTypeInfo().GetDeclaredField(filedName).GetValue(@this);

        public static void SetFieldValue(this object @this, string filedName, object value)
            => @this.GetType().GetTypeInfo().GetDeclaredField(filedName).SetValue(@this, value);
        public static void SetFieldValue<TThis>(this object @this, string filedName, object value)
            => typeof(TThis).GetTypeInfo().GetDeclaredField(filedName).SetValue(@this, value);
    }
}

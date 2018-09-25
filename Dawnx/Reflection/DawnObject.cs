using System;
using System.Linq;
using System.Reflection;

namespace Dawnx.Reflection
{
    public static class Reflection_DawnObject
    {
        // Method
        public static object InnerInvoke(this object @this, string methodName, params object[] parameters)
            => @this.GetType().GetTypeInfo().GetDeclaredMethod(methodName).Invoke(@this, parameters);
        public static TRet InnerInvoke<TRet>(this object @this, string methodName, params object[] parameters)
            => (TRet)@this.GetType().GetTypeInfo().GetDeclaredMethod(methodName).Invoke(@this, parameters);
        public static TRet InnerInvoke<TThis, TRet>(this object @this, string methodName, params object[] parameters)
            => (TRet)typeof(TThis).GetTypeInfo().GetDeclaredMethod(methodName).Invoke(@this, parameters);

        // Property
        public static TRet GetPropertyValue<TRet>(this object @this, string propertyName)
            => (TRet)@this.GetType().GetTypeInfo().GetDeclaredProperty(propertyName).GetValue(@this);
        public static TRet GetPropertyValue<TThis, TRet>(this object @this, string propertyName)
            => (TRet)typeof(TThis).GetTypeInfo().GetDeclaredProperty(propertyName).GetValue(@this);

        public static void SetPropertyValue(this object @this, string propertyName, object value)
            => @this.GetType().GetTypeInfo().GetDeclaredProperty(propertyName).SetValue(@this, value);
        public static void SetPropertyValue<TThis>(this object @this, string propertyName, object value)
            => typeof(TThis).GetTypeInfo().GetDeclaredProperty(propertyName).SetValue(@this, value);

        // Field
        public static TRet GetFieldValue<TRet>(this object @this, string filedName)
            => (TRet)@this.GetType().GetTypeInfo().GetDeclaredField(filedName).GetValue(@this);
        public static TRet GetFieldValue<TThis, TRet>(this object @this, string filedName)
            => (TRet)typeof(TThis).GetTypeInfo().GetDeclaredField(filedName).GetValue(@this);

        public static void SetFieldValue(this object @this, string filedName, object value)
            => @this.GetType().GetTypeInfo().GetDeclaredField(filedName).SetValue(@this, value);
        public static void SetFieldValue<TThis>(this object @this, string filedName, object value)
            => typeof(TThis).GetTypeInfo().GetDeclaredField(filedName).SetValue(@this, value);

    }
}

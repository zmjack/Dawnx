using System.Reflection;

namespace Dawnx.Reflection
{
    public static partial class DawnObject
    {
        // Method
        public static object InnerInvoke(this object @this, string methodName, params object[] parameters)
            => @this.GetType().GetTypeInfo().GetDeclaredMethod(methodName).Invoke(@this, parameters);
        public static object InnerInvoke<TThis>(this object @this, string methodName, params object[] parameters)
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

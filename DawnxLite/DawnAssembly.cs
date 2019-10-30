using System;
using System.Linq;
using System.Reflection;

namespace Dawnx
{
    public static class DawnAssembly
    {
        public static Type[] GetTypesWhichExtends<TExtendClass>(this Assembly @this, bool recursiveSearch = false, bool useGenericX = false)
        {
            return GetTypesWhichExtends(@this, typeof(TExtendClass), recursiveSearch, useGenericX);
        }
        public static Type[] GetTypesWhichExtends(this Assembly @this, Type @class, bool recursiveSearch = false, bool useGenericX = false)
        {
            return @this.GetTypes().Where(type => DawnType.IsExtend(type, @class, recursiveSearch, useGenericX)).ToArray();
        }

        public static Type[] GetTypesWhichImplements<TInterface>(this Assembly @this, bool useGenericX = false)
            where TInterface : class
        {
            return GetTypesWhichImplements(@this, typeof(TInterface), useGenericX);
        }
        public static Type[] GetTypesWhichImplements(this Assembly @this, Type @interface, bool useGenericX = false)
        {
            return @this.GetTypes().Where(type => DawnType.IsImplement(type, @interface, useGenericX)).ToArray();
        }

        public static Type[] GetTypesWhichMarkedAs<TAttribute>(this Assembly @this)
            where TAttribute : Attribute
        {
            return GetTypesWhichMarkedAs(@this, typeof(TAttribute));
        }
        public static Type[] GetTypesWhichMarkedAs(this Assembly @this, Type attribute)
        {
            return @this.GetTypes()
                .Where(type => type.Assembly.FullName == @this.FullName)
                .Where(type => type.IsMarkedAs(attribute))
                .ToArray();
        }

    }
}

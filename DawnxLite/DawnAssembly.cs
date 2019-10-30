using System;
using System.Linq;
using System.Reflection;

namespace Dawnx
{
    public static class DawnAssembly
    {
        public static Type[] GetTypesWhichExtends<TExtendClass>(this Assembly @this, bool recursiveSearch = false)
        {
            return GetTypesWhichExtends(@this, typeof(TExtendClass), recursiveSearch);
        }
        public static Type[] GetTypesWhichExtends(this Assembly @this, Type @class, bool recursiveSearch = false)
        {
            return @this.GetTypes().Where(type => DawnType.IsExtend(type, @class, recursiveSearch)).ToArray();
        }
        public static Type[] GetTypesWhichExtendsGeneric(this Assembly @this, Type @class, bool recursiveSearch = false)
        {
            return @this.GetTypes().Where(type => DawnType.IsExtendGeneric(type, @class, recursiveSearch)).ToArray();
        }

        public static Type[] GetTypesWhichImplements<TInterface>(this Assembly @this)
            where TInterface : class
        {
            return GetTypesWhichImplements(@this, typeof(TInterface));
        }
        public static Type[] GetTypesWhichImplements(this Assembly @this, Type @interface)
        {
            return @this.GetTypes().Where(type => DawnType.IsImplement(type, @interface)).ToArray();
        }
        public static Type[] GetTypesWhichImplementsGeneric(this Assembly @this, Type @interface)
        {
            return @this.GetTypes().Where(type => DawnType.IsImplementGeneric(type, @interface)).ToArray();
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

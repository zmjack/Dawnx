using System;
using System.Linq;
using System.Reflection;

namespace Dawnx
{
    public static class DawnAssembly
    {
        public static Type[] GetTypesWhichExtends<TExtendClass>(this Assembly @this, bool recursiveSearch = false)
        {
            return @this.GetTypes().Where(type => DawnType.IsExtend<TExtendClass>(type, recursiveSearch)).ToArray();
        }
        public static Type[] GetTypesWhichExtends(this Assembly @this, Type @class, bool recursiveSearch = false)
        {
            return @this.GetTypes().Where(type => DawnType.IsExtend(type, @class, recursiveSearch)).ToArray();
        }

        public static Type[] GetTypesWhichImplements<TInterface>(this Assembly @this)
            where TInterface : class
        {
            return @this.GetTypes().Where(type => DawnType.IsImplement<TInterface>(type)).ToArray();
        }
        public static Type[] GetTypesWhichImplements(this Assembly @this, Type @interface)
        {
            return @this.GetTypes().Where(type => DawnType.IsImplement(type, @interface)).ToArray();
        }

        public static Type[] GetTypesWhichMarkedAs<TAttribute>(this Assembly @this)
            where TAttribute : Attribute
        {
            return @this.GetTypes()
                .Where(type => type.Assembly.FullName == @this.FullName)
                .Where(type => type.IsMarkedAs<TAttribute>())
                .ToArray();
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

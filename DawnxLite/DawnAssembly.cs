using System;
using System.Linq;
using System.Reflection;

namespace Dawnx
{
    public static class DawnAssembly
    {
        public static Type[] GetTypesWhichExtends<TSuperClass>(this Assembly @this, bool recursiveSearch = false)
        {
            return @this.GetTypes().Where(type =>
            {
                if (recursiveSearch)
                    return RecursiveSearchExtends<TSuperClass>(type.BaseType);
                else return type.BaseType?.FullName == typeof(TSuperClass).FullName;
            }).ToArray();
        }

        private static bool RecursiveSearchExtends<TSuperClass>(Type type)
        {
            if (type != null)
            {
                if (type.FullName == typeof(TSuperClass).FullName)
                    return true;
                else return RecursiveSearchExtends<TSuperClass>(type.BaseType);
            }
            else return false;
        }

        public static Type[] GetTypesWhichImplements<TImplementInterface>(this Assembly @this)
        {
            return @this.GetTypes().Where(type =>
            {
                return type.GetInterfaces().Any(x => x.FullName == typeof(TImplementInterface).FullName);
            }).ToArray();
        }

        public static Type[] GetTypesWhichMarkedAs<TAttribute>(this Assembly @this)
            where TAttribute : Attribute
        {
            return @this.GetTypes()
                .Where(type => type.Assembly.FullName == @this.FullName)
                .Where(type => type.GetCustomAttribute<TAttribute>() != null)
                .ToArray();
        }

    }
}

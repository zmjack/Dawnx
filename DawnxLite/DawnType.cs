using System;
using System.Linq;
using System.Reflection;

namespace Dawnx
{
    public static class DawnType
    {
        public static MethodInfo GetMethodViaQualifiedName(this Type @this, string formatName)
        {
            return @this.GetMethods().First(x => x.ToString() == formatName);
        }

        public static bool IsImplement<TInterface>(this Type @this)
            where TInterface : class
        {
            return IsImplement(@this, typeof(TInterface));
        }
        public static bool IsImplement(this Type @this, Type @interface)
        {
            return @this.GetInterfaces().Any(x => x.FullName == @interface.FullName);
        }

        public static bool IsExtend<TExtendType>(this Type @this, bool recursiveSearch = false)
        {
            return IsExtend(@this, typeof(TExtendType), recursiveSearch);
        }
        public static bool IsExtend(this Type @this, Type extendType, bool recursiveSearch = false)
        {
            if (recursiveSearch)
                return RecursiveSearchExtends(@this.BaseType, extendType);
            else return @this.BaseType?.FullName == extendType.FullName;
        }

        private static bool RecursiveSearchExtends(Type type, Type extendType)
        {
            if (type != null)
            {
                if (type.FullName == extendType.FullName)
                    return true;
                else return RecursiveSearchExtends(type.BaseType, extendType);
            }
            else return false;
        }

    }
}

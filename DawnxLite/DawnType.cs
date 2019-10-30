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

        public static bool IsImplement<TInterface>(this Type @this, bool useGenericX = false)
            where TInterface : class
        {
            return IsImplement(@this, typeof(TInterface), useGenericX);
        }
        public static bool IsImplement(this Type @this, Type @interface, bool useGenericX = false)
        {
            var interfaces = @this.GetInterfaces();
            if (!useGenericX)
                return interfaces.Any(x => x.FullName == @interface.FullName);
            else return interfaces.Any(x => x.FullName.StartsWith(@interface.FullName));
        }

        public static bool IsExtend<TExtendType>(this Type @this, bool recursiveSearch = false, bool useGenericX = false)
        {
            return IsExtend(@this, typeof(TExtendType), recursiveSearch, useGenericX);
        }
        public static bool IsExtend(this Type @this, Type extendType, bool recursiveSearch = false, bool useGenericX = false)
        {
            if (!recursiveSearch)
            {
                if (!useGenericX)
                    return @this.BaseType?.FullName == extendType.FullName;
                else return @this.BaseType?.FullName.StartsWith(extendType.FullName) ?? false;
            }
            else return RecursiveSearchExtends(@this.BaseType, extendType);
        }

        private static bool RecursiveSearchExtends(Type type, Type extendType, bool useGenericX = false)
        {
            if (type != null)
            {
                if (!useGenericX && type.FullName == extendType.FullName)
                    return true;
                else if (useGenericX && type.FullName.StartsWith(extendType.FullName))
                    return true;
                else return RecursiveSearchExtends(type.BaseType, extendType);
            }
            else return false;
        }

    }
}

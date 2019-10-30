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
        public static bool IsImplementGeneric(this Type @this, Type @interface)
        {
            if (!@interface.IsGenericType)
                throw new ArgumentException($"The `${nameof(@interface)}` is not generic type.");

            return @this.GetInterfaces().Any(x => x.FullName.StartsWith(@interface.FullName));
        }

        public static bool IsExtend<TExtendType>(this Type @this, bool recursiveSearch = false)
        {
            return IsExtend(@this, typeof(TExtendType), recursiveSearch);
        }
        public static bool IsExtend(this Type @this, Type extendType, bool recursiveSearch = false)
        {
            if (!recursiveSearch)
                return @this.BaseType?.FullName == extendType.FullName;
            else return RecursiveSearchExtends(@this.BaseType, extendType, false);
        }
        public static bool IsExtendGeneric(this Type @this, Type extendType, bool recursiveSearch = false)
        {
            if (!extendType.IsGenericType)
                throw new ArgumentException($"The `${nameof(extendType)}` is not generic type.");

            if (!recursiveSearch)
                return @this.BaseType?.FullName.StartsWith(extendType.FullName) ?? false;
            else return RecursiveSearchExtends(@this.BaseType, extendType, true);
        }

        private static bool RecursiveSearchExtends(Type type, Type extendType, bool useGenericX)
        {
            if (type != null)
            {
                if (!useGenericX && type.FullName == extendType.FullName)
                    return true;
                else if (useGenericX && type.FullName.StartsWith(extendType.FullName))
                    return true;
                else return RecursiveSearchExtends(type.BaseType, extendType, useGenericX);
            }
            else return false;
        }

    }
}

using Castle.DynamicProxy;

namespace Dawnx
{
    public static class DawnIInterceptor
    {
        public static T Proxy<T>(this T @this)
            where T : class, IInterceptor, new()
        {
            return new ProxyGenerator().CreateClassProxy<T>(new T());
        }
    }
}

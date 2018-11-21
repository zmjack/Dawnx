using Microsoft.AspNetCore.Hosting;
using System;
using System.Reflection;

namespace Dawnx.AspNetCore
{
    public static class DIUtility
    {
        public static TService GetEntryService<TService>()
            where TService : class
            => GetEntryService<TService>(typeof(TService));

        public static TService GetEntryService<TService>(Type type)
            where TService : class
        {
            var webHost = GetWebHost();

            return (webHost.Services
                .GetService(type) as TService)
                .For(_ =>
                {
                    if (_ != null) return _;
                    else throw new EntryPointNotFoundException($"Can not find service.");
                });
        }

        private static IWebHost GetWebHost()
        {
            var assembly = Assembly.GetEntryAssembly();
            var webHostBuilder = assembly
                .For(_ =>
                {
                    var className = $"{assembly.GetName().Name}.Program";
                    var type = _.GetType(className);
                    if (type != null) return type;
                    else throw new EntryPointNotFoundException($"Can not find class '{className}'");
                })
                .For(_ =>
                {
                    var methodName = "CreateWebHostBuilder";
                    var method = _.GetMethod(methodName);
                    if (method != null) return method;
                    else throw new EntryPointNotFoundException($"Can not find method '{methodName}'");
                })
                .Invoke(null, new object[] { new string[0] }) as IWebHostBuilder;

            return webHostBuilder
                .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .Build();
        }

    }
}

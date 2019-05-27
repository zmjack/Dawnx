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

            try
            {
                var service = webHost.Services.GetService(type) as TService;
                return service;
            }
            catch (ArgumentNullException exception)
            {
                throw new EntryPointNotFoundException($"Can not find service.", exception);
            }
        }

        private static IWebHost GetWebHost()
        {
            var assembly = Assembly.GetEntryAssembly();
            var webHostBuilder = assembly
                .For(self =>
                {
                    var className = $"{assembly.GetName().Name}.Program";
                    var type = self.GetType(className);
                    if (type != null) return type;
                    else throw new EntryPointNotFoundException($"Can not find class '{className}'");
                })
                .For(self =>
                {
                    var methodName = "CreateWebHostBuilder";
                    var method = self.GetMethod(methodName);
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

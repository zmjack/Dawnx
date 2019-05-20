using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public abstract class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        protected abstract void Handle(HttpContext context, Exception exception);

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Handle(context, ex);
                throw;
            }
        }
    }

    public static class ErrorLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorLogging<TErrorLoggingMiddleware>(this IApplicationBuilder builder)
            where TErrorLoggingMiddleware : ErrorLoggingMiddleware
        {
            return builder.UseMiddleware<TErrorLoggingMiddleware>();
        }
    }

}

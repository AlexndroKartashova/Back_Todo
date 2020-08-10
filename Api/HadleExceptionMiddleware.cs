using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Api
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class HadleExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public HadleExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HadleExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseHadleExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HadleExceptionMiddleware>();
        }
    }
}

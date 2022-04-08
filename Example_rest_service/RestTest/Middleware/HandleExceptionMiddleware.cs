using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebApiExample.Middleware
{
    public class HandleExceptionMiddleware
    {
        private RequestDelegate _next;
        public HandleExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {                
                await context.Response.WriteAsync("Error: " + e.Message);
            }
        }
    }
}
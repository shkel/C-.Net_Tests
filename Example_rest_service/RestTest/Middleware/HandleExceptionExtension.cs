using Microsoft.AspNetCore.Builder;

namespace WebApiExample.Middleware
{
    public static class HandleExceptionExtension
    {
        public static IApplicationBuilder UseHandleException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HandleExceptionMiddleware>();
        }
    }
}

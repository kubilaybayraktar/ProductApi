using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Product.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (!(ex is UserFriendlyError))
                    _logger.LogError(ex, ex.InnerException?.Message);

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (exception is UserFriendlyError)
            {
                UserFriendlyError ufException = exception as UserFriendlyError;
                var statusCode = ufException.StatusCode;
                string msg = ufException.Message + ufException.InnerException?.Message;
                context.Response.StatusCode = statusCode;
                return context.Response.WriteAsync(ufException.ToString());
            }
            else
            {
                string errorMessage = exception.Message + exception.InnerException?.Message;

#if !DEBUG
                errorMessage = "Sorry, there has been an error. We'll fix the problem shortly.";
#endif

                return context.Response.WriteAsync(errorMessage);
            }
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync("Internal Server Error!");
                    }
                });
            });
        }

        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}

using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Mime;

namespace ChatApp.Api.Extensions
{
    public static class ExceptionHandlerException
    {
        public static void ConfigureExceptionHandler<T>(this WebApplication app, ILogger<T> logger)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async httpContext =>
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    httpContext.Response.ContentType = MediaTypeNames.Application.Json;
                    var feature = httpContext.Features.Get<IExceptionHandlerFeature>();
                    if (feature != null)
                    {
                        logger.LogError(feature.Error.Message);
                        await httpContext.Response.WriteAsJsonAsync(new
                        {
                            StatusCode = httpContext.Response.StatusCode,
                            Message = feature.Error.Message,
                            Title = "Error"
                        });
                    }
                });
            });
        }
    }
}

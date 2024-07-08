using ChatApp.Api.Responses;
using ChatApp.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Net.Mime;
namespace ChatApp.Api.Extensions
{

    public static class ExceptionHandlerExtensions
    {
        public static void ConfigureExceptionHandler<T>(this WebApplication app, ILogger<T> logger)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async httpContext =>
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    httpContext.Response.ContentType = MediaTypeNames.Application.Json;

                    var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var exception = contextFeature.Error;

                        logger.LogError(exception, "An unhandled exception occurred.");

                        ApiResponse<object> response;

                        if (exception is SecurityTokenExpiredException)
                        {
                            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            response = ApiResponse<object>.Fail(
                                error: null,
                                message: "Token has expired. Please login again.",
                                code: httpContext.Response.StatusCode
                            );
                        }
                        else if (exception is CustomException customException)
                        {
                            httpContext.Response.StatusCode = (int)customException.StatusCode;
                            response = ApiResponse<object>.Fail(
                                error: null,
                                message: customException.Message,
                                code: httpContext.Response.StatusCode
                            );
                        }
                        else
                        {
                            response = ApiResponse<object>.Error(
                                message: "An unexpected error occurred. Please try again later.",
                                code: httpContext.Response.StatusCode,
                                error: new
                                {
                                    exception.Message,
                                    exception.StackTrace
                                }
                            );
                        }

                        await httpContext.Response.WriteAsJsonAsync(response);
                    }
                });
            });
        }
    }
}



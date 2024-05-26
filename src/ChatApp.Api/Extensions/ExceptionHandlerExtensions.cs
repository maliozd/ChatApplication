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

                        var errorDetails = new ErrorDetails
                        {
                            StatusCode = httpContext.Response.StatusCode,
                            Message = "An unexpected error occurred. Please try again later.",
                            Title = "Error"
                        };

                        if (exception is SecurityTokenExpiredException)
                        {
                            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            errorDetails = new ErrorDetails
                            {
                                StatusCode = httpContext.Response.StatusCode,
                                Message = "Token has expired. Please login again.",
                                Title = "Unauthorized"
                            };
                        }

                        if (exception is CustomException customException)
                        {
                            httpContext.Response.StatusCode = (int)customException.StatusCode;
                            errorDetails = new ErrorDetails
                            {
                                StatusCode = httpContext.Response.StatusCode,
                                Message = customException.Message,
                                Title = customException.Title
                            };
                        }

                        errorDetails.Detail = exception.StackTrace;

                        await httpContext.Response.WriteAsJsonAsync(errorDetails);
                    }
                });
            });
        }
    }
}

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Title { get; set; }
    public string Detail { get; set; }
}

public class CustomException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string Title { get; }

    public CustomException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest, string title = "Error")
        : base(message)
    {
        StatusCode = statusCode;
        Title = title;
    }
}

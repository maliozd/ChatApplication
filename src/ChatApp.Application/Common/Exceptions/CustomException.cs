using System.Net;

namespace ChatApp.Application.Common.Exceptions
{
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

        public CustomException(HttpStatusCode statusCode, string title)
        {
            StatusCode = statusCode;
            Title = title;
        }
    }

}

using System.Net;

namespace ChatApp.Api.Responses
{
    public record ApiResponse<T>(string Status, int StatusCode, string Message, T Data, object Errors)
    {
        public string Status { get; set; } = Status;
        public int StatusCode { get; set; } = StatusCode;
        public string Message { get; set; } = Message;
        public T Data { get; set; } = Data;
        public object Errors { get; set; } = Errors;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static ApiResponse<T> Success(T data, string message = "Successfull", int code = (int)HttpStatusCode.OK)
        {
            return new ApiResponse<T>("success", code, message, data, null);
        }

        public static ApiResponse<T> Fail(object error, string message = "Request failed", int code = (int)HttpStatusCode.BadRequest)
        {
            return new ApiResponse<T>("fail", code, message, default(T), error);
        }

        public static ApiResponse<T> Error(string message = "Internal server error", int code = (int)HttpStatusCode.InternalServerError, object error = null)
        {
            return new ApiResponse<T>("error", code, message, default(T), error);
        }
    }
}

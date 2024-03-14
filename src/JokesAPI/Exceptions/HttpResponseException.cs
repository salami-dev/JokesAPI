using JokesAPI.DTOs;

namespace JokesAPI.Exceptions
{
    public class HttpResponseException : Exception
    {
        private object? Value {  get; }

            public HttpResponseException(int statusCode, string message, object? value = null) =>
                (StatusCode, Message, Value) = (statusCode, message, value);
            
        public string Message { get; }
            public int StatusCode { get; }

            public ErrorResponse Error { get => new ErrorResponse {Message=Message}; }
        
    }
}

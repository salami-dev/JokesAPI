namespace JokesAPI.Exceptions
{
    public class ConflictException : HttpResponseException
    {
        public ConflictException(string message, object? error = null) : base(409, message, error)
        {
        }
    }
}

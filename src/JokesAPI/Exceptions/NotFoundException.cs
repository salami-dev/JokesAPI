namespace JokesAPI.Exceptions
{
    public class NotFoundException : HttpResponseException
    {
        public NotFoundException(string message, object? error = null) : base(404, message, error)
        {
        }
    }
}

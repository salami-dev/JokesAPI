namespace JokesAPI.DTOs
{
    public class ErrorResponse
    {
        public required string Message { get; set; }
        public int? EntityId { get; set; } = null;
        public Dictionary<string, object> error { get; set; } = new Dictionary<string, object>();
    }
}

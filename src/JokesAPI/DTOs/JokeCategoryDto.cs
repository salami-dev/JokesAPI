namespace JokesAPI.DTOs
{
    public class JokeCategoryDto : BaseDto
    {
        public required string Category { get; set; }
        public string? Description { get; set; }
    }

    public class CreateJokeCategoryDto
    {
        private string _Category;
        public required string Category { get => _Category; set => _Category = value.ToLower(); }
        public string? Description { get; set; }

    }

    public class SlimJokeCategoryDto
    {
        public long Id { get; set; }
        public string Category { get; set; }
    }

}

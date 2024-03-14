using Microsoft.EntityFrameworkCore;

namespace JokesAPI.Models
{
    [Index(nameof(Category))]
    public class JokeCategory : BaseModel
    {
        public required string Category { get; set; }
        public string? Description { get; set; }

        public ICollection<Joke> Jokes { get; set; } = [];
    }
}
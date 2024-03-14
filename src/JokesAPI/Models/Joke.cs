using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JokesAPI.Models
{
    public class Joke : BaseModel
    {
        
        public long Likes { get; set; }
        public long Dislikes { get; set; }
        public required string Text { get; set; }

        public ICollection<JokeCategory> Categories { get; set; } = [];

        [Timestamp]
        public uint Version { get; set; }
    }
}

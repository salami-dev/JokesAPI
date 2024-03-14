namespace JokesAPI.DTOs
{
    public class JokeDto : BaseDto
    {
        /// <summary>
        /// Number of Likes a Jokes has
        /// </summary>
        public long Likes { get; set; }
        /// <summary>
        /// Number of Dislikes a joke has
        /// </summary>
        public long Dislikes { get; set; }
        /// <summary>
        /// The Joke
        /// </summary>
        public string Text { get; set; }
        
    }

    public class CreateJokeDto
    {
        public required string Text { get; set; }

    }



    public class FatJokeDto : JokeDto
    {

        public ICollection<SlimJokeCategoryDto> Categories { get; set; }

    }
}

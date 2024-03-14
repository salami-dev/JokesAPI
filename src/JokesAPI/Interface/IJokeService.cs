namespace JokesAPI.Interface
{
    public interface IJokeService
    {
        public Task<Models.Joke> Add(DTOs.CreateJokeDto createJokeDto);

        public Task<Models.Joke> Get(long id);

        public Task<IEnumerable<Models.Joke>> All();

        public Task<bool> Delete(long id);

        public Task<Models.Joke> GetRandomJoke();

        public Task<bool> ReactToJoke(long id, DTOs.ReactToJokeDto reaction);




    }
}

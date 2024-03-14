using JokesAPI.DTOs;
using JokesAPI.Models;

namespace JokesAPI.Interface
{
    public interface IJokeCategoryService
    {
        public Task<IEnumerable<JokeCategory>> All();
        public Task<JokeCategory> Add(CreateJokeCategoryDto jokeCategoryDto);

        public Task<JokeCategory> Get(long id);

        public Task<bool> Delete(long id);

        public Task<IEnumerable<Joke>> AllJokes(string category);
        public Task<Joke> AddNewJoke(string category, CreateJokeDto jokeDto);

        public Task<Joke> AddExistingJoke(string category, long jokeId);

        public Task<Joke> RemoveJoke(string category, long jokeId);

        public Task<Joke> GetRandomJoke(string category);

        public Task<JokeCategory> GetByCategory(string categoryName);
    }
}

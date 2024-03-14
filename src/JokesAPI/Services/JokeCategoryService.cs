using AutoMapper;
using JokesAPI.DTOs;
using JokesAPI.Interface;
using JokesAPI.Models;
using JokesAPI.Data;
using Microsoft.EntityFrameworkCore;
using JokesAPI.Exceptions;
using System.Reflection.Metadata;

namespace JokesAPI.Services
{
    public class JokeCategoryService : IJokeCategoryService
    {
        private readonly JokeContext _context;
        private readonly IMapper _mapper;
        private readonly IFetcherService _fetcherService;

        public JokeCategoryService(JokeContext context, IMapper mapper, IFetcherService fetcherService)
        {
            _context = context;
            _mapper = mapper;
            _fetcherService = fetcherService;
        }

        public async Task<JokeCategory> Add(CreateJokeCategoryDto jokeCategoryDto)
        {

            try
            {
                await GetByCategory(jokeCategoryDto.Category);

                throw new ConflictException("Joke Category Already Exists");

            } catch (NotFoundException ) {} 

            var jokeCategory = _mapper.Map<JokeCategory>(jokeCategoryDto);

            _context.JokeCategories.Add(jokeCategory);

            await _context.SaveChangesAsync();

            return jokeCategory;
        }

        private async Task<Joke> GetJoke(long id)
        {
            var joke = await _context.Jokes.FindAsync(id);

            if (joke == null)
            {
                throw new NotFoundException("Joke not found.");
            }

            return joke;
        }

        public async Task<Joke> AddExistingJoke(string category, long jokeId)
        {
            var jokeCategory = await GetByCategory(category);

            var joke = await GetJoke(jokeId);

            await _context.Entry(joke)
                    .Collection(jc => jc.Categories)
                    .LoadAsync();

            if (!joke.Categories.Contains(jokeCategory))
            {
                joke.Categories.Add(jokeCategory);

                await _context.SaveChangesAsync();
            }

            return joke;
        }

        public async Task<Joke> AddNewJoke(string category, CreateJokeDto jokeDto)
        {
            var jokeCategory = await GetByCategory(category);

            var joke = _mapper.Map<Joke>(jokeDto);

            joke.Categories.Add(jokeCategory);

            _context.Jokes.Add(joke);

            await _context.SaveChangesAsync();

            return joke;
        }

        public async Task<IEnumerable<JokeCategory>> All()
        {
            return await _context.JokeCategories.ToListAsync();
        }

        public async Task<bool> Delete(long id)
        {
            var jokeCategory = await _Get(id);

            _context.JokeCategories.Remove(jokeCategory);

            await _context.SaveChangesAsync();

            return true;
        }
        private async Task<JokeCategory> _Get(long id)
        {
            return await _fetcherService.GetOrThrowAsync<JokeCategory>(q => q.Where(j => j.Id == id));
        }

        public async Task<JokeCategory> Get(long id)
        {
            return await _Get(id);
        }

        public async Task<JokeCategory> GetByCategory(string categoryName)
        {
            return await _fetcherService.GetOrThrowAsync<JokeCategory>(q => q.Where(_ => EF.Functions.ILike(_.Category, $"%{categoryName}%")));
        }

        public async Task<IEnumerable<Joke>> AllJokes(string category)
        {

            var jokeCategory = await GetByCategory(category);

            var jokes = await _context.Jokes.Where(
                j => j.Categories.Any(c => c.Id == jokeCategory.Id)
                ).ToListAsync();

            return jokes;
        }

        public async Task<Joke> GetRandomJoke(string category)
        {
            var jokeCategory = await GetByCategory(category);


            var randomJoke = await _context.Jokes.Where(j => j.Categories.Any(jc => jc.Id == jokeCategory.Id)).OrderBy(j => EF.Functions.Random()).FirstOrDefaultAsync();

            if (randomJoke == null)
            {
                throw new NotFoundException("No Jokes Available.");
            }

            return randomJoke;
        }

        public async Task<Joke> RemoveJoke(string category, long jokeId)
        {
            var jokeCategory = await GetByCategory(category);

            var joke = await GetJoke(jokeId);

            await _context.Entry(joke)
                    .Collection(jc => jc.Categories)
                    .LoadAsync();

            if (joke.Categories.Contains(jokeCategory))
            {
                joke.Categories.Remove(jokeCategory);

                await _context.SaveChangesAsync();
            }

            return joke;
        }
    }
}

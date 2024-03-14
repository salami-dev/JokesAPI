using AutoMapper;
using JokesAPI.Data;
using JokesAPI.DTOs;
using JokesAPI.Exceptions;
using JokesAPI.Interface;
using JokesAPI.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;

namespace JokesAPI.Services
{
    public class JokeService : IJokeService
    {
        private readonly JokeContext _context;
        private readonly IMapper _mapper;
        private readonly IFetcherService _fetcherService;

        public JokeService(IMapper mapper, IFetcherService fetcherService, JokeContext context)
        {
            _context = context;
            _fetcherService = fetcherService;
            _mapper = mapper;
        }

        public async Task<Joke> Add(CreateJokeDto createJokeDto)
        {
            var joke = _mapper.Map<Joke>(createJokeDto);

            _context.Jokes.Add(joke);

            await _context.SaveChangesAsync();

            return joke;
        }

        public async Task<IEnumerable<Joke>> All()
        {
            var jokes = await _context.Jokes.ToListAsync();

            return jokes;
        }

        public async Task<bool> Delete(long id)
        {
            var joke = await _Get(id);

            _context.Jokes.Remove(joke);

            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<Joke> _Get(long id)
        {
            return await _fetcherService.GetOrThrowAsync<Joke>(q => q.Where(j => j.Id == id));
        }

        public async Task<Joke> Get(long id)
        {
            var joke = await _fetcherService.GetOrThrowAsync<Joke>(q => q.Where(j => j.Id == id).Include(j => j.Categories));

            return joke;
        }

        public async Task<Joke> GetRandomJoke()
        {
            var randomJoke = await _context.Jokes.OrderBy(j => EF.Functions.Random()).FirstOrDefaultAsync();

            if (randomJoke == null)
            {
                throw new NotFoundException("No Jokes Available.");
            }

            return randomJoke;
        }

        public async Task<bool> ReactToJoke(long id, ReactToJokeDto reaction)
        {
            var joke = await _Get(id);


            if (reaction.Action == ReactToJokeDto.ACTION_CHOICES.LIKE)
            {
                joke.Likes += 1;
            }
            else if (reaction.Action == ReactToJokeDto.ACTION_CHOICES.DISLIKE)
            {
                joke.Dislikes += 1;
            }

            var saved = false;

            while (!saved)
            {
                try
                {
                    await _context.SaveChangesAsync();
                    saved = true;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        // Refresh original values to bypass next concurrency check
                        entry.OriginalValues.SetValues(databaseValues);

                    }
                }

            }


            return true;
        }


    }
}

using JokesAPI.Data;
using JokesAPI.Exceptions;
using JokesAPI.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JokesAPI.Services
{
    public class FetcherService : IFetcherService
    {
        private readonly JokeContext _context;

        public FetcherService(JokeContext context)
        {
            _context = context;
        }

        //public async Task<T> GetOrThrowAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        //{
        //    var entity = await _context.Set<T>().FirstOrDefaultAsync(predicate);
        //    if (entity == null)
        //    {
        //        throw new NotFoundException($"{typeof(T).Name} not found.");
        //    }
        //    return entity;
        //}

        public async Task<T> GetOrThrowAsync<T>(Func<IQueryable<T>, IQueryable<T>> queryFunc) where T : class
        {
            var query = queryFunc(_context.Set<T>());
            var entity = await query.FirstOrDefaultAsync();
            if (entity == null)
            {
                throw new NotFoundException($"{typeof(T).Name} not found.");
            }

            return entity;
        }
    }
}

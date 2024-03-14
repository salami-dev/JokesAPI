namespace JokesAPI.Interface
{
    public interface IFetcherService
    {
        Task<T> GetOrThrowAsync<T>(Func<IQueryable<T>, IQueryable<T>> queryFunc) where T : class;
    }
}

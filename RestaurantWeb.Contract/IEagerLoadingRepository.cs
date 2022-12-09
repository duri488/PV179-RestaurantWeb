namespace RestaurantWeb.Contract;

public interface IEagerLoadingRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(bool includeNavigationProperty);
}
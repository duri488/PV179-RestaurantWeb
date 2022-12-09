namespace RestaurantWeb.Contract;

public interface IEagerLoadingRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(int id, bool includeNavigationProperty);
    Task<IEnumerable<TEntity>> GetAllAsync(bool includeNavigationProperty);
}
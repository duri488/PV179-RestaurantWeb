namespace RestaurantWeb.Contract;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(int id);

    Task<IEnumerable<TEntity>> GetAllAsync();

    void Insert(TEntity entity);

    Task DeleteAsync(object id);

    void Delete(TEntity entityToDelete);

    void Update(TEntity entityToUpdate);
}
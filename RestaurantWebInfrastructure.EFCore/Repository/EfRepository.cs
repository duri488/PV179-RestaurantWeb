using Microsoft.EntityFrameworkCore;
using RestaurantWeb.Contract;
using RestaurantWebDAL;

namespace RestaurantWebInfrastructure.EFCore.Repository;

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly RestaurantWebDbContext _dbContext;
    internal readonly DbSet<TEntity> DbSet;
    public EfRepository(RestaurantWebDbContext dbContext)
    {
        _dbContext = dbContext;
        DbSet = _dbContext.Set<TEntity>();
    }
    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>()
            .ToListAsync();
    }

    public void Insert(TEntity entity)
    {
        DbSet.Add(entity: entity);
    }

    public async Task DeleteAsync(object id)
    {
        TEntity entityToDelete = await DbSet.FindAsync(id);

        if (entityToDelete is null)
        {
            throw new ArgumentException($"Entity with '{id}' does not exists");
        }

        Delete(entityToDelete);
    }

    public void Delete(TEntity entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Detached)
        {
            DbSet.Attach(entity);
        }
        DbSet.Remove(entity);
    }
    public void Update(TEntity entityToUpdate)
    {
        DbSet.Attach(entityToUpdate);
        _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
    }
}
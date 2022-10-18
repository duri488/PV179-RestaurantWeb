using Microsoft.EntityFrameworkCore;
using RestaurantWebDAL;
using RestaurantWebInfrastructure.Interfaces;

namespace RestaurantWebInfrastructure.EntityFramework;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly RestaurantWebDbContext _dbContext;
    internal readonly DbSet<TEntity> DbSet;
    public Repository(RestaurantWebDbContext dbContext)
    {
        _dbContext = dbContext;
        DbSet = _dbContext.Set<TEntity>();
    }
    public TEntity GetById(int id)
    {
        return DbSet.Find(id);
    }

    public void Insert(TEntity entity)
    {
        DbSet.Add(entity: entity);
    }

    public void Delete(object id)
    {
        TEntity entityToDelete = DbSet.Find(id);
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
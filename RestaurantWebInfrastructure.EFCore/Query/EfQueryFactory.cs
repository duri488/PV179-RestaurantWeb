using RestaurantWeb.Contract;
using RestaurantWebDAL;

namespace RestaurantWebInfrastructure.EFCore.Query;

public class EfQueryFactory<TEntity> : IQueryFactory<TEntity> where TEntity : class, new()
{
    private readonly RestaurantWebDbContext _context;

    public EfQueryFactory(RestaurantWebDbContext context)
    {
        _context = context;
    }

    public IQuery<TEntity> Build()
    {
        return new EfQuery<TEntity>(_context);
    }
}
namespace RestaurantWeb.Contract;

public interface IQueryFactory<TEntity> where TEntity : class, new()
{
    IQuery<TEntity> Build();
}
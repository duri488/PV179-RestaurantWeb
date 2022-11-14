namespace RestaurantWeb.Contract
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
    }
}

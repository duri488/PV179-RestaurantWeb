namespace RestaurantWeb.Contract
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        Task CommitAsync();
    }
}

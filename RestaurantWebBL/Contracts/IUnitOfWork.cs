namespace RestaurantWebBL.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
    }
}

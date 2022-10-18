namespace RestaurantWebInfrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();
    }
}

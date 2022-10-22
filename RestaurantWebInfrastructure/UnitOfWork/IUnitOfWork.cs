namespace RestaurantWebInfrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task Commit();
    }
}

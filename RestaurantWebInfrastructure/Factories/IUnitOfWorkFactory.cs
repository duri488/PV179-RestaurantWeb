using RestaurantWebInfrastructure.UnitOfWork;

namespace RestaurantWebInfrastructure.Factories
{
    public interface IUnitOfWorkFactory
    {
        public IUnitOfWork Build();
    }
}

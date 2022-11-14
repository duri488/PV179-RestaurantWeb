using RestaurantWebDAL;
using RestaurantWebInfrastructure.EFCore.UnitOfWork;
using RestaurantWebInfrastructure.Factories;
using RestaurantWebInfrastructure.UnitOfWork;

namespace RestaurantWebInfrastructure.EFCore.Factories
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly RestaurantWebDbContext _dbcontext;
        public UnitOfWorkFactory(RestaurantWebDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IUnitOfWork Build()
        {
            return new EfUnitOfWork(_dbcontext);
        }
    }
}

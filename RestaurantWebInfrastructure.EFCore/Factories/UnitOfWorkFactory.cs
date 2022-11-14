using RestaurantWeb.Contract;
using RestaurantWebDAL;
using RestaurantWebInfrastructure.EFCore.UnitOfWork;

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

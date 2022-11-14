using Microsoft.EntityFrameworkCore.Storage;
using RestaurantWebDAL;
using RestaurantWebDAL.Models;
using RestaurantWebInfrastructure.EFCore.Repository;
using RestaurantWebInfrastructure.UnitOfWork;

namespace RestaurantWebInfrastructure.EFCore.UnitOfWork
{
    public class EfUnitOfWork : IUnitOfWork
    {
        public RestaurantWebDbContext Context { get; } = new();
        public EfRepository<DailyMenu> DailyMenuRepository { get; }
        public EfRepository<Drink> DrinkRepository { get; }
        public EfRepository<Localization> LocalizationRepository { get; }
        public EfRepository<Meal> MealRepository { get; }
        public EfRepository<User> UserRepository { get; }
        public EfRepository<WeeklyMenu> WeeklyMenuRepository { get; }
        private readonly IDbContextTransaction _transaction; 
        public EfUnitOfWork(RestaurantWebDbContext dbContext)
        {
            Context = dbContext;
            DailyMenuRepository = new EfRepository<DailyMenu>(Context);
            DrinkRepository = new EfRepository<Drink>(Context);
            LocalizationRepository = new EfRepository<Localization>(Context);
            MealRepository = new EfRepository<Meal>(Context);
            UserRepository = new EfRepository<User>(Context);
            WeeklyMenuRepository = new EfRepository<WeeklyMenu>(Context);
            _transaction = Context.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            try
            {
                await Context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await _transaction.RollbackAsync();
                throw;
            }
        }

        private bool _isDisposed = false;

        public void Dispose()
        {
            if (_isDisposed) return;
            _transaction.Dispose();
            _isDisposed = true;
        }
    }
}

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

        public EfUnitOfWork(RestaurantWebDbContext dbContext)
        {
            Context = dbContext;
            DailyMenuRepository = new EfRepository<DailyMenu>(Context);
            DrinkRepository = new EfRepository<Drink>(Context);
            LocalizationRepository = new EfRepository<Localization>(Context);
            MealRepository = new EfRepository<Meal>(Context);
            UserRepository = new EfRepository<User>(Context);
            WeeklyMenuRepository = new EfRepository<WeeklyMenu>(Context);
        }

        public async Task Commit()
        {
            await Context.SaveChangesAsync();
        }

        private bool _isDisposed = false;

        public void Dispose()
        {
            if (_isDisposed) return;

            Context.Dispose();
            GC.SuppressFinalize(this);
            _isDisposed = true;
        }
    }
}

using RestaurantWebDAL;
using RestaurantWebDAL.Models;
using RestaurantWebInfrastructure.Interfaces;

namespace RestaurantWebInfrastructure.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        public RestaurantWebDbContext Context { get; } = new();
        public Repository<DailyMenu> DailyMenuRepository { get; }
        public Repository<Drink> DrinkRepository { get; }
        public Repository<Localization> LocalizationRepository { get; }
        public Repository<Meal> MealRepository { get; }
        public Repository<User> UserRepository { get; }
        public Repository<WeeklyMenu> WeeklyMenuRepository { get; }

        public UnitOfWork(RestaurantWebDbContext dbContext)
        {
            Context = dbContext;
            DailyMenuRepository = new Repository<DailyMenu>(Context);
            DrinkRepository = new Repository<Drink>(Context);
            LocalizationRepository = new Repository<Localization>(Context);
            MealRepository = new Repository<Meal>(Context);
            UserRepository = new Repository<User>(Context);
            WeeklyMenuRepository = new Repository<WeeklyMenu>(Context);
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

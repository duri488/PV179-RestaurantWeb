using RestaurantWebDAL;
using RestaurantWebDAL.Models;

namespace RestaurantWebInfrastructure.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        public RestaurantWebDbContext Context { get; } = new();
        private Repository<DailyMenu> dailyMenuRepository;
        private Repository<Drink> drinkRepository;
        private Repository<Localization> localizationRepository;
        private Repository<Meal> mealRepository;
        private Repository<User> userRepository;
        private Repository<WeeklyMenu> weeklyMenuRepository;

        public UnitOfWork(RestaurantWebDbContext dbContext)
        {
            Context = dbContext;
        }
        
        public Repository<DailyMenu> DailyMenuRepository
        {
            get
            {
                if (this.DailyMenuRepository == null)
                {
                    this.dailyMenuRepository = new Repository<DailyMenu>(Context);
                }
                return dailyMenuRepository;
            }
        }
        
        public Repository<Drink> DrinkRepository
        {
            get
            {
                if (this.drinkRepository == null)
                {
                    this.drinkRepository = new Repository<Drink>(Context);
                }
                return drinkRepository;
            }
        }

        public Repository<Localization> LocalizationRepository
        {
            get
            {
                if (this.localizationRepository == null)
                {
                    this.localizationRepository = new Repository<Localization>(Context);
                }
                return localizationRepository;
            }
        }

        public Repository<Meal> MealRepository
        {
            get
            {
                if (this.mealRepository == null)
                {
                    this.mealRepository = new Repository<Meal>(Context);
                }
                return mealRepository;
            }
        }

        public Repository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new Repository<User>(Context);
                }
                return userRepository;
            }
        }

        public Repository<WeeklyMenu> WeeklyMenuRepository
        {
            get
            {
                if (this.weeklyMenuRepository == null)
                {
                    this.weeklyMenuRepository = new Repository<WeeklyMenu>(Context);
                }
                return weeklyMenuRepository;
            }
        }
        

        public async Task Commit()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}

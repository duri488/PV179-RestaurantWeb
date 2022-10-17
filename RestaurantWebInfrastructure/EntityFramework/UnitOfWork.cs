using RestaurantWebDAL;
using RestaurantWebDAL.Models;
using RestaurantWebInfrastructure.Interfaces;

namespace RestaurantWebInfrastructure.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        public RestaurantWebDbContext Context { get; } = new();
        private IRepository<DailyMenu> dailyMenuRepository;
        private IRepository<Drink> drinkRepository;
        private IRepository<Localization> localizationRepository;
        private IRepository<Meal> mealRepository;
        private IRepository<User> userRepository;
        private IRepository<WeeklyMenu> weeklyMenuRepository;

        public UnitOfWork(RestaurantWebDbContext dbContext)
        {
            Context = dbContext;
        }
        
        public IRepository<DailyMenu> DailyMenuRepository
        {
            get
            {
                if (this.dailyMenuRepository == null)
                {
                    this.dailyMenuRepository = new Repository<DailyMenu>(Context);
                }
                return dailyMenuRepository;
            }
        }
        
        public IRepository<Drink> DrinkRepository
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

        public IRepository<Localization> LocalizationRepository
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

        public IRepository<Meal> MealRepository
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

        public IRepository<User> UserRepository
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

        public IRepository<WeeklyMenu> WeeklyMenuRepository
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

using RestaurantWebDAL;
using RestaurantWebDAL.Models;

namespace RestaurantWebInfrastructure.UnitOfWork
{
    public class UnitOfWork
    {
        public RestaurantWebDbContext Context { get; } = new();
        private Repository<DailyMenu> DailyMenuRepository;
        private Repository<Drink> DrinkRepository;
        private Repository<Localization> LocalizationRepository;
        private Repository<Meal> MealRepository;
        private Repository<User> UserRepository;
        private Repository<WeeklyMenu> WeeklyMenuRepository;

        public UnitOfWork(RestaurantWebDbContext dbContext)
        {
            Context = dbContext;
        }
        /* 
        public Repository<DailyMenu> DailyMenuRepository
        {
            get
            {
                if (this.DailyMenuRepository == null)
                {
                    this.DailyMenuRepository = new Repository<DailyMenu>(Context);
                }
                return DailyMenuRepository;
            }
        }

        public Repository<Drink> DrinkRepository
        {
            get
            {
                if (this.DrinkRepository == null)
                {
                    this.DrinkRepository = new Repository<Drink>(Context);
                }
                return DrinkRepository;
            }
        }

        public Repository<Localization> LocalizationRepository
        {
            get
            {
                if (this.LocalizationRepository == null)
                {
                    this.LocalizationRepository = new Repository<Localization>(Context);
                }
                return LocalizationRepository;
            }
        }

        public Repository<Meal> MealRepository
        {
            get
            {
                if (this.MealRepository == null)
                {
                    this.MealRepository = new Repository<Meal>(Context);
                }
                return MealRepository;
            }
        }

        public Repository<User> UserRepository
        {
            get
            {
                if (this.UserRepository == null)
                {
                    this.UserRepository = new Repository<User>(Context);
                }
                return UserRepository;
            }
        }

        public Repository<WeeklyMenu> WeeklyMenuRepository
        {
            get
            {
                if (this.WeeklyMenuRepository == null)
                {
                    this.WeeklyMenuRepository = new Repository<WeeklyMenu>(Context);
                }
                return WeeklyMenuRepository;
            }
        }
        */

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

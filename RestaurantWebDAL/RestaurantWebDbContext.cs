using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantWebDAL.Models;

namespace RestaurantWebDAL
{
    public class RestaurantWebDbContext : IdentityDbContext<User>
    {
        public DbSet<User> User { get; set; }
        public DbSet<Meal> Meal { get; set; }
        public DbSet<Drink> Drink { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<DailyMenu> DailyMenu { get; set; }
        public DbSet<WeeklyMenu> WeeklyMenu { get; set; }
        public DbSet<Localization> Localization { get; set; }
        public DbSet<Allergen> Allergen { get; set; }


        public RestaurantWebDbContext(DbContextOptions<RestaurantWebDbContext> options) : base(options)
        {
        }

        public RestaurantWebDbContext() : base()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
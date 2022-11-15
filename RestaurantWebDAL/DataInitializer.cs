using Microsoft.EntityFrameworkCore;
using RestaurantWebDAL.Models;
using RestaurantWebUtilities.Helpers;

namespace RestaurantWebDAL;

public static class DataInitializer
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        SeedUser(modelBuilder);
        SeedRestaurant(modelBuilder);
        SeedMeal(modelBuilder);
        SeedDrink(modelBuilder);
        SeedLocalization(modelBuilder);
        SeedWeeklyMenu(modelBuilder);
        SeedDailyMenu(modelBuilder);

    }

    public static void SeedUser(this ModelBuilder modelBuilder)
    {
        byte[] salt = CryptoHashHelper.GenerateSalt();
        modelBuilder.Entity<User>()
            .HasData(
                new User
                {
                    Id = 1,
                    Username = "John Doe",
                    Salt = salt,
                    HashedPassword = CryptoHashHelper.GenerateSaltedPbkdf2Hash("password123", salt)
                },
                new User
                {
                    Id = 2,
                    Username = "Mary Jane",
                    Salt = salt,
                    HashedPassword = CryptoHashHelper.GenerateSaltedPbkdf2Hash("password123", salt)
                }
            );
    }
    public static void SeedRestaurant(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Restaurant>()
            .HasData(new Restaurant
            {
                Id = 1,
                Name = "Marco",
                Description = "Pizza from Italy",
                Address = "Hlavna 65 080 01 Presov",
                Latitude = 48.996865,
                Longtitude = 21.240334,
                Phone = "0917123456",
                Email = "marco@pizza.sk",
            });
    }
    public static void SeedMeal(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Meal>()
            .HasData(
                 new Meal
                 {
                     Id = 1,
                     Name = "Capri",
                     Description = "Tomato, Cheese, Ham, Mushrooms",
                     Price = 6.35M,
                     Picture = "path to picture"
                 },

                 new Meal
                 {
                     Id = 2,
                     Name = "Hawai",
                     Description = "Tomato, Cheese, Ham, Pineapple",
                     Price = 8.99M,
                     Picture = "path to picture"
                 }
            );

        modelBuilder.Entity<Meal>()
            .HasMany(p => p.Restaurants)
            .WithMany(p => p.Meals)
            .UsingEntity(j => j.HasData(new { MealsId = 1, RestaurantsId = 1 }));

        modelBuilder.Entity<Meal>()
            .HasMany(p => p.Restaurants)
            .WithMany(p => p.Meals)
            .UsingEntity(j => j.HasData(new { MealsId = 2, RestaurantsId = 1 }));
    }
    public static void SeedDrink(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Drink>()
            .HasData(
                 new Drink
                 {
                     Id = 1,
                     Name = "Kofola",
                     Volume = 0.1M,
                     Price = 0.3M,
                 },

                 new Drink
                 {
                     Id = 2,
                     Name = "Pilsner Urquell",
                     Volume = 0.5M,
                     Price = 2M,
                 }
            );

        modelBuilder.Entity<Drink>()
            .HasMany(p => p.Restaurants)
            .WithMany(p => p.Drinks)
            .UsingEntity(j => j.HasData(new { DrinksId = 1, RestaurantsId = 1 }));

        modelBuilder.Entity<Drink>()
            .HasMany(p => p.Restaurants)
            .WithMany(p => p.Drinks)
            .UsingEntity(j => j.HasData(new { DrinksId = 2, RestaurantsId = 1 }));
    }

    public static void SeedLocalization(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Localization>()
            .HasData(
                new Localization
                {
                    Id = 1,
                    IsoLanguageCode = "sk",
                    StringCode = "loginSk",
                    LocalizedString = "Prihlasenia"
                },
                new Localization
                {
                    Id = 2,
                    IsoLanguageCode = "en",
                    StringCode = "loginEN",
                    LocalizedString = "Login"
                }
            );
    }

    public static void SeedWeeklyMenu(this ModelBuilder modelBuilder)
    {
       
        modelBuilder.Entity<WeeklyMenu>()
            .HasData(
                new WeeklyMenu
                {
                    Id = 1,
                    DateFrom = new DateTime(2022, 10, 3),
                    DateTo = new DateTime(2022, 10, 7),
                    MealId = 1,
                    RestaurantId = 1,
                },
                new WeeklyMenu
                {
                    Id = 2,
                    DateFrom = new DateTime(2022, 10, 10),
                    DateTo = new DateTime(2022, 10, 14),
                    MealId = 2,
                    RestaurantId = 1,
                }
            );
    }

    public static void SeedDailyMenu(this ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<DailyMenu>()
            .HasData(
                new DailyMenu
                {
                    Id = 1,
                    DayOfWeek = DayOfWeek.Monday,
                    MenuPrice = 6.5M,
                    WeeklyMenuId = 1,
                    MealId = 1,
                },
                new DailyMenu
                {
                    Id = 2,
                    DayOfWeek = DayOfWeek.Tuesday,
                    MenuPrice = 7.0M,
                    WeeklyMenuId = 2,
                    MealId = 2,
                }
            );

    }

}
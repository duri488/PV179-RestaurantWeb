using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantWebDAL.Models;

namespace RestaurantWebDAL;

public static partial class DataInitializer
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
        SeedAllergen(modelBuilder);
    }

    public static void SeedUser(this ModelBuilder modelBuilder)
    {
        var hasher = new PasswordHasher<User>();

        var admin = new User()
        {
            Id = 1,
            UserName = "admin",
            FirstName = "Gordon",
            LastName = "Ramsay",
            NormalizedUserName = "ADMIN",
            Email = "lazorik.juraj@gmail.com",
            NormalizedEmail = "lazorik.juraj@gmail.com".ToUpper(),
            EmailConfirmed = true,
            SecurityStamp = string.Empty,
            PhoneNumber = "0917123456",
            PhoneNumberConfirmed = true
        };
        admin.PasswordHash = hasher.HashPassword(admin, "admin");

        modelBuilder.Entity<User>().HasData(admin);
    }

    public static void SeedRestaurant(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Restaurant>()
            .HasData(new Restaurant
            {
                Id = 1,
                Name = "Marco Restaurant",
                Description = "A pizza restaurant is a place where you can order and enjoy a delicious pizza with " +
                              "various toppings and ingredients. The restaurant typically also offers other dishes " +
                              "such as pasta, salads, sandwiches, and various drinks. Many pizza restaurants also" +
                              " offer the option to order food for delivery or pick it up for takeout. The atmosphere" +
                              " in the restaurant is usually pleasant and the staff is always willing to help with" +
                              " selecting dishes or recommending a wine pairing. A pizza restaurant is the perfect" +
                              " place for a family dinner, a gathering with friends, or just a quick bite to eat.",
                Address = "Hlavna 65 080 01 Prešov",
                Latitude = "49.2099917",
                Longitude = "16.598866360931357",
                Phone = "0917123456",
                Email = "marcorestaurant@marcorestaurant.com",
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
                    Picture = "https://i.imgur.com/BH6YDXe.jpeg",
                    RestaurantId = 1,
                    AllergenFlags = 0b1000001
                },
                new Meal
                {
                    Id = 2,
                    Name = "Hawai",
                    Description = "Tomato, Cheese, Ham, Pineapple",
                    Price = 8.99M,
                    Picture = "https://i.imgur.com/1Urm6EY.jpeg",
                    RestaurantId = 1,
                    AllergenFlags = 0b1000001
                }
            );
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
                    RestaurantId = 1
                },
                new Drink
                {
                    Id = 2,
                    Name = "Pilsner Urquell",
                    Volume = 0.5M,
                    Price = 2M,
                    RestaurantId = 1
                }
            );
    }

    private static void SeedLocalization(this ModelBuilder modelBuilder)
    {
        SeedSkGeneralLocalization(modelBuilder);
        SeedEnGeneralLocalization(modelBuilder);
        SeedSkAllergenLocalization(modelBuilder);
        SeedEnAllergenLocalization(modelBuilder);
        SeedSkRestaurantLocalization(modelBuilder);
        SeedEnRestaurantLocalization(modelBuilder);
        SeedEnDrinkLocalization(modelBuilder);
        SeedSkDrinkLocalization(modelBuilder);
        SeedEnMealLocalization(modelBuilder);
        SeedSkMealLocalization(modelBuilder);
        SeedSkLocalization(modelBuilder);
        SeedEnLocalization(modelBuilder);
        SeedSkMenuLocalization(modelBuilder);
        SeedEnMenuLocalization(modelBuilder);
    }

    private static void SeedSkLocalization(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Localization>()
        .HasData(
            new Localization
            {
                Id = 2000,
                IsoLanguageCode = "sk",
                StringCode = "localization",
                LocalizedString = "Lokalizácia"
            }
            );
    }

    private static void SeedEnLocalization(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Localization>()
        .HasData(
            new Localization
            {
                Id = 2001,
                IsoLanguageCode = "en",
                StringCode = "localization",
                LocalizedString = "Localization"
            }
    );
    }

    private static void SeedEnDrinkLocalization(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Localization>()
            .HasData(
                new Localization
                {
                    Id = 99,
                    IsoLanguageCode = "en",
                    StringCode = "create-drink",
                    LocalizedString = "Create New"
                },
                new Localization
                {
                    Id = 100,
                    IsoLanguageCode = "en",
                    StringCode = "drink-name",
                    LocalizedString = "Name"
                },
                new Localization
                {
                    Id = 101,
                    IsoLanguageCode = "en",
                    StringCode = "drink-volume",
                    LocalizedString = "Volume"
                },
                new Localization
                {
                    Id = 102,
                    IsoLanguageCode = "en",
                    StringCode = "drink-price",
                    LocalizedString = "Price"
                },
                new Localization
                {
                    Id = 103,
                    IsoLanguageCode = "en",
                    StringCode = "drink-details",
                    LocalizedString = "Details"
                },
                new Localization
                {
                    Id = 104,
                    IsoLanguageCode = "en",
                    StringCode = "drink-edit",
                    LocalizedString = "Edit"
                },
                new Localization
                {
                    Id = 105,
                    IsoLanguageCode = "en",
                    StringCode = "drink-delete",
                    LocalizedString = "Delete"
                },
                new Localization
                {
                    Id = 106,
                    IsoLanguageCode = "en",
                    StringCode = "drink-back",
                    LocalizedString = "Back"
                },
                new Localization
                {
                    Id = 1009,
                    IsoLanguageCode = "en",
                    StringCode = "drink",
                    LocalizedString = "Drink"
                },
                new Localization
                {
                    Id = 1010,
                    IsoLanguageCode = "en",
                    StringCode = "drinks",
                    LocalizedString = "Drinks"
                }
                );
    }

    private static void SeedSkDrinkLocalization(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Localization>()
            .HasData(
                new Localization
                {
                    Id = 107,
                    IsoLanguageCode = "sk",
                    StringCode = "create-drink",
                    LocalizedString = "Vytvoriť"
                },
                new Localization
                {
                    Id = 108,
                    IsoLanguageCode = "sk",
                    StringCode = "drink-name",
                    LocalizedString = "Meno"
                },
                new Localization
                {
                    Id = 109,
                    IsoLanguageCode = "sk",
                    StringCode = "drink-volume",
                    LocalizedString = "Objem"
                },
                new Localization
                {
                    Id = 110,
                    IsoLanguageCode = "sk",
                    StringCode = "drink-price",
                    LocalizedString = "Cena"
                },
                new Localization
                {
                    Id = 111,
                    IsoLanguageCode = "sk",
                    StringCode = "drink-details",
                    LocalizedString = "Podrobnosti"
                },
                new Localization
                {
                    Id = 112,
                    IsoLanguageCode = "sk",
                    StringCode = "drink-edit",
                    LocalizedString = "Upraviť"
                },
                new Localization
                {
                    Id = 113,
                    IsoLanguageCode = "sk",
                    StringCode = "drink-delete",
                    LocalizedString = "Vymazať"
                },
                new Localization
                {
                    Id = 114,
                    IsoLanguageCode = "sk",
                    StringCode = "drink-back",
                    LocalizedString = "Späť"
                },
                new Localization
                {
                    Id = 1007,
                    IsoLanguageCode = "sk",
                    StringCode = "drink",
                    LocalizedString = "Nápoj"
                },
                new Localization
                {
                    Id = 1008,
                    IsoLanguageCode = "sk",
                    StringCode = "drinks",
                    LocalizedString = "Nápoje"
                }
                );
    }

    private static void SeedEnMealLocalization(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Localization>()
            .HasData(
                new Localization
                {
                    Id = 115,
                    IsoLanguageCode = "en",
                    StringCode = "create-meal",
                    LocalizedString = "Create New"
                },
                new Localization
                {
                    Id = 116,
                    IsoLanguageCode = "en",
                    StringCode = "meal-name",
                    LocalizedString = "Name"
                },
                new Localization
                {
                    Id = 117,
                    IsoLanguageCode = "en",
                    StringCode = "meal-price",
                    LocalizedString = "Price"
                },
                new Localization
                {
                    Id = 118,
                    IsoLanguageCode = "en",
                    StringCode = "meal-details",
                    LocalizedString = "Details"
                },
                new Localization
                {
                    Id = 119,
                    IsoLanguageCode = "en",
                    StringCode = "meal-edit",
                    LocalizedString = "Edit"
                },
                new Localization
                {
                    Id = 120,
                    IsoLanguageCode = "en",
                    StringCode = "meal-delete",
                    LocalizedString = "Delete"
                },
                new Localization
                {
                    Id = 121,
                    IsoLanguageCode = "en",
                    StringCode = "meal-back",
                    LocalizedString = "Back"
                },
                new Localization
                {
                    Id = 122,
                    IsoLanguageCode = "en",
                    StringCode = "meal-description",
                    LocalizedString = "Description"
                },
                new Localization
                {
                    Id = 123,
                    IsoLanguageCode = "en",
                    StringCode = "meal-allergens",
                    LocalizedString = "Allergens"
                },
                new Localization
                {
                    Id = 124,
                    IsoLanguageCode = "en",
                    StringCode = "meal-allergensDesctption",
                    LocalizedString = " - Write number of allergen separated by space, expect for last allergen"
                },
                new Localization
                {
                    Id = 125,
                    IsoLanguageCode = "en",
                    StringCode = "meal-pictureDescription",
                    LocalizedString = " - Write link to picture"
                },
                new Localization
                {
                    Id = 1002,
                    IsoLanguageCode = "en",
                    StringCode = "meal",
                    LocalizedString = "Meals"
                },
                new Localization
                {
                    Id = 1004,
                    IsoLanguageCode = "en",
                    StringCode = "meal-single",
                    LocalizedString = "Meal"
                },
                new Localization
                {
                    Id = 1006,
                    IsoLanguageCode = "en",
                    StringCode = "meal-picture",
                    LocalizedString = "Picture"
                }
                );
    }

    private static void SeedSkMealLocalization(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Localization>()
            .HasData(
                new Localization
                {
                    Id = 126,
                    IsoLanguageCode = "sk",
                    StringCode = "create-meal",
                    LocalizedString = "Vytvoriť"
                },
                new Localization
                {
                    Id = 127,
                    IsoLanguageCode = "sk",
                    StringCode = "meal-name",
                    LocalizedString = "Meno"
                },
                new Localization
                {
                    Id = 128,
                    IsoLanguageCode = "sk",
                    StringCode = "meal-price",
                    LocalizedString = "Cena"
                },
                new Localization
                {
                    Id = 129,
                    IsoLanguageCode = "sk",
                    StringCode = "meal-details",
                    LocalizedString = "Podprobnosti"
                },
                new Localization
                {
                    Id = 130,
                    IsoLanguageCode = "sk",
                    StringCode = "meal-edit",
                    LocalizedString = "Upraviť"
                },
                new Localization
                {
                    Id = 131,
                    IsoLanguageCode = "sk",
                    StringCode = "meal-delete",
                    LocalizedString = "Vymazať"
                },
                new Localization
                {
                    Id = 132,
                    IsoLanguageCode = "sk",
                    StringCode = "meal-back",
                    LocalizedString = "Späť"
                },
                new Localization
                {
                    Id = 133,
                    IsoLanguageCode = "sk",
                    StringCode = "meal-description",
                    LocalizedString = "Popis"
                },
                new Localization
                {
                    Id = 134,
                    IsoLanguageCode = "sk",
                    StringCode = "meal-allergens",
                    LocalizedString = "Alergény"
                },
                new Localization
                {
                    Id = 135,
                    IsoLanguageCode = "sk",
                    StringCode = "meal-allergensDesctption",
                    LocalizedString = " - Napíšte číslo alergénu oddelené medzerou, okrem posledného alegénu"
                },
                new Localization
                {
                    Id = 136,
                    IsoLanguageCode = "sk",
                    StringCode = "meal-pictureDescription",
                    LocalizedString = " - Vložte link na obrázok"
                },
                new Localization
                {
                    Id = 1001,
                    IsoLanguageCode = "sk",
                    StringCode = "meal",
                    LocalizedString = "Jedlá"
                },
                new Localization
                {
                    Id = 1003,
                    IsoLanguageCode = "sk",
                    StringCode = "meal-single",
                    LocalizedString = "Jedlo"
                },
                new Localization
                {
                    Id = 1005,
                    IsoLanguageCode = "sk",
                    StringCode = "meal-picture",
                    LocalizedString = "Obrázok"
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
                },
                new WeeklyMenu
                {
                    Id = 2,
                    DateFrom = new DateTime(2022, 10, 10),
                    DateTo = new DateTime(2022, 10, 14),
                }
            );
    }

    private static void SeedDailyMenu(this ModelBuilder modelBuilder)
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
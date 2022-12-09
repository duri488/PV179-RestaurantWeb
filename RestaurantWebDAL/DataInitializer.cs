using Microsoft.EntityFrameworkCore;
using RestaurantWeb.Contract.Enums;
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
        SeedAllergen(modelBuilder);
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
                Name = "Marco Restaurant",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                Address = "Hlavna 65 080 01 Prešov",
                Latitude = 48.996865,
                Longtitude = 21.240334,
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
                    Picture = "path to picture",
                    RestaurantId = 1,
                    AllergenFlags = 0b1000001
                },
                new Meal
                {
                    Id = 2,
                    Name = "Hawai",
                    Description = "Tomato, Cheese, Ham, Pineapple",
                    Price = 8.99M,
                    Picture = "path to picture",
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

    public static void SeedLocalization(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Localization>()
            .HasData(
                new Localization
                {
                    Id = 1,
                    IsoLanguageCode = "sk",
                    StringCode = "login",
                    LocalizedString = "Prihlasenie"
                },
                new Localization
                {
                    Id = 2,
                    IsoLanguageCode = "en",
                    StringCode = "login",
                    LocalizedString = "Login"
                },
                new Localization
                {
                    Id = 3,
                    IsoLanguageCode = "sk",
                    StringCode = "gluten-number",
                    LocalizedString = "1"
                },
                new Localization
                {
                    Id = 4,
                    IsoLanguageCode = "sk",
                    StringCode = "gluten-name",
                    LocalizedString = "lepok"
                },
                new Localization
                {
                    Id = 5,
                    IsoLanguageCode = "sk",
                    StringCode = "crustaceans-number",
                    LocalizedString = "2"
                },
                new Localization
                {
                    Id = 6,
                    IsoLanguageCode = "sk",
                    StringCode = "crustaceans-name",
                    LocalizedString = "kôrovce"
                },
                new Localization
                {
                    Id = 7,
                    IsoLanguageCode = "sk",
                    StringCode = "eggs-number",
                    LocalizedString = "3"
                },
                new Localization
                {
                    Id = 8,
                    IsoLanguageCode = "sk",
                    StringCode = "eggs-name",
                    LocalizedString = "vajcia"
                },
                new Localization
                {
                    Id = 9,
                    IsoLanguageCode = "sk",
                    StringCode = "fish-number",
                    LocalizedString = "4"
                },
                new Localization
                {
                    Id = 10,
                    IsoLanguageCode = "sk",
                    StringCode = "fish-name",
                    LocalizedString = "ryby"
                },
                new Localization
                {
                    Id = 11,
                    IsoLanguageCode = "sk",
                    StringCode = "peanuts-number",
                    LocalizedString = "5"
                },
                new Localization
                {
                    Id = 12,
                    IsoLanguageCode = "sk",
                    StringCode = "peanuts-name",
                    LocalizedString = "podzemnica olejná (arašidy)"
                },
                new Localization
                {
                    Id = 13,
                    IsoLanguageCode = "sk",
                    StringCode = "soybeans-number",
                    LocalizedString = "6"
                },
                new Localization
                {
                    Id = 14,
                    IsoLanguageCode = "sk",
                    StringCode = "soybeans-name",
                    LocalizedString = "sójové bôby (sója)"
                },
                new Localization
                {
                    Id = 15,
                    IsoLanguageCode = "sk",
                    StringCode = "milk-number",
                    LocalizedString = "7"
                },
                new Localization
                {
                    Id = 16,
                    IsoLanguageCode = "sk",
                    StringCode = "milk-name",
                    LocalizedString = "mlieko"
                },
                new Localization
                {
                    Id = 17,
                    IsoLanguageCode = "sk",
                    StringCode = "nuts-number",
                    LocalizedString = "8"
                },
                new Localization
                {
                    Id = 18,
                    IsoLanguageCode = "sk",
                    StringCode = "nuts-name",
                    LocalizedString = "škrupinové plody"
                },
                new Localization
                {
                    Id = 19,
                    IsoLanguageCode = "sk",
                    StringCode = "celery-number",
                    LocalizedString = "9"
                },
                new Localization
                {
                    Id = 20,
                    IsoLanguageCode = "sk",
                    StringCode = "celery-name",
                    LocalizedString = "zeler"
                },
                new Localization
                {
                    Id = 21,
                    IsoLanguageCode = "sk",
                    StringCode = "mustard-number",
                    LocalizedString = "10"
                },
                new Localization
                {
                    Id = 22,
                    IsoLanguageCode = "sk",
                    StringCode = "mustard-name",
                    LocalizedString = "horčica"
                },
                new Localization
                {
                    Id = 23,
                    IsoLanguageCode = "sk",
                    StringCode = "sesame-seeds-number",
                    LocalizedString = "11"
                },
                new Localization
                {
                    Id = 24,
                    IsoLanguageCode = "sk",
                    StringCode = "sesame-seeds-name",
                    LocalizedString = "sezamové semená (sezam)"
                },
                new Localization
                {
                    Id = 25,
                    IsoLanguageCode = "sk",
                    StringCode = "sulfur-number",
                    LocalizedString = "12"
                },
                new Localization
                {
                    Id = 26,
                    IsoLanguageCode = "sk",
                    StringCode = "sulfur-name",
                    LocalizedString = "oxid siričitý a siričitany"
                },
                new Localization
                {
                    Id = 27,
                    IsoLanguageCode = "sk",
                    StringCode = "lupin-number",
                    LocalizedString = "13"
                },
                new Localization
                {
                    Id = 28,
                    IsoLanguageCode = "sk",
                    StringCode = "lupin-name",
                    LocalizedString = "vlčí bôb (lupina)"
                },
                new Localization
                {
                    Id = 29,
                    IsoLanguageCode = "sk",
                    StringCode = "molluscs-number",
                    LocalizedString = "14"
                },
                new Localization
                {
                    Id = 30,
                    IsoLanguageCode = "sk",
                    StringCode = "molluscs-name",
                    LocalizedString = "mäkkýše"
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

    private static void SeedAllergen(this ModelBuilder builder)
    {
        builder.Entity<Allergen>()
            .HasData(
                new Allergen
                {
                    Id = (int) Allergens.Gluten,
                    Name = "gluten",
                    NumberLocalizationCode = "gluten-number",
                    NameLocalizationCode = "gluten-name"
                },
                new Allergen
                {
                    Id = (int) Allergens.Crustaceans,
                    Name = "crustaceans",
                    NumberLocalizationCode = "crustaceans-number",
                    NameLocalizationCode = "crustaceans-name",
                },
                new Allergen
                {
                    Id = (int) Allergens.Eggs,
                    Name = "eggs",
                    NumberLocalizationCode = "eggs-number",
                    NameLocalizationCode = "eggs-name",
                },
                new Allergen
                {
                    Id = (int) Allergens.Fish,
                    Name = "fish",
                    NumberLocalizationCode = "fish-number",
                    NameLocalizationCode = "fish-name",
                },
                new Allergen
                {
                    Id = (int) Allergens.Peanuts,
                    Name = "peanuts",
                    NumberLocalizationCode = "peanuts-number",
                    NameLocalizationCode = "peanuts-name",
                },
                new Allergen
                {
                    Id = (int) Allergens.Soybeans,
                    Name = "soybeans",
                    NumberLocalizationCode = "soybeans-number",
                    NameLocalizationCode = "soybeans-name",
                },
                new Allergen
                {
                    Id = (int) Allergens.Milk,
                    Name = "milk",
                    NumberLocalizationCode = "milk-number",
                    NameLocalizationCode = "milk-name",
                },
                new Allergen
                {
                    Id = (int) Allergens.Nuts,
                    Name = "nuts",
                    NumberLocalizationCode = "nuts-number",
                    NameLocalizationCode = "nuts-name",
                },
                new Allergen
                {
                    Id = (int) Allergens.Celery,
                    Name = "celery",
                    NumberLocalizationCode = "celery-number",
                    NameLocalizationCode = "celery-name",
                },
                new Allergen
                {
                    Id = (int) Allergens.Mustard,
                    Name = "mustard",
                    NumberLocalizationCode = "mustard-number",
                    NameLocalizationCode = "mustard-name",
                },
                new Allergen
                {
                    Id = (int) Allergens.SesameSeeds,
                    Name = "sesame-seeds",
                    NumberLocalizationCode = "sesame-seeds-number",
                    NameLocalizationCode = "sesame-seeds-name",
                },
                new Allergen
                {
                    Id = (int) Allergens.Sulphur,
                    Name = "sulphur",
                    NumberLocalizationCode = "sulphur-number",
                    NameLocalizationCode = "sulphur-name",
                },
                new Allergen
                {
                    Id = (int) Allergens.Lupin,
                    Name = "lupin",
                    NumberLocalizationCode = "lupin-number",
                    NameLocalizationCode = "lupin-name",
                },
                new Allergen
                {
                    Id = (int) Allergens.Molluscs,
                    Name = "molluscs",
                    NumberLocalizationCode = "molluscs-number",
                    NameLocalizationCode = "molluscs-name",
                }
            );
    }
}
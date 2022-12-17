using Microsoft.AspNetCore.Identity;
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
                    Picture = "/Assets/capri.jpg",
                    RestaurantId = 1,
                    AllergenFlags = 0b1000001
                },
                new Meal
                {
                    Id = 2,
                    Name = "Hawai",
                    Description = "Tomato, Cheese, Ham, Pineapple",
                    Price = 8.99M,
                    Picture = "/Assets/pizza-hawai.jpg",
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
                    RestaurantId = 1,
                    AllergenFlags = 0b1000001
                },
                new Drink
                {
                    Id = 2,
                    Name = "Pilsner Urquell",
                    Volume = 0.5M,
                    Price = 2M,
                    RestaurantId = 1,
                    AllergenFlags = 0b1000001
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
                }
            );
        SeedSkAllergenLocalization(modelBuilder);
        SeedEnAllergenLocalization(modelBuilder);
    }

    private static void SeedEnAllergenLocalization(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Localization>()
            .HasData(
                new Localization
                {
                    Id = 31,
                    IsoLanguageCode = "en",
                    StringCode = "gluten-number",
                    LocalizedString = "1"
                },
                new Localization
                {
                    Id = 32,
                    IsoLanguageCode = "en",
                    StringCode = "gluten-name",
                    LocalizedString = "Cereals containing gluten"
                },
                new Localization
                {
                    Id = 33,
                    IsoLanguageCode = "en",
                    StringCode = "crustaceans-number",
                    LocalizedString = "2"
                },
                new Localization
                {
                    Id = 34,
                    IsoLanguageCode = "en",
                    StringCode = "crustaceans-name",
                    LocalizedString = "Crustaceans and products thereof"
                },
                new Localization
                {
                    Id = 35,
                    IsoLanguageCode = "en",
                    StringCode = "eggs-number",
                    LocalizedString = "3"
                },
                new Localization
                {
                    Id = 36,
                    IsoLanguageCode = "en",
                    StringCode = "eggs-name",
                    LocalizedString = "Eggs and products thereof"
                },
                new Localization
                {
                    Id = 37,
                    IsoLanguageCode = "en",
                    StringCode = "fish-number",
                    LocalizedString = "4"
                },
                new Localization
                {
                    Id = 38,
                    IsoLanguageCode = "en",
                    StringCode = "fish-name",
                    LocalizedString = "Fish and products thereof"
                },
                new Localization
                {
                    Id = 39,
                    IsoLanguageCode = "en",
                    StringCode = "peanuts-number",
                    LocalizedString = "5"
                },
                new Localization
                {
                    Id = 40,
                    IsoLanguageCode = "en",
                    StringCode = "peanuts-name",
                    LocalizedString = "Peanuts and products thereof"
                },
                new Localization
                {
                    Id = 41,
                    IsoLanguageCode = "en",
                    StringCode = "soybeans-number",
                    LocalizedString = "6"
                },
                new Localization
                {
                    Id = 42,
                    IsoLanguageCode = "en",
                    StringCode = "soybeans-name",
                    LocalizedString = "Soybeans and products thereof"
                },
                new Localization
                {
                    Id = 43,
                    IsoLanguageCode = "en",
                    StringCode = "milk-number",
                    LocalizedString = "7"
                },
                new Localization
                {
                    Id = 44,
                    IsoLanguageCode = "en",
                    StringCode = "milk-name",
                    LocalizedString = "Milk and products thereof (including lactose)"
                },
                new Localization
                {
                    Id = 45,
                    IsoLanguageCode = "en",
                    StringCode = "nuts-number",
                    LocalizedString = "8"
                },
                new Localization
                {
                    Id = 46,
                    IsoLanguageCode = "en",
                    StringCode = "nuts-name",
                    LocalizedString = "Nuts"
                },
                new Localization
                {
                    Id = 47,
                    IsoLanguageCode = "en",
                    StringCode = "celery-number",
                    LocalizedString = "9"
                },
                new Localization
                {
                    Id = 48,
                    IsoLanguageCode = "en",
                    StringCode = "celery-name",
                    LocalizedString = "Celery and products thereof"
                },
                new Localization
                {
                    Id = 49,
                    IsoLanguageCode = "en",
                    StringCode = "mustard-number",
                    LocalizedString = "10"
                },
                new Localization
                {
                    Id = 50,
                    IsoLanguageCode = "en",
                    StringCode = "mustard-name",
                    LocalizedString = "Mustard and products thereof"
                },
                new Localization
                {
                    Id = 51,
                    IsoLanguageCode = "en",
                    StringCode = "sesame-seeds-number",
                    LocalizedString = "11"
                },
                new Localization
                {
                    Id = 52,
                    IsoLanguageCode = "en",
                    StringCode = "sesame-seeds-name",
                    LocalizedString = "Sesame seeds and products thereof"
                },
                new Localization
                {
                    Id = 53,
                    IsoLanguageCode = "sk",
                    StringCode = "sulfur-number",
                    LocalizedString = "12"
                },
                new Localization
                {
                    Id = 54,
                    IsoLanguageCode = "en",
                    StringCode = "sulfur-name",
                    LocalizedString = "Sulphur dioxide and sulphites at concentrations of more than 10 mg/kg or 10 mg/litre"
                },
                new Localization
                {
                    Id = 55,
                    IsoLanguageCode = "en",
                    StringCode = "lupin-number",
                    LocalizedString = "13"
                },
                new Localization
                {
                    Id = 56,
                    IsoLanguageCode = "en",
                    StringCode = "lupin-name",
                    LocalizedString = "Lupin and products thereof"
                },
                new Localization
                {
                    Id = 57,
                    IsoLanguageCode = "sk",
                    StringCode = "molluscs-number",
                    LocalizedString = "14"
                },
                new Localization
                {
                    Id = 58,
                    IsoLanguageCode = "sk",
                    StringCode = "molluscs-name",
                    LocalizedString = "Molluscs and products thereof"
                }
            );
    }

    private static void SeedSkAllergenLocalization(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Localization>()
            .HasData(
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
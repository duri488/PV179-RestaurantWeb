using Microsoft.EntityFrameworkCore;
using RestaurantWebDAL.Models;

namespace RestaurantWebDAL;

public static partial class DataInitializer
{
    private static void SeedSkGeneralLocalization(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Localization>()
            .HasData(
                new Localization
                {
                    Id = 1,
                    IsoLanguageCode = "sk",
                    StringCode = "login",
                    LocalizedString = "Prihlásiť sa"
                },
                new Localization
                {
                    Id = 68,
                    IsoLanguageCode = "sk",
                    StringCode = "greetings",
                    LocalizedString = "Ahoj"
                },
                new Localization
                {
                    Id = 72,
                    IsoLanguageCode = "sk",
                    StringCode = "logout",
                    LocalizedString = "Odhlásiť sa"
                },
                new Localization
                {
                    Id = 75,
                    IsoLanguageCode = "sk",
                    StringCode = "home",
                    LocalizedString = "Domov"
                },
                new Localization
                {
                    Id = 76,
                    IsoLanguageCode = "sk",
                    StringCode = "daily-menu",
                    LocalizedString = "Denné menu"
                },
                new Localization
                {
                    Id = 77,
                    IsoLanguageCode = "sk",
                    StringCode = "drinks",
                    LocalizedString = "Nápoje"
                },
                new Localization
                {
                    Id = 78,
                    IsoLanguageCode = "sk",
                    StringCode = "meals",
                    LocalizedString = "Jedlá"
                },
                new Localization
                {
                    Id = 138,
                    IsoLanguageCode = "sk",
                    StringCode = "weekly-menu",
                    LocalizedString = "Týždenné menu"
                }
            );
    }

    private static void SeedEnGeneralLocalization(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Localization>()
            .HasData(
                new Localization
                {
                    Id = 2,
                    IsoLanguageCode = "en",
                    StringCode = "login",
                    LocalizedString = "Login"
                },
                new Localization
                {
                    Id = 92,
                    IsoLanguageCode = "en",
                    StringCode = "logout",
                    LocalizedString = "Logout"
                },
                new Localization
                {
                    Id = 88,
                    IsoLanguageCode = "en",
                    StringCode = "greetings",
                    LocalizedString = "Hello"
                },
                new Localization
                {
                    Id = 95,
                    IsoLanguageCode = "en",
                    StringCode = "home",
                    LocalizedString = "Home"
                },
                new Localization
                {
                    Id = 96,
                    IsoLanguageCode = "en",
                    StringCode = "daily-menu",
                    LocalizedString = "Daily menu"
                },
                new Localization
                {
                    Id = 97,
                    IsoLanguageCode = "en",
                    StringCode = "drinks",
                    LocalizedString = "Drinks"
                },
                new Localization
                {
                    Id = 98,
                    IsoLanguageCode = "en",
                    StringCode = "meals",
                    LocalizedString = "Meals"
                },
                new Localization
                {
                    Id = 137,
                    IsoLanguageCode = "en",
                    StringCode = "weekly-menu",
                    LocalizedString = "Weekly Menu"
                }
            );
    }    
}
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
                },
                new Localization
                {
                    Id = 400,
                    IsoLanguageCode = "sk",
                    StringCode = "delete",
                    LocalizedString = "Vymazať"
                },
                new Localization
                {
                    Id = 401,
                    IsoLanguageCode = "sk",
                    StringCode = "edit",
                    LocalizedString = "Upraviť"
                },
                new Localization
                {
                    Id = 402,
                    IsoLanguageCode = "sk",
                    StringCode = "back",
                    LocalizedString = "Späť"
                },
                new Localization
                {
                    Id = 403,
                    IsoLanguageCode = "sk",
                    StringCode = "details",
                    LocalizedString = "Detaily"
                },
                new Localization
                {
                    Id = 404,
                    IsoLanguageCode = "sk",
                    StringCode = "confirm-delete",
                    LocalizedString = "Ste si istý že si prajete zmazať túto položku?"
                },
                new Localization
                {
                    Id = 405,
                    IsoLanguageCode = "sk",
                    StringCode = "create",
                    LocalizedString = "Vytvoriť"
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
                },
                new Localization
                {
                    Id = 450,
                    IsoLanguageCode = "en",
                    StringCode = "delete",
                    LocalizedString = "Delete"
                },
                new Localization
                {
                    Id = 451,
                    IsoLanguageCode = "en",
                    StringCode = "edit",
                    LocalizedString = "Edit"
                },
                new Localization
                {
                    Id = 452,
                    IsoLanguageCode = "en",
                    StringCode = "back",
                    LocalizedString = "Back"
                },
                new Localization
                {
                    Id = 453,
                    IsoLanguageCode = "en",
                    StringCode = "details",
                    LocalizedString = "Details"
                },
                new Localization
                {
                    Id = 454,
                    IsoLanguageCode = "en",
                    StringCode = "confirm-delete",
                    LocalizedString = "Are you sure you wish to delete this?"
                },
                new Localization
                {
                    Id = 455,
                    IsoLanguageCode = "en",
                    StringCode = "create",
                    LocalizedString = "Create"
                }
            );
    }    
}
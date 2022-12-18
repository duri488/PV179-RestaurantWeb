using Microsoft.EntityFrameworkCore;
using RestaurantWebDAL.Models;

namespace RestaurantWebDAL;

public static partial class DataInitializer
{
    private static void SeedEnDailyMenuLocalization(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Localization>()
            .HasData(
                new Localization
                {
                    Id = 300,
                    IsoLanguageCode = "en",
                    StringCode = "daily-menu-week",
                    LocalizedString = "Week"
                },
                new Localization
                {
                    Id = 302,
                    IsoLanguageCode = "en",
                    StringCode = "daily-menu-date",
                    LocalizedString = "Date"
                },
                new Localization
                {
                    Id = 303,
                    IsoLanguageCode = "en",
                    StringCode = "daily-menu-day",
                    LocalizedString = "Day"
                },
                new Localization
                {
                    Id = 304,
                    IsoLanguageCode = "en",
                    StringCode = "daily-menu-price",
                    LocalizedString = "Price"
                },
                new Localization
                {
                    Id = 305,
                    IsoLanguageCode = "en",
                    StringCode = "daily-menu-meal",
                    LocalizedString = "Meal"
                }
                // new Localization
                // {
                //     Id = 306,
                //     IsoLanguageCode = "en",
                //     StringCode = "",
                //     LocalizedString = ""
                // },
                // new Localization
                // {
                //     Id = 307,
                //     IsoLanguageCode = "en",
                //     StringCode = "",
                //     LocalizedString = ""
                // },
                // new Localization
                // {
                //     Id = 308,
                //     IsoLanguageCode = "en",
                //     StringCode = "",
                //     LocalizedString = ""
                // },
                // new Localization
                // {
                //     Id = 309,
                //     IsoLanguageCode = "en",
                //     StringCode = "",
                //     LocalizedString = ""
                // },
                // new Localization
                // {
                //     Id = 310,
                //     IsoLanguageCode = "en",
                //     StringCode = "",
                //     LocalizedString = ""
                // },
                // new Localization
                // {
                //     Id = 311,
                //     IsoLanguageCode = "en",
                //     StringCode = "",
                //     LocalizedString = ""
                // }
            );
    }

    private static void SeedSkDailyMenuLocalization(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Localization>()
            .HasData
            (
                new Localization
                {
                    Id = 350,
                    IsoLanguageCode = "sk",
                    StringCode = "daily-menu-week",
                    LocalizedString = "Týždeň"
                },
                new Localization
                {
                    Id = 352,
                    IsoLanguageCode = "sk",
                    StringCode = "daily-menu-date",
                    LocalizedString = "Dátum"
                },
                new Localization
                {
                    Id = 353,
                    IsoLanguageCode = "sk",
                    StringCode = "daily-menu-day",
                    LocalizedString = "Deň"
                },
                new Localization
                {
                    Id = 354,
                    IsoLanguageCode = "sk",
                    StringCode = "daily-menu-price",
                    LocalizedString = "Cena"
                },
                new Localization
                {
                    Id = 355,
                    IsoLanguageCode = "sk",
                    StringCode = "daily-menu-meal",
                    LocalizedString = "Jedlo"
                }
                // new Localization
                // {
                //     Id = 356,
                //     IsoLanguageCode = "sk",
                //     StringCode = "",
                //     LocalizedString = ""
                // },
                // new Localization
                // {
                //     Id = 357,
                //     IsoLanguageCode = "sk",
                //     StringCode = "",
                //     LocalizedString = ""
                // },
                // new Localization
                // {
                //     Id = 358,
                //     IsoLanguageCode = "sk",
                //     StringCode = "",
                //     LocalizedString = ""
                // },
                // new Localization
                // {
                //     Id = 359,
                //     IsoLanguageCode = "sk",
                //     StringCode = "",
                //     LocalizedString = ""
                // },
                // new Localization
                // {
                //     Id = 360,
                //     IsoLanguageCode = "sk",
                //     StringCode = "",
                //     LocalizedString = ""
                // }
            );
    }
}
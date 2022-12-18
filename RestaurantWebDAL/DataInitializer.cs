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
                Description = "A pizza restaurant is a place where you can order and enjoy a delicious pizza with various toppings and ingredients. The restaurant typically also offers other dishes such as pasta, salads, sandwiches, and various drinks. Many pizza restaurants also offer the option to order food for delivery or pick it up for takeout. The atmosphere in the restaurant is usually pleasant and the staff is always willing to help with selecting dishes or recommending a wine pairing. A pizza restaurant is the perfect place for a family dinner, a gathering with friends, or just a quick bite to eat.",
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

    public static void SeedLocalization(this ModelBuilder modelBuilder)
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
                    Id = 2,
                    IsoLanguageCode = "en",
                    StringCode = "login",
                    LocalizedString = "Login"
                }
            );
        SeedSkAllergenLocalization(modelBuilder);
        SeedEnAllergenLocalization(modelBuilder);
        SeedSkRestaurantLocalization(modelBuilder);
        SeedEnRestaurantLocalization(modelBuilder);
        SeedEnDrinkLocalization(modelBuilder);
        SeedSkDrinkLocalization(modelBuilder);
    }

    private static void SeedEnRestaurantLocalization(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Localization>()
            .HasData(
                new Localization
                {
                    Id = 79,
                    IsoLanguageCode = "en",
                    StringCode = "update-restaurant-button",
                    LocalizedString = "Edit restaurant"
                },
                new Localization
                {
                    Id = 80,
                    IsoLanguageCode = "en",
                    StringCode = "restaurant-description",
                    LocalizedString = "A pizza restaurant is a place where you can order and enjoy a delicious pizza with various toppings and ingredients. The restaurant typically also offers other dishes such as pasta, salads, sandwiches, and various drinks. Many pizza restaurants also offer the option to order food for delivery or pick it up for takeout. The atmosphere in the restaurant is usually pleasant and the staff is always willing to help with selecting dishes or recommending a wine pairing. A pizza restaurant is the perfect place for a family dinner, a gathering with friends, or just a quick bite to eat."
                },
                new Localization
                {
                    Id = 81,
                    IsoLanguageCode = "en",
                    StringCode = "restaurant-contacts",
                    LocalizedString = "Contacts"
                },
                new Localization
                {
                    Id = 82,
                    IsoLanguageCode = "en",
                    StringCode = "restaurant-address",
                    LocalizedString = "Address"
                },
                new Localization
                {
                    Id = 83,
                    IsoLanguageCode = "en",
                    StringCode = "restaurant-phone",
                    LocalizedString = "Phone"
                },
                new Localization
                {
                    Id = 84,
                    IsoLanguageCode = "en",
                    StringCode = "email",
                    LocalizedString = "E-mail"
                },
                new Localization
                {
                    Id = 85,
                    IsoLanguageCode = "en",
                    StringCode = "restaurant-contact-us",
                    LocalizedString = "Contact us"
                },
                new Localization
                {
                    Id = 86,
                    IsoLanguageCode = "en",
                    StringCode = "name",
                    LocalizedString = "Name"
                },
                new Localization
                {
                    Id = 87,
                    IsoLanguageCode = "en",
                    StringCode = "restaurant-message",
                    LocalizedString = "Message"
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
                    Id = 89,
                    IsoLanguageCode = "en",
                    StringCode = "submit-form-button",
                    LocalizedString = "Send"
                },
                /*new Localization
                {
                    Id = 70,
                    IsoLanguageCode = "sk",
                    StringCode = "login",
                    LocalizedString = "Prihlásiť sa"
                },*/
                new Localization
                {
                    Id = 91,
                    IsoLanguageCode = "en",
                    StringCode = "password",
                    LocalizedString = "Password"
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
                    Id = 93,
                    IsoLanguageCode = "en",
                    StringCode = "contact-form-name",
                    LocalizedString = "Your name"
                },
                new Localization
                {
                    Id = 94,
                    IsoLanguageCode = "en",
                    StringCode = "contact-form-email",
                    LocalizedString = "name@mail.com"
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
                }
                );

    }

    private static void SeedSkRestaurantLocalization(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Localization>()
            .HasData(
                new Localization
                {
                    Id = 59,
                    IsoLanguageCode = "sk",
                    StringCode = "update-restaurant-button",
                    LocalizedString = "Upraviť reštauráciu"
                },
                new Localization
                {
                    Id = 60,
                    IsoLanguageCode = "sk",
                    StringCode = "restaurant-description",
                    LocalizedString = "Pizza reštaurácia je miesto, kde si môžete objednať a vychutnať si chutnú pizzu s rôznymi polevami a prísadami. Reštaurácia obvykle ponúka aj iné jedlá ako napríklad cestoviny, šaláty, sendviče a rôzne nápoje. Väčšina pizza reštaurácií poskytuje aj možnosť objednania jedla na donášku alebo si ho môžete vyzdvihnúť na prevzatie. Atmosféra v reštaurácii je príjemná a obsluha je vždy ochotná pomôcť s výberom jedál alebo poradiť s výberom vín. Pizza reštaurácia je ideálne miesto pre rodinné večere, spoločné stretnutia s priateľmi alebo len tak na rýchle občerstvenie."
                },
                new Localization
                {
                    Id = 61,
                    IsoLanguageCode = "sk",
                    StringCode = "restaurant-contacts",
                    LocalizedString = "Kontakty"
                },
                new Localization
                {
                    Id = 62,
                    IsoLanguageCode = "sk",
                    StringCode = "restaurant-address",
                    LocalizedString = "Adresa"
                },
                new Localization
                {
                    Id = 63,
                    IsoLanguageCode = "sk",
                    StringCode = "restaurant-phone",
                    LocalizedString = "Telefón"
                },
                new Localization
                {
                    Id = 64,
                    IsoLanguageCode = "sk",
                    StringCode = "email",
                    LocalizedString = "E-mail"
                },
                new Localization
                {
                    Id = 65,
                    IsoLanguageCode = "sk",
                    StringCode = "restaurant-contact-us",
                    LocalizedString = "Kontaktujte nás"
                },
                new Localization
                {
                    Id = 66,
                    IsoLanguageCode = "sk",
                    StringCode = "name",
                    LocalizedString = "Meno"
                },
                new Localization
                {
                    Id = 67,
                    IsoLanguageCode = "sk",
                    StringCode = "restaurant-message",
                    LocalizedString = "Správa"
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
                    Id = 69,
                    IsoLanguageCode = "sk",
                    StringCode = "submit-form-button",
                    LocalizedString = "Odoslať"
                },
                /*new Localization
                {
                    Id = 70,
                    IsoLanguageCode = "sk",
                    StringCode = "login",
                    LocalizedString = "Prihlásiť sa"
                },*/
                new Localization
                {
                    Id = 71,
                    IsoLanguageCode = "sk",
                    StringCode = "password",
                    LocalizedString = "Heslo"
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
                    Id = 73,
                    IsoLanguageCode = "sk",
                    StringCode = "contact-form-name",
                    LocalizedString = "Vaše meno"
                },
                new Localization
                {
                    Id = 74,
                    IsoLanguageCode = "sk",
                    StringCode = "contact-form-email",
                    LocalizedString = "meno@mail.com"
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
                    LocalizedString = "Vytvorit"
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
                    LocalizedString = "Upravit"
                },
                new Localization
                {
                    Id = 113,
                    IsoLanguageCode = "sk",
                    StringCode = "drink-delete",
                    LocalizedString = "Vymazat"
                },
                new Localization
                {
                    Id = 114,
                    IsoLanguageCode = "sk",
                    StringCode = "drink-back",
                    LocalizedString = "Spat"
                }
                );
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
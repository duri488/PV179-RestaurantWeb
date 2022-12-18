using Microsoft.EntityFrameworkCore;
using RestaurantWebDAL.Models;

namespace RestaurantWebDAL;

public static partial class DataInitializer
{
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
                    LocalizedString = "Pizza reštaurácia je miesto, kde si môžete objednať a vychutnať si chutnú " +
                                      "pizzu s rôznymi polevami a prísadami. Reštaurácia obvykle ponúka aj iné jedlá" +
                                      " ako napríklad cestoviny, šaláty, sendviče a rôzne nápoje. Väčšina pizza " +
                                      "reštaurácií poskytuje aj možnosť objednania jedla na donášku alebo si ho " +
                                      "môžete vyzdvihnúť na prevzatie. Atmosféra v reštaurácii je príjemná a obsluha" +
                                      " je vždy ochotná pomôcť s výberom jedál alebo poradiť s výberom vín. Pizza" +
                                      " reštaurácia je ideálne miesto pre rodinné večere, spoločné stretnutia s " +
                                      "priateľmi alebo len tak na rýchle občerstvenie."
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
                }
            );
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
                    LocalizedString =
                        "A pizza restaurant is a place where you can order and enjoy a delicious pizza with various " +
                        "toppings and ingredients. The restaurant typically also offers other dishes such as pasta, " +
                        "salads, sandwiches, and various drinks. Many pizza restaurants also offer the option to " +
                        "order food for delivery or pick it up for takeout. The atmosphere in the restaurant is " +
                        "usually pleasant and the staff is always willing to help with selecting dishes or" +
                        " recommending a wine pairing. A pizza restaurant is the perfect place for a family dinner," +
                        " a gathering with friends, or just a quick bite to eat."
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
                    Id = 89,
                    IsoLanguageCode = "en",
                    StringCode = "submit-form-button",
                    LocalizedString = "Send"
                },
                new Localization
                {
                    Id = 91,
                    IsoLanguageCode = "en",
                    StringCode = "password",
                    LocalizedString = "Password"
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
                }
            );
    }
}
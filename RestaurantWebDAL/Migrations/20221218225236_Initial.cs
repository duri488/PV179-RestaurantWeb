using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantWebDAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allergen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberLocalizationCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameLocalizationCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsoLanguageCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    StringCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LocalizedString = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restaurant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeeklyMenu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateFrom = table.Column<DateTime>(type: "DATE", nullable: false),
                    DateTo = table.Column<DateTime>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyMenu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drink",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drink_Restaurant_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Meal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AllergenFlags = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meal_Restaurant_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DailyMenu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    MenuPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    WeeklyMenuId = table.Column<int>(type: "int", nullable: false),
                    MealId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyMenu_Meal_MealId",
                        column: x => x.MealId,
                        principalTable: "Meal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DailyMenu_WeeklyMenu_WeeklyMenuId",
                        column: x => x.WeeklyMenuId,
                        principalTable: "WeeklyMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Allergen",
                columns: new[] { "Id", "Name", "NameLocalizationCode", "NumberLocalizationCode" },
                values: new object[,]
                {
                    { 1, "gluten", "gluten-name", "gluten-number" },
                    { 2, "crustaceans", "crustaceans-name", "crustaceans-number" },
                    { 4, "eggs", "eggs-name", "eggs-number" },
                    { 8, "fish", "fish-name", "fish-number" },
                    { 16, "peanuts", "peanuts-name", "peanuts-number" },
                    { 32, "soybeans", "soybeans-name", "soybeans-number" },
                    { 64, "milk", "milk-name", "milk-number" },
                    { 128, "nuts", "nuts-name", "nuts-number" },
                    { 256, "celery", "celery-name", "celery-number" },
                    { 512, "mustard", "mustard-name", "mustard-number" },
                    { 1024, "sesame-seeds", "sesame-seeds-name", "sesame-seeds-number" },
                    { 2048, "sulphur", "sulfur-name", "sulfur-number" },
                    { 4096, "lupin", "lupin-name", "lupin-number" },
                    { 8192, "molluscs", "molluscs-name", "molluscs-number" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3b2b98cb-5a7d-4c40-82cb-342f72014bfb", 0, "fa400435-6389-4ae8-b9a8-6bcc6331d100", "lazorik.juraj@gmail.com", true, "Gordon", "Ramsay", false, null, "LAZORIK.JURAJ@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEOKVJsSevM2wlg1c7EJcWMDMkZxa2Nvs/f1w3v68wjEszGhQnX1guG5IFDA/1LSc6w==", "0917123456", true, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "Localization",
                columns: new[] { "Id", "IsoLanguageCode", "LocalizedString", "StringCode" },
                values: new object[,]
                {
                    { 1, "sk", "Prihlásiť sa", "login" },
                    { 2, "en", "Login", "login" },
                    { 3, "sk", "1", "gluten-number" },
                    { 4, "sk", "lepok", "gluten-name" },
                    { 5, "sk", "2", "crustaceans-number" },
                    { 6, "sk", "kôrovce", "crustaceans-name" },
                    { 7, "sk", "3", "eggs-number" },
                    { 8, "sk", "vajcia", "eggs-name" },
                    { 9, "sk", "4", "fish-number" },
                    { 10, "sk", "ryby", "fish-name" },
                    { 11, "sk", "5", "peanuts-number" },
                    { 12, "sk", "podzemnica olejná (arašidy)", "peanuts-name" },
                    { 13, "sk", "6", "soybeans-number" },
                    { 14, "sk", "sójové bôby (sója)", "soybeans-name" },
                    { 15, "sk", "7", "milk-number" },
                    { 16, "sk", "mlieko", "milk-name" },
                    { 17, "sk", "8", "nuts-number" },
                    { 18, "sk", "škrupinové plody", "nuts-name" },
                    { 19, "sk", "9", "celery-number" },
                    { 20, "sk", "zeler", "celery-name" },
                    { 21, "sk", "10", "mustard-number" },
                    { 22, "sk", "horčica", "mustard-name" },
                    { 23, "sk", "11", "sesame-seeds-number" },
                    { 24, "sk", "sezamové semená (sezam)", "sesame-seeds-name" },
                    { 25, "sk", "12", "sulfur-number" },
                    { 26, "sk", "oxid siričitý a siričitany", "sulfur-name" },
                    { 27, "sk", "13", "lupin-number" },
                    { 28, "sk", "vlčí bôb (lupina)", "lupin-name" },
                    { 29, "sk", "14", "molluscs-number" },
                    { 30, "sk", "mäkkýše", "molluscs-name" },
                    { 31, "en", "1", "gluten-number" },
                    { 32, "en", "Cereals containing gluten", "gluten-name" },
                    { 33, "en", "2", "crustaceans-number" },
                    { 34, "en", "Crustaceans and products thereof", "crustaceans-name" },
                    { 35, "en", "3", "eggs-number" },
                    { 36, "en", "Eggs and products thereof", "eggs-name" },
                    { 37, "en", "4", "fish-number" },
                    { 38, "en", "Fish and products thereof", "fish-name" },
                    { 39, "en", "5", "peanuts-number" },
                    { 40, "en", "Peanuts and products thereof", "peanuts-name" },
                    { 41, "en", "6", "soybeans-number" },
                    { 42, "en", "Soybeans and products thereof", "soybeans-name" },
                    { 43, "en", "7", "milk-number" },
                    { 44, "en", "Milk and products thereof (including lactose)", "milk-name" },
                    { 45, "en", "8", "nuts-number" },
                    { 46, "en", "Nuts", "nuts-name" },
                    { 47, "en", "9", "celery-number" },
                    { 48, "en", "Celery and products thereof", "celery-name" },
                    { 49, "en", "10", "mustard-number" },
                    { 50, "en", "Mustard and products thereof", "mustard-name" },
                    { 51, "en", "11", "sesame-seeds-number" },
                    { 52, "en", "Sesame seeds and products thereof", "sesame-seeds-name" },
                    { 53, "en", "12", "sulfur-number" },
                    { 54, "en", "Sulphur dioxide and sulphites at concentrations of more than 10 mg/kg or 10 mg/litre", "sulfur-name" },
                    { 55, "en", "13", "lupin-number" },
                    { 56, "en", "Lupin and products thereof", "lupin-name" },
                    { 57, "en", "14", "molluscs-number" },
                    { 58, "en", "Molluscs and products thereof", "molluscs-name" },
                    { 59, "sk", "Upraviť reštauráciu", "update-restaurant-button" },
                    { 60, "sk", "Pizza reštaurácia je miesto, kde si môžete objednať a vychutnať si chutnú pizzu s rôznymi polevami a prísadami. Reštaurácia obvykle ponúka aj iné jedlá ako napríklad cestoviny, šaláty, sendviče a rôzne nápoje. Väčšina pizza reštaurácií poskytuje aj možnosť objednania jedla na donášku alebo si ho môžete vyzdvihnúť na prevzatie. Atmosféra v reštaurácii je príjemná a obsluha je vždy ochotná pomôcť s výberom jedál alebo poradiť s výberom vín. Pizza reštaurácia je ideálne miesto pre rodinné večere, spoločné stretnutia s priateľmi alebo len tak na rýchle občerstvenie.", "restaurant-description" },
                    { 61, "sk", "Kontakty", "restaurant-contacts" },
                    { 62, "sk", "Adresa", "restaurant-address" },
                    { 63, "sk", "Telefón", "restaurant-phone" },
                    { 64, "sk", "E-mail", "email" },
                    { 65, "sk", "Kontaktujte nás", "restaurant-contact-us" },
                    { 66, "sk", "Meno", "name" },
                    { 67, "sk", "Správa", "restaurant-message" },
                    { 68, "sk", "Ahoj", "greetings" },
                    { 69, "sk", "Odoslať", "submit-form-button" },
                    { 71, "sk", "Heslo", "password" },
                    { 72, "sk", "Odhlásiť sa", "logout" },
                    { 73, "sk", "Vaše meno", "contact-form-name" },
                    { 74, "sk", "meno@mail.com", "contact-form-email" },
                    { 75, "sk", "Domov", "home" },
                    { 76, "sk", "Denné menu", "daily-menu" },
                    { 77, "sk", "Nápoje", "drinks" },
                    { 78, "sk", "Jedlá", "meals" },
                    { 79, "en", "Edit restaurant", "update-restaurant-button" },
                    { 80, "en", "A pizza restaurant is a place where you can order and enjoy a delicious pizza with various toppings and ingredients. The restaurant typically also offers other dishes such as pasta, salads, sandwiches, and various drinks. Many pizza restaurants also offer the option to order food for delivery or pick it up for takeout. The atmosphere in the restaurant is usually pleasant and the staff is always willing to help with selecting dishes or recommending a wine pairing. A pizza restaurant is the perfect place for a family dinner, a gathering with friends, or just a quick bite to eat.", "restaurant-description" },
                    { 81, "en", "Contacts", "restaurant-contacts" },
                    { 82, "en", "Address", "restaurant-address" },
                    { 83, "en", "Phone", "restaurant-phone" },
                    { 84, "en", "E-mail", "email" },
                    { 85, "en", "Contact us", "restaurant-contact-us" },
                    { 86, "en", "Name", "name" },
                    { 87, "en", "Message", "restaurant-message" },
                    { 88, "en", "Hello", "greetings" },
                    { 89, "en", "Send", "submit-form-button" },
                    { 91, "en", "Password", "password" },
                    { 92, "en", "Logout", "logout" },
                    { 93, "en", "Your name", "contact-form-name" },
                    { 94, "en", "name@mail.com", "contact-form-email" },
                    { 95, "en", "Home", "home" },
                    { 96, "en", "Daily menu", "daily-menu" },
                    { 97, "en", "Drinks", "drinks" },
                    { 98, "en", "Meals", "meals" },
                    { 99, "en", "Create New", "create-drink" },
                    { 100, "en", "Name", "drink-name" },
                    { 101, "en", "Volume", "drink-volume" },
                    { 102, "en", "Price", "drink-price" },
                    { 103, "en", "Details", "drink-details" },
                    { 104, "en", "Edit", "drink-edit" },
                    { 105, "en", "Delete", "drink-delete" },
                    { 106, "en", "Back", "drink-back" },
                    { 107, "sk", "Vytvoriť", "create-drink" },
                    { 108, "sk", "Meno", "drink-name" },
                    { 109, "sk", "Objem", "drink-volume" },
                    { 110, "sk", "Cena", "drink-price" },
                    { 111, "sk", "Podrobnosti", "drink-details" },
                    { 112, "sk", "Upraviť", "drink-edit" },
                    { 113, "sk", "Vymazať", "drink-delete" },
                    { 114, "sk", "Späť", "drink-back" },
                    { 115, "en", "Create New", "create-meal" },
                    { 116, "en", "Name", "meal-name" },
                    { 117, "en", "Price", "meal-price" },
                    { 118, "en", "Details", "meal-details" },
                    { 119, "en", "Edit", "meal-edit" },
                    { 120, "en", "Delete", "meal-delete" },
                    { 121, "en", "Back", "meal-back" },
                    { 122, "en", "Description", "meal-description" },
                    { 123, "en", "Allergens", "meal-allergens" },
                    { 124, "en", " - Write number of allergen separated by space, expect for last allergen", "meal-allergensDesctption" },
                    { 125, "en", " - Write link to picture", "meal-pictureDescription" },
                    { 126, "sk", "Vytvoriť", "create-meal" },
                    { 127, "sk", "Meno", "meal-name" },
                    { 128, "sk", "Cena", "meal-price" },
                    { 129, "sk", "Podprobnosti", "meal-details" },
                    { 130, "sk", "Upraviť", "meal-edit" },
                    { 131, "sk", "Vymazať", "meal-delete" },
                    { 132, "sk", "Späť", "meal-back" },
                    { 133, "sk", "Popis", "meal-description" },
                    { 134, "sk", "Alergény", "meal-allergens" },
                    { 135, "sk", " - Napíšte číslo alergénu oddelené medzerou, okrem posledného alegénu", "meal-allergensDesctption" },
                    { 136, "sk", " - Vložte link na obrázok", "meal-pictureDescription" },
                    { 137, "en", "Weekly Menu", "weekly-menu" },
                    { 138, "sk", "Týždenné menu", "weekly-menu" },
                    { 300, "en", "Week", "daily-menu-week" },
                    { 302, "en", "Date", "daily-menu-date" },
                    { 303, "en", "Day", "daily-menu-day" },
                    { 304, "en", "Price", "daily-menu-price" },
                    { 305, "en", "Meal", "daily-menu-meal" },
                    { 307, "en", "Date (start - end)", "weekly-menu-date" },
                    { 308, "en", "We apologize, there are currently no weekly menus offered for this date", "no-weekly-menus" },
                    { 309, "en", "Date From", "weekly-menu-date-from" },
                    { 310, "en", "Date To", "weekly-menu-date-to" },
                    { 350, "sk", "Týždeň", "daily-menu-week" },
                    { 352, "sk", "Dátum", "daily-menu-date" },
                    { 353, "sk", "Deň", "daily-menu-day" },
                    { 354, "sk", "Cena", "daily-menu-price" },
                    { 355, "sk", "Jedlo", "daily-menu-meal" },
                    { 356, "sk", "Dátum (od - do)", "weekly-menu-date" },
                    { 357, "sk", "Ľutujeme, pre tento týždeň neponúkame žiadne meníčka.", "no-weekly-menus" },
                    { 358, "sk", "Dátum (Od)", "weekly-menu-date-from" },
                    { 359, "sk", "Dátum(Do)", "weekly-menu-date-to" },
                    { 400, "sk", "Vymazať", "delete" },
                    { 401, "sk", "Upraviť", "edit" },
                    { 402, "sk", "Späť", "back" },
                    { 403, "sk", "Detaily", "details" },
                    { 404, "sk", "Ste si istý že si prajete zmazať túto položku?", "confirm-delete" },
                    { 405, "sk", "Vytvoriť", "create" },
                    { 450, "en", "Delete", "delete" },
                    { 451, "en", "Edit", "edit" },
                    { 452, "en", "Back", "back" },
                    { 453, "en", "Details", "details" },
                    { 454, "en", "Are you sure you wish to delete this?", "confirm-delete" },
                    { 455, "en", "Create", "create" },
                    { 1001, "sk", "Jedlá", "meal" },
                    { 1002, "en", "Meals", "meal" },
                    { 1003, "sk", "Jedlo", "meal-single" },
                    { 1004, "en", "Meal", "meal-single" },
                    { 1005, "sk", "Obrázok", "meal-picture" },
                    { 1006, "en", "Picture", "meal-picture" },
                    { 1007, "sk", "Nápoj", "drink" },
                    { 1008, "sk", "Nápoje", "drinks" },
                    { 1009, "en", "Drink", "drink" },
                    { 1010, "en", "Drinks", "drinks" },
                    { 2000, "sk", "Lokalizácia", "localization" },
                    { 2001, "en", "Localization", "localization" },
                    { 2002, "sk", "Vytvoriť", "localization-create" },
                    { 2003, "en", "Create", "localization-create" },
                    { 2004, "sk", "Jazykový Kód Iso", "localization-iso" },
                    { 2005, "en", "Iso Language Code", "localization-iso" },
                    { 2006, "sk", "Kód textu", "localization-stringcode" },
                    { 2007, "en", "String code", "localization-stringcode" },
                    { 2008, "sk", "Lokalizovaný text", "localization-localizedsting" },
                    { 2009, "en", "Localized string", "localization-localizedsting" },
                    { 2010, "sk", "Vymazať", "localization-delete" },
                    { 2011, "en", "Delete", "localization-delete" },
                    { 2012, "sk", "Upraviť", "localization-edit" },
                    { 2013, "en", "Edit", "localization-edit" }
                });

            migrationBuilder.InsertData(
                table: "Restaurant",
                columns: new[] { "Id", "Address", "Description", "Email", "Latitude", "Longitude", "Name", "Phone" },
                values: new object[] { 1, "Hlavna 65 080 01 Prešov", "A pizza restaurant is a place where you can order and enjoy a delicious pizza with various toppings and ingredients. The restaurant typically also offers other dishes such as pasta, salads, sandwiches, and various drinks. Many pizza restaurants also offer the option to order food for delivery or pick it up for takeout. The atmosphere in the restaurant is usually pleasant and the staff is always willing to help with selecting dishes or recommending a wine pairing. A pizza restaurant is the perfect place for a family dinner, a gathering with friends, or just a quick bite to eat.", "marcorestaurant@marcorestaurant.com", "49.2099917", "16.598866360931357", "Marco Restaurant", "0917123456" });

            migrationBuilder.InsertData(
                table: "WeeklyMenu",
                columns: new[] { "Id", "DateFrom", "DateTo" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2022, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Drink",
                columns: new[] { "Id", "Name", "Price", "RestaurantId", "Volume" },
                values: new object[,]
                {
                    { 1, "Kofola", 0.3m, 1, 0.1m },
                    { 2, "Pilsner Urquell", 2m, 1, 0.5m }
                });

            migrationBuilder.InsertData(
                table: "Meal",
                columns: new[] { "Id", "AllergenFlags", "Description", "Name", "Picture", "Price", "RestaurantId" },
                values: new object[,]
                {
                    { 1, 65, "Tomato, Cheese, Ham, Mushrooms", "Capri", "https://i.imgur.com/BH6YDXe.jpeg", 6.35m, 1 },
                    { 2, 65, "Tomato, Cheese, Ham, Pineapple", "Hawai", "https://i.imgur.com/1Urm6EY.jpeg", 8.99m, 1 }
                });

            migrationBuilder.InsertData(
                table: "DailyMenu",
                columns: new[] { "Id", "DayOfWeek", "MealId", "MenuPrice", "WeeklyMenuId" },
                values: new object[,]
                {
                    { 1, 1, 1, 6.5m, 1 },
                    { 2, 1, 2, 6.5m, 1 },
                    { 3, 1, 2, 6.5m, 2 },
                    { 4, 2, 2, 7.0m, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allergen_NameLocalizationCode",
                table: "Allergen",
                column: "NameLocalizationCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Allergen_NumberLocalizationCode",
                table: "Allergen",
                column: "NumberLocalizationCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMenu_MealId",
                table: "DailyMenu",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMenu_WeeklyMenuId",
                table: "DailyMenu",
                column: "WeeklyMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Drink_RestaurantId",
                table: "Drink",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Meal_RestaurantId",
                table: "Meal",
                column: "RestaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allergen");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DailyMenu");

            migrationBuilder.DropTable(
                name: "Drink");

            migrationBuilder.DropTable(
                name: "Localization");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Meal");

            migrationBuilder.DropTable(
                name: "WeeklyMenu");

            migrationBuilder.DropTable(
                name: "Restaurant");
        }
    }
}

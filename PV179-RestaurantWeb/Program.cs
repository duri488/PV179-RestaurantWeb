using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PV179_RestaurantWeb.MappingProfiles;
using RestaurantWeb.Contract;
using RestaurantWebBL.Facades;
using RestaurantWebBL.Interfaces;
using RestaurantWebBL.MappingProfiles;
using RestaurantWebBL.QueryObjects;
using RestaurantWebBL.Services;
using RestaurantWebDAL;
using RestaurantWebDAL.Models;
using RestaurantWebInfrastructure.EFCore.Factories;
using RestaurantWebInfrastructure.EFCore.Query;
using RestaurantWebInfrastructure.EFCore.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RestaurantWebDbContext>(
    options => options
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        // .UseLazyLoadingProxies()
);

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<RestaurantWebDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LogoutPath = "/Identity/Logout";
    options.LoginPath = "/Identity/Login";
});

builder.Services.AddTransient<IMapper, Mapper>(x =>
    new Mapper(new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<BusinessLayerProfile>();
        cfg.AddProfile<PresentationLayerProfile>();
    })));

builder.Services.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();
builder.Services.AddTransient<IEagerLoadingRepository<DailyMenu>, EfDailyMenuRepository>();
builder.Services.AddTransient<IDailyMenuService, DailyMenuService>();
builder.Services.AddTransient<IDrinkService, DrinkService>();
builder.Services.AddTransient<IAllergenService, AllergenService>();
builder.Services.AddTransient<IRestaurantService, RestaurantService>();
builder.Services.AddTransient<IMenuFacade, MenuFacade>();
builder.Services.AddTransient<IMealService, MealService>();
builder.Services.AddTransient<IMealQueryObject, MealQueryObject>();
builder.Services.AddTransient<IWeeklyMenuService, WeeklyMenuService>();
builder.Services.AddTransient<IWeeklyMenuQueryObject, WeeklyMenuQueryObject>();
builder.Services.AddTransient<ILocalizationService, LocalizationService>();
builder.Services.AddTransient<ILocalizationQueryObject, LocalizationQueryObject>();
builder.Services.AddTransient(typeof(IQuery<>), typeof(EfQuery<>));
builder.Services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Restaurant}/{action=Index}/{id?}");

app.Run();

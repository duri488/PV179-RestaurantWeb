using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PV179_RestaurantWeb.MappingProfiles;
using RestaurantWeb.Contract;
using RestaurantWebBL.Interfaces;
using RestaurantWebBL.MappingProfiles;
using RestaurantWebBL.Services;
using RestaurantWebDAL;
using RestaurantWebDAL.Models;
using RestaurantWebInfrastructure.EFCore.Factories;
using RestaurantWebInfrastructure.EFCore.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RestaurantWebDbContext>(
    options => options
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        // .UseLazyLoadingProxies()
);

builder.Services.AddTransient<DbContext>(x => x.GetRequiredService<RestaurantWebDbContext>());

builder.Services.AddTransient<IMapper, Mapper>(x =>
    new Mapper(new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<BusinessLayerProfile>();
        cfg.AddProfile<PresentationLayerProfile>();
    })));

builder.Services.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();
builder.Services.AddTransient<IEagerLoadingRepository<DailyMenu>, EfDailyMenuRepository>();
builder.Services.AddTransient<IRepository<Drink>, EfRepository<Drink>>();
builder.Services.AddTransient<IRepository<Meal>, EfRepository<Meal>>();
builder.Services.AddTransient<IRepository<Localization>, EfRepository<Localization>>();
builder.Services.AddTransient<IRepository<Restaurant>, EfRepository<Restaurant>>();
builder.Services.AddTransient<IRepository<Allergen>, EfRepository<Allergen>>();
builder.Services.AddTransient<IDailyMenuService, DailyMenuService>();
builder.Services.AddTransient<IDrinkService, DrinkService>();
builder.Services.AddTransient<IAllergenService, AllergenService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Restaurant}/{action=Index}/{id?}");

app.Run();

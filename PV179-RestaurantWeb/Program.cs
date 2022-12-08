using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantWeb.Contract;
using RestaurantWebBL.Configs;
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
    new Mapper(new MapperConfiguration(BusinessLayerProfile.ConfigureMapping)));

builder.Services.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();
builder.Services.AddTransient<IRepository<DailyMenu>, EfRepository<DailyMenu>>();
builder.Services.AddTransient<IRepository<Drink>, EfRepository<Drink>>();
builder.Services.AddTransient<IRepository<Meal>, EfRepository<Meal>>();
builder.Services.AddTransient<IRepository<Localization>, EfRepository<Localization>>();
builder.Services.AddTransient<IRepository<Restaurant>, EfRepository<Restaurant>>();

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

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantWeb.Contract;
using RestaurantWebBL.Configs;
using RestaurantWebDAL;
using RestaurantWebInfrastructure.EFCore.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RestaurantWebDbContext>(
    options => options
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        // .UseLazyLoadingProxies()
);
builder.Services.AddTransient<IMapper, Mapper>(x =>
    new Mapper(new MapperConfiguration(BusinessMappingConfig.ConfigureMapping)));

builder.Services.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

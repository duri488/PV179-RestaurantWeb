using Microsoft.EntityFrameworkCore;
using RestaurantWebBL.Helpers;
using RestaurantWebDAL.Models;

namespace RestaurantWebDAL;

public static class DataInitializer
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        byte[] salt = CryptoHashHelper.GenerateSalt();
        modelBuilder.Entity<User>()
            .HasData(
                new User
                {
                    Id = 1,
                    Username = "John Doe",
                    Salt = salt,
                    HashedPassword = CryptoHashHelper.GenerateSaltedPbkdf2Hash("password123", salt)
                },
                new User
                {
                    Id = 2,
                    Username = "Mary Jane",
                    Salt = salt,
                    HashedPassword = CryptoHashHelper.GenerateSaltedPbkdf2Hash("password123", salt)
                }
            );

        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "Marco",
            Description = "Pizza from Italy",
            Address = "Hlavna 65 080 01 Presov",
            Latitude = 48.996865,
            Longtitude = 21.240334,
            Phone = "0917123456",
            Email = "marco@pizza.sk",
        };

        modelBuilder.Entity<Restaurant>()
            .HasData(restaurant);

        var capri = new Meal
        {
            Id = 1,
            Name = "Capri",
            Description = "Tomato, Cheese, Ham, Mushrooms",
            Price = 6.99M,
            Picture = "path to picture"
        };

        modelBuilder.Entity<Meal>()
            .HasData(capri);

        modelBuilder.Entity<Meal>()
            .HasMany(p => p.Restaurants)
            .WithMany(p => p.Meals)
            .UsingEntity(j => j.HasData(new { MealsId = 1, RestaurantsId = 1 }));

    }
}
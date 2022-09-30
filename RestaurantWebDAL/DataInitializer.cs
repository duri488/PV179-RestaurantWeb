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
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PV179_RestaurantWeb.Areas.Identity.Data;

public class AuthDbContext : IdentityDbContext<AdminUser>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }

    protected override async void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var hasher = new PasswordHasher<AdminUser>();

        var admin = new AdminUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "lazorik.juraj@gmail.com",
            FirstName = "Gordon",
            LastName = "Ramsay",
            NormalizedUserName = "lazorik.juraj@gmail.com",
            Email = "lazorik.juraj@gmail.com",
            NormalizedEmail = "lazorik.juraj@gmail.com",
            EmailConfirmed = true,
            SecurityStamp = string.Empty,
            PhoneNumber = "0917123456",
            PhoneNumberConfirmed = true
        };
        admin.PasswordHash = hasher.HashPassword(admin, "admin");


        builder.Entity<AdminUser>().HasData(admin);
    }
}

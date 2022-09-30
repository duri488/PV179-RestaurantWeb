using Microsoft.EntityFrameworkCore;
using RestaurantWebDAL.Models;
namespace RestaurantWebDAL
{
    public class RestaurantWebDbContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public RestaurantWebDbContext(DbContextOptions<RestaurantWebDbContext> options) : base(options)
        {
        }

        public RestaurantWebDbContext() : base()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
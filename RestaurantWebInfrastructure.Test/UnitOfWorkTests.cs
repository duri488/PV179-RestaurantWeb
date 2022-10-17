using Microsoft.EntityFrameworkCore;
using RestaurantWebDAL.Models;
using RestaurantWebDAL;
using RestaurantWebInfrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace RestaurantWebInfrastructure.Test
{
    public class UnitOfWorkTests
    {
        private RestaurantWebDbContext DbContext { get; set; }

        [SetUp]
        public void Setup()
        {
            string dbName = "test_database_" + DateTime.Now.ToFileTimeUtc();
            DbContextOptions<RestaurantWebDbContext> options = new DbContextOptionsBuilder<RestaurantWebDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            DbContext = new RestaurantWebDbContext(options);
        }

        [Test]
        public async Task UoWTransactionSucceed()
        {
            var pizza = new Meal { Name = "Pizza", Price = (decimal) 10.00, Picture = "Picture", Description = "mnam", Restaurants = new List<Restaurant>() };
            
            using UnitOfWork test = new(DbContext);
            {
                test.MealRepository.Insert(pizza);
                await test.Commit();
            }

            Assert.That(DbContext.Meal.Contains(pizza), Is.True);
        }
    }
}

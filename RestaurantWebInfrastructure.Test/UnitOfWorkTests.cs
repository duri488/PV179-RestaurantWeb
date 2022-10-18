using Microsoft.EntityFrameworkCore;
using RestaurantWebDAL;
using RestaurantWebDAL.Models;
using RestaurantWebInfrastructure.EntityFramework;

namespace RestaurantWebInfrastructure.Test
{
    public class UnitOfWorkTests
    {
        private RestaurantWebDbContext DbContext { get; set; }

        [SetUp]
        public void Setup()
        {
            string dbName = "test_database";
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

        [Test]
        public async Task UoWTransactionFail()
        {
            //TODO - not working properly
            string dbName = "test_database1";
            DbContextOptions<RestaurantWebDbContext> options = new DbContextOptionsBuilder<RestaurantWebDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var DbContext1 = new RestaurantWebDbContext(options);

            var pizza = new Meal {Id = 1, Name = "pizza", Picture = "Picture", Description = "mnam", Restaurants = new List<Restaurant>() };

            using UnitOfWork Unit1 = new(DbContext1);
            {
                Unit1.MealRepository.Insert(pizza);
                await Unit1.Commit();                
            }

            var DbContext2 = new RestaurantWebDbContext(options);

            var burger = new Meal { Id = 2, Name = "burger", Picture = "Picture", Description = "mnam", Restaurants = new List<Restaurant>() };

            using UnitOfWork Unit2 = new(DbContext2);
            {
                Unit2.MealRepository.Insert(burger);
                Unit2.MealRepository.Insert(pizza);
                //await Unit2.Commit();
                Assert.ThrowsAsync<ArgumentException>(async () => await Unit2.Commit()); //ok
            }

            var DbContext3 = new RestaurantWebDbContext(options);
            
            Assert.That(DbContext3.Meal.Count() == 1, Is.True); //fail
            Assert.That(DbContext3.Meal.Count() == 2, Is.True); //ok               
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RestaurantWebDAL.Models;
using RestaurantWebDAL;
using RestaurantWebInfrastructure.EntityFramework;

namespace RestaurantWebInfrastructure.Test
{
    public class QueryTests
    {
        private readonly RestaurantWebDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();

            var dbContextOptions = new DbContextOptionsBuilder<RestaurantWebDbContext>()
                            .UseInMemoryDatabase(databaseName: myDatabaseName)
                            .Options;

            var dbContext = new RestaurantWebDbContext(dbContextOptions);

            dbContext.Meal.Add(new Meal
            {
                Id = 1,
                Name = "Rezen",
                Description = "Obalovane maso",
                Picture = "obrazok",
                Price = (decimal)100.00,
                Restaurants = new List<Restaurant>()
            });
        }

        [Test]
        public void MealExists_QueryWhere_Test()
        {
            var efquery = new Query<Meal>(dbContext);
            efquery.Where<string>(a => a == "Rezen", "Name");
            var result = efquery.Execute();

            Assert.True(result.Count() == 1);

            Assert.True(result.First().Name == "Rezen");
        }
    }
}

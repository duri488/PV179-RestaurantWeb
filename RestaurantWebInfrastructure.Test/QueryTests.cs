using Microsoft.EntityFrameworkCore;
using RestaurantWebDAL.Models;
using RestaurantWebDAL;
using RestaurantWebInfrastructure.EntityFramework;

namespace RestaurantWebInfrastructure.Test
{
    public class QueryTests
    {
        private RestaurantWebDbContext DbContext { get; set; }

        [SetUp]
        public void Setup()
        {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();

            var dbContextOptions = new DbContextOptionsBuilder<RestaurantWebDbContext>()
                            .UseInMemoryDatabase(databaseName: myDatabaseName)
                            .Options;

            DbContext = new RestaurantWebDbContext(dbContextOptions);

            DbContext.Meal.Add(new Meal
            {
                Id = 1,
                Name = "Rezen",
                Description = "Obalovane maso",
                Picture = "obrazok",
                Price = (decimal)100.00,
                Restaurants = new List<Restaurant>()
            });

            DbContext.SaveChanges();
        }

        [Test]
        public void MealExists_QueryWhere_Test()
        {
            var efquery = new Query<Meal>(DbContext);
            efquery.Where<string>(a => a == "Rezen", "Name");
            var result = efquery.Execute();

            Assert.True(result.Count() == 1);
            Assert.True(result.First().Name == "Rezen");
        }

        [Test]
        public void MealExists_QueryOrderBy_Test()
        {

            DbContext.Meal.Add(new Meal
            {
                Id = 2,
                Name = "Burger",
                Description = "Maso v zemli",
                Picture = "obrazok",
                Price = (decimal)150.00,
                Restaurants = new List<Restaurant>()
            });

            DbContext.SaveChanges();

            var efquery = new Query<Meal>(DbContext);
            efquery.OrderBy<int>("Id", false);
            var result = efquery.Execute();

            Assert.True(result.Count() == 2);
            Assert.True(result.First().Name == "Burger");
        }

        [Test]
        public void MealExists_QueryPage_Test()
        {
            DbContext.Meal.Add(new Meal
            {
                Id = 2,
                Name = "Burger",
                Description = "Maso v zemli",
                Picture = "obrazok",
                Price = (decimal)150.00,
                Restaurants = new List<Restaurant>()
            });

            DbContext.SaveChanges();

            DbContext.Meal.Add(new Meal
            {
                Id = 3,
                Name = "Pizza",
                Description = "Maso v zemli",
                Picture = "obrazok",
                Price = (decimal)150.00,
                Restaurants = new List<Restaurant>()
            });

            DbContext.SaveChanges();

            var efquery = new Query<Meal>(DbContext);
            efquery.Page(1, 2);
            var result = efquery.Execute();

            Assert.True(result.Count() == 2);
            Assert.True(result.First().Name == "Rezen");
        }

    }
}

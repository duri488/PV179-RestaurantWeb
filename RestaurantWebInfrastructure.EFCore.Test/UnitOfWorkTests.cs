using Microsoft.EntityFrameworkCore;
using RestaurantWebDAL;
using RestaurantWebDAL.Models;
using RestaurantWebInfrastructure.EntityFramework;

namespace RestaurantWebInfrastructure.EFCore.Test
{
    public class UnitOfWorkTests
    {
        private RestaurantWebDbContext DbContext { get; set; }
        const string ConnectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=unittest;Integrated Security=True";
        private readonly DbContextOptions<RestaurantWebDbContext> _dbContextOptions =
            new DbContextOptionsBuilder<RestaurantWebDbContext>()
                .UseSqlServer(ConnectionString)
                .Options;

        [SetUp]
        public void Setup()
        {
            DbContext = new RestaurantWebDbContext(_dbContextOptions);
            DbContext.Database.EnsureDeleted();
            DbContext.Database.EnsureCreated();
        }

        [Test]
        public async Task UnitOfWork_Commit_HappyPath()
        {
            var pizza = new Meal { Name = "Pizza", Price = (decimal)10.00, Picture = "Picture", Description = "mnam" };

            using UnitOfWork test = new(DbContext);
            {
                test.MealRepository.Insert(pizza);
                await test.Commit();
            }

            Assert.That(DbContext.Meal.Contains(pizza), Is.True);
        }

        [Test]
        public async Task UnitOfWork_Commit_CommitFailsAndNoChangesAreWrittenToDb()
        {
            var pizza = new Meal { Name = "pizza", Picture = "Picture", Description = "mnam" };
            using (var dbContext = new RestaurantWebDbContext(_dbContextOptions))
            {
                using (UnitOfWork unitOfWork = new(dbContext))
                {
                    unitOfWork.MealRepository.Insert(pizza);
                    await unitOfWork.Commit();
                    Assert.That(await DbContext.Meal.FindAsync(pizza.Id), Is.Not.Null);
                }
            }

            await using (var dbContext = new RestaurantWebDbContext(_dbContextOptions))
            {
                using (UnitOfWork unitOfWork = new(dbContext))
                {
                    var burger = new Meal { Name = "burger", Picture = "Picture", Description = "mnam" };
                    unitOfWork.MealRepository.Insert(burger);
                    unitOfWork.MealRepository.Insert(pizza);
                    Assert.CatchAsync<Exception>(() => unitOfWork.Commit());

                    Assert.That(await dbContext.Meal.FindAsync(burger.Id), Is.Null,
                        "Transaction should fail and object should not be inserted");
                    Assert.That(await dbContext.Meal.FindAsync(pizza.Id), Is.Not.Null,
                        "Object should exist after failed transaction");
                }
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RestaurantWebDAL;
using RestaurantWebDAL.Models;
using RestaurantWebInfrastructure.EFCore.UnitOfWork;

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

            using EfUnitOfWork test = new(DbContext);
            {
                test.MealRepository.Insert(pizza);
                await test.CommitAsync();
            }

            Assert.That(DbContext.Meal.Contains(pizza), Is.True);
        }

        [Test]
        public async Task UnitOfWork_Commit_CommitFailsAndNoChangesAreWrittenToDb()
        {
            var pizza = new Meal { Name = "pizza", Picture = "Picture", Description = "mnam" };

            using (EfUnitOfWork unitOfWork = new(DbContext))
            {
                unitOfWork.MealRepository.Insert(pizza);
                await unitOfWork.CommitAsync();
                Assert.That(await DbContext.Meal.FindAsync(pizza.Id), Is.Not.Null);
            }
            
            using (EfUnitOfWork unitOfWork = new(DbContext))
            {
                var burger = new Meal { Name = "burger", Picture = "Picture", Description = "mnam" };
                unitOfWork.MealRepository.Insert(burger);
                unitOfWork.MealRepository.Insert(pizza);
                Assert.CatchAsync<Exception>(() => unitOfWork.CommitAsync());

                Assert.That(await DbContext.Meal.FindAsync(burger.Id), Is.Null,
                    "Transaction should fail and object should not be inserted");
                Assert.That(await DbContext.Meal.FindAsync(pizza.Id), Is.Not.Null,
                    "Object should exist after failed transaction");
            }
        }
    }
}

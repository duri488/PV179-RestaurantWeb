using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Constraints;
using RestaurantWebDAL.Models;
using RestaurantWebInfrastructure.EntityFramework;

namespace RestaurantWebInfrastructure.Test;

using RestaurantWebDAL;

public class Tests
{
    private Repository<Drink>? RepositoryUnderTest { get; set; }

    [SetUp]
    public void Setup()
    {
        string dbName = "test_database_" + DateTime.Now.ToFileTimeUtc();
        DbContextOptions<RestaurantWebDbContext> options = new DbContextOptionsBuilder<RestaurantWebDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
        var dbContext = new RestaurantWebDbContext(options);
        RepositoryUnderTest = new Repository<Drink>(dbContext);
    }

    [TearDown]
    public void TearDown()
    {
        RepositoryUnderTest = null;
    }

    [Test]
    public void Repository_Insert_dbSetContainsCorrectElement()
    {
        var drink = CreateTestedEntity( "Mojito", (decimal) 50.00, (decimal) 500.00);
        RepositoryUnderTest.Insert(drink);
        AssertElementStateInLocal(drink, Is.True);
    }

    [Test]
    public void Repository_DeleteByObject_removesCorrectElement()
    {
        Drink mojito = CreateTestedEntity("Mojito", (decimal) 50.00, (decimal) 500.00);
        Drink mimosa = CreateTestedEntity("Mimosa", (decimal) 50.00, (decimal) 500.00);
        RepositoryUnderTest.Insert(mojito);
        RepositoryUnderTest.Insert(mimosa);
        RepositoryUnderTest.Delete(mojito);
        AssertElementStateInLocal(mojito, Is.False);
        AssertElementStateInLocal(mimosa, Is.True);
    }
    
    [Test]
    public void Repository_DeleteById_removesCorrectElement()
    {
        Drink mojito = CreateTestedEntity("Mojito", (decimal) 50.00, (decimal) 500.00);
        Drink mimosa = CreateTestedEntity("Mimosa", (decimal) 50.00, (decimal) 500.00, 2);
        RepositoryUnderTest.Insert(mojito);
        RepositoryUnderTest.Insert(mimosa);
        RepositoryUnderTest.Delete(1);
        AssertElementStateInLocal(mojito, Is.False);
        AssertElementStateInLocal(mimosa, Is.True);
    }

    [Test]
    public void Repository_GetById_FindsInsertedElement()
    {
        Drink mojito = CreateTestedEntity("Mojito", (decimal) 50.00, (decimal) 500.00);
        RepositoryUnderTest.Insert(mojito);
        Assert.That(RepositoryUnderTest.GetById(mojito.Id), Is.EqualTo(mojito));
    }

    [Test]
    public void Repository_Update_UpdatesCorrectElement()
    {
        Drink mojito = CreateTestedEntity( "Mojito", (decimal) 50.00, (decimal) 500.00);
        RepositoryUnderTest.Insert(mojito);
        AssertElementStateInLocal(mojito, Is.True);
        Drink updated = RepositoryUnderTest.GetById(mojito.Id);
        updated.Volume = (decimal) 700.00;
        Assert.That(RepositoryUnderTest.GetById(mojito.Id), Is.EqualTo(updated), "Object was expected to be updated");
    }
    
    private static Drink CreateTestedEntity(string drinkName, decimal price, decimal volume, int id = 0)
    {
        return new Drink{Id = id, Name = drinkName, Price = price, Volume = volume, Restaurants = new List<Restaurant>()};
    }

    private void AssertElementStateInLocal(Drink entity, IResolveConstraint exists)
    {
        Assert.That(RepositoryUnderTest.DbSet.Local.Any(d => d.Name == entity.Name &&
                                                         d.Price == entity.Price &&
                                                         d.Volume == entity.Volume),
            exists);
    }
}
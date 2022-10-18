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
        var drink = new Drink{Id = 1, Name = "Mojito", Price = (decimal) 50.00, Volume = (decimal) 500.00};
        RepositoryUnderTest.Insert(drink);
        AssertElementExistsInLocalDb(drink);
    }

    [Test]
    public void Repository_DeleteByObject_removesCorrectElement()
    {
        var mojito = new Drink{Id = 1, Name = "Mojito", Price = (decimal) 50.00, Volume = (decimal) 500.00};
        var mimosa = new Drink{Id = 2, Name = "Mimosa", Price = (decimal) 50.00, Volume = (decimal) 500.00};
        RepositoryUnderTest.Insert(mojito);
        RepositoryUnderTest.Insert(mimosa);
        RepositoryUnderTest.Delete(mojito);
        AssertElementExistsInLocalDb(mojito, false);
        AssertElementExistsInLocalDb(mimosa);
    }
    
    [Test]
    public void Repository_DeleteById_removesCorrectElement()
    {
        var mojito = new Drink{Id = 1, Name = "Mojito", Price = (decimal) 50.00, Volume = (decimal) 500.00};
        var mimosa = new Drink{Id = 2, Name = "Mimosa", Price = (decimal) 50.00, Volume = (decimal) 500.00};
        RepositoryUnderTest.Insert(mojito);
        RepositoryUnderTest.Insert(mimosa);
        AssertElementExistsInLocalDb(mojito);
        RepositoryUnderTest.Delete(1);
        AssertElementExistsInLocalDb(mojito, false);
        AssertElementExistsInLocalDb(mimosa);
    }

    [Test]
    public void Repository_GetById_FindsInsertedElement()
    {
        var mojito = new Drink{Id = 1, Name = "Mojito", Price = (decimal) 50.00, Volume = (decimal) 500.00};
        RepositoryUnderTest.Insert(mojito);
        Assert.That(RepositoryUnderTest.GetById(mojito.Id), Is.EqualTo(mojito));
    }

    [Test]
    public void Repository_Update_UpdatesCorrectElement()
    {
        var mojito = new Drink{Id = 1, Name = "Mojito", Price = (decimal) 50.00, Volume = (decimal) 500.00};
        RepositoryUnderTest.Insert(mojito);
        AssertElementExistsInLocalDb(mojito);
        Drink updated = RepositoryUnderTest.GetById(mojito.Id);
        updated.Volume = (decimal) 700.00;
        Assert.That(RepositoryUnderTest.GetById(mojito.Id), Is.EqualTo(updated), "Object was expected to be updated");
    }

    private void AssertElementExistsInLocalDb(BaseEntity entity, bool isElementInDatabase = true)
    {
        IResolveConstraint nullConstraint = Is.Not.Null;
        if (!isElementInDatabase)
            nullConstraint = Is.Null;
        
        Assert.That(RepositoryUnderTest!.DbSet.Local.FirstOrDefault(e => e.Id == entity.Id), nullConstraint);
    }
}
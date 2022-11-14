using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Constraints;
using RestaurantWebDAL.Models;
using RestaurantWebInfrastructure.EFCore;

namespace RestaurantWebInfrastructure.EFCore.Test;

using RestaurantWebDAL;
using RestaurantWebInfrastructure.EFCore.Repository;

public class Tests
{
    private EfRepository<Drink>? RepositoryUnderTest { get; set; }

    [SetUp]
    public void Setup()
    {
        string dbName = "test_database_" + DateTime.Now.ToFileTimeUtc();
        DbContextOptions<RestaurantWebDbContext> options = new DbContextOptionsBuilder<RestaurantWebDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
        var dbContext = new RestaurantWebDbContext(options);
        RepositoryUnderTest = new EfRepository<Drink>(dbContext);
    }

    [TearDown]
    public void TearDown()
    {
        RepositoryUnderTest = null;
    }

    [Test]
    public void Repository_Insert_dbSetContainsCorrectElement()
    {
        if (RepositoryUnderTest is null)
            throw new InvalidOperationException("Repository not initiated!");

        var drink = new Drink { Id = 1, Name = "Mojito", Price = (decimal)50.00, Volume = (decimal)500.00 };
        RepositoryUnderTest.Insert(drink);
        AssertElementExistsInLocalDb(drink);
    }

    [Test]
    public void Repository_DeleteByObject_removesCorrectElement()
    {
        if (RepositoryUnderTest is null)
            throw new InvalidOperationException("Repository not initiated!");

        var mojito = new Drink { Id = 1, Name = "Mojito", Price = (decimal)50.00, Volume = (decimal)500.00 };
        var mimosa = new Drink { Id = 2, Name = "Mimosa", Price = (decimal)50.00, Volume = (decimal)500.00 };
        RepositoryUnderTest.Insert(mojito);
        RepositoryUnderTest.Insert(mimosa);
        RepositoryUnderTest.Delete(mojito);
        AssertElementExistsInLocalDb(mojito, false);
        AssertElementExistsInLocalDb(mimosa);
    }

    [Test]
    public async Task Repository_DeleteById_removesCorrectElement()
    {
        if (RepositoryUnderTest is null)
            throw new InvalidOperationException("Repository not initiated!");

        var mojito = new Drink { Id = 1, Name = "Mojito", Price = (decimal)50.00, Volume = (decimal)500.00 };
        var mimosa = new Drink { Id = 2, Name = "Mimosa", Price = (decimal)50.00, Volume = (decimal)500.00 };
        RepositoryUnderTest.Insert(mojito);
        RepositoryUnderTest.Insert(mimosa);
        AssertElementExistsInLocalDb(mojito);
        await RepositoryUnderTest.DeleteAsync(1);
        AssertElementExistsInLocalDb(mojito, false);
        AssertElementExistsInLocalDb(mimosa);
    }

    [Test]
    public async Task Repository_GetById_FindsInsertedElement()
    {
        if (RepositoryUnderTest is null)
            throw new InvalidOperationException("Repository not initiated!");

        var mojito = new Drink { Id = 1, Name = "Mojito", Price = (decimal)50.00, Volume = (decimal)500.00 };
        RepositoryUnderTest.Insert(mojito);
        Assert.That(await RepositoryUnderTest.GetByIdAsync(mojito.Id), Is.EqualTo(mojito));
    }

    [Test]
    public async Task Repository_Update_UpdatesCorrectElement()
    {
        if (RepositoryUnderTest is null)
            throw new InvalidOperationException("Repository not initiated!");

        var mojito = new Drink { Id = 1, Name = "Mojito", Price = (decimal)50.00, Volume = (decimal)500.00 };
        RepositoryUnderTest.Insert(mojito);
        AssertElementExistsInLocalDb(mojito);
        var updated = await RepositoryUnderTest.GetByIdAsync(mojito.Id);
        updated.Volume = (decimal)700.00;
        Assert.That(await RepositoryUnderTest.GetByIdAsync(mojito.Id), Is.EqualTo(updated), "Object was expected to be updated");
    }

    private void AssertElementExistsInLocalDb(BaseEntity entity, bool isElementInDatabase = true)
    {
        IResolveConstraint nullConstraint = Is.Not.Null;
        if (!isElementInDatabase)
            nullConstraint = Is.Null;

        Assert.That(RepositoryUnderTest!.DbSet.Local.FirstOrDefault(e => e.Id == entity.Id), nullConstraint);
    }
}
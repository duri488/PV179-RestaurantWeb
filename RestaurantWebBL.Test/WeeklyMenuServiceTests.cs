using AutoMapper;
using FluentAssertions;
using Moq;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.MappingProfiles;
using RestaurantWebBL.Services;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.Test;

public class WeeklyMenuServiceTests
{
    private Mapper _mapper = null!;
    private Mock<IRepository<WeeklyMenu>> _weeklyMenuRepositoryMock = null!;
    private Mock<IUnitOfWorkFactory> _unitOfWorkFactoryMock = null!;
    private Mock<IUnitOfWork> _unitOfWorkMock = null!;

    private Meal _meal = null!;
    private Restaurant _restaurant = null!;
    private WeeklyMenu _weeklyMenu = null!;
    private WeeklyMenuDto _weeklyMenuDto = null!;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<BusinessLayerProfile>();
        }));

        _restaurant = new Restaurant
        {
            Address = "Koblizna 30",
            Description = "Italian restaurant",
            Email = "",
            Id = 1,
            Latitude = 20.0,
            Longtitude = 20.0,
            Name = "Papa Johns",
            Phone = ""
        };

        _meal = new Meal
        {
            Description = "Bolognese sauce, pasta, minced beef-pork mix",
            Id = 1,
            Name = "Lasagne",
            Picture = "",
            Price = (decimal) 50.0,
            Restaurant = _restaurant,
            RestaurantId = _restaurant.Id
        };

        _weeklyMenu = new WeeklyMenu
        {
            DateFrom = DateTime.Today.Add(TimeSpan.FromDays(-7)),
            DateTo = DateTime.Today,
            Id = 1,
            Meal = _meal,
            MealId = _meal.Id,
            Restaurant = _restaurant,
            RestaurantId = _restaurant.Id
        };

        _weeklyMenuDto = new WeeklyMenuDto()
        {
            Id = _weeklyMenu.Id,
            DateFrom = _weeklyMenu.DateFrom,
            DateTo = _weeklyMenu.DateTo,
        };

    }
    
    [SetUp]
    public void Setup()
    {
        _weeklyMenuRepositoryMock = new Mock<IRepository<WeeklyMenu>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _unitOfWorkMock.Setup(m => m.CommitAsync()).Returns(Task.CompletedTask);
        _unitOfWorkFactoryMock = new Mock<IUnitOfWorkFactory>();
        _unitOfWorkFactoryMock.Setup(m => m.Build()).Returns(_unitOfWorkMock.Object);
    }

    [Test]
    public async Task WeeklyMenuService_CreateAsync_HappyPath()
    {
        WeeklyMenu expected = _weeklyMenu;
        WeeklyMenu actual = null!;
        _weeklyMenuRepositoryMock
            .Setup(_ => _.Insert(It.IsAny<WeeklyMenu>()))
            .Callback(new InvocationAction(i => actual = (WeeklyMenu) i.Arguments[0]));

        var service = new WeeklyMenuService(_weeklyMenuRepositoryMock.Object, _mapper, _unitOfWorkFactoryMock.Object);
        await service.CreateAsync(_weeklyMenuDto, _weeklyMenu.MealId, _weeklyMenu.RestaurantId);
        
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        AssertEqual(expected, actual);
    }

    [Test]
    public async Task WeeklyMenuService_GetByIdAsync_HappyPath()
    {
        WeeklyMenuDto expected = _weeklyMenuDto;
        _weeklyMenuRepositoryMock.Setup(m => m.GetByIdAsync(1).Result)
            .Returns(_weeklyMenu);

        var service = new WeeklyMenuService(_weeklyMenuRepositoryMock.Object, _mapper, _unitOfWorkFactoryMock.Object);
        WeeklyMenuDto? actual = await service.GetByIdAsync(1);
        
        Assert.That(actual, Is.Not.Null);
        AssertEqual(expected, actual!);
    }

    [Test]
    public async Task WeeklyMenuService_UpdateAsync_HappyPath()
    {
        WeeklyMenu expected = _weeklyMenu;
        expected.DateTo = DateTime.Today.Add(TimeSpan.FromDays(1));
        WeeklyMenu actual = null!;
        _weeklyMenuRepositoryMock.Setup(_ => _.Update(It.IsAny<WeeklyMenu>()))
            .Callback(new InvocationAction(i => actual = (WeeklyMenu) i.Arguments[0]));
        _weeklyMenuRepositoryMock.Setup(_ => _.GetByIdAsync(_weeklyMenu.Id).Result)
            .Returns(_weeklyMenu);
        
        var service = new WeeklyMenuService(_weeklyMenuRepositoryMock.Object, _mapper, _unitOfWorkFactoryMock.Object);
        await service.UpdateAsync(_weeklyMenuDto, _weeklyMenu.MealId, _weeklyMenu.RestaurantId);
        
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        AssertEqual(expected, actual);
    }

    [Test]
    public async Task WeeklyMenuService_DeleteAsync_HappyPath()
    {
        var service = new WeeklyMenuService(_weeklyMenuRepositoryMock.Object, _mapper, _unitOfWorkFactoryMock.Object);
        await service.DeleteAsync(1);
        
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        _weeklyMenuRepositoryMock.Verify(r => r.DeleteAsync(1));
    }

    [Test]
    public async Task WeeklyMenuService_GetAllAsync_HappyPath()
    {
        var expected = (IEnumerable<WeeklyMenuDto>) new []{_weeklyMenuDto};
        _weeklyMenuRepositoryMock.Setup(r => r.GetAllAsync().Result)
            .Returns(new List<WeeklyMenu> {_weeklyMenu});
        
        var service = new WeeklyMenuService(_weeklyMenuRepositoryMock.Object, _mapper, _unitOfWorkFactoryMock.Object);
        IEnumerable<WeeklyMenuDto> actual = await service.GetAllAsync();
        
        AssertEqual(expected.First(), actual.First());
    }

    private static void AssertEqual(WeeklyMenu actual, WeeklyMenu expected)
    {
        expected.Should().BeEquivalentTo(actual, options =>
            options
                .Excluding(o => o.Restaurant)
                .Excluding(o => o.Meal)
        );
    }

    private static void AssertEqual(WeeklyMenuDto actual, WeeklyMenuDto expected)
    {
        expected.Should().BeEquivalentTo(actual, options =>
            options
                .Excluding(o => o.Meal)
                .Excluding(o => o.Restaurant)
        );
    }
}
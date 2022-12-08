using AutoMapper;
using FluentAssertions;
using Moq;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.MappingProfiles;
using RestaurantWebBL.Services;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.Test;

public class DailyMenuServiceTests
{
    private Mapper _mapper = null!;
    private Mock<IRepository<DailyMenu>> _dailyMenuRepositoryMock = null!;
    private Mock<IUnitOfWorkFactory> _unitOfWorkFactoryMock = null!;
    private Mock<IUnitOfWork> _unitOfWorkMock = null!;

    private DailyMenu _dailyMenu = null!;
    private Meal _meal = null!;
    private Restaurant _restaurant = null!;
    private WeeklyMenu _weeklyMenu = null!;
    private DailyMenuDto _dailyMenuDto = null!;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _mapper = new Mapper(new MapperConfiguration(cfg => {cfg.AddProfile<BusinessLayerProfile>();}));

        _restaurant = new Restaurant
        {
            Address = "Koblizna 30",
            Description = "Italian restaurant",
            Email = "",
            Id = 1,
            Latitude = 20.0,
            Longtitude = 20.0,
            Name = "Papa Johns",
            Phone = "",
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
            DateFrom = DateTime.Now.Add(TimeSpan.FromDays(-7)),
            DateTo = DateTime.Now,
            Id = 1,
            Meal = _meal,
            MealId = _meal.Id,
            Restaurant = _restaurant,
            RestaurantId = _restaurant.Id
        };

        _dailyMenu = new DailyMenu
        {
            DayOfWeek = DayOfWeek.Monday,
            Id = 1,
            Meal = _meal,
            MealId = _meal.Id,
            MenuPrice = (decimal) 40.0,
            WeeklyMenu = _weeklyMenu,
            WeeklyMenuId = _weeklyMenu.Id
        };
        
        _dailyMenuDto = new DailyMenuDto
        {
            DayOfWeek = _dailyMenu.DayOfWeek,
            Id = _dailyMenu.Id,
            MenuPrice = _dailyMenu.MenuPrice,
        };

    }
    
    [SetUp]
    public void Setup()
    {
        _dailyMenuRepositoryMock = new Mock<IRepository<DailyMenu>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _unitOfWorkMock.Setup(m => m.CommitAsync()).Returns(Task.CompletedTask);
        _unitOfWorkFactoryMock = new Mock<IUnitOfWorkFactory>();
        _unitOfWorkFactoryMock.Setup(m => m.Build()).Returns(_unitOfWorkMock.Object);
    }

    [Test]
    public async Task DailyMenuService_CreateAsync_HappyPath()
    {
        DailyMenu expected = _dailyMenu;
        DailyMenu actual = null!;
        _dailyMenuRepositoryMock
            .Setup(_ => _.Insert(It.IsAny<DailyMenu>()))
            .Callback(new InvocationAction(i => actual = (DailyMenu) i.Arguments[0]));

        var service = new DailyMenuService(_dailyMenuRepositoryMock.Object, _mapper, _unitOfWorkFactoryMock.Object);
        await service.CreateAsync(_dailyMenuDto, _dailyMenu.MealId, _dailyMenu.WeeklyMenuId);
        
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        AssertEqual(expected, actual);
    }

    [Test]
    public async Task DailyMenuService_GetByIdAsync_HappyPath()
    {
        DailyMenuDto expected = _dailyMenuDto;
        _dailyMenuRepositoryMock.Setup(m => m.GetByIdAsync(1).Result)
            .Returns(_dailyMenu);

        var service = new DailyMenuService(_dailyMenuRepositoryMock.Object, _mapper, _unitOfWorkFactoryMock.Object);
        DailyMenuDto? actual = await service.GetByIdAsync(1);
        
        Assert.That(actual, Is.Not.Null);
        AssertEqual(expected, actual!);
    }

    [Test]
    public async Task DailyMenuService_UpdateAsync_HappyPath()
    {
        DailyMenu expected = _dailyMenu;
        expected.MenuPrice = (decimal) 10.0;
        DailyMenu actual = null!;
        _dailyMenuRepositoryMock.Setup(_ => _.Update(It.IsAny<DailyMenu>()))
            .Callback(new InvocationAction(i => actual = (DailyMenu) i.Arguments[0]));
        _dailyMenuRepositoryMock.Setup(_ => _.GetByIdAsync(_dailyMenu.Id).Result)
            .Returns(_dailyMenu);
        
        var service = new DailyMenuService(_dailyMenuRepositoryMock.Object, _mapper, _unitOfWorkFactoryMock.Object);
        await service.UpdateAsync(_dailyMenuDto, _dailyMenu.MealId, _dailyMenu.WeeklyMenuId);
        
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        expected.Should().BeEquivalentTo(actual, 
            o => o
                .Excluding(m => m.Meal)
                .Excluding(m => m.MealId)
                .Excluding(m => m.WeeklyMenu)
                .Excluding(m => m.WeeklyMenuId)
            );
    }

    [Test]
    public async Task DailyMenuService_DeleteAsync_HappyPath()
    {
        var service = new DailyMenuService(_dailyMenuRepositoryMock.Object, _mapper, _unitOfWorkFactoryMock.Object);
        await service.DeleteAsync(1);
        
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        _dailyMenuRepositoryMock.Verify(r => r.DeleteAsync(1));
    }

    [Test]
    public async Task DailyMenuService_GetAllAsync_HappyPath()
    {
        var expected = (IEnumerable<DailyMenuDto>) new []{_dailyMenuDto};
        _dailyMenuRepositoryMock.Setup(r => r.GetAllAsync().Result)
            .Returns(new List<DailyMenu> {_dailyMenu});
        
        var service = new DailyMenuService(_dailyMenuRepositoryMock.Object, _mapper, _unitOfWorkFactoryMock.Object);
        IEnumerable<DailyMenuDto> actual = await service.GetAllAsync();
        
        AssertEqual(expected.First(), actual.First());
    }

    private static void AssertEqual(DailyMenu actual, DailyMenu expected)
    {
        expected.Should().BeEquivalentTo(actual, options =>
            options
                .Excluding(o => o.Meal)
                .Excluding(o => o.WeeklyMenu)
        );
    }

    private static void AssertEqual(DailyMenuDto actual, DailyMenuDto expected)
    {
        expected.Should().BeEquivalentTo(actual, options =>
            options
                .Excluding(o => o.Meal)
                .Excluding(o => o.WeeklyMenu)
        );
    }
}
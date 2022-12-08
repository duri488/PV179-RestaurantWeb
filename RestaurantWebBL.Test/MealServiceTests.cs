using AutoMapper;
using Moq;
using RestaurantWeb.Contract;
using RestaurantWebBL.Interfaces;
using RestaurantWebBL.MappingProfiles;
using RestaurantWebBL.Services;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.Test
{
    public class MealServiceTests
    {
        private static readonly IMapper Mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<BusinessLayerProfile>();
        }));
        Mock<IRepository<Meal>> _mealRepositoryMock;
        Mock<IUnitOfWorkFactory> _unitOfWorkFactory;
        Mock<IMealQueryObject> _mealQueryObjectMock;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
            _mealRepositoryMock = new Mock<IRepository<Meal >>();
            _mealQueryObjectMock = new Mock<IMealQueryObject>();
        }

        [Test]
        public async Task MealService_GetAllAsync_HappyPathAsync()
        {
            _unitOfWorkFactory.Setup(x => x.Build().CommitAsync());
            var expected = 1;
            _mealRepositoryMock.Setup(x => x.GetAllAsync().Result)
                .Returns(new Meal[]
                {
                    new Meal
                    {
                        Id= expected,
                        Description= "text",
                        Name = "Rezen",
                        Picture = "obrazok",
                        Price = 100
                    },
                    
                    new Meal
                    {
                        Id= expected,
                        Description= "text",
                        Name = "Burger",
                        Picture = "obrazok",
                        Price = 200
                    },
                });

            var service = new MealService(_unitOfWorkFactory.Object,Mapper,_mealRepositoryMock.Object, _mealQueryObjectMock.Object);

            var actual = await service.GetAllAsync();
            Assert.That(actual.Count, Is.EqualTo(2));

        }
    }

}

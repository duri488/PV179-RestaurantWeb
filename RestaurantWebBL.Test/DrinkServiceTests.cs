using AutoMapper;
using Moq;
using RestaurantWeb.Contract;
using RestaurantWebBL.Configs;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Services;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.Test
{
    public class DrinkServiceTests
    {

        private static readonly IMapper Mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<BusinessLayerProfile>();
        }));
        Mock<IUnitOfWorkFactory> _unitOfWorkFactory;
        Mock<IRepository<Drink>> _drinkRepository;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
            _drinkRepository = new Mock<IRepository<Drink>>();
        }

        [Test]
        public async Task DrinkService_CreateAsync_HappyPathAsync()
        {
            var Id = 1;

            var drink = new Drink
            {
                Id = Id,
                Name = "Voda",
                Volume = 1.00m,
                Price = 1.50m,
            };

            var expected = new DrinkDto
            {
                Id = Id,
                Name = "Voda",
                Volume = 1.00m,
                Price = 1.50m,
            };

            _drinkRepository
                .Setup(x => x.Insert(It.IsAny<Drink>()));

            _unitOfWorkFactory.Setup(x => x.Build().CommitAsync()); //await?

            _drinkRepository
                .Setup(x => x.GetByIdAsync(Id).Result)
                .Returns(drink);

            var service = new DrinkService(_unitOfWorkFactory.Object, Mapper, _drinkRepository.Object);

            await service.CreateAsync(expected, 1);
            var actual = await service.GetByIdAsync(Id);
            Assert.Multiple(() =>
            {
                Assert.That(expected.Id, Is.EqualTo(actual.Id));
                Assert.That(expected.Name, Is.EqualTo(actual.Name));
                Assert.That(expected.Volume, Is.EqualTo(actual.Volume));
                Assert.That(expected.Price, Is.EqualTo(actual.Price));
            });

            // Verify that the method was called as expected
            _drinkRepository.Verify(x => x.Insert(It.IsAny<Drink>()), Times.Once());
        }
    }
}

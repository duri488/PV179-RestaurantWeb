using AutoMapper;
using Moq;
using RestaurantWeb.Contract;
using RestaurantWebBL.Configs;
using RestaurantWebBL.Services;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.Test
{
    internal class LocalizationServiceTests
    {

        private static readonly IMapper Mapper = new Mapper(new MapperConfiguration(BusinessMappingConfig.ConfigureMapping));
        Mock<IRepository<Localization>> _localRepositoryMock;
        Mock<IUnitOfWorkFactory> _unitOfWorkFactory;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
            _localRepositoryMock = new Mock<IRepository<Localization>>();
        }

        [Test]
        public async Task LocalizationService_GetAllWithIso_HappyPathAsync()
        {
            _unitOfWorkFactory.Setup(x => x.Build().CommitAsync());
            var expected = 1;
            _localRepositoryMock.Setup(x => x.GetAllAsync().Result)
                .Returns(new Localization[]
                {
                    new Localization
                    {
                        Id = expected,
                        IsoLanguageCode = "en",
                        StringCode = "loginEN",
                        LocalizedString = "Login"
                    },
                    new Localization
                    {
                        Id = expected,
                        IsoLanguageCode = "en",
                        StringCode = "logoutEN",
                        LocalizedString = "Logout"
                    },
                    new Localization
                    {
                        Id = expected,
                        IsoLanguageCode = "sk",
                        StringCode = "logoutSK",
                        LocalizedString = "Odhlasenie"
                    }

                });

            var service = new LocalizationService(_unitOfWorkFactory.Object, Mapper, _localRepositoryMock.Object);

            var actual = await service.GetAllWithIsoAsync("en");
            Assert.That(actual.Count, Is.EqualTo(2));

        }
    }
}

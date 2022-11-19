using AutoMapper;
using FluentAssertions;
using Moq;
using RestaurantWeb.Contract;
using RestaurantWebBL.Configs;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Services;
using RestaurantWebDAL.Models;
using System.Runtime.InteropServices;

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
        public async Task LocalizationService_CreateAsync_HappyPathAsync()
        {
            var Id = 1;

            var local = new Localization
            {
                Id = Id,
                IsoLanguageCode = "en",
                StringCode = "buttonLogin",
                LocalizedString = "Login"
            };

            var expected = new LocalizationDto
            {
                Id = Id,
                IsoLanguageCode = "en",
                StringCode = "buttonLogin",
                LocalizedString = "Login"
            };

            _localRepositoryMock
                .Setup(x => x.Insert(It.IsAny<Localization>()));

            _unitOfWorkFactory.Setup(x => x.Build().CommitAsync());

            _localRepositoryMock
                .Setup(x => x.GetByIdAsync(Id).Result)
                .Returns(local);

            var service = new LocalizationService(_unitOfWorkFactory.Object, Mapper, _localRepositoryMock.Object);

            await service.CreateAsync(expected);
            var actual = await service.GetByIdAsync(Id);
            Assert.Multiple(() =>
            {
                Assert.That(expected.Id, Is.EqualTo(actual.Id));
                Assert.That(expected.IsoLanguageCode, Is.EqualTo(actual.IsoLanguageCode));
                Assert.That(expected.StringCode, Is.EqualTo(actual.StringCode));
                Assert.That(expected.LocalizedString, Is.EqualTo(actual.LocalizedString));
            });

            _localRepositoryMock.Verify(x => x.Insert(It.IsAny<Localization>()), Times.Once());
        }


        [Test]
        public async Task LocalizationService_CreateAsync_SadPathAsync()
        {
            var Id = 1;

            _localRepositoryMock
                .Setup(x => x.GetAllAsync().Result)
                .Returns(new Localization[]
                {
                    new Localization
                    {
                        Id = Id,
                        IsoLanguageCode = "en",
                        StringCode = "buttonLogin",
                        LocalizedString = "Login"
                    },
                });

            var expectedSame = new LocalizationDto
            {
                Id = 2,
                IsoLanguageCode = "en",
                StringCode = "buttonLogin",
                LocalizedString = "LoginTest"
            };

            _localRepositoryMock
                .Setup(x => x.Insert(It.IsAny<Localization>()));

            _unitOfWorkFactory.Setup(x => x.Build().CommitAsync());

            var service = new LocalizationService(_unitOfWorkFactory.Object, Mapper, _localRepositoryMock.Object);

            Func<Task> act = async () => { await service.CreateAsync(expectedSame); };
            await act.Should().ThrowAsync<SystemException>();
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

using AutoMapper;
using FluentAssertions;
using Moq;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebBL.MappingProfiles;
using RestaurantWebBL.Services;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.Test
{
    internal class LocalizationServiceTests
    {

        private static readonly IMapper Mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<BusinessLayerProfile>();
        }));
        Mock<IRepository<Localization>> _localRepositoryMock;
        Mock<IUnitOfWorkFactory> _unitOfWorkFactory;
        Mock<ILocalizationQueryObject> _localQueryObjectMock;
        Mock<ILanguageContext> _languageContextMock;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
            _localQueryObjectMock = new Mock<ILocalizationQueryObject>();
            _localRepositoryMock = new Mock<IRepository<Localization>>();
            _languageContextMock = new Mock<ILanguageContext>();
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

            _localQueryObjectMock
                .Setup(x => x.GetStringWithCode(It.IsAny<LocalizationFilterDTOs>()))
                .Returns(new QueryResultDto<LocalizationDto>());
            
            var service = new LocalizationService(_unitOfWorkFactory.Object, Mapper, _localRepositoryMock.Object, _localQueryObjectMock.Object, _languageContextMock.Object);

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

            _localQueryObjectMock
                .Setup(x => x.GetStringWithCode(It.IsAny<LocalizationFilterDTOs>()))
                .Returns(new QueryResultDto<LocalizationDto>()
                {
                    Items = new List<LocalizationDto>{
                    new LocalizationDto
                     {
                        Id = 2,
                        IsoLanguageCode = "en",
                        StringCode = "buttonLogin",
                        LocalizedString = "LoginTest"
                    }
                }
                });

            var service = new LocalizationService(_unitOfWorkFactory.Object, Mapper, _localRepositoryMock.Object, _localQueryObjectMock.Object, _languageContextMock.Object);

            Func<Task> act = async () => { await service.CreateAsync(expectedSame); };
            await act.Should().ThrowAsync<SystemException>();
        }

        [Test]
        public async Task LocalizationService_GetAllWithIso_HappyPathAsync()
        {
            Localization actual = null!;

            _localQueryObjectMock
                .Setup(x => x.GetStringWithIso(It.IsAny<LocalizationFilterDTOs>()))
                .Returns(new QueryResultDto<LocalizationDto>(){ Items =new List<LocalizationDto>{
                    new LocalizationDto
                     {
                        Id = 2,
                        IsoLanguageCode = "en",
                        StringCode = "buttonLogin",
                        LocalizedString = "LoginTest"
                    }
                }
                });

            _localRepositoryMock
                .Setup(x => x.Insert(It.IsAny<Localization>()));

            _unitOfWorkFactory.Setup(x => x.Build().CommitAsync());

            var service = new LocalizationService(_unitOfWorkFactory.Object, Mapper, _localRepositoryMock.Object, _localQueryObjectMock.Object, _languageContextMock.Object);
            var result = service.GetAllWithIso("en");

            Assert.That(result.Count, Is.EqualTo(1));

        }
    }
}

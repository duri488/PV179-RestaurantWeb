using AutoMapper;
using Moq;
using RestaurantWeb.Contract;
using RestaurantWebBL.Configs;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebBL.Services;
using RestaurantWebDAL.Models;
using RestaurantWebUtilities.Helpers;

namespace RestaurantWebBL.Test
{
    public class UserServiceTests
    {
        private IMapper _mapper;
        private Mock<IRepository<User>> _userRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IUnitOfWorkFactory> _unitOfWorkFactoryMock;
        private Mock<IUserQueryObject> _userQueryObjectMock;

        private string username = "Hello";
        private string password = "world";

        [SetUp]
        public void Setup()
        {
            _mapper = new Mapper(new MapperConfiguration(BusinessMappingConfig.ConfigureMapping));
            _userRepositoryMock = new Mock<IRepository<User>>();
            _userQueryObjectMock = new Mock<IUserQueryObject>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(m => m.CommitAsync()).Returns(Task.CompletedTask);
            _unitOfWorkFactoryMock = new Mock<IUnitOfWorkFactory>();
            _unitOfWorkFactoryMock.Setup(m => m.Build()).Returns(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task UserService_CreateAsync_HappyPathAsync()
        {
            User actual = null!;

            _userQueryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()))
                .Returns(new QueryResultDto<UserDto>());

            _userRepositoryMock.Setup(x => x.Insert(It.IsAny<User>()))
                .Callback(new InvocationAction(i => actual = (User)i.Arguments[0]));

            var service = new UserService(_unitOfWorkFactoryMock.Object, _mapper, _userRepositoryMock.Object, _userQueryObjectMock.Object);
            await service.CreateAsync(username, password);

            var expectedHash = CryptoHashHelper.GenerateSaltedPbkdf2Hash(password, actual.Salt);
            
            Assert.That(actual.HashedPassword, Is.EqualTo(expectedHash));
            Assert.That(actual.Username, Is.EqualTo(username));
        }

        [Test]
        public void UserService_CreateAsync_SadPath()
        {
            User actual = null!;

            _userQueryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()))
                .Returns(new QueryResultDto<UserDto>() { Items = new List<UserDto>
                {
                    new UserDto()
                    {
                        Username = username,
                    }
                }});

            _userRepositoryMock.Setup(x => x.Insert(It.IsAny<User>()))
                .Callback(new InvocationAction(i => actual = (User)i.Arguments[0]));

            var service = new UserService(_unitOfWorkFactoryMock.Object, _mapper, _userRepositoryMock.Object, _userQueryObjectMock.Object);

            Assert.CatchAsync<Exception>(() => service.CreateAsync(username, password));
        }

        [Test]
        public async Task UserService_LogInAsync_HappyPathAsync()
        {
            byte[] salt = CryptoHashHelper.GenerateSalt();
            UserDto userDto = new UserDto()
            {
                Username = username,
                HashedPassword = CryptoHashHelper.GenerateSaltedPbkdf2Hash(password, salt),
                Salt = salt,
            };

            _userQueryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()))
                .Returns(new QueryResultDto<UserDto>()
                {
                    Items = new List<UserDto>
                    {
                        userDto
                    }
                });

            var service = new UserService(_unitOfWorkFactoryMock.Object, _mapper, _userRepositoryMock.Object, _userQueryObjectMock.Object);
            await service.LogInAsync(username, password);
        }

        [Test]
        public void UserService_LogInAsync_SadPathAsync_UserDoesntExists()
        {
            _userQueryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()))
                .Returns(new QueryResultDto<UserDto>());

            var service = new UserService(_unitOfWorkFactoryMock.Object, _mapper, _userRepositoryMock.Object, _userQueryObjectMock.Object);
            Assert.CatchAsync<Exception>(() => service.LogInAsync(username, password));
        }

        [Test]
        public void UserService_LogInAsync_SadPathAsync_PasswordsDoesntMatch()
        {
            byte[] salt = CryptoHashHelper.GenerateSalt();
            UserDto userDto = new UserDto()
            {
                Username = username,
                HashedPassword = CryptoHashHelper.GenerateSaltedPbkdf2Hash(password, salt),
                Salt = salt,
            };

            _userQueryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()))
                .Returns(new QueryResultDto<UserDto>()
                {
                    Items = new List<UserDto>
                {
                    userDto
                }
                });

            var service = new UserService(_unitOfWorkFactoryMock.Object, _mapper, _userRepositoryMock.Object, _userQueryObjectMock.Object);
            Assert.CatchAsync<Exception>(() => service.LogInAsync(username, "different"));
        }
    }
}

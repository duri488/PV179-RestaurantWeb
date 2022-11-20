using AutoMapper;
using Moq;
using RestaurantWeb.Contract;
using RestaurantWebBL.Configs;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebBL.Services;
using RestaurantWebDAL.Models;

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
                .Setup(x => x.ExecuteQuery(It.IsAny<UserNameFilterDto>()))
                .Returns(new QueryResultDto<UserDto>());

            _userRepositoryMock.Setup(x => x.GetAllAsync().Result)
                .Returns(new List<User>());

            _userRepositoryMock.Setup(x => x.Insert(It.IsAny<User>()))
                .Callback(new InvocationAction(i => actual = (User)i.Arguments[0]));

            var service = new UserService(_unitOfWorkFactoryMock.Object, _mapper, _userRepositoryMock.Object, _userQueryObjectMock.Object);
            await service.CreateAsync(username, password);

            ;
        }
    }
}

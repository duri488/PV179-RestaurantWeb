using AutoMapper;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;
using RestaurantWebUtilities.Helpers;

namespace RestaurantWebBL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IRepository<User> _userRepository;
        private readonly IUserQueryObject _userQueryObject;
        public UserService(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, IRepository<User> userRepository, IUserQueryObject userQueryObject)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _userRepository = userRepository;
            _mapper = mapper;
            _userQueryObject = userQueryObject;
        }

        public async Task CreateAsync(string userName, string password)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();

            var users = GetByName(userName);
            if (users.Any())
            {
                throw new Exception("Username already exists.");
            }

            byte[] salt = CryptoHashHelper.GenerateSalt();
            var userDto = new UserDto()
            {
                Username = userName,
                HashedPassword = CryptoHashHelper.GenerateSaltedPbkdf2Hash(password, salt),
                Salt = salt,
            };

            var user = _mapper.Map<User>(userDto);
            _userRepository.Insert(user);
            await unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int entityId)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
            await _userRepository.DeleteAsync(entityId);
            await unitOfWork.CommitAsync();
        }

        public async Task<UserDto?> GetByIdAsync(int entityId)
        {
            var user = await _userRepository.GetByIdAsync(entityId);
            return _mapper.Map<UserDto?>(user);
        }

        public async Task LogInAsync(string userName, string password)
        {
            var user = await Task.Run(() => GetByName(userName));
            if (!user.Any())
            {
                throw new Exception("Unknown username.");
            }

            var hashedPassword = CryptoHashHelper.GenerateSaltedPbkdf2Hash(password, user.First().Salt);
            if (!CryptoHashHelper.IsByteArrayEqual(hashedPassword, user.First().HashedPassword))
            {
                throw new Exception("Incorrect password.");
            }
        }

        public async Task UpdateAsync(int entityId, UserDto updatedEntity)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
            var updatedUser = _mapper.Map<User>(updatedEntity);
            _userRepository.Update(updatedUser);
            await unitOfWork.CommitAsync();
        }

        public IEnumerable<UserDto?> GetByName(string userName)
        {
            var result = _userQueryObject.ExecuteQuery(new UserFilterDto() { Name = userName, SortAscending = true });
            return result.Items;
        }
    }
}

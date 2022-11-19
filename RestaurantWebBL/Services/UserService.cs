using AutoMapper;
using Castle.Core.Internal;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;
using System.Xml.Linq;

namespace RestaurantWebBL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IRepository<User> _userRepository;
        public UserService(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, IRepository<User> userRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(UserDto createdEntity)
        {
            //todo username should be unique
            using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();

            var users = await _userRepository.GetAllAsync();
            var userInDb = users.Where(x => x.Username == createdEntity.Username);

            if (userInDb.Any())
            {
                throw new Exception("Username already exists.");
            }

            var user = _mapper.Map<User>(createdEntity);
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

        public async Task LogInAsync(UserDto logInEntity)
        {
            var users = await _userRepository.GetAllAsync();
            var userInDb = users.Where(x => x.Username == logInEntity.Username);

            if (userInDb.IsNullOrEmpty())
            {
                throw new Exception("Unknown username.");
            }

            if (logInEntity.HashedPassword != userInDb.First().HashedPassword)
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
    }
}

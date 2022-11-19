using AutoMapper;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebBL.Services
{
    internal class RestaurantService : IRestaurantService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IRepository<Restaurant> _restaurantRepository;

        public RestaurantService(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, IRepository<Restaurant> restaurantRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(RestaurantDto createdEntity)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
            var restaurant = _mapper.Map<Restaurant>(createdEntity);
            _restaurantRepository.Insert(restaurant);
            await unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int entityId)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
            await _restaurantRepository.DeleteAsync(entityId);
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
        {
            var restaurant = await _restaurantRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RestaurantDto>>(restaurant);
        }

        public async Task<RestaurantDto?> GetByIdAsync(int entityId)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(entityId);
            return _mapper.Map<RestaurantDto?>(restaurant);
        }

        public async Task UpdateAsync(int entityId, RestaurantDto updatedEntity)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
            var updatedRestaurant = _mapper.Map<Restaurant>(updatedEntity);
            _restaurantRepository.Update(updatedRestaurant);
            await unitOfWork.CommitAsync();
        }
    }
}

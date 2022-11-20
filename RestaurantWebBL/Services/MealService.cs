using AutoMapper;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebBL.QueryObjects;
using RestaurantWebDAL.Models;
using System.Diagnostics;

namespace RestaurantWebBL.Services
{
    public class MealService : IMealService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IRepository<Meal> _mealRepository;
        private readonly IMealQueryObject _mealQueryObject;

        public MealService(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, IRepository<Meal> mealRepository, IMealQueryObject mealQueryObject)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _mealRepository = mealRepository;
            _mapper = mapper;
            _mealQueryObject = mealQueryObject;
        }

        public async Task CreateAsync(MealDto createdEntity, int restaurantId)
        {
            if (createdEntity.Restaurant != null)
            {
                throw new SystemException();
            }
            using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
            var meal = _mapper.Map<Meal>(createdEntity);
            meal.RestaurantId = restaurantId;
            _mealRepository.Insert(meal);
            await unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int entityId)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
            await _mealRepository.DeleteAsync(entityId);
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<MealDto>> GetAllAsync()
        {
            var meals = await _mealRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MealDto>>(meals);
        }

        public async Task<MealDto?> GetByIdAsync(int entityId)
        {
            var meal = await _mealRepository.GetByIdAsync(entityId);
            return _mapper.Map<MealDto?>(meal);
        }

        public async Task UpdateAsync(MealDto updatedEntity, int restaurantId)
        {
            if (updatedEntity.Restaurant != null)
            {
                throw new SystemException();
            }
            using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
            var updatedMeal = _mapper.Map<Meal>(updatedEntity);
            updatedMeal.RestaurantId = restaurantId;
            _mealRepository.Update(updatedMeal);
            await unitOfWork.CommitAsync();
        }

        public IEnumerable<MealDto> GetMealsPriceIsBigger(decimal price)
        {
            var localizationsISO = _mealQueryObject.GetMealByPrice(new MealFilterDTOs() { Price = price }, 1);
            return localizationsISO.Items;
        }

        public IEnumerable<MealDto> GetMealsPriceIsLower(decimal price)
        {
            var localizationsISO = _mealQueryObject.GetMealByPrice(new MealFilterDTOs() { Price = price }, 0);
            return localizationsISO.Items;
        }
    }
}

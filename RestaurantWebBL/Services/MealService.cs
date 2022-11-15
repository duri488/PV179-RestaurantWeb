﻿using AutoMapper;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;
using System.Diagnostics;

namespace RestaurantWebBL.Services
{
    public class MealService : IMealService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IRepository<Meal> _mealRepository;

        public MealService(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, IRepository<Meal> mealRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _mealRepository = mealRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(MealDto createdEntity)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
            var meal = _mapper.Map<Meal>(createdEntity);
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

        public async Task UpdateAsync(int entityId, MealDto updatedEntity)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
            var updatedMeal = _mapper.Map<Meal>(updatedEntity);
            _mealRepository.Update(updatedMeal);
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<MealDto>> GetMealsPriceIsBigger(decimal price)
        {
            var meals = await _mealRepository.GetAllAsync();
            var mealsBigger = meals.Where(x => x.Price > price);
            return _mapper.Map<IEnumerable<MealDto>>(mealsBigger);
        }

        public async Task<IEnumerable<MealDto>> GetMealsPriceIsLower(decimal price)
        {
            var meals = await _mealRepository.GetAllAsync();
            var mealsLower = meals.Where(x => x.Price < price);
            return _mapper.Map<IEnumerable<MealDto>>(mealsLower);
        }

        public async Task<MealDto?> GetByNameAsync(string name)
        {
            var meals = await _mealRepository.GetAllAsync();
            var mealName = meals.Where(x => x.Name == name);
            return _mapper.Map<MealDto?>(mealName);
        }
    }
}
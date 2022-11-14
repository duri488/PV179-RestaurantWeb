using AutoMapper;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IRepository<Drink> _drinkRepository;

        public DrinkService(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, IRepository<Drink> drinkRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _drinkRepository = drinkRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(DrinkDto createdEntity)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
            var drink = _mapper.Map<Drink>(createdEntity);
            _drinkRepository.Insert(drink);
            await unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int entityId)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
            await _drinkRepository.DeleteAsync(entityId);
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<DrinkDto>> GetAllAsync()
        {
            var drinks = await _drinkRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DrinkDto>>(drinks);
        }

        public async Task<DrinkDto?> GetByIdAsync(int entityId)
        {
            var drink = await _drinkRepository.GetByIdAsync(entityId);
            return _mapper.Map<DrinkDto?>(drink);
        }

        public async Task UpdateAsync(int entityId, DrinkDto updatedEntity)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
            var updatedDrink = _mapper.Map<Drink>(updatedEntity);
            _drinkRepository.Update(updatedDrink);
            await unitOfWork.CommitAsync();
        }
    }
}

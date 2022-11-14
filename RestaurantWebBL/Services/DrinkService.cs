using AutoMapper;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDomain;
using RestaurantWebInfrastructure.Repository;
using RestaurantWebInfrastructure.UnitOfWork;

namespace RestaurantWebBL.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Drink> _drinkRepository;

        public DrinkService(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Drink> drinkRepository)
        {
            _unitOfWork = unitOfWork;
            _drinkRepository = drinkRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(DrinkDto createdEntity)
        {
            var drink = _mapper.Map<Drink>(createdEntity);
            _drinkRepository.Insert(drink);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int entityId)
        {
            await _drinkRepository.DeleteAsync(entityId);
            await _unitOfWork.CommitAsync();
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
            var updatedDrink = _mapper.Map<Drink>(updatedEntity);
            _drinkRepository.Update(updatedDrink);

            await _unitOfWork.CommitAsync();
        }
    }
}

using AutoMapper;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;
using System.Diagnostics;

namespace RestaurantWebBL.Services
{
    public class LocalizationService : ILocalizationService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IRepository<Localization> _localizationRepository;

        public LocalizationService(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, IRepository<Localization> localizationRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _localizationRepository = localizationRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(LocalizationDto createdEntity)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
            var localization = _mapper.Map<Localization>(createdEntity);
            _localizationRepository.Insert(localization);
            await unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int entityId)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
            await _localizationRepository.DeleteAsync(entityId);
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<LocalizationDto>> GetAllAsync()
        {
            var localizations = await _localizationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LocalizationDto>>(localizations);
        }

        public async Task<LocalizationDto?> GetByIdAsync(int entityId)
        {
            var localization = await _localizationRepository.GetByIdAsync(entityId);
            return _mapper.Map<LocalizationDto?>(localization);
        }

        public async Task UpdateAsync(int entityId, LocalizationDto updatedEntity)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
            var updatedLocal = _mapper.Map<Localization>(updatedEntity);
            _localizationRepository.Update(updatedLocal);
            await unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<LocalizationDto>> GetAllWithIso(string iso)
        {
            var meals = await _localizationRepository.GetAllAsync();
            var localizationsISO = meals.Where(x => x.IsoLanguageCode == iso);
            return _mapper.Map<IEnumerable<LocalizationDto>>(localizationsISO);
        }

        public async Task<IEnumerable<LocalizationDto>> GetStringWithCode(string stringCode)
        {
            var meals = await _localizationRepository.GetAllAsync();
            var localizationsCode = meals.Where(x => x.StringCode == stringCode);
            return _mapper.Map<IEnumerable<LocalizationDto>>(localizationsCode);
        }
    }
}

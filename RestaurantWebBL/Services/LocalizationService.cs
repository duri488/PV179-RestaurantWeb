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
            var checkExistence = await _localizationRepository.GetAllAsync();
            var existing = checkExistence.Where(x => x.IsoLanguageCode == createdEntity.IsoLanguageCode && x.StringCode == createdEntity.StringCode);
            if (existing.Count() == 0)
            {
                using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
                var localization = _mapper.Map<Localization>(createdEntity);
                _localizationRepository.Insert(localization);
                await unitOfWork.CommitAsync();
            }
            else 
            {
                //Exepcion Type ?
                throw new SystemException(); 
            }
            
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

        public async Task<IEnumerable<LocalizationDto>> GetAllWithIsoAsync(string iso)
        {
            var local = await _localizationRepository.GetAllAsync();
            var localizationsISO = local.Where(x => x.IsoLanguageCode == iso);
            return _mapper.Map<IEnumerable<LocalizationDto>>(localizationsISO);
        }

        public async Task<IEnumerable<LocalizationDto>> GetStringWithCodeAsync(string iso, string stringCode)
        {
            var local = await _localizationRepository.GetAllAsync();
            var localizationsCode = local.Where(x => x.StringCode == stringCode && x.IsoLanguageCode == iso);
            return _mapper.Map<IEnumerable<LocalizationDto>>(localizationsCode);
        }
    }
}

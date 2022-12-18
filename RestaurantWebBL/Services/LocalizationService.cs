using AutoMapper;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.Services
{
    public class LocalizationService : ILocalizationService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IRepository<Localization> _localizationRepository;
        private readonly ILocalizationQueryObject _localizationQueryObject;
        private readonly ILanguageContext _languageContext;
        private const string DefaultLanguageCode = "en";

        public LocalizationService(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper, 
            IRepository<Localization> localizationRepository, ILocalizationQueryObject localizationQueryObject,
            ILanguageContext languageContext)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _localizationRepository = localizationRepository;
            _mapper = mapper;
            _localizationQueryObject = localizationQueryObject;
            _languageContext = languageContext;
        }
        public async Task CreateAsync(LocalizationDto createdEntity)
        {
            var result = _localizationQueryObject.GetStringWithCode(new LocalizationFilterDTOs() { IsoLanguageCode = createdEntity.IsoLanguageCode, StringCode = createdEntity.StringCode});
            if (result.Items.Count() > 0)
            {
                throw new SystemException();

            }
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

        public async Task UpdateAsync(LocalizationDto updatedEntity)
        {
            using IUnitOfWork unitOfWork = _unitOfWorkFactory.Build();
            var updatedLocal = _mapper.Map<Localization>(updatedEntity);
            _localizationRepository.Update(updatedLocal);
            await unitOfWork.CommitAsync();
        }
        public IEnumerable<LocalizationDto> GetAllWithIso(string iso)
        {
            var localizationsISO = _localizationQueryObject.GetStringWithIso(new LocalizationFilterDTOs() { IsoLanguageCode = iso});
            return localizationsISO.Items;
        }

        public string GetStringWithCode( string stringCode)
        {
            var filter = new LocalizationFilterDTOs() {IsoLanguageCode = _languageContext.GetCurrentLanguage(),
                StringCode = stringCode};
            
            LocalizationDto? localizationDto = _localizationQueryObject.GetStringWithCode(filter)
                .Items
                .FirstOrDefault();

            if (localizationDto is not null) return localizationDto.LocalizedString;
            
            filter.StringCode = DefaultLanguageCode;
            localizationDto = _localizationQueryObject.GetStringWithCode(filter)
                .Items
                .FirstOrDefault();
            return localizationDto?.LocalizedString ?? 
                   throw new NotImplementedException(
                       $"Localization for code: {stringCode} - isoCode: {_languageContext.GetCurrentLanguage()} " +
                       $"combination was not found");
        }

        public LocalizationDto? GetDtoWithCode(string stringCode)
        {
            var filter = new LocalizationFilterDTOs() {IsoLanguageCode = _languageContext.GetCurrentLanguage(),
                StringCode = stringCode};
            
            LocalizationDto? localizationDto = _localizationQueryObject.GetStringWithCode(filter)
                .Items
                .FirstOrDefault();

            if (localizationDto is not null) return localizationDto;
            
            filter.StringCode = DefaultLanguageCode;
            localizationDto = _localizationQueryObject.GetStringWithCode(filter)
                .Items
                .FirstOrDefault();
            return localizationDto;
        }
    }
}

using RestaurantWebBL.DTOs;

namespace RestaurantWebBL.Interfaces
{
    public interface ILocalizationService
    {
        Task CreateAsync(LocalizationDto createdEntity);
        Task<LocalizationDto?> GetByIdAsync(int entityId);
        Task UpdateAsync(LocalizationDto updatedEntity);
        Task DeleteAsync(int entityId);
        Task<IEnumerable<LocalizationDto>> GetAllAsync();
        IEnumerable<LocalizationDto?> GetAllWithIso(string iso);
        string GetStringWithCode(string stringCode);
        LocalizationDto? GetDtoWithCode(string stringCode);
    }
}

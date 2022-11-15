using RestaurantWebBL.DTOs;

namespace RestaurantWebBL.Interfaces
{
    public interface ILocalizationService
    {
        Task CreateAsync(LocalizationDto createdEntity);
        Task<LocalizationDto?> GetByIdAsync(int entityId);
        Task UpdateAsync(int entityId, LocalizationDto updatedEntity);
        Task DeleteAsync(int entityId);
        Task<IEnumerable<LocalizationDto>> GetAllAsync();
        Task<IEnumerable<LocalizationDto>> GetAllWithIso(string iso);
        Task<IEnumerable<LocalizationDto>> GetStringWithCode(string iso, string stringCode);
    }
}

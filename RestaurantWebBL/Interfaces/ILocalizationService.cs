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
        Task<IEnumerable<LocalizationDto>> GetAllWithIsoAsync(string iso);
        Task<IEnumerable<LocalizationDto>> GetStringWithCodeAsync(string iso, string stringCode);
    }
}

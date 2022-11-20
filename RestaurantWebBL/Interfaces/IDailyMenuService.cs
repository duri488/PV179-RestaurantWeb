using RestaurantWebBL.DTOs;

namespace RestaurantWebBL.Interfaces;

public interface IDailyMenuService
{
    Task<int> CreateAsync(DailyMenuDto createdEntity);
    Task<DailyMenuDto?> GetByIdAsync(int entityId);
    Task UpdateAsync(DailyMenuDto updatedEntity);
    Task DeleteAsync(int entityId);
    Task<IEnumerable<DailyMenuDto>> GetAllAsync();
}
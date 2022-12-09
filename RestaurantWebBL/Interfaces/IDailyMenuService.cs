using RestaurantWebBL.DTOs;

namespace RestaurantWebBL.Interfaces;

public interface IDailyMenuService
{
    Task<int> CreateAsync(DailyMenuDto createdEntity, int? menuId, int? weeklyMenuId);
    Task<DailyMenuDto?> GetByIdAsync(int entityId);
    Task UpdateAsync(DailyMenuDto updatedEntity, int? menuId, int? weeklyMenuId);
    Task DeleteAsync(int entityId);
    Task<IEnumerable<DailyMenuDto>> GetAllAsync();
    Task<IEnumerable<DailyMenuDto>> GetAllAsync(bool includeNavigationProperties);
}
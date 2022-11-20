using RestaurantWebBL.DTOs;

namespace RestaurantWebBL.Interfaces;

public interface IWeeklyMenuService
{
    Task<int> CreateAsync(WeeklyMenuDto createdEntity, int? mealId, int? restaurantId);
    Task<WeeklyMenuDto?> GetByIdAsync(int entityId);
    Task UpdateAsync(WeeklyMenuDto updatedEntity, int? mealId, int? restaurantId);
    Task DeleteAsync(int entityId);
    Task<IEnumerable<WeeklyMenuDto>> GetAllAsync();
}
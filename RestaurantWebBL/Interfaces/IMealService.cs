using RestaurantWebBL.DTOs;

namespace RestaurantWebBL.Interfaces
{
    public interface IMealService
    {
        Task CreateAsync(MealDto createdEntity);
        Task<MealDto?> GetByIdAsync(int entityId);
        Task UpdateAsync(int entityId, MealDto updatedEntity);
        Task DeleteAsync(int entityId);
        Task<IEnumerable<MealDto>> GetAllAsync();
        Task<IEnumerable<MealDto>> GetMealsPriceIsBigger(decimal price);
        Task<IEnumerable<MealDto>> GetMealsPriceIsLower(decimal price);
        Task<MealDto?> GetByNameAsync(string name);
    }
}

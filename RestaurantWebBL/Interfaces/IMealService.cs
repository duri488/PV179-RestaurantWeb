using RestaurantWebBL.DTOs;

namespace RestaurantWebBL.Interfaces
{
    public interface IMealService
    {
        Task CreateAsync(MealDto createdEntity);
        Task DeleteAsync(int entityId);
        Task<IEnumerable<MealDto>> GetAllAsync();
        Task<MealDto?> GetByIdAsync(int entityId);
        Task UpdateAsync(int entityId, MealDto updatedEntity);
        Task<IEnumerable<MealDto>> GetMealsPriceIsBiggerAsync(decimal price);
        Task<IEnumerable<MealDto>> GetMealsPriceIsLowerAsync(decimal price);
        Task<MealDto?> GetByNameAsync(string name);
    }
}

using RestaurantWebBL.DTOs;

namespace RestaurantWebBL.Interfaces
{
    public interface IMealService
    {
        Task CreateAsync(MealDto createdEntity, int restaurantId);
        Task DeleteAsync(int entityId);
        Task<IEnumerable<MealDto>> GetAllAsync();
        Task<MealDto?> GetByIdAsync(int entityId);
        Task UpdateAsync(MealDto updatedEntity, int restaurantId);
        Task<IEnumerable<MealDto>> GetMealsPriceIsBiggerAsync(decimal price);
        Task<IEnumerable<MealDto>> GetMealsPriceIsLowerAsync(decimal price);
    }
}

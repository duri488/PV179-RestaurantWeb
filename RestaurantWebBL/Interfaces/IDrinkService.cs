using RestaurantWebBL.DTOs;

namespace RestaurantWebBL.Interfaces
{
    public interface IDrinkService
    {
        Task CreateAsync(DrinkDto createdEntity, int restaurantId);
        Task<DrinkDto?> GetByIdAsync(int entityId);
        Task UpdateAsync(DrinkDto updatedEntity, int restaurantId);
        Task DeleteAsync(int entityId);
        Task<IEnumerable<DrinkDto>> GetAllAsync();
    }
}

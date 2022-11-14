using RestaurantWebBL.DTOs;

namespace RestaurantWebBL.Interfaces
{
    public interface IDrinkService
    {
        Task CreateAsync(DrinkDto createdEntity);
        Task<DrinkDto?> GetByIdAsync(int entityId);
        Task UpdateAsync(int entityId, DrinkDto updatedEntity);
        Task DeleteAsync(int entityId);
        Task<IEnumerable<DrinkDto>> GetAllAsync();
    }
}

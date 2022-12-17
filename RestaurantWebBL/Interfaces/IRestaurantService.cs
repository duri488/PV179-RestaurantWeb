using RestaurantWebBL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebBL.Interfaces
{
    public interface IRestaurantService
    {
        Task CreateAsync(RestaurantDto createdEntity);
        Task<RestaurantDto?> GetByIdAsync(int entityId);
        Task UpdateAsync(RestaurantDto updatedEntity);
        Task DeleteAsync(int entityId);
        Task<IEnumerable<RestaurantDto>> GetAllAsync();
        Task<RestaurantDto?> GetFirstAsync();
    }
}

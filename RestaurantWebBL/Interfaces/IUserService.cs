using RestaurantWebBL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebBL.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(UserDto createdEntity);
        Task LogInAsync(UserDto logInEntity);
        Task DeleteAsync(int entityId);
        Task<UserDto?> GetByIdAsync(int entityId);
        Task UpdateAsync(int entityId, UserDto updatedEntity);
    }
}

﻿using RestaurantWebBL.DTOs;

namespace RestaurantWebBL.Interfaces;

public interface IDailyMenuService
{
    Task CreateAsync(DailyMenuDto createdEntity);
    Task<DailyMenuDto?> GetByIdAsync(int entityId);
    Task UpdateAsync(DailyMenuUpdateDto updatedEntity);
    Task DeleteAsync(int entityId);
    Task<IEnumerable<DailyMenuDto>> GetAllAsync();
}
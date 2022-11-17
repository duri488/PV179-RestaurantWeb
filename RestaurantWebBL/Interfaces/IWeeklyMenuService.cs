﻿using RestaurantWebBL.DTOs;

namespace RestaurantWebBL.Interfaces;

public interface IWeeklyMenuService
{
    Task CreateAsync(WeeklyMenuDto createdEntity);
    Task<WeeklyMenuDto?> GetByIdAsync(int entityId);
    Task UpdateAsync(WeeklyMenuUpdateDto updatedEntity);
    Task DeleteAsync(int entityId);
    Task<IEnumerable<WeeklyMenuDto>> GetAllAsync();
}
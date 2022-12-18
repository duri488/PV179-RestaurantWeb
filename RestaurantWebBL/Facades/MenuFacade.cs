using RestaurantWebBL.DTOs;
using RestaurantWebBL.Interfaces;

namespace RestaurantWebBL.Facades;

public class MenuFacade : IMenuFacade
{
    private readonly IMealService _mealService;
    private readonly IWeeklyMenuService _weeklyMenuService;
    private readonly IDailyMenuService _dailyMenuService;

    public MenuFacade(IMealService mealService, IWeeklyMenuService weeklyMenuService, IDailyMenuService dailyMenuService)
    {
        _mealService = mealService;
        _weeklyMenuService = weeklyMenuService;
        _dailyMenuService = dailyMenuService;
    }

    public async Task<IEnumerable<MealDto>> GetAllMealsAsync()
    {
        return await _mealService.GetAllAsync();
    }

    public async Task<MealDto?> GetMealByIdAsync(int id)
    {
        return await _mealService.GetByIdAsync(id);
    }

    public async Task<IEnumerable<DailyMenuDto>> GetAllDailyMenusAsync()
    {
        return await _dailyMenuService.GetAllAsync();
    }

    public async Task<IEnumerable<DailyMenuDto>> GetAllDailyMenusAsync(bool includeNavigationProperty)
    {
        return await _dailyMenuService.GetAllAsync(includeNavigationProperty);
    }

    public async Task<IEnumerable<WeeklyMenuDto>> GetAllWeeklyMenusAsync()
    {
        return await _weeklyMenuService.GetAllAsync();
    }

    public async Task<WeeklyMenuDto?> GetWeeklyMenuByIdAsync(int id)
    {
        return await _weeklyMenuService.GetByIdAsync(id);
    }

    public IEnumerable<WeeklyMenuDto> GetWeeklyMenusByDate(DateTime date)
    {
        return _weeklyMenuService.GetWeeklyMenusByDate(date);
    }

    public async Task<IEnumerable<DailyMenuDto>> GetDailyMenusForWeeklyMenu(int weeklyMenuId, bool includeNavigationProperty)
    {
        var dailyMenuDtos = _dailyMenuService.GetDailyMenusForWeeklyMenu(weeklyMenuId);

        if (!includeNavigationProperty) return dailyMenuDtos;
        
        // I realize this is incredibly ugly and should not be done this way
        // A better solution would be to move the DailyMenuId to Meal and then poll Meals based on the DailyMenuId
        // However due to deadline, I don't have enough time so need a hacky workaround
        List<DailyMenuDto> dailyMenuDtosWithNavigationProperties = new();
        foreach (DailyMenuDto dailyMenuDto in dailyMenuDtos)
        {
            DailyMenuDto? dailyMenuDtoWithProperties = 
                await _dailyMenuService.GetByIdAsync(dailyMenuDto.Id, true);
            if(dailyMenuDtoWithProperties is null) continue;
                
            dailyMenuDtosWithNavigationProperties.Add(dailyMenuDtoWithProperties);
        }

        return dailyMenuDtosWithNavigationProperties;
    }
}
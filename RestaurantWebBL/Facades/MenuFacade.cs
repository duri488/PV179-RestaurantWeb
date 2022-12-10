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
}
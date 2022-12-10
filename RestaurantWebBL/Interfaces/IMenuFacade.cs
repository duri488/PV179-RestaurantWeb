using RestaurantWebBL.DTOs;

namespace RestaurantWebBL.Interfaces;

public interface IMenuFacade
{
    Task<IEnumerable<MealDto>> GetAllMealsAsync();
    Task<IEnumerable<DailyMenuDto>> GetAllDailyMenusAsync();
    Task<IEnumerable<DailyMenuDto>> GetAllDailyMenusAsync(bool includeNavigationProperty);
    Task<IEnumerable<WeeklyMenuDto>> GetAllWeeklyMenusAsync();
}
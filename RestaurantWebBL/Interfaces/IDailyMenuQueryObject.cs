using RestaurantWebBL.DTOs;
using RestaurantWebBL.DTOs.FilterDTOs;

namespace RestaurantWebBL.Interfaces;

public interface IDailyMenuQueryObject
{
    QueryResultDto<DailyMenuDto> DailyMenusAssociatedToWeeklyMenu(DailyMenuFilterDto filter);
}
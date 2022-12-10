using RestaurantWebBL.DTOs;
using RestaurantWebBL.DTOs.FilterDTOs;

namespace RestaurantWebBL.Interfaces;

public interface IWeeklyMenuQueryObject
{
    QueryResultDto<WeeklyMenuDto> WeeklyMenusInDateRange(WeeklyMenuFilterDto filter);
}
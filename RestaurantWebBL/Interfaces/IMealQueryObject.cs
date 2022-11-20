using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebBL.DTOs;

namespace RestaurantWebBL.Interfaces
{
    public interface IMealQueryObject
    {
        public QueryResultDto<MealDto> GetMealByPrice(MealFilterDTOs filter, int bigger);
    }
}

using AutoMapper;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.QueryObjects
{
    public class MealQueryObject : IMealQueryObject
    {
        private IMapper mapper;
        private IQuery<Meal> myQuery;

        public MealQueryObject(IMapper mapper, IQuery<Meal> _mealQuery)
        {
            this.mapper = mapper;
            myQuery = _mealQuery;
        }

        public QueryResultDto<MealDto> GetMealByPrice(MealFilterDTOs filter, int bigger)
        {
            var query = myQuery;
            if ( bigger == 1) {
                query = myQuery
                    .Where<decimal>(a => a > filter.Price, nameof(Meal.Price));
            }
            else
            {
                query = myQuery
                    .Where<decimal>(a => a < filter.Price, nameof(Meal.Price));
            }

            if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
            {
                query = query.OrderBy<string>(filter.SortCriteria, filter.SortAscending);
            }
            if (filter.RequestedPageNumber.HasValue)
            {
                query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            return mapper.Map<QueryResultDto<MealDto>>(query.Execute());
        }

    }
}

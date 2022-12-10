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
        private readonly IMapper _mapper;
        private readonly IQuery<Meal> _mealQuery;

        public MealQueryObject(IMapper mapper, IQuery<Meal> mealQuery)
        {
            this._mapper = mapper;
            _mealQuery = mealQuery;
        }

        public QueryResultDto<MealDto> GetMealByPrice(MealFilterDTOs filter, int bigger)
        {
            var query = _mealQuery;
            if ( bigger == 1) {
                query = _mealQuery
                    .Where<decimal>(a => a > filter.Price, nameof(Meal.Price));
            }
            else
            {
                query = _mealQuery
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

            return _mapper.Map<QueryResultDto<MealDto>>(query.Execute());
        }

    }
}

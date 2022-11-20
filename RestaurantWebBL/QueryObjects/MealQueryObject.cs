using AutoMapper;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebBL.DTOs;
using RestaurantWebDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWebBL.QueryObjects
{
    public class MealQueryObject
    {
        private IMapper mapper;
        private IQuery<Meal> myQuery;

        public MealQueryObject(IMapper mapper, IQuery<Meal> _mealQuery)
        {
            this.mapper = mapper;
            myQuery = _mealQuery;
        }

        public QueryResultDto<MealDto> ExecuteQuery(MealFilterDTOs filter, int bigger)
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

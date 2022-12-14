using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IServiceProvider _serviceProvider;

        public MealQueryObject(IMapper mapper, IServiceProvider serviceProvider)
        {
            this._mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public QueryResultDto<MealDto> GetMealByPrice(MealFilterDTOs filter, int bigger)
        {
            var query = _serviceProvider.GetRequiredService<IQuery<Meal>>();
            if ( bigger == 1) 
            {
                query.Where<decimal>(a => a > filter.Price, nameof(Meal.Price));
            }
            else
            {
                query.Where<decimal>(a => a < filter.Price, nameof(Meal.Price));
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

﻿using AutoMapper;
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
        private readonly IQueryFactory<Meal> _queryFactory;

        public MealQueryObject(IMapper mapper, IQueryFactory<Meal> queryFactory)
        {
            this._mapper = mapper;
            _queryFactory = queryFactory;
        }

        public QueryResultDto<MealDto> GetMealByPrice(MealFilterDTOs filter, int bigger)
        {
            var query = _queryFactory.Build();
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

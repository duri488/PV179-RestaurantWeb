using AutoMapper;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.QueryObjects
{
    public class UserQueryObject
    {
        private IMapper mapper;
        private IQueryFactory<User> _queryFactory;

        public UserQueryObject(IMapper mapper, IQueryFactory<User> queryFactory)
        {
            this.mapper = mapper;
            _queryFactory = queryFactory;
        }

        public QueryResultDto<UserDto> ExecuteQuery(UserFilterDto filter)
        {
            var query = _queryFactory.Build()
                .Where<string>(a => a == filter.Name, nameof(User.Username));
            if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
            {
                query = query.OrderBy<string>(filter.SortCriteria, filter.SortAscending);
            }
            if (filter.RequestedPageNumber.HasValue)
            {
                query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            return mapper.Map<QueryResultDto<UserDto>>(query.Execute());
        }
    }
}

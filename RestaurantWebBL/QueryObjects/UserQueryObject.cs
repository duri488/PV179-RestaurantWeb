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
        private IQuery<User> myQuery;

        public UserQueryObject(IMapper mapper, IQuery<User> _userQuery)
        {
            this.mapper = mapper;
            myQuery = _userQuery;
        }

        public QueryResultDto<UserDto> ExecuteQuery(UserFilterDto filter)
        {
            var query = myQuery
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

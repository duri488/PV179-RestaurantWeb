using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RestaurantWeb.Contract;
using RestaurantWebBL.DTOs;
using RestaurantWebBL.DTOs.FilterDTOs;
using RestaurantWebBL.Interfaces;
using RestaurantWebDAL.Models;

namespace RestaurantWebBL.QueryObjects
{
    public class UserQueryObject : IUserQueryObject
    {
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;

        public UserQueryObject(IMapper mapper, IServiceProvider serviceProvider)
        {
            this._mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public QueryResultDto<UserDto> ExecuteQuery(UserFilterDto filter)
        {
            var query = _serviceProvider.GetRequiredService<IQuery<User>>();

            query.Where<string>(a => a == filter.Name, nameof(User.UserName));
            if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
            {
                query = query.OrderBy<string>(filter.SortCriteria, filter.SortAscending);
            }
            if (filter.RequestedPageNumber.HasValue)
            {
                query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            return _mapper.Map<QueryResultDto<UserDto>>(query.Execute());
        }
    }
}

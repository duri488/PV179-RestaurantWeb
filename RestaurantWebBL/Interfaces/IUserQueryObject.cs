using RestaurantWebBL.DTOs;
using RestaurantWebBL.DTOs.FilterDTOs;

namespace RestaurantWebBL.Interfaces
{
    public interface IUserQueryObject
    {
        public QueryResultDto<UserDto> ExecuteQuery(UserNameFilterDto filter);
    }
}
